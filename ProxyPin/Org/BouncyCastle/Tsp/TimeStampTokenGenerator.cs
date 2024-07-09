using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Asn1.Ess;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.Tsp;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Cms;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Security.Certificates;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.X509;
using Org.BouncyCastle.X509.Store;

namespace Org.BouncyCastle.Tsp
{
	// Token: 0x020006C0 RID: 1728
	public class TimeStampTokenGenerator
	{
		// Token: 0x06003C83 RID: 15491 RVA: 0x0014E5E4 File Offset: 0x0014E5E4
		public TimeStampTokenGenerator(AsymmetricKeyParameter key, X509Certificate cert, string digestOID, string tsaPolicyOID) : this(key, cert, digestOID, tsaPolicyOID, null, null)
		{
		}

		// Token: 0x06003C84 RID: 15492 RVA: 0x0014E5F4 File Offset: 0x0014E5F4
		public TimeStampTokenGenerator(AsymmetricKeyParameter key, X509Certificate cert, string digestOID, string tsaPolicyOID, Org.BouncyCastle.Asn1.Cms.AttributeTable signedAttr, Org.BouncyCastle.Asn1.Cms.AttributeTable unsignedAttr)
		{
			this.key = key;
			this.cert = cert;
			this.digestOID = digestOID;
			this.tsaPolicyOID = tsaPolicyOID;
			this.unsignedAttr = unsignedAttr;
			TspUtil.ValidateCertificate(cert);
			IDictionary dictionary;
			if (signedAttr != null)
			{
				dictionary = signedAttr.ToDictionary();
			}
			else
			{
				dictionary = Platform.CreateHashtable();
			}
			try
			{
				byte[] hash = DigestUtilities.CalculateDigest("SHA-1", cert.GetEncoded());
				EssCertID essCertID = new EssCertID(hash);
				Org.BouncyCastle.Asn1.Cms.Attribute attribute = new Org.BouncyCastle.Asn1.Cms.Attribute(PkcsObjectIdentifiers.IdAASigningCertificate, new DerSet(new SigningCertificate(essCertID)));
				dictionary[attribute.AttrType] = attribute;
			}
			catch (CertificateEncodingException e)
			{
				throw new TspException("Exception processing certificate.", e);
			}
			catch (SecurityUtilityException e2)
			{
				throw new TspException("Can't find a SHA-1 implementation.", e2);
			}
			this.signedAttr = new Org.BouncyCastle.Asn1.Cms.AttributeTable(dictionary);
		}

		// Token: 0x06003C85 RID: 15493 RVA: 0x0014E6F8 File Offset: 0x0014E6F8
		public void SetCertificates(IX509Store certificates)
		{
			this.x509Certs = certificates;
		}

		// Token: 0x06003C86 RID: 15494 RVA: 0x0014E704 File Offset: 0x0014E704
		public void SetCrls(IX509Store crls)
		{
			this.x509Crls = crls;
		}

		// Token: 0x06003C87 RID: 15495 RVA: 0x0014E710 File Offset: 0x0014E710
		public void SetAccuracySeconds(int accuracySeconds)
		{
			this.accuracySeconds = accuracySeconds;
		}

		// Token: 0x06003C88 RID: 15496 RVA: 0x0014E71C File Offset: 0x0014E71C
		public void SetAccuracyMillis(int accuracyMillis)
		{
			this.accuracyMillis = accuracyMillis;
		}

		// Token: 0x06003C89 RID: 15497 RVA: 0x0014E728 File Offset: 0x0014E728
		public void SetAccuracyMicros(int accuracyMicros)
		{
			this.accuracyMicros = accuracyMicros;
		}

		// Token: 0x06003C8A RID: 15498 RVA: 0x0014E734 File Offset: 0x0014E734
		public void SetOrdering(bool ordering)
		{
			this.ordering = ordering;
		}

		// Token: 0x06003C8B RID: 15499 RVA: 0x0014E740 File Offset: 0x0014E740
		public void SetTsa(GeneralName tsa)
		{
			this.tsa = tsa;
		}

