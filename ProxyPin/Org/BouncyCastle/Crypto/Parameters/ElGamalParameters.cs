using System;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x02000451 RID: 1105
	public class ElGamalParameters : ICipherParameters
	{
		// Token: 0x060022AE RID: 8878 RVA: 0x000C5548 File Offset: 0x000C5548
		public ElGamalParameters(BigInteger p, BigInteger g) : this(p, g, 0)
		{
		}

		// Token: 0x060022AF RID: 8879 RVA: 0x000C5554 File Offset: 0x000C5554
		public ElGamalParameters(BigInteger p, BigInteger g, int l)
		{
			if (p == null)
			{
				throw new ArgumentNullException("p");
			}
			if (g == null)
			{
				throw new ArgumentNullException("g");
			}
			this.p = p;
			this.g = g;
			this.l = l;
		}

		// Token: 0x17000692 RID: 1682
		// (get) Token: 0x060022B0 RID: 8880 RVA: 0x000C5594 File Offset: 0x000C5594
		public BigInteger P
		{
			get
			{
				return this.p;
			}
		}

		// Token: 0x17000693 RID: 1683
		// (get) Token: 0x060022B1 RID: 8881 RVA: 0x000C559C File Offset: 0x000C559C
		public BigInteger G
		{
			get
			{
				return this.g;
			}
		}

		// Token: 0x17000694 RID: 1684
		// (get) Token: 0x060022B2 RID: 8882 RVA: 0x000C55A4 File Offset: 0x000C55A4
		public int L
		{
			get
			{
				return this.l;
			}
		}

		// Token: 0x060022B3 RID: 8883 RVA: 0x000C55AC File Offset: 0x000C55AC
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			ElGamalParameters elGamalParameters = obj as ElGamalParameters;
			return elGamalParameters != null && this.Equals(elGamalParameters);
		}

		// Token: 0x060022B4 RID: 8884 RVA: 0x000C55DC File Offset: 0x000C55DC
		protected bool Equals(ElGamalParameters other)
		{
			return this.p.Equals(other.p) && this.g.Equals(other.g) && this.l == other.l;
		}

		// Token: 0x060022B5 RID: 8885 RVA: 0x000C561C File Offset: 0x000C561C
		public override int GetHashCode()
		{
			return this.p.GetHashCode() ^ this.g.GetHashCode() ^ this.l;
		}

		// Token: 0x04001620 RID: 5664
		private readonly BigInteger p;

		// Token: 0x04001621 RID: 5665
		private readonly BigInteger g;

		// Token: 0x04001622 RID: 5666
		private readonly int l;
	}
}
