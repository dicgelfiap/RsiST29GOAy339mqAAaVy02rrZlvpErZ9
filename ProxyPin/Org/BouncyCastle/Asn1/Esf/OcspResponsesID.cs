using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Esf
{
	// Token: 0x02000156 RID: 342
	public class OcspResponsesID : Asn1Encodable
	{
		// Token: 0x06000BC0 RID: 3008 RVA: 0x0004D2BC File Offset: 0x0004D2BC
		public static OcspResponsesID GetInstance(object obj)
		{
			if (obj == null || obj is OcspResponsesID)
			{
				return (OcspResponsesID)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new OcspResponsesID((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in 'OcspResponsesID' factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06000BC1 RID: 3009 RVA: 0x0004D318 File Offset: 0x0004D318
		private OcspResponsesID(Asn1Sequence seq)
		{
			if (seq == null)
			{
				throw new ArgumentNullException("seq");
			}
			if (seq.Count < 1 || seq.Count > 2)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count, "seq");
			}
			this.ocspIdentifier = OcspIdentifier.GetInstance(seq[0].ToAsn1Object());
			if (seq.Count > 1)
			{
				this.ocspRepHash = OtherHash.GetInstance(seq[1].ToAsn1Object());
			}
		}

		// Token: 0x06000BC2 RID: 3010 RVA: 0x0004D3B4 File Offset: 0x0004D3B4
		public OcspResponsesID(OcspIdentifier ocspIdentifier) : this(ocspIdentifier, null)
		{
		}

		// Token: 0x06000BC3 RID: 3011 RVA: 0x0004D3C0 File Offset: 0x0004D3C0
		public OcspResponsesID(OcspIdentifier ocspIdentifier, OtherHash ocspRepHash)
		{
			if (ocspIdentifier == null)
			{
				throw new ArgumentNullException("ocspIdentifier");
			}
			this.ocspIdentifier = ocspIdentifier;
			this.ocspRepHash = ocspRepHash;
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06000BC4 RID: 3012 RVA: 0x0004D3E8 File Offset: 0x0004D3E8
		public OcspIdentifier OcspIdentifier
		{
			get
			{
				return this.ocspIdentifier;
			}
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06000BC5 RID: 3013 RVA: 0x0004D3F0 File Offset: 0x0004D3F0
		public OtherHash OcspRepHash
		{
			get
			{
				return this.ocspRepHash;
			}
		}

		// Token: 0x06000BC6 RID: 3014 RVA: 0x0004D3F8 File Offset: 0x0004D3F8
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.ocspIdentifier.ToAsn1Object()
			});
			if (this.ocspRepHash != null)
			{
				asn1EncodableVector.Add(this.ocspRepHash.ToAsn1Object());
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04000810 RID: 2064
		private readonly OcspIdentifier ocspIdentifier;

		// Token: 0x04000811 RID: 2065
		private readonly OtherHash ocspRepHash;
	}
}
