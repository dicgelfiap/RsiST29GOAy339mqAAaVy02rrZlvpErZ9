using System;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x02000112 RID: 274
	public class KeyTransRecipientInfo : Asn1Encodable
	{
		// Token: 0x060009CF RID: 2511 RVA: 0x00046964 File Offset: 0x00046964
		public KeyTransRecipientInfo(RecipientIdentifier rid, AlgorithmIdentifier keyEncryptionAlgorithm, Asn1OctetString encryptedKey)
		{
			if (rid.ToAsn1Object() is Asn1TaggedObject)
			{
				this.version = new DerInteger(2);
			}
			else
			{
				this.version = new DerInteger(0);
			}
			this.rid = rid;
			this.keyEncryptionAlgorithm = keyEncryptionAlgorithm;
			this.encryptedKey = encryptedKey;
		}

		// Token: 0x060009D0 RID: 2512 RVA: 0x000469C0 File Offset: 0x000469C0
		public KeyTransRecipientInfo(Asn1Sequence seq)
		{
			this.version = (DerInteger)seq[0];
			this.rid = RecipientIdentifier.GetInstance(seq[1]);
			this.keyEncryptionAlgorithm = AlgorithmIdentifier.GetInstance(seq[2]);
			this.encryptedKey = (Asn1OctetString)seq[3];
		}

		// Token: 0x060009D1 RID: 2513 RVA: 0x00046A20 File Offset: 0x00046A20
		public static KeyTransRecipientInfo GetInstance(object obj)
		{
			if (obj == null || obj is KeyTransRecipientInfo)
			{
				return (KeyTransRecipientInfo)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new KeyTransRecipientInfo((Asn1Sequence)obj);
			}
			throw new ArgumentException("Illegal object in KeyTransRecipientInfo: " + Platform.GetTypeName(obj));
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x060009D2 RID: 2514 RVA: 0x00046A78 File Offset: 0x00046A78
		public DerInteger Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x060009D3 RID: 2515 RVA: 0x00046A80 File Offset: 0x00046A80
		public RecipientIdentifier RecipientIdentifier
		{
			get
			{
				return this.rid;
			}
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x060009D4 RID: 2516 RVA: 0x00046A88 File Offset: 0x00046A88
		public AlgorithmIdentifier KeyEncryptionAlgorithm
		{
			get
			{
				return this.keyEncryptionAlgorithm;
			}
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x060009D5 RID: 2517 RVA: 0x00046A90 File Offset: 0x00046A90
		public Asn1OctetString EncryptedKey
		{
			get
			{
				return this.encryptedKey;
			}
		}

		// Token: 0x060009D6 RID: 2518 RVA: 0x00046A98 File Offset: 0x00046A98
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.version,
				this.rid,
				this.keyEncryptionAlgorithm,
				this.encryptedKey
			});
		}

		// Token: 0x040006FF RID: 1791
		private DerInteger version;

		// Token: 0x04000700 RID: 1792
		private RecipientIdentifier rid;

		// Token: 0x04000701 RID: 1793
		private AlgorithmIdentifier keyEncryptionAlgorithm;

		// Token: 0x04000702 RID: 1794
		private Asn1OctetString encryptedKey;
	}
}
