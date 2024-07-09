using System;
using Org.BouncyCastle.Math.EC;
using Org.BouncyCastle.Math.EC.Multiplier;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x02000463 RID: 1123
	public class MqvPrivateParameters : ICipherParameters
	{
		// Token: 0x0600230F RID: 8975 RVA: 0x000C6110 File Offset: 0x000C6110
		public MqvPrivateParameters(ECPrivateKeyParameters staticPrivateKey, ECPrivateKeyParameters ephemeralPrivateKey) : this(staticPrivateKey, ephemeralPrivateKey, null)
		{
		}

		// Token: 0x06002310 RID: 8976 RVA: 0x000C611C File Offset: 0x000C611C
		public MqvPrivateParameters(ECPrivateKeyParameters staticPrivateKey, ECPrivateKeyParameters ephemeralPrivateKey, ECPublicKeyParameters ephemeralPublicKey)
		{
			if (staticPrivateKey == null)
			{
				throw new ArgumentNullException("staticPrivateKey");
			}
			if (ephemeralPrivateKey == null)
			{
				throw new ArgumentNullException("ephemeralPrivateKey");
			}
			ECDomainParameters parameters = staticPrivateKey.Parameters;
			if (!parameters.Equals(ephemeralPrivateKey.Parameters))
			{
				throw new ArgumentException("Static and ephemeral private keys have different domain parameters");
			}
			if (ephemeralPublicKey == null)
			{
				ECPoint q = new FixedPointCombMultiplier().Multiply(parameters.G, ephemeralPrivateKey.D);
				ephemeralPublicKey = new ECPublicKeyParameters(q, parameters);
			}
			else if (!parameters.Equals(ephemeralPublicKey.Parameters))
			{
				throw new ArgumentException("Ephemeral public key has different domain parameters");
			}
			this.staticPrivateKey = staticPrivateKey;
			this.ephemeralPrivateKey = ephemeralPrivateKey;
			this.ephemeralPublicKey = ephemeralPublicKey;
		}

		// Token: 0x170006B6 RID: 1718
		// (get) Token: 0x06002311 RID: 8977 RVA: 0x000C61D4 File Offset: 0x000C61D4
		public virtual ECPrivateKeyParameters StaticPrivateKey
		{
			get
			{
				return this.staticPrivateKey;
			}
		}

		// Token: 0x170006B7 RID: 1719
		// (get) Token: 0x06002312 RID: 8978 RVA: 0x000C61DC File Offset: 0x000C61DC
		public virtual ECPrivateKeyParameters EphemeralPrivateKey
		{
			get
			{
				return this.ephemeralPrivateKey;
			}
		}

		// Token: 0x170006B8 RID: 1720
		// (get) Token: 0x06002313 RID: 8979 RVA: 0x000C61E4 File Offset: 0x000C61E4
		public virtual ECPublicKeyParameters EphemeralPublicKey
		{
			get
			{
				return this.ephemeralPublicKey;
			}
		}

		// Token: 0x0400164E RID: 5710
		private readonly ECPrivateKeyParameters staticPrivateKey;

		// Token: 0x0400164F RID: 5711
		private readonly ECPrivateKeyParameters ephemeralPrivateKey;

		// Token: 0x04001650 RID: 5712
		private readonly ECPublicKeyParameters ephemeralPublicKey;
	}
}
