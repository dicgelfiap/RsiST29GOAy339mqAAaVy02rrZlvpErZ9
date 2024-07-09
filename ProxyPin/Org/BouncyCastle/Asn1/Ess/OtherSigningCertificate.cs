using System;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Ess
{
	// Token: 0x02000168 RID: 360
	[Obsolete("Use version in Asn1.Esf instead")]
	public class OtherSigningCertificate : Asn1Encodable
	{
		// Token: 0x06000C46 RID: 3142 RVA: 0x0004F21C File Offset: 0x0004F21C
		public static OtherSigningCertificate GetInstance(object o)
		{
			if (o == null || o is OtherSigningCertificate)
			{
				return (OtherSigningCertificate)o;
			}
			if (o is Asn1Sequence)
			{
				return new OtherSigningCertificate((Asn1Sequence)o);
			}
			throw new ArgumentException("unknown object in 'OtherSigningCertificate' factory : " + Platform.GetTypeName(o) + ".");
		}

		// Token: 0x06000C47 RID: 3143 RVA: 0x0004F278 File Offset: 0x0004F278
		public OtherSigningCertificate(Asn1Sequence seq)
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

		// Token: 0x06000C48 RID: 3144 RVA: 0x0004F2F4 File Offset: 0x0004F2F4
		public OtherSigningCertificate(OtherCertID otherCertID)
		{
			this.certs = new DerSequence(otherCertID);
		}

		// Token: 0x06000C49 RID: 3145 RVA: 0x0004F308 File Offset: 0x0004F308
		public OtherCertID[] GetCerts()
		{
			OtherCertID[] array = new OtherCertID[this.certs.Count];
			for (int num = 0; num != this.certs.Count; num++)
			{
				array[num] = OtherCertID.GetInstance(this.certs[num]);
			}
			return array;
		}

		// Token: 0x06000C4A RID: 3146 RVA: 0x0004F35C File Offset: 0x0004F35C
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

		// Token: 0x06000C4B RID: 3147 RVA: 0x0004F3C0 File Offset: 0x0004F3C0
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

		// Token: 0x04000837 RID: 2103
		private Asn1Sequence certs;

		// Token: 0x04000838 RID: 2104
		private Asn1Sequence policies;
	}
}
