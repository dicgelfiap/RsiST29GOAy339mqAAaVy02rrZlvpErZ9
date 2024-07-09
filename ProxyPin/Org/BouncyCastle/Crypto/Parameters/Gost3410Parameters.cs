using System;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x02000456 RID: 1110
	public class Gost3410Parameters : ICipherParameters
	{
		// Token: 0x060022CA RID: 8906 RVA: 0x000C58A4 File Offset: 0x000C58A4
		public Gost3410Parameters(BigInteger p, BigInteger q, BigInteger a) : this(p, q, a, null)
		{
		}

		// Token: 0x060022CB RID: 8907 RVA: 0x000C58B0 File Offset: 0x000C58B0
		public Gost3410Parameters(BigInteger p, BigInteger q, BigInteger a, Gost3410ValidationParameters validation)
		{
			if (p == null)
			{
				throw new ArgumentNullException("p");
			}
			if (q == null)
			{
				throw new ArgumentNullException("q");
			}
			if (a == null)
			{
				throw new ArgumentNullException("a");
			}
			this.p = p;
			this.q = q;
			this.a = a;
			this.validation = validation;
		}

		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x060022CC RID: 8908 RVA: 0x000C5918 File Offset: 0x000C5918
		public BigInteger P
		{
			get
			{
				return this.p;
			}
		}

		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x060022CD RID: 8909 RVA: 0x000C5920 File Offset: 0x000C5920
		public BigInteger Q
		{
			get
			{
				return this.q;
			}
		}

		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x060022CE RID: 8910 RVA: 0x000C5928 File Offset: 0x000C5928
		public BigInteger A
		{
			get
			{
				return this.a;
			}
		}

		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x060022CF RID: 8911 RVA: 0x000C5930 File Offset: 0x000C5930
		public Gost3410ValidationParameters ValidationParameters
		{
			get
			{
				return this.validation;
			}
		}

		// Token: 0x060022D0 RID: 8912 RVA: 0x000C5938 File Offset: 0x000C5938
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			Gost3410Parameters gost3410Parameters = obj as Gost3410Parameters;
			return gost3410Parameters != null && this.Equals(gost3410Parameters);
		}

		// Token: 0x060022D1 RID: 8913 RVA: 0x000C5968 File Offset: 0x000C5968
		protected bool Equals(Gost3410Parameters other)
		{
			return this.p.Equals(other.p) && this.q.Equals(other.q) && this.a.Equals(other.a);
		}

		// Token: 0x060022D2 RID: 8914 RVA: 0x000C59B8 File Offset: 0x000C59B8
		public override int GetHashCode()
		{
			return this.p.GetHashCode() ^ this.q.GetHashCode() ^ this.a.GetHashCode();
		}

		// Token: 0x04001629 RID: 5673
		private readonly BigInteger p;

		// Token: 0x0400162A RID: 5674
		private readonly BigInteger q;

		// Token: 0x0400162B RID: 5675
		private readonly BigInteger a;

		// Token: 0x0400162C RID: 5676
		private readonly Gost3410ValidationParameters validation;
	}
}
