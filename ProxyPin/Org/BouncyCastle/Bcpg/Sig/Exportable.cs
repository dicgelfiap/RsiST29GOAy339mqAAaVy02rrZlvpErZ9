using System;

namespace Org.BouncyCastle.Bcpg.Sig
{
	// Token: 0x02000284 RID: 644
	public class Exportable : SignatureSubpacket
	{
		// Token: 0x0600145B RID: 5211 RVA: 0x0006D5E8 File Offset: 0x0006D5E8
		private static byte[] BooleanToByteArray(bool val)
		{
			byte[] array = new byte[1];
			if (val)
			{
				array[0] = 1;
				return array;
			}
			return array;
		}

		// Token: 0x0600145C RID: 5212 RVA: 0x0006D610 File Offset: 0x0006D610
		public Exportable(bool critical, bool isLongLength, byte[] data) : base(SignatureSubpacketTag.Exportable, critical, isLongLength, data)
		{
		}

		// Token: 0x0600145D RID: 5213 RVA: 0x0006D61C File Offset: 0x0006D61C
		public Exportable(bool critical, bool isExportable) : base(SignatureSubpacketTag.Exportable, critical, false, Exportable.BooleanToByteArray(isExportable))
		{
		}

		// Token: 0x0600145E RID: 5214 RVA: 0x0006D630 File Offset: 0x0006D630
		public bool IsExportable()
		{
			return this.data[0] != 0;
		}
	}
}
