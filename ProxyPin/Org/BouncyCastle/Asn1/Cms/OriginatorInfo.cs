using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x02000115 RID: 277
	public class OriginatorInfo : Asn1Encodable
	{
		// Token: 0x060009ED RID: 2541 RVA: 0x00046E64 File Offset: 0x00046E64
		public OriginatorInfo(Asn1Set certs, Asn1Set crls)
		{
			this.certs = certs;
			this.crls = crls;
		}

		// Token: 0x060009EE RID: 2542 RVA: 0x00046E7C File Offset: 0x00046E7C
		public OriginatorInfo(Asn1Sequence seq)
		{
			switch (seq.Count)
			{
			case 0:
				return;
			case 1:
			{
				Asn1TaggedObject asn1TaggedObject = (Asn1TaggedObject)seq[0];
				switch (asn1TaggedObject.TagNo)
				{
				case 0:
					this.certs = Asn1Set.GetInstance(asn1TaggedObject, false);
					return;
				case 1:
					this.crls = Asn1Set.GetInstance(asn1TaggedObject, false);
					return;
				default:
					throw new ArgumentException("Bad tag in OriginatorInfo: " + asn1TaggedObject.TagNo);
				}
				break;
			}
			case 2:
				this.certs = Asn1Set.GetInstance((Asn1TaggedObject)seq[0], false);
				this.crls = Asn1Set.GetInstance((Asn1TaggedObject)seq[1], false);
				return;
			default:
				throw new ArgumentException("OriginatorInfo too big");
			}
		}

		// Token: 0x060009EF RID: 2543 RVA: 0x00046F4C File Offset: 0x00046F4C
		public static OriginatorInfo GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return OriginatorInfo.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x060009F0 RID: 2544 RVA: 0x00046F5C File Offset: 0x00046F5C
		public static OriginatorInfo GetInstance(object obj)
		{
			if (obj == null || obj is OriginatorInfo)
			{
				return (OriginatorInfo)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new OriginatorInfo((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid OriginatorInfo: " + Platform.GetTypeName(obj));
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x060009F1 RID: 2545 RVA: 0x00046FB4 File Offset: 0x00046FB4
		public Asn1Set Certificates
		{
			get
			{
				return this.certs;
			}
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x060009F2 RID: 2546 RVA: 0x00046FBC File Offset: 0x00046FBC
		public Asn1Set Crls
		{
			get
			{
				return this.crls;
			}
		}

		// Token: 0x060009F3 RID: 2547 RVA: 0x00046FC4 File Offset: 0x00046FC4
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector();
			asn1EncodableVector.AddOptionalTagged(false, 0, this.certs);
			asn1EncodableVector.AddOptionalTagged(false, 1, this.crls);
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04000708 RID: 1800
		private Asn1Set certs;

		// Token: 0x04000709 RID: 1801
		private Asn1Set crls;
	}
}
