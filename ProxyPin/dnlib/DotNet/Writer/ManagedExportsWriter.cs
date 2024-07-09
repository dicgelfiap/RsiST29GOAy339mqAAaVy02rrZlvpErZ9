using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using dnlib.IO;
using dnlib.PE;

namespace dnlib.DotNet.Writer
{
	// Token: 0x020008A8 RID: 2216
	internal sealed class ManagedExportsWriter
	{
		// Token: 0x1700117A RID: 4474
		// (get) Token: 0x060054B8 RID: 21688 RVA: 0x0019C2A8 File Offset: 0x0019C2A8
		private bool Is64Bit
		{
			get
			{
				return this.machine.Is64Bit();
			}
		}

		// Token: 0x1700117B RID: 4475
		// (get) Token: 0x060054B9 RID: 21689 RVA: 0x0019C2B8 File Offset: 0x0019C2B8
		private FileOffset ExportDirOffset
		{
			get
			{
				return this.sdataChunk.FileOffset + this.exportDirOffset;
			}
		}

		// Token: 0x1700117C RID: 4476
		// (get) Token: 0x060054BA RID: 21690 RVA: 0x0019C2CC File Offset: 0x0019C2CC
		private RVA ExportDirRVA
		{
			get
			{
				return this.sdataChunk.RVA + this.exportDirOffset;
			}
		}

		// Token: 0x1700117D RID: 4477
		// (get) Token: 0x060054BB RID: 21691 RVA: 0x0019C2E0 File Offset: 0x0019C2E0
		private uint ExportDirSize
		{
			get
			{
				return 40U;
			}
		}

		// Token: 0x1700117E RID: 4478
		// (get) Token: 0x060054BC RID: 21692 RVA: 0x0019C2E4 File Offset: 0x0019C2E4
		internal bool HasExports
		{
			get
			{
				return this.vtables.Count != 0;
			}
		}

		// Token: 0x060054BD RID: 21693 RVA: 0x0019C2F4 File Offset: 0x0019C2F4
		public ManagedExportsWriter(string moduleName, Machine machine, RelocDirectory relocDirectory, Metadata metadata, PEHeaders peHeaders, Action<string, object[]> logError)
		{
			this.moduleName = moduleName;
			this.machine = machine;
			this.relocDirectory = relocDirectory;
			this.metadata = metadata;
			this.peHeaders = peHeaders;
			this.logError = logError;
			this.vtableFixups = new ManagedExportsWriter.VtableFixupsChunk(this);
			this.stubsChunk = new ManagedExportsWriter.StubsChunk(this);
			this.sdataChunk = new ManagedExportsWriter.SdataChunk(this);
			this.exportDir = new ManagedExportsWriter.ExportDir(this);
			this.vtables = new List<ManagedExportsWriter.VTableInfo>();
			this.allMethodInfos = new List<ManagedExportsWriter.MethodInfo>();
			this.sortedOrdinalMethodInfos = new List<ManagedExportsWriter.MethodInfo>();
			this.sortedNameMethodInfos = new List<ManagedExportsWriter.MethodInfo>();
			CpuArch.TryGetCpuArch(machine, out this.cpuArch);
		}

		// Token: 0x060054BE RID: 21694 RVA: 0x0019C3A4 File Offset: 0x0019C3A4
		internal void AddTextChunks(PESection textSection)
		{
			textSection.Add(this.vtableFixups, 4U);
			if (this.cpuArch != null)
			{
				textSection.Add(this.stubsChunk, this.cpuArch.GetStubAlignment(StubType.Export));
			}
		}

		// Token: 0x060054BF RID: 21695 RVA: 0x0019C3D8 File Offset: 0x0019C3D8
		internal void AddSdataChunks(PESection sdataSection)
		{
			sdataSection.Add(this.sdataChunk, 8U);
		}

		// Token: 0x060054C0 RID: 21696 RVA: 0x0019C3E8 File Offset: 0x0019C3E8
		internal void InitializeChunkProperties()
		{
			if (this.allMethodInfos.Count == 0)
			{
				return;
			}
			this.peHeaders.ExportDirectory = this.exportDir;
			this.peHeaders.ImageCor20Header.VtableFixups = this.vtableFixups;
		}

		// Token: 0x060054C1 RID: 21697 RVA: 0x0019C424 File Offset: 0x0019C424
		internal void AddExportedMethods(List<MethodDef> methods, uint timestamp)
		{
			if (methods.Count == 0)
			{
				return;
			}
			if (this.cpuArch == null)
			{
				this.logError("The module has exported methods but the CPU architecture isn't supported: {0} (0x{1:X4})", new object[]
				{
					this.machine,
					(ushort)this.machine
				});
				return;
			}
			if (methods.Count > 65536)
			{
				this.logError("Too many methods have been exported. No more than 2^16 methods can be exported. Number of exported methods: {0}", new object[]
				{
					methods.Count
				});
				return;
			}
			this.Initialize(methods, timestamp);
		}

