using System;

namespace Org.BouncyCastle.Math.EC.Multiplier
{
	// Token: 0x020005F5 RID: 1525
	public class FixedPointPreCompInfo : PreCompInfo
	{
		// Token: 0x1700095F RID: 2399
		// (get) Token: 0x0600331B RID: 13083 RVA: 0x00108A38 File Offset: 0x00108A38
		// (set) Token: 0x0600331C RID: 13084 RVA: 0x00108A40 File Offset: 0x00108A40
		public virtual ECLookupTable LookupTable
		{
			get
			{
				return this.m_lookupTable;
			}
			set
			{
				this.m_lookupTable = value;
			}
		}

		// Token: 0x17000960 RID: 2400
		// (get) Token: 0x0600331D RID: 13085 RVA: 0x00108A4C File Offset: 0x00108A4C
		// (set) Token: 0x0600331E RID: 13086 RVA: 0x00108A54 File Offset: 0x00108A54
		public virtual ECPoint Offset
		{
			get
			{
				return this.m_offset;
			}
			set
			{
				this.m_offset = value;
			}
		}

		// Token: 0x17000961 RID: 2401
		// (get) Token: 0x0600331F RID: 13087 RVA: 0x00108A60 File Offset: 0x00108A60
		// (set) Token: 0x06003320 RID: 13088 RVA: 0x00108A68 File Offset: 0x00108A68
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

		// Token: 0x04001C87 RID: 7303
		protected ECPoint m_offset = null;

		// Token: 0x04001C88 RID: 7304
		protected ECLookupTable m_lookupTable = null;

		// Token: 0x04001C89 RID: 7305
		protected int m_width = -1;
	}
}
