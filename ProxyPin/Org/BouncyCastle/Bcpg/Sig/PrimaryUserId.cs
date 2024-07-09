using System;

namespace Org.BouncyCastle.Bcpg.Sig
{
	// Token: 0x0200028B RID: 651
	public class PrimaryUserId : SignatureSubpacket
	{
		// Token: 0x0600147D RID: 5245 RVA: 0x0006DC64 File Offset: 0x0006DC64
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

		// Token: 0x0600147E RID: 5246 RVA: 0x0006DC8C File Offset: 0x0006DC8C
		public PrimaryUserId(bool critical, bool isLongLength, byte[] data) : base(SignatureSubpacketTag.PrimaryUserId, critical, isLongLength, data)
		{
		}

		// Token: 0x0600147F RID: 5247 RVA: 0x0006DC9C File Offset: 0x0006DC9C
		public PrimaryUserId(bool critical, bool isPrimaryUserId) : base(SignatureSubpacketTag.PrimaryUserId, critical, false, PrimaryUserId.BooleanToByteArray(isPrimaryUserId))
		{
		}

		// Token: 0x06001480 RID: 5248 RVA: 0x0006DCB0 File Offset: 0x0006DCB0
		public bool IsPrimaryUserId()
		{
			return this.data[0] != 0;
		}
	}
}
