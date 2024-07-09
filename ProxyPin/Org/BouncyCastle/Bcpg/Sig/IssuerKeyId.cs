using System;

namespace Org.BouncyCastle.Bcpg.Sig
{
	// Token: 0x02000286 RID: 646
	public class IssuerKeyId : SignatureSubpacket
	{
		// Token: 0x06001466 RID: 5222 RVA: 0x0006D754 File Offset: 0x0006D754
		protected static byte[] KeyIdToBytes(long keyId)
		{
			return new byte[]
			{
				(byte)(keyId >> 56),
				(byte)(keyId >> 48),
				(byte)(keyId >> 40),
				(byte)(keyId >> 32),
				(byte)(keyId >> 24),
				(byte)(keyId >> 16),
				(byte)(keyId >> 8),
				(byte)keyId
			};
		}

		// Token: 0x06001467 RID: 5223 RVA: 0x0006D7AC File Offset: 0x0006D7AC
		public IssuerKeyId(bool critical, bool isLongLength, byte[] data) : base(SignatureSubpacketTag.IssuerKeyId, critical, isLongLength, data)
		{
		}

		// Token: 0x06001468 RID: 5224 RVA: 0x0006D7BC File Offset: 0x0006D7BC
		public IssuerKeyId(bool critical, long keyId) : base(SignatureSubpacketTag.IssuerKeyId, critical, false, IssuerKeyId.KeyIdToBytes(keyId))
		{
		}

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x06001469 RID: 5225 RVA: 0x0006D7D0 File Offset: 0x0006D7D0
		public long KeyId
		{
			get
			{
				return (long)(this.data[0] & byte.MaxValue) << 56 | (long)(this.data[1] & byte.MaxValue) << 48 | (long)(this.data[2] & byte.MaxValue) << 40 | (long)(this.data[3] & byte.MaxValue) << 32 | (long)(this.data[4] & byte.MaxValue) << 24 | (long)(this.data[5] & byte.MaxValue) << 16 | (long)(this.data[6] & byte.MaxValue) << 8 | (long)((ulong)this.data[7] & 255UL);
			}
		}
	}
}
