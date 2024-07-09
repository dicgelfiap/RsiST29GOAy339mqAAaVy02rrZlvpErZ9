using System;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x02000470 RID: 1136
	public class RsaKeyGenerationParameters : KeyGenerationParameters
	{
		// Token: 0x06002343 RID: 9027 RVA: 0x000C65D4 File Offset: 0x000C65D4
		public RsaKeyGenerationParameters(BigInteger publicExponent, SecureRandom random, int strength, int certainty) : base(random, strength)
		{
			this.publicExponent = publicExponent;
			this.certainty = certainty;
		}

		// Token: 0x170006CE RID: 1742
		// (get) Token: 0x06002344 RID: 9028 RVA: 0x000C65F0 File Offset: 0x000C65F0
		public BigInteger PublicExponent
		{
			get
			{
				return this.publicExponent;
			}
		}

		// Token: 0x170006CF RID: 1743
		// (get) Token: 0x06002345 RID: 9029 RVA: 0x000C65F8 File Offset: 0x000C65F8
		public int Certainty
		{
			get
			{
				return this.certainty;
			}
		}

		// Token: 0x06002346 RID: 9030 RVA: 0x000C6600 File Offset: 0x000C6600
		public override bool Equals(object obj)
		{
			RsaKeyGenerationParameters rsaKeyGenerationParameters = obj as RsaKeyGenerationParameters;
			return rsaKeyGenerationParameters != null && this.certainty == rsaKeyGenerationParameters.certainty && this.publicExponent.Equals(rsaKeyGenerationParameters.publicExponent);
		}

		// Token: 0x06002347 RID: 9031 RVA: 0x000C6644 File Offset: 0x000C6644
		public override int GetHashCode()
		{
			return this.certainty.GetHashCode() ^ this.publicExponent.GetHashCode();
		}

		// Token: 0x04001668 RID: 5736
		private readonly BigInteger publicExponent;

		// Token: 0x04001669 RID: 5737
		private readonly int certainty;
	}
}
