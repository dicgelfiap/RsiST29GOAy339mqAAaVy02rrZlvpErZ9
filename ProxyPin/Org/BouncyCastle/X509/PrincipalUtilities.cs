using System;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Security.Certificates;

namespace Org.BouncyCastle.X509
{
	// Token: 0x02000714 RID: 1812
	public class PrincipalUtilities
	{
		// Token: 0x06003F6A RID: 16234 RVA: 0x0015B9F8 File Offset: 0x0015B9F8
		public static X509Name GetIssuerX509Principal(X509Certificate cert)
		{
			X509Name issuer;
			try
			{
				TbsCertificateStructure instance = TbsCertificateStructure.GetInstance(Asn1Object.FromByteArray(cert.GetTbsCertificate()));
				issuer = instance.Issuer;
			}
			catch (Exception e)
			{
				throw new CertificateEncodingException("Could not extract issuer", e);
			}
			return issuer;
		}

		// Token: 0x06003F6B RID: 16235 RVA: 0x0015BA40 File Offset: 0x0015BA40
		public static X509Name GetSubjectX509Principal(X509Certificate cert)
		{
			X509Name subject;
			try
			{
				TbsCertificateStructure instance = TbsCertificateStructure.GetInstance(Asn1Object.FromByteArray(cert.GetTbsCertificate()));
				subject = instance.Subject;
			}
			catch (Exception e)
			{
				throw new CertificateEncodingException("Could not extract subject", e);
			}
			return subject;
		}

		// Token: 0x06003F6C RID: 16236 RVA: 0x0015BA88 File Offset: 0x0015BA88
		public static X509Name GetIssuerX509Principal(X509Crl crl)
		{
			X509Name issuer;
			try
			{
				TbsCertificateList instance = TbsCertificateList.GetInstance(Asn1Object.FromByteArray(crl.GetTbsCertList()));
				issuer = instance.Issuer;
			}
			catch (Exception e)
			{
				throw new CrlException("Could not extract issuer", e);
			}
			return issuer;
		}
	}
}
