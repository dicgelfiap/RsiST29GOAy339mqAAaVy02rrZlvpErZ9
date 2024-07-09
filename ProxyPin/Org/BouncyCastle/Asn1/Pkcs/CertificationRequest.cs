using System;
using Org.BouncyCastle.Asn1.X509;

namespace Org.BouncyCastle.Asn1.Pkcs
{
	// Token: 0x020001A4 RID: 420
	public class CertificationRequest : Asn1Encodable
	{
		// Token: 0x06000DBE RID: 3518 RVA: 0x00054FB8 File Offset: 0x00054FB8
		public static CertificationRequest GetInstance(object obj)
		{
			if (obj is CertificationRequest)
			{
				return (CertificationRequest)obj;
			}
			if (obj != null)
			{
				return new CertificationRequest((Asn1Sequence)obj);
			}
			return null;
		}

		// Token: 0x06000DBF RID: 3519 RVA: 0x00054FE0 File Offset: 0x00054FE0
		protected CertificationRequest()
		{
		}

		// Token: 0x06000DC0 RID: 3520 RVA: 0x00054FE8 File Offset: 0x00054FE8
		public CertificationRequest(CertificationRequestInfo requestInfo, AlgorithmIdentifier algorithm, DerBitString signature)
		{
			this.reqInfo = requestInfo;
			this.sigAlgId = algorithm;
			this.sigBits = signature;
		}

		// Token: 0x06000DC1 RID: 3521 RVA: 0x00055008 File Offset: 0x00055008
		[Obsolete("Use 'GetInstance' instead")]
		public CertificationRequest(Asn1Sequence seq)
		{
			if (seq.Count != 3)
			{
				throw new ArgumentException("Wrong number of elements in sequence", "seq");
			}
			this.reqInfo = CertificationRequestInfo.GetInstance(seq[0]);
			this.sigAlgId = AlgorithmIdentifier.GetInstance(seq[1]);
			this.sigBits = DerBitString.GetInstance(seq[2]);
		}

		// Token: 0x06000DC2 RID: 3522 RVA: 0x00055074 File Offset: 0x00055074
		public CertificationRequestInfo GetCertificationRequestInfo()
		{
			return this.reqInfo;
		}

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06000DC3 RID: 3523 RVA: 0x0005507C File Offset: 0x0005507C
		public AlgorithmIdentifier SignatureAlgorithm
		{
			get
			{
				return this.sigAlgId;
			}
		}

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06000DC4 RID: 3524 RVA: 0x00055084 File Offset: 0x00055084
		public DerBitString Signature
		{
			get
			{
				return this.sigBits;
			}
		}

		// Token: 0x06000DC5 RID: 3525 RVA: 0x0005508C File Offset: 0x0005508C
		public byte[] GetSignatureOctets()
		{
			return this.sigBits.GetOctets();
		}

		// Token: 0x06000DC6 RID: 3526 RVA: 0x0005509C File Offset: 0x0005509C
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.reqInfo,
				this.sigAlgId,
				this.sigBits
			});
		}

		// Token: 0x040009D0 RID: 2512
		protected CertificationRequestInfo reqInfo;

		// Token: 0x040009D1 RID: 2513
		protected AlgorithmIdentifier sigAlgId;

		// Token: 0x040009D2 RID: 2514
		protected DerBitString sigBits;
	}
}
