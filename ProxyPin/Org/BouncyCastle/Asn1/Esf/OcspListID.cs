using System;
using System.Collections;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Collections;

namespace Org.BouncyCastle.Asn1.Esf
{
	// Token: 0x02000155 RID: 341
	public class OcspListID : Asn1Encodable
	{
		// Token: 0x06000BBA RID: 3002 RVA: 0x0004D0A8 File Offset: 0x0004D0A8
		public static OcspListID GetInstance(object obj)
		{
			if (obj == null || obj is OcspListID)
			{
				return (OcspListID)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new OcspListID((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in 'OcspListID' factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06000BBB RID: 3003 RVA: 0x0004D104 File Offset: 0x0004D104
		private OcspListID(Asn1Sequence seq)
		{
			if (seq == null)
			{
				throw new ArgumentNullException("seq");
			}
			if (seq.Count != 1)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count, "seq");
			}
			this.ocspResponses = (Asn1Sequence)seq[0].ToAsn1Object();
			foreach (object obj in this.ocspResponses)
			{
				Asn1Encodable asn1Encodable = (Asn1Encodable)obj;
				OcspResponsesID.GetInstance(asn1Encodable.ToAsn1Object());
			}
		}

		// Token: 0x06000BBC RID: 3004 RVA: 0x0004D1C8 File Offset: 0x0004D1C8
		public OcspListID(params OcspResponsesID[] ocspResponses)
		{
			if (ocspResponses == null)
			{
				throw new ArgumentNullException("ocspResponses");
			}
			this.ocspResponses = new DerSequence(ocspResponses);
		}

		// Token: 0x06000BBD RID: 3005 RVA: 0x0004D1F0 File Offset: 0x0004D1F0
		public OcspListID(IEnumerable ocspResponses)
		{
			if (ocspResponses == null)
			{
				throw new ArgumentNullException("ocspResponses");
			}
			if (!CollectionUtilities.CheckElementsAreOfType(ocspResponses, typeof(OcspResponsesID)))
			{
				throw new ArgumentException("Must contain only 'OcspResponsesID' objects", "ocspResponses");
			}
			this.ocspResponses = new DerSequence(Asn1EncodableVector.FromEnumerable(ocspResponses));
		}

		// Token: 0x06000BBE RID: 3006 RVA: 0x0004D250 File Offset: 0x0004D250
		public OcspResponsesID[] GetOcspResponses()
		{
			OcspResponsesID[] array = new OcspResponsesID[this.ocspResponses.Count];
			for (int i = 0; i < this.ocspResponses.Count; i++)
			{
				array[i] = OcspResponsesID.GetInstance(this.ocspResponses[i].ToAsn1Object());
			}
			return array;
		}

		// Token: 0x06000BBF RID: 3007 RVA: 0x0004D2AC File Offset: 0x0004D2AC
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(this.ocspResponses);
		}

		// Token: 0x0400080F RID: 2063
		private readonly Asn1Sequence ocspResponses;
	}
}
