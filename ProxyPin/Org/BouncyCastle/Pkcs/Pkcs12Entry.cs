using System;
using System.Collections;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Utilities.Collections;

namespace Org.BouncyCastle.Pkcs
{
	// Token: 0x0200067C RID: 1660
	public abstract class Pkcs12Entry
	{
		// Token: 0x060039F8 RID: 14840 RVA: 0x0013763C File Offset: 0x0013763C
		protected internal Pkcs12Entry(IDictionary attributes)
		{
			this.attributes = attributes;
			foreach (object obj in attributes)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				if (!(dictionaryEntry.Key is string))
				{
					throw new ArgumentException("Attribute keys must be of type: " + typeof(string).FullName, "attributes");
				}
				if (!(dictionaryEntry.Value is Asn1Encodable))
				{
					throw new ArgumentException("Attribute values must be of type: " + typeof(Asn1Encodable).FullName, "attributes");
				}
			}
		}

		// Token: 0x060039F9 RID: 14841 RVA: 0x00137714 File Offset: 0x00137714
		[Obsolete("Use 'object[index]' syntax instead")]
		public Asn1Encodable GetBagAttribute(DerObjectIdentifier oid)
		{
			return (Asn1Encodable)this.attributes[oid.Id];
		}

		// Token: 0x060039FA RID: 14842 RVA: 0x0013772C File Offset: 0x0013772C
		[Obsolete("Use 'object[index]' syntax instead")]
		public Asn1Encodable GetBagAttribute(string oid)
		{
			return (Asn1Encodable)this.attributes[oid];
		}

		// Token: 0x060039FB RID: 14843 RVA: 0x00137740 File Offset: 0x00137740
		[Obsolete("Use 'BagAttributeKeys' property")]
		public IEnumerator GetBagAttributeKeys()
		{
			return this.attributes.Keys.GetEnumerator();
		}

		// Token: 0x170009FB RID: 2555
		public Asn1Encodable this[DerObjectIdentifier oid]
		{
			get
			{
				return (Asn1Encodable)this.attributes[oid.Id];
			}
		}

		// Token: 0x170009FC RID: 2556
		public Asn1Encodable this[string oid]
		{
			get
			{
				return (Asn1Encodable)this.attributes[oid];
			}
		}

		// Token: 0x170009FD RID: 2557
		// (get) Token: 0x060039FE RID: 14846 RVA: 0x00137780 File Offset: 0x00137780
		public IEnumerable BagAttributeKeys
		{
			get
			{
				return new EnumerableProxy(this.attributes.Keys);
			}
		}

		// Token: 0x04001E2F RID: 7727
		private readonly IDictionary attributes;
	}
}
