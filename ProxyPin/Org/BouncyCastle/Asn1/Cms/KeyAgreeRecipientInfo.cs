using System;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x02000111 RID: 273
	public class KeyAgreeRecipientInfo : Asn1Encodable
	{
		// Token: 0x060009C5 RID: 2501 RVA: 0x00046780 File Offset: 0x00046780
		public KeyAgreeRecipientInfo(OriginatorIdentifierOrKey originator, Asn1OctetString ukm, AlgorithmIdentifier keyEncryptionAlgorithm, Asn1Sequence recipientEncryptedKeys)
		{
			this.version = new DerInteger(3);
			this.originator = originator;
			this.ukm = ukm;
			this.keyEncryptionAlgorithm = keyEncryptionAlgorithm;
			this.recipientEncryptedKeys = recipientEncryptedKeys;
		}

		// Token: 0x060009C6 RID: 2502 RVA: 0x000467B4 File Offset: 0x000467B4
		public KeyAgreeRecipientInfo(Asn1Sequence seq)
		{
			int index = 0;
			this.version = (DerInteger)seq[index++];
			this.originator = OriginatorIdentifierOrKey.GetInstance((Asn1TaggedObject)seq[index++], true);
			if (seq[index] is Asn1TaggedObject)
			{
				this.ukm = Asn1OctetString.GetInstance((Asn1TaggedObject)seq[index++], true);
			}
			this.keyEncryptionAlgorithm = AlgorithmIdentifier.GetInstance(seq[index++]);
			this.recipientEncryptedKeys = (Asn1Sequence)seq[index++];
		}

		// Token: 0x060009C7 RID: 2503 RVA: 0x00046858 File Offset: 0x00046858
		public static KeyAgreeRecipientInfo GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return KeyAgreeRecipientInfo.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x060009C8 RID: 2504 RVA: 0x00046868 File Offset: 0x00046868
		public static KeyAgreeRecipientInfo GetInstance(object obj)
		{
			if (obj == null || obj is KeyAgreeRecipientInfo)
			{
				return (KeyAgreeRecipientInfo)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new KeyAgreeRecipientInfo((Asn1Sequence)obj);
			}
			throw new ArgumentException("Illegal object in KeyAgreeRecipientInfo: " + Platform.GetTypeName(obj));
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x060009C9 RID: 2505 RVA: 0x000468C0 File Offset: 0x000468C0
		public DerInteger Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x060009CA RID: 2506 RVA: 0x000468C8 File Offset: 0x000468C8
		public OriginatorIdentifierOrKey Originator
		{
			get
			{
				return this.originator;
			}
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x060009CB RID: 2507 RVA: 0x000468D0 File Offset: 0x000468D0
		public Asn1OctetString UserKeyingMaterial
		{
			get
			{
				return this.ukm;
			}
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x060009CC RID: 2508 RVA: 0x000468D8 File Offset: 0x000468D8
		public AlgorithmIdentifier KeyEncryptionAlgorithm
		{
			get
			{
				return this.keyEncryptionAlgorithm;
			}
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x060009CD RID: 2509 RVA: 0x000468E0 File Offset: 0x000468E0
		public Asn1Sequence RecipientEncryptedKeys
		{
			get
			{
				return this.recipientEncryptedKeys;
			}
		}

		// Token: 0x060009CE RID: 2510 RVA: 0x000468E8 File Offset: 0x000468E8
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.version,
				new DerTaggedObject(true, 0, this.originator)
			});
			asn1EncodableVector.AddOptionalTagged(true, 1, this.ukm);
			asn1EncodableVector.Add(new Asn1Encodable[]
			{
				this.keyEncryptionAlgorithm,
				this.recipientEncryptedKeys
			});
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x040006FA RID: 1786
		private DerInteger version;

		// Token: 0x040006FB RID: 1787
		private OriginatorIdentifierOrKey originator;

		// Token: 0x040006FC RID: 1788
		private Asn1OctetString ukm;

		// Token: 0x040006FD RID: 1789
		private AlgorithmIdentifier keyEncryptionAlgorithm;

		// Token: 0x040006FE RID: 1790
		private Asn1Sequence recipientEncryptedKeys;
	}
}
