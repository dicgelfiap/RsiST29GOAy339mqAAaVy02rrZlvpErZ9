using System;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x02000466 RID: 1126
	public class NaccacheSternKeyParameters : AsymmetricKeyParameter
	{
		// Token: 0x0600231C RID: 8988 RVA: 0x000C62DC File Offset: 0x000C62DC
		public NaccacheSternKeyParameters(bool privateKey, BigInteger g, BigInteger n, int lowerSigmaBound) : base(privateKey)
		{
			this.g = g;
			this.n = n;
			this.lowerSigmaBound = lowerSigmaBound;
		}

		// Token: 0x170006BE RID: 1726
		// (get) Token: 0x0600231D RID: 8989 RVA: 0x000C62FC File Offset: 0x000C62FC
		public BigInteger G
		{
			get
			{
				return this.g;
			}
		}

		// Token: 0x170006BF RID: 1727
		// (get) Token: 0x0600231E RID: 8990 RVA: 0x000C6304 File Offset: 0x000C6304
		public int LowerSigmaBound
		{
			get
			{
				return this.lowerSigmaBound;
			}
		}

		// Token: 0x170006C0 RID: 1728
		// (get) Token: 0x0600231F RID: 8991 RVA: 0x000C630C File Offset: 0x000C630C
		public BigInteger Modulus
		{
			get
			{
				return this.n;
			}
		}

		// Token: 0x04001655 RID: 5717
		private readonly BigInteger g;

		// Token: 0x04001656 RID: 5718
		private readonly BigInteger n;

		// Token: 0x04001657 RID: 5719
		private readonly int lowerSigmaBound;
	}
}
