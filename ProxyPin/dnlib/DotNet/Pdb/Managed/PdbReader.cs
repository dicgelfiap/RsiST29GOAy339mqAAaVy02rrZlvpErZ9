using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using dnlib.DotNet.Emit;
using dnlib.DotNet.Pdb.Symbols;
using dnlib.DotNet.Pdb.WindowsPdb;
using dnlib.IO;

namespace dnlib.DotNet.Pdb.Managed
{
	// Token: 0x02000954 RID: 2388
	internal sealed class PdbReader : SymbolReader
	{
		// Token: 0x17001365 RID: 4965
		// (get) Token: 0x06005BAA RID: 23466 RVA: 0x001BF054 File Offset: 0x001BF054
		public override PdbFileKind PdbFileKind
		{
			get
			{
				return PdbFileKind.WindowsPDB;
			}
		}

		// Token: 0x17001366 RID: 4966
		// (get) Token: 0x06005BAB RID: 23467 RVA: 0x001BF058 File Offset: 0x001BF058
		// (set) Token: 0x06005BAC RID: 23468 RVA: 0x001BF060 File Offset: 0x001BF060
		private uint Age { get; set; }

		// Token: 0x17001367 RID: 4967
		// (get) Token: 0x06005BAD RID: 23469 RVA: 0x001BF06C File Offset: 0x001BF06C
		// (set) Token: 0x06005BAE RID: 23470 RVA: 0x001BF074 File Offset: 0x001BF074
		private Guid Guid { get; set; }

		// Token: 0x17001368 RID: 4968
		// (get) Token: 0x06005BAF RID: 23471 RVA: 0x001BF080 File Offset: 0x001BF080
		internal bool MatchesModule
		{
			get
			{
				return this.expectedGuid == this.Guid && this.expectedAge == this.Age;
			}
		}

		// Token: 0x06005BB0 RID: 23472 RVA: 0x001BF0A8 File Offset: 0x001BF0A8
		public PdbReader(Guid expectedGuid, uint expectedAge)
		{
			this.expectedGuid = expectedGuid;
			this.expectedAge = expectedAge;
		}

		// Token: 0x06005BB1 RID: 23473 RVA: 0x001BF0C0 File Offset: 0x001BF0C0
		public override void Initialize(ModuleDef module)
		{
			this.module = module;
		}

		// Token: 0x06005BB2 RID: 23474 RVA: 0x001BF0CC File Offset: 0x001BF0CC
		public void Read(DataReader reader)
		{
			try
			{
				this.ReadInternal(ref reader);
			}
			catch (Exception ex)
			{
				if (ex is PdbException)
				{
					throw;
				}
				throw new PdbException(ex);
			}
			finally
			{
				this.streams = null;
				this.names = null;
				this.strings = null;
				this.modules = null;
			}
		}

		// Token: 0x06005BB3 RID: 23475 RVA: 0x001BF134 File Offset: 0x001BF134
		private static uint RoundUpDiv(uint value, uint divisor)
		{
			return (value + divisor - 1U) / divisor;
		}

		// Token: 0x06005BB4 RID: 23476 RVA: 0x001BF140 File Offset: 0x001BF140
		private void ReadInternal(ref DataReader reader)
		{
			if (reader.ReadString(30, Encoding.ASCII) != "Microsoft C/C++ MSF 7.00\r\n\u001aDS\0")
			{
				throw new PdbException("Invalid signature");
			}
			reader.Position += 2U;
			uint num = reader.ReadUInt32();
			reader.ReadUInt32();
			uint num2 = reader.ReadUInt32();
			uint num3 = reader.ReadUInt32();
			reader.ReadUInt32();
			uint num4 = PdbReader.RoundUpDiv(num3, num);
			uint num5 = PdbReader.RoundUpDiv(num4 * 4U, num);
			if (num2 * num != reader.Length)
			{
				throw new PdbException("File size mismatch");
			}
			DataReader[] array = new DataReader[num2];
			uint num6 = 0U;
			for (uint num7 = 0U; num7 < num2; num7 += 1U)
			{
				array[(int)num7] = reader.Slice(num6, num);
				num6 += num;
			}
			DataReader[] array2 = new DataReader[num4];
			int num8 = 0;
			int num9 = 0;
			while ((long)num9 < (long)((ulong)num5) && (long)num8 < (long)((ulong)num4))
			{
				DataReader dataReader = array[(int)reader.ReadUInt32()];
				dataReader.Position = 0U;
				while (dataReader.Position < dataReader.Length && (long)num8 < (long)((ulong)num4))
				{
					array2[num8] = array[(int)dataReader.ReadUInt32()];
					num8++;
				}
				num9++;
			}
			this.ReadRootDirectory(new MsfStream(array2, num3), array, num);
			this.ReadNames();
			if (!this.MatchesModule)
			{
				return;
			}
			this.ReadStringTable();
			ushort? num10 = this.ReadModules();
			this.documents = new Dictionary<string, DbiDocument>(StringComparer.OrdinalIgnoreCase);
			foreach (DbiModule dbiModule in this.modules)
			{
				if (this.IsValidStreamIndex(dbiModule.StreamId))
				{
					dbiModule.LoadFunctions(this, ref this.streams[(int)dbiModule.StreamId].Content);
				}
			}
			if (this.IsValidStreamIndex(num10 ?? 65535))
			{
				this.ApplyRidMap(ref this.streams[(int)num10.Value].Content);
			}
			this.functions = new Dictionary<int, DbiFunction>();
			foreach (DbiModule dbiModule2 in this.modules)
			{
				foreach (DbiFunction dbiFunction in dbiModule2.Functions)
				{
					dbiFunction.reader = this;
					this.functions.Add(dbiFunction.Token, dbiFunction);
				}
			}
			this.sourcelinkData = this.TryGetRawFileData("sourcelink");
			this.srcsrvData = this.TryGetRawFileData("srcsrv");
		}

