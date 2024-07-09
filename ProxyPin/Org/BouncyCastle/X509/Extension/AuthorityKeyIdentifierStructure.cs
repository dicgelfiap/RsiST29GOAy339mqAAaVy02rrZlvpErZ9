using System;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Security.Certificates;

namespace Org.BouncyCastle.X509.Extension
{
	// Token: 0x02000703 RID: 1795
	public class AuthorityKeyIdentifierStructure : AuthorityKeyIdentifier
	{
		// Token: 0x06003EDE RID: 16094 RVA: 0x00159EA4 File Offset: 0x00159EA4
		public AuthorityKeyIdentifierStructure(Asn1OctetString encodedValue) : base((Asn1Sequence)X509ExtensionUtilities.FromExtensionValue(encodedValue))
		{
		}

		// Token: 0x06003EDF RID: 16095 RVA: 0x00159EB8 File Offset: 0x00159EB8
		private static Asn1Sequence FromCertificate(X509Certificate certificate)
		{
			Asn1Sequence result;
			try
			{
				GeneralName name = new GeneralName(PrincipalUtilities.GetIssuerX509Principal(certificate));
				if (certificate.Version == 3)
				{
					Asn1OctetString extensionValue = certificate.GetExtensionValue(X509Extensions.SubjectKeyIdentifier);
					if (extensionValue != null)
					{
						Asn1OctetString asn1OctetString = (Asn1OctetString)X509ExtensionUtilities.FromExtensionValue(extensionValue);
						return (Asn1Sequence)new AuthorityKeyIdentifier(asn1OctetString.GetOctets(), new GeneralNames(name), certificate.SerialNumber).ToAsn1Object();
					}
				}
				SubjectPublicKeyInfo spki = SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(certificate.GetPublicKey());
				result = (Asn1Sequence)new AuthorityKeyIdentifier(spki, new GeneralNames(name), certificate.SerialNumber).ToAsn1Object();
			}
			catch (Exception exception)
			{
				throw new CertificateParsingException("Exception extracting certificate details", exception);
			}
			return result;
		}

		// Token: 0x06003EE0 RID: 16096 RVA: 0x00159F74 File Offset: 0x00159F74
		private static Asn1Sequence FromKey(AsymmetricKeyParameter pubKey)
		{
			Asn1Sequence result;
			try
			{
				SubjectPublicKeyInfo spki = SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(pubKey);
				result = (Asn1Sequence)new AuthorityKeyIdentifier(spki).ToAsn1Object();
			}
			catch (Exception arg)
			{
				throw new InvalidKeyException("can't process key: " + arg);
			}
			return result;
		}

		// Token: 0x06003EE1 RID: 16097 RVA: 0x00159FC4 File Offset: 0x00159FC4
		public AuthorityKeyIdentifierStructure(X509Certificate certificate) : base(AuthorityKeyIdentifierStructure.FromCertificate(certificate))
		{
		}

		// Token: 0x06003EE2 RID: 16098 RVA: 0x00159FD4 File Offset: 0x00159FD4
		public AuthorityKeyIdentifierStructure(AsymmetricKeyParameter pubKey) : base(AuthorityKeyIdentifierStructure.FromKey(pubKey))
		{
		}
	}
}
