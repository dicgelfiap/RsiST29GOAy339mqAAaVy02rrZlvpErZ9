using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Operators;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Security.Certificates;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Collections;

namespace Org.BouncyCastle.X509
{
	// Token: 0x02000725 RID: 1829
	public class X509V2CrlGenerator
	{
		// Token: 0x06004030 RID: 16432 RVA: 0x0015F710 File Offset: 0x0015F710
		public X509V2CrlGenerator()
		{
			this.tbsGen = new V2TbsCertListGenerator();
		}

		// Token: 0x06004031 RID: 16433 RVA: 0x0015F730 File Offset: 0x0015F730
		public void Reset()
		{
			this.tbsGen = new V2TbsCertListGenerator();
			this.extGenerator.Reset();
		}

		// Token: 0x06004032 RID: 16434 RVA: 0x0015F748 File Offset: 0x0015F748
		public void SetIssuerDN(X509Name issuer)
		{
			this.tbsGen.SetIssuer(issuer);
		}

		// Token: 0x06004033 RID: 16435 RVA: 0x0015F758 File Offset: 0x0015F758
		public void SetThisUpdate(DateTime date)
		{
			this.tbsGen.SetThisUpdate(new Time(date));
		}

		// Token: 0x06004034 RID: 16436 RVA: 0x0015F76C File Offset: 0x0015F76C
		public void SetNextUpdate(DateTime date)
		{
			this.tbsGen.SetNextUpdate(new Time(date));
		}

		// Token: 0x06004035 RID: 16437 RVA: 0x0015F780 File Offset: 0x0015F780
		public void AddCrlEntry(BigInteger userCertificate, DateTime revocationDate, int reason)
		{
			this.tbsGen.AddCrlEntry(new DerInteger(userCertificate), new Time(revocationDate), reason);
		}

		// Token: 0x06004036 RID: 16438 RVA: 0x0015F79C File Offset: 0x0015F79C
		public void AddCrlEntry(BigInteger userCertificate, DateTime revocationDate, int reason, DateTime invalidityDate)
		{
			this.tbsGen.AddCrlEntry(new DerInteger(userCertificate), new Time(revocationDate), reason, new DerGeneralizedTime(invalidityDate));
		}

		// Token: 0x06004037 RID: 16439 RVA: 0x0015F7C0 File Offset: 0x0015F7C0
		public void AddCrlEntry(BigInteger userCertificate, DateTime revocationDate, X509Extensions extensions)
		{
			this.tbsGen.AddCrlEntry(new DerInteger(userCertificate), new Time(revocationDate), extensions);
		}

		// Token: 0x06004038 RID: 16440 RVA: 0x0015F7DC File Offset: 0x0015F7DC
		public void AddCrl(X509Crl other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			ISet revokedCertificates = other.GetRevokedCertificates();
			if (revokedCertificates != null)
			{
				foreach (object obj in revokedCertificates)
				{
					X509CrlEntry x509CrlEntry = (X509CrlEntry)obj;
					try
					{
						this.tbsGen.AddCrlEntry(Asn1Sequence.GetInstance(Asn1Object.FromByteArray(x509CrlEntry.GetEncoded())));
					}
					catch (IOException e)
					{
						throw new CrlException("exception processing encoding of CRL", e);
					}
				}
			}
		}

		// Token: 0x06004039 RID: 16441 RVA: 0x0015F88C File Offset: 0x0015F88C
		[Obsolete("Not needed if Generate used with an ISignatureFactory")]
		public void SetSignatureAlgorithm(string signatureAlgorithm)
		{
			this.signatureAlgorithm = signatureAlgorithm;
			try
			{
				this.sigOID = X509Utilities.GetAlgorithmOid(signatureAlgorithm);
			}
			catch (Exception innerException)
			{
				throw new ArgumentException("Unknown signature type requested", innerException);
			}
			this.sigAlgId = X509Utilities.GetSigAlgID(this.sigOID, signatureAlgorithm);
			this.tbsGen.SetSignature(this.sigAlgId);
		}

		// Token: 0x0600403A RID: 16442 RVA: 0x0015F8F4 File Offset: 0x0015F8F4
		public void AddExtension(string oid, bool critical, Asn1Encodable extensionValue)
		{
			this.extGenerator.AddExtension(new DerObjectIdentifier(oid), critical, extensionValue);
		}