		// Token: 0x060054C2 RID: 21698 RVA: 0x0019C4BC File Offset: 0x0019C4BC
		private void Initialize(List<MethodDef> methods, uint timestamp)
		{
			Dictionary<int, List<ManagedExportsWriter.VTableInfo>> dictionary = new Dictionary<int, List<ManagedExportsWriter.VTableInfo>>();
			VTableFlags vtableFlags = this.Is64Bit ? VTableFlags.Bit64 : VTableFlags.Bit32;
			uint num = 0U;
			uint stubAlignment = this.cpuArch.GetStubAlignment(StubType.Export);
			uint stubCodeOffset = this.cpuArch.GetStubCodeOffset(StubType.Export);
			uint stubSize = this.cpuArch.GetStubSize(StubType.Export);
			foreach (MethodDef methodDef in methods)
			{
				MethodExportInfo exportInfo = methodDef.ExportInfo;
				if (exportInfo != null)
				{
					VTableFlags vtableFlags2 = vtableFlags;
					if ((exportInfo.Options & MethodExportInfoOptions.FromUnmanaged) != MethodExportInfoOptions.None)
					{
						vtableFlags2 |= VTableFlags.FromUnmanaged;
					}
					if ((exportInfo.Options & MethodExportInfoOptions.FromUnmanagedRetainAppDomain) != MethodExportInfoOptions.None)
					{
						vtableFlags2 |= VTableFlags.FromUnmanagedRetainAppDomain;
					}
					if ((exportInfo.Options & MethodExportInfoOptions.CallMostDerived) != MethodExportInfoOptions.None)
					{
						vtableFlags2 |= VTableFlags.CallMostDerived;
					}
					List<ManagedExportsWriter.VTableInfo> list;
					if (!dictionary.TryGetValue((int)vtableFlags2, out list))
					{
						dictionary.Add((int)vtableFlags2, list = new List<ManagedExportsWriter.VTableInfo>());
					}
					if (list.Count == 0 || list[list.Count - 1].Methods.Count >= 65535)
					{
						list.Add(new ManagedExportsWriter.VTableInfo(vtableFlags2));
					}
					ManagedExportsWriter.MethodInfo item = new ManagedExportsWriter.MethodInfo(methodDef, num + stubCodeOffset);
					this.allMethodInfos.Add(item);
					list[list.Count - 1].Methods.Add(item);
					num = (num + stubSize + stubAlignment - 1U & ~(stubAlignment - 1U));
				}
			}
			foreach (KeyValuePair<int, List<ManagedExportsWriter.VTableInfo>> keyValuePair in dictionary)
			{
				this.vtables.AddRange(keyValuePair.Value);
			}
			this.WriteSdataBlob(timestamp);
			this.vtableFixups.length = (uint)(this.vtables.Count * 8);
			this.stubsChunk.length = num;
			this.sdataChunk.length = (uint)this.sdataBytesInfo.Data.Length;
			uint num2 = 0U;
			foreach (ManagedExportsWriter.MethodInfo methodInfo in this.allMethodInfos)
			{
				uint num3 = methodInfo.StubChunkOffset - stubCodeOffset;
				if (num2 != num3)
				{
					throw new InvalidOperationException();
				}
				this.cpuArch.WriteStubRelocs(StubType.Export, this.relocDirectory, this.stubsChunk, num3);
				num2 = (num3 + stubSize + stubAlignment - 1U & ~(stubAlignment - 1U));
			}
			if (num2 != num)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x060054C3 RID: 21699 RVA: 0x0019C790 File Offset: 0x0019C790
		private void WriteSdataBlob(uint timestamp)
		{
			MemoryStream memoryStream = new MemoryStream();
			DataWriter dataWriter = new DataWriter(memoryStream);
			foreach (ManagedExportsWriter.VTableInfo vtableInfo in this.vtables)
			{
				vtableInfo.SdataChunkOffset = (uint)dataWriter.Position;
				foreach (ManagedExportsWriter.MethodInfo methodInfo in vtableInfo.Methods)
				{
					methodInfo.ManagedVtblOffset = (uint)dataWriter.Position;
					dataWriter.WriteUInt32(100663296U + this.metadata.GetRid(methodInfo.Method));
					if ((vtableInfo.Flags & VTableFlags.Bit64) != (VTableFlags)0)
					{
						dataWriter.WriteUInt32(0U);
					}
				}
			}
			ManagedExportsWriter.NamesBlob namesBlob = new ManagedExportsWriter.NamesBlob(false);
			int num = 0;
			foreach (ManagedExportsWriter.MethodInfo methodInfo2 in this.allMethodInfos)
			{
				MethodExportInfo exportInfo = methodInfo2.Method.ExportInfo;
				string text = exportInfo.Name;
				if (text == null)
				{
					if (exportInfo.Ordinal != null)
					{
						this.sortedOrdinalMethodInfos.Add(methodInfo2);
						continue;
					}
					text = methodInfo2.Method.Name;
				}
				if (string.IsNullOrEmpty(text))
				{
					this.logError("Exported method name is null or empty, method: {0} (0x{1:X8})", new object[]
					{
						methodInfo2.Method,
						methodInfo2.Method.MDToken.Raw
					});
				}
				else
				{
					methodInfo2.NameOffset = namesBlob.GetMethodNameOffset(text, out methodInfo2.NameBytes);
					methodInfo2.NameIndex = num++;
					this.sortedNameMethodInfos.Add(methodInfo2);
				}
			}
			this.sdataBytesInfo.MethodNameOffsets = namesBlob.GetMethodNameOffsets();
			this.sdataBytesInfo.moduleNameOffset = namesBlob.GetOtherNameOffset(this.moduleName);
			this.sortedOrdinalMethodInfos.Sort((ManagedExportsWriter.MethodInfo a, ManagedExportsWriter.MethodInfo b) => a.Method.ExportInfo.Ordinal.Value.CompareTo(b.Method.ExportInfo.Ordinal.Value));
			this.sortedNameMethodInfos.Sort((ManagedExportsWriter.MethodInfo a, ManagedExportsWriter.MethodInfo b) => ManagedExportsWriter.CompareTo(a.NameBytes, b.NameBytes));
			int num2;
			int num3;
			if (this.sortedOrdinalMethodInfos.Count == 0)
			{
				num2 = 0;
				num3 = 0;
			}
			else
			{
				num2 = (int)this.sortedOrdinalMethodInfos[0].Method.ExportInfo.Ordinal.Value;
				num3 = (int)(this.sortedOrdinalMethodInfos[this.sortedOrdinalMethodInfos.Count - 1].Method.ExportInfo.Ordinal.Value + 1);
			}
			int num4 = num3 - num2;
			int num5 = 0;
			for (int i = 0; i < this.sortedOrdinalMethodInfos.Count; i++)
			{
				int num6 = (int)this.sortedOrdinalMethodInfos[i].Method.ExportInfo.Ordinal.Value - num2;
				this.sortedOrdinalMethodInfos[i].FunctionIndex = num6;
				num5 = num6;
			}
			for (int j = 0; j < this.sortedNameMethodInfos.Count; j++)
			{
				num5 = num4 + j;
				this.sortedNameMethodInfos[j].FunctionIndex = num5;
			}
			int num7 = num5 + 1;
			if (num7 > 65536)
			{
				this.logError("Exported function array is too big", Array2.Empty<object>());
				return;
			}
			this.exportDirOffset = (uint)dataWriter.Position;
			dataWriter.WriteUInt32(0U);
			dataWriter.WriteUInt32(timestamp);
			dataWriter.WriteUInt32(0U);
			this.sdataBytesInfo.exportDirModuleNameStreamOffset = (uint)dataWriter.Position;
			dataWriter.WriteUInt32(0U);
			dataWriter.WriteInt32(num2);
			dataWriter.WriteUInt32((uint)num7);
			dataWriter.WriteInt32(this.sdataBytesInfo.MethodNameOffsets.Length);
			this.sdataBytesInfo.exportDirAddressOfFunctionsStreamOffset = (uint)dataWriter.Position;
			dataWriter.WriteUInt32(0U);
			dataWriter.WriteUInt32(0U);
			dataWriter.WriteUInt32(0U);
			this.sdataBytesInfo.addressOfFunctionsStreamOffset = (uint)dataWriter.Position;
			dataWriter.WriteZeroes(num7 * 4);
			this.sdataBytesInfo.addressOfNamesStreamOffset = (uint)dataWriter.Position;
			dataWriter.WriteZeroes(this.sdataBytesInfo.MethodNameOffsets.Length * 4);
			this.sdataBytesInfo.addressOfNameOrdinalsStreamOffset = (uint)dataWriter.Position;
			dataWriter.WriteZeroes(this.sdataBytesInfo.MethodNameOffsets.Length * 2);
			this.sdataBytesInfo.namesBlobStreamOffset = (uint)dataWriter.Position;
			namesBlob.Write(dataWriter);
			this.sdataBytesInfo.Data = memoryStream.ToArray();
		}

		// Token: 0x060054C4 RID: 21700 RVA: 0x0019CC88 File Offset: 0x0019CC88
		private void WriteSdata(DataWriter writer)
		{
			if (this.sdataBytesInfo.Data == null)
			{
				return;
			}
			this.PatchSdataBytesBlob();
			writer.WriteBytes(this.sdataBytesInfo.Data);
		}

		// Token: 0x060054C5 RID: 21701 RVA: 0x0019CCB4 File Offset: 0x0019CCB4
		private void PatchSdataBytesBlob()
		{
			uint rva = (uint)this.sdataChunk.RVA;
			uint num = rva + this.sdataBytesInfo.namesBlobStreamOffset;
			DataWriter dataWriter = new DataWriter(new MemoryStream(this.sdataBytesInfo.Data));
			dataWriter.Position = (long)((ulong)this.sdataBytesInfo.exportDirModuleNameStreamOffset);
			dataWriter.WriteUInt32(num + this.sdataBytesInfo.moduleNameOffset);
			dataWriter.Position = (long)((ulong)this.sdataBytesInfo.exportDirAddressOfFunctionsStreamOffset);
			dataWriter.WriteUInt32(rva + this.sdataBytesInfo.addressOfFunctionsStreamOffset);
			if (this.sdataBytesInfo.MethodNameOffsets.Length != 0)
			{
				dataWriter.WriteUInt32(rva + this.sdataBytesInfo.addressOfNamesStreamOffset);
				dataWriter.WriteUInt32(rva + this.sdataBytesInfo.addressOfNameOrdinalsStreamOffset);
			}
			uint rva2 = (uint)this.stubsChunk.RVA;
			dataWriter.Position = (long)((ulong)this.sdataBytesInfo.addressOfFunctionsStreamOffset);
			int num2 = 0;
			foreach (ManagedExportsWriter.MethodInfo methodInfo in this.sortedOrdinalMethodInfos)
			{
				int num3 = methodInfo.FunctionIndex - num2;
				if (num3 < 0)
				{
					throw new InvalidOperationException();
				}
				while (num3-- > 0)
				{
					dataWriter.WriteInt32(0);
				}
				dataWriter.WriteUInt32(rva2 + methodInfo.StubChunkOffset);
				num2 = methodInfo.FunctionIndex + 1;
			}
			foreach (ManagedExportsWriter.MethodInfo methodInfo2 in this.sortedNameMethodInfos)
			{
				if (methodInfo2.FunctionIndex != num2++)
				{
					throw new InvalidOperationException();
				}
				dataWriter.WriteUInt32(rva2 + methodInfo2.StubChunkOffset);
			}
			uint[] methodNameOffsets = this.sdataBytesInfo.MethodNameOffsets;
			if (methodNameOffsets.Length != 0)
			{
				dataWriter.Position = (long)((ulong)this.sdataBytesInfo.addressOfNamesStreamOffset);
				foreach (ManagedExportsWriter.MethodInfo methodInfo3 in this.sortedNameMethodInfos)
				{
					dataWriter.WriteUInt32(num + methodNameOffsets[methodInfo3.NameIndex]);
				}
				dataWriter.Position = (long)((ulong)this.sdataBytesInfo.addressOfNameOrdinalsStreamOffset);
				foreach (ManagedExportsWriter.MethodInfo methodInfo4 in this.sortedNameMethodInfos)
				{
					dataWriter.WriteUInt16((ushort)methodInfo4.FunctionIndex);
				}
			}
		}

		// Token: 0x060054C6 RID: 21702 RVA: 0x0019CF6C File Offset: 0x0019CF6C
		private void WriteVtableFixups(DataWriter writer)
		{
			if (this.vtables.Count == 0)
			{
				return;
			}
			foreach (ManagedExportsWriter.VTableInfo vtableInfo in this.vtables)
			{
				writer.WriteUInt32((uint)(this.sdataChunk.RVA + vtableInfo.SdataChunkOffset));
				writer.WriteUInt16((ushort)vtableInfo.Methods.Count);
				writer.WriteUInt16((ushort)vtableInfo.Flags);
			}
		}

		// Token: 0x060054C7 RID: 21703 RVA: 0x0019D008 File Offset: 0x0019D008
		private void WriteStubs(DataWriter writer)
		{
			if (this.vtables.Count == 0)
			{
				return;
			}
			if (this.cpuArch == null)
			{
				return;
			}
			ulong imageBase = this.peHeaders.ImageBase;
			uint rva = (uint)this.stubsChunk.RVA;
			uint rva2 = (uint)this.sdataChunk.RVA;
			uint num = 0U;
			uint stubCodeOffset = this.cpuArch.GetStubCodeOffset(StubType.Export);
			uint stubSize = this.cpuArch.GetStubSize(StubType.Export);
			uint stubAlignment = this.cpuArch.GetStubAlignment(StubType.Export);
			int num2 = (int)((stubSize + stubAlignment - 1U & ~(int)(stubAlignment - 1U)) - stubSize);
			foreach (ManagedExportsWriter.MethodInfo methodInfo in this.allMethodInfos)
			{
				uint num3 = methodInfo.StubChunkOffset - stubCodeOffset;
				if (num != num3)
				{
					throw new InvalidOperationException();
				}
				ulong position = (ulong)writer.Position;
				this.cpuArch.WriteStub(StubType.Export, writer, imageBase, rva + num3, rva2 + methodInfo.ManagedVtblOffset);
				if (position + (ulong)stubSize != (ulong)writer.Position)
				{
					throw new InvalidOperationException();
				}
				if (num2 != 0)
				{
					writer.WriteZeroes(num2);
				}
				num = (num3 + stubSize + stubAlignment - 1U & ~(stubAlignment - 1U));
			}
			if (num != this.stubsChunk.length)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x060054C8 RID: 21704 RVA: 0x0019D168 File Offset: 0x0019D168
		private static int CompareTo(byte[] a, byte[] b)
		{
			if (a == b)
			{
				return 0;
			}
			int num = Math.Min(a.Length, b.Length);
			for (int i = 0; i < num; i++)
			{
				int num2 = (int)(a[i] - b[i]);
				if (num2 != 0)
				{
					return num2;
				}
			}
			return a.Length - b.Length;
		}

		// Token: 0x040028A2 RID: 10402
		private const uint DEFAULT_VTBL_FIXUPS_ALIGNMENT = 4U;

		// Token: 0x040028A3 RID: 10403
		private const uint DEFAULT_SDATA_ALIGNMENT = 8U;

		// Token: 0x040028A4 RID: 10404
		private const StubType stubType = StubType.Export;

		// Token: 0x040028A5 RID: 10405
		private readonly string moduleName;

		// Token: 0x040028A6 RID: 10406
		private readonly Machine machine;

		// Token: 0x040028A7 RID: 10407
		private readonly RelocDirectory relocDirectory;

		// Token: 0x040028A8 RID: 10408
		private readonly Metadata metadata;

		// Token: 0x040028A9 RID: 10409
		private readonly PEHeaders peHeaders;

		// Token: 0x040028AA RID: 10410
		private readonly Action<string, object[]> logError;

		// Token: 0x040028AB RID: 10411
		private readonly ManagedExportsWriter.VtableFixupsChunk vtableFixups;

		// Token: 0x040028AC RID: 10412
		private readonly ManagedExportsWriter.StubsChunk stubsChunk;

		// Token: 0x040028AD RID: 10413
		private readonly ManagedExportsWriter.SdataChunk sdataChunk;

		// Token: 0x040028AE RID: 10414
		private readonly ManagedExportsWriter.ExportDir exportDir;

		// Token: 0x040028AF RID: 10415
		private readonly List<ManagedExportsWriter.VTableInfo> vtables;

		// Token: 0x040028B0 RID: 10416
		private readonly List<ManagedExportsWriter.MethodInfo> allMethodInfos;

		// Token: 0x040028B1 RID: 10417
		private readonly List<ManagedExportsWriter.MethodInfo> sortedOrdinalMethodInfos;

		// Token: 0x040028B2 RID: 10418
		private readonly List<ManagedExportsWriter.MethodInfo> sortedNameMethodInfos;

		// Token: 0x040028B3 RID: 10419
		private readonly CpuArch cpuArch;

		// Token: 0x040028B4 RID: 10420
		private uint exportDirOffset;

		// Token: 0x040028B5 RID: 10421
		private ManagedExportsWriter.SdataBytesInfo sdataBytesInfo;

		// Token: 0x02001010 RID: 4112
		private sealed class ExportDir : IChunk
		{
			// Token: 0x17001DF9 RID: 7673
			// (get) Token: 0x06008F12 RID: 36626 RVA: 0x002AB5FC File Offset: 0x002AB5FC
			public FileOffset FileOffset
			{
				get
				{
					return this.owner.ExportDirOffset;
				}
			}

			// Token: 0x17001DFA RID: 7674
			// (get) Token: 0x06008F13 RID: 36627 RVA: 0x002AB60C File Offset: 0x002AB60C
			public RVA RVA
			{
				get
				{
					return this.owner.ExportDirRVA;
				}
			}

			// Token: 0x06008F14 RID: 36628 RVA: 0x002AB61C File Offset: 0x002AB61C
			public ExportDir(ManagedExportsWriter owner)
			{
				this.owner = owner;
			}

			// Token: 0x06008F15 RID: 36629 RVA: 0x002AB62C File Offset: 0x002AB62C
			void IChunk.SetOffset(FileOffset offset, RVA rva)
			{
				throw new NotSupportedException();
			}

			// Token: 0x06008F16 RID: 36630 RVA: 0x002AB634 File Offset: 0x002AB634
			public uint GetFileLength()
			{
				return this.owner.ExportDirSize;
			}

			// Token: 0x06008F17 RID: 36631 RVA: 0x002AB644 File Offset: 0x002AB644
			public uint GetVirtualSize()
			{
				return this.GetFileLength();
			}

			// Token: 0x06008F18 RID: 36632 RVA: 0x002AB64C File Offset: 0x002AB64C
			void IChunk.WriteTo(DataWriter writer)
			{
				throw new NotSupportedException();
			}

			// Token: 0x0400446D RID: 17517
			private readonly ManagedExportsWriter owner;
		}

		// Token: 0x02001011 RID: 4113
		private sealed class VtableFixupsChunk : IChunk
		{
			// Token: 0x17001DFB RID: 7675
			// (get) Token: 0x06008F19 RID: 36633 RVA: 0x002AB654 File Offset: 0x002AB654
			public FileOffset FileOffset
			{
				get
				{
					return this.offset;
				}
			}

			// Token: 0x17001DFC RID: 7676
			// (get) Token: 0x06008F1A RID: 36634 RVA: 0x002AB65C File Offset: 0x002AB65C
			public RVA RVA
			{
				get
				{
					return this.rva;
				}
			}

			// Token: 0x06008F1B RID: 36635 RVA: 0x002AB664 File Offset: 0x002AB664
			public VtableFixupsChunk(ManagedExportsWriter owner)
			{
				this.owner = owner;
			}

			// Token: 0x06008F1C RID: 36636 RVA: 0x002AB674 File Offset: 0x002AB674
			public void SetOffset(FileOffset offset, RVA rva)
			{
				this.offset = offset;
				this.rva = rva;
			}

			// Token: 0x06008F1D RID: 36637 RVA: 0x002AB684 File Offset: 0x002AB684
			public uint GetFileLength()
			{
				return this.length;
			}

			// Token: 0x06008F1E RID: 36638 RVA: 0x002AB68C File Offset: 0x002AB68C
			public uint GetVirtualSize()
			{
				return this.GetFileLength();
			}

			// Token: 0x06008F1F RID: 36639 RVA: 0x002AB694 File Offset: 0x002AB694
			public void WriteTo(DataWriter writer)
			{
				this.owner.WriteVtableFixups(writer);
			}

			// Token: 0x0400446E RID: 17518
			private readonly ManagedExportsWriter owner;

			// Token: 0x0400446F RID: 17519
			private FileOffset offset;

			// Token: 0x04004470 RID: 17520
			private RVA rva;

			// Token: 0x04004471 RID: 17521
			internal uint length;
		}

		// Token: 0x02001012 RID: 4114
		private sealed class StubsChunk : IChunk
		{
			// Token: 0x17001DFD RID: 7677
			// (get) Token: 0x06008F20 RID: 36640 RVA: 0x002AB6A4 File Offset: 0x002AB6A4
			public FileOffset FileOffset
			{
				get
				{
					return this.offset;
				}
			}

			// Token: 0x17001DFE RID: 7678
			// (get) Token: 0x06008F21 RID: 36641 RVA: 0x002AB6AC File Offset: 0x002AB6AC
			public RVA RVA
			{
				get
				{
					return this.rva;
				}
			}

			// Token: 0x06008F22 RID: 36642 RVA: 0x002AB6B4 File Offset: 0x002AB6B4
			public StubsChunk(ManagedExportsWriter owner)
			{
				this.owner = owner;
			}

			// Token: 0x06008F23 RID: 36643 RVA: 0x002AB6C4 File Offset: 0x002AB6C4
			public void SetOffset(FileOffset offset, RVA rva)
			{
				this.offset = offset;
				this.rva = rva;
			}

			// Token: 0x06008F24 RID: 36644 RVA: 0x002AB6D4 File Offset: 0x002AB6D4
			public uint GetFileLength()
			{
				return this.length;
			}

			// Token: 0x06008F25 RID: 36645 RVA: 0x002AB6DC File Offset: 0x002AB6DC
			public uint GetVirtualSize()
			{
				return this.GetFileLength();
			}

			// Token: 0x06008F26 RID: 36646 RVA: 0x002AB6E4 File Offset: 0x002AB6E4
			public void WriteTo(DataWriter writer)
			{
				this.owner.WriteStubs(writer);
			}

			// Token: 0x04004472 RID: 17522
			private readonly ManagedExportsWriter owner;

			// Token: 0x04004473 RID: 17523
			private FileOffset offset;

			// Token: 0x04004474 RID: 17524
			private RVA rva;

			// Token: 0x04004475 RID: 17525
			internal uint length;
		}

		// Token: 0x02001013 RID: 4115
		private sealed class SdataChunk : IChunk
		{
			// Token: 0x17001DFF RID: 7679
			// (get) Token: 0x06008F27 RID: 36647 RVA: 0x002AB6F4 File Offset: 0x002AB6F4
			public FileOffset FileOffset
			{
				get
				{
					return this.offset;
				}
			}

			// Token: 0x17001E00 RID: 7680
			// (get) Token: 0x06008F28 RID: 36648 RVA: 0x002AB6FC File Offset: 0x002AB6FC
			public RVA RVA
			{
				get
				{
					return this.rva;
				}
			}

			// Token: 0x06008F29 RID: 36649 RVA: 0x002AB704 File Offset: 0x002AB704
			public SdataChunk(ManagedExportsWriter owner)
			{
				this.owner = owner;
			}

			// Token: 0x06008F2A RID: 36650 RVA: 0x002AB714 File Offset: 0x002AB714
			public void SetOffset(FileOffset offset, RVA rva)
			{
				this.offset = offset;
				this.rva = rva;
			}

			// Token: 0x06008F2B RID: 36651 RVA: 0x002AB724 File Offset: 0x002AB724
			public uint GetFileLength()
			{
				return this.length;
			}

			// Token: 0x06008F2C RID: 36652 RVA: 0x002AB72C File Offset: 0x002AB72C
			public uint GetVirtualSize()
			{
				return this.GetFileLength();
			}

			// Token: 0x06008F2D RID: 36653 RVA: 0x002AB734 File Offset: 0x002AB734
			public void WriteTo(DataWriter writer)
			{
				this.owner.WriteSdata(writer);
			}

			// Token: 0x04004476 RID: 17526
			private readonly ManagedExportsWriter owner;

			// Token: 0x04004477 RID: 17527
			private FileOffset offset;

			// Token: 0x04004478 RID: 17528
			private RVA rva;

			// Token: 0x04004479 RID: 17529
			internal uint length;
		}

		// Token: 0x02001014 RID: 4116
		private sealed class MethodInfo
		{
			// Token: 0x06008F2E RID: 36654 RVA: 0x002AB744 File Offset: 0x002AB744
			public MethodInfo(MethodDef method, uint stubChunkOffset)
			{
				this.Method = method;
				this.StubChunkOffset = stubChunkOffset;
			}

			// Token: 0x0400447A RID: 17530
			public readonly MethodDef Method;

			// Token: 0x0400447B RID: 17531
			public readonly uint StubChunkOffset;

			// Token: 0x0400447C RID: 17532
			public int FunctionIndex;

			// Token: 0x0400447D RID: 17533
			public uint ManagedVtblOffset;

			// Token: 0x0400447E RID: 17534
			public uint NameOffset;

			// Token: 0x0400447F RID: 17535
			public int NameIndex;

			// Token: 0x04004480 RID: 17536
			public byte[] NameBytes;
		}

		// Token: 0x02001015 RID: 4117
		private sealed class VTableInfo
		{
			// Token: 0x17001E01 RID: 7681
			// (get) Token: 0x06008F2F RID: 36655 RVA: 0x002AB75C File Offset: 0x002AB75C
			// (set) Token: 0x06008F30 RID: 36656 RVA: 0x002AB764 File Offset: 0x002AB764
			public uint SdataChunkOffset { get; set; }

			// Token: 0x06008F31 RID: 36657 RVA: 0x002AB770 File Offset: 0x002AB770
			public VTableInfo(VTableFlags flags)
			{
				this.Flags = flags;
				this.Methods = new List<ManagedExportsWriter.MethodInfo>();
			}

			// Token: 0x04004482 RID: 17538
			public readonly VTableFlags Flags;

			// Token: 0x04004483 RID: 17539
			public readonly List<ManagedExportsWriter.MethodInfo> Methods;
		}

		// Token: 0x02001016 RID: 4118
		private struct NamesBlob
		{
			// Token: 0x17001E02 RID: 7682
			// (get) Token: 0x06008F32 RID: 36658 RVA: 0x002AB78C File Offset: 0x002AB78C
			public int MethodNamesCount
			{
				get
				{
					return this.methodNamesCount;
				}
			}

			// Token: 0x06008F33 RID: 36659 RVA: 0x002AB794 File Offset: 0x002AB794
			public NamesBlob(bool dummy)
			{
				this.nameOffsets = new Dictionary<string, ManagedExportsWriter.NamesBlob.NameInfo>(StringComparer.Ordinal);
				this.names = new List<byte[]>();
				this.methodNameOffsets = new List<uint>();
				this.currentOffset = 0U;
				this.methodNamesCount = 0;
				this.methodNamesIsFrozen = false;
			}

			// Token: 0x06008F34 RID: 36660 RVA: 0x002AB7D4 File Offset: 0x002AB7D4
			public uint GetMethodNameOffset(string name, out byte[] bytes)
			{
				if (this.methodNamesIsFrozen)
				{
					throw new InvalidOperationException();
				}
				this.methodNamesCount++;
				uint offset = this.GetOffset(name, out bytes);
				this.methodNameOffsets.Add(offset);
				return offset;
			}

			// Token: 0x06008F35 RID: 36661 RVA: 0x002AB81C File Offset: 0x002AB81C
			public uint GetOtherNameOffset(string name)
			{
				this.methodNamesIsFrozen = true;
				byte[] array;
				return this.GetOffset(name, out array);
			}

			// Token: 0x06008F36 RID: 36662 RVA: 0x002AB840 File Offset: 0x002AB840
			private uint GetOffset(string name, out byte[] bytes)
			{
				ManagedExportsWriter.NamesBlob.NameInfo nameInfo;
				if (this.nameOffsets.TryGetValue(name, out nameInfo))
				{
					bytes = nameInfo.Bytes;
					return nameInfo.Offset;
				}
				bytes = ManagedExportsWriter.NamesBlob.GetNameASCIIZ(name);
				this.names.Add(bytes);
				uint num = this.currentOffset;
				this.nameOffsets.Add(name, new ManagedExportsWriter.NamesBlob.NameInfo(num, bytes));
				this.currentOffset += (uint)bytes.Length;
				return num;
			}

			// Token: 0x06008F37 RID: 36663 RVA: 0x002AB8B8 File Offset: 0x002AB8B8
			private static byte[] GetNameASCIIZ(string name)
			{
				byte[] array = new byte[Encoding.UTF8.GetByteCount(name) + 1];
				Encoding.UTF8.GetBytes(name, 0, name.Length, array, 0);
				if (array[array.Length - 1] != 0)
				{
					throw new ModuleWriterException();
				}
				return array;
			}

			// Token: 0x06008F38 RID: 36664 RVA: 0x002AB904 File Offset: 0x002AB904
			public void Write(DataWriter writer)
			{
				foreach (byte[] source in this.names)
				{
					writer.WriteBytes(source);
				}
			}

			// Token: 0x06008F39 RID: 36665 RVA: 0x002AB960 File Offset: 0x002AB960
			public uint[] GetMethodNameOffsets()
			{
				return this.methodNameOffsets.ToArray();
			}

			// Token: 0x04004484 RID: 17540
			private readonly Dictionary<string, ManagedExportsWriter.NamesBlob.NameInfo> nameOffsets;

			// Token: 0x04004485 RID: 17541
			private readonly List<byte[]> names;

			// Token: 0x04004486 RID: 17542
			private readonly List<uint> methodNameOffsets;

			// Token: 0x04004487 RID: 17543
			private uint currentOffset;

			// Token: 0x04004488 RID: 17544
			private int methodNamesCount;

			// Token: 0x04004489 RID: 17545
			private bool methodNamesIsFrozen;

			// Token: 0x0200120D RID: 4621
			private readonly struct NameInfo
			{
				// Token: 0x06009697 RID: 38551 RVA: 0x002CC6C4 File Offset: 0x002CC6C4
				public NameInfo(uint offset, byte[] bytes)
				{
					this.Offset = offset;
					this.Bytes = bytes;
				}

				// Token: 0x04004F11 RID: 20241
				public readonly uint Offset;

				// Token: 0x04004F12 RID: 20242
				public readonly byte[] Bytes;
			}
		}

		// Token: 0x02001017 RID: 4119
		private struct SdataBytesInfo
		{
			// Token: 0x0400448A RID: 17546
			public byte[] Data;

			// Token: 0x0400448B RID: 17547
			public uint namesBlobStreamOffset;

			// Token: 0x0400448C RID: 17548
			public uint moduleNameOffset;

			// Token: 0x0400448D RID: 17549
			public uint exportDirModuleNameStreamOffset;

			// Token: 0x0400448E RID: 17550
			public uint exportDirAddressOfFunctionsStreamOffset;

			// Token: 0x0400448F RID: 17551
			public uint addressOfFunctionsStreamOffset;

			// Token: 0x04004490 RID: 17552
			public uint addressOfNamesStreamOffset;

			// Token: 0x04004491 RID: 17553
			public uint addressOfNameOrdinalsStreamOffset;

			// Token: 0x04004492 RID: 17554
			public uint[] MethodNameOffsets;
		}
	}
}
