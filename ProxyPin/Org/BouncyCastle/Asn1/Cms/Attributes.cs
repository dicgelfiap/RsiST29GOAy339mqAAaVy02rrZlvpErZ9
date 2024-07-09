using System;

namespace Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x020000FB RID: 251
	public class Attributes : Asn1Encodable
	{
		// Token: 0x0600091F RID: 2335 RVA: 0x00044458 File Offset: 0x00044458
		private Attributes(Asn1Set attributes)
		{
			this.attributes = attributes;
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x00044468 File Offset: 0x00044468
		public Attributes(Asn1EncodableVector v)
		{
			this.attributes = new BerSet(v);
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x0004447C File Offset: 0x0004447C
		public static Attributes GetInstance(object obj)
		{
			if (obj is Attributes)
			{
				return (Attributes)obj;
			}
			if (obj != null)
			{
				return new Attributes(Asn1Set.GetInstance(obj));
			}
			return null;
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x000444A4 File Offset: 0x000444A4
		public virtual Attribute[] GetAttributes()
		{
			Attribute[] array = new Attribute[this.attributes.Count];
			for (int num = 0; num != array.Length; num++)
			{
				array[num] = Attribute.GetInstance(this.attributes[num]);
			}
			return array;
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x000444F0 File Offset: 0x000444F0
		public override Asn1Object ToAsn1Object()
		{
			return this.attributes;
		}

		// Token: 0x040006A5 RID: 1701
		private readonly Asn1Set attributes;
	}
}
