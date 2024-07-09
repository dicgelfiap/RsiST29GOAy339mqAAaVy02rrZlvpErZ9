using System;
using System.Collections;

namespace Org.BouncyCastle.Asn1.X9
{
	// Token: 0x0200022C RID: 556
	public class OtherInfo : Asn1Encodable
	{
		// Token: 0x060011FC RID: 4604 RVA: 0x000661F0 File Offset: 0x000661F0
		public OtherInfo(KeySpecificInfo keyInfo, Asn1OctetString partyAInfo, Asn1OctetString suppPubInfo)
		{
			this.keyInfo = keyInfo;
			this.partyAInfo = partyAInfo;
			this.suppPubInfo = suppPubInfo;
		}

		// Token: 0x060011FD RID: 4605 RVA: 0x00066210 File Offset: 0x00066210
		public OtherInfo(Asn1Sequence seq)
		{
			IEnumerator enumerator = seq.GetEnumerator();
			enumerator.MoveNext();
			this.keyInfo = new KeySpecificInfo((Asn1Sequence)enumerator.Current);
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				DerTaggedObject derTaggedObject = (DerTaggedObject)obj;
				if (derTaggedObject.TagNo == 0)
				{
					this.partyAInfo = (Asn1OctetString)derTaggedObject.GetObject();
				}
				else if (derTaggedObject.TagNo == 2)
				{
					this.suppPubInfo = (Asn1OctetString)derTaggedObject.GetObject();
				}
			}
		}

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x060011FE RID: 4606 RVA: 0x000662A4 File Offset: 0x000662A4
		public KeySpecificInfo KeyInfo
		{
			get
			{
				return this.keyInfo;
			}
		}

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x060011FF RID: 4607 RVA: 0x000662AC File Offset: 0x000662AC
		public Asn1OctetString PartyAInfo
		{
			get
			{
				return this.partyAInfo;
			}
		}

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x06001200 RID: 4608 RVA: 0x000662B4 File Offset: 0x000662B4
		public Asn1OctetString SuppPubInfo
		{
			get
			{
				return this.suppPubInfo;
			}
		}

		// Token: 0x06001201 RID: 4609 RVA: 0x000662BC File Offset: 0x000662BC
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.keyInfo
			});
			asn1EncodableVector.AddOptionalTagged(true, 0, this.partyAInfo);
			asn1EncodableVector.Add(new DerTaggedObject(2, this.suppPubInfo));
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04000CFE RID: 3326
		private KeySpecificInfo keyInfo;

		// Token: 0x04000CFF RID: 3327
		private Asn1OctetString partyAInfo;

		// Token: 0x04000D00 RID: 3328
		private Asn1OctetString suppPubInfo;
	}
}
