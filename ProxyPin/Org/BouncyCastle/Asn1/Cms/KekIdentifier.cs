using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x0200010E RID: 270
	public class KekIdentifier : Asn1Encodable
	{
		// Token: 0x060009AD RID: 2477 RVA: 0x00046378 File Offset: 0x00046378
		public KekIdentifier(byte[] keyIdentifier, DerGeneralizedTime date, OtherKeyAttribute other)
		{
			this.keyIdentifier = new DerOctetString(keyIdentifier);
			this.date = date;
			this.other = other;
		}

		// Token: 0x060009AE RID: 2478 RVA: 0x0004639C File Offset: 0x0004639C
		public KekIdentifier(Asn1Sequence seq)
		{
			this.keyIdentifier = (Asn1OctetString)seq[0];
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
				throw new ArgumentException("Invalid KekIdentifier");
			}
		}

		// Token: 0x060009AF RID: 2479 RVA: 0x0004644C File Offset: 0x0004644C
		public static KekIdentifier GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return KekIdentifier.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x060009B0 RID: 2480 RVA: 0x0004645C File Offset: 0x0004645C
		public static KekIdentifier GetInstance(object obj)
		{
			if (obj == null || obj is KekIdentifier)
			{
				return (KekIdentifier)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new KekIdentifier((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid KekIdentifier: " + Platform.GetTypeName(obj));
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x060009B1 RID: 2481 RVA: 0x000464B4 File Offset: 0x000464B4
		public Asn1OctetString KeyIdentifier
		{
			get
			{
				return this.keyIdentifier;
			}
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x060009B2 RID: 2482 RVA: 0x000464BC File Offset: 0x000464BC
		public DerGeneralizedTime Date
		{
			get
			{
				return this.date;
			}
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x060009B3 RID: 2483 RVA: 0x000464C4 File Offset: 0x000464C4
		public OtherKeyAttribute Other
		{
			get
			{
				return this.other;
			}
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x000464CC File Offset: 0x000464CC
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.keyIdentifier
			});
			asn1EncodableVector.AddOptional(new Asn1Encodable[]
			{
				this.date,
				this.other
			});
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x040006F1 RID: 1777
		private Asn1OctetString keyIdentifier;

		// Token: 0x040006F2 RID: 1778
		private DerGeneralizedTime date;

		// Token: 0x040006F3 RID: 1779
		private OtherKeyAttribute other;
	}
}
