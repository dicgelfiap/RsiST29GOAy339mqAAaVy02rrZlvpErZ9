using System;
using System.Collections.Generic;
using dnlib.DotNet.Pdb.Symbols;
using dnlib.IO;

namespace dnlib.DotNet.Pdb.Managed
{
	// Token: 0x0200094A RID: 2378
	internal sealed class DbiModule
	{
		// Token: 0x06005B6A RID: 23402 RVA: 0x001BE08C File Offset: 0x001BE08C
		public DbiModule()
		{
			this.Functions = new List<DbiFunction>();
			this.Documents = new List<DbiDocument>();
		}

		// Token: 0x17001351 RID: 4945
		// (get) Token: 0x06005B6B RID: 23403 RVA: 0x001BE0AC File Offset: 0x001BE0AC
		// (set) Token: 0x06005B6C RID: 23404 RVA: 0x001BE0B4 File Offset: 0x001BE0B4
		public ushort StreamId { get; private set; }

		// Token: 0x17001352 RID: 4946
		// (get) Token: 0x06005B6D RID: 23405 RVA: 0x001BE0C0 File Offset: 0x001BE0C0
		// (set) Token: 0x06005B6E RID: 23406 RVA: 0x001BE0C8 File Offset: 0x001BE0C8
		public string ModuleName { get; private set; }

		// Token: 0x17001353 RID: 4947
		// (get) Token: 0x06005B6F RID: 23407 RVA: 0x001BE0D4 File Offset: 0x001BE0D4
		// (set) Token: 0x06005B70 RID: 23408 RVA: 0x001BE0DC File Offset: 0x001BE0DC
		public string ObjectName { get; private set; }

		// Token: 0x17001354 RID: 4948
		// (get) Token: 0x06005B71 RID: 23409 RVA: 0x001BE0E8 File Offset: 0x001BE0E8
		// (set) Token: 0x06005B72 RID: 23410 RVA: 0x001BE0F0 File Offset: 0x001BE0F0
		public List<DbiFunction> Functions { get; private set; }

		// Token: 0x17001355 RID: 4949
		// (get) Token: 0x06005B73 RID: 23411 RVA: 0x001BE0FC File Offset: 0x001BE0FC
		// (set) Token: 0x06005B74 RID: 23412 RVA: 0x001BE104 File Offset: 0x001BE104
		public List<DbiDocument> Documents { get; private set; }

		// Token: 0x06005B75 RID: 23413 RVA: 0x001BE110 File Offset: 0x001BE110
		public void Read(ref DataReader reader)
		{
			reader.Position += 34U;
			this.StreamId = reader.ReadUInt16();
			this.cbSyms = reader.ReadUInt32();
			this.cbOldLines = reader.ReadUInt32();
			this.cbLines = reader.ReadUInt32();
			reader.Position += 16U;
			if (this.cbSyms < 0U)
			{
				this.cbSyms = 0U;
			}
			if (this.cbOldLines < 0U)
			{
				this.cbOldLines = 0U;
			}
			if (this.cbLines < 0U)
			{
				this.cbLines = 0U;
			}
			this.ModuleName = PdbReader.ReadCString(ref reader);
			this.ObjectName = PdbReader.ReadCString(ref reader);
			reader.Position = (reader.Position + 3U & 4294967292U);
		}

		// Token: 0x06005B76 RID: 23414 RVA: 0x001BE1D4 File Offset: 0x001BE1D4
		public void LoadFunctions(PdbReader pdbReader, ref DataReader reader)
		{
			reader.Position = 0U;
			this.ReadFunctions(reader.Slice(reader.Position, this.cbSyms));
			if (this.Functions.Count > 0)
			{
				reader.Position += this.cbSyms + this.cbOldLines;
				this.ReadLines(pdbReader, reader.Slice(reader.Position, this.cbLines));
			}
		}

		// Token: 0x06005B77 RID: 23415 RVA: 0x001BE248 File Offset: 0x001BE248
		private void ReadFunctions(DataReader reader)
		{
			if (reader.ReadUInt32() != 4U)
			{
				throw new PdbException("Invalid signature");
			}
			while (reader.Position < reader.Length)
			{
				ushort num = reader.ReadUInt16();
				uint num2 = reader.Position + (uint)num;
				SymbolType symbolType = (SymbolType)reader.ReadUInt16();
				if (symbolType - SymbolType.S_GMANPROC <= 1)
				{
					DbiFunction dbiFunction = new DbiFunction();
					dbiFunction.Read(ref reader, num2);
					this.Functions.Add(dbiFunction);
				}
				else
				{
					reader.Position = num2;
				}
			}
		}

