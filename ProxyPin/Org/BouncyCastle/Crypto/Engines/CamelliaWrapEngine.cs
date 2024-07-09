using System;

namespace Org.BouncyCastle.Crypto.Engines
{
	// Token: 0x0200037F RID: 895
	public class CamelliaWrapEngine : Rfc3394WrapEngine
	{
		// Token: 0x06001BFD RID: 7165 RVA: 0x0009BC84 File Offset: 0x0009BC84
		public CamelliaWrapEngine() : base(new CamelliaEngine())
		{
		}
	}
}
