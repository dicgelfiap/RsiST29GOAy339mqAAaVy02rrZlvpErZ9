using System;
using System.Collections;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x020001E7 RID: 487
	public class AttributeTable
	{
		// Token: 0x06000FA2 RID: 4002 RVA: 0x0005CF5C File Offset: 0x0005CF5C
		public AttributeTable(IDictionary attrs)
		{
			this.attributes = Platform.CreateHashtable(attrs);
		}

		// Token: 0x06000FA3 RID: 4003 RVA: 0x0005CF70 File Offset: 0x0005CF70
		[Obsolete]
		public AttributeTable(Hashtable attrs)
		{
			this.attributes = Platform.CreateHashtable(attrs);
		}

		// Token: 0x06000FA4 RID: 4004 RVA: 0x0005CF84 File Offset: 0x0005CF84
		public AttributeTable(Asn1EncodableVector v)
		{
			this.attributes = Platform.CreateHashtable(v.Count);
			for (int num = 0; num != v.Count; num++)
			{
				AttributeX509 instance = AttributeX509.GetInstance(v[num]);
				this.attributes.Add(instance.AttrType, instance);
			}
		}

		// Token: 0x06000FA5 RID: 4005 RVA: 0x0005CFE0 File Offset: 0x0005CFE0
		public AttributeTable(Asn1Set s)
		{
			this.attributes = Platform.CreateHashtable(s.Count);
			for (int num = 0; num != s.Count; num++)
			{
				AttributeX509 instance = AttributeX509.GetInstance(s[num]);
				this.attributes.Add(instance.AttrType, instance);
			}
		}

		// Token: 0x06000FA6 RID: 4006 RVA: 0x0005D03C File Offset: 0x0005D03C
		public AttributeX509 Get(DerObjectIdentifier oid)
		{
			return (AttributeX509)this.attributes[oid];
		}

		// Token: 0x06000FA7 RID: 4007 RVA: 0x0005D050 File Offset: 0x0005D050
		[Obsolete("Use 'ToDictionary' instead")]
		public Hashtable ToHashtable()
		{
			return new Hashtable(this.attributes);
		}

		// Token: 0x06000FA8 RID: 4008 RVA: 0x0005D060 File Offset: 0x0005D060
		public IDictionary ToDictionary()
		{
			return Platform.CreateHashtable(this.attributes);
		}

		// Token: 0x04000BAC RID: 2988
		private readonly IDictionary attributes;
	}
}
