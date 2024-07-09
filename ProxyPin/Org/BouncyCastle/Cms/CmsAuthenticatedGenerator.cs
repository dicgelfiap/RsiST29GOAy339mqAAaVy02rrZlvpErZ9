using System;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x020002D8 RID: 728
	public class CmsAuthenticatedGenerator : CmsEnvelopedGenerator
	{
		// Token: 0x06001615 RID: 5653 RVA: 0x000736F4 File Offset: 0x000736F4
		public CmsAuthenticatedGenerator()
		{
		}

		// Token: 0x06001616 RID: 5654 RVA: 0x000736FC File Offset: 0x000736FC
		public CmsAuthenticatedGenerator(SecureRandom rand) : base(rand)
		{
		}
	}
}
