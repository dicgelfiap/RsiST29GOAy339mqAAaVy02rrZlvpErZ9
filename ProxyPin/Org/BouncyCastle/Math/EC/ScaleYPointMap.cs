using System;

namespace Org.BouncyCastle.Math.EC
{
	// Token: 0x02000617 RID: 1559
	public class ScaleYPointMap : ECPointMap
	{
		// Token: 0x06003519 RID: 13593 RVA: 0x00118070 File Offset: 0x00118070
		public ScaleYPointMap(ECFieldElement scale)
		{
			this.scale = scale;
		}

		// Token: 0x0600351A RID: 13594 RVA: 0x00118080 File Offset: 0x00118080
		public virtual ECPoint Map(ECPoint p)
		{
			return p.ScaleY(this.scale);
		}

		// Token: 0x04001D15 RID: 7445
		protected readonly ECFieldElement scale;
	}
}
