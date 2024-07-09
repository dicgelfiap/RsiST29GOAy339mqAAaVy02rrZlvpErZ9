using System;
using System.Runtime.InteropServices;
using System.Text;
using dnlib.IO;
using dnlib.PE;

namespace dnlib.DotNet.Writer
{
	// Token: 0x020008A5 RID: 2213
	[ComVisible(true)]
	public sealed class ImportDirectory : IChunk
	{
		// Token: 0x17001171 RID: 4465
		// (get) Token: 0x060054A1 RID: 21665 RVA: 0x0019BFE8 File Offset: 0x0019BFE8
		// (set) Token: 0x060054A2 RID: 21666 RVA: 0x0019BFF0 File Offset: 0x0019BFF0
		public ImportAddressTable ImportAddressTable { get; set; }

		// Token: 0x17001172 RID: 4466
		// (get) Token: 0x060054A3 RID: 21667 RVA: 0x0019BFFC File Offset: 0x0019BFFC
		public RVA CorXxxMainRVA
		{
			get
			{
				return this.corXxxMainRVA;
			}
		}

		// Token: 0x17001173 RID: 4467
		// (get) Token: 0x060054A4 RID: 21668 RVA: 0x0019C004 File Offset: 0x0019C004
		public RVA IatCorXxxMainRVA
		{
			get
			{
				return this.ImportAddressTable.RVA;
			}
		}

		// Token: 0x17001174 RID: 4468
		// (get) Token: 0x060054A5 RID: 21669 RVA: 0x0019C014 File Offset: 0x0019C014
		// (set) Token: 0x060054A6 RID: 21670 RVA: 0x0019C01C File Offset: 0x0019C01C
		public bool IsExeFile
		{
			get
			{
				return this.isExeFile;
			}
			set
			{
				this.isExeFile = value;
			}
		}

		// Token: 0x17001175 RID: 4469
		// (get) Token: 0x060054A7 RID: 21671 RVA: 0x0019C028 File Offset: 0x0019C028
		public FileOffset FileOffset
		{
			get
			{
				return this.offset;
			}
		}

		// Token: 0x17001176 RID: 4470
		// (get) Token: 0x060054A8 RID: 21672 RVA: 0x0019C030 File Offset: 0x0019C030
		public RVA RVA
		{
			get
			{
				return this.rva;
			}
		}

		// Token: 0x17001177 RID: 4471
		// (get) Token: 0x060054A9 RID: 21673 RVA: 0x0019C038 File Offset: 0x0019C038
		// (set) Token: 0x060054AA RID: 21674 RVA: 0x0019C040 File Offset: 0x0019C040
		internal bool Enable { get; set; }

		// Token: 0x17001178 RID: 4472
		// (get) Token: 0x060054AB RID: 21675 RVA: 0x0019C04C File Offset: 0x0019C04C
		// (set) Token: 0x060054AC RID: 21676 RVA: 0x0019C060 File Offset: 0x0019C060
		public string DllToImport
		{
			get
			{
				return this.dllToImport ?? "mscoree.dll";
			}
			set
			{
				this.dllToImport = value;
			}
		}

		// Token: 0x17001179 RID: 4473
		// (get) Token: 0x060054AD RID: 21677 RVA: 0x0019C06C File Offset: 0x0019C06C
		// (set) Token: 0x060054AE RID: 21678 RVA: 0x0019C094 File Offset: 0x0019C094
		public string EntryPointName
		{
			get
			{
				string result;
				if ((result = this.entryPointName) == null)
				{
					if (!this.IsExeFile)
					{
						return "_CorDllMain";
					}
					result = "_CorExeMain";
				}
				return result;
			}
			set
			{
				this.entryPointName = value;
			}
		}

		// Token: 0x060054AF RID: 21679 RVA: 0x0019C0A0 File Offset: 0x0019C0A0
		public ImportDirectory(bool is64bit)
		{
			this.is64bit = is64bit;
		}

		// Token: 0x060054B0 RID: 21680 RVA: 0x0019C0B0 File Offset: 0x0019C0B0
		public void SetOffset(FileOffset offset, RVA rva)
		{
			this.offset = offset;
			this.rva = rva;
			this.length = 40U;
			this.importLookupTableRVA = rva + this.length;
			this.length += (this.is64bit ? 16U : 8U);
			this.stringsPadding = (int)(rva.AlignUp(16U) - rva);
			this.length += (uint)this.stringsPadding;
			this.corXxxMainRVA = rva + this.length;
			this.length += (uint)(2 + this.EntryPointName.Length + 1);
			this.dllToImportRVA = rva + this.length;
			this.length += (uint)(this.DllToImport.Length + 1);
			this.length += 1U;
		}

		// Token: 0x060054B1 RID: 21681 RVA: 0x0019C188 File Offset: 0x0019C188
		public uint GetFileLength()
		{
			if (!this.Enable)
			{
				return 0U;
			}
			return this.length;
		}

		// Token: 0x060054B2 RID: 21682 RVA: 0x0019C1A0 File Offset: 0x0019C1A0
		public uint GetVirtualSize()
		{
			return this.GetFileLength();
		}

		// Token: 0x060054B3 RID: 21683 RVA: 0x0019C1A8 File Offset: 0x0019C1A8
		public void WriteTo(DataWriter writer)
		{
			if (!this.Enable)
			{
				return;
			}
			writer.WriteUInt32((uint)this.importLookupTableRVA);
			writer.WriteInt32(0);
			writer.WriteInt32(0);
			writer.WriteUInt32((uint)this.dllToImportRVA);
			writer.WriteUInt32((uint)this.ImportAddressTable.RVA);
			writer.WriteUInt64(0UL);
			writer.WriteUInt64(0UL);
			writer.WriteInt32(0);
			if (this.is64bit)
			{
				writer.WriteUInt64((ulong)this.corXxxMainRVA);
				writer.WriteUInt64(0UL);
			}
			else
			{
				writer.WriteUInt32((uint)this.corXxxMainRVA);
				writer.WriteInt32(0);
			}
			writer.WriteZeroes(this.stringsPadding);
			writer.WriteUInt16(0);
			writer.WriteBytes(Encoding.UTF8.GetBytes(this.EntryPointName + "\0"));
			writer.WriteBytes(Encoding.UTF8.GetBytes(this.DllToImport + "\0"));
			writer.WriteByte(0);
		}

		// Token: 0x04002894 RID: 10388
		private readonly bool is64bit;

		// Token: 0x04002895 RID: 10389
		private FileOffset offset;

		// Token: 0x04002896 RID: 10390
		private RVA rva;

		// Token: 0x04002897 RID: 10391
		private bool isExeFile;

		// Token: 0x04002898 RID: 10392
		private uint length;

		// Token: 0x04002899 RID: 10393
		private RVA importLookupTableRVA;

		// Token: 0x0400289A RID: 10394
		private RVA corXxxMainRVA;

		// Token: 0x0400289B RID: 10395
		private RVA dllToImportRVA;

		// Token: 0x0400289C RID: 10396
		private int stringsPadding;

		// Token: 0x0400289D RID: 10397
		private string dllToImport;

		// Token: 0x0400289E RID: 10398
		private string entryPointName;

		// Token: 0x040028A1 RID: 10401
		private const uint STRINGS_ALIGNMENT = 16U;
	}
}
