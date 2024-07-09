using System;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x02000439 RID: 1081
	public class DHPublicKeyParameters : DHKeyParameters
	{
		// Token: 0x06002218 RID: 8728 RVA: 0x000C3F18 File Offset: 0x000C3F18
		private static BigInteger Validate(BigInteger y, DHParameters dhParams)
		{
			if (y == null)
			{
				throw new ArgumentNullException("y");
			}
			if (y.CompareTo(BigInteger.Two) < 0 || y.CompareTo(dhParams.P.Subtract(BigInteger.Two)) > 0)
			{
				throw new ArgumentException("invalid DH public key", "y");
			}
			if (dhParams.Q != null && !y.ModPow(dhParams.Q, dhParams.P).Equals(BigInteger.One))
			{
				throw new ArgumentException("y value does not appear to be in correct group", "y");
			}
			return y;
		}

		// Token: 0x06002219 RID: 8729 RVA: 0x000C3FB4 File Offset: 0x000C3FB4
		public DHPublicKeyParameters(BigInteger y, DHParameters parameters) : base(false, parameters)
		{
			this.y = DHPublicKeyParameters.Validate(y, parameters);
		}

		// Token: 0x0600221A RID: 8730 RVA: 0x000C3FCC File Offset: 0x000C3FCC
		public DHPublicKeyParameters(BigInteger y, DHParameters parameters, DerObjectIdentifier algorithmOid) : base(false, parameters, algorithmOid)
		{
			this.y = DHPublicKeyParameters.Validate(y, parameters);
		}

		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x0600221B RID: 8731 RVA: 0x000C3FE4 File Offset: 0x000C3FE4
		public virtual BigInteger Y
		{
			get
			{
				return this.y;
			}
		}

		// Token: 0x0600221C RID: 8732 RVA: 0x000C3FEC File Offset: 0x000C3FEC
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DHPublicKeyParameters dhpublicKeyParameters = obj as DHPublicKeyParameters;
			return dhpublicKeyParameters != null && this.Equals(dhpublicKeyParameters);
		}

		// Token: 0x0600221D RID: 8733 RVA: 0x000C401C File Offset: 0x000C401C
		protected bool Equals(DHPublicKeyParameters other)
		{
			return this.y.Equals(other.y) && base.Equals(other);
		}

		// Token: 0x0600221E RID: 8734 RVA: 0x000C4040 File Offset: 0x000C4040
		public override int GetHashCode()
		{
			return this.y.GetHashCode() ^ base.GetHashCode();
		}

		// Token: 0x040015EB RID: 5611
		private readonly BigInteger y;
	}
}
