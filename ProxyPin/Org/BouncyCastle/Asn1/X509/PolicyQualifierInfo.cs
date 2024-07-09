using System;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x02000208 RID: 520
	public class PolicyQualifierInfo : Asn1Encodable
	{
		// Token: 0x060010BC RID: 4284 RVA: 0x00061000 File Offset: 0x00061000
		public PolicyQualifierInfo(DerObjectIdentifier policyQualifierId, Asn1Encodable qualifier)
		{
			this.policyQualifierId = policyQualifierId;
			this.qualifier = qualifier;
		}

		// Token: 0x060010BD RID: 4285 RVA: 0x00061018 File Offset: 0x00061018
		public PolicyQualifierInfo(string cps)
		{
			this.policyQualifierId = PolicyQualifierID.IdQtCps;
			this.qualifier = new DerIA5String(cps);
		}

		// Token: 0x060010BE RID: 4286 RVA: 0x00061038 File Offset: 0x00061038
		private PolicyQualifierInfo(Asn1Sequence seq)
		{
			if (seq.Count != 2)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count, "seq");
			}
			this.policyQualifierId = DerObjectIdentifier.GetInstance(seq[0]);
			this.qualifier = seq[1];
		}

		// Token: 0x060010BF RID: 4287 RVA: 0x0006109C File Offset: 0x0006109C
		public static PolicyQualifierInfo GetInstance(object obj)
		{
			if (obj is PolicyQualifierInfo)
			{
				return (PolicyQualifierInfo)obj;
			}
			if (obj == null)
			{
				return null;
			}
			return new PolicyQualifierInfo(Asn1Sequence.GetInstance(obj));
		}

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x060010C0 RID: 4288 RVA: 0x000610C4 File Offset: 0x000610C4
		public virtual DerObjectIdentifier PolicyQualifierId
		{
			get
			{
				return this.policyQualifierId;
			}
		}

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x060010C1 RID: 4289 RVA: 0x000610CC File Offset: 0x000610CC
		public virtual Asn1Encodable Qualifier
		{
			get
			{
				return this.qualifier;
			}
		}

		// Token: 0x060010C2 RID: 4290 RVA: 0x000610D4 File Offset: 0x000610D4
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.policyQualifierId,
				this.qualifier
			});
		}

		// Token: 0x04000C28 RID: 3112
		private readonly DerObjectIdentifier policyQualifierId;

		// Token: 0x04000C29 RID: 3113
		private readonly Asn1Encodable qualifier;
	}
}
