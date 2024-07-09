using System;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x02000453 RID: 1107
	public class ElGamalPublicKeyParameters : ElGamalKeyParameters
	{
		// Token: 0x060022BB RID: 8891 RVA: 0x000C56D0 File Offset: 0x000C56D0
		public ElGamalPublicKeyParameters(BigInteger y, ElGamalParameters parameters) : base(false, parameters)
		{
			if (y == null)
			{
				throw new ArgumentNullException("y");
			}
			this.y = y;
		}

		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x060022BC RID: 8892 RVA: 0x000C56F4 File Offset: 0x000C56F4
		public BigInteger Y
		{
			get
			{
				return this.y;
			}
		}

		// Token: 0x060022BD RID: 8893 RVA: 0x000C56FC File Offset: 0x000C56FC
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			ElGamalPublicKeyParameters elGamalPublicKeyParameters = obj as ElGamalPublicKeyParameters;
			return elGamalPublicKeyParameters != null && this.Equals(elGamalPublicKeyParameters);
		}

		// Token: 0x060022BE RID: 8894 RVA: 0x000C572C File Offset: 0x000C572C
		protected bool Equals(ElGamalPublicKeyParameters other)
		{
			return this.y.Equals(other.y) && base.Equals(other);
		}

		// Token: 0x060022BF RID: 8895 RVA: 0x000C5750 File Offset: 0x000C5750
		public override int GetHashCode()
		{
			return this.y.GetHashCode() ^ base.GetHashCode();
		}

		// Token: 0x04001624 RID: 5668
		private readonly BigInteger y;
	}
}
