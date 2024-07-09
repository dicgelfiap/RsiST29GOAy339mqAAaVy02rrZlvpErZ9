using System;

namespace Org.BouncyCastle.Math.EC
{
	// Token: 0x02000615 RID: 1557
	public class ScaleXPointMap : ECPointMap
	{
		// Token: 0x06003515 RID: 13589 RVA: 0x00118030 File Offset: 0x00118030
		public ScaleXPointMap(ECFieldElement scale)
		{
			this.scale = scale;
		}

		// Token: 0x06003516 RID: 13590 RVA: 0x00118040 File Offset: 0x00118040
		public virtual ECPoint Map(ECPoint p)
		{
			return p.ScaleX(this.scale);
		}

		// Token: 0x04001D13 RID: 7443
		protected readonly ECFieldElement scale;
	}
}
