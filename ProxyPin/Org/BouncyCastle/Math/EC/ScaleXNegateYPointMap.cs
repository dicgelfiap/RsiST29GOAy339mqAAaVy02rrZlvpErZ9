using System;

namespace Org.BouncyCastle.Math.EC
{
	// Token: 0x02000614 RID: 1556
	public class ScaleXNegateYPointMap : ECPointMap
	{
		// Token: 0x06003513 RID: 13587 RVA: 0x00118010 File Offset: 0x00118010
		public ScaleXNegateYPointMap(ECFieldElement scale)
		{
			this.scale = scale;
		}

		// Token: 0x06003514 RID: 13588 RVA: 0x00118020 File Offset: 0x00118020
		public virtual ECPoint Map(ECPoint p)
		{
			return p.ScaleXNegateY(this.scale);
		}

		// Token: 0x04001D12 RID: 7442
		protected readonly ECFieldElement scale;
	}
}
