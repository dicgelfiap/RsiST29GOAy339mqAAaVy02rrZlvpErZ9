using System;
using System.Collections;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x020002FD RID: 765
	public class DefaultAuthenticatedAttributeTableGenerator : CmsAttributeTableGenerator
	{
		// Token: 0x0600172F RID: 5935 RVA: 0x000795A8 File Offset: 0x000795A8
		public DefaultAuthenticatedAttributeTableGenerator()
		{
			this.table = Platform.CreateHashtable();
		}

		// Token: 0x06001730 RID: 5936 RVA: 0x000795BC File Offset: 0x000795BC
		public DefaultAuthenticatedAttributeTableGenerator(AttributeTable attributeTable)
		{
			if (attributeTable != null)
			{
				this.table = attributeTable.ToDictionary();
				return;
			}
			this.table = Platform.CreateHashtable();
		}

		// Token: 0x06001731 RID: 5937 RVA: 0x000795E4 File Offset: 0x000795E4
		protected virtual IDictionary CreateStandardAttributeTable(IDictionary parameters)
		{
			IDictionary dictionary = Platform.CreateHashtable(this.table);
			if (!dictionary.Contains(CmsAttributes.ContentType))
			{
				DerObjectIdentifier element = (DerObjectIdentifier)parameters[CmsAttributeTableParameter.ContentType];
				Org.BouncyCastle.Asn1.Cms.Attribute attribute = new Org.BouncyCastle.Asn1.Cms.Attribute(CmsAttributes.ContentType, new DerSet(element));
				dictionary[attribute.AttrType] = attribute;
			}
			if (!dictionary.Contains(CmsAttributes.MessageDigest))
			{
				byte[] str = (byte[])parameters[CmsAttributeTableParameter.Digest];
				Org.BouncyCastle.Asn1.Cms.Attribute attribute2 = new Org.BouncyCastle.Asn1.Cms.Attribute(CmsAttributes.MessageDigest, new DerSet(new DerOctetString(str)));
				dictionary[attribute2.AttrType] = attribute2;
			}
			return dictionary;
		}

		// Token: 0x06001732 RID: 5938 RVA: 0x0007968C File Offset: 0x0007968C
		public virtual AttributeTable GetAttributes(IDictionary parameters)
		{
			IDictionary attrs = this.CreateStandardAttributeTable(parameters);
			return new AttributeTable(attrs);
		}

		// Token: 0x04000FA0 RID: 4000
		private readonly IDictionary table;
	}
}
