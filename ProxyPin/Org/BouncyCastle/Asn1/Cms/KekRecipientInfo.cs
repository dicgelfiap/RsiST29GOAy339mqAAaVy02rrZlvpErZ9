using System;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x0200010F RID: 271
	public class KekRecipientInfo : Asn1Encodable
	{
		// Token: 0x060009B5 RID: 2485 RVA: 0x00046528 File Offset: 0x00046528
		public KekRecipientInfo(KekIdentifier kekID, AlgorithmIdentifier keyEncryptionAlgorithm, Asn1OctetString encryptedKey)
		{
			this.version = new DerInteger(4);
			this.kekID = kekID;
			this.keyEncryptionAlgorithm = keyEncryptionAlgorithm;
			this.encryptedKey = encryptedKey;
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x00046554 File Offset: 0x00046554
		public KekRecipientInfo(Asn1Sequence seq)
		{
			this.version = (DerInteger)seq[0];
			this.kekID = KekIdentifier.GetInstance(seq[1]);
			this.keyEncryptionAlgorithm = AlgorithmIdentifier.GetInstance(seq[2]);
			this.encryptedKey = (Asn1OctetString)seq[3];
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x000465B4 File Offset: 0x000465B4
		public static KekRecipientInfo GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return KekRecipientInfo.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x000465C4 File Offset: 0x000465C4
		public static KekRecipientInfo GetInstance(object obj)
		{
			if (obj == null || obj is KekRecipientInfo)
			{
				return (KekRecipientInfo)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new KekRecipientInfo((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid KekRecipientInfo: " + Platform.GetTypeName(obj));
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x060009B9 RID: 2489 RVA: 0x0004661C File Offset: 0x0004661C
		public DerInteger Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x060009BA RID: 2490 RVA: 0x00046624 File Offset: 0x00046624
		public KekIdentifier KekID
		{
			get
			{
				return this.kekID;
			}
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x060009BB RID: 2491 RVA: 0x0004662C File Offset: 0x0004662C
		public AlgorithmIdentifier KeyEncryptionAlgorithm
		{
			get
			{
				return this.keyEncryptionAlgorithm;
			}
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x060009BC RID: 2492 RVA: 0x00046634 File Offset: 0x00046634
		public Asn1OctetString EncryptedKey
		{
			get
			{
				return this.encryptedKey;
			}
		}

		// Token: 0x060009BD RID: 2493 RVA: 0x0004663C File Offset: 0x0004663C
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.version,
				this.kekID,
				this.keyEncryptionAlgorithm,
				this.encryptedKey
			});
		}

		// Token: 0x040006F4 RID: 1780
		private DerInteger version;

		// Token: 0x040006F5 RID: 1781
		private KekIdentifier kekID;

		// Token: 0x040006F6 RID: 1782
		private AlgorithmIdentifier keyEncryptionAlgorithm;

		// Token: 0x040006F7 RID: 1783
		private Asn1OctetString encryptedKey;
	}
}
