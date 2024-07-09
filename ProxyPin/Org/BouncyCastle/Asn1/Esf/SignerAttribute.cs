using System;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Esf
{
	// Token: 0x02000160 RID: 352
	public class SignerAttribute : Asn1Encodable
	{
		// Token: 0x06000C07 RID: 3079 RVA: 0x0004E5E0 File Offset: 0x0004E5E0
		public static SignerAttribute GetInstance(object obj)
		{
			if (obj == null || obj is SignerAttribute)
			{
				return (SignerAttribute)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new SignerAttribute(obj);
			}
			throw new ArgumentException("Unknown object in 'SignerAttribute' factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06000C08 RID: 3080 RVA: 0x0004E638 File Offset: 0x0004E638
		private SignerAttribute(object obj)
		{
			Asn1Sequence asn1Sequence = (Asn1Sequence)obj;
			DerTaggedObject derTaggedObject = (DerTaggedObject)asn1Sequence[0];
			if (derTaggedObject.TagNo == 0)
			{
				this.claimedAttributes = Asn1Sequence.GetInstance(derTaggedObject, true);
				return;
			}
			if (derTaggedObject.TagNo == 1)
			{
				this.certifiedAttributes = AttributeCertificate.GetInstance(derTaggedObject);
				return;
			}
			throw new ArgumentException("illegal tag.", "obj");
		}

		// Token: 0x06000C09 RID: 3081 RVA: 0x0004E6A4 File Offset: 0x0004E6A4
		public SignerAttribute(Asn1Sequence claimedAttributes)
		{
			this.claimedAttributes = claimedAttributes;
		}

		// Token: 0x06000C0A RID: 3082 RVA: 0x0004E6B4 File Offset: 0x0004E6B4
		public SignerAttribute(AttributeCertificate certifiedAttributes)
		{
			this.certifiedAttributes = certifiedAttributes;
		}

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06000C0B RID: 3083 RVA: 0x0004E6C4 File Offset: 0x0004E6C4
		public virtual Asn1Sequence ClaimedAttributes
		{
			get
			{
				return this.claimedAttributes;
			}
		}

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06000C0C RID: 3084 RVA: 0x0004E6CC File Offset: 0x0004E6CC
		public virtual AttributeCertificate CertifiedAttributes
		{
			get
			{
				return this.certifiedAttributes;
			}
		}

		// Token: 0x06000C0D RID: 3085 RVA: 0x0004E6D4 File Offset: 0x0004E6D4
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector();
			if (this.claimedAttributes != null)
			{
				asn1EncodableVector.Add(new DerTaggedObject(0, this.claimedAttributes));
			}
			else
			{
				asn1EncodableVector.Add(new DerTaggedObject(1, this.certifiedAttributes));
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04000825 RID: 2085
		private Asn1Sequence claimedAttributes;

		// Token: 0x04000826 RID: 2086
		private AttributeCertificate certifiedAttributes;
	}
}
