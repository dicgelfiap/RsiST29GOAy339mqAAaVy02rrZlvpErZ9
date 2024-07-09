using System;
using System.Text;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.X509;

namespace Org.BouncyCastle.Pkix
{
	// Token: 0x020006A1 RID: 1697
	public class TrustAnchor
	{
		// Token: 0x06003B7A RID: 15226 RVA: 0x00144180 File Offset: 0x00144180
		public TrustAnchor(X509Certificate trustedCert, byte[] nameConstraints)
		{
			if (trustedCert == null)
			{
				throw new ArgumentNullException("trustedCert");
			}
			this.trustedCert = trustedCert;
			this.pubKey = null;
			this.caName = null;
			this.caPrincipal = null;
			this.setNameConstraints(nameConstraints);
		}

		// Token: 0x06003B7B RID: 15227 RVA: 0x001441BC File Offset: 0x001441BC
		public TrustAnchor(X509Name caPrincipal, AsymmetricKeyParameter pubKey, byte[] nameConstraints)
		{
			if (caPrincipal == null)
			{
				throw new ArgumentNullException("caPrincipal");
			}
			if (pubKey == null)
			{
				throw new ArgumentNullException("pubKey");
			}
			this.trustedCert = null;
			this.caPrincipal = caPrincipal;
			this.caName = caPrincipal.ToString();
			this.pubKey = pubKey;
			this.setNameConstraints(nameConstraints);
		}

		// Token: 0x06003B7C RID: 15228 RVA: 0x00144220 File Offset: 0x00144220
		public TrustAnchor(string caName, AsymmetricKeyParameter pubKey, byte[] nameConstraints)
		{
			if (caName == null)
			{
				throw new ArgumentNullException("caName");
			}
			if (pubKey == null)
			{
				throw new ArgumentNullException("pubKey");
			}
			if (caName.Length == 0)
			{
				throw new ArgumentException("caName can not be an empty string");
			}
			this.caPrincipal = new X509Name(caName);
			this.pubKey = pubKey;
			this.caName = caName;
			this.trustedCert = null;
			this.setNameConstraints(nameConstraints);
		}

		// Token: 0x17000A21 RID: 2593
		// (get) Token: 0x06003B7D RID: 15229 RVA: 0x00144298 File Offset: 0x00144298
		public X509Certificate TrustedCert
		{
			get
			{
				return this.trustedCert;
			}
		}

		// Token: 0x17000A22 RID: 2594
		// (get) Token: 0x06003B7E RID: 15230 RVA: 0x001442A0 File Offset: 0x001442A0
		public X509Name CA
		{
			get
			{
				return this.caPrincipal;
			}
		}

		// Token: 0x17000A23 RID: 2595
		// (get) Token: 0x06003B7F RID: 15231 RVA: 0x001442A8 File Offset: 0x001442A8
		public string CAName
		{
			get
			{
				return this.caName;
			}
		}

		// Token: 0x17000A24 RID: 2596
		// (get) Token: 0x06003B80 RID: 15232 RVA: 0x001442B0 File Offset: 0x001442B0
		public AsymmetricKeyParameter CAPublicKey
		{
			get
			{
				return this.pubKey;
			}
		}

		// Token: 0x06003B81 RID: 15233 RVA: 0x001442B8 File Offset: 0x001442B8
		private void setNameConstraints(byte[] bytes)
		{
			if (bytes == null)
			{
				this.ncBytes = null;
				this.nc = null;
				return;
			}
			this.ncBytes = (byte[])bytes.Clone();
			this.nc = NameConstraints.GetInstance(Asn1Object.FromByteArray(bytes));
		}

		// Token: 0x17000A25 RID: 2597
		// (get) Token: 0x06003B82 RID: 15234 RVA: 0x001442F4 File Offset: 0x001442F4
		public byte[] GetNameConstraints
		{
			get
			{
				return Arrays.Clone(this.ncBytes);
			}
		}

		// Token: 0x06003B83 RID: 15235 RVA: 0x00144304 File Offset: 0x00144304
		public override string ToString()
		{
			string newLine = Platform.NewLine;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[");
			stringBuilder.Append(newLine);
			if (this.pubKey != null)
			{
				stringBuilder.Append("  Trusted CA Public Key: ").Append(this.pubKey).Append(newLine);
				stringBuilder.Append("  Trusted CA Issuer Name: ").Append(this.caName).Append(newLine);
			}
			else
			{
				stringBuilder.Append("  Trusted CA cert: ").Append(this.TrustedCert).Append(newLine);
			}
			if (this.nc != null)
			{
				stringBuilder.Append("  Name Constraints: ").Append(this.nc).Append(newLine);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04001E8E RID: 7822
		private readonly AsymmetricKeyParameter pubKey;

		// Token: 0x04001E8F RID: 7823
		private readonly string caName;

		// Token: 0x04001E90 RID: 7824
		private readonly X509Name caPrincipal;

		// Token: 0x04001E91 RID: 7825
		private readonly X509Certificate trustedCert;

		// Token: 0x04001E92 RID: 7826
		private byte[] ncBytes;

		// Token: 0x04001E93 RID: 7827
		private NameConstraints nc;
	}
}
