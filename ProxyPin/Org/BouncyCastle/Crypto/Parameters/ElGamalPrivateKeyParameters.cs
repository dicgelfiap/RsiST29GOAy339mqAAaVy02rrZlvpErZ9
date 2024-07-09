using System;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x02000452 RID: 1106
	public class ElGamalPrivateKeyParameters : ElGamalKeyParameters
	{
		// Token: 0x060022B6 RID: 8886 RVA: 0x000C563C File Offset: 0x000C563C
		public ElGamalPrivateKeyParameters(BigInteger x, ElGamalParameters parameters) : base(true, parameters)
		{
			if (x == null)
			{
				throw new ArgumentNullException("x");
			}
			this.x = x;
		}

		// Token: 0x17000695 RID: 1685
		// (get) Token: 0x060022B7 RID: 8887 RVA: 0x000C5660 File Offset: 0x000C5660
		public BigInteger X
		{
			get
			{
				return this.x;
			}
		}

		// Token: 0x060022B8 RID: 8888 RVA: 0x000C5668 File Offset: 0x000C5668
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			ElGamalPrivateKeyParameters elGamalPrivateKeyParameters = obj as ElGamalPrivateKeyParameters;
			return elGamalPrivateKeyParameters != null && this.Equals(elGamalPrivateKeyParameters);
		}

		// Token: 0x060022B9 RID: 8889 RVA: 0x000C5698 File Offset: 0x000C5698
		protected bool Equals(ElGamalPrivateKeyParameters other)
		{
			return other.x.Equals(this.x) && base.Equals(other);
		}

		// Token: 0x060022BA RID: 8890 RVA: 0x000C56BC File Offset: 0x000C56BC
		public override int GetHashCode()
		{
			return this.x.GetHashCode() ^ base.GetHashCode();
		}

		// Token: 0x04001623 RID: 5667
		private readonly BigInteger x;
	}
}
