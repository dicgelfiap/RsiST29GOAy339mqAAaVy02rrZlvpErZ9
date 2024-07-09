using System;
using Org.BouncyCastle.Math.EC.Multiplier;

namespace Org.BouncyCastle.Math.EC.Endo
{
	// Token: 0x020005E9 RID: 1513
	public class EndoPreCompInfo : PreCompInfo
	{
		// Token: 0x17000947 RID: 2375
		// (get) Token: 0x060032E8 RID: 13032 RVA: 0x00108404 File Offset: 0x00108404
		// (set) Token: 0x060032E9 RID: 13033 RVA: 0x0010840C File Offset: 0x0010840C
		public virtual ECEndomorphism Endomorphism
		{
			get
			{
				return this.m_endomorphism;
			}
			set
			{
				this.m_endomorphism = value;
			}
		}

		// Token: 0x17000948 RID: 2376
		// (get) Token: 0x060032EA RID: 13034 RVA: 0x00108418 File Offset: 0x00108418
		// (set) Token: 0x060032EB RID: 13035 RVA: 0x00108420 File Offset: 0x00108420
		public virtual ECPoint MappedPoint
		{
			get
			{
				return this.m_mappedPoint;
			}
			set
			{
				this.m_mappedPoint = value;
			}
		}

		// Token: 0x04001C73 RID: 7283
		protected ECEndomorphism m_endomorphism;

		// Token: 0x04001C74 RID: 7284
		protected ECPoint m_mappedPoint;
	}
}
