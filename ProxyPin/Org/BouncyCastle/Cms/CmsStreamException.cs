using System;
using System.IO;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x020002F9 RID: 761
	[Serializable]
	public class CmsStreamException : IOException
	{
		// Token: 0x06001716 RID: 5910 RVA: 0x00079130 File Offset: 0x00079130
		public CmsStreamException()
		{
		}

		// Token: 0x06001717 RID: 5911 RVA: 0x00079138 File Offset: 0x00079138
		public CmsStreamException(string name) : base(name)
		{
		}

		// Token: 0x06001718 RID: 5912 RVA: 0x00079144 File Offset: 0x00079144
		public CmsStreamException(string name, Exception e) : base(name, e)
		{
		}
	}
}
