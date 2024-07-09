using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Esf
{
	// Token: 0x02000162 RID: 354
	public class SigPolicyQualifierInfo : Asn1Encodable
	{
		// Token: 0x06000C1A RID: 3098 RVA: 0x0004E99C File Offset: 0x0004E99C
		public static SigPolicyQualifierInfo GetInstance(object obj)
		{
			if (obj == null || obj is SigPolicyQualifierInfo)
			{
				return (SigPolicyQualifierInfo)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new SigPolicyQualifierInfo((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in 'SigPolicyQualifierInfo' factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06000C1B RID: 3099 RVA: 0x0004E9F8 File Offset: 0x0004E9F8
		private SigPolicyQualifierInfo(Asn1Sequence seq)
		{
			if (seq == null)
			{
				throw new ArgumentNullException("seq");
			}
			if (seq.Count != 2)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count, "seq");
			}
			this.sigPolicyQualifierId = (DerObjectIdentifier)seq[0].ToAsn1Object();
			this.sigQualifier = seq[1].ToAsn1Object();
		}

		// Token: 0x06000C1C RID: 3100 RVA: 0x0004EA78 File Offset: 0x0004EA78
		public SigPolicyQualifierInfo(DerObjectIdentifier sigPolicyQualifierId, Asn1Encodable sigQualifier)
		{
			this.sigPolicyQualifierId = sigPolicyQualifierId;
			this.sigQualifier = sigQualifier.ToAsn1Object();
		}

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06000C1D RID: 3101 RVA: 0x0004EA94 File Offset: 0x0004EA94
		public DerObjectIdentifier SigPolicyQualifierId
		{
			get
			{
				return this.sigPolicyQualifierId;
			}
		}

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06000C1E RID: 3102 RVA: 0x0004EA9C File Offset: 0x0004EA9C
		public Asn1Object SigQualifier
		{
			get
			{
				return this.sigQualifier;
			}
		}

		// Token: 0x06000C1F RID: 3103 RVA: 0x0004EAA4 File Offset: 0x0004EAA4
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.sigPolicyQualifierId,
				this.sigQualifier
			});
		}

		// Token: 0x0400082A RID: 2090
		private readonly DerObjectIdentifier sigPolicyQualifierId;

		// Token: 0x0400082B RID: 2091
		private readonly Asn1Object sigQualifier;
	}
}
