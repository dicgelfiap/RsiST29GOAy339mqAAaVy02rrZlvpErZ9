using System;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Math.EC;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x02000442 RID: 1090
	public class ECDomainParameters
	{
		// Token: 0x0600224F RID: 8783 RVA: 0x000C462C File Offset: 0x000C462C
		public ECDomainParameters(ECCurve curve, ECPoint g, BigInteger n) : this(curve, g, n, BigInteger.One, null)
		{
		}

		// Token: 0x06002250 RID: 8784 RVA: 0x000C4640 File Offset: 0x000C4640
		public ECDomainParameters(ECCurve curve, ECPoint g, BigInteger n, BigInteger h) : this(curve, g, n, h, null)
		{
		}

		// Token: 0x06002251 RID: 8785 RVA: 0x000C4650 File Offset: 0x000C4650
		public ECDomainParameters(ECCurve curve, ECPoint g, BigInteger n, BigInteger h, byte[] seed)
		{
			if (curve == null)
			{
				throw new ArgumentNullException("curve");
			}
			if (g == null)
			{
				throw new ArgumentNullException("g");
			}
			if (n == null)
			{
				throw new ArgumentNullException("n");
			}
			this.curve = curve;
			this.g = ECDomainParameters.ValidatePublicPoint(curve, g);
			this.n = n;
			this.h = h;
			this.seed = Arrays.Clone(seed);
		}

		// Token: 0x17000680 RID: 1664
		// (get) Token: 0x06002252 RID: 8786 RVA: 0x000C46CC File Offset: 0x000C46CC
		public ECCurve Curve
		{
			get
			{
				return this.curve;
			}
		}

		// Token: 0x17000681 RID: 1665
		// (get) Token: 0x06002253 RID: 8787 RVA: 0x000C46D4 File Offset: 0x000C46D4
		public ECPoint G
		{
			get
			{
				return this.g;
			}
		}

		// Token: 0x17000682 RID: 1666
		// (get) Token: 0x06002254 RID: 8788 RVA: 0x000C46DC File Offset: 0x000C46DC
		public BigInteger N
		{
			get
			{
				return this.n;
			}
		}

		// Token: 0x17000683 RID: 1667
		// (get) Token: 0x06002255 RID: 8789 RVA: 0x000C46E4 File Offset: 0x000C46E4
		public BigInteger H
		{
			get
			{
				return this.h;
			}
		}

		// Token: 0x17000684 RID: 1668
		// (get) Token: 0x06002256 RID: 8790 RVA: 0x000C46EC File Offset: 0x000C46EC
		public BigInteger HInv
		{
			get
			{
				BigInteger result;
				lock (this)
				{
					if (this.hInv == null)
					{
						this.hInv = this.h.ModInverse(this.n);
					}
					result = this.hInv;
				}
				return result;
			}
		}

		// Token: 0x06002257 RID: 8791 RVA: 0x000C4748 File Offset: 0x000C4748
		public byte[] GetSeed()
		{
			return Arrays.Clone(this.seed);
		}

		// Token: 0x06002258 RID: 8792 RVA: 0x000C4758 File Offset: 0x000C4758
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			ECDomainParameters ecdomainParameters = obj as ECDomainParameters;
			return ecdomainParameters != null && this.Equals(ecdomainParameters);
		}

		// Token: 0x06002259 RID: 8793 RVA: 0x000C4788 File Offset: 0x000C4788
		protected virtual bool Equals(ECDomainParameters other)
		{
			return this.curve.Equals(other.curve) && this.g.Equals(other.g) && this.n.Equals(other.n);
		}

		// Token: 0x0600225A RID: 8794 RVA: 0x000C47D8 File Offset: 0x000C47D8
		public override int GetHashCode()
		{
			int num = 4;
			num *= 257;
			num ^= this.curve.GetHashCode();
			num *= 257;
			num ^= this.g.GetHashCode();
			num *= 257;
			return num ^ this.n.GetHashCode();
		}

		// Token: 0x0600225B RID: 8795 RVA: 0x000C4830 File Offset: 0x000C4830
		public BigInteger ValidatePrivateScalar(BigInteger d)
		{
			if (d == null)
			{
				throw new ArgumentNullException("d", "Scalar cannot be null");
			}
			if (d.CompareTo(BigInteger.One) < 0 || d.CompareTo(this.N) >= 0)
			{
				throw new ArgumentException("Scalar is not in the interval [1, n - 1]", "d");
			}
			return d;
		}

		// Token: 0x0600225C RID: 8796 RVA: 0x000C488C File Offset: 0x000C488C
		public ECPoint ValidatePublicPoint(ECPoint q)
		{
			return ECDomainParameters.ValidatePublicPoint(this.Curve, q);
		}

		// Token: 0x0600225D RID: 8797 RVA: 0x000C489C File Offset: 0x000C489C
		internal static ECPoint ValidatePublicPoint(ECCurve c, ECPoint q)
		{
			if (q == null)
			{
				throw new ArgumentNullException("q", "Point cannot be null");
			}
			q = ECAlgorithms.ImportPoint(c, q).Normalize();
			if (q.IsInfinity)
			{
				throw new ArgumentException("Point at infinity", "q");
			}
			if (!q.IsValid())
			{
				throw new ArgumentException("Point not on curve", "q");
			}
			return q;
		}

		// Token: 0x04001600 RID: 5632
		private readonly ECCurve curve;

		// Token: 0x04001601 RID: 5633
		private readonly byte[] seed;

		// Token: 0x04001602 RID: 5634
		private readonly ECPoint g;

		// Token: 0x04001603 RID: 5635
		private readonly BigInteger n;

		// Token: 0x04001604 RID: 5636
		private readonly BigInteger h;

		// Token: 0x04001605 RID: 5637
		private BigInteger hInv;
	}
}
