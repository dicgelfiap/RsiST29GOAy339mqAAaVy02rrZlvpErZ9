using System;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000513 RID: 1299
	public class PskTlsClient : AbstractTlsClient
	{
		// Token: 0x0600278D RID: 10125 RVA: 0x000D5FD0 File Offset: 0x000D5FD0
		public PskTlsClient(TlsPskIdentity pskIdentity) : this(new DefaultTlsCipherFactory(), pskIdentity)
		{
		}

		// Token: 0x0600278E RID: 10126 RVA: 0x000D5FE0 File Offset: 0x000D5FE0
		public PskTlsClient(TlsCipherFactory cipherFactory, TlsPskIdentity pskIdentity) : this(cipherFactory, new DefaultTlsDHVerifier(), pskIdentity)
		{
		}

		// Token: 0x0600278F RID: 10127 RVA: 0x000D5FF0 File Offset: 0x000D5FF0
		public PskTlsClient(TlsCipherFactory cipherFactory, TlsDHVerifier dhVerifier, TlsPskIdentity pskIdentity) : base(cipherFactory)
		{
			this.mDHVerifier = dhVerifier;
			this.mPskIdentity = pskIdentity;
		}

		// Token: 0x06002790 RID: 10128 RVA: 0x000D6008 File Offset: 0x000D6008
		public override int[] GetCipherSuites()
		{
			return new int[]
			{
				49207,
				49205
			};
		}

		// Token: 0x06002791 RID: 10129 RVA: 0x000D6034 File Offset: 0x000D6034
		public override TlsKeyExchange GetKeyExchange()
		{
			int keyExchangeAlgorithm = TlsUtilities.GetKeyExchangeAlgorithm(this.mSelectedCipherSuite);
			int num = keyExchangeAlgorithm;
			switch (num)
			{
			case 13:
			case 14:
			case 15:
				break;
			default:
				if (num != 24)
				{
					throw new TlsFatalAlert(80);
				}
				break;
			}
			return this.CreatePskKeyExchange(keyExchangeAlgorithm);
		}

		// Token: 0x06002792 RID: 10130 RVA: 0x000D6080 File Offset: 0x000D6080
		public override TlsAuthentication GetAuthentication()
		{
			throw new TlsFatalAlert(80);
		}

		// Token: 0x06002793 RID: 10131 RVA: 0x000D608C File Offset: 0x000D608C
		protected virtual TlsKeyExchange CreatePskKeyExchange(int keyExchange)
		{
			return new TlsPskKeyExchange(keyExchange, this.mSupportedSignatureAlgorithms, this.mPskIdentity, null, this.mDHVerifier, null, this.mNamedCurves, this.mClientECPointFormats, this.mServerECPointFormats);
		}

		// Token: 0x04001A12 RID: 6674
		protected TlsDHVerifier mDHVerifier;

		// Token: 0x04001A13 RID: 6675
		protected TlsPskIdentity mPskIdentity;
	}
}
