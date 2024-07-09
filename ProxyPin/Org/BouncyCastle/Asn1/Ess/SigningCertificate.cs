using System;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Ess
{
	// Token: 0x02000169 RID: 361
	public class SigningCertificate : Asn1Encodable
	{
		// Token: 0x06000C4C RID: 3148 RVA: 0x0004F410 File Offset: 0x0004F410
		public static SigningCertificate GetInstance(object o)
		{
			if (o == null || o is SigningCertificate)
			{
				return (SigningCertificate)o;
			}
			if (o is Asn1Sequence)
			{
				return new SigningCertificate((Asn1Sequence)o);
			}
			throw new ArgumentException("unknown object in 'SigningCertificate' factory : " + Platform.GetTypeName(o) + ".");
		}

		// Token: 0x06000C4D RID: 3149 RVA: 0x0004F46C File Offset: 0x0004F46C
		public SigningCertificate(Asn1Sequence seq)
		{
			if (seq.Count < 1 || seq.Count > 2)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count);
			}
			this.certs = Asn1Sequence.GetInstance(seq[0]);
			if (seq.Count > 1)
			{
				this.policies = Asn1Sequence.GetInstance(seq[1]);
			}
		}

		// Token: 0x06000C4E RID: 3150 RVA: 0x0004F4E8 File Offset: 0x0004F4E8
		public SigningCertificate(EssCertID essCertID)
		{
			this.certs = new DerSequence(essCertID);
		}

		// Token: 0x06000C4F RID: 3151 RVA: 0x0004F4FC File Offset: 0x0004F4FC
		public EssCertID[] GetCerts()
		{
			EssCertID[] array = new EssCertID[this.certs.Count];
			for (int num = 0; num != this.certs.Count; num++)
			{
				array[num] = EssCertID.GetInstance(this.certs[num]);
			}
			return array;
		}

		// Token: 0x06000C50 RID: 3152 RVA: 0x0004F550 File Offset: 0x0004F550
		public PolicyInformation[] GetPolicies()
		{
			if (this.policies == null)
			{
				return null;
			}
			PolicyInformation[] array = new PolicyInformation[this.policies.Count];
			for (int num = 0; num != this.policies.Count; num++)
			{
				array[num] = PolicyInformation.GetInstance(this.policies[num]);
			}
			return array;
		}

		// Token: 0x06000C51 RID: 3153 RVA: 0x0004F5B4 File Offset: 0x0004F5B4
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.certs
			});
			asn1EncodableVector.AddOptional(new Asn1Encodable[]
			{
				this.policies
			});
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04000839 RID: 2105
		private Asn1Sequence certs;

		// Token: 0x0400083A RID: 2106
		private Asn1Sequence policies;
	}
}
