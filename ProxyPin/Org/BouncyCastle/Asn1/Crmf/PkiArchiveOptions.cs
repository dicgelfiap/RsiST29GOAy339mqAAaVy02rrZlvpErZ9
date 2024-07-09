using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Crmf
{
	// Token: 0x02000136 RID: 310
	public class PkiArchiveOptions : Asn1Encodable, IAsn1Choice
	{
		// Token: 0x06000AF1 RID: 2801 RVA: 0x00049DAC File Offset: 0x00049DAC
		public static PkiArchiveOptions GetInstance(object obj)
		{
			if (obj is PkiArchiveOptions)
			{
				return (PkiArchiveOptions)obj;
			}
			if (obj is Asn1TaggedObject)
			{
				return new PkiArchiveOptions((Asn1TaggedObject)obj);
			}
			throw new ArgumentException("Invalid object: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06000AF2 RID: 2802 RVA: 0x00049E00 File Offset: 0x00049E00
		private PkiArchiveOptions(Asn1TaggedObject tagged)
		{
			switch (tagged.TagNo)
			{
			case 0:
				this.value = EncryptedKey.GetInstance(tagged.GetObject());
				return;
			case 1:
				this.value = Asn1OctetString.GetInstance(tagged, false);
				return;
			case 2:
				this.value = DerBoolean.GetInstance(tagged, false);
				return;
			default:
				throw new ArgumentException("unknown tag number: " + tagged.TagNo, "tagged");
			}
		}

		// Token: 0x06000AF3 RID: 2803 RVA: 0x00049E84 File Offset: 0x00049E84
		public PkiArchiveOptions(EncryptedKey encKey)
		{
			this.value = encKey;
		}

		// Token: 0x06000AF4 RID: 2804 RVA: 0x00049E94 File Offset: 0x00049E94
		public PkiArchiveOptions(Asn1OctetString keyGenParameters)
		{
			this.value = keyGenParameters;
		}

		// Token: 0x06000AF5 RID: 2805 RVA: 0x00049EA4 File Offset: 0x00049EA4
		public PkiArchiveOptions(bool archiveRemGenPrivKey)
		{
			this.value = DerBoolean.GetInstance(archiveRemGenPrivKey);
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06000AF6 RID: 2806 RVA: 0x00049EB8 File Offset: 0x00049EB8
		public virtual int Type
		{
			get
			{
				if (this.value is EncryptedKey)
				{
					return 0;
				}
				if (this.value is Asn1OctetString)
				{
					return 1;
				}
				return 2;
			}
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000AF7 RID: 2807 RVA: 0x00049EE0 File Offset: 0x00049EE0
		public virtual Asn1Encodable Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x06000AF8 RID: 2808 RVA: 0x00049EE8 File Offset: 0x00049EE8
		public override Asn1Object ToAsn1Object()
		{
			if (this.value is EncryptedKey)
			{
				return new DerTaggedObject(true, 0, this.value);
			}
			if (this.value is Asn1OctetString)
			{
				return new DerTaggedObject(false, 1, this.value);
			}
			return new DerTaggedObject(false, 2, this.value);
		}

		// Token: 0x0400077C RID: 1916
		public const int encryptedPrivKey = 0;

		// Token: 0x0400077D RID: 1917
		public const int keyGenParameters = 1;

		// Token: 0x0400077E RID: 1918
		public const int archiveRemGenPrivKey = 2;

		// Token: 0x0400077F RID: 1919
		private readonly Asn1Encodable value;
	}
}
