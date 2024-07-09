using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Esf
{
	// Token: 0x02000151 RID: 337
	public class CrlOcspRef : Asn1Encodable
	{
		// Token: 0x06000BA4 RID: 2980 RVA: 0x0004CB20 File Offset: 0x0004CB20
		public static CrlOcspRef GetInstance(object obj)
		{
			if (obj == null || obj is CrlOcspRef)
			{
				return (CrlOcspRef)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new CrlOcspRef((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in 'CrlOcspRef' factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06000BA5 RID: 2981 RVA: 0x0004CB7C File Offset: 0x0004CB7C
		private CrlOcspRef(Asn1Sequence seq)
		{
			if (seq == null)
			{
				throw new ArgumentNullException("seq");
			}
			foreach (object obj in seq)
			{
				Asn1TaggedObject asn1TaggedObject = (Asn1TaggedObject)obj;
				Asn1Object @object = asn1TaggedObject.GetObject();
				switch (asn1TaggedObject.TagNo)
				{
				case 0:
					this.crlids = CrlListID.GetInstance(@object);
					break;
				case 1:
					this.ocspids = OcspListID.GetInstance(@object);
					break;
				case 2:
					this.otherRev = OtherRevRefs.GetInstance(@object);
					break;
				default:
					throw new ArgumentException("Illegal tag in CrlOcspRef", "seq");
				}
			}
		}

		// Token: 0x06000BA6 RID: 2982 RVA: 0x0004CC54 File Offset: 0x0004CC54
		public CrlOcspRef(CrlListID crlids, OcspListID ocspids, OtherRevRefs otherRev)
		{
			this.crlids = crlids;
			this.ocspids = ocspids;
			this.otherRev = otherRev;
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06000BA7 RID: 2983 RVA: 0x0004CC74 File Offset: 0x0004CC74
		public CrlListID CrlIDs
		{
			get
			{
				return this.crlids;
			}
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06000BA8 RID: 2984 RVA: 0x0004CC7C File Offset: 0x0004CC7C
		public OcspListID OcspIDs
		{
			get
			{
				return this.ocspids;
			}
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06000BA9 RID: 2985 RVA: 0x0004CC84 File Offset: 0x0004CC84
		public OtherRevRefs OtherRev
		{
			get
			{
				return this.otherRev;
			}
		}

		// Token: 0x06000BAA RID: 2986 RVA: 0x0004CC8C File Offset: 0x0004CC8C
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector();
			if (this.crlids != null)
			{
				asn1EncodableVector.Add(new DerTaggedObject(true, 0, this.crlids.ToAsn1Object()));
			}
			if (this.ocspids != null)
			{
				asn1EncodableVector.Add(new DerTaggedObject(true, 1, this.ocspids.ToAsn1Object()));
			}
			if (this.otherRev != null)
			{
				asn1EncodableVector.Add(new DerTaggedObject(true, 2, this.otherRev.ToAsn1Object()));
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x040007FA RID: 2042
		private readonly CrlListID crlids;

		// Token: 0x040007FB RID: 2043
		private readonly OcspListID ocspids;

		// Token: 0x040007FC RID: 2044
		private readonly OtherRevRefs otherRev;
	}
}
