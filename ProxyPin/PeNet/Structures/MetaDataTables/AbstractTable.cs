using System;
using System.Runtime.InteropServices;
using PeNet.Utilities;

namespace PeNet.Structures.MetaDataTables
{
	// Token: 0x02000BC7 RID: 3015
	[ComVisible(true)]
	public class AbstractTable : AbstractStructure
	{
		// Token: 0x17001A1C RID: 6684
		// (get) Token: 0x060079A6 RID: 31142 RVA: 0x0023FD14 File Offset: 0x0023FD14
		protected HeapSizes HeapSizes { get; }

		// Token: 0x17001A1D RID: 6685
		// (get) Token: 0x060079A7 RID: 31143 RVA: 0x0023FD1C File Offset: 0x0023FD1C
		protected IndexSize IndexSizes { get; }

		// Token: 0x060079A8 RID: 31144 RVA: 0x0023FD24 File Offset: 0x0023FD24
		public AbstractTable(byte[] buff, uint offset, HeapSizes heapSizes, IndexSize indexSizes) : base(buff, offset)
		{
			this.HeapSizes = heapSizes;
			this.IndexSizes = indexSizes;
			this.CurrentOffset = this.Offset;
		}

		// Token: 0x060079A9 RID: 31145 RVA: 0x0023FD4C File Offset: 0x0023FD4C
		private uint ReadSize(uint size, ref uint offset)
		{
			switch (size)
			{
			case 1U:
				offset += 1U;
				return (uint)this.Buff[(int)(offset - 1U)];
			case 2U:
				offset += 2U;
				return (uint)this.Buff.BytesToUInt16((ulong)(offset - 2U));
			case 4U:
				offset += 4U;
				return this.Buff.BytesToUInt32(offset - 4U);
			}
			throw new ArgumentException("Unsupported offset size.");
		}

		// Token: 0x060079AA RID: 31146 RVA: 0x0023FDC4 File Offset: 0x0023FDC4
		protected uint ReadSize(uint size)
		{
			return this.ReadSize(size, ref this.CurrentOffset);
		}

		// Token: 0x04003A6E RID: 14958
		protected uint CurrentOffset;
	}
}
