using System;
using Org.BouncyCastle.Math.EC;
using Org.BouncyCastle.Math.EC.Multiplier;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x02000474 RID: 1140
	public class SM2KeyExchangePrivateParameters : ICipherParameters
	{
		// Token: 0x06002362 RID: 9058 RVA: 0x000C6B70 File Offset: 0x000C6B70
		public SM2KeyExchangePrivateParameters(bool initiator, ECPrivateKeyParameters staticPrivateKey, ECPrivateKeyParameters ephemeralPrivateKey)
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
			ECMultiplier ecmultiplier = new FixedPointCombMultiplier();
			this.mInitiator = initiator;
			this.mStaticPrivateKey = staticPrivateKey;
			this.mStaticPublicPoint = ecmultiplier.Multiply(parameters.G, staticPrivateKey.D).Normalize();
			this.mEphemeralPrivateKey = ephemeralPrivateKey;
			this.mEphemeralPublicPoint = ecmultiplier.Multiply(parameters.G, ephemeralPrivateKey.D).Normalize();
		}

		// Token: 0x170006D8 RID: 1752
		// (get) Token: 0x06002363 RID: 9059 RVA: 0x000C6C24 File Offset: 0x000C6C24
		public virtual bool IsInitiator
		{
			get
			{
				return this.mInitiator;
			}
		}

		// Token: 0x170006D9 RID: 1753
		// (get) Token: 0x06002364 RID: 9060 RVA: 0x000C6C2C File Offset: 0x000C6C2C
		public virtual ECPrivateKeyParameters StaticPrivateKey
		{
			get
			{
				return this.mStaticPrivateKey;
			}
		}

		// Token: 0x170006DA RID: 1754
		// (get) Token: 0x06002365 RID: 9061 RVA: 0x000C6C34 File Offset: 0x000C6C34
		public virtual ECPoint StaticPublicPoint
		{
			get
			{
				return this.mStaticPublicPoint;
			}
		}

		// Token: 0x170006DB RID: 1755
		// (get) Token: 0x06002366 RID: 9062 RVA: 0x000C6C3C File Offset: 0x000C6C3C
		public virtual ECPrivateKeyParameters EphemeralPrivateKey
		{
			get
			{
				return this.mEphemeralPrivateKey;
			}
		}

		// Token: 0x170006DC RID: 1756
		// (get) Token: 0x06002367 RID: 9063 RVA: 0x000C6C44 File Offset: 0x000C6C44
		public virtual ECPoint EphemeralPublicPoint
		{
			get
			{
				return this.mEphemeralPublicPoint;
			}
		}

		// Token: 0x0400167C RID: 5756
		private readonly bool mInitiator;

		// Token: 0x0400167D RID: 5757
		private readonly ECPrivateKeyParameters mStaticPrivateKey;

		// Token: 0x0400167E RID: 5758
		private readonly ECPoint mStaticPublicPoint;

		// Token: 0x0400167F RID: 5759
		private readonly ECPrivateKeyParameters mEphemeralPrivateKey;

		// Token: 0x04001680 RID: 5760
		private readonly ECPoint mEphemeralPublicPoint;
	}
}
