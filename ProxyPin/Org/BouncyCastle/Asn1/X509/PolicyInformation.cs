using System;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x02000205 RID: 517
	public class PolicyInformation : Asn1Encodable
	{
		// Token: 0x060010AF RID: 4271 RVA: 0x00060DD0 File Offset: 0x00060DD0
		private PolicyInformation(Asn1Sequence seq)
		{
			if (seq.Count < 1 || seq.Count > 2)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count);
			}
			this.policyIdentifier = DerObjectIdentifier.GetInstance(seq[0]);
			if (seq.Count > 1)
			{
				this.policyQualifiers = Asn1Sequence.GetInstance(seq[1]);
			}
		}

		// Token: 0x060010B0 RID: 4272 RVA: 0x00060E4C File Offset: 0x00060E4C
		public PolicyInformation(DerObjectIdentifier policyIdentifier)
		{
			this.policyIdentifier = policyIdentifier;
		}

		// Token: 0x060010B1 RID: 4273 RVA: 0x00060E5C File Offset: 0x00060E5C
		public PolicyInformation(DerObjectIdentifier policyIdentifier, Asn1Sequence policyQualifiers)
		{
			this.policyIdentifier = policyIdentifier;
			this.policyQualifiers = policyQualifiers;
		}

		// Token: 0x060010B2 RID: 4274 RVA: 0x00060E74 File Offset: 0x00060E74
		public static PolicyInformation GetInstance(object obj)
		{
			if (obj == null || obj is PolicyInformation)
			{
				return (PolicyInformation)obj;
			}
			return new PolicyInformation(Asn1Sequence.GetInstance(obj));
		}

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x060010B3 RID: 4275 RVA: 0x00060E9C File Offset: 0x00060E9C
		public DerObjectIdentifier PolicyIdentifier
		{
			get
			{
				return this.policyIdentifier;
			}
		}

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x060010B4 RID: 4276 RVA: 0x00060EA4 File Offset: 0x00060EA4
		public Asn1Sequence PolicyQualifiers
		{
			get
			{
				return this.policyQualifiers;
			}
		}

		// Token: 0x060010B5 RID: 4277 RVA: 0x00060EAC File Offset: 0x00060EAC
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.policyIdentifier
			});
			asn1EncodableVector.AddOptional(new Asn1Encodable[]
			{
				this.policyQualifiers
			});
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04000C22 RID: 3106
		private readonly DerObjectIdentifier policyIdentifier;

		// Token: 0x04000C23 RID: 3107
		private readonly Asn1Sequence policyQualifiers;
	}
}
