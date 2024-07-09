using System;
using System.Collections;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Collections;

namespace Org.BouncyCastle.Asn1.Esf
{
	// Token: 0x0200015C RID: 348
	public class OtherSigningCertificate : Asn1Encodable
	{
		// Token: 0x06000BE8 RID: 3048 RVA: 0x0004DB58 File Offset: 0x0004DB58
		public static OtherSigningCertificate GetInstance(object obj)
		{
			if (obj == null || obj is OtherSigningCertificate)
			{
				return (OtherSigningCertificate)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new OtherSigningCertificate((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in 'OtherSigningCertificate' factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06000BE9 RID: 3049 RVA: 0x0004DBB4 File Offset: 0x0004DBB4
		private OtherSigningCertificate(Asn1Sequence seq)
		{
			if (seq == null)
			{
				throw new ArgumentNullException("seq");
			}
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

		// Token: 0x06000BEA RID: 3050 RVA: 0x0004DC50 File Offset: 0x0004DC50
		public OtherSigningCertificate(params OtherCertID[] certs) : this(certs, null)
		{
		}

		// Token: 0x06000BEB RID: 3051 RVA: 0x0004DC5C File Offset: 0x0004DC5C
		public OtherSigningCertificate(OtherCertID[] certs, params PolicyInformation[] policies)
		{
			if (certs == null)
			{
				throw new ArgumentNullException("certs");
			}
			this.certs = new DerSequence(certs);
			if (policies != null)
			{
				this.policies = new DerSequence(policies);
			}
		}

		// Token: 0x06000BEC RID: 3052 RVA: 0x0004DC94 File Offset: 0x0004DC94
		public OtherSigningCertificate(IEnumerable certs) : this(certs, null)
		{
		}

		// Token: 0x06000BED RID: 3053 RVA: 0x0004DCA0 File Offset: 0x0004DCA0
		public OtherSigningCertificate(IEnumerable certs, IEnumerable policies)
		{
			if (certs == null)
			{
				throw new ArgumentNullException("certs");
			}
			if (!CollectionUtilities.CheckElementsAreOfType(certs, typeof(OtherCertID)))
			{
				throw new ArgumentException("Must contain only 'OtherCertID' objects", "certs");
			}
			this.certs = new DerSequence(Asn1EncodableVector.FromEnumerable(certs));
			if (policies != null)
			{
				if (!CollectionUtilities.CheckElementsAreOfType(policies, typeof(PolicyInformation)))
				{
					throw new ArgumentException("Must contain only 'PolicyInformation' objects", "policies");
				}
				this.policies = new DerSequence(Asn1EncodableVector.FromEnumerable(policies));
			}
		}

		// Token: 0x06000BEE RID: 3054 RVA: 0x0004DD3C File Offset: 0x0004DD3C
		public OtherCertID[] GetCerts()
		{
			OtherCertID[] array = new OtherCertID[this.certs.Count];
			for (int i = 0; i < this.certs.Count; i++)
			{
				array[i] = OtherCertID.GetInstance(this.certs[i].ToAsn1Object());
			}
			return array;
		}

		// Token: 0x06000BEF RID: 3055 RVA: 0x0004DD98 File Offset: 0x0004DD98
		public PolicyInformation[] GetPolicies()
		{
			if (this.policies == null)
			{
				return null;
			}
			PolicyInformation[] array = new PolicyInformation[this.policies.Count];
			for (int i = 0; i < this.policies.Count; i++)
			{
				array[i] = PolicyInformation.GetInstance(this.policies[i].ToAsn1Object());
			}
			return array;
		}

		// Token: 0x06000BF0 RID: 3056 RVA: 0x0004DE00 File Offset: 0x0004DE00
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

		// Token: 0x0400081C RID: 2076
		private readonly Asn1Sequence certs;

		// Token: 0x0400081D RID: 2077
		private readonly Asn1Sequence policies;
	}
}
