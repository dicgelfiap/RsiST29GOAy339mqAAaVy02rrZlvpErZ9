using System;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Math.EC;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x02000443 RID: 1091
	public class ECNamedDomainParameters : ECDomainParameters
	{
		// Token: 0x17000685 RID: 1669
		// (get) Token: 0x0600225E RID: 8798 RVA: 0x000C4908 File Offset: 0x000C4908
		public DerObjectIdentifier Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x0600225F RID: 8799 RVA: 0x000C4910 File Offset: 0x000C4910
		public ECNamedDomainParameters(DerObjectIdentifier name, ECDomainParameters dp) : this(name, dp.Curve, dp.G, dp.N, dp.H, dp.GetSeed())
		{
		}

		// Token: 0x06002260 RID: 8800 RVA: 0x000C4948 File Offset: 0x000C4948
		public ECNamedDomainParameters(DerObjectIdentifier name, ECCurve curve, ECPoint g, BigInteger n) : base(curve, g, n)
		{
			this.name = name;
		}

		// Token: 0x06002261 RID: 8801 RVA: 0x000C495C File Offset: 0x000C495C
		public ECNamedDomainParameters(DerObjectIdentifier name, ECCurve curve, ECPoint g, BigInteger n, BigInteger h) : base(curve, g, n, h)
		{
			this.name = name;
		}

		// Token: 0x06002262 RID: 8802 RVA: 0x000C4974 File Offset: 0x000C4974
		public ECNamedDomainParameters(DerObjectIdentifier name, ECCurve curve, ECPoint g, BigInteger n, BigInteger h, byte[] seed) : base(curve, g, n, h, seed)
		{
			this.name = name;
		}

		// Token: 0x04001606 RID: 5638
		private readonly DerObjectIdentifier name;
	}
}
