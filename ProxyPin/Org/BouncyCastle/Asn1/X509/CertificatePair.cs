using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x020001EC RID: 492
	public class CertificatePair : Asn1Encodable
	{
		// Token: 0x06000FD9 RID: 4057 RVA: 0x0005D8BC File Offset: 0x0005D8BC
		public static CertificatePair GetInstance(object obj)
		{
			if (obj == null || obj is CertificatePair)
			{
				return (CertificatePair)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new CertificatePair((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06000FDA RID: 4058 RVA: 0x0005D918 File Offset: 0x0005D918
		private CertificatePair(Asn1Sequence seq)
		{
			if (seq.Count != 1 && seq.Count != 2)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count, "seq");
			}
			foreach (object obj in seq)
			{
				Asn1TaggedObject instance = Asn1TaggedObject.GetInstance(obj);
				if (instance.TagNo == 0)
				{
					this.forward = X509CertificateStructure.GetInstance(instance, true);
				}
				else
				{
					if (instance.TagNo != 1)
					{
						throw new ArgumentException("Bad tag number: " + instance.TagNo);
					}
					this.reverse = X509CertificateStructure.GetInstance(instance, true);
				}
			}
		}

		// Token: 0x06000FDB RID: 4059 RVA: 0x0005DA04 File Offset: 0x0005DA04
		public CertificatePair(X509CertificateStructure forward, X509CertificateStructure reverse)
		{
			this.forward = forward;
			this.reverse = reverse;
		}

		// Token: 0x06000FDC RID: 4060 RVA: 0x0005DA1C File Offset: 0x0005DA1C
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector();
			asn1EncodableVector.AddOptionalTagged(true, 0, this.forward);
			asn1EncodableVector.AddOptionalTagged(true, 1, this.reverse);
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x06000FDD RID: 4061 RVA: 0x0005DA58 File Offset: 0x0005DA58
		public X509CertificateStructure Forward
		{
			get
			{
				return this.forward;
			}
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x06000FDE RID: 4062 RVA: 0x0005DA60 File Offset: 0x0005DA60
		public X509CertificateStructure Reverse
		{
			get
			{
				return this.reverse;
			}
		}

		// Token: 0x04000BB6 RID: 2998
		private X509CertificateStructure forward;

		// Token: 0x04000BB7 RID: 2999
		private X509CertificateStructure reverse;
	}
}