		// Token: 0x06005B78 RID: 23416 RVA: 0x001BE2D0 File Offset: 0x001BE2D0
		private void ReadLines(PdbReader pdbReader, DataReader reader)
		{
			Dictionary<uint, DbiDocument> documents = new Dictionary<uint, DbiDocument>();
			reader.Position = 0U;
			while (reader.Position < reader.Length)
			{
				int num = (int)reader.ReadUInt32();
				uint num2 = reader.ReadUInt32();
				uint num3 = reader.Position + num2 + 3U & 4294967292U;
				if (num == 244)
				{
					this.ReadFiles(pdbReader, documents, ref reader, num3);
				}
				reader.Position = num3;
			}
			DbiFunction[] array = new DbiFunction[this.Functions.Count];
			this.Functions.CopyTo(array, 0);
			Array.Sort<DbiFunction>(array, (DbiFunction a, DbiFunction b) => a.Address.CompareTo(b.Address));
			reader.Position = 0U;
			while (reader.Position < reader.Length)
			{
				int num4 = (int)reader.ReadUInt32();
				uint num5 = reader.ReadUInt32();
				uint num6 = reader.Position + num5;
				if (num4 == 242)
				{
					this.ReadLines(array, documents, ref reader, num6);
				}
				reader.Position = num6;
			}
		}

		// Token: 0x06005B79 RID: 23417 RVA: 0x001BE3E0 File Offset: 0x001BE3E0
		private void ReadFiles(PdbReader pdbReader, Dictionary<uint, DbiDocument> documents, ref DataReader reader, uint end)
		{
			uint position = reader.Position;
			while (reader.Position < end)
			{
				uint key = reader.Position - position;
				uint nameId = reader.ReadUInt32();
				byte b = reader.ReadByte();
				reader.ReadByte();
				DbiDocument document = pdbReader.GetDocument(nameId);
				documents.Add(key, document);
				reader.Position += (uint)b;
				reader.Position = (reader.Position + 3U & 4294967292U);
			}
		}

		// Token: 0x06005B7A RID: 23418 RVA: 0x001BE458 File Offset: 0x001BE458
		private void ReadLines(DbiFunction[] funcs, Dictionary<uint, DbiDocument> documents, ref DataReader reader, uint end)
		{
			PdbAddress b = PdbAddress.ReadAddress(ref reader);
			int i = 0;
			int num = funcs.Length - 1;
			int j = -1;
			while (i <= num)
			{
				int num2 = i + (num - i >> 1);
				PdbAddress address = funcs[num2].Address;
				if (address < b)
				{
					i = num2 + 1;
				}
				else
				{
					if (!(address > b))
					{
						j = num2;
						break;
					}
					num = num2 - 1;
				}
			}
			if (j == -1)
			{
				return;
			}
			ushort num3 = reader.ReadUInt16();
			reader.Position += 4U;
			if (funcs[j].Lines == null)
			{
				while (j > 0)
				{
					DbiFunction dbiFunction = funcs[j - 1];
					if (dbiFunction != null || dbiFunction.Address != b)
					{
						break;
					}
					j--;
				}
			}
			else
			{
				while (j < funcs.Length - 1 && funcs[j] != null && !(funcs[j + 1].Address != b))
				{
					j++;
				}
			}
			DbiFunction dbiFunction2 = funcs[j];
			if (dbiFunction2.Lines != null)
			{
				return;
			}
			dbiFunction2.Lines = new List<SymbolSequencePoint>();
			while (reader.Position < end)
			{
				DbiDocument document = documents[reader.ReadUInt32()];
				uint num4 = reader.ReadUInt32();
				reader.Position += 4U;
				uint position = reader.Position;
				uint num5 = reader.Position + num4 * 8U;
				for (uint num6 = 0U; num6 < num4; num6 += 1U)
				{
					reader.Position = position + num6 * 8U;
					SymbolSequencePoint symbolSequencePoint = new SymbolSequencePoint
					{
						Document = document
					};
					symbolSequencePoint.Offset = reader.ReadInt32();
					uint num7 = reader.ReadUInt32();
					symbolSequencePoint.Line = (int)(num7 & 16777215U);
					symbolSequencePoint.EndLine = symbolSequencePoint.Line + (int)(num7 >> 24 & 127U);
					if ((num3 & 1) != 0)
					{
						reader.Position = num5 + num6 * 4U;
						symbolSequencePoint.Column = (int)reader.ReadUInt16();
						symbolSequencePoint.EndColumn = (int)reader.ReadUInt16();
					}
					dbiFunction2.Lines.Add(symbolSequencePoint);
				}
			}
		}

		// Token: 0x04002C37 RID: 11319
		private uint cbSyms;

		// Token: 0x04002C38 RID: 11320
		private uint cbOldLines;

		// Token: 0x04002C39 RID: 11321
		private uint cbLines;
	}
}
