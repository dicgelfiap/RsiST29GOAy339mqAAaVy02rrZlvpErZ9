using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x0200011B RID: 283
	public class RecipientEncryptedKey : Asn1Encodable
	{
		// Token: 0x06000A19 RID: 2585 RVA: 0x00047544 File Offset: 0x00047544
		private RecipientEncryptedKey(Asn1Sequence seq)
		{
			this.identifier = KeyAgreeRecipientIdentifier.GetInstance(seq[0]);
			this.encryptedKey = (Asn1OctetString)seq[1];
		}

		// Token: 0x06000A1A RID: 2586 RVA: 0x00047570 File Offset: 0x00047570
		public static RecipientEncryptedKey GetInstance(Asn1TaggedObject obj, bool isExplicit)
		{
			return RecipientEncryptedKey.GetInstance(Asn1Sequence.GetInstance(obj, isExplicit));
		}

		// Token: 0x06000A1B RID: 2587 RVA: 0x00047580 File Offset: 0x00047580
		public static RecipientEncryptedKey GetInstance(object obj)
		{
			if (obj == null || obj is RecipientEncryptedKey)
			{
				return (RecipientEncryptedKey)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new RecipientEncryptedKey((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid RecipientEncryptedKey: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06000A1C RID: 2588 RVA: 0x000475DC File Offset: 0x000475DC
		public RecipientEncryptedKey(KeyAgreeRecipientIdentifier id, Asn1OctetString encryptedKey)
		{
			this.identifier = id;
			this.encryptedKey = encryptedKey;
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06000A1D RID: 2589 RVA: 0x000475F4 File Offset: 0x000475F4
		public KeyAgreeRecipientIdentifier Identifier
		{
			get
			{
				return this.identifier;
			}
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06000A1E RID: 2590 RVA: 0x000475FC File Offset: 0x000475FC
		public Asn1OctetString EncryptedKey
		{
			get
			{
				return this.encryptedKey;
			}
		}

		// Token: 0x06000A1F RID: 2591 RVA: 0x00047604 File Offset: 0x00047604
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.identifier,
				this.encryptedKey
			});
		}

		// Token: 0x04000716 RID: 1814
		private readonly KeyAgreeRecipientIdentifier identifier;

		// Token: 0x04000717 RID: 1815
		private readonly Asn1OctetString encryptedKey;
	}
}
