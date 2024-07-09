using System;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.IsisMtt.Ocsp
{
	// Token: 0x02000174 RID: 372
	public class CertHash : Asn1Encodable
	{
		// Token: 0x06000C8A RID: 3210 RVA: 0x0005079C File Offset: 0x0005079C
		public static CertHash GetInstance(object obj)
		{
			if (obj == null || obj is CertHash)
			{
				return (CertHash)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new CertHash((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06000C8B RID: 3211 RVA: 0x000507F8 File Offset: 0x000507F8
		private CertHash(Asn1Sequence seq)
		{
			if (seq.Count != 2)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count);
			}
			this.hashAlgorithm = AlgorithmIdentifier.GetInstance(seq[0]);
			this.certificateHash = Asn1OctetString.GetInstance(seq[1]).GetOctets();
		}

		// Token: 0x06000C8C RID: 3212 RVA: 0x00050860 File Offset: 0x00050860
		public CertHash(AlgorithmIdentifier hashAlgorithm, byte[] certificateHash)
		{
			if (hashAlgorithm == null)
			{
				throw new ArgumentNullException("hashAlgorithm");
			}
			if (certificateHash == null)
			{
				throw new ArgumentNullException("certificateHash");
			}
			this.hashAlgorithm = hashAlgorithm;
			this.certificateHash = (byte[])certificateHash.Clone();
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06000C8D RID: 3213 RVA: 0x000508B4 File Offset: 0x000508B4
		public AlgorithmIdentifier HashAlgorithm
		{
			get
			{
				return this.hashAlgorithm;
			}
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06000C8E RID: 3214 RVA: 0x000508BC File Offset: 0x000508BC
		public byte[] CertificateHash
		{
			get
			{
				return (byte[])this.certificateHash.Clone();
			}
		}

		// Token: 0x06000C8F RID: 3215 RVA: 0x000508D0 File Offset: 0x000508D0
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.hashAlgorithm,
				new DerOctetString(this.certificateHash)
			});
		}

		// Token: 0x040008B4 RID: 2228
		private readonly AlgorithmIdentifier hashAlgorithm;

		// Token: 0x040008B5 RID: 2229
		private readonly byte[] certificateHash;
	}
}
