using System;

namespace Org.BouncyCastle.Crypto
{
	// Token: 0x0200055B RID: 1371
	public class AsymmetricCipherKeyPair
	{
		// Token: 0x06002A9F RID: 10911 RVA: 0x000E463C File Offset: 0x000E463C
		public AsymmetricCipherKeyPair(AsymmetricKeyParameter publicParameter, AsymmetricKeyParameter privateParameter)
		{
			if (publicParameter.IsPrivate)
			{
				throw new ArgumentException("Expected a public key", "publicParameter");
			}
			if (!privateParameter.IsPrivate)
			{
				throw new ArgumentException("Expected a private key", "privateParameter");
			}
			this.publicParameter = publicParameter;
			this.privateParameter = privateParameter;
		}

		// Token: 0x170007AF RID: 1967
		// (get) Token: 0x06002AA0 RID: 10912 RVA: 0x000E4698 File Offset: 0x000E4698
		public AsymmetricKeyParameter Public
		{
			get
			{
				return this.publicParameter;
			}
		}

		// Token: 0x170007B0 RID: 1968
		// (get) Token: 0x06002AA1 RID: 10913 RVA: 0x000E46A0 File Offset: 0x000E46A0
		public AsymmetricKeyParameter Private
		{
			get
			{
				return this.privateParameter;
			}
		}

		// Token: 0x04001B32 RID: 6962
		private readonly AsymmetricKeyParameter publicParameter;

		// Token: 0x04001B33 RID: 6963
		private readonly AsymmetricKeyParameter privateParameter;
	}
}
