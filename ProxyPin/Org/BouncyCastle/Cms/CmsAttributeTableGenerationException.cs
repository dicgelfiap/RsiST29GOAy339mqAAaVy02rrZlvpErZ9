using System;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x020002D3 RID: 723
	[Serializable]
	public class CmsAttributeTableGenerationException : CmsException
	{
		// Token: 0x060015F7 RID: 5623 RVA: 0x0007315C File Offset: 0x0007315C
		public CmsAttributeTableGenerationException()
		{
		}

		// Token: 0x060015F8 RID: 5624 RVA: 0x00073164 File Offset: 0x00073164
		public CmsAttributeTableGenerationException(string name) : base(name)
		{
		}

		// Token: 0x060015F9 RID: 5625 RVA: 0x00073170 File Offset: 0x00073170
		public CmsAttributeTableGenerationException(string name, Exception e) : base(name, e)
		{
		}
	}
}
