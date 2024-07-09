using System;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x02000464 RID: 1124
	public class MqvPublicParameters : ICipherParameters
	{
		// Token: 0x06002314 RID: 8980 RVA: 0x000C61EC File Offset: 0x000C61EC
		public MqvPublicParameters(ECPublicKeyParameters staticPublicKey, ECPublicKeyParameters ephemeralPublicKey)
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
			this.staticPublicKey = staticPublicKey;
			this.ephemeralPublicKey = ephemeralPublicKey;
		}

		// Token: 0x170006B9 RID: 1721
		// (get) Token: 0x06002315 RID: 8981 RVA: 0x000C6254 File Offset: 0x000C6254
		public virtual ECPublicKeyParameters StaticPublicKey
		{
			get
			{
				return this.staticPublicKey;
			}
		}

		// Token: 0x170006BA RID: 1722
		// (get) Token: 0x06002316 RID: 8982 RVA: 0x000C625C File Offset: 0x000C625C
		public virtual ECPublicKeyParameters EphemeralPublicKey
		{
			get
			{
				return this.ephemeralPublicKey;
			}
		}

		// Token: 0x04001651 RID: 5713
		private readonly ECPublicKeyParameters staticPublicKey;

		// Token: 0x04001652 RID: 5714
		private readonly ECPublicKeyParameters ephemeralPublicKey;
	}
}
