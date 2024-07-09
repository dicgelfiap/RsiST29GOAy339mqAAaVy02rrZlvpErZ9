using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Asn1.Ess;
using Org.BouncyCastle.Asn1.Nist;
using Org.BouncyCastle.Asn1.Oiw;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.Tsp;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Cms;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Security.Certificates;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.X509;
using Org.BouncyCastle.X509.Store;

namespace Org.BouncyCastle.Tsp
{
	// Token: 0x020006BF RID: 1727
	public class TimeStampToken
	{
		// Token: 0x06003C76 RID: 15478 RVA: 0x0014E1AC File Offset: 0x0014E1AC
		public TimeStampToken(Org.BouncyCastle.Asn1.Cms.ContentInfo contentInfo) : this(new CmsSignedData(contentInfo))
		{
		}

		// Token: 0x06003C77 RID: 15479 RVA: 0x0014E1BC File Offset: 0x0014E1BC
		public TimeStampToken(CmsSignedData signedData)
		{
			this.tsToken = signedData;
			if (!this.tsToken.SignedContentType.Equals(PkcsObjectIdentifiers.IdCTTstInfo))
			{
				throw new TspValidationException("ContentInfo object not for a time stamp.");
			}
			ICollection signers = this.tsToken.GetSignerInfos().GetSigners();
			if (signers.Count != 1)
			{
				throw new ArgumentException("Time-stamp token signed by " + signers.Count + " signers, but it must contain just the TSA signature.");
			}
			IEnumerator enumerator = signers.GetEnumerator();
			enumerator.MoveNext();
			this.tsaSignerInfo = (SignerInformation)enumerator.Current;
			try
			{
				CmsProcessable signedContent = this.tsToken.SignedContent;
				MemoryStream memoryStream = new MemoryStream();
				signedContent.Write(memoryStream);
				this.tstInfo = new TimeStampTokenInfo(TstInfo.GetInstance(Asn1Object.FromByteArray(memoryStream.ToArray())));
				Org.BouncyCastle.Asn1.Cms.Attribute attribute = this.tsaSignerInfo.SignedAttributes[PkcsObjectIdentifiers.IdAASigningCertificate];
				if (attribute != null)
				{
					SigningCertificate instance = SigningCertificate.GetInstance(attribute.AttrValues[0]);
					this.certID = new TimeStampToken.CertID(EssCertID.GetInstance(instance.GetCerts()[0]));
				}
				else
				{
					attribute = this.tsaSignerInfo.SignedAttributes[PkcsObjectIdentifiers.IdAASigningCertificateV2];
					if (attribute == null)
					{
						throw new TspValidationException("no signing certificate attribute found, time stamp invalid.");
					}
					SigningCertificateV2 instance2 = SigningCertificateV2.GetInstance(attribute.AttrValues[0]);
					this.certID = new TimeStampToken.CertID(EssCertIDv2.GetInstance(instance2.GetCerts()[0]));
				}
			}
			catch (CmsException ex)
			{
				throw new TspException(ex.Message, ex.InnerException);
			}
		}

		// Token: 0x17000A36 RID: 2614
		// (get) Token: 0x06003C78 RID: 15480 RVA: 0x0014E364 File Offset: 0x0014E364
		public TimeStampTokenInfo TimeStampInfo
		{
			get
			{
				return this.tstInfo;
			}
		}

		// Token: 0x17000A37 RID: 2615
		// (get) Token: 0x06003C79 RID: 15481 RVA: 0x0014E36C File Offset: 0x0014E36C
		public SignerID SignerID
		{
			get
			{
				return this.tsaSignerInfo.SignerID;
			}
		}

		// Token: 0x17000A38 RID: 2616
		// (get) Token: 0x06003C7A RID: 15482 RVA: 0x0014E37C File Offset: 0x0014E37C
		public Org.BouncyCastle.Asn1.Cms.AttributeTable SignedAttributes
		{
			get
			{
				return this.tsaSignerInfo.SignedAttributes;
			}
		}

		// Token: 0x17000A39 RID: 2617
		// (get) Token: 0x06003C7B RID: 15483 RVA: 0x0014E38C File Offset: 0x0014E38C
		public Org.BouncyCastle.Asn1.Cms.AttributeTable UnsignedAttributes
		{
			get
			{
				return this.tsaSignerInfo.UnsignedAttributes;
			}
		}

		// Token: 0x06003C7C RID: 15484 RVA: 0x0014E39C File Offset: 0x0014E39C
		public IX509Store GetCertificates(string type)
		{
			return this.tsToken.GetCertificates(type);
		}

		// Token: 0x06003C7D RID: 15485 RVA: 0x0014E3AC File Offset: 0x0014E3AC
		public IX509Store GetCrls(string type)
		{
			return this.tsToken.GetCrls(type);
		}

		// Token: 0x06003C7E RID: 15486 RVA: 0x0014E3BC File Offset: 0x0014E3BC
		public IX509Store GetAttributeCertificates(string type)
		{
			return this.tsToken.GetAttributeCertificates(type);
		}

