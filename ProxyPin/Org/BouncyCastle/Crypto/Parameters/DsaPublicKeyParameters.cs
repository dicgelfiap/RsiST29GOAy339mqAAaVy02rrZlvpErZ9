using System;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x02000440 RID: 1088
	public class DsaPublicKeyParameters : DsaKeyParameters
	{
		// Token: 0x06002241 RID: 8769 RVA: 0x000C4434 File Offset: 0x000C4434
		private static BigInteger Validate(BigInteger y, DsaParameters parameters)
		{
			if (parameters != null && (y.CompareTo(BigInteger.Two) < 0 || y.CompareTo(parameters.P.Subtract(BigInteger.Two)) > 0 || !y.ModPow(parameters.Q, parameters.P).Equals(BigInteger.One)))
			{
				throw new ArgumentException("y value does not appear to be in correct group");
			}
			return y;
		}

		// Token: 0x06002242 RID: 8770 RVA: 0x000C44A8 File Offset: 0x000C44A8
		public DsaPublicKeyParameters(BigInteger y, DsaParameters parameters) : base(false, parameters)
		{
			if (y == null)
			{
				throw new ArgumentNullException("y");
			}
			this.y = DsaPublicKeyParameters.Validate(y, parameters);
		}

		// Token: 0x1700067D RID: 1661
		// (get) Token: 0x06002243 RID: 8771 RVA: 0x000C44D0 File Offset: 0x000C44D0
		public BigInteger Y
		{
			get
			{
				return this.y;
			}
		}

		// Token: 0x06002244 RID: 8772 RVA: 0x000C44D8 File Offset: 0x000C44D8
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DsaPublicKeyParameters dsaPublicKeyParameters = obj as DsaPublicKeyParameters;
			return dsaPublicKeyParameters != null && this.Equals(dsaPublicKeyParameters);
		}

		// Token: 0x06002245 RID: 8773 RVA: 0x000C4508 File Offset: 0x000C4508
		protected bool Equals(DsaPublicKeyParameters other)
		{
			return this.y.Equals(other.y) && base.Equals(other);
		}

		// Token: 0x06002246 RID: 8774 RVA: 0x000C452C File Offset: 0x000C452C
		public override int GetHashCode()
		{
			return this.y.GetHashCode() ^ base.GetHashCode();
		}

		// Token: 0x040015FC RID: 5628
		private readonly BigInteger y;
	}
}
