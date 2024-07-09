using System;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x020002D2 RID: 722
	[Serializable]
	public class CmsException : Exception
	{
		// Token: 0x060015F4 RID: 5620 RVA: 0x0007313C File Offset: 0x0007313C
		public CmsException()
		{
		}

		// Token: 0x060015F5 RID: 5621 RVA: 0x00073144 File Offset: 0x00073144
		public CmsException(string msg) : base(msg)
		{
		}

		// Token: 0x060015F6 RID: 5622 RVA: 0x00073150 File Offset: 0x00073150
		public CmsException(string msg, Exception e) : base(msg, e)
		{
		}
	}
}