		// Token: 0x06005BB5 RID: 23477 RVA: 0x001BF454 File Offset: 0x001BF454
		private byte[] TryGetRawFileData(string name)
		{
			uint num;
			if (!this.names.TryGetValue(name, out num))
			{
				return null;
			}
			if (num > 65535U || !this.IsValidStreamIndex((ushort)num))
			{
				return null;
			}
			return this.streams[(int)num].Content.ToArray();
		}

		// Token: 0x06005BB6 RID: 23478 RVA: 0x001BF4AC File Offset: 0x001BF4AC
		private bool IsValidStreamIndex(ushort index)
		{
			return index != ushort.MaxValue && (int)index < this.streams.Length;
		}

		// Token: 0x06005BB7 RID: 23479 RVA: 0x001BF4C8 File Offset: 0x001BF4C8
		private void ReadRootDirectory(MsfStream stream, DataReader[] pages, uint pageSize)
		{
			uint num = stream.Content.ReadUInt32();
			uint[] array = new uint[num];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = stream.Content.ReadUInt32();
			}
			this.streams = new MsfStream[num];
			for (int j = 0; j < array.Length; j++)
			{
				if (array[j] == 4294967295U)
				{
					this.streams[j] = null;
				}
				else
				{
					DataReader[] array2 = new DataReader[PdbReader.RoundUpDiv(array[j], pageSize)];
					for (int k = 0; k < array2.Length; k++)
					{
						array2[k] = pages[(int)stream.Content.ReadUInt32()];
					}
					this.streams[j] = new MsfStream(array2, array[j]);
				}
			}
		}

		// Token: 0x06005BB8 RID: 23480 RVA: 0x001BF59C File Offset: 0x001BF59C
		private void ReadNames()
		{
			ref DataReader ptr = ref this.streams[1].Content;
			ptr.Position = 8U;
			this.Age = ptr.ReadUInt32();
			this.Guid = ptr.ReadGuid();
			uint num = ptr.ReadUInt32();
			DataReader dataReader = ptr.Slice(ptr.Position, num);
			ptr.Position += num;
			ptr.ReadUInt32();
			uint num2 = ptr.ReadUInt32();
			BitArray bitArray = new BitArray(ptr.ReadBytes(ptr.ReadInt32() * 4));
			if (ptr.ReadUInt32() != 0U)
			{
				throw new NotSupportedException();
			}
			this.names = new Dictionary<string, uint>(StringComparer.OrdinalIgnoreCase);
			num2 = Math.Min(num2, (uint)bitArray.Count);
			int num3 = 0;
			while ((long)num3 < (long)((ulong)num2))
			{
				if (bitArray[num3])
				{
					uint position = ptr.ReadUInt32();
					uint value = ptr.ReadUInt32();
					dataReader.Position = position;
					string key = PdbReader.ReadCString(ref dataReader);
					this.names[key] = value;
				}
				num3++;
			}
		}

		// Token: 0x06005BB9 RID: 23481 RVA: 0x001BF6A8 File Offset: 0x001BF6A8
		private void ReadStringTable()
		{
			uint num;
			if (!this.names.TryGetValue("/names", out num))
			{
				throw new PdbException("String table not found");
			}
			ref DataReader ptr = ref this.streams[(int)num].Content;
			ptr.Position = 8U;
			uint num2 = ptr.ReadUInt32();
			DataReader dataReader = ptr.Slice(ptr.Position, num2);
			ptr.Position += num2;
			uint num3 = ptr.ReadUInt32();
			this.strings = new Dictionary<uint, string>((int)num3);
			for (uint num4 = 0U; num4 < num3; num4 += 1U)
			{
				uint num5 = ptr.ReadUInt32();
				if (num5 != 0U)
				{
					dataReader.Position = num5;
					this.strings[num5] = PdbReader.ReadCString(ref dataReader);
				}
			}
		}

