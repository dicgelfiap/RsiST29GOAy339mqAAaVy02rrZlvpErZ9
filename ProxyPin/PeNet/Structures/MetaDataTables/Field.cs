using System;
using System.Runtime.InteropServices;

namespace PeNet.Structures.MetaDataTables
{
	// Token: 0x02000BD5 RID: 3029
	[ComVisible(true)]
	public class Field : AbstractTable
	{
		// Token: 0x060079E9 RID: 31209 RVA: 0x00240464 File Offset: 0x00240464
		public Field(byte[] buff, uint offset, HeapSizes heapSizes, IndexSize indexSizes) : base(buff, offset, heapSizes, indexSizes)
		{
			this.Flags = (ushort)base.ReadSize(2U);
			this.Name = base.ReadSize(base.HeapSizes.String);
			this.Signature = base.ReadSize(base.HeapSizes.Blob);
		}

		// Token: 0x17001A4F RID: 6735
		// (get) Token: 0x060079EA RID: 31210 RVA: 0x002404BC File Offset: 0x002404BC
		public ushort Flags { get; }

		// Token: 0x17001A50 RID: 6736
		// (get) Token: 0x060079EB RID: 31211 RVA: 0x002404C4 File Offset: 0x002404C4
		public uint Name { get; }

		// Token: 0x17001A51 RID: 6737
		// (get) Token: 0x060079EC RID: 31212 RVA: 0x002404CC File Offset: 0x002404CC
		public uint Signature { get; }
	}
}
