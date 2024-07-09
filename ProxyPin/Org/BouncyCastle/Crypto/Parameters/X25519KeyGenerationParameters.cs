using System;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x02000478 RID: 1144
	public class X25519KeyGenerationParameters : KeyGenerationParameters
	{
		// Token: 0x06002371 RID: 9073 RVA: 0x000C6D18 File Offset: 0x000C6D18
		public X25519KeyGenerationParameters(SecureRandom random) : base(random, 255)
		{
		}
	}
}
