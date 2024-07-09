using System;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Ocsp
{
	// Token: 0x0200018E RID: 398
	public class CertID : Asn1Encodable
	{
		// Token: 0x06000D20 RID: 3360 RVA: 0x000532B4 File Offset: 0x000532B4
		public static CertID GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return CertID.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x06000D21 RID: 3361 RVA: 0x000532C4 File Offset: 0x000532C4
		public static CertID GetInstance(object obj)
		{
			if (obj == null || obj is CertID)
			{
				return (CertID)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new CertID((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06000D22 RID: 3362 RVA: 0x00053320 File Offset: 0x00053320
		public CertID(AlgorithmIdentifier hashAlgorithm, Asn1OctetString issuerNameHash, Asn1OctetString issuerKeyHash, DerInteger serialNumber)
		{
			this.hashAlgorithm = hashAlgorithm;
			this.issuerNameHash = issuerNameHash;
			this.issuerKeyHash = issuerKeyHash;
			this.serialNumber = serialNumber;
		}

		// Token: 0x06000D23 RID: 3363 RVA: 0x00053348 File Offset: 0x00053348
		private CertID(Asn1Sequence seq)
		{
			if (seq.Count != 4)
			{
				throw new ArgumentException("Wrong number of elements in sequence", "seq");
			}
			this.hashAlgorithm = AlgorithmIdentifier.GetInstance(seq[0]);
			this.issuerNameHash = Asn1OctetString.GetInstance(seq[1]);
			this.issuerKeyHash = Asn1OctetString.GetInstance(seq[2]);
			this.serialNumber = DerInteger.GetInstance(seq[3]);
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06000D24 RID: 3364 RVA: 0x000533C4 File Offset: 0x000533C4
		public AlgorithmIdentifier HashAlgorithm
		{
			get
			{
				return this.hashAlgorithm;
			}
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06000D25 RID: 3365 RVA: 0x000533CC File Offset: 0x000533CC
		public Asn1OctetString IssuerNameHash
		{
			get
			{
				return this.issuerNameHash;
			}
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06000D26 RID: 3366 RVA: 0x000533D4 File Offset: 0x000533D4
		public Asn1OctetString IssuerKeyHash
		{
			get
			{
				return this.issuerKeyHash;
			}
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06000D27 RID: 3367 RVA: 0x000533DC File Offset: 0x000533DC
		public DerInteger SerialNumber
		{
			get
			{
				return this.serialNumber;
			}
		}

		// Token: 0x06000D28 RID: 3368 RVA: 0x000533E4 File Offset: 0x000533E4
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.hashAlgorithm,
				this.issuerNameHash,
				this.issuerKeyHash,
				this.serialNumber
			});
		}

		// Token: 0x0400097F RID: 2431
		private readonly AlgorithmIdentifier hashAlgorithm;

		// Token: 0x04000980 RID: 2432
		private readonly Asn1OctetString issuerNameHash;

		// Token: 0x04000981 RID: 2433
		private readonly Asn1OctetString issuerKeyHash;

		// Token: 0x04000982 RID: 2434
		private readonly DerInteger serialNumber;
	}
}
