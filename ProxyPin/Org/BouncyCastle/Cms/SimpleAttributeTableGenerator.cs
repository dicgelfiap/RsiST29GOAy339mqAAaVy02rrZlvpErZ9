using System;
using System.Collections;
using Org.BouncyCastle.Asn1.Cms;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x02000318 RID: 792
	public class SimpleAttributeTableGenerator : CmsAttributeTableGenerator
	{
		// Token: 0x060017F5 RID: 6133 RVA: 0x0007CA80 File Offset: 0x0007CA80
		public SimpleAttributeTableGenerator(AttributeTable attributes)
		{
			this.attributes = attributes;
		}

		// Token: 0x060017F6 RID: 6134 RVA: 0x0007CA90 File Offset: 0x0007CA90
		public virtual AttributeTable GetAttributes(IDictionary parameters)
		{
			return this.attributes;
		}

		// Token: 0x04000FF1 RID: 4081
		private readonly AttributeTable attributes;
	}
}
