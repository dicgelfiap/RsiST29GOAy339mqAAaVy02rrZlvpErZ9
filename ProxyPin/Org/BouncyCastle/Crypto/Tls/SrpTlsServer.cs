using System;
using System.Collections;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000523 RID: 1315
	public class SrpTlsServer : AbstractTlsServer
	{
		// Token: 0x060027FF RID: 10239 RVA: 0x000D7268 File Offset: 0x000D7268
		public SrpTlsServer(TlsSrpIdentityManager srpIdentityManager) : this(new DefaultTlsCipherFactory(), srpIdentityManager)
		{
		}

		// Token: 0x06002800 RID: 10240 RVA: 0x000D7278 File Offset: 0x000D7278
		public SrpTlsServer(TlsCipherFactory cipherFactory, TlsSrpIdentityManager srpIdentityManager) : base(cipherFactory)
		{
			this.mSrpIdentityManager = srpIdentityManager;
		}

		// Token: 0x06002801 RID: 10241 RVA: 0x000D7298 File Offset: 0x000D7298
		protected virtual TlsSignerCredentials GetDsaSignerCredentials()
		{
			throw new TlsFatalAlert(80);
		}

		// Token: 0x06002802 RID: 10242 RVA: 0x000D72A4 File Offset: 0x000D72A4
		protected virtual TlsSignerCredentials GetRsaSignerCredentials()
		{
			throw new TlsFatalAlert(80);
		}

		// Token: 0x06002803 RID: 10243 RVA: 0x000D72B0 File Offset: 0x000D72B0
		protected override int[] GetCipherSuites()
		{
			return new int[]
			{
				49186,
				49183,
				49185,
				49182,
				49184,
				49181
			};
		}

		// Token: 0x06002804 RID: 10244 RVA: 0x000D72C4 File Offset: 0x000D72C4
		public override void ProcessClientExtensions(IDictionary clientExtensions)
		{
			base.ProcessClientExtensions(clientExtensions);
			this.mSrpIdentity = TlsSrpUtilities.GetSrpExtension(clientExtensions);
		}

		// Token: 0x06002805 RID: 10245 RVA: 0x000D72DC File Offset: 0x000D72DC
		public override int GetSelectedCipherSuite()
		{
			int selectedCipherSuite = base.GetSelectedCipherSuite();
			if (TlsSrpUtilities.IsSrpCipherSuite(selectedCipherSuite))
			{
				if (this.mSrpIdentity != null)
				{
					this.mLoginParameters = this.mSrpIdentityManager.GetLoginParameters(this.mSrpIdentity);
				}
				if (this.mLoginParameters == null)
				{
					throw new TlsFatalAlert(115);
				}
			}
			return selectedCipherSuite;
		}

		// Token: 0x06002806 RID: 10246 RVA: 0x000D7338 File Offset: 0x000D7338
		public override TlsCredentials GetCredentials()
		{
			switch (TlsUtilities.GetKeyExchangeAlgorithm(this.mSelectedCipherSuite))
			{
			case 21:
				return null;
			case 22:
				return this.GetDsaSignerCredentials();
			case 23:
				return this.GetRsaSignerCredentials();
			default:
				throw new TlsFatalAlert(80);
			}
		}

		// Token: 0x06002807 RID: 10247 RVA: 0x000D7388 File Offset: 0x000D7388
		public override TlsKeyExchange GetKeyExchange()
		{
			int keyExchangeAlgorithm = TlsUtilities.GetKeyExchangeAlgorithm(this.mSelectedCipherSuite);
			switch (keyExchangeAlgorithm)
			{
			case 21:
			case 22:
			case 23:
				return this.CreateSrpKeyExchange(keyExchangeAlgorithm);
			default:
				throw new TlsFatalAlert(80);
			}
		}

		// Token: 0x06002808 RID: 10248 RVA: 0x000D73D0 File Offset: 0x000D73D0
		protected virtual TlsKeyExchange CreateSrpKeyExchange(int keyExchange)
		{
			return new TlsSrpKeyExchange(keyExchange, this.mSupportedSignatureAlgorithms, this.mSrpIdentity, this.mLoginParameters);
		}

		// Token: 0x04001A5A RID: 6746
		protected TlsSrpIdentityManager mSrpIdentityManager;

		// Token: 0x04001A5B RID: 6747
		protected byte[] mSrpIdentity = null;

		// Token: 0x04001A5C RID: 6748
		protected TlsSrpLoginParameters mLoginParameters = null;
	}
}
