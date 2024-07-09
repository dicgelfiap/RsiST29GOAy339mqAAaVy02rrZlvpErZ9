using System;
using System.Collections;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Operators;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.X509
{
	// Token: 0x02000722 RID: 1826
	public class X509V1CertificateGenerator
	{
		// Token: 0x06003FF8 RID: 16376 RVA: 0x0015ED84 File Offset: 0x0015ED84
		public X509V1CertificateGenerator()
		{
			this.tbsGen = new V1TbsCertificateGenerator();
		}

		// Token: 0x06003FF9 RID: 16377 RVA: 0x0015ED98 File Offset: 0x0015ED98
		public void Reset()
		{
			this.tbsGen = new V1TbsCertificateGenerator();
		}

		// Token: 0x06003FFA RID: 16378 RVA: 0x0015EDA8 File Offset: 0x0015EDA8
		public void SetSerialNumber(BigInteger serialNumber)
		{
			if (serialNumber.SignValue <= 0)
			{
				throw new ArgumentException("serial number must be a positive integer", "serialNumber");
			}
			this.tbsGen.SetSerialNumber(new DerInteger(serialNumber));
		}

		// Token: 0x06003FFB RID: 16379 RVA: 0x0015EDD8 File Offset: 0x0015EDD8
		public void SetIssuerDN(X509Name issuer)
		{
			this.tbsGen.SetIssuer(issuer);
		}

		// Token: 0x06003FFC RID: 16380 RVA: 0x0015EDE8 File Offset: 0x0015EDE8
		public void SetNotBefore(DateTime date)
		{
			this.tbsGen.SetStartDate(new Time(date));
		}

		// Token: 0x06003FFD RID: 16381 RVA: 0x0015EDFC File Offset: 0x0015EDFC
		public void SetNotAfter(DateTime date)
		{
			this.tbsGen.SetEndDate(new Time(date));
		}

		// Token: 0x06003FFE RID: 16382 RVA: 0x0015EE10 File Offset: 0x0015EE10
		public void SetSubjectDN(X509Name subject)
		{
			this.tbsGen.SetSubject(subject);
		}

		// Token: 0x06003FFF RID: 16383 RVA: 0x0015EE20 File Offset: 0x0015EE20
		public void SetPublicKey(AsymmetricKeyParameter publicKey)
		{
			try
			{
				this.tbsGen.SetSubjectPublicKeyInfo(SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(publicKey));
			}
			catch (Exception ex)
			{
				throw new ArgumentException("unable to process key - " + ex.ToString());
			}
		}

		// Token: 0x06004000 RID: 16384 RVA: 0x0015EE6C File Offset: 0x0015EE6C
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
				throw new ArgumentException("Unknown signature type requested", "signatureAlgorithm");
			}
			this.sigAlgId = X509Utilities.GetSigAlgID(this.sigOID, signatureAlgorithm);
			this.tbsGen.SetSignature(this.sigAlgId);
		}

		// Token: 0x06004001 RID: 16385 RVA: 0x0015EED8 File Offset: 0x0015EED8
		[Obsolete("Use Generate with an ISignatureFactory")]
		public X509Certificate Generate(AsymmetricKeyParameter privateKey)
		{
			return this.Generate(privateKey, null);
		}

		// Token: 0x06004002 RID: 16386 RVA: 0x0015EEE4 File Offset: 0x0015EEE4
		[Obsolete("Use Generate with an ISignatureFactory")]
		public X509Certificate Generate(AsymmetricKeyParameter privateKey, SecureRandom random)
		{
			return this.Generate(new Asn1SignatureFactory(this.signatureAlgorithm, privateKey, random));
		}

		// Token: 0x06004003 RID: 16387 RVA: 0x0015EEFC File Offset: 0x0015EEFC
		public X509Certificate Generate(ISignatureFactory signatureCalculatorFactory)
		{
			this.tbsGen.SetSignature((AlgorithmIdentifier)signatureCalculatorFactory.AlgorithmDetails);
			TbsCertificateStructure tbsCertificateStructure = this.tbsGen.GenerateTbsCertificate();
			IStreamCalculator streamCalculator = signatureCalculatorFactory.CreateCalculator();
			byte[] derEncoded = tbsCertificateStructure.GetDerEncoded();
			streamCalculator.Stream.Write(derEncoded, 0, derEncoded.Length);
			Platform.Dispose(streamCalculator.Stream);
			return this.GenerateJcaObject(tbsCertificateStructure, (AlgorithmIdentifier)signatureCalculatorFactory.AlgorithmDetails, ((IBlockResult)streamCalculator.GetResult()).Collect());
		}

		// Token: 0x06004004 RID: 16388 RVA: 0x0015EF7C File Offset: 0x0015EF7C
		private X509Certificate GenerateJcaObject(TbsCertificateStructure tbsCert, AlgorithmIdentifier sigAlg, byte[] signature)
		{
			return new X509Certificate(new X509CertificateStructure(tbsCert, sigAlg, new DerBitString(signature)));
		}

		// Token: 0x17000AD2 RID: 2770
		// (get) Token: 0x06004005 RID: 16389 RVA: 0x0015EF90 File Offset: 0x0015EF90
		public IEnumerable SignatureAlgNames
		{
			get
			{
				return X509Utilities.GetAlgNames();
			}
		}

		// Token: 0x040020CD RID: 8397
		private V1TbsCertificateGenerator tbsGen;

		// Token: 0x040020CE RID: 8398
		private DerObjectIdentifier sigOID;

		// Token: 0x040020CF RID: 8399
		private AlgorithmIdentifier sigAlgId;

		// Token: 0x040020D0 RID: 8400
		private string signatureAlgorithm;
	}
}
