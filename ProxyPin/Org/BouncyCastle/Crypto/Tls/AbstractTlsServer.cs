using System;
using System.Collections;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x020004C4 RID: 1220
	public abstract class AbstractTlsServer : AbstractTlsPeer, TlsServer, TlsPeer
	{
		// Token: 0x060025B3 RID: 9651 RVA: 0x000CF084 File Offset: 0x000CF084
		public AbstractTlsServer() : this(new DefaultTlsCipherFactory())
		{
		}

		// Token: 0x060025B4 RID: 9652 RVA: 0x000CF094 File Offset: 0x000CF094
		public AbstractTlsServer(TlsCipherFactory cipherFactory)
		{
			this.mCipherFactory = cipherFactory;
		}

		// Token: 0x17000724 RID: 1828
		// (get) Token: 0x060025B5 RID: 9653 RVA: 0x000CF0A4 File Offset: 0x000CF0A4
		protected virtual bool AllowEncryptThenMac
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000725 RID: 1829
		// (get) Token: 0x060025B6 RID: 9654 RVA: 0x000CF0A8 File Offset: 0x000CF0A8
		protected virtual bool AllowTruncatedHMac
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060025B7 RID: 9655 RVA: 0x000CF0AC File Offset: 0x000CF0AC
		protected virtual IDictionary CheckServerExtensions()
		{
			return this.mServerExtensions = TlsExtensionsUtilities.EnsureExtensionsInitialised(this.mServerExtensions);
		}

		// Token: 0x060025B8 RID: 9656
		protected abstract int[] GetCipherSuites();

		// Token: 0x060025B9 RID: 9657 RVA: 0x000CF0D4 File Offset: 0x000CF0D4
		protected byte[] GetCompressionMethods()
		{
			return new byte[1];
		}

		// Token: 0x17000726 RID: 1830
		// (get) Token: 0x060025BA RID: 9658 RVA: 0x000CF0F0 File Offset: 0x000CF0F0
		protected virtual ProtocolVersion MaximumVersion
		{
			get
			{
				return ProtocolVersion.TLSv11;
			}
		}

		// Token: 0x17000727 RID: 1831
		// (get) Token: 0x060025BB RID: 9659 RVA: 0x000CF0F8 File Offset: 0x000CF0F8
		protected virtual ProtocolVersion MinimumVersion
		{
			get
			{
				return ProtocolVersion.TLSv10;
			}
		}

		// Token: 0x060025BC RID: 9660 RVA: 0x000CF100 File Offset: 0x000CF100
		protected virtual bool SupportsClientEccCapabilities(int[] namedCurves, byte[] ecPointFormats)
		{
			if (namedCurves == null)
			{
				return TlsEccUtilities.HasAnySupportedNamedCurves();
			}
			foreach (int namedCurve in namedCurves)
			{
				if (NamedCurve.IsValid(namedCurve) && (!NamedCurve.RefersToASpecificNamedCurve(namedCurve) || TlsEccUtilities.IsSupportedNamedCurve(namedCurve)))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060025BD RID: 9661 RVA: 0x000CF158 File Offset: 0x000CF158
		public virtual void Init(TlsServerContext context)
		{
			this.mContext = context;
		}

		// Token: 0x060025BE RID: 9662 RVA: 0x000CF164 File Offset: 0x000CF164
		public virtual void NotifyClientVersion(ProtocolVersion clientVersion)
		{
			this.mClientVersion = clientVersion;
		}

		// Token: 0x060025BF RID: 9663 RVA: 0x000CF170 File Offset: 0x000CF170
		public virtual void NotifyFallback(bool isFallback)
		{
			if (isFallback && this.MaximumVersion.IsLaterVersionOf(this.mClientVersion))
			{
				throw new TlsFatalAlert(86);
			}
		}

		// Token: 0x060025C0 RID: 9664 RVA: 0x000CF198 File Offset: 0x000CF198
		public virtual void NotifyOfferedCipherSuites(int[] offeredCipherSuites)
		{
			this.mOfferedCipherSuites = offeredCipherSuites;
			this.mEccCipherSuitesOffered = TlsEccUtilities.ContainsEccCipherSuites(this.mOfferedCipherSuites);
		}

		// Token: 0x060025C1 RID: 9665 RVA: 0x000CF1B4 File Offset: 0x000CF1B4
		public virtual void NotifyOfferedCompressionMethods(byte[] offeredCompressionMethods)
		{
			this.mOfferedCompressionMethods = offeredCompressionMethods;
		}

		// Token: 0x060025C2 RID: 9666 RVA: 0x000CF1C0 File Offset: 0x000CF1C0
		public virtual void ProcessClientExtensions(IDictionary clientExtensions)
		{
			this.mClientExtensions = clientExtensions;
			if (clientExtensions != null)
			{
				this.mEncryptThenMacOffered = TlsExtensionsUtilities.HasEncryptThenMacExtension(clientExtensions);
				this.mMaxFragmentLengthOffered = TlsExtensionsUtilities.GetMaxFragmentLengthExtension(clientExtensions);
				if (this.mMaxFragmentLengthOffered >= 0 && !MaxFragmentLength.IsValid((byte)this.mMaxFragmentLengthOffered))
				{
					throw new TlsFatalAlert(47);
				}
				this.mTruncatedHMacOffered = TlsExtensionsUtilities.HasTruncatedHMacExtension(clientExtensions);
				this.mSupportedSignatureAlgorithms = TlsUtilities.GetSignatureAlgorithmsExtension(clientExtensions);
				if (this.mSupportedSignatureAlgorithms != null && !TlsUtilities.IsSignatureAlgorithmsExtensionAllowed(this.mClientVersion))
				{
					throw new TlsFatalAlert(47);
				}
				this.mNamedCurves = TlsEccUtilities.GetSupportedEllipticCurvesExtension(clientExtensions);
				this.mClientECPointFormats = TlsEccUtilities.GetSupportedPointFormatsExtension(clientExtensions);
			}
		}

		// Token: 0x060025C3 RID: 9667 RVA: 0x000CF270 File Offset: 0x000CF270
		public virtual ProtocolVersion GetServerVersion()
		{
			if (this.MinimumVersion.IsEqualOrEarlierVersionOf(this.mClientVersion))
			{
				ProtocolVersion maximumVersion = this.MaximumVersion;
				if (this.mClientVersion.IsEqualOrEarlierVersionOf(maximumVersion))
				{
					return this.mServerVersion = this.mClientVersion;
				}
				if (this.mClientVersion.IsLaterVersionOf(maximumVersion))
				{
					return this.mServerVersion = maximumVersion;
				}
			}
			throw new TlsFatalAlert(70);
		}

		// Token: 0x060025C4 RID: 9668 RVA: 0x000CF2E4 File Offset: 0x000CF2E4
		public virtual int GetSelectedCipherSuite()
		{
			IList usableSignatureAlgorithms = TlsUtilities.GetUsableSignatureAlgorithms(this.mSupportedSignatureAlgorithms);
			bool flag = this.SupportsClientEccCapabilities(this.mNamedCurves, this.mClientECPointFormats);
			foreach (int num in this.GetCipherSuites())
			{
				if (Arrays.Contains(this.mOfferedCipherSuites, num) && (flag || !TlsEccUtilities.IsEccCipherSuite(num)) && TlsUtilities.IsValidCipherSuiteForVersion(num, this.mServerVersion) && TlsUtilities.IsValidCipherSuiteForSignatureAlgorithms(num, usableSignatureAlgorithms))
				{
					return this.mSelectedCipherSuite = num;
				}
			}
			throw new TlsFatalAlert(40);
		}

		// Token: 0x060025C5 RID: 9669 RVA: 0x000CF38C File Offset: 0x000CF38C
		public virtual byte GetSelectedCompressionMethod()
		{
			byte[] compressionMethods = this.GetCompressionMethods();
			for (int i = 0; i < compressionMethods.Length; i++)
			{
				if (Arrays.Contains(this.mOfferedCompressionMethods, compressionMethods[i]))
				{
					return this.mSelectedCompressionMethod = compressionMethods[i];
				}
			}
			throw new TlsFatalAlert(40);
		}

		// Token: 0x060025C6 RID: 9670 RVA: 0x000CF3DC File Offset: 0x000CF3DC
		public virtual IDictionary GetServerExtensions()
		{
			if (this.mEncryptThenMacOffered && this.AllowEncryptThenMac && TlsUtilities.IsBlockCipherSuite(this.mSelectedCipherSuite))
			{
				TlsExtensionsUtilities.AddEncryptThenMacExtension(this.CheckServerExtensions());
			}
			if (this.mMaxFragmentLengthOffered >= 0 && TlsUtilities.IsValidUint8((int)this.mMaxFragmentLengthOffered) && MaxFragmentLength.IsValid((byte)this.mMaxFragmentLengthOffered))
			{
				TlsExtensionsUtilities.AddMaxFragmentLengthExtension(this.CheckServerExtensions(), (byte)this.mMaxFragmentLengthOffered);
			}
			if (this.mTruncatedHMacOffered && this.AllowTruncatedHMac)
			{
				TlsExtensionsUtilities.AddTruncatedHMacExtension(this.CheckServerExtensions());
			}
			if (this.mClientECPointFormats != null && TlsEccUtilities.IsEccCipherSuite(this.mSelectedCipherSuite))
			{
				this.mServerECPointFormats = new byte[]
				{
					0,
					1,
					2
				};
				TlsEccUtilities.AddSupportedPointFormatsExtension(this.CheckServerExtensions(), this.mServerECPointFormats);
			}
			return this.mServerExtensions;
		}

		// Token: 0x060025C7 RID: 9671 RVA: 0x000CF4C8 File Offset: 0x000CF4C8
		public virtual IList GetServerSupplementalData()
		{
			return null;
		}

		// Token: 0x060025C8 RID: 9672
		public abstract TlsCredentials GetCredentials();

		// Token: 0x060025C9 RID: 9673 RVA: 0x000CF4CC File Offset: 0x000CF4CC
		public virtual CertificateStatus GetCertificateStatus()
		{
			return null;
		}

		// Token: 0x060025CA RID: 9674
		public abstract TlsKeyExchange GetKeyExchange();

		// Token: 0x060025CB RID: 9675 RVA: 0x000CF4D0 File Offset: 0x000CF4D0
		public virtual CertificateRequest GetCertificateRequest()
		{
			return null;
		}

		// Token: 0x060025CC RID: 9676 RVA: 0x000CF4D4 File Offset: 0x000CF4D4
		public virtual void ProcessClientSupplementalData(IList clientSupplementalData)
		{
			if (clientSupplementalData != null)
			{
				throw new TlsFatalAlert(10);
			}
		}

		// Token: 0x060025CD RID: 9677 RVA: 0x000CF4E4 File Offset: 0x000CF4E4
		public virtual void NotifyClientCertificate(Certificate clientCertificate)
		{
			throw new TlsFatalAlert(80);
		}

		// Token: 0x060025CE RID: 9678 RVA: 0x000CF4F0 File Offset: 0x000CF4F0
		public override TlsCompression GetCompression()
		{
			byte b = this.mSelectedCompressionMethod;
			if (b == 0)
			{
				return new TlsNullCompression();
			}
			throw new TlsFatalAlert(80);
		}

		// Token: 0x060025CF RID: 9679 RVA: 0x000CF51C File Offset: 0x000CF51C
		public override TlsCipher GetCipher()
		{
			int encryptionAlgorithm = TlsUtilities.GetEncryptionAlgorithm(this.mSelectedCipherSuite);
			int macAlgorithm = TlsUtilities.GetMacAlgorithm(this.mSelectedCipherSuite);
			return this.mCipherFactory.CreateCipher(this.mContext, encryptionAlgorithm, macAlgorithm);
		}

		// Token: 0x060025D0 RID: 9680 RVA: 0x000CF558 File Offset: 0x000CF558
		public virtual NewSessionTicket GetNewSessionTicket()
		{
			return new NewSessionTicket(0L, TlsUtilities.EmptyBytes);
		}

		// Token: 0x04001794 RID: 6036
		protected TlsCipherFactory mCipherFactory;

		// Token: 0x04001795 RID: 6037
		protected TlsServerContext mContext;

		// Token: 0x04001796 RID: 6038
		protected ProtocolVersion mClientVersion;

		// Token: 0x04001797 RID: 6039
		protected int[] mOfferedCipherSuites;

		// Token: 0x04001798 RID: 6040
		protected byte[] mOfferedCompressionMethods;

		// Token: 0x04001799 RID: 6041
		protected IDictionary mClientExtensions;

		// Token: 0x0400179A RID: 6042
		protected bool mEncryptThenMacOffered;

		// Token: 0x0400179B RID: 6043
		protected short mMaxFragmentLengthOffered;

		// Token: 0x0400179C RID: 6044
		protected bool mTruncatedHMacOffered;

		// Token: 0x0400179D RID: 6045
		protected IList mSupportedSignatureAlgorithms;

		// Token: 0x0400179E RID: 6046
		protected bool mEccCipherSuitesOffered;

		// Token: 0x0400179F RID: 6047
		protected int[] mNamedCurves;

		// Token: 0x040017A0 RID: 6048
		protected byte[] mClientECPointFormats;

		// Token: 0x040017A1 RID: 6049
		protected byte[] mServerECPointFormats;

		// Token: 0x040017A2 RID: 6050
		protected ProtocolVersion mServerVersion;

		// Token: 0x040017A3 RID: 6051
		protected int mSelectedCipherSuite;

		// Token: 0x040017A4 RID: 6052
		protected byte mSelectedCompressionMethod;

		// Token: 0x040017A5 RID: 6053
		protected IDictionary mServerExtensions;
	}
}
