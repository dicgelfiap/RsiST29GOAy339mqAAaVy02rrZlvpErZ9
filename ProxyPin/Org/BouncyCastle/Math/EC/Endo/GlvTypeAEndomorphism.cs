using System;

namespace Org.BouncyCastle.Math.EC.Endo
{
	// Token: 0x020005EC RID: 1516
	public class GlvTypeAEndomorphism : GlvEndomorphism, ECEndomorphism
	{
		// Token: 0x060032F3 RID: 13043 RVA: 0x00108578 File Offset: 0x00108578
		public GlvTypeAEndomorphism(ECCurve curve, GlvTypeAParameters parameters)
		{
			this.m_parameters = parameters;
			this.m_pointMap = new ScaleYNegateXPointMap(curve.FromBigInteger(parameters.I));
		}

		// Token: 0x060032F4 RID: 13044 RVA: 0x001085A0 File Offset: 0x001085A0
		public virtual BigInteger[] DecomposeScalar(BigInteger k)
		{
			return EndoUtilities.DecomposeScalar(this.m_parameters.SplitParams, k);
		}

		// Token: 0x17000949 RID: 2377
		// (get) Token: 0x060032F5 RID: 13045 RVA: 0x001085B4 File Offset: 0x001085B4
		public virtual ECPointMap PointMap
		{
			get
			{
				return this.m_pointMap;
			}
		}

		// Token: 0x1700094A RID: 2378
		// (get) Token: 0x060032F6 RID: 13046 RVA: 0x001085BC File Offset: 0x001085BC
		public virtual bool HasEfficientPointMap
		{
			get
			{
				return true;
			}
		}

		// Token: 0x04001C76 RID: 7286
		protected readonly GlvTypeAParameters m_parameters;

		// Token: 0x04001C77 RID: 7287
		protected readonly ECPointMap m_pointMap;
	}
}
