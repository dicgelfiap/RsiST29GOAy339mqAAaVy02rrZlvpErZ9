using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Security.Certificates;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.IO;
using Org.BouncyCastle.X509;
using Org.BouncyCastle.X509.Store;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x020002FB RID: 763
	internal class CmsUtilities
	{
		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x0600171F RID: 5919 RVA: 0x000791C4 File Offset: 0x000791C4
		internal static int MaximumMemory
		{
			get
			{
				long num = 2147483647L;
				if (num > 2147483647L)
				{
					return int.MaxValue;
				}
				return (int)num;
			}
		}

		// Token: 0x06001720 RID: 5920 RVA: 0x000791F0 File Offset: 0x000791F0
		internal static ContentInfo ReadContentInfo(byte[] input)
		{
			return CmsUtilities.ReadContentInfo(new Asn1InputStream(input));
		}

		// Token: 0x06001721 RID: 5921 RVA: 0x00079200 File Offset: 0x00079200
		internal static ContentInfo ReadContentInfo(Stream input)
		{
			return CmsUtilities.ReadContentInfo(new Asn1InputStream(input, CmsUtilities.MaximumMemory));
		}

		// Token: 0x06001722 RID: 5922 RVA: 0x00079214 File Offset: 0x00079214
		private static ContentInfo ReadContentInfo(Asn1InputStream aIn)
		{
			ContentInfo instance;
			try
			{
				instance = ContentInfo.GetInstance(aIn.ReadObject());
			}
			catch (IOException e)
			{
				throw new CmsException("IOException reading content.", e);
			}
			catch (InvalidCastException e2)
			{
				throw new CmsException("Malformed content.", e2);
			}
			catch (ArgumentException e3)
			{
				throw new CmsException("Malformed content.", e3);
			}
			return instance;
		}

		// Token: 0x06001723 RID: 5923 RVA: 0x00079284 File Offset: 0x00079284
		public static byte[] StreamToByteArray(Stream inStream)
		{
			return Streams.ReadAll(inStream);
		}

		// Token: 0x06001724 RID: 5924 RVA: 0x0007928C File Offset: 0x0007928C
		public static byte[] StreamToByteArray(Stream inStream, int limit)
		{
			return Streams.ReadAllLimited(inStream, limit);
		}

		// Token: 0x06001725 RID: 5925 RVA: 0x00079298 File Offset: 0x00079298
		public static IList GetCertificatesFromStore(IX509Store certStore)
		{
			IList result;
			try
			{
				IList list = Platform.CreateArrayList();
				if (certStore != null)
				{
					foreach (object obj in certStore.GetMatches(null))
					{
						X509Certificate x509Certificate = (X509Certificate)obj;
						list.Add(X509CertificateStructure.GetInstance(Asn1Object.FromByteArray(x509Certificate.GetEncoded())));
					}
				}
				result = list;
			}
			catch (CertificateEncodingException e)
			{
				throw new CmsException("error encoding certs", e);
			}
			catch (Exception e2)
			{
				throw new CmsException("error processing certs", e2);
			}
			return result;
		}

		// Token: 0x06001726 RID: 5926 RVA: 0x00079358 File Offset: 0x00079358
		public static IList GetCrlsFromStore(IX509Store crlStore)
		{
			IList result;
			try
			{
				IList list = Platform.CreateArrayList();
				if (crlStore != null)
				{
					foreach (object obj in crlStore.GetMatches(null))
					{
						X509Crl x509Crl = (X509Crl)obj;
						list.Add(CertificateList.GetInstance(Asn1Object.FromByteArray(x509Crl.GetEncoded())));
					}
				}
				result = list;
			}
			catch (CrlException e)
			{
				throw new CmsException("error encoding crls", e);
			}
			catch (Exception e2)
			{
				throw new CmsException("error processing crls", e2);
			}
			return result;
		}

		// Token: 0x06001727 RID: 5927 RVA: 0x00079418 File Offset: 0x00079418
		public static Asn1Set CreateBerSetFromList(IList berObjects)
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector();
			foreach (object obj in berObjects)
			{
				Asn1Encodable element = (Asn1Encodable)obj;
				asn1EncodableVector.Add(element);
			}
			return new BerSet(asn1EncodableVector);
		}

		// Token: 0x06001728 RID: 5928 RVA: 0x00079484 File Offset: 0x00079484
		public static Asn1Set CreateDerSetFromList(IList derObjects)
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector();
			foreach (object obj in derObjects)
			{
				Asn1Encodable element = (Asn1Encodable)obj;
				asn1EncodableVector.Add(element);
			}
			return new DerSet(asn1EncodableVector);
		}

		// Token: 0x06001729 RID: 5929 RVA: 0x000794F0 File Offset: 0x000794F0
		internal static Stream CreateBerOctetOutputStream(Stream s, int tagNo, bool isExplicit, int bufferSize)
		{
			BerOctetStringGenerator berOctetStringGenerator = new BerOctetStringGenerator(s, tagNo, isExplicit);
			return berOctetStringGenerator.GetOctetOutputStream(bufferSize);
		}

		// Token: 0x0600172A RID: 5930 RVA: 0x00079514 File Offset: 0x00079514
		internal static TbsCertificateStructure GetTbsCertificateStructure(X509Certificate cert)
		{
			return TbsCertificateStructure.GetInstance(Asn1Object.FromByteArray(cert.GetTbsCertificate()));
		}

		// Token: 0x0600172B RID: 5931 RVA: 0x00079528 File Offset: 0x00079528
		internal static IssuerAndSerialNumber GetIssuerAndSerialNumber(X509Certificate cert)
		{
			TbsCertificateStructure tbsCertificateStructure = CmsUtilities.GetTbsCertificateStructure(cert);
			return new IssuerAndSerialNumber(tbsCertificateStructure.Issuer, tbsCertificateStructure.SerialNumber.Value);
		}
	}
}