		// Token: 0x06005BBA RID: 23482 RVA: 0x001BF76C File Offset: 0x001BF76C
		private static uint ReadSizeField(ref DataReader reader)
		{
			int num = reader.ReadInt32();
			if (num > 0)
			{
				return (uint)num;
			}
			return 0U;
		}

		// Token: 0x06005BBB RID: 23483 RVA: 0x001BF790 File Offset: 0x001BF790
		private ushort? ReadModules()
		{
			ref DataReader ptr = ref this.streams[3].Content;
			this.modules = new List<DbiModule>();
			if (ptr.Length == 0U)
			{
				return null;
			}
			ptr.Position = 20U;
			ushort num = ptr.ReadUInt16();
			ptr.Position += 2U;
			uint num2 = PdbReader.ReadSizeField(ref ptr);
			uint num3 = 0U;
			num3 += PdbReader.ReadSizeField(ref ptr);
			num3 += PdbReader.ReadSizeField(ref ptr);
			num3 += PdbReader.ReadSizeField(ref ptr);
			num3 += PdbReader.ReadSizeField(ref ptr);
			ptr.ReadUInt32();
			uint num4 = PdbReader.ReadSizeField(ref ptr);
			num3 += PdbReader.ReadSizeField(ref ptr);
			ptr.Position += 8U;
			DataReader dataReader = ptr.Slice(ptr.Position, num2);
			while (dataReader.Position < dataReader.Length)
			{
				DbiModule dbiModule = new DbiModule();
				dbiModule.Read(ref dataReader);
				this.modules.Add(dbiModule);
			}
			if (this.IsValidStreamIndex(num))
			{
				this.ReadGlobalSymbols(ref this.streams[(int)num].Content);
			}
			if (num4 != 0U)
			{
				ptr.Position += num2;
				ptr.Position += num3;
				ptr.Position += 12U;
				return new ushort?(ptr.ReadUInt16());
			}
			return null;
		}

		// Token: 0x06005BBC RID: 23484 RVA: 0x001BF8FC File Offset: 0x001BF8FC
		internal DbiDocument GetDocument(uint nameId)
		{
			string text = this.strings[nameId];
			DbiDocument dbiDocument;
			if (!this.documents.TryGetValue(text, out dbiDocument))
			{
				dbiDocument = new DbiDocument(text);
				uint num;
				if (this.names.TryGetValue("/src/files/" + text, out num))
				{
					dbiDocument.Read(ref this.streams[(int)num].Content);
				}
				this.documents.Add(text, dbiDocument);
			}
			return dbiDocument;
		}

		// Token: 0x06005BBD RID: 23485 RVA: 0x001BF978 File Offset: 0x001BF978
		private void ReadGlobalSymbols(ref DataReader reader)
		{
			reader.Position = 0U;
			while (reader.Position < reader.Length)
			{
				ushort num = reader.ReadUInt16();
				uint position = reader.Position + (uint)num;
				if (reader.ReadUInt16() == 4366)
				{
					reader.Position += 4U;
					uint num2 = reader.ReadUInt32();
					reader.Position += 2U;
					if (PdbReader.ReadCString(ref reader) == "COM+_Entry_Point")
					{
						this.entryPt = num2;
						return;
					}
				}
				reader.Position = position;
			}
		}

