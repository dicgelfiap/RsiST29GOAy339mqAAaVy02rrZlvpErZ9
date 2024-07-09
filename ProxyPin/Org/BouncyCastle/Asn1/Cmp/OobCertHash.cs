using System;
using Org.BouncyCastle.Asn1.Crmf;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Cmp
{
	// Token: 0x020000DE RID: 222
	public class OobCertHash : Asn1Encodable
	{
		// Token: 0x0600084F RID: 2127 RVA: 0x00041F04 File Offset: 0x00041F04
		private OobCertHash(Asn1Sequence seq)
		{
			int num = seq.Count - 1;
			this.hashVal = DerBitString.GetInstance(seq[num--]);
			for (int i = num; i >= 0; i--)
			{
				Asn1TaggedObject asn1TaggedObject = (Asn1TaggedObject)seq[i];
				if (asn1TaggedObject.TagNo == 0)
				{
					this.hashAlg = AlgorithmIdentifier.GetInstance(asn1TaggedObject, true);
				}
				else
				{
					this.certId = CertId.GetInstance(asn1TaggedObject, true);
				}
			}
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x00041F80 File Offset: 0x00041F80
		public static OobCertHash GetInstance(object obj)
		{
			if (obj is OobCertHash)
			{
				return (OobCertHash)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new OobCertHash((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid object: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000851 RID: 2129 RVA: 0x00041FD4 File Offset: 0x00041FD4
		public virtual AlgorithmIdentifier HashAlg
		{
			get
			{
				return this.hashAlg;
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06000852 RID: 2130 RVA: 0x00041FDC File Offset: 0x00041FDC
		public virtual CertId CertID
		{
			get
			{
				return this.certId;
			}
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x00041FE4 File Offset: 0x00041FE4
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector();
			asn1EncodableVector.AddOptionalTagged(true, 0, this.hashAlg);
			asn1EncodableVector.AddOptionalTagged(true, 1, this.certId);
			asn1EncodableVector.Add(this.hashVal);
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04000615 RID: 1557
		private readonly AlgorithmIdentifier hashAlg;

		// Token: 0x04000616 RID: 1558
		private readonly CertId certId;

		// Token: 0x04000617 RID: 1559
		private readonly DerBitString hashVal;
	}
}
