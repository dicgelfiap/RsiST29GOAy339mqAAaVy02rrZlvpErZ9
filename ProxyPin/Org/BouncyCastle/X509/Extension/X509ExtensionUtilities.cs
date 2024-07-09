using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Security.Certificates;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.X509.Extension
{
	// Token: 0x02000705 RID: 1797
	public class X509ExtensionUtilities
	{
		// Token: 0x06003EE6 RID: 16102 RVA: 0x0015A05C File Offset: 0x0015A05C
		public static Asn1Object FromExtensionValue(Asn1OctetString extensionValue)
		{
			return Asn1Object.FromByteArray(extensionValue.GetOctets());
		}

		// Token: 0x06003EE7 RID: 16103 RVA: 0x0015A06C File Offset: 0x0015A06C
		public static ICollection GetIssuerAlternativeNames(X509Certificate cert)
		{
			Asn1OctetString extensionValue = cert.GetExtensionValue(X509Extensions.IssuerAlternativeName);
			return X509ExtensionUtilities.GetAlternativeName(extensionValue);
		}

		// Token: 0x06003EE8 RID: 16104 RVA: 0x0015A090 File Offset: 0x0015A090
		public static ICollection GetSubjectAlternativeNames(X509Certificate cert)
		{
			Asn1OctetString extensionValue = cert.GetExtensionValue(X509Extensions.SubjectAlternativeName);
			return X509ExtensionUtilities.GetAlternativeName(extensionValue);
		}

		// Token: 0x06003EE9 RID: 16105 RVA: 0x0015A0B4 File Offset: 0x0015A0B4
		private static ICollection GetAlternativeName(Asn1OctetString extVal)
		{
			IList list = Platform.CreateArrayList();
			if (extVal != null)
			{
				try
				{
					Asn1Sequence instance = Asn1Sequence.GetInstance(X509ExtensionUtilities.FromExtensionValue(extVal));
					foreach (object obj in instance)
					{
						Asn1Encodable obj2 = (Asn1Encodable)obj;
						IList list2 = Platform.CreateArrayList();
						GeneralName instance2 = GeneralName.GetInstance(obj2);
						list2.Add(instance2.TagNo);
						switch (instance2.TagNo)
						{
						case 0:
						case 3:
						case 5:
							list2.Add(instance2.Name.ToAsn1Object());
							break;
						case 1:
						case 2:
						case 6:
							list2.Add(((IAsn1String)instance2.Name).GetString());
							break;
						case 4:
							list2.Add(X509Name.GetInstance(instance2.Name).ToString());
							break;
						case 7:
							list2.Add(Asn1OctetString.GetInstance(instance2.Name).GetOctets());
							break;
						case 8:
							list2.Add(DerObjectIdentifier.GetInstance(instance2.Name).Id);
							break;
						default:
							throw new IOException("Bad tag number: " + instance2.TagNo);
						}
						list.Add(list2);
					}
				}
				catch (Exception ex)
				{
					throw new CertificateParsingException(ex.Message);
				}
			}
			return list;
		}
	}
}
