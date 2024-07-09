using System;

namespace Org.BouncyCastle.Math.EC.Multiplier
{
	// Token: 0x02000602 RID: 1538
	public class WTauNafPreCompInfo : PreCompInfo
	{
		// Token: 0x17000969 RID: 2409
		// (get) Token: 0x06003368 RID: 13160 RVA: 0x00109B7C File Offset: 0x00109B7C
		// (set) Token: 0x06003369 RID: 13161 RVA: 0x00109B84 File Offset: 0x00109B84
		public virtual AbstractF2mPoint[] PreComp
		{
			get
			{
				return this.m_preComp;
			}
			set
			{
				this.m_preComp = value;
			}
		}

		// Token: 0x04001C9E RID: 7326
		protected AbstractF2mPoint[] m_preComp;
	}
}
