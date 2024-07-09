using System;

namespace Org.BouncyCastle.Asn1.Pkcs
{
	// Token: 0x020001B2 RID: 434
	public class Pfx : Asn1Encodable
	{
		// Token: 0x06000E22 RID: 3618 RVA: 0x00056228 File Offset: 0x00056228
		public static Pfx GetInstance(object obj)
		{
			if (obj is Pfx)
			{
				return (Pfx)obj;
			}
			if (obj == null)
			{
				return null;
			}
			return new Pfx(Asn1Sequence.GetInstance(obj));
		}

		// Token: 0x06000E23 RID: 3619 RVA: 0x00056250 File Offset: 0x00056250
		[Obsolete("Use 'GetInstance' instead")]
		public Pfx(Asn1Sequence seq)
		{
			DerInteger instance = DerInteger.GetInstance(seq[0]);
			if (instance.IntValueExact != 3)
			{
				throw new ArgumentException("wrong version for PFX PDU");
			}
			this.contentInfo = ContentInfo.GetInstance(seq[1]);
			if (seq.Count == 3)
			{
				this.macData = MacData.GetInstance(seq[2]);
			}
		}

		// Token: 0x06000E24 RID: 3620 RVA: 0x000562BC File Offset: 0x000562BC
		public Pfx(ContentInfo contentInfo, MacData macData)
		{
			this.contentInfo = contentInfo;
			this.macData = macData;
		}

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06000E25 RID: 3621 RVA: 0x000562D4 File Offset: 0x000562D4
		public ContentInfo AuthSafe
		{
			get
			{
				return this.contentInfo;
			}
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06000E26 RID: 3622 RVA: 0x000562DC File Offset: 0x000562DC
		public MacData MacData
		{
			get
			{
				return this.macData;
			}
		}

		// Token: 0x06000E27 RID: 3623 RVA: 0x000562E4 File Offset: 0x000562E4
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				new DerInteger(3),
				this.contentInfo
			});
			asn1EncodableVector.AddOptional(new Asn1Encodable[]
			{
				this.macData
			});
			return new BerSequence(asn1EncodableVector);
		}

		// Token: 0x040009EF RID: 2543
		private readonly ContentInfo contentInfo;

		// Token: 0x040009F0 RID: 2544
		private readonly MacData macData;
	}
}
