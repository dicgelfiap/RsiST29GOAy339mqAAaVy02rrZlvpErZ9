using System;
using System.Collections;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x020002FE RID: 766
	public class DefaultSignedAttributeTableGenerator : CmsAttributeTableGenerator
	{
		// Token: 0x06001733 RID: 5939 RVA: 0x000796AC File Offset: 0x000796AC
		public DefaultSignedAttributeTableGenerator()
		{
			this.table = Platform.CreateHashtable();
		}

		// Token: 0x06001734 RID: 5940 RVA: 0x000796C0 File Offset: 0x000796C0
		public DefaultSignedAttributeTableGenerator(AttributeTable attributeTable)
		{
			if (attributeTable != null)
			{
				this.table = attributeTable.ToDictionary();
				return;
			}
			this.table = Platform.CreateHashtable();
		}

		// Token: 0x06001735 RID: 5941 RVA: 0x000796E8 File Offset: 0x000796E8
		protected virtual Hashtable createStandardAttributeTable(IDictionary parameters)
		{
			Hashtable hashtable = new Hashtable(this.table);
			this.DoCreateStandardAttributeTable(parameters, hashtable);
			return hashtable;
		}

		// Token: 0x06001736 RID: 5942 RVA: 0x00079710 File Offset: 0x00079710
		private void DoCreateStandardAttributeTable(IDictionary parameters, IDictionary std)
		{
			if (parameters.Contains(CmsAttributeTableParameter.ContentType) && !std.Contains(CmsAttributes.ContentType))
			{
				DerObjectIdentifier element = (DerObjectIdentifier)parameters[CmsAttributeTableParameter.ContentType];
				Org.BouncyCastle.Asn1.Cms.Attribute attribute = new Org.BouncyCastle.Asn1.Cms.Attribute(CmsAttributes.ContentType, new DerSet(element));
				std[attribute.AttrType] = attribute;
			}
			if (!std.Contains(CmsAttributes.SigningTime))
			{
				Org.BouncyCastle.Asn1.Cms.Attribute attribute2 = new Org.BouncyCastle.Asn1.Cms.Attribute(CmsAttributes.SigningTime, new DerSet(new Time(DateTime.UtcNow)));
				std[attribute2.AttrType] = attribute2;
			}
			if (!std.Contains(CmsAttributes.MessageDigest))
			{
				byte[] str = (byte[])parameters[CmsAttributeTableParameter.Digest];
				Org.BouncyCastle.Asn1.Cms.Attribute attribute3 = new Org.BouncyCastle.Asn1.Cms.Attribute(CmsAttributes.MessageDigest, new DerSet(new DerOctetString(str)));
				std[attribute3.AttrType] = attribute3;
			}
		}

		// Token: 0x06001737 RID: 5943 RVA: 0x000797F4 File Offset: 0x000797F4
		public virtual AttributeTable GetAttributes(IDictionary parameters)
		{
			IDictionary attrs = this.createStandardAttributeTable(parameters);
			return new AttributeTable(attrs);
		}

		// Token: 0x04000FA1 RID: 4001
		private readonly IDictionary table;
	}
}
