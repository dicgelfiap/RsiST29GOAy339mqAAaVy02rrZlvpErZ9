using System;

namespace Org.BouncyCastle.Math.EC.Endo
{
	// Token: 0x020005EF RID: 1519
	public class GlvTypeBParameters
	{
		// Token: 0x060032FF RID: 13055 RVA: 0x00108640 File Offset: 0x00108640
		[Obsolete("Use constructor taking a ScalarSplitParameters instead")]
		public GlvTypeBParameters(BigInteger beta, BigInteger lambda, BigInteger[] v1, BigInteger[] v2, BigInteger g1, BigInteger g2, int bits)
		{
			this.m_beta = beta;
			this.m_lambda = lambda;
			this.m_splitParams = new ScalarSplitParameters(v1, v2, g1, g2, bits);
		}

		// Token: 0x06003300 RID: 13056 RVA: 0x0010866C File Offset: 0x0010866C
		public GlvTypeBParameters(BigInteger beta, BigInteger lambda, ScalarSplitParameters splitParams)
		{
			this.m_beta = beta;
			this.m_lambda = lambda;
			this.m_splitParams = splitParams;
		}

		// Token: 0x17000950 RID: 2384
		// (get) Token: 0x06003301 RID: 13057 RVA: 0x0010868C File Offset: 0x0010868C
		public virtual BigInteger Beta
		{
			get
			{
				return this.m_beta;
			}
		}

		// Token: 0x17000951 RID: 2385
		// (get) Token: 0x06003302 RID: 13058 RVA: 0x00108694 File Offset: 0x00108694
		public virtual BigInteger Lambda
		{
			get
			{
				return this.m_lambda;
			}
		}

		// Token: 0x17000952 RID: 2386
		// (get) Token: 0x06003303 RID: 13059 RVA: 0x0010869C File Offset: 0x0010869C
		public virtual ScalarSplitParameters SplitParams
		{
			get
			{
				return this.m_splitParams;
			}
		}

		// Token: 0x17000953 RID: 2387
		// (get) Token: 0x06003304 RID: 13060 RVA: 0x001086A4 File Offset: 0x001086A4
		[Obsolete("Access via SplitParams instead")]
		public virtual BigInteger[] V1
		{
			get
			{
				return new BigInteger[]
				{
					this.m_splitParams.V1A,
					this.m_splitParams.V1B
				};
			}
		}

		// Token: 0x17000954 RID: 2388
		// (get) Token: 0x06003305 RID: 13061 RVA: 0x001086E4 File Offset: 0x001086E4
		[Obsolete("Access via SplitParams instead")]
		public virtual BigInteger[] V2
		{
			get
			{
				return new BigInteger[]
				{
					this.m_splitParams.V2A,
					this.m_splitParams.V2B
				};
			}
		}

		// Token: 0x17000955 RID: 2389
		// (get) Token: 0x06003306 RID: 13062 RVA: 0x00108724 File Offset: 0x00108724
		[Obsolete("Access via SplitParams instead")]
		public virtual BigInteger G1
		{
			get
			{
				return this.m_splitParams.G1;
			}
		}

		// Token: 0x17000956 RID: 2390
		// (get) Token: 0x06003307 RID: 13063 RVA: 0x00108734 File Offset: 0x00108734
		[Obsolete("Access via SplitParams instead")]
		public virtual BigInteger G2
		{
			get
			{
				return this.m_splitParams.G2;
			}
		}

		// Token: 0x17000957 RID: 2391
		// (get) Token: 0x06003308 RID: 13064 RVA: 0x00108744 File Offset: 0x00108744
		[Obsolete("Access via SplitParams instead")]
		public virtual int Bits
		{
			get
			{
				return this.m_splitParams.Bits;
			}
		}

		// Token: 0x04001C7D RID: 7293
		protected readonly BigInteger m_beta;

		// Token: 0x04001C7E RID: 7294
		protected readonly BigInteger m_lambda;

		// Token: 0x04001C7F RID: 7295
		protected readonly ScalarSplitParameters m_splitParams;
	}
}
