using System;
using System.Collections;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Collections;

namespace Org.BouncyCastle.Asn1.Esf
{
	// Token: 0x0200014E RID: 334
	public class CompleteRevocationRefs : Asn1Encodable
	{
		// Token: 0x06000B90 RID: 2960 RVA: 0x0004C564 File Offset: 0x0004C564
		public static CompleteRevocationRefs GetInstance(object obj)
		{
			if (obj == null || obj is CompleteRevocationRefs)
			{
				return (CompleteRevocationRefs)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new CompleteRevocationRefs((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in 'CompleteRevocationRefs' factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06000B91 RID: 2961 RVA: 0x0004C5C0 File Offset: 0x0004C5C0
		private CompleteRevocationRefs(Asn1Sequence seq)
		{
			if (seq == null)
			{
				throw new ArgumentNullException("seq");
			}
			foreach (object obj in seq)
			{
				Asn1Encodable asn1Encodable = (Asn1Encodable)obj;
				CrlOcspRef.GetInstance(asn1Encodable.ToAsn1Object());
			}
			this.crlOcspRefs = seq;
		}

		// Token: 0x06000B92 RID: 2962 RVA: 0x0004C640 File Offset: 0x0004C640
		public CompleteRevocationRefs(params CrlOcspRef[] crlOcspRefs)
		{
			if (crlOcspRefs == null)
			{
				throw new ArgumentNullException("crlOcspRefs");
			}
			this.crlOcspRefs = new DerSequence(crlOcspRefs);
		}

		// Token: 0x06000B93 RID: 2963 RVA: 0x0004C668 File Offset: 0x0004C668
		public CompleteRevocationRefs(IEnumerable crlOcspRefs)
		{
			if (crlOcspRefs == null)
			{
				throw new ArgumentNullException("crlOcspRefs");
			}
			if (!CollectionUtilities.CheckElementsAreOfType(crlOcspRefs, typeof(CrlOcspRef)))
			{
				throw new ArgumentException("Must contain only 'CrlOcspRef' objects", "crlOcspRefs");
			}
			this.crlOcspRefs = new DerSequence(Asn1EncodableVector.FromEnumerable(crlOcspRefs));
		}

		// Token: 0x06000B94 RID: 2964 RVA: 0x0004C6C8 File Offset: 0x0004C6C8
		public CrlOcspRef[] GetCrlOcspRefs()
		{
			CrlOcspRef[] array = new CrlOcspRef[this.crlOcspRefs.Count];
			for (int i = 0; i < this.crlOcspRefs.Count; i++)
			{
				array[i] = CrlOcspRef.GetInstance(this.crlOcspRefs[i].ToAsn1Object());
			}
			return array;
		}

		// Token: 0x06000B95 RID: 2965 RVA: 0x0004C724 File Offset: 0x0004C724
		public override Asn1Object ToAsn1Object()
		{
			return this.crlOcspRefs;
		}

		// Token: 0x040007F5 RID: 2037
		private readonly Asn1Sequence crlOcspRefs;
	}
}
