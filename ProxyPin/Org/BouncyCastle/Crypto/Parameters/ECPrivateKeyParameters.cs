using System;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x02000447 RID: 1095
	public class ECPrivateKeyParameters : ECKeyParameters
	{
		// Token: 0x06002278 RID: 8824 RVA: 0x000C4CF0 File Offset: 0x000C4CF0
		public ECPrivateKeyParameters(BigInteger d, ECDomainParameters parameters) : this("EC", d, parameters)
		{
		}

		// Token: 0x06002279 RID: 8825 RVA: 0x000C4D00 File Offset: 0x000C4D00
		[Obsolete("Use version with explicit 'algorithm' parameter")]
		public ECPrivateKeyParameters(BigInteger d, DerObjectIdentifier publicKeyParamSet) : base("ECGOST3410", true, publicKeyParamSet)
		{
			this.d = base.Parameters.ValidatePrivateScalar(d);
		}

		// Token: 0x0600227A RID: 8826 RVA: 0x000C4D24 File Offset: 0x000C4D24
		public ECPrivateKeyParameters(string algorithm, BigInteger d, ECDomainParameters parameters) : base(algorithm, true, parameters)
		{
			this.d = base.Parameters.ValidatePrivateScalar(d);
		}

		// Token: 0x0600227B RID: 8827 RVA: 0x000C4D44 File Offset: 0x000C4D44
		public ECPrivateKeyParameters(string algorithm, BigInteger d, DerObjectIdentifier publicKeyParamSet) : base(algorithm, true, publicKeyParamSet)
		{
			this.d = base.Parameters.ValidatePrivateScalar(d);
		}

		// Token: 0x1700068E RID: 1678
		// (get) Token: 0x0600227C RID: 8828 RVA: 0x000C4D64 File Offset: 0x000C4D64
		public BigInteger D
		{
			get
			{
				return this.d;
			}
		}

		// Token: 0x0600227D RID: 8829 RVA: 0x000C4D6C File Offset: 0x000C4D6C
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			ECPrivateKeyParameters ecprivateKeyParameters = obj as ECPrivateKeyParameters;
			return ecprivateKeyParameters != null && this.Equals(ecprivateKeyParameters);
		}

		// Token: 0x0600227E RID: 8830 RVA: 0x000C4D9C File Offset: 0x000C4D9C
		protected bool Equals(ECPrivateKeyParameters other)
		{
			return this.d.Equals(other.d) && base.Equals(other);
		}

		// Token: 0x0600227F RID: 8831 RVA: 0x000C4DC0 File Offset: 0x000C4DC0
		public override int GetHashCode()
		{
			return this.d.GetHashCode() ^ base.GetHashCode();
		}

		// Token: 0x04001610 RID: 5648
		private readonly BigInteger d;
	}
}
