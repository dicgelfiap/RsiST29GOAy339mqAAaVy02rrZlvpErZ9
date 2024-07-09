using System;
using Org.BouncyCastle.Asn1.Cmp;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Cms;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.X509;

namespace Org.BouncyCastle.Cmp
{
	// Token: 0x020002C9 RID: 713
	public class CertificateStatus
	{
		// Token: 0x060015BC RID: 5564 RVA: 0x00072930 File Offset: 0x00072930
		public CertificateStatus(DefaultDigestAlgorithmIdentifierFinder digestAlgFinder, CertStatus certStatus)
		{
			this.digestAlgFinder = digestAlgFinder;
			this.certStatus = certStatus;
		}

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x060015BD RID: 5565 RVA: 0x00072948 File Offset: 0x00072948
		public PkiStatusInfo PkiStatusInfo
		{
			get
			{
				return this.certStatus.StatusInfo;
			}
		}

		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x060015BE RID: 5566 RVA: 0x00072958 File Offset: 0x00072958
		public BigInteger CertRequestId
		{
			get
			{
				return this.certStatus.CertReqID.Value;
			}
		}

		// Token: 0x060015BF RID: 5567 RVA: 0x0007296C File Offset: 0x0007296C
		public bool IsVerified(X509Certificate cert)
		{
			AlgorithmIdentifier algorithmIdentifier = this.digestAlgFinder.find(CertificateStatus.sigAlgFinder.Find(cert.SigAlgName));
			if (algorithmIdentifier == null)
			{
				throw new CmpException("cannot find algorithm for digest from signature " + cert.SigAlgName);
			}
			byte[] b = DigestUtilities.CalculateDigest(algorithmIdentifier.Algorithm, cert.GetEncoded());
			return Arrays.ConstantTimeAreEqual(this.certStatus.CertHash.GetOctets(), b);
		}

		// Token: 0x04000EE2 RID: 3810
		private static readonly DefaultSignatureAlgorithmIdentifierFinder sigAlgFinder = new DefaultSignatureAlgorithmIdentifierFinder();

		// Token: 0x04000EE3 RID: 3811
		private readonly DefaultDigestAlgorithmIdentifierFinder digestAlgFinder;

		// Token: 0x04000EE4 RID: 3812
		private readonly CertStatus certStatus;
	}
}
