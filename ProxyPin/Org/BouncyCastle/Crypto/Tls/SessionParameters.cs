using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x0200051C RID: 1308
	public sealed class SessionParameters
	{
		// Token: 0x060027DB RID: 10203 RVA: 0x000D6D7C File Offset: 0x000D6D7C
		private SessionParameters(int cipherSuite, byte compressionAlgorithm, byte[] masterSecret, Certificate peerCertificate, byte[] pskIdentity, byte[] srpIdentity, byte[] encodedServerExtensions, bool extendedMasterSecret)
		{
			this.mCipherSuite = cipherSuite;
			this.mCompressionAlgorithm = compressionAlgorithm;
			this.mMasterSecret = Arrays.Clone(masterSecret);
			this.mPeerCertificate = peerCertificate;
			this.mPskIdentity = Arrays.Clone(pskIdentity);
			this.mSrpIdentity = Arrays.Clone(srpIdentity);
			this.mEncodedServerExtensions = encodedServerExtensions;
			this.mExtendedMasterSecret = extendedMasterSecret;
		}

		// Token: 0x060027DC RID: 10204 RVA: 0x000D6DE0 File Offset: 0x000D6DE0
		public void Clear()
		{
			if (this.mMasterSecret != null)
			{
				Arrays.Fill(this.mMasterSecret, 0);
			}
		}

		// Token: 0x060027DD RID: 10205 RVA: 0x000D6DFC File Offset: 0x000D6DFC
		public SessionParameters Copy()
		{
			return new SessionParameters(this.mCipherSuite, this.mCompressionAlgorithm, this.mMasterSecret, this.mPeerCertificate, this.mPskIdentity, this.mSrpIdentity, this.mEncodedServerExtensions, this.mExtendedMasterSecret);
		}

		// Token: 0x17000770 RID: 1904
		// (get) Token: 0x060027DE RID: 10206 RVA: 0x000D6E34 File Offset: 0x000D6E34
		public int CipherSuite
		{
			get
			{
				return this.mCipherSuite;
			}
		}

		// Token: 0x17000771 RID: 1905
		// (get) Token: 0x060027DF RID: 10207 RVA: 0x000D6E3C File Offset: 0x000D6E3C
		public byte CompressionAlgorithm
		{
			get
			{
				return this.mCompressionAlgorithm;
			}
		}

		// Token: 0x17000772 RID: 1906
		// (get) Token: 0x060027E0 RID: 10208 RVA: 0x000D6E44 File Offset: 0x000D6E44
		public bool IsExtendedMasterSecret
		{
			get
			{
				return this.mExtendedMasterSecret;
			}
		}

		// Token: 0x17000773 RID: 1907
		// (get) Token: 0x060027E1 RID: 10209 RVA: 0x000D6E4C File Offset: 0x000D6E4C
		public byte[] MasterSecret
		{
			get
			{
				return this.mMasterSecret;
			}
		}

		// Token: 0x17000774 RID: 1908
		// (get) Token: 0x060027E2 RID: 10210 RVA: 0x000D6E54 File Offset: 0x000D6E54
		public Certificate PeerCertificate
		{
			get
			{
				return this.mPeerCertificate;
			}
		}

		// Token: 0x17000775 RID: 1909
		// (get) Token: 0x060027E3 RID: 10211 RVA: 0x000D6E5C File Offset: 0x000D6E5C
		public byte[] PskIdentity
		{
			get
			{
				return this.mPskIdentity;
			}
		}

		// Token: 0x17000776 RID: 1910
		// (get) Token: 0x060027E4 RID: 10212 RVA: 0x000D6E64 File Offset: 0x000D6E64
		public byte[] SrpIdentity
		{
			get
			{
				return this.mSrpIdentity;
			}
		}

		// Token: 0x060027E5 RID: 10213 RVA: 0x000D6E6C File Offset: 0x000D6E6C
		public IDictionary ReadServerExtensions()
		{
			if (this.mEncodedServerExtensions == null)
			{
				return null;
			}
			MemoryStream input = new MemoryStream(this.mEncodedServerExtensions, false);
			return TlsProtocol.ReadExtensions(input);
		}

		// Token: 0x04001A44 RID: 6724
		private int mCipherSuite;

		// Token: 0x04001A45 RID: 6725
		private byte mCompressionAlgorithm;

		// Token: 0x04001A46 RID: 6726
		private byte[] mMasterSecret;

		// Token: 0x04001A47 RID: 6727
		private Certificate mPeerCertificate;

		// Token: 0x04001A48 RID: 6728
		private byte[] mPskIdentity;

		// Token: 0x04001A49 RID: 6729
		private byte[] mSrpIdentity;

		// Token: 0x04001A4A RID: 6730
		private byte[] mEncodedServerExtensions;

		// Token: 0x04001A4B RID: 6731
		private bool mExtendedMasterSecret;

		// Token: 0x02000E23 RID: 3619
		public sealed class Builder
		{
			// Token: 0x06008C59 RID: 35929 RVA: 0x002A2524 File Offset: 0x002A2524
			public SessionParameters Build()
			{
				this.Validate(this.mCipherSuite >= 0, "cipherSuite");
				this.Validate(this.mCompressionAlgorithm >= 0, "compressionAlgorithm");
				this.Validate(this.mMasterSecret != null, "masterSecret");
				return new SessionParameters(this.mCipherSuite, (byte)this.mCompressionAlgorithm, this.mMasterSecret, this.mPeerCertificate, this.mPskIdentity, this.mSrpIdentity, this.mEncodedServerExtensions, this.mExtendedMasterSecret);
			}

			// Token: 0x06008C5A RID: 35930 RVA: 0x002A25B0 File Offset: 0x002A25B0
			public SessionParameters.Builder SetCipherSuite(int cipherSuite)
			{
				this.mCipherSuite = cipherSuite;
				return this;
			}

			// Token: 0x06008C5B RID: 35931 RVA: 0x002A25BC File Offset: 0x002A25BC
			public SessionParameters.Builder SetCompressionAlgorithm(byte compressionAlgorithm)
			{
				this.mCompressionAlgorithm = (short)compressionAlgorithm;
				return this;
			}

			// Token: 0x06008C5C RID: 35932 RVA: 0x002A25C8 File Offset: 0x002A25C8
			public SessionParameters.Builder SetExtendedMasterSecret(bool extendedMasterSecret)
			{
				this.mExtendedMasterSecret = extendedMasterSecret;
				return this;
			}

			// Token: 0x06008C5D RID: 35933 RVA: 0x002A25D4 File Offset: 0x002A25D4
			public SessionParameters.Builder SetMasterSecret(byte[] masterSecret)
			{
				this.mMasterSecret = masterSecret;
				return this;
			}

			// Token: 0x06008C5E RID: 35934 RVA: 0x002A25E0 File Offset: 0x002A25E0
			public SessionParameters.Builder SetPeerCertificate(Certificate peerCertificate)
			{
				this.mPeerCertificate = peerCertificate;
				return this;
			}

			// Token: 0x06008C5F RID: 35935 RVA: 0x002A25EC File Offset: 0x002A25EC
			public SessionParameters.Builder SetPskIdentity(byte[] pskIdentity)
			{
				this.mPskIdentity = pskIdentity;
				return this;
			}

			// Token: 0x06008C60 RID: 35936 RVA: 0x002A25F8 File Offset: 0x002A25F8
			public SessionParameters.Builder SetSrpIdentity(byte[] srpIdentity)
			{
				this.mSrpIdentity = srpIdentity;
				return this;
			}

			// Token: 0x06008C61 RID: 35937 RVA: 0x002A2604 File Offset: 0x002A2604
			public SessionParameters.Builder SetServerExtensions(IDictionary serverExtensions)
			{
				if (serverExtensions == null)
				{
					this.mEncodedServerExtensions = null;
				}
				else
				{
					MemoryStream memoryStream = new MemoryStream();
					TlsProtocol.WriteExtensions(memoryStream, serverExtensions);
					this.mEncodedServerExtensions = memoryStream.ToArray();
				}
				return this;
			}

			// Token: 0x06008C62 RID: 35938 RVA: 0x002A2644 File Offset: 0x002A2644
			private void Validate(bool condition, string parameter)
			{
				if (!condition)
				{
					throw new InvalidOperationException("Required session parameter '" + parameter + "' not configured");
				}
			}

			// Token: 0x04004189 RID: 16777
			private int mCipherSuite = -1;

			// Token: 0x0400418A RID: 16778
			private short mCompressionAlgorithm = -1;

			// Token: 0x0400418B RID: 16779
			private byte[] mMasterSecret = null;

			// Token: 0x0400418C RID: 16780
			private Certificate mPeerCertificate = null;

			// Token: 0x0400418D RID: 16781
			private byte[] mPskIdentity = null;

			// Token: 0x0400418E RID: 16782
			private byte[] mSrpIdentity = null;

			// Token: 0x0400418F RID: 16783
			private byte[] mEncodedServerExtensions = null;

			// Token: 0x04004190 RID: 16784
			private bool mExtendedMasterSecret = false;
		}
	}
}
