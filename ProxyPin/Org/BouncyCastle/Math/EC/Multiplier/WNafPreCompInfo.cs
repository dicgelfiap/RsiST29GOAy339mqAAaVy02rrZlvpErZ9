using System;

namespace Org.BouncyCastle.Math.EC.Multiplier
{
	// Token: 0x020005FF RID: 1535
	public class WNafPreCompInfo : PreCompInfo
	{
		// Token: 0x0600333F RID: 13119 RVA: 0x001090CC File Offset: 0x001090CC
		internal int DecrementPromotionCountdown()
		{
			int num = this.m_promotionCountdown;
			if (num > 0)
			{
				num = (this.m_promotionCountdown = num - 1);
			}
			return num;
		}

		// Token: 0x17000962 RID: 2402
		// (get) Token: 0x06003340 RID: 13120 RVA: 0x001090FC File Offset: 0x001090FC
		// (set) Token: 0x06003341 RID: 13121 RVA: 0x00109108 File Offset: 0x00109108
		internal int PromotionCountdown
		{
			get
			{
				return this.m_promotionCountdown;
			}
			set
			{
				this.m_promotionCountdown = value;
			}
		}

		// Token: 0x17000963 RID: 2403
		// (get) Token: 0x06003342 RID: 13122 RVA: 0x00109114 File Offset: 0x00109114
		public virtual bool IsPromoted
		{
			get
			{
				return this.m_promotionCountdown <= 0;
			}
		}

		// Token: 0x17000964 RID: 2404
		// (get) Token: 0x06003343 RID: 13123 RVA: 0x00109124 File Offset: 0x00109124
		// (set) Token: 0x06003344 RID: 13124 RVA: 0x0010912C File Offset: 0x0010912C
		public virtual int ConfWidth
		{
			get
			{
				return this.m_confWidth;
			}
			set
			{
				this.m_confWidth = value;
			}
		}

		// Token: 0x17000965 RID: 2405
		// (get) Token: 0x06003345 RID: 13125 RVA: 0x00109138 File Offset: 0x00109138
		// (set) Token: 0x06003346 RID: 13126 RVA: 0x00109140 File Offset: 0x00109140
		public virtual ECPoint[] PreComp
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

		// Token: 0x17000966 RID: 2406
		// (get) Token: 0x06003347 RID: 13127 RVA: 0x0010914C File Offset: 0x0010914C
		// (set) Token: 0x06003348 RID: 13128 RVA: 0x00109154 File Offset: 0x00109154
		public virtual ECPoint[] PreCompNeg
		{
			get
			{
				return this.m_preCompNeg;
			}
			set
			{
				this.m_preCompNeg = value;
			}
		}

		// Token: 0x17000967 RID: 2407
		// (get) Token: 0x06003349 RID: 13129 RVA: 0x00109160 File Offset: 0x00109160
		// (set) Token: 0x0600334A RID: 13130 RVA: 0x00109168 File Offset: 0x00109168
		public virtual ECPoint Twice
		{
			get
			{
				return this.m_twice;
			}
			set
			{
				this.m_twice = value;
			}
		}

		// Token: 0x17000968 RID: 2408
		// (get) Token: 0x0600334B RID: 13131 RVA: 0x00109174 File Offset: 0x00109174
		// (set) Token: 0x0600334C RID: 13132 RVA: 0x0010917C File Offset: 0x0010917C
		public virtual int Width
		{
			get
			{
				return this.m_width;
			}
			set
			{
				this.m_width = value;
			}
		}

		// Token: 0x04001C93 RID: 7315
		internal volatile int m_promotionCountdown = 4;

		// Token: 0x04001C94 RID: 7316
		protected int m_confWidth = -1;

		// Token: 0x04001C95 RID: 7317
		protected ECPoint[] m_preComp = null;

		// Token: 0x04001C96 RID: 7318
		protected ECPoint[] m_preCompNeg = null;

		// Token: 0x04001C97 RID: 7319
		protected ECPoint m_twice = null;

		// Token: 0x04001C98 RID: 7320
		protected int m_width = -1;
	}
}
