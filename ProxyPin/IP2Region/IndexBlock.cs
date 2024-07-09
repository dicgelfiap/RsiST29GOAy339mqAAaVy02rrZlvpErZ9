using System;

namespace IP2Region
{
	// Token: 0x02000A5A RID: 2650
	internal class IndexBlock
	{
		// Token: 0x170015ED RID: 5613
		// (get) Token: 0x060067FB RID: 26619 RVA: 0x001FAB60 File Offset: 0x001FAB60
		// (set) Token: 0x060067FC RID: 26620 RVA: 0x001FAB68 File Offset: 0x001FAB68
		public long StartIP { get; private set; }

		// Token: 0x170015EE RID: 5614
		// (get) Token: 0x060067FD RID: 26621 RVA: 0x001FAB74 File Offset: 0x001FAB74
		// (set) Token: 0x060067FE RID: 26622 RVA: 0x001FAB7C File Offset: 0x001FAB7C
		public long EndIp { get; private set; }

		// Token: 0x170015EF RID: 5615
		// (get) Token: 0x060067FF RID: 26623 RVA: 0x001FAB88 File Offset: 0x001FAB88
		// (set) Token: 0x06006800 RID: 26624 RVA: 0x001FAB90 File Offset: 0x001FAB90
		public uint DataPtr { get; private set; }

		// Token: 0x170015F0 RID: 5616
		// (get) Token: 0x06006801 RID: 26625 RVA: 0x001FAB9C File Offset: 0x001FAB9C
		// (set) Token: 0x06006802 RID: 26626 RVA: 0x001FABA4 File Offset: 0x001FABA4
		public int DataLen { get; private set; }

		// Token: 0x06006803 RID: 26627 RVA: 0x001FABB0 File Offset: 0x001FABB0
		public IndexBlock(long startIp, long endIp, uint dataPtr, int dataLen)
		{
			this.StartIP = startIp;
			this.EndIp = endIp;
			this.DataPtr = dataPtr;
			this.DataLen = dataLen;
		}

		// Token: 0x06006804 RID: 26628 RVA: 0x001FABE4 File Offset: 0x001FABE4
		public byte[] GetBytes()
		{
			byte[] array = new byte[12];
			Utils.WriteIntLong(array, 0, this.StartIP);
			Utils.WriteIntLong(array, 4, this.EndIp);
			long v = (long)((ulong)this.DataPtr | (ulong)((long)((long)this.DataLen << 24) & (long)((ulong)-16777216)));
			Utils.WriteIntLong(array, 8, v);
			return array;
		}

		// Token: 0x040034F3 RID: 13555
		public const int LENGTH = 12;
	}
}
