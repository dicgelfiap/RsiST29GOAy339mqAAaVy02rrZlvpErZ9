using System;

namespace Org.BouncyCastle.Math.EC.Endo
{
	// Token: 0x020005F0 RID: 1520
	public class ScalarSplitParameters
	{
		// Token: 0x06003309 RID: 13065 RVA: 0x00108754 File Offset: 0x00108754
		private static void CheckVector(BigInteger[] v, string name)
		{
			if (v == null || v.Length != 2 || v[0] == null || v[1] == null)
			{
				throw new ArgumentException("Must consist of exactly 2 (non-null) values", name);
			}
		}

		// Token: 0x0600330A RID: 13066 RVA: 0x0010878C File Offset: 0x0010878C
		public ScalarSplitParameters(BigInteger[] v1, BigInteger[] v2, BigInteger g1, BigInteger g2, int bits)
		{
			ScalarSplitParameters.CheckVector(v1, "v1");
			ScalarSplitParameters.CheckVector(v2, "v2");
			this.m_v1A = v1[0];
			this.m_v1B = v1[1];
			this.m_v2A = v2[0];
			this.m_v2B = v2[1];
			this.m_g1 = g1;
			this.m_g2 = g2;
			this.m_bits = bits;
		}

		// Token: 0x17000958 RID: 2392
		// (get) Token: 0x0600330B RID: 13067 RVA: 0x00108804 File Offset: 0x00108804
		public virtual BigInteger V1A
		{
			get
			{
				return this.m_v1A;
			}
		}

		// Token: 0x17000959 RID: 2393
		// (get) Token: 0x0600330C RID: 13068 RVA: 0x0010880C File Offset: 0x0010880C
		public virtual BigInteger V1B
		{
			get
			{
				return this.m_v1B;
			}
		}

		// Token: 0x1700095A RID: 2394
		// (get) Token: 0x0600330D RID: 13069 RVA: 0x00108814 File Offset: 0x00108814
		public virtual BigInteger V2A
		{
			get
			{
				return this.m_v2A;
			}
		}

		// Token: 0x1700095B RID: 2395
		// (get) Token: 0x0600330E RID: 13070 RVA: 0x0010881C File Offset: 0x0010881C
		public virtual BigInteger V2B
		{
			get
			{
				return this.m_v2B;
			}
		}

		// Token: 0x1700095C RID: 2396
		// (get) Token: 0x0600330F RID: 13071 RVA: 0x00108824 File Offset: 0x00108824
		public virtual BigInteger G1
		{
			get
			{
				return this.m_g1;
			}
		}

		// Token: 0x1700095D RID: 2397
		// (get) Token: 0x06003310 RID: 13072 RVA: 0x0010882C File Offset: 0x0010882C
		public virtual BigInteger G2
		{
			get
			{
				return this.m_g2;
			}
		}

		// Token: 0x1700095E RID: 2398
		// (get) Token: 0x06003311 RID: 13073 RVA: 0x00108834 File Offset: 0x00108834
		public virtual int Bits
		{
			get
			{
				return this.m_bits;
			}
		}

		// Token: 0x04001C80 RID: 7296
		protected readonly BigInteger m_v1A;

		// Token: 0x04001C81 RID: 7297
		protected readonly BigInteger m_v1B;

		// Token: 0x04001C82 RID: 7298
		protected readonly BigInteger m_v2A;

		// Token: 0x04001C83 RID: 7299
		protected readonly BigInteger m_v2B;

		// Token: 0x04001C84 RID: 7300
		protected readonly BigInteger m_g1;

		// Token: 0x04001C85 RID: 7301
		protected readonly BigInteger m_g2;

		// Token: 0x04001C86 RID: 7302
		protected readonly int m_bits;
	}
}
