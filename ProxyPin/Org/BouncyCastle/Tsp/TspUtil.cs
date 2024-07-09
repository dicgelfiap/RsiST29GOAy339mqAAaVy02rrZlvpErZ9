using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Asn1.CryptoPro;
using Org.BouncyCastle.Asn1.GM;
using Org.BouncyCastle.Asn1.Nist;
using Org.BouncyCastle.Asn1.Oiw;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.Rosstandart;
using Org.BouncyCastle.Asn1.TeleTrust;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Cms;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Collections;
using Org.BouncyCastle.X509;

namespace Org.BouncyCastle.Tsp
{
	// Token: 0x020006C4 RID: 1732
	public class TspUtil
	{
		// Token: 0x06003CA0 RID: 15520 RVA: 0x0014ECA0 File Offset: 0x0014ECA0
		static TspUtil()
		{
			TspUtil.digestLengths.Add(PkcsObjectIdentifiers.MD5.Id, 16);
			TspUtil.digestLengths.Add(OiwObjectIdentifiers.IdSha1.Id, 20);
			TspUtil.digestLengths.Add(NistObjectIdentifiers.IdSha224.Id, 28);
			TspUtil.digestLengths.Add(NistObjectIdentifiers.IdSha256.Id, 32);
			TspUtil.digestLengths.Add(NistObjectIdentifiers.IdSha384.Id, 48);
			TspUtil.digestLengths.Add(NistObjectIdentifiers.IdSha512.Id, 64);
			TspUtil.digestLengths.Add(TeleTrusTObjectIdentifiers.RipeMD128.Id, 16);
			TspUtil.digestLengths.Add(TeleTrusTObjectIdentifiers.RipeMD160.Id, 20);
			TspUtil.digestLengths.Add(TeleTrusTObjectIdentifiers.RipeMD256.Id, 32);
			TspUtil.digestLengths.Add(CryptoProObjectIdentifiers.GostR3411.Id, 32);
			TspUtil.digestLengths.Add(RosstandartObjectIdentifiers.id_tc26_gost_3411_12_256.Id, 32);
			TspUtil.digestLengths.Add(RosstandartObjectIdentifiers.id_tc26_gost_3411_12_512.Id, 64);
			TspUtil.digestLengths.Add(GMObjectIdentifiers.sm3.Id, 32);
			TspUtil.digestNames.Add(PkcsObjectIdentifiers.MD5.Id, "MD5");
			TspUtil.digestNames.Add(OiwObjectIdentifiers.IdSha1.Id, "SHA1");
			TspUtil.digestNames.Add(NistObjectIdentifiers.IdSha224.Id, "SHA224");
			TspUtil.digestNames.Add(NistObjectIdentifiers.IdSha256.Id, "SHA256");
			TspUtil.digestNames.Add(NistObjectIdentifiers.IdSha384.Id, "SHA384");
			TspUtil.digestNames.Add(NistObjectIdentifiers.IdSha512.Id, "SHA512");
			TspUtil.digestNames.Add(PkcsObjectIdentifiers.MD5WithRsaEncryption.Id, "MD5");
			TspUtil.digestNames.Add(PkcsObjectIdentifiers.Sha1WithRsaEncryption.Id, "SHA1");
			TspUtil.digestNames.Add(PkcsObjectIdentifiers.Sha224WithRsaEncryption.Id, "SHA224");
			TspUtil.digestNames.Add(PkcsObjectIdentifiers.Sha256WithRsaEncryption.Id, "SHA256");
			TspUtil.digestNames.Add(PkcsObjectIdentifiers.Sha384WithRsaEncryption.Id, "SHA384");
			TspUtil.digestNames.Add(PkcsObjectIdentifiers.Sha512WithRsaEncryption.Id, "SHA512");
			TspUtil.digestNames.Add(TeleTrusTObjectIdentifiers.RipeMD128.Id, "RIPEMD128");
			TspUtil.digestNames.Add(TeleTrusTObjectIdentifiers.RipeMD160.Id, "RIPEMD160");
			TspUtil.digestNames.Add(TeleTrusTObjectIdentifiers.RipeMD256.Id, "RIPEMD256");
			TspUtil.digestNames.Add(CryptoProObjectIdentifiers.GostR3411.Id, "GOST3411");
			TspUtil.digestNames.Add(OiwObjectIdentifiers.DsaWithSha1.Id, "SHA1");
			TspUtil.digestNames.Add(OiwObjectIdentifiers.Sha1WithRsa.Id, "SHA1");
			TspUtil.digestNames.Add(OiwObjectIdentifiers.MD5WithRsa.Id, "MD5");
			TspUtil.digestNames.Add(RosstandartObjectIdentifiers.id_tc26_gost_3411_12_256.Id, "GOST3411-2012-256");
			TspUtil.digestNames.Add(RosstandartObjectIdentifiers.id_tc26_gost_3411_12_512.Id, "GOST3411-2012-512");
			TspUtil.digestNames.Add(GMObjectIdentifiers.sm3.Id, "SM3");
		}

