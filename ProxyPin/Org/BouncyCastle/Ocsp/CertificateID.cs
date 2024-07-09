using System;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509;

namespace Org.BouncyCastle.Ocsp
{
	// Token: 0x02000634 RID: 1588
	public class CertificateID
	{
		// Token: 0x06003764 RID: 14180 RVA: 0x001297D4 File Offset: 0x001297D4
		public CertificateID(CertID id)
		{
			if (id == null)
			{
				throw new ArgumentNullException("id");
			}
			this.id = id;
		}

		// Token: 0x06003765 RID: 14181 RVA: 0x001297F4 File Offset: 0x001297F4
		public CertificateID(string hashAlgorithm, X509Certificate issuerCert, BigInteger serialNumber)
		{
			AlgorithmIdentifier hashAlg = new AlgorithmIdentifier(new DerObjectIdentifier(hashAlgorithm), DerNull.Instance);
			this.id = CertificateID.CreateCertID(hashAlg, issuerCert, new DerInteger(serialNumber));
		}

		// Token: 0x170009A1 RID: 2465
		// (get) Token: 0x06003766 RID: 14182 RVA: 0x00129830 File Offset: 0x00129830
		public string HashAlgOid
		{
			get
			{
				return this.id.HashAlgorithm.Algorithm.Id;
			}
		}

		// Token: 0x06003767 RID: 14183 RVA: 0x00129848 File Offset: 0x00129848
		public byte[] GetIssuerNameHash()
		{
			return this.id.IssuerNameHash.GetOctets();
		}

		// Token: 0x06003768 RID: 14184 RVA: 0x0012985C File Offset: 0x0012985C
		public byte[] GetIssuerKeyHash()
		{
			return this.id.IssuerKeyHash.GetOctets();
		}

		// Token: 0x170009A2 RID: 2466
		// (get) Token: 0x06003769 RID: 14185 RVA: 0x00129870 File Offset: 0x00129870
		public BigInteger SerialNumber
		{
			get
			{
				return this.id.SerialNumber.Value;
			}
		}

		// Token: 0x0600376A RID: 14186 RVA: 0x00129884 File Offset: 0x00129884
		public bool MatchesIssuer(X509Certificate issuerCert)
		{
			return CertificateID.CreateCertID(this.id.HashAlgorithm, issuerCert, this.id.SerialNumber).Equals(this.id);
		}

		// Token: 0x0600376B RID: 14187 RVA: 0x001298B0 File Offset: 0x001298B0
		public CertID ToAsn1Object()
		{
			return this.id;
		}

		// Token: 0x0600376C RID: 14188 RVA: 0x001298B8 File Offset: 0x001298B8
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			CertificateID certificateID = obj as CertificateID;
			return certificateID != null && this.id.ToAsn1Object().Equals(certificateID.id.ToAsn1Object());
		}

		// Token: 0x0600376D RID: 14189 RVA: 0x001298FC File Offset: 0x001298FC
		public override int GetHashCode()
		{
			return this.id.ToAsn1Object().GetHashCode();
		}

		// Token: 0x0600376E RID: 14190 RVA: 0x00129910 File Offset: 0x00129910
		public static CertificateID DeriveCertificateID(CertificateID original, BigInteger newSerialNumber)
		{
			return new CertificateID(new CertID(original.id.HashAlgorithm, original.id.IssuerNameHash, original.id.IssuerKeyHash, new DerInteger(newSerialNumber)));
		}

		// Token: 0x0600376F RID: 14191 RVA: 0x00129954 File Offset: 0x00129954
		private static CertID CreateCertID(AlgorithmIdentifier hashAlg, X509Certificate issuerCert, DerInteger serialNumber)
		{
			CertID result;
			try
			{
				string algorithm = hashAlg.Algorithm.Id;
				X509Name subjectX509Principal = PrincipalUtilities.GetSubjectX509Principal(issuerCert);
				byte[] str = DigestUtilities.CalculateDigest(algorithm, subjectX509Principal.GetEncoded());
				AsymmetricKeyParameter publicKey = issuerCert.GetPublicKey();
				SubjectPublicKeyInfo subjectPublicKeyInfo = SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(publicKey);
				byte[] str2 = DigestUtilities.CalculateDigest(algorithm, subjectPublicKeyInfo.PublicKeyData.GetBytes());
				result = new CertID(hashAlg, new DerOctetString(str), new DerOctetString(str2), serialNumber);
			}
			catch (Exception ex)
			{
				throw new OcspException("problem creating ID: " + ex, ex);
			}
			return result;
		}

		// Token: 0x04001D54 RID: 7508
		public const string HashSha1 = "1.3.14.3.2.26";

		// Token: 0x04001D55 RID: 7509
		private readonly CertID id;
	}
}
