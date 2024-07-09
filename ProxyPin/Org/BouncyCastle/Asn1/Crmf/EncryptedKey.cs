using System;
using Org.BouncyCastle.Asn1.Cms;

namespace Org.BouncyCastle.Asn1.Crmf
{
	// Token: 0x02000133 RID: 307
	public class EncryptedKey : Asn1Encodable, IAsn1Choice
	{
		// Token: 0x06000ADB RID: 2779 RVA: 0x000499D0 File Offset: 0x000499D0
		public static EncryptedKey GetInstance(object o)
		{
			if (o is EncryptedKey)
			{
				return (EncryptedKey)o;
			}
			if (o is Asn1TaggedObject)
			{
				return new EncryptedKey(EnvelopedData.GetInstance((Asn1TaggedObject)o, false));
			}
			if (o is EncryptedValue)
			{
				return new EncryptedKey((EncryptedValue)o);
			}
			return new EncryptedKey(EncryptedValue.GetInstance(o));
		}

		// Token: 0x06000ADC RID: 2780 RVA: 0x00049A34 File Offset: 0x00049A34
		public EncryptedKey(EnvelopedData envelopedData)
		{
			this.envelopedData = envelopedData;
		}

		// Token: 0x06000ADD RID: 2781 RVA: 0x00049A44 File Offset: 0x00049A44
		public EncryptedKey(EncryptedValue encryptedValue)
		{
			this.encryptedValue = encryptedValue;
		}

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000ADE RID: 2782 RVA: 0x00049A54 File Offset: 0x00049A54
		public virtual bool IsEncryptedValue
		{
			get
			{
				return this.encryptedValue != null;
			}
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000ADF RID: 2783 RVA: 0x00049A64 File Offset: 0x00049A64
		public virtual Asn1Encodable Value
		{
			get
			{
				if (this.encryptedValue != null)
				{
					return this.encryptedValue;
				}
				return this.envelopedData;
			}
		}

		// Token: 0x06000AE0 RID: 2784 RVA: 0x00049A80 File Offset: 0x00049A80
		public override Asn1Object ToAsn1Object()
		{
			if (this.encryptedValue != null)
			{
				return this.encryptedValue.ToAsn1Object();
			}
			return new DerTaggedObject(false, 0, this.envelopedData);
		}

		// Token: 0x04000772 RID: 1906
		private readonly EnvelopedData envelopedData;

		// Token: 0x04000773 RID: 1907
		private readonly EncryptedValue encryptedValue;
	}
}
