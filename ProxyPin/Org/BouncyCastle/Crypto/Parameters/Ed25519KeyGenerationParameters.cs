using System;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x02000449 RID: 1097
	public class Ed25519KeyGenerationParameters : KeyGenerationParameters
	{
		// Token: 0x06002288 RID: 8840 RVA: 0x000C4EF4 File Offset: 0x000C4EF4
		public Ed25519KeyGenerationParameters(SecureRandom random) : base(random, 256)
		{
		}
	}
}
