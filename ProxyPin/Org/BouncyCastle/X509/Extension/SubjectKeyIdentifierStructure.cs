using System;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security.Certificates;

namespace Org.BouncyCastle.X509.Extension
{
	// Token: 0x02000704 RID: 1796
	public class SubjectKeyIdentifierStructure : SubjectKeyIdentifier
	{
		// Token: 0x06003EE3 RID: 16099 RVA: 0x00159FE4 File Offset: 0x00159FE4
		public SubjectKeyIdentifierStructure(Asn1OctetString encodedValue) : base((Asn1OctetString)X509ExtensionUtilities.FromExtensionValue(encodedValue))
		{
		}

		// Token: 0x06003EE4 RID: 16100 RVA: 0x00159FF8 File Offset: 0x00159FF8
		private static Asn1OctetString FromPublicKey(AsymmetricKeyParameter pubKey)
		{
			Asn1OctetString result;
			try
			{
				SubjectPublicKeyInfo spki = SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(pubKey);
				result = (Asn1OctetString)new SubjectKeyIdentifier(spki).ToAsn1Object();
			}
			catch (Exception ex)
			{
				throw new CertificateParsingException("Exception extracting certificate details: " + ex.ToString());
			}
			return result;
		}

		// Token: 0x06003EE5 RID: 16101 RVA: 0x0015A04C File Offset: 0x0015A04C
		public SubjectKeyIdentifierStructure(AsymmetricKeyParameter pubKey) : base(SubjectKeyIdentifierStructure.FromPublicKey(pubKey))
		{
		}
	}
}