		// Token: 0x06005BBE RID: 23486 RVA: 0x001BFA0C File Offset: 0x001BFA0C
		private void ApplyRidMap(ref DataReader reader)
		{
			reader.Position = 0U;
			uint[] array = new uint[reader.Length / 4U];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = reader.ReadUInt32();
			}
			foreach (DbiModule dbiModule in this.modules)
			{
				foreach (DbiFunction dbiFunction in dbiModule.Functions)
				{
					uint num = (uint)(dbiFunction.Token & 16777215);
					num = array[(int)num];
					dbiFunction.token = (int)(((long)dbiFunction.Token & (long)((ulong)-16777216)) | (long)((ulong)num));
				}
			}
			if (this.entryPt != 0U)
			{
				uint num2 = this.entryPt & 16777215U;
				num2 = array[(int)num2];
				this.entryPt = ((this.entryPt & 4278190080U) | num2);
			}
		}

		// Token: 0x06005BBF RID: 23487 RVA: 0x001BFB2C File Offset: 0x001BFB2C
		internal static string ReadCString(ref DataReader reader)
		{
			return reader.TryReadZeroTerminatedUtf8String() ?? string.Empty;
		}

		// Token: 0x06005BC0 RID: 23488 RVA: 0x001BFB40 File Offset: 0x001BFB40
		public override SymbolMethod GetMethod(MethodDef method, int version)
		{
			if (version != 1)
			{
				return null;
			}
			DbiFunction result;
			if (this.functions.TryGetValue(method.MDToken.ToInt32(), out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x17001369 RID: 4969
		// (get) Token: 0x06005BC1 RID: 23489 RVA: 0x001BFB80 File Offset: 0x001BFB80
		public override IList<SymbolDocument> Documents
		{
			get
			{
				if (this.documentsResult == null)
				{
					SymbolDocument[] array = new SymbolDocument[this.documents.Count];
					int num = 0;
					foreach (KeyValuePair<string, DbiDocument> keyValuePair in this.documents)
					{
						array[num++] = keyValuePair.Value;
					}
					this.documentsResult = array;
				}
				return this.documentsResult;
			}
		}

		// Token: 0x1700136A RID: 4970
		// (get) Token: 0x06005BC2 RID: 23490 RVA: 0x001BFC14 File Offset: 0x001BFC14
		public override int UserEntryPoint
		{
			get
			{
				return (int)this.entryPt;
			}
		}

		// Token: 0x06005BC3 RID: 23491 RVA: 0x001BFC1C File Offset: 0x001BFC1C
		internal void GetCustomDebugInfos(DbiFunction symMethod, MethodDef method, CilBody body, IList<PdbCustomDebugInfo> result)
		{
			PdbAsyncMethodCustomDebugInfo pdbAsyncMethodCustomDebugInfo = PseudoCustomDebugInfoFactory.TryCreateAsyncMethod(method.Module, method, body, symMethod.AsyncKickoffMethod, symMethod.AsyncStepInfos, symMethod.AsyncCatchHandlerILOffset);
			if (pdbAsyncMethodCustomDebugInfo != null)
			{
				result.Add(pdbAsyncMethodCustomDebugInfo);
			}
			byte[] symAttribute = symMethod.Root.GetSymAttribute("MD2");
			if (symAttribute == null)
			{
				return;
			}
			PdbCustomDebugInfoReader.Read(method, body, result, symAttribute);
		}

		// Token: 0x06005BC4 RID: 23492 RVA: 0x001BFC80 File Offset: 0x001BFC80
		public override void GetCustomDebugInfos(int token, GenericParamContext gpContext, IList<PdbCustomDebugInfo> result)
		{
			if (token == 1)
			{
				this.GetCustomDebugInfos_ModuleDef(result);
			}
		}

		// Token: 0x06005BC5 RID: 23493 RVA: 0x001BFC90 File Offset: 0x001BFC90
		private void GetCustomDebugInfos_ModuleDef(IList<PdbCustomDebugInfo> result)
		{
			if (this.sourcelinkData != null)
			{
				result.Add(new PdbSourceLinkCustomDebugInfo(this.sourcelinkData));
			}
			if (this.srcsrvData != null)
			{
				result.Add(new PdbSourceServerCustomDebugInfo(this.srcsrvData));
			}
		}

		// Token: 0x04002C7C RID: 11388
		private MsfStream[] streams;

		// Token: 0x04002C7D RID: 11389
		private Dictionary<string, uint> names;

		// Token: 0x04002C7E RID: 11390
		private Dictionary<uint, string> strings;

		// Token: 0x04002C7F RID: 11391
		private List<DbiModule> modules;

		// Token: 0x04002C80 RID: 11392
		private ModuleDef module;

		// Token: 0x04002C81 RID: 11393
		private const int STREAM_ROOT = 0;

		// Token: 0x04002C82 RID: 11394
		private const int STREAM_NAMES = 1;

		// Token: 0x04002C83 RID: 11395
		private const int STREAM_TPI = 2;

		// Token: 0x04002C84 RID: 11396
		private const int STREAM_DBI = 3;

		// Token: 0x04002C85 RID: 11397
		private const ushort STREAM_INVALID_INDEX = 65535;

		// Token: 0x04002C86 RID: 11398
		private Dictionary<string, DbiDocument> documents;

		// Token: 0x04002C87 RID: 11399
		private Dictionary<int, DbiFunction> functions;

		// Token: 0x04002C88 RID: 11400
		private byte[] sourcelinkData;

		// Token: 0x04002C89 RID: 11401
		private byte[] srcsrvData;

		// Token: 0x04002C8A RID: 11402
		private uint entryPt;

		// Token: 0x04002C8D RID: 11405
		private readonly Guid expectedGuid;

		// Token: 0x04002C8E RID: 11406
		private readonly uint expectedAge;

		// Token: 0x04002C8F RID: 11407
		private volatile SymbolDocument[] documentsResult;
	}
}
