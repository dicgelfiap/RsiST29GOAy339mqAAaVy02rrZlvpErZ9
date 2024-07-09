using System;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x02000458 RID: 1112
	public class Gost3410PublicKeyParameters : Gost3410KeyParameters
	{
		// Token: 0x060022D6 RID: 8918 RVA: 0x000C5AB0 File Offset: 0x000C5AB0
		public Gost3410PublicKeyParameters(BigInteger y, Gost3410Parameters parameters) : base(false, parameters)
		{
			if (y.SignValue < 1 || y.CompareTo(base.Parameters.P) >= 0)
			{
				throw new ArgumentException("Invalid y for GOST3410 public key", "y");
			}
			this.y = y;
		}

		// Token: 0x060022D7 RID: 8919 RVA: 0x000C5B04 File Offset: 0x000C5B04
		public Gost3410PublicKeyParameters(BigInteger y, DerObjectIdentifier publicKeyParamSet) : base(false, publicKeyParamSet)
		{
			if (y.SignValue < 1 || y.CompareTo(base.Parameters.P) >= 0)
			{
				throw new ArgumentException("Invalid y for GOST3410 public key", "y");
			}
			this.y = y;
		}

		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x060022D8 RID: 8920 RVA: 0x000C5B58 File Offset: 0x000C5B58
		public BigInteger Y
		{
			get
			{
				return this.y;
			}
		}

		// Token: 0x0400162E RID: 5678
		private readonly BigInteger y;
	}
}
