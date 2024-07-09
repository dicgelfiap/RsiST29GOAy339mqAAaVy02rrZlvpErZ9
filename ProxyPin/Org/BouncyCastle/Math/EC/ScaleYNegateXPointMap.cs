using System;

namespace Org.BouncyCastle.Math.EC
{
	// Token: 0x02000616 RID: 1558
	public class ScaleYNegateXPointMap : ECPointMap
	{
		// Token: 0x06003517 RID: 13591 RVA: 0x00118050 File Offset: 0x00118050
		public ScaleYNegateXPointMap(ECFieldElement scale)
		{
			this.scale = scale;
		}

		// Token: 0x06003518 RID: 13592 RVA: 0x00118060 File Offset: 0x00118060
		public virtual ECPoint Map(ECPoint p)
		{
			return p.ScaleYNegateX(this.scale);
		}

		// Token: 0x04001D14 RID: 7444
		protected readonly ECFieldElement scale;
	}
}
