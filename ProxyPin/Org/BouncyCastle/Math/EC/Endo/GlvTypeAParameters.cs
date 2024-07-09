using System;

namespace Org.BouncyCastle.Math.EC.Endo
{
	// Token: 0x020005ED RID: 1517
	public class GlvTypeAParameters
	{
		// Token: 0x060032F7 RID: 13047 RVA: 0x001085C0 File Offset: 0x001085C0
		public GlvTypeAParameters(BigInteger i, BigInteger lambda, ScalarSplitParameters splitParams)
		{
			this.m_i = i;
			this.m_lambda = lambda;
			this.m_splitParams = splitParams;
		}

		// Token: 0x1700094B RID: 2379
		// (get) Token: 0x060032F8 RID: 13048 RVA: 0x001085E0 File Offset: 0x001085E0
		public virtual BigInteger I
		{
			get
			{
				return this.m_i;
			}
		}

		// Token: 0x1700094C RID: 2380
		// (get) Token: 0x060032F9 RID: 13049 RVA: 0x001085E8 File Offset: 0x001085E8
		public virtual BigInteger Lambda
		{
			get
			{
				return this.m_lambda;
			}
		}

		// Token: 0x1700094D RID: 2381
		// (get) Token: 0x060032FA RID: 13050 RVA: 0x001085F0 File Offset: 0x001085F0
		public virtual ScalarSplitParameters SplitParams
		{
			get
			{
				return this.m_splitParams;
			}
		}

		// Token: 0x04001C78 RID: 7288
		protected readonly BigInteger m_i;

		// Token: 0x04001C79 RID: 7289
		protected readonly BigInteger m_lambda;

		// Token: 0x04001C7A RID: 7290
		protected readonly ScalarSplitParameters m_splitParams;
	}
}
