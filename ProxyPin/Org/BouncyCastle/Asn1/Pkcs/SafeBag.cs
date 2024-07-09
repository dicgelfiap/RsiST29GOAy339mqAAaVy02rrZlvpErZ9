using System;

namespace Org.BouncyCastle.Asn1.Pkcs
{
	// Token: 0x020001BA RID: 442
	public class SafeBag : Asn1Encodable
	{
		// Token: 0x06000E65 RID: 3685 RVA: 0x00057858 File Offset: 0x00057858
		public static SafeBag GetInstance(object obj)
		{
			if (obj is SafeBag)
			{
				return (SafeBag)obj;
			}
			if (obj == null)
			{
				return null;
			}
			return new SafeBag(Asn1Sequence.GetInstance(obj));
		}

		// Token: 0x06000E66 RID: 3686 RVA: 0x00057880 File Offset: 0x00057880
		public SafeBag(DerObjectIdentifier oid, Asn1Object obj)
		{
			this.bagID = oid;
			this.bagValue = obj;
			this.bagAttributes = null;
		}

		// Token: 0x06000E67 RID: 3687 RVA: 0x000578A0 File Offset: 0x000578A0
		public SafeBag(DerObjectIdentifier oid, Asn1Object obj, Asn1Set bagAttributes)
		{
			this.bagID = oid;
			this.bagValue = obj;
			this.bagAttributes = bagAttributes;
		}

		// Token: 0x06000E68 RID: 3688 RVA: 0x000578C0 File Offset: 0x000578C0
		[Obsolete("Use 'GetInstance' instead")]
		public SafeBag(Asn1Sequence seq)
		{
			this.bagID = (DerObjectIdentifier)seq[0];
			this.bagValue = ((DerTaggedObject)seq[1]).GetObject();
			if (seq.Count == 3)
			{
				this.bagAttributes = (Asn1Set)seq[2];
			}
		}

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06000E69 RID: 3689 RVA: 0x00057920 File Offset: 0x00057920
		public DerObjectIdentifier BagID
		{
			get
			{
				return this.bagID;
			}
		}

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06000E6A RID: 3690 RVA: 0x00057928 File Offset: 0x00057928
		public Asn1Object BagValue
		{
			get
			{
				return this.bagValue;
			}
		}

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06000E6B RID: 3691 RVA: 0x00057930 File Offset: 0x00057930
		public Asn1Set BagAttributes
		{
			get
			{
				return this.bagAttributes;
			}
		}

		// Token: 0x06000E6C RID: 3692 RVA: 0x00057938 File Offset: 0x00057938
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.bagID,
				new DerTaggedObject(0, this.bagValue)
			});
			asn1EncodableVector.AddOptional(new Asn1Encodable[]
			{
				this.bagAttributes
			});
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04000AA2 RID: 2722
		private readonly DerObjectIdentifier bagID;

		// Token: 0x04000AA3 RID: 2723
		private readonly Asn1Object bagValue;

		// Token: 0x04000AA4 RID: 2724
		private readonly Asn1Set bagAttributes;
	}
}
