using System;

namespace Org.BouncyCastle.Asn1.Ocsp
{
	// Token: 0x02000190 RID: 400
	public class CrlID : Asn1Encodable
	{
		// Token: 0x06000D31 RID: 3377 RVA: 0x0005357C File Offset: 0x0005357C
		public CrlID(Asn1Sequence seq)
		{
			foreach (object obj in seq)
			{
				Asn1TaggedObject asn1TaggedObject = (Asn1TaggedObject)obj;
				switch (asn1TaggedObject.TagNo)
				{
				case 0:
					this.crlUrl = DerIA5String.GetInstance(asn1TaggedObject, true);
					break;
				case 1:
					this.crlNum = DerInteger.GetInstance(asn1TaggedObject, true);
					break;
				case 2:
					this.crlTime = DerGeneralizedTime.GetInstance(asn1TaggedObject, true);
					break;
				default:
					throw new ArgumentException("unknown tag number: " + asn1TaggedObject.TagNo);
				}
			}
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06000D32 RID: 3378 RVA: 0x0005364C File Offset: 0x0005364C
		public DerIA5String CrlUrl
		{
			get
			{
				return this.crlUrl;
			}
		}

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06000D33 RID: 3379 RVA: 0x00053654 File Offset: 0x00053654
		public DerInteger CrlNum
		{
			get
			{
				return this.crlNum;
			}
		}

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06000D34 RID: 3380 RVA: 0x0005365C File Offset: 0x0005365C
		public DerGeneralizedTime CrlTime
		{
			get
			{
				return this.crlTime;
			}
		}

		// Token: 0x06000D35 RID: 3381 RVA: 0x00053664 File Offset: 0x00053664
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector();
			asn1EncodableVector.AddOptionalTagged(true, 0, this.crlUrl);
			asn1EncodableVector.AddOptionalTagged(true, 1, this.crlNum);
			asn1EncodableVector.AddOptionalTagged(true, 2, this.crlTime);
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04000985 RID: 2437
		private readonly DerIA5String crlUrl;

		// Token: 0x04000986 RID: 2438
		private readonly DerInteger crlNum;

		// Token: 0x04000987 RID: 2439
		private readonly DerGeneralizedTime crlTime;
	}
}
