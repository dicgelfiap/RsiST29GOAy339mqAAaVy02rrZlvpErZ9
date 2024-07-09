using System;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x0200044C RID: 1100
	public class Ed448KeyGenerationParameters : KeyGenerationParameters
	{
		// Token: 0x06002297 RID: 8855 RVA: 0x000C51C4 File Offset: 0x000C51C4
		public Ed448KeyGenerationParameters(SecureRandom random) : base(random, 448)
		{
		}
	}
}
