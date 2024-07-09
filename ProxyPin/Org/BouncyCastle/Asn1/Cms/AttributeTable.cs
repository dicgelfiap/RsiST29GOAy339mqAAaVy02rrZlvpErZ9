using System;
using System.Collections;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x020000FC RID: 252
	public class AttributeTable
	{
		// Token: 0x06000924 RID: 2340 RVA: 0x000444F8 File Offset: 0x000444F8
		[Obsolete]
		public AttributeTable(Hashtable attrs)
		{
			this.attributes = Platform.CreateHashtable(attrs);
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x0004450C File Offset: 0x0004450C
		public AttributeTable(IDictionary attrs)
		{
			this.attributes = Platform.CreateHashtable(attrs);
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x00044520 File Offset: 0x00044520
		public AttributeTable(Asn1EncodableVector v)
		{
			this.attributes = Platform.CreateHashtable(v.Count);
			foreach (object obj in v)
			{
				Asn1Encodable obj2 = (Asn1Encodable)obj;
				Attribute instance = Attribute.GetInstance(obj2);
				this.AddAttribute(instance);
			}
		}

		// Token: 0x06000927 RID: 2343 RVA: 0x0004459C File Offset: 0x0004459C
		public AttributeTable(Asn1Set s)
		{
			this.attributes = Platform.CreateHashtable(s.Count);
			for (int num = 0; num != s.Count; num++)
			{
				Attribute instance = Attribute.GetInstance(s[num]);
				this.AddAttribute(instance);
			}
		}

		// Token: 0x06000928 RID: 2344 RVA: 0x000445EC File Offset: 0x000445EC
		public AttributeTable(Attributes attrs) : this(Asn1Set.GetInstance(attrs.ToAsn1Object()))
		{
		}

		// Token: 0x06000929 RID: 2345 RVA: 0x00044600 File Offset: 0x00044600
		private void AddAttribute(Attribute a)
		{
			DerObjectIdentifier attrType = a.AttrType;
			object obj = this.attributes[attrType];
			if (obj == null)
			{
				this.attributes[attrType] = a;
				return;
			}
			IList list;
			if (obj is Attribute)
			{
				list = Platform.CreateArrayList();
				list.Add(obj);
				list.Add(a);
			}
			else
			{
				list = (IList)obj;
				list.Add(a);
			}
			this.attributes[attrType] = list;
		}

		// Token: 0x1700020C RID: 524
		public Attribute this[DerObjectIdentifier oid]
		{
			get
			{
				object obj = this.attributes[oid];
				if (obj is IList)
				{
					return (Attribute)((IList)obj)[0];
				}
				return (Attribute)obj;
			}
		}

		// Token: 0x0600092B RID: 2347 RVA: 0x000446C0 File Offset: 0x000446C0
		[Obsolete("Use 'object[oid]' syntax instead")]
		public Attribute Get(DerObjectIdentifier oid)
		{
			return this[oid];
		}

		// Token: 0x0600092C RID: 2348 RVA: 0x000446CC File Offset: 0x000446CC
		public Asn1EncodableVector GetAll(DerObjectIdentifier oid)
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector();
			object obj = this.attributes[oid];
			if (obj is IList)
			{
				using (IEnumerator enumerator = ((IList)obj).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj2 = enumerator.Current;
						Attribute element = (Attribute)obj2;
						asn1EncodableVector.Add(element);
					}
					return asn1EncodableVector;
				}
			}
			if (obj != null)
			{
				asn1EncodableVector.Add((Attribute)obj);
			}
			return asn1EncodableVector;
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x0600092D RID: 2349 RVA: 0x00044764 File Offset: 0x00044764
		public int Count
		{
			get
			{
				int num = 0;
				foreach (object obj in this.attributes.Values)
				{
					if (obj is IList)
					{
						num += ((IList)obj).Count;
					}
					else
					{
						num++;
					}
				}
				return num;
			}
		}

		// Token: 0x0600092E RID: 2350 RVA: 0x000447E4 File Offset: 0x000447E4
		public IDictionary ToDictionary()
		{
			return Platform.CreateHashtable(this.attributes);
		}

		// Token: 0x0600092F RID: 2351 RVA: 0x000447F4 File Offset: 0x000447F4
		[Obsolete("Use 'ToDictionary' instead")]
		public Hashtable ToHashtable()
		{
			return new Hashtable(this.attributes);
		}

		// Token: 0x06000930 RID: 2352 RVA: 0x00044804 File Offset: 0x00044804
		public Asn1EncodableVector ToAsn1EncodableVector()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector();
			foreach (object obj in this.attributes.Values)
			{
				if (obj is IList)
				{
					using (IEnumerator enumerator2 = ((IList)obj).GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							object obj2 = enumerator2.Current;
							asn1EncodableVector.Add(Attribute.GetInstance(obj2));
						}
						continue;
					}
				}
				asn1EncodableVector.Add(Attribute.GetInstance(obj));
			}
			return asn1EncodableVector;
		}

		// Token: 0x06000931 RID: 2353 RVA: 0x000448D8 File Offset: 0x000448D8
		public Attributes ToAttributes()
		{
			return new Attributes(this.ToAsn1EncodableVector());
		}

		// Token: 0x06000932 RID: 2354 RVA: 0x000448E8 File Offset: 0x000448E8
		public AttributeTable Add(DerObjectIdentifier attrType, Asn1Encodable attrValue)
		{
			AttributeTable attributeTable = new AttributeTable(this.attributes);
			attributeTable.AddAttribute(new Attribute(attrType, new DerSet(attrValue)));
			return attributeTable;
		}

		// Token: 0x06000933 RID: 2355 RVA: 0x00044918 File Offset: 0x00044918
		public AttributeTable Remove(DerObjectIdentifier attrType)
		{
			AttributeTable attributeTable = new AttributeTable(this.attributes);
			attributeTable.attributes.Remove(attrType);
			return attributeTable;
		}

		// Token: 0x040006A6 RID: 1702
		private readonly IDictionary attributes;
	}
}
