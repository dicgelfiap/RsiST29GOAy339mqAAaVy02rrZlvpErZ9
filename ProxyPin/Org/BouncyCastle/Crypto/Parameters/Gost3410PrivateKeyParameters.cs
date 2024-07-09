using System;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x02000457 RID: 1111
	public class Gost3410PrivateKeyParameters : Gost3410KeyParameters
	{
		// Token: 0x060022D3 RID: 8915 RVA: 0x000C59E0 File Offset: 0x000C59E0
		public Gost3410PrivateKeyParameters(BigInteger x, Gost3410Parameters parameters) : base(true, parameters)
		{
			if (x.SignValue < 1 || x.BitLength > 256 || x.CompareTo(base.Parameters.Q) >= 0)
			{
				throw new ArgumentException("Invalid x for GOST3410 private key", "x");
			}
			this.x = x;
		}

		// Token: 0x060022D4 RID: 8916 RVA: 0x000C5A44 File Offset: 0x000C5A44
		public Gost3410PrivateKeyParameters(BigInteger x, DerObjectIdentifier publicKeyParamSet) : base(true, publicKeyParamSet)
		{
			if (x.SignValue < 1 || x.BitLength > 256 || x.CompareTo(base.Parameters.Q) >= 0)
			{
				throw new ArgumentException("Invalid x for GOST3410 private key", "x");
			}
			this.x = x;
		}

		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x060022D5 RID: 8917 RVA: 0x000C5AA8 File Offset: 0x000C5AA8
		public BigInteger X
		{
			get
			{
				return this.x;
			}
		}

		// Token: 0x0400162D RID: 5677
		private readonly BigInteger x;
	}
}
