using System;
using System.Collections;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x020004BC RID: 1212
	public abstract class AbstractTlsClient : AbstractTlsPeer, TlsClient, TlsPeer
	{
		// Token: 0x0600254B RID: 9547 RVA: 0x000CE9FC File Offset: 0x000CE9FC
		public AbstractTlsClient() : this(new DefaultTlsCipherFactory())
		{
		}

		// Token: 0x0600254C RID: 9548 RVA: 0x000CEA0C File Offset: 0x000CEA0C
		public AbstractTlsClient(TlsCipherFactory cipherFactory)
		{
			this.mCipherFactory = cipherFactory;
		}

		// Token: 0x0600254D RID: 9549 RVA: 0x000CEA1C File Offset: 0x000CEA1C
		protected virtual bool AllowUnexpectedServerExtension(int extensionType, byte[] extensionData)
		{
			switch (extensionType)
			{
			case 10:
				TlsEccUtilities.ReadSupportedEllipticCurvesExtension(extensionData);
				return true;
			case 11:
				TlsEccUtilities.ReadSupportedPointFormatsExtension(extensionData);
				return true;
			default:
				return false;
			}
		}

		// Token: 0x0600254E RID: 9550 RVA: 0x000CEA58 File Offset: 0x000CEA58
		protected virtual void CheckForUnexpectedServerExtension(IDictionary serverExtensions, int extensionType)
		{
			byte[] extensionData = TlsUtilities.GetExtensionData(serverExtensions, extensionType);
			if (extensionData != null && !this.AllowUnexpectedServerExtension(extensionType, extensionData))
			{
				throw new TlsFatalAlert(47);
			}
		}

		// Token: 0x0600254F RID: 9551 RVA: 0x000CEA8C File Offset: 0x000CEA8C
		public virtual void Init(TlsClientContext context)
		{
			this.mContext = context;
		}

		// Token: 0x06002550 RID: 9552 RVA: 0x000CEA98 File Offset: 0x000CEA98
		public virtual TlsSession GetSessionToResume()
		{
			return null;
		}

		// Token: 0x1700070E RID: 1806
		// (get) Token: 0x06002551 RID: 9553 RVA: 0x000CEA9C File Offset: 0x000CEA9C
		public virtual ProtocolVersion ClientHelloRecordLayerVersion
		{
			get
			{
				return this.ClientVersion;
			}
		}

		// Token: 0x1700070F RID: 1807
		// (get) Token: 0x06002552 RID: 9554 RVA: 0x000CEAA4 File Offset: 0x000CEAA4
		public virtual ProtocolVersion ClientVersion
		{
			get
			{
				return ProtocolVersion.TLSv12;
			}
		}

		// Token: 0x17000710 RID: 1808
		// (get) Token: 0x06002553 RID: 9555 RVA: 0x000CEAAC File Offset: 0x000CEAAC
		public virtual bool IsFallback
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06002554 RID: 9556 RVA: 0x000CEAB0 File Offset: 0x000CEAB0
		public virtual IDictionary GetClientExtensions()
		{
			IDictionary dictionary = null;
			ProtocolVersion clientVersion = this.mContext.ClientVersion;
			if (TlsUtilities.IsSignatureAlgorithmsExtensionAllowed(clientVersion))
			{
				this.mSupportedSignatureAlgorithms = TlsUtilities.GetDefaultSupportedSignatureAlgorithms();
				dictionary = TlsExtensionsUtilities.EnsureExtensionsInitialised(dictionary);
				TlsUtilities.AddSignatureAlgorithmsExtension(dictionary, this.mSupportedSignatureAlgorithms);
			}
			if (TlsEccUtilities.ContainsEccCipherSuites(this.GetCipherSuites()))
			{
				this.mNamedCurves = new int[]
				{
					23,
					24
				};
				this.mClientECPointFormats = new byte[]
				{
					0,
					1,
					2
				};
				dictionary = TlsExtensionsUtilities.EnsureExtensionsInitialised(dictionary);
				TlsEccUtilities.AddSupportedEllipticCurvesExtension(dictionary, this.mNamedCurves);
				TlsEccUtilities.AddSupportedPointFormatsExtension(dictionary, this.mClientECPointFormats);
			}
			return dictionary;
		}

		// Token: 0x17000711 RID: 1809
		// (get) Token: 0x06002555 RID: 9557 RVA: 0x000CEB58 File Offset: 0x000CEB58
		public virtual ProtocolVersion MinimumVersion
		{
			get
			{
				return ProtocolVersion.TLSv10;
			}
		}

		// Token: 0x06002556 RID: 9558 RVA: 0x000CEB60 File Offset: 0x000CEB60
		public virtual void NotifyServerVersion(ProtocolVersion serverVersion)
		{
			if (!this.MinimumVersion.IsEqualOrEarlierVersionOf(serverVersion))
			{
				throw new TlsFatalAlert(70);
			}
		}

		// Token: 0x06002557 RID: 9559
		public abstract int[] GetCipherSuites();

		// Token: 0x06002558 RID: 9560 RVA: 0x000CEB7C File Offset: 0x000CEB7C
		public virtual byte[] GetCompressionMethods()
		{
			return new byte[1];
		}

		// Token: 0x06002559 RID: 9561 RVA: 0x000CEB98 File Offset: 0x000CEB98
		public virtual void NotifySessionID(byte[] sessionID)
		{
		}

		// Token: 0x0600255A RID: 9562 RVA: 0x000CEB9C File Offset: 0x000CEB9C
		public virtual void NotifySelectedCipherSuite(int selectedCipherSuite)
		{
			this.mSelectedCipherSuite = selectedCipherSuite;
		}

		// Token: 0x0600255B RID: 9563 RVA: 0x000CEBA8 File Offset: 0x000CEBA8
		public virtual void NotifySelectedCompressionMethod(byte selectedCompressionMethod)
		{
			this.mSelectedCompressionMethod = (short)selectedCompressionMethod;
		}

		// Token: 0x0600255C RID: 9564 RVA: 0x000CEBB4 File Offset: 0x000CEBB4
		public virtual void ProcessServerExtensions(IDictionary serverExtensions)
		{
			if (serverExtensions != null)
			{
				this.CheckForUnexpectedServerExtension(serverExtensions, 13);
				this.CheckForUnexpectedServerExtension(serverExtensions, 10);
				if (TlsEccUtilities.IsEccCipherSuite(this.mSelectedCipherSuite))
				{
					this.mServerECPointFormats = TlsEccUtilities.GetSupportedPointFormatsExtension(serverExtensions);
				}
				else
				{
					this.CheckForUnexpectedServerExtension(serverExtensions, 11);
				}
				this.CheckForUnexpectedServerExtension(serverExtensions, 21);
			}
		}

		// Token: 0x0600255D RID: 9565 RVA: 0x000CEC10 File Offset: 0x000CEC10
		public virtual void ProcessServerSupplementalData(IList serverSupplementalData)
		{
			if (serverSupplementalData != null)
			{
				throw new TlsFatalAlert(10);
			}
		}

		// Token: 0x0600255E RID: 9566
		public abstract TlsKeyExchange GetKeyExchange();

		// Token: 0x0600255F RID: 9567
		public abstract TlsAuthentication GetAuthentication();

		// Token: 0x06002560 RID: 9568 RVA: 0x000CEC20 File Offset: 0x000CEC20
		public virtual IList GetClientSupplementalData()
		{
			return null;
		}

		// Token: 0x06002561 RID: 9569 RVA: 0x000CEC24 File Offset: 0x000CEC24
		public override TlsCompression GetCompression()
		{
			switch (this.mSelectedCompressionMethod)
			{
			case 0:
				return new TlsNullCompression();
			case 1:
				return new TlsDeflateCompression();
			default:
				throw new TlsFatalAlert(80);
			}
		}

		// Token: 0x06002562 RID: 9570 RVA: 0x000CEC64 File Offset: 0x000CEC64
		public override TlsCipher GetCipher()
		{
			int encryptionAlgorithm = TlsUtilities.GetEncryptionAlgorithm(this.mSelectedCipherSuite);
			int macAlgorithm = TlsUtilities.GetMacAlgorithm(this.mSelectedCipherSuite);
			return this.mCipherFactory.CreateCipher(this.mContext, encryptionAlgorithm, macAlgorithm);
		}

		// Token: 0x06002563 RID: 9571 RVA: 0x000CECA0 File Offset: 0x000CECA0
		public virtual void NotifyNewSessionTicket(NewSessionTicket newSessionTicket)
		{
		}

		// Token: 0x04001781 RID: 6017
		protected TlsCipherFactory mCipherFactory;

		// Token: 0x04001782 RID: 6018
		protected TlsClientContext mContext;

		// Token: 0x04001783 RID: 6019
		protected IList mSupportedSignatureAlgorithms;

		// Token: 0x04001784 RID: 6020
		protected int[] mNamedCurves;

		// Token: 0x04001785 RID: 6021
		protected byte[] mClientECPointFormats;

		// Token: 0x04001786 RID: 6022
		protected byte[] mServerECPointFormats;

		// Token: 0x04001787 RID: 6023
		protected int mSelectedCipherSuite;

		// Token: 0x04001788 RID: 6024
		protected short mSelectedCompressionMethod;
	}
}
