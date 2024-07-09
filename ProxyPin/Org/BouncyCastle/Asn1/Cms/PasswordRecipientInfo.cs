using System;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x0200011A RID: 282
	public class PasswordRecipientInfo : Asn1Encodable
	{
		// Token: 0x06000A0F RID: 2575 RVA: 0x00047368 File Offset: 0x00047368
		public PasswordRecipientInfo(AlgorithmIdentifier keyEncryptionAlgorithm, Asn1OctetString encryptedKey)
		{
			this.version = new DerInteger(0);
			this.keyEncryptionAlgorithm = keyEncryptionAlgorithm;
			this.encryptedKey = encryptedKey;
		}

		// Token: 0x06000A10 RID: 2576 RVA: 0x0004738C File Offset: 0x0004738C
		public PasswordRecipientInfo(AlgorithmIdentifier keyDerivationAlgorithm, AlgorithmIdentifier keyEncryptionAlgorithm, Asn1OctetString encryptedKey)
		{
			this.version = new DerInteger(0);
			this.keyDerivationAlgorithm = keyDerivationAlgorithm;
			this.keyEncryptionAlgorithm = keyEncryptionAlgorithm;
			this.encryptedKey = encryptedKey;
		}

		// Token: 0x06000A11 RID: 2577 RVA: 0x000473B8 File Offset: 0x000473B8
		public PasswordRecipientInfo(Asn1Sequence seq)
		{
			this.version = (DerInteger)seq[0];
			if (seq[1] is Asn1TaggedObject)
			{
				this.keyDerivationAlgorithm = AlgorithmIdentifier.GetInstance((Asn1TaggedObject)seq[1], false);
				this.keyEncryptionAlgorithm = AlgorithmIdentifier.GetInstance(seq[2]);
				this.encryptedKey = (Asn1OctetString)seq[3];
				return;
			}
			this.keyEncryptionAlgorithm = AlgorithmIdentifier.GetInstance(seq[1]);
			this.encryptedKey = (Asn1OctetString)seq[2];
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x00047454 File Offset: 0x00047454
		public static PasswordRecipientInfo GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return PasswordRecipientInfo.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x06000A13 RID: 2579 RVA: 0x00047464 File Offset: 0x00047464
		public static PasswordRecipientInfo GetInstance(object obj)
		{
			if (obj == null || obj is PasswordRecipientInfo)
			{
				return (PasswordRecipientInfo)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new PasswordRecipientInfo((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid PasswordRecipientInfo: " + Platform.GetTypeName(obj));
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06000A14 RID: 2580 RVA: 0x000474BC File Offset: 0x000474BC
		public DerInteger Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000A15 RID: 2581 RVA: 0x000474C4 File Offset: 0x000474C4
		public AlgorithmIdentifier KeyDerivationAlgorithm
		{
			get
			{
				return this.keyDerivationAlgorithm;
			}
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000A16 RID: 2582 RVA: 0x000474CC File Offset: 0x000474CC
		public AlgorithmIdentifier KeyEncryptionAlgorithm
		{
			get
			{
				return this.keyEncryptionAlgorithm;
			}
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06000A17 RID: 2583 RVA: 0x000474D4 File Offset: 0x000474D4
		public Asn1OctetString EncryptedKey
		{
			get
			{
				return this.encryptedKey;
			}
		}

		// Token: 0x06000A18 RID: 2584 RVA: 0x000474DC File Offset: 0x000474DC
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.version
			});
			asn1EncodableVector.AddOptionalTagged(false, 0, this.keyDerivationAlgorithm);
			asn1EncodableVector.Add(new Asn1Encodable[]
			{
				this.keyEncryptionAlgorithm,
				this.encryptedKey
			});
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04000712 RID: 1810
		private readonly DerInteger version;

		// Token: 0x04000713 RID: 1811
		private readonly AlgorithmIdentifier keyDerivationAlgorithm;

		// Token: 0x04000714 RID: 1812
		private readonly AlgorithmIdentifier keyEncryptionAlgorithm;

		// Token: 0x04000715 RID: 1813
		private readonly Asn1OctetString encryptedKey;
	}
}
