using System;
using Org.BouncyCastle.Crypto.Agreement;
using Org.BouncyCastle.Crypto.Parameters;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000514 RID: 1300
	public class PskTlsServer : AbstractTlsServer
	{
		// Token: 0x06002794 RID: 10132 RVA: 0x000D60CC File Offset: 0x000D60CC
		public PskTlsServer(TlsPskIdentityManager pskIdentityManager) : this(new DefaultTlsCipherFactory(), pskIdentityManager)
		{
		}

		// Token: 0x06002795 RID: 10133 RVA: 0x000D60DC File Offset: 0x000D60DC
		public PskTlsServer(TlsCipherFactory cipherFactory, TlsPskIdentityManager pskIdentityManager) : base(cipherFactory)
		{
			this.mPskIdentityManager = pskIdentityManager;
		}

		// Token: 0x06002796 RID: 10134 RVA: 0x000D60EC File Offset: 0x000D60EC
		protected virtual TlsEncryptionCredentials GetRsaEncryptionCredentials()
		{
			throw new TlsFatalAlert(80);
		}

		// Token: 0x06002797 RID: 10135 RVA: 0x000D60F8 File Offset: 0x000D60F8
		protected virtual DHParameters GetDHParameters()
		{
			return DHStandardGroups.rfc7919_ffdhe2048;
		}

		// Token: 0x06002798 RID: 10136 RVA: 0x000D6100 File Offset: 0x000D6100
		protected override int[] GetCipherSuites()
		{
			return new int[]
			{
				49207,
				49205,
				178,
				144
			};
		}

		// Token: 0x06002799 RID: 10137 RVA: 0x000D6114 File Offset: 0x000D6114
		public override TlsCredentials GetCredentials()
		{
			int keyExchangeAlgorithm = TlsUtilities.GetKeyExchangeAlgorithm(this.mSelectedCipherSuite);
			int num = keyExchangeAlgorithm;
			switch (num)
			{
			case 13:
			case 14:
				break;
			case 15:
				return this.GetRsaEncryptionCredentials();
			default:
				if (num != 24)
				{
					throw new TlsFatalAlert(80);
				}
				break;
			}
			return null;
		}

		// Token: 0x0600279A RID: 10138 RVA: 0x000D6160 File Offset: 0x000D6160
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

		// Token: 0x0600279B RID: 10139 RVA: 0x000D61AC File Offset: 0x000D61AC
		protected virtual TlsKeyExchange CreatePskKeyExchange(int keyExchange)
		{
			return new TlsPskKeyExchange(keyExchange, this.mSupportedSignatureAlgorithms, null, this.mPskIdentityManager, null, this.GetDHParameters(), this.mNamedCurves, this.mClientECPointFormats, this.mServerECPointFormats);
		}

		// Token: 0x04001A14 RID: 6676
		protected TlsPskIdentityManager mPskIdentityManager;
	}
}
