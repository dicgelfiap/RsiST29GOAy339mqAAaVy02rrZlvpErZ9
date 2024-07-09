using System;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.X509;

namespace Org.BouncyCastle.X509
{
	// Token: 0x02000717 RID: 1815
	public class X509Attribute : Asn1Encodable
	{
		// Token: 0x06003F7B RID: 16251 RVA: 0x0015C35C File Offset: 0x0015C35C
		internal X509Attribute(Asn1Encodable at)
		{
			this.attr = AttributeX509.GetInstance(at);
		}

		// Token: 0x06003F7C RID: 16252 RVA: 0x0015C370 File Offset: 0x0015C370
		public X509Attribute(string oid, Asn1Encodable value)
		{
			this.attr = new AttributeX509(new DerObjectIdentifier(oid), new DerSet(value));
		}

		// Token: 0x06003F7D RID: 16253 RVA: 0x0015C390 File Offset: 0x0015C390
		public X509Attribute(string oid, Asn1EncodableVector value)
		{
			this.attr = new AttributeX509(new DerObjectIdentifier(oid), new DerSet(value));
		}

		// Token: 0x17000AB9 RID: 2745
		// (get) Token: 0x06003F7E RID: 16254 RVA: 0x0015C3B0 File Offset: 0x0015C3B0
		public string Oid
		{
			get
			{
				return this.attr.AttrType.Id;
			}
		}

		// Token: 0x06003F7F RID: 16255 RVA: 0x0015C3C4 File Offset: 0x0015C3C4
		public Asn1Encodable[] GetValues()
		{
			Asn1Set attrValues = this.attr.AttrValues;
			Asn1Encodable[] array = new Asn1Encodable[attrValues.Count];
			for (int num = 0; num != attrValues.Count; num++)
			{
				array[num] = attrValues[num];
			}
			return array;
		}

		// Token: 0x06003F80 RID: 16256 RVA: 0x0015C410 File Offset: 0x0015C410
		public override Asn1Object ToAsn1Object()
		{
			return this.attr.ToAsn1Object();
		}

		// Token: 0x0400209F RID: 8351
		private readonly AttributeX509 attr;
	}
}
