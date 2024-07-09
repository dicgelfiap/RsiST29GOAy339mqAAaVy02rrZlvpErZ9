using System;
using Org.BouncyCastle.Crypto;

namespace Org.BouncyCastle.Bcpg.OpenPgp
{
	// Token: 0x0200065C RID: 1628
	public class PgpPrivateKey
	{
		// Token: 0x0600386F RID: 14447 RVA: 0x0012F184 File Offset: 0x0012F184
		public PgpPrivateKey(long keyID, PublicKeyPacket publicKeyPacket, AsymmetricKeyParameter privateKey)
		{
			if (!privateKey.IsPrivate)
			{
				throw new ArgumentException("Expected a private key", "privateKey");
			}
			this.keyID = keyID;
			this.publicKeyPacket = publicKeyPacket;
			this.privateKey = privateKey;
		}

		// Token: 0x170009CE RID: 2510
		// (get) Token: 0x06003870 RID: 14448 RVA: 0x0012F1BC File Offset: 0x0012F1BC
		public long KeyId
		{
			get
			{
				return this.keyID;
			}
		}

		// Token: 0x170009CF RID: 2511
		// (get) Token: 0x06003871 RID: 14449 RVA: 0x0012F1C4 File Offset: 0x0012F1C4
		public PublicKeyPacket PublicKeyPacket
		{
			get
			{
				return this.publicKeyPacket;
			}
		}

		// Token: 0x170009D0 RID: 2512
		// (get) Token: 0x06003872 RID: 14450 RVA: 0x0012F1CC File Offset: 0x0012F1CC
		public AsymmetricKeyParameter Key
		{
			get
			{
				return this.privateKey;
			}
		}

		// Token: 0x04001DD4 RID: 7636
		private readonly long keyID;

		// Token: 0x04001DD5 RID: 7637
		private readonly PublicKeyPacket publicKeyPacket;

		// Token: 0x04001DD6 RID: 7638
		private readonly AsymmetricKeyParameter privateKey;
	}
}
