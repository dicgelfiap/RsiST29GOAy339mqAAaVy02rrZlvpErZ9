using System;

namespace Org.BouncyCastle.Bcpg.Sig
{
	// Token: 0x02000287 RID: 647
	public class KeyExpirationTime : SignatureSubpacket
	{
		// Token: 0x0600146A RID: 5226 RVA: 0x0006D878 File Offset: 0x0006D878
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

		// Token: 0x0600146B RID: 5227 RVA: 0x0006D8B0 File Offset: 0x0006D8B0
		public KeyExpirationTime(bool critical, bool isLongLength, byte[] data) : base(SignatureSubpacketTag.KeyExpireTime, critical, isLongLength, data)
		{
		}

		// Token: 0x0600146C RID: 5228 RVA: 0x0006D8C0 File Offset: 0x0006D8C0
		public KeyExpirationTime(bool critical, long seconds) : base(SignatureSubpacketTag.KeyExpireTime, critical, false, KeyExpirationTime.TimeToBytes(seconds))
		{
		}

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x0600146D RID: 5229 RVA: 0x0006D8D4 File Offset: 0x0006D8D4
		public long Time
		{
			get
			{
				return (long)(this.data[0] & byte.MaxValue) << 24 | (long)(this.data[1] & byte.MaxValue) << 16 | (long)(this.data[2] & byte.MaxValue) << 8 | (long)((ulong)this.data[3] & 255UL);
			}
		}
	}
}
