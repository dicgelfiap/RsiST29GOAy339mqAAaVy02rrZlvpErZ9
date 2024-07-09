using System;
using System.Collections;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Collections;

namespace Org.BouncyCastle.Asn1.Esf
{
	// Token: 0x02000150 RID: 336
	public class CrlListID : Asn1Encodable
	{
		// Token: 0x06000B9E RID: 2974 RVA: 0x0004C90C File Offset: 0x0004C90C
		public static CrlListID GetInstance(object obj)
		{
			if (obj == null || obj is CrlListID)
			{
				return (CrlListID)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new CrlListID((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in 'CrlListID' factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06000B9F RID: 2975 RVA: 0x0004C968 File Offset: 0x0004C968
		private CrlListID(Asn1Sequence seq)
		{
			if (seq == null)
			{
				throw new ArgumentNullException("seq");
			}
			if (seq.Count != 1)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count, "seq");
			}
			this.crls = (Asn1Sequence)seq[0].ToAsn1Object();
			foreach (object obj in this.crls)
			{
				Asn1Encodable asn1Encodable = (Asn1Encodable)obj;
				CrlValidatedID.GetInstance(asn1Encodable.ToAsn1Object());
			}
		}

		// Token: 0x06000BA0 RID: 2976 RVA: 0x0004CA2C File Offset: 0x0004CA2C
		public CrlListID(params CrlValidatedID[] crls)
		{
			if (crls == null)
			{
				throw new ArgumentNullException("crls");
			}
			this.crls = new DerSequence(crls);
		}

		// Token: 0x06000BA1 RID: 2977 RVA: 0x0004CA54 File Offset: 0x0004CA54
		public CrlListID(IEnumerable crls)
		{
			if (crls == null)
			{
				throw new ArgumentNullException("crls");
			}
			if (!CollectionUtilities.CheckElementsAreOfType(crls, typeof(CrlValidatedID)))
			{
				throw new ArgumentException("Must contain only 'CrlValidatedID' objects", "crls");
			}
			this.crls = new DerSequence(Asn1EncodableVector.FromEnumerable(crls));
		}

		// Token: 0x06000BA2 RID: 2978 RVA: 0x0004CAB4 File Offset: 0x0004CAB4
		public CrlValidatedID[] GetCrls()
		{
			CrlValidatedID[] array = new CrlValidatedID[this.crls.Count];
			for (int i = 0; i < this.crls.Count; i++)
			{
				array[i] = CrlValidatedID.GetInstance(this.crls[i].ToAsn1Object());
			}
			return array;
		}

		// Token: 0x06000BA3 RID: 2979 RVA: 0x0004CB10 File Offset: 0x0004CB10
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(this.crls);
		}

		// Token: 0x040007F9 RID: 2041
		private readonly Asn1Sequence crls;
	}
}
