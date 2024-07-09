using System;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x02000437 RID: 1079
	public class DHParameters : ICipherParameters
	{
		// Token: 0x06002201 RID: 8705 RVA: 0x000C3B78 File Offset: 0x000C3B78
		private static int GetDefaultMParam(int lParam)
		{
			if (lParam == 0)
			{
				return 160;
			}
			return Math.Min(lParam, 160);
		}

		// Token: 0x06002202 RID: 8706 RVA: 0x000C3B94 File Offset: 0x000C3B94
		public DHParameters(BigInteger p, BigInteger g) : this(p, g, null, 0)
		{
		}

		// Token: 0x06002203 RID: 8707 RVA: 0x000C3BA0 File Offset: 0x000C3BA0
		public DHParameters(BigInteger p, BigInteger g, BigInteger q) : this(p, g, q, 0)
		{
		}

		// Token: 0x06002204 RID: 8708 RVA: 0x000C3BAC File Offset: 0x000C3BAC
		public DHParameters(BigInteger p, BigInteger g, BigInteger q, int l) : this(p, g, q, DHParameters.GetDefaultMParam(l), l, null, null)
		{
		}

		// Token: 0x06002205 RID: 8709 RVA: 0x000C3BD4 File Offset: 0x000C3BD4
		public DHParameters(BigInteger p, BigInteger g, BigInteger q, int m, int l) : this(p, g, q, m, l, null, null)
		{
		}

		// Token: 0x06002206 RID: 8710 RVA: 0x000C3BF4 File Offset: 0x000C3BF4
		public DHParameters(BigInteger p, BigInteger g, BigInteger q, BigInteger j, DHValidationParameters validation) : this(p, g, q, 160, 0, j, validation)
		{
		}

		// Token: 0x06002207 RID: 8711 RVA: 0x000C3C18 File Offset: 0x000C3C18
		public DHParameters(BigInteger p, BigInteger g, BigInteger q, int m, int l, BigInteger j, DHValidationParameters validation)
		{
			if (p == null)
			{
				throw new ArgumentNullException("p");
			}
			if (g == null)
			{
				throw new ArgumentNullException("g");
			}
			if (!p.TestBit(0))
			{
				throw new ArgumentException("field must be an odd prime", "p");
			}
			if (g.CompareTo(BigInteger.Two) < 0 || g.CompareTo(p.Subtract(BigInteger.Two)) > 0)
			{
				throw new ArgumentException("generator must in the range [2, p - 2]", "g");
			}
			if (q != null && q.BitLength >= p.BitLength)
			{
				throw new ArgumentException("q too big to be a factor of (p-1)", "q");
			}
			if (m >= p.BitLength)
			{
				throw new ArgumentException("m value must be < bitlength of p", "m");
			}
			if (l != 0)
			{
				if (l >= p.BitLength)
				{
					throw new ArgumentException("when l value specified, it must be less than bitlength(p)", "l");
				}
				if (l < m)
				{
					throw new ArgumentException("when l value specified, it may not be less than m value", "l");
				}
			}
			if (j != null && j.CompareTo(BigInteger.Two) < 0)
			{
				throw new ArgumentException("subgroup factor must be >= 2", "j");
			}
			this.p = p;
			this.g = g;
			this.q = q;
			this.m = m;
			this.l = l;
			this.j = j;
			this.validation = validation;
		}

		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x06002208 RID: 8712 RVA: 0x000C3D84 File Offset: 0x000C3D84
		public BigInteger P
		{
			get
			{
				return this.p;
			}
		}

		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x06002209 RID: 8713 RVA: 0x000C3D8C File Offset: 0x000C3D8C
		public BigInteger G
		{
			get
			{
				return this.g;
			}
		}

		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x0600220A RID: 8714 RVA: 0x000C3D94 File Offset: 0x000C3D94
		public BigInteger Q
		{
			get
			{
				return this.q;
			}
		}

		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x0600220B RID: 8715 RVA: 0x000C3D9C File Offset: 0x000C3D9C
		public BigInteger J
		{
			get
			{
				return this.j;
			}
		}

		// Token: 0x1700066B RID: 1643
		// (get) Token: 0x0600220C RID: 8716 RVA: 0x000C3DA4 File Offset: 0x000C3DA4
		public int M
		{
			get
			{
				return this.m;
			}
		}

		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x0600220D RID: 8717 RVA: 0x000C3DAC File Offset: 0x000C3DAC
		public int L
		{
			get
			{
				return this.l;
			}
		}

		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x0600220E RID: 8718 RVA: 0x000C3DB4 File Offset: 0x000C3DB4
		public DHValidationParameters ValidationParameters
		{
			get
			{
				return this.validation;
			}
		}

		// Token: 0x0600220F RID: 8719 RVA: 0x000C3DBC File Offset: 0x000C3DBC
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DHParameters dhparameters = obj as DHParameters;
			return dhparameters != null && this.Equals(dhparameters);
		}

		// Token: 0x06002210 RID: 8720 RVA: 0x000C3DEC File Offset: 0x000C3DEC
		protected virtual bool Equals(DHParameters other)
		{
			return this.p.Equals(other.p) && this.g.Equals(other.g) && object.Equals(this.q, other.q);
		}

		// Token: 0x06002211 RID: 8721 RVA: 0x000C3E3C File Offset: 0x000C3E3C
		public override int GetHashCode()
		{
			int num = this.p.GetHashCode() ^ this.g.GetHashCode();
			if (this.q != null)
			{
				num ^= this.q.GetHashCode();
			}
			return num;
		}

		// Token: 0x040015E2 RID: 5602
		private const int DefaultMinimumLength = 160;

		// Token: 0x040015E3 RID: 5603
		private readonly BigInteger p;

		// Token: 0x040015E4 RID: 5604
		private readonly BigInteger g;

		// Token: 0x040015E5 RID: 5605
		private readonly BigInteger q;

		// Token: 0x040015E6 RID: 5606
		private readonly BigInteger j;

		// Token: 0x040015E7 RID: 5607
		private readonly int m;

		// Token: 0x040015E8 RID: 5608
		private readonly int l;

		// Token: 0x040015E9 RID: 5609
		private readonly DHValidationParameters validation;
	}
}
