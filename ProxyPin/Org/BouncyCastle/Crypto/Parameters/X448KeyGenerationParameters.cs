using System;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x0200047B RID: 1147
	public class X448KeyGenerationParameters : KeyGenerationParameters
	{
		// Token: 0x0600237F RID: 9087 RVA: 0x000C6F00 File Offset: 0x000C6F00
		public X448KeyGenerationParameters(SecureRandom random) : base(random, 448)
		{
		}
	}
}
