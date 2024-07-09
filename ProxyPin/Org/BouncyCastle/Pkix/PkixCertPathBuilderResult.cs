using System;
using System.Text;
using Org.BouncyCastle.Crypto;

namespace Org.BouncyCastle.Pkix
{
	// Token: 0x02000695 RID: 1685
	public class PkixCertPathBuilderResult : PkixCertPathValidatorResult
	{
		// Token: 0x06003ABF RID: 15039 RVA: 0x0013C680 File Offset: 0x0013C680
		public PkixCertPathBuilderResult(PkixCertPath certPath, TrustAnchor trustAnchor, PkixPolicyNode policyTree, AsymmetricKeyParameter subjectPublicKey) : base(trustAnchor, policyTree, subjectPublicKey)
		{
			if (certPath == null)
			{
				throw new ArgumentNullException("certPath");
			}
			this.certPath = certPath;
		}

		// Token: 0x17000A13 RID: 2579
		// (get) Token: 0x06003AC0 RID: 15040 RVA: 0x0013C6A4 File Offset: 0x0013C6A4
		public PkixCertPath CertPath
		{
			get
			{
				return this.certPath;
			}
		}

		// Token: 0x06003AC1 RID: 15041 RVA: 0x0013C6AC File Offset: 0x0013C6AC
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("SimplePKIXCertPathBuilderResult: [\n");
			stringBuilder.Append("  Certification Path: ").Append(this.CertPath).Append('\n');
			stringBuilder.Append("  Trust Anchor: ").Append(base.TrustAnchor.TrustedCert.IssuerDN.ToString()).Append('\n');
			stringBuilder.Append("  Subject Public Key: ").Append(base.SubjectPublicKey).Append("\n]");
			return stringBuilder.ToString();
		}

		// Token: 0x04001E6B RID: 7787
		private PkixCertPath certPath;
	}
}
