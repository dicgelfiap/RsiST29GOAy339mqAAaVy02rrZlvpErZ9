using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x0200011E RID: 286
	public class RecipientKeyIdentifier : Asn1Encodable
	{
		// Token: 0x06000A33 RID: 2611 RVA: 0x00047988 File Offset: 0x00047988
		public RecipientKeyIdentifier(Asn1OctetString subjectKeyIdentifier, DerGeneralizedTime date, OtherKeyAttribute other)
		{
			this.subjectKeyIdentifier = subjectKeyIdentifier;
			this.date = date;
			this.other = other;
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x000479A8 File Offset: 0x000479A8
		public RecipientKeyIdentifier(byte[] subjectKeyIdentifier) : this(subjectKeyIdentifier, null, null)
		{
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x000479B4 File Offset: 0x000479B4
		public RecipientKeyIdentifier(byte[] subjectKeyIdentifier, DerGeneralizedTime date, OtherKeyAttribute other)
		{
			this.subjectKeyIdentifier = new DerOctetString(subjectKeyIdentifier);
			this.date = date;
			this.other = other;
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x000479D8 File Offset: 0x000479D8
		public RecipientKeyIdentifier(Asn1Sequence seq)
		{
			this.subjectKeyIdentifier = Asn1OctetString.GetInstance(seq[0]);
			switch (seq.Count)
			{
			case 1:
				return;
			case 2:
				if (seq[1] is DerGeneralizedTime)
				{
					this.date = (DerGeneralizedTime)seq[1];
					return;
				}
				this.other = OtherKeyAttribute.GetInstance(seq[2]);
				return;
			case 3:
				this.date = (DerGeneralizedTime)seq[1];
				this.other = OtherKeyAttribute.GetInstance(seq[2]);
				return;
			default:
				throw new ArgumentException("Invalid RecipientKeyIdentifier");
			}
		}

		// Token: 0x06000A37 RID: 2615 RVA: 0x00047A88 File Offset: 0x00047A88
		public static RecipientKeyIdentifier GetInstance(Asn1TaggedObject ato, bool explicitly)
		{
			return RecipientKeyIdentifier.GetInstance(Asn1Sequence.GetInstance(ato, explicitly));
		}

		// Token: 0x06000A38 RID: 2616 RVA: 0x00047A98 File Offset: 0x00047A98
		public static RecipientKeyIdentifier GetInstance(object obj)
		{
			if (obj == null || obj is RecipientKeyIdentifier)
			{
				return (RecipientKeyIdentifier)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new RecipientKeyIdentifier((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid RecipientKeyIdentifier: " + Platform.GetTypeName(obj));
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000A39 RID: 2617 RVA: 0x00047AF0 File Offset: 0x00047AF0
		public Asn1OctetString SubjectKeyIdentifier
		{
			get
			{
				return this.subjectKeyIdentifier;
			}
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000A3A RID: 2618 RVA: 0x00047AF8 File Offset: 0x00047AF8
		public DerGeneralizedTime Date
		{
			get
			{
				return this.date;
			}
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000A3B RID: 2619 RVA: 0x00047B00 File Offset: 0x00047B00
		public OtherKeyAttribute OtherKeyAttribute
		{
			get
			{
				return this.other;
			}
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x00047B08 File Offset: 0x00047B08
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.subjectKeyIdentifier
			});
			asn1EncodableVector.AddOptional(new Asn1Encodable[]
			{
				this.date,
				this.other
			});
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x0400071A RID: 1818
		private Asn1OctetString subjectKeyIdentifier;

		// Token: 0x0400071B RID: 1819
		private DerGeneralizedTime date;

		// Token: 0x0400071C RID: 1820
		private OtherKeyAttribute other;
	}
}
