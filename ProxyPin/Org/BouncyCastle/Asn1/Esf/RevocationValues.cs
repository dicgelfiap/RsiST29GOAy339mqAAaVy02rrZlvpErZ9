using System;
using System.Collections;
using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Utilities.Collections;

namespace Org.BouncyCastle.Asn1.Esf
{
	// Token: 0x0200015D RID: 349
	public class RevocationValues : Asn1Encodable
	{
		// Token: 0x06000BF1 RID: 3057 RVA: 0x0004DE50 File Offset: 0x0004DE50
		public static RevocationValues GetInstance(object obj)
		{
			if (obj == null || obj is RevocationValues)
			{
				return (RevocationValues)obj;
			}
			return new RevocationValues(Asn1Sequence.GetInstance(obj));
		}

		// Token: 0x06000BF2 RID: 3058 RVA: 0x0004DE78 File Offset: 0x0004DE78
		private RevocationValues(Asn1Sequence seq)
		{
			if (seq == null)
			{
				throw new ArgumentNullException("seq");
			}
			if (seq.Count > 3)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count, "seq");
			}
			foreach (object obj in seq)
			{
				Asn1TaggedObject asn1TaggedObject = (Asn1TaggedObject)obj;
				Asn1Object @object = asn1TaggedObject.GetObject();
				switch (asn1TaggedObject.TagNo)
				{
				case 0:
				{
					Asn1Sequence asn1Sequence = (Asn1Sequence)@object;
					foreach (object obj2 in asn1Sequence)
					{
						Asn1Encodable asn1Encodable = (Asn1Encodable)obj2;
						CertificateList.GetInstance(asn1Encodable.ToAsn1Object());
					}
					this.crlVals = asn1Sequence;
					break;
				}
				case 1:
				{
					Asn1Sequence asn1Sequence2 = (Asn1Sequence)@object;
					foreach (object obj3 in asn1Sequence2)
					{
						Asn1Encodable asn1Encodable2 = (Asn1Encodable)obj3;
						BasicOcspResponse.GetInstance(asn1Encodable2.ToAsn1Object());
					}
					this.ocspVals = asn1Sequence2;
					break;
				}
				case 2:
					this.otherRevVals = OtherRevVals.GetInstance(@object);
					break;
				default:
					throw new ArgumentException("Illegal tag in RevocationValues", "seq");
				}
			}
		}

		// Token: 0x06000BF3 RID: 3059 RVA: 0x0004E064 File Offset: 0x0004E064
		public RevocationValues(CertificateList[] crlVals, BasicOcspResponse[] ocspVals, OtherRevVals otherRevVals)
		{
			if (crlVals != null)
			{
				this.crlVals = new DerSequence(crlVals);
			}
			if (ocspVals != null)
			{
				this.ocspVals = new DerSequence(ocspVals);
			}
			this.otherRevVals = otherRevVals;
		}

		// Token: 0x06000BF4 RID: 3060 RVA: 0x0004E098 File Offset: 0x0004E098
		public RevocationValues(IEnumerable crlVals, IEnumerable ocspVals, OtherRevVals otherRevVals)
		{
			if (crlVals != null)
			{
				if (!CollectionUtilities.CheckElementsAreOfType(crlVals, typeof(CertificateList)))
				{
					throw new ArgumentException("Must contain only 'CertificateList' objects", "crlVals");
				}
				this.crlVals = new DerSequence(Asn1EncodableVector.FromEnumerable(crlVals));
			}
			if (ocspVals != null)
			{
				if (!CollectionUtilities.CheckElementsAreOfType(ocspVals, typeof(BasicOcspResponse)))
				{
					throw new ArgumentException("Must contain only 'BasicOcspResponse' objects", "ocspVals");
				}
				this.ocspVals = new DerSequence(Asn1EncodableVector.FromEnumerable(ocspVals));
			}
			this.otherRevVals = otherRevVals;
		}

		// Token: 0x06000BF5 RID: 3061 RVA: 0x0004E130 File Offset: 0x0004E130
		public CertificateList[] GetCrlVals()
		{
			CertificateList[] array = new CertificateList[this.crlVals.Count];
			for (int i = 0; i < this.crlVals.Count; i++)
			{
				array[i] = CertificateList.GetInstance(this.crlVals[i].ToAsn1Object());
			}
			return array;
		}

		// Token: 0x06000BF6 RID: 3062 RVA: 0x0004E18C File Offset: 0x0004E18C
		public BasicOcspResponse[] GetOcspVals()
		{
			BasicOcspResponse[] array = new BasicOcspResponse[this.ocspVals.Count];
			for (int i = 0; i < this.ocspVals.Count; i++)
			{
				array[i] = BasicOcspResponse.GetInstance(this.ocspVals[i].ToAsn1Object());
			}
			return array;
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06000BF7 RID: 3063 RVA: 0x0004E1E8 File Offset: 0x0004E1E8
		public OtherRevVals OtherRevVals
		{
			get
			{
				return this.otherRevVals;
			}
		}

		// Token: 0x06000BF8 RID: 3064 RVA: 0x0004E1F0 File Offset: 0x0004E1F0
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector();
			asn1EncodableVector.AddOptionalTagged(true, 0, this.crlVals);
			asn1EncodableVector.AddOptionalTagged(true, 1, this.ocspVals);
			if (this.otherRevVals != null)
			{
				asn1EncodableVector.Add(new DerTaggedObject(true, 2, this.otherRevVals.ToAsn1Object()));
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x0400081E RID: 2078
		private readonly Asn1Sequence crlVals;

		// Token: 0x0400081F RID: 2079
		private readonly Asn1Sequence ocspVals;

		// Token: 0x04000820 RID: 2080
		private readonly OtherRevVals otherRevVals;
	}
}
