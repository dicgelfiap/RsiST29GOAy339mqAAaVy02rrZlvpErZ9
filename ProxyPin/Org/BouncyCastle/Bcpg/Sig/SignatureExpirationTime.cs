using System;

namespace Org.BouncyCastle.Bcpg.Sig
{
	// Token: 0x02000292 RID: 658
	public class SignatureExpirationTime : SignatureSubpacket
	{
		// Token: 0x06001494 RID: 5268 RVA: 0x0006DF14 File Offset: 0x0006DF14
		protected static byte[] TimeToBytes(long t)
		{
			return new byte[]
			{
				(byte)(t >> 24),
				(byte)(t >> 16),
				(byte)(t >> 8),
				(byte)t
			};
		}

		// Token: 0x06001495 RID: 5269 RVA: 0x0006DF4C File Offset: 0x0006DF4C
		public SignatureExpirationTime(bool critical, bool isLongLength, byte[] data) : base(SignatureSubpacketTag.ExpireTime, critical, isLongLength, data)
		{
		}

		// Token: 0x06001496 RID: 5270 RVA: 0x0006DF58 File Offset: 0x0006DF58
		public SignatureExpirationTime(bool critical, long seconds) : base(SignatureSubpacketTag.ExpireTime, critical, false, SignatureExpirationTime.TimeToBytes(seconds))
		{
		}

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x06001497 RID: 5271 RVA: 0x0006DF6C File Offset: 0x0006DF6C
		public long Time
		{
			get
			{
				return (long)(this.data[0] & byte.MaxValue) << 24 | (long)(this.data[1] & byte.MaxValue) << 16 | (long)(this.data[2] & byte.MaxValue) << 8 | (long)((ulong)this.data[3] & 255UL);
			}
		}
	}
}
