using System;
using System.Runtime.InteropServices;
using dnlib.DotNet.MD;
using dnlib.IO;
using dnlib.PE;

namespace dnlib.DotNet.Writer
{
	// Token: 0x020008A3 RID: 2211
	[ComVisible(true)]
	public sealed class ImageCor20Header : IChunk
	{
		// Token: 0x17001167 RID: 4455
		// (get) Token: 0x06005487 RID: 21639 RVA: 0x0019BD88 File Offset: 0x0019BD88
		// (set) Token: 0x06005488 RID: 21640 RVA: 0x0019BD90 File Offset: 0x0019BD90
		public Metadata Metadata { get; set; }

		// Token: 0x17001168 RID: 4456
		// (get) Token: 0x06005489 RID: 21641 RVA: 0x0019BD9C File Offset: 0x0019BD9C
		// (set) Token: 0x0600548A RID: 21642 RVA: 0x0019BDA4 File Offset: 0x0019BDA4
		public NetResources NetResources { get; set; }

		// Token: 0x17001169 RID: 4457
		// (get) Token: 0x0600548B RID: 21643 RVA: 0x0019BDB0 File Offset: 0x0019BDB0
		// (set) Token: 0x0600548C RID: 21644 RVA: 0x0019BDB8 File Offset: 0x0019BDB8
		public StrongNameSignature StrongNameSignature { get; set; }

		// Token: 0x1700116A RID: 4458
		// (get) Token: 0x0600548D RID: 21645 RVA: 0x0019BDC4 File Offset: 0x0019BDC4
		// (set) Token: 0x0600548E RID: 21646 RVA: 0x0019BDCC File Offset: 0x0019BDCC
		internal IChunk VtableFixups { get; set; }

		// Token: 0x1700116B RID: 4459
		// (get) Token: 0x0600548F RID: 21647 RVA: 0x0019BDD8 File Offset: 0x0019BDD8
		public FileOffset FileOffset
		{
			get
			{
				return this.offset;
			}
		}

		// Token: 0x1700116C RID: 4460
		// (get) Token: 0x06005490 RID: 21648 RVA: 0x0019BDE0 File Offset: 0x0019BDE0
		public RVA RVA
		{
			get
			{
				return this.rva;
			}
		}

		// Token: 0x06005491 RID: 21649 RVA: 0x0019BDE8 File Offset: 0x0019BDE8
		public ImageCor20Header(Cor20HeaderOptions options)
		{
			this.options = options;
		}

		// Token: 0x06005492 RID: 21650 RVA: 0x0019BDF8 File Offset: 0x0019BDF8
		public void SetOffset(FileOffset offset, RVA rva)
		{
			this.offset = offset;
			this.rva = rva;
		}

		// Token: 0x06005493 RID: 21651 RVA: 0x0019BE08 File Offset: 0x0019BE08
		public uint GetFileLength()
		{
			return 72U;
		}

		// Token: 0x06005494 RID: 21652 RVA: 0x0019BE0C File Offset: 0x0019BE0C
		public uint GetVirtualSize()
		{
			return this.GetFileLength();
		}

		// Token: 0x06005495 RID: 21653 RVA: 0x0019BE14 File Offset: 0x0019BE14
		public void WriteTo(DataWriter writer)
		{
			writer.WriteInt32(72);
			writer.WriteUInt16(this.options.MajorRuntimeVersion ?? 2);
			writer.WriteUInt16(this.options.MinorRuntimeVersion ?? 5);
			writer.WriteDataDirectory(this.Metadata);
			writer.WriteUInt32((uint)(this.options.Flags ?? ComImageFlags.ILOnly));
			writer.WriteUInt32(this.options.EntryPoint.GetValueOrDefault());
			writer.WriteDataDirectory(this.NetResources);
			writer.WriteDataDirectory(this.StrongNameSignature);
			writer.WriteDataDirectory(null);
			writer.WriteDataDirectory(this.VtableFixups);
			writer.WriteDataDirectory(null);
			writer.WriteDataDirectory(null);
		}

		// Token: 0x04002888 RID: 10376
		private FileOffset offset;

		// Token: 0x04002889 RID: 10377
		private RVA rva;

		// Token: 0x0400288A RID: 10378
		private Cor20HeaderOptions options;
	}
}
