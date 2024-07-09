using System;
using System.Collections;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Operators;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Security.Certificates;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.X509
{
	// Token: 0x02000724 RID: 1828
	public class X509V2AttributeCertificateGenerator
	{
		// Token: 0x06004020 RID: 16416 RVA: 0x0015F4BC File Offset: 0x0015F4BC
		public X509V2AttributeCertificateGenerator()
		{
			this.acInfoGen = new V2AttributeCertificateInfoGenerator();
		}

		// Token: 0x06004021 RID: 16417 RVA: 0x0015F4DC File Offset: 0x0015F4DC
		public void Reset()
		{
			this.acInfoGen = new V2AttributeCertificateInfoGenerator();
			this.extGenerator.Reset();
		}

		// Token: 0x06004022 RID: 16418 RVA: 0x0015F4F4 File Offset: 0x0015F4F4
		public void SetHolder(AttributeCertificateHolder holder)
		{
			this.acInfoGen.SetHolder(holder.holder);
		}

		// Token: 0x06004023 RID: 16419 RVA: 0x0015F508 File Offset: 0x0015F508
		public void SetIssuer(AttributeCertificateIssuer issuer)
		{
			this.acInfoGen.SetIssuer(AttCertIssuer.GetInstance(issuer.form));
		}

		// Token: 0x06004024 RID: 16420 RVA: 0x0015F520 File Offset: 0x0015F520
		public void SetSerialNumber(BigInteger serialNumber)
		{
			this.acInfoGen.SetSerialNumber(new DerInteger(serialNumber));
		}

		// Token: 0x06004025 RID: 16421 RVA: 0x0015F534 File Offset: 0x0015F534
		public void SetNotBefore(DateTime date)
		{
			this.acInfoGen.SetStartDate(new DerGeneralizedTime(date));
		}

		// Token: 0x06004026 RID: 16422 RVA: 0x0015F548 File Offset: 0x0015F548
		public void SetNotAfter(DateTime date)
		{
			this.acInfoGen.SetEndDate(new DerGeneralizedTime(date));
		}

		// Token: 0x06004027 RID: 16423 RVA: 0x0015F55C File Offset: 0x0015F55C
		[Obsolete("Not needed if Generate used with an ISignatureFactory")]
		public void SetSignatureAlgorithm(string signatureAlgorithm)
		{
			this.signatureAlgorithm = signatureAlgorithm;
			try
			{
				this.sigOID = X509Utilities.GetAlgorithmOid(signatureAlgorithm);
			}
			catch (Exception)
			{
				throw new ArgumentException("Unknown signature type requested");
			}
			this.sigAlgId = X509Utilities.GetSigAlgID(this.sigOID, signatureAlgorithm);
			this.acInfoGen.SetSignature(this.sigAlgId);
		}

		// Token: 0x06004028 RID: 16424 RVA: 0x0015F5C0 File Offset: 0x0015F5C0
		public void AddAttribute(X509Attribute attribute)
		{
			this.acInfoGen.AddAttribute(AttributeX509.GetInstance(attribute.ToAsn1Object()));
		}

		// Token: 0x06004029 RID: 16425 RVA: 0x0015F5D8 File Offset: 0x0015F5D8
		public void SetIssuerUniqueId(bool[] iui)
		{
			throw Platform.CreateNotImplementedException("SetIssuerUniqueId()");
		}

		// Token: 0x0600402A RID: 16426 RVA: 0x0015F5E4 File Offset: 0x0015F5E4
		public void AddExtension(string oid, bool critical, Asn1Encodable extensionValue)
		{
			this.extGenerator.AddExtension(new DerObjectIdentifier(oid), critical, extensionValue);
		}

		// Token: 0x0600402B RID: 16427 RVA: 0x0015F5FC File Offset: 0x0015F5FC
		public void AddExtension(string oid, bool critical, byte[] extensionValue)
		{
			this.extGenerator.AddExtension(new DerObjectIdentifier(oid), critical, extensionValue);
		}

		// Token: 0x0600402C RID: 16428 RVA: 0x0015F614 File Offset: 0x0015F614
		[Obsolete("Use Generate with an ISignatureFactory")]
		public IX509AttributeCertificate Generate(AsymmetricKeyParameter privateKey)
		{
			return this.Generate(privateKey, null);
		}

		// Token: 0x0600402D RID: 16429 RVA: 0x0015F620 File Offset: 0x0015F620
		[Obsolete("Use Generate with an ISignatureFactory")]
		public IX509AttributeCertificate Generate(AsymmetricKeyParameter privateKey, SecureRandom random)
		{
			return this.Generate(new Asn1SignatureFactory(this.signatureAlgorithm, privateKey, random));
		}

		// Token: 0x0600402E RID: 16430 RVA: 0x0015F638 File Offset: 0x0015F638
		public IX509AttributeCertificate Generate(ISignatureFactory signatureCalculatorFactory)
		{
			if (!this.extGenerator.IsEmpty)
			{
				this.acInfoGen.SetExtensions(this.extGenerator.Generate());
			}
			AlgorithmIdentifier signature = (AlgorithmIdentifier)signatureCalculatorFactory.AlgorithmDetails;
			this.acInfoGen.SetSignature(signature);
			AttributeCertificateInfo attributeCertificateInfo = this.acInfoGen.GenerateAttributeCertificateInfo();
			byte[] derEncoded = attributeCertificateInfo.GetDerEncoded();
			IStreamCalculator streamCalculator = signatureCalculatorFactory.CreateCalculator();
			streamCalculator.Stream.Write(derEncoded, 0, derEncoded.Length);
			Platform.Dispose(streamCalculator.Stream);
			IX509AttributeCertificate result;
			try
			{
				DerBitString signatureValue = new DerBitString(((IBlockResult)streamCalculator.GetResult()).Collect());
				result = new X509V2AttributeCertificate(new AttributeCertificate(attributeCertificateInfo, signature, signatureValue));
			}
			catch (Exception e)
			{
				throw new CertificateEncodingException("constructed invalid certificate", e);
			}
			return result;
		}

		// Token: 0x17000ADB RID: 2779
		// (get) Token: 0x0600402F RID: 16431 RVA: 0x0015F708 File Offset: 0x0015F708
		public IEnumerable SignatureAlgNames
		{
			get
			{
				return X509Utilities.GetAlgNames();
			}
		}

		// Token: 0x040020D4 RID: 8404
		private readonly X509ExtensionsGenerator extGenerator = new X509ExtensionsGenerator();

		// Token: 0x040020D5 RID: 8405
		private V2AttributeCertificateInfoGenerator acInfoGen;

		// Token: 0x040020D6 RID: 8406
		private DerObjectIdentifier sigOID;

		// Token: 0x040020D7 RID: 8407
		private AlgorithmIdentifier sigAlgId;

		// Token: 0x040020D8 RID: 8408
		private string signatureAlgorithm;
	}
}