		// Token: 0x06003CA1 RID: 15521 RVA: 0x0014F068 File Offset: 0x0014F068
		public static ICollection GetSignatureTimestamps(SignerInformation signerInfo)
		{
			IList list = Platform.CreateArrayList();
			Org.BouncyCastle.Asn1.Cms.AttributeTable unsignedAttributes = signerInfo.UnsignedAttributes;
			if (unsignedAttributes != null)
			{
				foreach (object obj in unsignedAttributes.GetAll(PkcsObjectIdentifiers.IdAASignatureTimeStampToken))
				{
					Org.BouncyCastle.Asn1.Cms.Attribute attribute = (Org.BouncyCastle.Asn1.Cms.Attribute)obj;
					foreach (object obj2 in attribute.AttrValues)
					{
						Asn1Encodable asn1Encodable = (Asn1Encodable)obj2;
						try
						{
							Org.BouncyCastle.Asn1.Cms.ContentInfo instance = Org.BouncyCastle.Asn1.Cms.ContentInfo.GetInstance(asn1Encodable.ToAsn1Object());
							TimeStampToken timeStampToken = new TimeStampToken(instance);
							TimeStampTokenInfo timeStampInfo = timeStampToken.TimeStampInfo;
							byte[] a = DigestUtilities.CalculateDigest(TspUtil.GetDigestAlgName(timeStampInfo.MessageImprintAlgOid), signerInfo.GetSignature());
							if (!Arrays.ConstantTimeAreEqual(a, timeStampInfo.GetMessageImprintDigest()))
							{
								throw new TspValidationException("Incorrect digest in message imprint");
							}
							list.Add(timeStampToken);
						}
						catch (SecurityUtilityException)
						{
							throw new TspValidationException("Unknown hash algorithm specified in timestamp");
						}
						catch (Exception)
						{
							throw new TspValidationException("Timestamp could not be parsed");
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06003CA2 RID: 15522 RVA: 0x0014F1CC File Offset: 0x0014F1CC
		public static void ValidateCertificate(X509Certificate cert)
		{
			if (cert.Version != 3)
			{
				throw new ArgumentException("Certificate must have an ExtendedKeyUsage extension.");
			}
			Asn1OctetString extensionValue = cert.GetExtensionValue(X509Extensions.ExtendedKeyUsage);
			if (extensionValue == null)
			{
				throw new TspValidationException("Certificate must have an ExtendedKeyUsage extension.");
			}
			if (!cert.GetCriticalExtensionOids().Contains(X509Extensions.ExtendedKeyUsage.Id))
			{
				throw new TspValidationException("Certificate must have an ExtendedKeyUsage extension marked as critical.");
			}
			try
			{
				ExtendedKeyUsage instance = ExtendedKeyUsage.GetInstance(Asn1Object.FromByteArray(extensionValue.GetOctets()));
				if (!instance.HasKeyPurposeId(KeyPurposeID.IdKPTimeStamping) || instance.Count != 1)
				{
					throw new TspValidationException("ExtendedKeyUsage not solely time stamping.");
				}
			}
			catch (IOException)
			{
				throw new TspValidationException("cannot process ExtendedKeyUsage extension");
			}
		}

		// Token: 0x06003CA3 RID: 15523 RVA: 0x0014F28C File Offset: 0x0014F28C
		internal static string GetDigestAlgName(string digestAlgOID)
		{
			string text = (string)TspUtil.digestNames[digestAlgOID];
			if (text == null)
			{
				return digestAlgOID;
			}
			return text;
		}

		// Token: 0x06003CA4 RID: 15524 RVA: 0x0014F2B8 File Offset: 0x0014F2B8
		internal static int GetDigestLength(string digestAlgOID)
		{
			if (!TspUtil.digestLengths.Contains(digestAlgOID))
			{
				throw new TspException("digest algorithm cannot be found.");
			}
			return (int)TspUtil.digestLengths[digestAlgOID];
		}

		// Token: 0x06003CA5 RID: 15525 RVA: 0x0014F2E8 File Offset: 0x0014F2E8
		internal static IDigest CreateDigestInstance(string digestAlgOID)
		{
			string digestAlgName = TspUtil.GetDigestAlgName(digestAlgOID);
			return DigestUtilities.GetDigest(digestAlgName);
		}

		// Token: 0x06003CA6 RID: 15526 RVA: 0x0014F308 File Offset: 0x0014F308
		internal static ISet GetCriticalExtensionOids(X509Extensions extensions)
		{
			if (extensions == null)
			{
				return TspUtil.EmptySet;
			}
			return CollectionUtilities.ReadOnly(new HashSet(extensions.GetCriticalExtensionOids()));
		}

		// Token: 0x06003CA7 RID: 15527 RVA: 0x0014F328 File Offset: 0x0014F328
		internal static ISet GetNonCriticalExtensionOids(X509Extensions extensions)
		{
			if (extensions == null)
			{
				return TspUtil.EmptySet;
			}
			return CollectionUtilities.ReadOnly(new HashSet(extensions.GetNonCriticalExtensionOids()));
		}

		// Token: 0x06003CA8 RID: 15528 RVA: 0x0014F348 File Offset: 0x0014F348
		internal static IList GetExtensionOids(X509Extensions extensions)
		{
			if (extensions == null)
			{
				return TspUtil.EmptyList;
			}
			return CollectionUtilities.ReadOnly(Platform.CreateArrayList(extensions.GetExtensionOids()));
		}

		// Token: 0x04001EDA RID: 7898
		private static ISet EmptySet = CollectionUtilities.ReadOnly(new HashSet());

		// Token: 0x04001EDB RID: 7899
		private static IList EmptyList = CollectionUtilities.ReadOnly(Platform.CreateArrayList());

		// Token: 0x04001EDC RID: 7900
		private static readonly IDictionary digestLengths = Platform.CreateHashtable();

		// Token: 0x04001EDD RID: 7901
		private static readonly IDictionary digestNames = Platform.CreateHashtable();
	}
}
