using System;
using System.Text;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x020001ED RID: 493
	public class CertificatePolicies : Asn1Encodable
	{
		// Token: 0x06000FDF RID: 4063 RVA: 0x0005DA68 File Offset: 0x0005DA68
		private static PolicyInformation[] Copy(PolicyInformation[] policyInfo)
		{
			return (PolicyInformation[])policyInfo.Clone();
		}

		// Token: 0x06000FE0 RID: 4064 RVA: 0x0005DA78 File Offset: 0x0005DA78
		public static CertificatePolicies GetInstance(object obj)
		{
			if (obj is CertificatePolicies)
			{
				return (CertificatePolicies)obj;
			}
			if (obj == null)
			{
				return null;
			}
			return new CertificatePolicies(Asn1Sequence.GetInstance(obj));
		}

		// Token: 0x06000FE1 RID: 4065 RVA: 0x0005DAA0 File Offset: 0x0005DAA0
		public static CertificatePolicies GetInstance(Asn1TaggedObject obj, bool isExplicit)
		{
			return CertificatePolicies.GetInstance(Asn1Sequence.GetInstance(obj, isExplicit));
		}

		// Token: 0x06000FE2 RID: 4066 RVA: 0x0005DAB0 File Offset: 0x0005DAB0
		public static CertificatePolicies FromExtensions(X509Extensions extensions)
		{
			return CertificatePolicies.GetInstance(X509Extensions.GetExtensionParsedValue(extensions, X509Extensions.CertificatePolicies));
		}

		// Token: 0x06000FE3 RID: 4067 RVA: 0x0005DAC4 File Offset: 0x0005DAC4
		public CertificatePolicies(PolicyInformation name)
		{
			this.policyInformation = new PolicyInformation[]
			{
				name
			};
		}

		// Token: 0x06000FE4 RID: 4068 RVA: 0x0005DAF4 File Offset: 0x0005DAF4
		public CertificatePolicies(PolicyInformation[] policyInformation)
		{
			this.policyInformation = CertificatePolicies.Copy(policyInformation);
		}

		// Token: 0x06000FE5 RID: 4069 RVA: 0x0005DB08 File Offset: 0x0005DB08
		private CertificatePolicies(Asn1Sequence seq)
		{
			this.policyInformation = new PolicyInformation[seq.Count];
			for (int i = 0; i < seq.Count; i++)
			{
				this.policyInformation[i] = PolicyInformation.GetInstance(seq[i]);
			}
		}

		// Token: 0x06000FE6 RID: 4070 RVA: 0x0005DB5C File Offset: 0x0005DB5C
		public virtual PolicyInformation[] GetPolicyInformation()
		{
			return CertificatePolicies.Copy(this.policyInformation);
		}

		// Token: 0x06000FE7 RID: 4071 RVA: 0x0005DB6C File Offset: 0x0005DB6C
		public virtual PolicyInformation GetPolicyInformation(DerObjectIdentifier policyIdentifier)
		{
			for (int num = 0; num != this.policyInformation.Length; num++)
			{
				if (policyIdentifier.Equals(this.policyInformation[num].PolicyIdentifier))
				{
					return this.policyInformation[num];
				}
			}
			return null;
		}

		// Token: 0x06000FE8 RID: 4072 RVA: 0x0005DBC0 File Offset: 0x0005DBC0
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(this.policyInformation);
		}

		// Token: 0x06000FE9 RID: 4073 RVA: 0x0005DBD0 File Offset: 0x0005DBD0
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder("CertificatePolicies:");
			if (this.policyInformation != null && this.policyInformation.Length > 0)
			{
				stringBuilder.Append(' ');
				stringBuilder.Append(this.policyInformation[0]);
				for (int i = 1; i < this.policyInformation.Length; i++)
				{
					stringBuilder.Append(", ");
					stringBuilder.Append(this.policyInformation[i]);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000BB8 RID: 3000
		private readonly PolicyInformation[] policyInformation;
	}
}
