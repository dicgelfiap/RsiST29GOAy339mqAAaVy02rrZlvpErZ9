using System;
using System.Collections;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Cmp;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Cms;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.X509;

namespace Org.BouncyCastle.Cmp
{
	// Token: 0x020002C8 RID: 712
	public class CertificateConfirmationContentBuilder
	{
		// Token: 0x060015B7 RID: 5559 RVA: 0x00072808 File Offset: 0x00072808
		public CertificateConfirmationContentBuilder() : this(new DefaultDigestAlgorithmIdentifierFinder())
		{
		}

		// Token: 0x060015B8 RID: 5560 RVA: 0x00072818 File Offset: 0x00072818
		public CertificateConfirmationContentBuilder(DefaultDigestAlgorithmIdentifierFinder digestAlgFinder)
		{
			this.digestAlgFinder = digestAlgFinder;
		}

		// Token: 0x060015B9 RID: 5561 RVA: 0x00072840 File Offset: 0x00072840
		public CertificateConfirmationContentBuilder AddAcceptedCertificate(X509Certificate certHolder, BigInteger certReqId)
		{
			this.acceptedCerts.Add(certHolder);
			this.acceptedReqIds.Add(certReqId);
			return this;
		}

		// Token: 0x060015BA RID: 5562 RVA: 0x00072860 File Offset: 0x00072860
		public CertificateConfirmationContent Build()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector();
			for (int num = 0; num != this.acceptedCerts.Count; num++)
			{
				X509Certificate x509Certificate = (X509Certificate)this.acceptedCerts[num];
				BigInteger certReqId = (BigInteger)this.acceptedReqIds[num];
				AlgorithmIdentifier sigAlgId = CertificateConfirmationContentBuilder.sigAlgFinder.Find(x509Certificate.SigAlgName);
				AlgorithmIdentifier algorithmIdentifier = this.digestAlgFinder.find(sigAlgId);
				if (algorithmIdentifier == null)
				{
					throw new CmpException("cannot find algorithm for digest from signature");
				}
				byte[] certHash = DigestUtilities.CalculateDigest(algorithmIdentifier.Algorithm, x509Certificate.GetEncoded());
				asn1EncodableVector.Add(new CertStatus(certHash, certReqId));
			}
			return new CertificateConfirmationContent(CertConfirmContent.GetInstance(new DerSequence(asn1EncodableVector)), this.digestAlgFinder);
		}

		// Token: 0x04000EDE RID: 3806
		private static readonly DefaultSignatureAlgorithmIdentifierFinder sigAlgFinder = new DefaultSignatureAlgorithmIdentifierFinder();

		// Token: 0x04000EDF RID: 3807
		private readonly DefaultDigestAlgorithmIdentifierFinder digestAlgFinder;

		// Token: 0x04000EE0 RID: 3808
		private readonly IList acceptedCerts = Platform.CreateArrayList();

		// Token: 0x04000EE1 RID: 3809
		private readonly IList acceptedReqIds = Platform.CreateArrayList();
	}
}
