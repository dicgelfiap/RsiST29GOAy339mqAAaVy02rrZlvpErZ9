using System;
using System.Collections;
using System.IO;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x020004C2 RID: 1218
	public abstract class AbstractTlsKeyExchange : TlsKeyExchange
	{
		// Token: 0x06002590 RID: 9616 RVA: 0x000CEEC0 File Offset: 0x000CEEC0
		protected AbstractTlsKeyExchange(int keyExchange, IList supportedSignatureAlgorithms)
		{
			this.mKeyExchange = keyExchange;
			this.mSupportedSignatureAlgorithms = supportedSignatureAlgorithms;
		}

		// Token: 0x06002591 RID: 9617 RVA: 0x000CEED8 File Offset: 0x000CEED8
		protected virtual DigitallySigned ParseSignature(Stream input)
		{
			DigitallySigned digitallySigned = DigitallySigned.Parse(this.mContext, input);
			SignatureAndHashAlgorithm algorithm = digitallySigned.Algorithm;
			if (algorithm != null)
			{
				TlsUtilities.VerifySupportedSignatureAlgorithm(this.mSupportedSignatureAlgorithms, algorithm);
			}
			return digitallySigned;
		}

		// Token: 0x06002592 RID: 9618 RVA: 0x000CEF10 File Offset: 0x000CEF10
		public virtual void Init(TlsContext context)
		{
			this.mContext = context;
			ProtocolVersion clientVersion = context.ClientVersion;
			if (TlsUtilities.IsSignatureAlgorithmsExtensionAllowed(clientVersion))
			{
				if (this.mSupportedSignatureAlgorithms == null)
				{
					switch (this.mKeyExchange)
					{
					case 1:
					case 5:
					case 9:
					case 15:
					case 18:
					case 19:
					case 23:
						this.mSupportedSignatureAlgorithms = TlsUtilities.GetDefaultRsaSignatureAlgorithms();
						return;
					case 3:
					case 7:
					case 22:
						this.mSupportedSignatureAlgorithms = TlsUtilities.GetDefaultDssSignatureAlgorithms();
						return;
					case 13:
					case 14:
					case 21:
					case 24:
						return;
					case 16:
					case 17:
						this.mSupportedSignatureAlgorithms = TlsUtilities.GetDefaultECDsaSignatureAlgorithms();
						return;
					}
					throw new InvalidOperationException("unsupported key exchange algorithm");
				}
			}
			else if (this.mSupportedSignatureAlgorithms != null)
			{
				throw new InvalidOperationException("supported_signature_algorithms not allowed for " + clientVersion);
			}
		}

		// Token: 0x06002593 RID: 9619
		public abstract void SkipServerCredentials();

		// Token: 0x06002594 RID: 9620 RVA: 0x000CF004 File Offset: 0x000CF004
		public virtual void ProcessServerCertificate(Certificate serverCertificate)
		{
			if (this.mSupportedSignatureAlgorithms == null)
			{
			}
		}

		// Token: 0x06002595 RID: 9621 RVA: 0x000CF014 File Offset: 0x000CF014
		public virtual void ProcessServerCredentials(TlsCredentials serverCredentials)
		{
			this.ProcessServerCertificate(serverCredentials.Certificate);
		}

		// Token: 0x17000723 RID: 1827
		// (get) Token: 0x06002596 RID: 9622 RVA: 0x000CF024 File Offset: 0x000CF024
		public virtual bool RequiresServerKeyExchange
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06002597 RID: 9623 RVA: 0x000CF028 File Offset: 0x000CF028
		public virtual byte[] GenerateServerKeyExchange()
		{
			if (this.RequiresServerKeyExchange)
			{
				throw new TlsFatalAlert(80);
			}
			return null;
		}

		// Token: 0x06002598 RID: 9624 RVA: 0x000CF040 File Offset: 0x000CF040
		public virtual void SkipServerKeyExchange()
		{
			if (this.RequiresServerKeyExchange)
			{
				throw new TlsFatalAlert(10);
			}
		}

		// Token: 0x06002599 RID: 9625 RVA: 0x000CF058 File Offset: 0x000CF058
		public virtual void ProcessServerKeyExchange(Stream input)
		{
			if (!this.RequiresServerKeyExchange)
			{
				throw new TlsFatalAlert(10);
			}
		}

		// Token: 0x0600259A RID: 9626
		public abstract void ValidateCertificateRequest(CertificateRequest certificateRequest);

		// Token: 0x0600259B RID: 9627 RVA: 0x000CF070 File Offset: 0x000CF070
		public virtual void SkipClientCredentials()
		{
		}

		// Token: 0x0600259C RID: 9628
		public abstract void ProcessClientCredentials(TlsCredentials clientCredentials);

		// Token: 0x0600259D RID: 9629 RVA: 0x000CF074 File Offset: 0x000CF074
		public virtual void ProcessClientCertificate(Certificate clientCertificate)
		{
		}

		// Token: 0x0600259E RID: 9630
		public abstract void GenerateClientKeyExchange(Stream output);

		// Token: 0x0600259F RID: 9631 RVA: 0x000CF078 File Offset: 0x000CF078
		public virtual void ProcessClientKeyExchange(Stream input)
		{
			throw new TlsFatalAlert(80);
		}

		// Token: 0x060025A0 RID: 9632
		public abstract byte[] GeneratePremasterSecret();

		// Token: 0x04001791 RID: 6033
		protected readonly int mKeyExchange;

		// Token: 0x04001792 RID: 6034
		protected IList mSupportedSignatureAlgorithms;

		// Token: 0x04001793 RID: 6035
		protected TlsContext mContext;
	}
}
