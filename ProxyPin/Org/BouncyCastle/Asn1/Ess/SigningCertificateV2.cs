using System;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Ess
{
	// Token: 0x0200016A RID: 362
	public class SigningCertificateV2 : Asn1Encodable
	{
		// Token: 0x06000C52 RID: 3154 RVA: 0x0004F604 File Offset: 0x0004F604
		public static SigningCertificateV2 GetInstance(object o)
		{
			if (o == null || o is SigningCertificateV2)
			{
				return (SigningCertificateV2)o;
			}
			if (o is Asn1Sequence)
			{
				return new SigningCertificateV2((Asn1Sequence)o);
			}
			throw new ArgumentException("unknown object in 'SigningCertificateV2' factory : " + Platform.GetTypeName(o) + ".");
		}

		// Token: 0x06000C53 RID: 3155 RVA: 0x0004F660 File Offset: 0x0004F660
		private SigningCertificateV2(Asn1Sequence seq)
		{
			if (seq.Count < 1 || seq.Count > 2)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count, "seq");
			}
			this.certs = Asn1Sequence.GetInstance(seq[0].ToAsn1Object());
			if (seq.Count > 1)
			{
				this.policies = Asn1Sequence.GetInstance(seq[1].ToAsn1Object());
			}
		}

		// Token: 0x06000C54 RID: 3156 RVA: 0x0004F6EC File Offset: 0x0004F6EC
		public SigningCertificateV2(EssCertIDv2 cert)
		{
			this.certs = new DerSequence(cert);
		}

		// Token: 0x06000C55 RID: 3157 RVA: 0x0004F700 File Offset: 0x0004F700
		public SigningCertificateV2(EssCertIDv2[] certs)
		{
			this.certs = new DerSequence(certs);
		}

		// Token: 0x06000C56 RID: 3158 RVA: 0x0004F714 File Offset: 0x0004F714
		public SigningCertificateV2(EssCertIDv2[] certs, PolicyInformation[] policies)
		{
			this.certs = new DerSequence(certs);
			if (policies != null)
			{
				this.policies = new DerSequence(policies);
			}
		}

		// Token: 0x06000C57 RID: 3159 RVA: 0x0004F73C File Offset: 0x0004F73C
		public EssCertIDv2[] GetCerts()
		{
			EssCertIDv2[] array = new EssCertIDv2[this.certs.Count];
			for (int num = 0; num != this.certs.Count; num++)
			{
				array[num] = EssCertIDv2.GetInstance(this.certs[num]);
			}
			return array;
		}

		// Token: 0x06000C58 RID: 3160 RVA: 0x0004F790 File Offset: 0x0004F790
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

		// Token: 0x06000C59 RID: 3161 RVA: 0x0004F7F4 File Offset: 0x0004F7F4
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

		// Token: 0x0400083B RID: 2107
		private readonly Asn1Sequence certs;

		// Token: 0x0400083C RID: 2108
		private readonly Asn1Sequence policies;
	}
}
