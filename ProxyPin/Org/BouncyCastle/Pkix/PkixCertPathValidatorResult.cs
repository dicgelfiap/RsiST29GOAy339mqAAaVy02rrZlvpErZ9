using System;
using System.Text;
using Org.BouncyCastle.Crypto;

namespace Org.BouncyCastle.Pkix
{
	// Token: 0x02000694 RID: 1684
	public class PkixCertPathValidatorResult
	{
		// Token: 0x17000A10 RID: 2576
		// (get) Token: 0x06003AB9 RID: 15033 RVA: 0x0013C578 File Offset: 0x0013C578
		public PkixPolicyNode PolicyTree
		{
			get
			{
				return this.policyTree;
			}
		}

		// Token: 0x17000A11 RID: 2577
		// (get) Token: 0x06003ABA RID: 15034 RVA: 0x0013C580 File Offset: 0x0013C580
		public TrustAnchor TrustAnchor
		{
			get
			{
				return this.trustAnchor;
			}
		}

		// Token: 0x17000A12 RID: 2578
		// (get) Token: 0x06003ABB RID: 15035 RVA: 0x0013C588 File Offset: 0x0013C588
		public AsymmetricKeyParameter SubjectPublicKey
		{
			get
			{
				return this.subjectPublicKey;
			}
		}

		// Token: 0x06003ABC RID: 15036 RVA: 0x0013C590 File Offset: 0x0013C590
		public PkixCertPathValidatorResult(TrustAnchor trustAnchor, PkixPolicyNode policyTree, AsymmetricKeyParameter subjectPublicKey)
		{
			if (subjectPublicKey == null)
			{
				throw new NullReferenceException("subjectPublicKey must be non-null");
			}
			if (trustAnchor == null)
			{
				throw new NullReferenceException("trustAnchor must be non-null");
			}
			this.trustAnchor = trustAnchor;
			this.policyTree = policyTree;
			this.subjectPublicKey = subjectPublicKey;
		}

		// Token: 0x06003ABD RID: 15037 RVA: 0x0013C5D0 File Offset: 0x0013C5D0
		public object Clone()
		{
			return new PkixCertPathValidatorResult(this.TrustAnchor, this.PolicyTree, this.SubjectPublicKey);
		}

		// Token: 0x06003ABE RID: 15038 RVA: 0x0013C5F8 File Offset: 0x0013C5F8
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("PKIXCertPathValidatorResult: [ \n");
			stringBuilder.Append("  Trust Anchor: ").Append(this.TrustAnchor).Append('\n');
			stringBuilder.Append("  Policy Tree: ").Append(this.PolicyTree).Append('\n');
			stringBuilder.Append("  Subject Public Key: ").Append(this.SubjectPublicKey).Append("\n]");
			return stringBuilder.ToString();
		}

		// Token: 0x04001E68 RID: 7784
		private TrustAnchor trustAnchor;

		// Token: 0x04001E69 RID: 7785
		private PkixPolicyNode policyTree;

		// Token: 0x04001E6A RID: 7786
		private AsymmetricKeyParameter subjectPublicKey;
	}
}
