using System;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x02000475 RID: 1141
	public class SM2KeyExchangePublicParameters : ICipherParameters
	{
		// Token: 0x06002368 RID: 9064 RVA: 0x000C6C4C File Offset: 0x000C6C4C
		public SM2KeyExchangePublicParameters(ECPublicKeyParameters staticPublicKey, ECPublicKeyParameters ephemeralPublicKey)
		{
			if (staticPublicKey == null)
			{
				throw new ArgumentNullException("staticPublicKey");
			}
			if (ephemeralPublicKey == null)
			{
				throw new ArgumentNullException("ephemeralPublicKey");
			}
			if (!staticPublicKey.Parameters.Equals(ephemeralPublicKey.Parameters))
			{
				throw new ArgumentException("Static and ephemeral public keys have different domain parameters");
			}
			this.mStaticPublicKey = staticPublicKey;
			this.mEphemeralPublicKey = ephemeralPublicKey;
		}

		// Token: 0x170006DD RID: 1757
		// (get) Token: 0x06002369 RID: 9065 RVA: 0x000C6CB4 File Offset: 0x000C6CB4
		public virtual ECPublicKeyParameters StaticPublicKey
		{
			get
			{
				return this.mStaticPublicKey;
			}
		}

		// Token: 0x170006DE RID: 1758
		// (get) Token: 0x0600236A RID: 9066 RVA: 0x000C6CBC File Offset: 0x000C6CBC
		public virtual ECPublicKeyParameters EphemeralPublicKey
		{
			get
			{
				return this.mEphemeralPublicKey;
			}
		}

		// Token: 0x04001681 RID: 5761
		private readonly ECPublicKeyParameters mStaticPublicKey;

		// Token: 0x04001682 RID: 5762
		private readonly ECPublicKeyParameters mEphemeralPublicKey;
	}
}