		// Token: 0x06003C7F RID: 15487 RVA: 0x0014E3CC File Offset: 0x0014E3CC
		public void Validate(X509Certificate cert)
		{
			try
			{
				byte[] b = DigestUtilities.CalculateDigest(this.certID.GetHashAlgorithmName(), cert.GetEncoded());
				if (!Arrays.ConstantTimeAreEqual(this.certID.GetCertHash(), b))
				{
					throw new TspValidationException("certificate hash does not match certID hash.");
				}
				if (this.certID.IssuerSerial != null)
				{
					if (!this.certID.IssuerSerial.Serial.Value.Equals(cert.SerialNumber))
					{
						throw new TspValidationException("certificate serial number does not match certID for signature.");
					}
					GeneralName[] names = this.certID.IssuerSerial.Issuer.GetNames();
					X509Name issuerX509Principal = PrincipalUtilities.GetIssuerX509Principal(cert);
					bool flag = false;
					for (int num = 0; num != names.Length; num++)
					{
						if (names[num].TagNo == 4 && X509Name.GetInstance(names[num].Name).Equivalent(issuerX509Principal))
						{
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						throw new TspValidationException("certificate name does not match certID for signature. ");
					}
				}
				TspUtil.ValidateCertificate(cert);
				cert.CheckValidity(this.tstInfo.GenTime);
				if (!this.tsaSignerInfo.Verify(cert))
				{
					throw new TspValidationException("signature not created by certificate.");
				}
			}
			catch (CmsException ex)
			{
				if (ex.InnerException != null)
				{
					throw new TspException(ex.Message, ex.InnerException);
				}
				throw new TspException("CMS exception: " + ex, ex);
			}
			catch (CertificateEncodingException ex2)
			{
				throw new TspException("problem processing certificate: " + ex2, ex2);
			}
			catch (SecurityUtilityException ex3)
			{
				throw new TspException("cannot find algorithm: " + ex3.Message, ex3);
			}
		}

		// Token: 0x06003C80 RID: 15488 RVA: 0x0014E5B8 File Offset: 0x0014E5B8
		public CmsSignedData ToCmsSignedData()
		{
			return this.tsToken;
		}

		// Token: 0x06003C81 RID: 15489 RVA: 0x0014E5C0 File Offset: 0x0014E5C0
		public byte[] GetEncoded()
		{
			return this.tsToken.GetEncoded("DER");
		}

		// Token: 0x06003C82 RID: 15490 RVA: 0x0014E5D4 File Offset: 0x0014E5D4
		public byte[] GetEncoded(string encoding)
		{
			return this.tsToken.GetEncoded(encoding);
		}

		// Token: 0x04001EB9 RID: 7865
		private readonly CmsSignedData tsToken;

		// Token: 0x04001EBA RID: 7866
		private readonly SignerInformation tsaSignerInfo;

		// Token: 0x04001EBB RID: 7867
		private readonly TimeStampTokenInfo tstInfo;

		// Token: 0x04001EBC RID: 7868
		private readonly TimeStampToken.CertID certID;

		// Token: 0x02000E70 RID: 3696
		private class CertID
		{
			// Token: 0x06008D74 RID: 36212 RVA: 0x002A69C8 File Offset: 0x002A69C8
			internal CertID(EssCertID certID)
			{
				this.certID = certID;
				this.certIDv2 = null;
			}

			// Token: 0x06008D75 RID: 36213 RVA: 0x002A69E0 File Offset: 0x002A69E0
			internal CertID(EssCertIDv2 certID)
			{
				this.certIDv2 = certID;
				this.certID = null;
			}

			// Token: 0x06008D76 RID: 36214 RVA: 0x002A69F8 File Offset: 0x002A69F8
			public string GetHashAlgorithmName()
			{
				if (this.certID != null)
				{
					return "SHA-1";
				}
				if (NistObjectIdentifiers.IdSha256.Equals(this.certIDv2.HashAlgorithm.Algorithm))
				{
					return "SHA-256";
				}
				return this.certIDv2.HashAlgorithm.Algorithm.Id;
			}

			// Token: 0x06008D77 RID: 36215 RVA: 0x002A6A54 File Offset: 0x002A6A54
			public AlgorithmIdentifier GetHashAlgorithm()
			{
				if (this.certID == null)
				{
					return this.certIDv2.HashAlgorithm;
				}
				return new AlgorithmIdentifier(OiwObjectIdentifiers.IdSha1);
			}

			// Token: 0x06008D78 RID: 36216 RVA: 0x002A6A78 File Offset: 0x002A6A78
			public byte[] GetCertHash()
			{
				if (this.certID == null)
				{
					return this.certIDv2.GetCertHash();
				}
				return this.certID.GetCertHash();
			}

			// Token: 0x17001DAF RID: 7599
			// (get) Token: 0x06008D79 RID: 36217 RVA: 0x002A6A9C File Offset: 0x002A6A9C
			public IssuerSerial IssuerSerial
			{
				get
				{
					if (this.certID == null)
					{
						return this.certIDv2.IssuerSerial;
					}
					return this.certID.IssuerSerial;
				}
			}

			// Token: 0x040042E7 RID: 17127
			private EssCertID certID;

			// Token: 0x040042E8 RID: 17128
			private EssCertIDv2 certIDv2;
		}
	}
}
