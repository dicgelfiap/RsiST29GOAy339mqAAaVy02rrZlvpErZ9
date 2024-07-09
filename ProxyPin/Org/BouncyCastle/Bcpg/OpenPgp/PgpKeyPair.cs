using System;
using Org.BouncyCastle.Crypto;

namespace Org.BouncyCastle.Bcpg.OpenPgp
{
	// Token: 0x02000650 RID: 1616
	public class PgpKeyPair
	{
		// Token: 0x0600381F RID: 14367 RVA: 0x0012DEA0 File Offset: 0x0012DEA0
		public PgpKeyPair(PublicKeyAlgorithmTag algorithm, AsymmetricCipherKeyPair keyPair, DateTime time) : this(algorithm, keyPair.Public, keyPair.Private, time)
		{
		}

		// Token: 0x06003820 RID: 14368 RVA: 0x0012DEC8 File Offset: 0x0012DEC8
		public PgpKeyPair(PublicKeyAlgorithmTag algorithm, AsymmetricKeyParameter pubKey, AsymmetricKeyParameter privKey, DateTime time)
		{
			this.pub = new PgpPublicKey(algorithm, pubKey, time);
			this.priv = new PgpPrivateKey(this.pub.KeyId, this.pub.PublicKeyPacket, privKey);
		}

		// Token: 0x06003821 RID: 14369 RVA: 0x0012DF10 File Offset: 0x0012DF10
		public PgpKeyPair(PgpPublicKey pub, PgpPrivateKey priv)
		{
			this.pub = pub;
			this.priv = priv;
		}

		// Token: 0x170009C0 RID: 2496
		// (get) Token: 0x06003822 RID: 14370 RVA: 0x0012DF28 File Offset: 0x0012DF28
		public long KeyId
		{
			get
			{
				return this.pub.KeyId;
			}
		}

		// Token: 0x170009C1 RID: 2497
		// (get) Token: 0x06003823 RID: 14371 RVA: 0x0012DF38 File Offset: 0x0012DF38
		public PgpPublicKey PublicKey
		{
			get
			{
				return this.pub;
			}
		}

		// Token: 0x170009C2 RID: 2498
		// (get) Token: 0x06003824 RID: 14372 RVA: 0x0012DF40 File Offset: 0x0012DF40
		public PgpPrivateKey PrivateKey
		{
			get
			{
				return this.priv;
			}
		}

		// Token: 0x04001DB4 RID: 7604
		private readonly PgpPublicKey pub;

		// Token: 0x04001DB5 RID: 7605
		private readonly PgpPrivateKey priv;
	}
}
