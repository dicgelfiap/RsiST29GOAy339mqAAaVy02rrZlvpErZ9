using System;
using System.Collections;
using Org.BouncyCastle.Asn1.Cms;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x020002D5 RID: 725
	public interface CmsAttributeTableGenerator
	{
		// Token: 0x060015FA RID: 5626
		AttributeTable GetAttributes(IDictionary parameters);
	}
}
