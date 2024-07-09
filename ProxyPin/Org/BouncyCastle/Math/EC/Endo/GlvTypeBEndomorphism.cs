using System;

namespace Org.BouncyCastle.Math.EC.Endo
{
	// Token: 0x020005EE RID: 1518
	public class GlvTypeBEndomorphism : GlvEndomorphism, ECEndomorphism
	{
		// Token: 0x060032FB RID: 13051 RVA: 0x001085F8 File Offset: 0x001085F8
		public GlvTypeBEndomorphism(ECCurve curve, GlvTypeBParameters parameters)
		{
			this.m_parameters = parameters;
			this.m_pointMap = new ScaleXPointMap(curve.FromBigInteger(parameters.Beta));
		}

		// Token: 0x060032FC RID: 13052 RVA: 0x00108620 File Offset: 0x00108620
		public virtual BigInteger[] DecomposeScalar(BigInteger k)
		{
			return EndoUtilities.DecomposeScalar(this.m_parameters.SplitParams, k);
		}

		// Token: 0x1700094E RID: 2382
		// (get) Token: 0x060032FD RID: 13053 RVA: 0x00108634 File Offset: 0x00108634
		public virtual ECPointMap PointMap
		{
			get
			{
				return this.m_pointMap;
			}
		}

		// Token: 0x1700094F RID: 2383
		// (get) Token: 0x060032FE RID: 13054 RVA: 0x0010863C File Offset: 0x0010863C
		public virtual bool HasEfficientPointMap
		{
			get
			{
				return true;
			}
		}

		// Token: 0x04001C7B RID: 7291
		protected readonly GlvTypeBParameters m_parameters;

		// Token: 0x04001C7C RID: 7292
		protected readonly ECPointMap m_pointMap;
	}
}
