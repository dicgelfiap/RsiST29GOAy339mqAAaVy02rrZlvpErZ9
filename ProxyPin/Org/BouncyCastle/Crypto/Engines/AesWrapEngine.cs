using System;

namespace Org.BouncyCastle.Crypto.Engines
{
	// Token: 0x0200037B RID: 891
	public class AesWrapEngine : Rfc3394WrapEngine
	{
		// Token: 0x06001BC1 RID: 7105 RVA: 0x000992F4 File Offset: 0x000992F4
		public AesWrapEngine() : base(new AesEngine())
		{
		}
	}
}