		// Token: 0x0600403B RID: 16443 RVA: 0x0015F90C File Offset: 0x0015F90C
		public void AddExtension(DerObjectIdentifier oid, bool critical, Asn1Encodable extensionValue)
		{
			this.extGenerator.AddExtension(oid, critical, extensionValue);
		}

		// Token: 0x0600403C RID: 16444 RVA: 0x0015F91C File Offset: 0x0015F91C
		public void AddExtension(string oid, bool critical, byte[] extensionValue)
		{
			this.extGenerator.AddExtension(new DerObjectIdentifier(oid), critical, new DerOctetString(extensionValue));
		}

		// Token: 0x0600403D RID: 16445 RVA: 0x0015F938 File Offset: 0x0015F938
		public void AddExtension(DerObjectIdentifier oid, bool critical, byte[] extensionValue)
		{
			this.extGenerator.AddExtension(oid, critical, new DerOctetString(extensionValue));
		}

		// Token: 0x0600403E RID: 16446 RVA: 0x0015F950 File Offset: 0x0015F950
		[Obsolete("Use Generate with an ISignatureFactory")]
		public X509Crl Generate(AsymmetricKeyParameter privateKey)
		{
			return this.Generate(privateKey, null);
		}

		// Token: 0x0600403F RID: 16447 RVA: 0x0015F95C File Offset: 0x0015F95C
		[Obsolete("Use Generate with an ISignatureFactory")]
		public X509Crl Generate(AsymmetricKeyParameter privateKey, SecureRandom random)
		{
			return this.Generate(new Asn1SignatureFactory(this.signatureAlgorithm, privateKey, random));
		}

		// Token: 0x06004040 RID: 16448 RVA: 0x0015F974 File Offset: 0x0015F974
		public X509Crl Generate(ISignatureFactory signatureCalculatorFactory)
		{
			this.tbsGen.SetSignature((AlgorithmIdentifier)signatureCalculatorFactory.AlgorithmDetails);
			TbsCertificateList tbsCertificateList = this.GenerateCertList();
			IStreamCalculator streamCalculator = signatureCalculatorFactory.CreateCalculator();
			byte[] derEncoded = tbsCertificateList.GetDerEncoded();
			streamCalculator.Stream.Write(derEncoded, 0, derEncoded.Length);
			Platform.Dispose(streamCalculator.Stream);
			return this.GenerateJcaObject(tbsCertificateList, (AlgorithmIdentifier)signatureCalculatorFactory.AlgorithmDetails, ((IBlockResult)streamCalculator.GetResult()).Collect());
		}

		// Token: 0x06004041 RID: 16449 RVA: 0x0015F9F0 File Offset: 0x0015F9F0
		private TbsCertificateList GenerateCertList()
		{
			if (!this.extGenerator.IsEmpty)
			{
				this.tbsGen.SetExtensions(this.extGenerator.Generate());
			}
			return this.tbsGen.GenerateTbsCertList();
		}

		// Token: 0x06004042 RID: 16450 RVA: 0x0015FA24 File Offset: 0x0015FA24
		private X509Crl GenerateJcaObject(TbsCertificateList tbsCrl, AlgorithmIdentifier algId, byte[] signature)
		{
			return new X509Crl(CertificateList.GetInstance(new DerSequence(new Asn1Encodable[]
			{
				tbsCrl,
				algId,
				new DerBitString(signature)
			})));
		}

		// Token: 0x17000ADC RID: 2780
		// (get) Token: 0x06004043 RID: 16451 RVA: 0x0015FA6C File Offset: 0x0015FA6C
		public IEnumerable SignatureAlgNames
		{
			get
			{
				return X509Utilities.GetAlgNames();
			}
		}

		// Token: 0x040020D9 RID: 8409
		private readonly X509ExtensionsGenerator extGenerator = new X509ExtensionsGenerator();

		// Token: 0x040020DA RID: 8410
		private V2TbsCertListGenerator tbsGen;

		// Token: 0x040020DB RID: 8411
		private DerObjectIdentifier sigOID;

		// Token: 0x040020DC RID: 8412
		private AlgorithmIdentifier sigAlgId;

		// Token: 0x040020DD RID: 8413
		private string signatureAlgorithm;
	}
}
