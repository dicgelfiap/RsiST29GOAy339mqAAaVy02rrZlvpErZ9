using System;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x02000459 RID: 1113
	public class Gost3410ValidationParameters
	{
		// Token: 0x060022D9 RID: 8921 RVA: 0x000C5B60 File Offset: 0x000C5B60
		public Gost3410ValidationParameters(int x0, int c)
		{
			this.x0 = x0;
			this.c = c;
		}

		// Token: 0x060022DA RID: 8922 RVA: 0x000C5B78 File Offset: 0x000C5B78
		public Gost3410ValidationParameters(long x0L, long cL)
		{
			this.x0L = x0L;
			this.cL = cL;
		}

		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x060022DB RID: 8923 RVA: 0x000C5B90 File Offset: 0x000C5B90
		public int C
		{
			get
			{
				return this.c;
			}
		}

		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x060022DC RID: 8924 RVA: 0x000C5B98 File Offset: 0x000C5B98
		public int X0
		{
			get
			{
				return this.x0;
			}
		}

		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x060022DD RID: 8925 RVA: 0x000C5BA0 File Offset: 0x000C5BA0
		public long CL
		{
			get
			{
				return this.cL;
			}
		}

		// Token: 0x170006A4 RID: 1700
		// (get) Token: 0x060022DE RID: 8926 RVA: 0x000C5BA8 File Offset: 0x000C5BA8
		public long X0L
		{
			get
			{
				return this.x0L;
			}
		}

		// Token: 0x060022DF RID: 8927 RVA: 0x000C5BB0 File Offset: 0x000C5BB0
		public override bool Equals(object obj)
		{
			Gost3410ValidationParameters gost3410ValidationParameters = obj as Gost3410ValidationParameters;
			return gost3410ValidationParameters != null && gost3410ValidationParameters.c == this.c && gost3410ValidationParameters.x0 == this.x0 && gost3410ValidationParameters.cL == this.cL && gost3410ValidationParameters.x0L == this.x0L;
		}

		// Token: 0x060022E0 RID: 8928 RVA: 0x000C5C14 File Offset: 0x000C5C14
		public override int GetHashCode()
		{
			return this.c.GetHashCode() ^ this.x0.GetHashCode() ^ this.cL.GetHashCode() ^ this.x0L.GetHashCode();
		}

		// Token: 0x0400162F RID: 5679
		private int x0;

		// Token: 0x04001630 RID: 5680
		private int c;

		// Token: 0x04001631 RID: 5681
		private long x0L;

		// Token: 0x04001632 RID: 5682
		private long cL;
	}
}
