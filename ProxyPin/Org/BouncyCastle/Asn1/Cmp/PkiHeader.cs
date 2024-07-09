using System;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Cmp
{
	// Token: 0x020000E8 RID: 232
	public class PkiHeader : Asn1Encodable
	{
		// Token: 0x06000891 RID: 2193 RVA: 0x00042B48 File Offset: 0x00042B48
		private PkiHeader(Asn1Sequence seq)
		{
			this.pvno = DerInteger.GetInstance(seq[0]);
			this.sender = GeneralName.GetInstance(seq[1]);
			this.recipient = GeneralName.GetInstance(seq[2]);
			for (int i = 3; i < seq.Count; i++)
			{
				Asn1TaggedObject asn1TaggedObject = (Asn1TaggedObject)seq[i];
				switch (asn1TaggedObject.TagNo)
				{
				case 0:
					this.messageTime = DerGeneralizedTime.GetInstance(asn1TaggedObject, true);
					break;
				case 1:
					this.protectionAlg = AlgorithmIdentifier.GetInstance(asn1TaggedObject, true);
					break;
				case 2:
					this.senderKID = Asn1OctetString.GetInstance(asn1TaggedObject, true);
					break;
				case 3:
					this.recipKID = Asn1OctetString.GetInstance(asn1TaggedObject, true);
					break;
				case 4:
					this.transactionID = Asn1OctetString.GetInstance(asn1TaggedObject, true);
					break;
				case 5:
					this.senderNonce = Asn1OctetString.GetInstance(asn1TaggedObject, true);
					break;
				case 6:
					this.recipNonce = Asn1OctetString.GetInstance(asn1TaggedObject, true);
					break;
				case 7:
					this.freeText = PkiFreeText.GetInstance(asn1TaggedObject, true);
					break;
				case 8:
					this.generalInfo = Asn1Sequence.GetInstance(asn1TaggedObject, true);
					break;
				default:
					throw new ArgumentException("unknown tag number: " + asn1TaggedObject.TagNo, "seq");
				}
			}
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x00042CB4 File Offset: 0x00042CB4
		public static PkiHeader GetInstance(object obj)
		{
			if (obj is PkiHeader)
			{
				return (PkiHeader)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new PkiHeader((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid object: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x00042D08 File Offset: 0x00042D08
		public PkiHeader(int pvno, GeneralName sender, GeneralName recipient) : this(new DerInteger(pvno), sender, recipient)
		{
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x00042D18 File Offset: 0x00042D18
		private PkiHeader(DerInteger pvno, GeneralName sender, GeneralName recipient)
		{
			this.pvno = pvno;
			this.sender = sender;
			this.recipient = recipient;
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000895 RID: 2197 RVA: 0x00042D38 File Offset: 0x00042D38
		public virtual DerInteger Pvno
		{
			get
			{
				return this.pvno;
			}
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06000896 RID: 2198 RVA: 0x00042D40 File Offset: 0x00042D40
		public virtual GeneralName Sender
		{
			get
			{
				return this.sender;
			}
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06000897 RID: 2199 RVA: 0x00042D48 File Offset: 0x00042D48
		public virtual GeneralName Recipient
		{
			get
			{
				return this.recipient;
			}
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000898 RID: 2200 RVA: 0x00042D50 File Offset: 0x00042D50
		public virtual DerGeneralizedTime MessageTime
		{
			get
			{
				return this.messageTime;
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000899 RID: 2201 RVA: 0x00042D58 File Offset: 0x00042D58
		public virtual AlgorithmIdentifier ProtectionAlg
		{
			get
			{
				return this.protectionAlg;
			}
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x0600089A RID: 2202 RVA: 0x00042D60 File Offset: 0x00042D60
		public virtual Asn1OctetString SenderKID
		{
			get
			{
				return this.senderKID;
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x0600089B RID: 2203 RVA: 0x00042D68 File Offset: 0x00042D68
		public virtual Asn1OctetString RecipKID
		{
			get
			{
				return this.recipKID;
			}
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x0600089C RID: 2204 RVA: 0x00042D70 File Offset: 0x00042D70
		public virtual Asn1OctetString TransactionID
		{
			get
			{
				return this.transactionID;
			}
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x0600089D RID: 2205 RVA: 0x00042D78 File Offset: 0x00042D78
		public virtual Asn1OctetString SenderNonce
		{
			get
			{
				return this.senderNonce;
			}
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x0600089E RID: 2206 RVA: 0x00042D80 File Offset: 0x00042D80
		public virtual Asn1OctetString RecipNonce
		{
			get
			{
				return this.recipNonce;
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x0600089F RID: 2207 RVA: 0x00042D88 File Offset: 0x00042D88
		public virtual PkiFreeText FreeText
		{
			get
			{
				return this.freeText;
			}
		}

		// Token: 0x060008A0 RID: 2208 RVA: 0x00042D90 File Offset: 0x00042D90
		public virtual InfoTypeAndValue[] GetGeneralInfo()
		{
			if (this.generalInfo == null)
			{
				return null;
			}
			InfoTypeAndValue[] array = new InfoTypeAndValue[this.generalInfo.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = InfoTypeAndValue.GetInstance(this.generalInfo[i]);
			}
			return array;
		}

		// Token: 0x060008A1 RID: 2209 RVA: 0x00042DEC File Offset: 0x00042DEC
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.pvno,
				this.sender,
				this.recipient
			});
			asn1EncodableVector.AddOptionalTagged(true, 0, this.messageTime);
			asn1EncodableVector.AddOptionalTagged(true, 1, this.protectionAlg);
			asn1EncodableVector.AddOptionalTagged(true, 2, this.senderKID);
			asn1EncodableVector.AddOptionalTagged(true, 3, this.recipKID);
			asn1EncodableVector.AddOptionalTagged(true, 4, this.transactionID);
			asn1EncodableVector.AddOptionalTagged(true, 5, this.senderNonce);
			asn1EncodableVector.AddOptionalTagged(true, 6, this.recipNonce);
			asn1EncodableVector.AddOptionalTagged(true, 7, this.freeText);
			asn1EncodableVector.AddOptionalTagged(true, 8, this.generalInfo);
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04000658 RID: 1624
		public static readonly GeneralName NULL_NAME = new GeneralName(X509Name.GetInstance(new DerSequence()));

		// Token: 0x04000659 RID: 1625
		public static readonly int CMP_1999 = 1;

		// Token: 0x0400065A RID: 1626
		public static readonly int CMP_2000 = 2;

		// Token: 0x0400065B RID: 1627
		private readonly DerInteger pvno;

		// Token: 0x0400065C RID: 1628
		private readonly GeneralName sender;

		// Token: 0x0400065D RID: 1629
		private readonly GeneralName recipient;

		// Token: 0x0400065E RID: 1630
		private readonly DerGeneralizedTime messageTime;

		// Token: 0x0400065F RID: 1631
		private readonly AlgorithmIdentifier protectionAlg;

		// Token: 0x04000660 RID: 1632
		private readonly Asn1OctetString senderKID;

		// Token: 0x04000661 RID: 1633
		private readonly Asn1OctetString recipKID;

		// Token: 0x04000662 RID: 1634
		private readonly Asn1OctetString transactionID;

		// Token: 0x04000663 RID: 1635
		private readonly Asn1OctetString senderNonce;

		// Token: 0x04000664 RID: 1636
		private readonly Asn1OctetString recipNonce;

		// Token: 0x04000665 RID: 1637
		private readonly PkiFreeText freeText;

		// Token: 0x04000666 RID: 1638
		private readonly Asn1Sequence generalInfo;
	}
}
