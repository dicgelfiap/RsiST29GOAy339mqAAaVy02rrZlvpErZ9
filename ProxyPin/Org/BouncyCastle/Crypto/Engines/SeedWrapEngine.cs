using System;

namespace Org.BouncyCastle.Crypto.Engines
{
	// Token: 0x020003A3 RID: 931
	public class SeedWrapEngine : Rfc3394WrapEngine
	{
		// Token: 0x06001D96 RID: 7574 RVA: 0x000A8764 File Offset: 0x000A8764
		public SeedWrapEngine() : base(new SeedEngine())
		{
		}
	}
}
