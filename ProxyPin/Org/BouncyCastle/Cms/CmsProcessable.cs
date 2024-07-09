using System;
using System.IO;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x020002EB RID: 747
	public interface CmsProcessable
	{
		// Token: 0x0600167B RID: 5755
		void Write(Stream outStream);

		// Token: 0x0600167C RID: 5756
		[Obsolete]
		object GetContent();
	}
}
