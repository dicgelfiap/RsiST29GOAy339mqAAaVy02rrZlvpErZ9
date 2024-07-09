using System;
using System.Runtime.InteropServices;
using dnlib.IO;
using dnlib.PE;

namespace dnlib.DotNet.Writer
{
	// Token: 0x020008A4 RID: 2212
	[ComVisible(true)]
	public sealed class ImportAddressTable : IChunk
	{
		// Token: 0x1700116D RID: 4461
		// (get) Token: 0x06005496 RID: 21654 RVA: 0x0019BF0C File Offset: 0x0019BF0C
		// (set) Token: 0x06005497 RID: 21655 RVA: 0x0019BF14 File Offset: 0x0019BF14
		public ImportDirectory ImportDirectory { get; set; }

		// Token: 0x1700116E RID: 4462
		// (get) Token: 0x06005498 RID: 21656 RVA: 0x0019BF20 File Offset: 0x0019BF20
		public FileOffset FileOffset
		{
			get
			{
				return this.offset;
			}
		}

		// Token: 0x1700116F RID: 4463
		// (get) Token: 0x06005499 RID: 21657 RVA: 0x0019BF28 File Offset: 0x0019BF28
		public RVA RVA
		{
			get
			{
				return this.rva;
			}
		}

		// Token: 0x17001170 RID: 4464
		// (get) Token: 0x0600549A RID: 21658 RVA: 0x0019BF30 File Offset: 0x0019BF30
		// (set) Token: 0x0600549B RID: 21659 RVA: 0x0019BF38 File Offset: 0x0019BF38
		internal bool Enable { get; set; }

		// Token: 0x0600549C RID: 21660 RVA: 0x0019BF44 File Offset: 0x0019BF44
		public ImportAddressTable(bool is64bit)
		{
			this.is64bit = is64bit;
		}

		// Token: 0x0600549D RID: 21661 RVA: 0x0019BF54 File Offset: 0x0019BF54
		public void SetOffset(FileOffset offset, RVA rva)
		{
			this.offset = offset;
			this.rva = rva;
		}

		// Token: 0x0600549E RID: 21662 RVA: 0x0019BF64 File Offset: 0x0019BF64
		public uint GetFileLength()
		{
			if (!this.Enable)
			{
				return 0U;
			}
			if (!this.is64bit)
			{
				return 8U;
			}
			return 16U;
		}

		// Token: 0x0600549F RID: 21663 RVA: 0x0019BF84 File Offset: 0x0019BF84
		public uint GetVirtualSize()
		{
			return this.GetFileLength();
		}

		// Token: 0x060054A0 RID: 21664 RVA: 0x0019BF8C File Offset: 0x0019BF8C
		public void WriteTo(DataWriter writer)
		{
			if (!this.Enable)
			{
				return;
			}
			if (this.is64bit)
			{
				writer.WriteUInt64((ulong)this.ImportDirectory.CorXxxMainRVA);
				writer.WriteUInt64(0UL);
				return;
			}
			writer.WriteUInt32((uint)this.ImportDirectory.CorXxxMainRVA);
			writer.WriteInt32(0);
		}

		// Token: 0x0400288F RID: 10383
		private readonly bool is64bit;

		// Token: 0x04002890 RID: 10384
		private FileOffset offset;

		// Token: 0x04002891 RID: 10385
		private RVA rva;
	}
}
