using System;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Asn1.X509;

namespace Org.BouncyCastle.Asn1.Smime
{
	// Token: 0x020001C7 RID: 455
	public class SmimeEncryptionKeyPreferenceAttribute : AttributeX509
	{
		// Token: 0x06000EBC RID: 3772 RVA: 0x00058F94 File Offset: 0x00058F94
		public SmimeEncryptionKeyPreferenceAttribute(IssuerAndSerialNumber issAndSer) : base(SmimeAttributes.EncrypKeyPref, new DerSet(new DerTaggedObject(false, 0, issAndSer)))
		{
		}

		// Token: 0x06000EBD RID: 3773 RVA: 0x00058FB0 File Offset: 0x00058FB0
		public SmimeEncryptionKeyPreferenceAttribute(RecipientKeyIdentifier rKeyID) : base(SmimeAttributes.EncrypKeyPref, new DerSet(new DerTaggedObject(false, 1, rKeyID)))
		{
		}

		// Token: 0x06000EBE RID: 3774 RVA: 0x00058FCC File Offset: 0x00058FCC
		public SmimeEncryptionKeyPreferenceAttribute(Asn1OctetString sKeyID) : base(SmimeAttributes.EncrypKeyPref, new DerSet(new DerTaggedObject(false, 2, sKeyID)))
		{
		}
	}
}
