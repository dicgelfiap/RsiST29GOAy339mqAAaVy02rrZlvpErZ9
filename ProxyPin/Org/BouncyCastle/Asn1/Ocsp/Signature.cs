using System;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Ocsp
{
	// Token: 0x0200019C RID: 412
	public class Signature : Asn1Encodable
	{
		// Token: 0x06000D87 RID: 3463 RVA: 0x0005458C File Offset: 0x0005458C
		public static Signature GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return Signature.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x06000D88 RID: 3464 RVA: 0x0005459C File Offset: 0x0005459C
		public static Signature GetInstance(object obj)
		{
			if (obj == null || obj is Signature)
			{
				return (Signature)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new Signature((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06000D89 RID: 3465 RVA: 0x000545F8 File Offset: 0x000545F8
		public Signature(AlgorithmIdentifier signatureAlgorithm, DerBitString signatureValue) : this(signatureAlgorithm, signatureValue, null)
		{
		}

		// Token: 0x06000D8A RID: 3466 RVA: 0x00054604 File Offset: 0x00054604
		public Signature(AlgorithmIdentifier signatureAlgorithm, DerBitString signatureValue, Asn1Sequence certs)
		{
			if (signatureAlgorithm == null)
			{
				throw new ArgumentException("signatureAlgorithm");
			}
			if (signatureValue == null)
			{
				throw new ArgumentException("signatureValue");
			}
			this.signatureAlgorithm = signatureAlgorithm;
			this.signatureValue = signatureValue;
			this.certs = certs;
		}

		// Token: 0x06000D8B RID: 3467 RVA: 0x00054644 File Offset: 0x00054644
		private Signature(Asn1Sequence seq)
		{
			this.signatureAlgorithm = AlgorithmIdentifier.GetInstance(seq[0]);
			this.signatureValue = (DerBitString)seq[1];
			if (seq.Count == 3)
			{
				this.certs = Asn1Sequence.GetInstance((Asn1TaggedObject)seq[2], true);
			}
		}

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06000D8C RID: 3468 RVA: 0x000546A4 File Offset: 0x000546A4
		public AlgorithmIdentifier SignatureAlgorithm
		{
			get
			{
				return this.signatureAlgorithm;
			}
		}

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06000D8D RID: 3469 RVA: 0x000546AC File Offset: 0x000546AC
		public DerBitString SignatureValue
		{
			get
			{
				return this.signatureValue;
			}
		}

		// Token: 0x06000D8E RID: 3470 RVA: 0x000546B4 File Offset: 0x000546B4
		public byte[] GetSignatureOctets()
		{
			return this.signatureValue.GetOctets();
		}

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06000D8F RID: 3471 RVA: 0x000546C4 File Offset: 0x000546C4
		public Asn1Sequence Certs
		{
			get
			{
				return this.certs;
			}
		}

		// Token: 0x06000D90 RID: 3472 RVA: 0x000546CC File Offset: 0x000546CC
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.signatureAlgorithm,
				this.signatureValue
			});
			asn1EncodableVector.AddOptionalTagged(true, 0, this.certs);
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x040009AE RID: 2478
		internal AlgorithmIdentifier signatureAlgorithm;

		// Token: 0x040009AF RID: 2479
		internal DerBitString signatureValue;

		// Token: 0x040009B0 RID: 2480
		internal Asn1Sequence certs;
	}
}