		// Token: 0x06003C8C RID: 15500 RVA: 0x0014E74C File Offset: 0x0014E74C
		public TimeStampToken Generate(TimeStampRequest request, BigInteger serialNumber, DateTime genTime)
		{
			DerObjectIdentifier algorithm = new DerObjectIdentifier(request.MessageImprintAlgOid);
			AlgorithmIdentifier hashAlgorithm = new AlgorithmIdentifier(algorithm, DerNull.Instance);
			MessageImprint messageImprint = new MessageImprint(hashAlgorithm, request.GetMessageImprintDigest());
			Accuracy accuracy = null;
			if (this.accuracySeconds > 0 || this.accuracyMillis > 0 || this.accuracyMicros > 0)
			{
				DerInteger seconds = null;
				if (this.accuracySeconds > 0)
				{
					seconds = new DerInteger(this.accuracySeconds);
				}
				DerInteger millis = null;
				if (this.accuracyMillis > 0)
				{
					millis = new DerInteger(this.accuracyMillis);
				}
				DerInteger micros = null;
				if (this.accuracyMicros > 0)
				{
					micros = new DerInteger(this.accuracyMicros);
				}
				accuracy = new Accuracy(seconds, millis, micros);
			}
			DerBoolean derBoolean = null;
			if (this.ordering)
			{
				derBoolean = DerBoolean.GetInstance(this.ordering);
			}
			DerInteger nonce = null;
			if (request.Nonce != null)
			{
				nonce = new DerInteger(request.Nonce);
			}
			DerObjectIdentifier tsaPolicyId = new DerObjectIdentifier(this.tsaPolicyOID);
			if (request.ReqPolicy != null)
			{
				tsaPolicyId = new DerObjectIdentifier(request.ReqPolicy);
			}
			TstInfo tstInfo = new TstInfo(tsaPolicyId, messageImprint, new DerInteger(serialNumber), new DerGeneralizedTime(genTime), accuracy, derBoolean, nonce, this.tsa, request.Extensions);
			TimeStampToken result;
			try
			{
				CmsSignedDataGenerator cmsSignedDataGenerator = new CmsSignedDataGenerator();
				byte[] derEncoded = tstInfo.GetDerEncoded();
				if (request.CertReq)
				{
					cmsSignedDataGenerator.AddCertificates(this.x509Certs);
				}
				cmsSignedDataGenerator.AddCrls(this.x509Crls);
				cmsSignedDataGenerator.AddSigner(this.key, this.cert, this.digestOID, this.signedAttr, this.unsignedAttr);
				CmsSignedData signedData = cmsSignedDataGenerator.Generate(PkcsObjectIdentifiers.IdCTTstInfo.Id, new CmsProcessableByteArray(derEncoded), true);
				result = new TimeStampToken(signedData);
			}
			catch (CmsException e)
			{
				throw new TspException("Error generating time-stamp token", e);
			}
			catch (IOException e2)
			{
				throw new TspException("Exception encoding info", e2);
			}
			catch (X509StoreException e3)
			{
				throw new TspException("Exception handling CertStore", e3);
			}
			return result;
		}

		// Token: 0x04001EBD RID: 7869
		private int accuracySeconds = -1;

		// Token: 0x04001EBE RID: 7870
		private int accuracyMillis = -1;

		// Token: 0x04001EBF RID: 7871
		private int accuracyMicros = -1;

		// Token: 0x04001EC0 RID: 7872
		private bool ordering = false;

		// Token: 0x04001EC1 RID: 7873
		private GeneralName tsa = null;

		// Token: 0x04001EC2 RID: 7874
		private string tsaPolicyOID;

		// Token: 0x04001EC3 RID: 7875
		private AsymmetricKeyParameter key;

		// Token: 0x04001EC4 RID: 7876
		private X509Certificate cert;

		// Token: 0x04001EC5 RID: 7877
		private string digestOID;

		// Token: 0x04001EC6 RID: 7878
		private Org.BouncyCastle.Asn1.Cms.AttributeTable signedAttr;

		// Token: 0x04001EC7 RID: 7879
		private Org.BouncyCastle.Asn1.Cms.AttributeTable unsignedAttr;

		// Token: 0x04001EC8 RID: 7880
		private IX509Store x509Certs;

		// Token: 0x04001EC9 RID: 7881
		private IX509Store x509Crls;
	}
}
