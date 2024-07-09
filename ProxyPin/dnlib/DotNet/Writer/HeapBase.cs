using System;
using System.Runtime.InteropServices;
using dnlib.IO;
using dnlib.PE;

namespace dnlib.DotNet.Writer
{
	// Token: 0x0200089E RID: 2206
	[ComVisible(true)]
	public abstract class HeapBase : IHeap, IChunk
	{
		// Token: 0x1700115E RID: 4446
		// (get) Token: 0x0600546C RID: 21612 RVA: 0x0019BC98 File Offset: 0x0019BC98
		public FileOffset FileOffset
		{
			get
			{
				return this.offset;
			}
		}

		// Token: 0x1700115F RID: 4447
		// (get) Token: 0x0600546D RID: 21613 RVA: 0x0019BCA0 File Offset: 0x0019BCA0
		public RVA RVA
		{
			get
			{
				return this.rva;
			}
		}

		// Token: 0x17001160 RID: 4448
		// (get) Token: 0x0600546E RID: 21614
		public abstract string Name { get; }

		// Token: 0x17001161 RID: 4449
		// (get) Token: 0x0600546F RID: 21615 RVA: 0x0019BCA8 File Offset: 0x0019BCA8
		public bool IsEmpty
		{
			get
			{
				return this.GetRawLength() <= 1U;
			}
		}

		// Token: 0x17001162 RID: 4450
		// (get) Token: 0x06005470 RID: 21616 RVA: 0x0019BCB8 File Offset: 0x0019BCB8
		public bool IsBig
		{
			get
			{
				return this.GetFileLength() > 65535U;
			}
		}

		// Token: 0x06005471 RID: 21617 RVA: 0x0019BCC8 File Offset: 0x0019BCC8
		public void SetReadOnly()
		{
			this.isReadOnly = true;
		}

		// Token: 0x06005472 RID: 21618 RVA: 0x0019BCD4 File Offset: 0x0019BCD4
		public virtual void SetOffset(FileOffset offset, RVA rva)
		{
			this.offset = offset;
			this.rva = rva;
		}

		// Token: 0x06005473 RID: 21619 RVA: 0x0019BCE4 File Offset: 0x0019BCE4
		public uint GetFileLength()
		{
			return Utils.AlignUp(this.GetRawLength(), 4U);
		}

		// Token: 0x06005474 RID: 21620 RVA: 0x0019BCF4 File Offset: 0x0019BCF4
		public uint GetVirtualSize()
		{
			return this.GetFileLength();
		}

		// Token: 0x06005475 RID: 21621
		public abstract uint GetRawLength();

		// Token: 0x06005476 RID: 21622 RVA: 0x0019BCFC File Offset: 0x0019BCFC
		public void WriteTo(DataWriter writer)
		{
			this.WriteToImpl(writer);
			writer.WriteZeroes((int)(Utils.AlignUp(this.GetRawLength(), 4U) - this.GetRawLength()));
		}

		// Token: 0x06005477 RID: 21623
		protected abstract void WriteToImpl(DataWriter writer);

		// Token: 0x06005478 RID: 21624 RVA: 0x0019BD30 File Offset: 0x0019BD30
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x0400287E RID: 10366
		internal const uint ALIGNMENT = 4U;

		// Token: 0x0400287F RID: 10367
		private FileOffset offset;

		// Token: 0x04002880 RID: 10368
		private RVA rva;

		// Token: 0x04002881 RID: 10369
		protected bool isReadOnly;
	}
}
