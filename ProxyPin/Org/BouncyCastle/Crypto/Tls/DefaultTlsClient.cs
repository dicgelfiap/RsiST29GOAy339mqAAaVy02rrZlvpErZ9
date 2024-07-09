using System;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x020004E8 RID: 1256
	public abstract class DefaultTlsClient : AbstractTlsClient
	{
		// Token: 0x06002693 RID: 9875 RVA: 0x000D12A0 File Offset: 0x000D12A0
		public DefaultTlsClient() : this(new DefaultTlsCipherFactory())
		{
		}

		// Token: 0x06002694 RID: 9876 RVA: 0x000D12B0 File Offset: 0x000D12B0
		public DefaultTlsClient(TlsCipherFactory cipherFactory) : this(cipherFactory, new DefaultTlsDHVerifier())
		{
		}

		// Token: 0x06002695 RID: 9877 RVA: 0x000D12C0 File Offset: 0x000D12C0
		public DefaultTlsClient(TlsCipherFactory cipherFactory, TlsDHVerifier dhVerifier) : base(cipherFactory)
		{
			this.mDHVerifier = dhVerifier;
		}

		// Token: 0x06002696 RID: 9878 RVA: 0x000D12D0 File Offset: 0x000D12D0
		public override int[] GetCipherSuites()
		{
			return new int[]
			{
				49195,
				49187,
				49161,
				49199,
				49191,
				49171,
				156,
				60,
				47
			};
		}

		// Token: 0x06002697 RID: 9879 RVA: 0x000D12E4 File Offset: 0x000D12E4
		public override TlsKeyExchange GetKeyExchange()
		{
			int keyExchangeAlgorithm = TlsUtilities.GetKeyExchangeAlgorithm(this.mSelectedCipherSuite);
			switch (keyExchangeAlgorithm)
			{
			case 1:
				return this.CreateRsaKeyExchange();
			case 3:
			case 5:
				return this.CreateDheKeyExchange(keyExchangeAlgorithm);
			case 7:
			case 9:
			case 11:
				return this.CreateDHKeyExchange(keyExchangeAlgorithm);
			case 16:
			case 18:
			case 20:
				return this.CreateECDHKeyExchange(keyExchangeAlgorithm);
			case 17:
			case 19:
				return this.CreateECDheKeyExchange(keyExchangeAlgorithm);
			}
			throw new TlsFatalAlert(80);
		}

		// Token: 0x06002698 RID: 9880 RVA: 0x000D1390 File Offset: 0x000D1390
		protected virtual TlsKeyExchange CreateDHKeyExchange(int keyExchange)
		{
			return new TlsDHKeyExchange(keyExchange, this.mSupportedSignatureAlgorithms, this.mDHVerifier, null);
		}

		// Token: 0x06002699 RID: 9881 RVA: 0x000D13A8 File Offset: 0x000D13A8
		protected virtual TlsKeyExchange CreateDheKeyExchange(int keyExchange)
		{
			return new TlsDheKeyExchange(keyExchange, this.mSupportedSignatureAlgorithms, this.mDHVerifier, null);
		}

		// Token: 0x0600269A RID: 9882 RVA: 0x000D13C0 File Offset: 0x000D13C0
		protected virtual TlsKeyExchange CreateECDHKeyExchange(int keyExchange)
		{
			return new TlsECDHKeyExchange(keyExchange, this.mSupportedSignatureAlgorithms, this.mNamedCurves, this.mClientECPointFormats, this.mServerECPointFormats);
		}

		// Token: 0x0600269B RID: 9883 RVA: 0x000D13E0 File Offset: 0x000D13E0
		protected virtual TlsKeyExchange CreateECDheKeyExchange(int keyExchange)
		{
			return new TlsECDheKeyExchange(keyExchange, this.mSupportedSignatureAlgorithms, this.mNamedCurves, this.mClientECPointFormats, this.mServerECPointFormats);
		}

		// Token: 0x0600269C RID: 9884 RVA: 0x000D1400 File Offset: 0x000D1400
		protected virtual TlsKeyExchange CreateRsaKeyExchange()
		{
			return new TlsRsaKeyExchange(this.mSupportedSignatureAlgorithms);
		}

		// Token: 0x04001912 RID: 6418
		protected TlsDHVerifier mDHVerifier;
	}
}
