using System;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Math.EC;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x02000448 RID: 1096
	public class ECPublicKeyParameters : ECKeyParameters
	{
		// Token: 0x06002280 RID: 8832 RVA: 0x000C4DD4 File Offset: 0x000C4DD4
		public ECPublicKeyParameters(ECPoint q, ECDomainParameters parameters) : this("EC", q, parameters)
		{
		}

		// Token: 0x06002281 RID: 8833 RVA: 0x000C4DE4 File Offset: 0x000C4DE4
		[Obsolete("Use version with explicit 'algorithm' parameter")]
		public ECPublicKeyParameters(ECPoint q, DerObjectIdentifier publicKeyParamSet) : base("ECGOST3410", false, publicKeyParamSet)
		{
			this.q = ECDomainParameters.ValidatePublicPoint(base.Parameters.Curve, q);
		}

		// Token: 0x06002282 RID: 8834 RVA: 0x000C4E1C File Offset: 0x000C4E1C
		public ECPublicKeyParameters(string algorithm, ECPoint q, ECDomainParameters parameters) : base(algorithm, false, parameters)
		{
			this.q = ECDomainParameters.ValidatePublicPoint(base.Parameters.Curve, q);
		}

		// Token: 0x06002283 RID: 8835 RVA: 0x000C4E50 File Offset: 0x000C4E50
		public ECPublicKeyParameters(string algorithm, ECPoint q, DerObjectIdentifier publicKeyParamSet) : base(algorithm, false, publicKeyParamSet)
		{
			this.q = ECDomainParameters.ValidatePublicPoint(base.Parameters.Curve, q);
		}

		// Token: 0x1700068F RID: 1679
		// (get) Token: 0x06002284 RID: 8836 RVA: 0x000C4E84 File Offset: 0x000C4E84
		public ECPoint Q
		{
			get
			{
				return this.q;
			}
		}

		// Token: 0x06002285 RID: 8837 RVA: 0x000C4E8C File Offset: 0x000C4E8C
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			ECPublicKeyParameters ecpublicKeyParameters = obj as ECPublicKeyParameters;
			return ecpublicKeyParameters != null && this.Equals(ecpublicKeyParameters);
		}

		// Token: 0x06002286 RID: 8838 RVA: 0x000C4EBC File Offset: 0x000C4EBC
		protected bool Equals(ECPublicKeyParameters other)
		{
			return this.q.Equals(other.q) && base.Equals(other);
		}

		// Token: 0x06002287 RID: 8839 RVA: 0x000C4EE0 File Offset: 0x000C4EE0
		public override int GetHashCode()
		{
			return this.q.GetHashCode() ^ base.GetHashCode();
		}

		// Token: 0x04001611 RID: 5649
		private readonly ECPoint q;
	}
}
