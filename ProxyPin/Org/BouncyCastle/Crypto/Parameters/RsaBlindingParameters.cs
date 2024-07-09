using System;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x0200046F RID: 1135
	public class RsaBlindingParameters : ICipherParameters
	{
		// Token: 0x06002340 RID: 9024 RVA: 0x000C6598 File Offset: 0x000C6598
		public RsaBlindingParameters(RsaKeyParameters publicKey, BigInteger blindingFactor)
		{
			if (publicKey.IsPrivate)
			{
				throw new ArgumentException("RSA parameters should be for a public key");
			}
			this.publicKey = publicKey;
			this.blindingFactor = blindingFactor;
		}

		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x06002341 RID: 9025 RVA: 0x000C65C4 File Offset: 0x000C65C4
		public RsaKeyParameters PublicKey
		{
			get
			{
				return this.publicKey;
			}
		}

		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x06002342 RID: 9026 RVA: 0x000C65CC File Offset: 0x000C65CC
		public BigInteger BlindingFactor
		{
			get
			{
				return this.blindingFactor;
			}
		}

		// Token: 0x04001666 RID: 5734
		private readonly RsaKeyParameters publicKey;

		// Token: 0x04001667 RID: 5735
		private readonly BigInteger blindingFactor;
	}
}
