using System;
using Org.BouncyCastle.Crypto.Agreement;
using Org.BouncyCastle.Crypto.Parameters;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x020004EC RID: 1260
	public abstract class DefaultTlsServer : AbstractTlsServer
	{
		// Token: 0x060026AC RID: 9900 RVA: 0x000D16C4 File Offset: 0x000D16C4
		public DefaultTlsServer()
		{
		}

		// Token: 0x060026AD RID: 9901 RVA: 0x000D16CC File Offset: 0x000D16CC
		public DefaultTlsServer(TlsCipherFactory cipherFactory) : base(cipherFactory)
		{
		}

		// Token: 0x060026AE RID: 9902 RVA: 0x000D16D8 File Offset: 0x000D16D8
		protected virtual TlsSignerCredentials GetDsaSignerCredentials()
		{
			throw new TlsFatalAlert(80);
		}

		// Token: 0x060026AF RID: 9903 RVA: 0x000D16E4 File Offset: 0x000D16E4
		protected virtual TlsSignerCredentials GetECDsaSignerCredentials()
		{
			throw new TlsFatalAlert(80);
		}

		// Token: 0x060026B0 RID: 9904 RVA: 0x000D16F0 File Offset: 0x000D16F0
		protected virtual TlsEncryptionCredentials GetRsaEncryptionCredentials()
		{
			throw new TlsFatalAlert(80);
		}

		// Token: 0x060026B1 RID: 9905 RVA: 0x000D16FC File Offset: 0x000D16FC
		protected virtual TlsSignerCredentials GetRsaSignerCredentials()
		{
			throw new TlsFatalAlert(80);
		}

		// Token: 0x060026B2 RID: 9906 RVA: 0x000D1708 File Offset: 0x000D1708
		protected virtual DHParameters GetDHParameters()
		{
			return DHStandardGroups.rfc7919_ffdhe2048;
		}

		// Token: 0x060026B3 RID: 9907 RVA: 0x000D1710 File Offset: 0x000D1710
		protected override int[] GetCipherSuites()
		{
			return new int[]
			{
				49200,
				49199,
				49192,
				49191,
				49172,
				49171,
				159,
				158,
				107,
				103,
				57,
				51,
				157,
				156,
				61,
				60,
				53,
				47
			};
		}

		// Token: 0x060026B4 RID: 9908 RVA: 0x000D1724 File Offset: 0x000D1724
		public override TlsCredentials GetCredentials()
		{
			int keyExchangeAlgorithm = TlsUtilities.GetKeyExchangeAlgorithm(this.mSelectedCipherSuite);
			int num = keyExchangeAlgorithm;
			switch (num)
			{
			case 1:
				return this.GetRsaEncryptionCredentials();
			case 2:
			case 4:
				goto IL_6E;
			case 3:
				return this.GetDsaSignerCredentials();
			case 5:
				break;
			default:
				if (num != 11)
				{
					switch (num)
					{
					case 17:
						return this.GetECDsaSignerCredentials();
					case 18:
						goto IL_6E;
					case 19:
						goto IL_60;
					case 20:
						break;
					default:
						goto IL_6E;
					}
				}
				return null;
			}
			IL_60:
			return this.GetRsaSignerCredentials();
			IL_6E:
			throw new TlsFatalAlert(80);
		}

		// Token: 0x060026B5 RID: 9909 RVA: 0x000D17AC File Offset: 0x000D17AC
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

		// Token: 0x060026B6 RID: 9910 RVA: 0x000D1858 File Offset: 0x000D1858
		protected virtual TlsKeyExchange CreateDHKeyExchange(int keyExchange)
		{
			return new TlsDHKeyExchange(keyExchange, this.mSupportedSignatureAlgorithms, null, this.GetDHParameters());
		}

		// Token: 0x060026B7 RID: 9911 RVA: 0x000D1870 File Offset: 0x000D1870
		protected virtual TlsKeyExchange CreateDheKeyExchange(int keyExchange)
		{
			return new TlsDheKeyExchange(keyExchange, this.mSupportedSignatureAlgorithms, null, this.GetDHParameters());
		}

		// Token: 0x060026B8 RID: 9912 RVA: 0x000D1888 File Offset: 0x000D1888
		protected virtual TlsKeyExchange CreateECDHKeyExchange(int keyExchange)
		{
			return new TlsECDHKeyExchange(keyExchange, this.mSupportedSignatureAlgorithms, this.mNamedCurves, this.mClientECPointFormats, this.mServerECPointFormats);
		}

		// Token: 0x060026B9 RID: 9913 RVA: 0x000D18A8 File Offset: 0x000D18A8
		protected virtual TlsKeyExchange CreateECDheKeyExchange(int keyExchange)
		{
			return new TlsECDheKeyExchange(keyExchange, this.mSupportedSignatureAlgorithms, this.mNamedCurves, this.mClientECPointFormats, this.mServerECPointFormats);
		}

		// Token: 0x060026BA RID: 9914 RVA: 0x000D18C8 File Offset: 0x000D18C8
		protected virtual TlsKeyExchange CreateRsaKeyExchange()
		{
			return new TlsRsaKeyExchange(this.mSupportedSignatureAlgorithms);
		}
	}
}
