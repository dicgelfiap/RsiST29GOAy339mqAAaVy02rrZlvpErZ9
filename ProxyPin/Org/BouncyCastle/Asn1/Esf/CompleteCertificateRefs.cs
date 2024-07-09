using System;
using System.Collections;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Collections;

namespace Org.BouncyCastle.Asn1.Esf
{
	// Token: 0x0200014D RID: 333
	public class CompleteCertificateRefs : Asn1Encodable
	{
		// Token: 0x06000B8A RID: 2954 RVA: 0x0004C39C File Offset: 0x0004C39C
		public static CompleteCertificateRefs GetInstance(object obj)
		{
			if (obj == null || obj is CompleteCertificateRefs)
			{
				return (CompleteCertificateRefs)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new CompleteCertificateRefs((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in 'CompleteCertificateRefs' factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06000B8B RID: 2955 RVA: 0x0004C3F8 File Offset: 0x0004C3F8
		private CompleteCertificateRefs(Asn1Sequence seq)
		{
			if (seq == null)
			{
				throw new ArgumentNullException("seq");
			}
			foreach (object obj in seq)
			{
				Asn1Encodable asn1Encodable = (Asn1Encodable)obj;
				OtherCertID.GetInstance(asn1Encodable.ToAsn1Object());
			}
			this.otherCertIDs = seq;
		}

		// Token: 0x06000B8C RID: 2956 RVA: 0x0004C478 File Offset: 0x0004C478
		public CompleteCertificateRefs(params OtherCertID[] otherCertIDs)
		{
			if (otherCertIDs == null)
			{
				throw new ArgumentNullException("otherCertIDs");
			}
			this.otherCertIDs = new DerSequence(otherCertIDs);
		}

		// Token: 0x06000B8D RID: 2957 RVA: 0x0004C4A0 File Offset: 0x0004C4A0
		public CompleteCertificateRefs(IEnumerable otherCertIDs)
		{
			if (otherCertIDs == null)
			{
				throw new ArgumentNullException("otherCertIDs");
			}
			if (!CollectionUtilities.CheckElementsAreOfType(otherCertIDs, typeof(OtherCertID)))
			{
				throw new ArgumentException("Must contain only 'OtherCertID' objects", "otherCertIDs");
			}
			this.otherCertIDs = new DerSequence(Asn1EncodableVector.FromEnumerable(otherCertIDs));
		}

		// Token: 0x06000B8E RID: 2958 RVA: 0x0004C500 File Offset: 0x0004C500
		public OtherCertID[] GetOtherCertIDs()
		{
			OtherCertID[] array = new OtherCertID[this.otherCertIDs.Count];
			for (int i = 0; i < this.otherCertIDs.Count; i++)
			{
				array[i] = OtherCertID.GetInstance(this.otherCertIDs[i].ToAsn1Object());
			}
			return array;
		}

		// Token: 0x06000B8F RID: 2959 RVA: 0x0004C55C File Offset: 0x0004C55C
		public override Asn1Object ToAsn1Object()
		{
			return this.otherCertIDs;
		}

		// Token: 0x040007F4 RID: 2036
		private readonly Asn1Sequence otherCertIDs;
	}
}
