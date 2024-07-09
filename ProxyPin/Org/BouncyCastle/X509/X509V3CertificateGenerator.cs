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
using Org.BouncyCastle.X509.Extension;

namespace Org.BouncyCastle.X509
{
	// Token: 0x02000726 RID: 1830
	public class X509V3CertificateGenerator
	{
		// Token: 0x06004044 RID: 16452 RVA: 0x0015FA74 File Offset: 0x0015FA74
		public X509V3CertificateGenerator()
		{
			this.tbsGen = new V3TbsCertificateGenerator();
		}

		// Token: 0x06004045 RID: 16453 RVA: 0x0015FA94 File Offset: 0x0015FA94
		public void Reset()
		{
			this.tbsGen = new V3TbsCertificateGenerator();
			this.extGenerator.Reset();
		}

		// Token: 0x06004046 RID: 16454 RVA: 0x0015FAAC File Offset: 0x0015FAAC
		public void SetSerialNumber(BigInteger serialNumber)
		{
			if (serialNumber.SignValue <= 0)
			{
				throw new ArgumentException("serial number must be a positive integer", "serialNumber");
			}
			this.tbsGen.SetSerialNumber(new DerInteger(serialNumber));
		}

		// Token: 0x06004047 RID: 16455 RVA: 0x0015FADC File Offset: 0x0015FADC
		public void SetIssuerDN(X509Name issuer)
		{
			this.tbsGen.SetIssuer(issuer);
		}

		// Token: 0x06004048 RID: 16456 RVA: 0x0015FAEC File Offset: 0x0015FAEC
		public void SetNotBefore(DateTime date)
		{
			this.tbsGen.SetStartDate(new Time(date));
		}

		// Token: 0x06004049 RID: 16457 RVA: 0x0015FB00 File Offset: 0x0015FB00
		public void SetNotAfter(DateTime date)
		{
			this.tbsGen.SetEndDate(new Time(date));
		}

		// Token: 0x0600404A RID: 16458 RVA: 0x0015FB14 File Offset: 0x0015FB14
		public void SetSubjectDN(X509Name subject)
		{
			this.tbsGen.SetSubject(subject);
		}

		// Token: 0x0600404B RID: 16459 RVA: 0x0015FB24 File Offset: 0x0015FB24
		public void SetPublicKey(AsymmetricKeyParameter publicKey)
		{
			this.tbsGen.SetSubjectPublicKeyInfo(SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(publicKey));
		}

		// Token: 0x0600404C RID: 16460 RVA: 0x0015FB38 File Offset: 0x0015FB38
		[Obsolete("Not needed if Generate used with an ISignatureFactory")]
		public void SetSignatureAlgorithm(string signatureAlgorithm)
		{
			this.signatureAlgorithm = signatureAlgorithm;
			try
			{
				this.sigOid = X509Utilities.GetAlgorithmOid(signatureAlgorithm);
			}
			catch (Exception)
			{
				throw new ArgumentException("Unknown signature type requested: " + signatureAlgorithm);
			}
			this.sigAlgId = X509Utilities.GetSigAlgID(this.sigOid, signatureAlgorithm);
			this.tbsGen.SetSignature(this.sigAlgId);
		}

		// Token: 0x0600404D RID: 16461 RVA: 0x0015FBA4 File Offset: 0x0015FBA4
		public void SetSubjectUniqueID(bool[] uniqueID)
		{
			this.tbsGen.SetSubjectUniqueID(this.booleanToBitString(uniqueID));
		}

		// Token: 0x0600404E RID: 16462 RVA: 0x0015FBB8 File Offset: 0x0015FBB8
		public void SetIssuerUniqueID(bool[] uniqueID)
		{
			this.tbsGen.SetIssuerUniqueID(this.booleanToBitString(uniqueID));
		}

		// Token: 0x0600404F RID: 16463 RVA: 0x0015FBCC File Offset: 0x0015FBCC
		private DerBitString booleanToBitString(bool[] id)
		{
			byte[] array = new byte[(id.Length + 7) / 8];
			for (int num = 0; num != id.Length; num++)
			{
				if (id[num])
				{
					byte[] array2;
					IntPtr intPtr;
					(array2 = array)[(int)(intPtr = (IntPtr)(num / 8))] = (array2[(int)intPtr] | (byte)(1 << 7 - num % 8));
				}
			}
			int num2 = id.Length % 8;
			if (num2 == 0)
			{
				return new DerBitString(array);
			}
			return new DerBitString(array, 8 - num2);
		}

		// Token: 0x06004050 RID: 16464 RVA: 0x0015FC3C File Offset: 0x0015FC3C
		public void AddExtension(string oid, bool critical, Asn1Encodable extensionValue)
		{
			this.extGenerator.AddExtension(new DerObjectIdentifier(oid), critical, extensionValue);
		}

		// Token: 0x06004051 RID: 16465 RVA: 0x0015FC54 File Offset: 0x0015FC54
		public void AddExtension(DerObjectIdentifier oid, bool critical, Asn1Encodable extensionValue)
		{
			this.extGenerator.AddExtension(oid, critical, extensionValue);
		}

		// Token: 0x06004052 RID: 16466 RVA: 0x0015FC64 File Offset: 0x0015FC64
		public void AddExtension(string oid, bool critical, byte[] extensionValue)
		{
			this.extGenerator.AddExtension(new DerObjectIdentifier(oid), critical, new DerOctetString(extensionValue));
		}

		// Token: 0x06004053 RID: 16467 RVA: 0x0015FC80 File Offset: 0x0015FC80
		public void AddExtension(DerObjectIdentifier oid, bool critical, byte[] extensionValue)
		{
			this.extGenerator.AddExtension(oid, critical, new DerOctetString(extensionValue));
		}

		// Token: 0x06004054 RID: 16468 RVA: 0x0015FC98 File Offset: 0x0015FC98
		public void CopyAndAddExtension(string oid, bool critical, X509Certificate cert)
		{
			this.CopyAndAddExtension(new DerObjectIdentifier(oid), critical, cert);
		}

		// Token: 0x06004055 RID: 16469 RVA: 0x0015FCA8 File Offset: 0x0015FCA8
		public void CopyAndAddExtension(DerObjectIdentifier oid, bool critical, X509Certificate cert)
		{
			Asn1OctetString extensionValue = cert.GetExtensionValue(oid);
			if (extensionValue == null)
			{
				throw new CertificateParsingException("extension " + oid + " not present");
			}
			try
			{
				Asn1Encodable extensionValue2 = X509ExtensionUtilities.FromExtensionValue(extensionValue);
				this.AddExtension(oid, critical, extensionValue2);
			}
			catch (Exception ex)
			{
				throw new CertificateParsingException(ex.Message, ex);
			}
		}

		// Token: 0x06004056 RID: 16470 RVA: 0x0015FD0C File Offset: 0x0015FD0C
		[Obsolete("Use Generate with an ISignatureFactory")]
		public X509Certificate Generate(AsymmetricKeyParameter privateKey)
		{
			return this.Generate(privateKey, null);
		}

		// Token: 0x06004057 RID: 16471 RVA: 0x0015FD18 File Offset: 0x0015FD18
		[Obsolete("Use Generate with an ISignatureFactory")]
		public X509Certificate Generate(AsymmetricKeyParameter privateKey, SecureRandom random)
		{
			return this.Generate(new Asn1SignatureFactory(this.signatureAlgorithm, privateKey, random));
		}

		// Token: 0x06004058 RID: 16472 RVA: 0x0015FD30 File Offset: 0x0015FD30
		public X509Certificate Generate(ISignatureFactory signatureCalculatorFactory)
		{
			this.tbsGen.SetSignature((AlgorithmIdentifier)signatureCalculatorFactory.AlgorithmDetails);
			if (!this.extGenerator.IsEmpty)
			{
				this.tbsGen.SetExtensions(this.extGenerator.Generate());
			}
			TbsCertificateStructure tbsCertificateStructure = this.tbsGen.GenerateTbsCertificate();
			IStreamCalculator streamCalculator = signatureCalculatorFactory.CreateCalculator();
			byte[] derEncoded = tbsCertificateStructure.GetDerEncoded();
			streamCalculator.Stream.Write(derEncoded, 0, derEncoded.Length);
			Platform.Dispose(streamCalculator.Stream);
			return this.GenerateJcaObject(tbsCertificateStructure, (AlgorithmIdentifier)signatureCalculatorFactory.AlgorithmDetails, ((IBlockResult)streamCalculator.GetResult()).Collect());
		}

		// Token: 0x06004059 RID: 16473 RVA: 0x0015FDD4 File Offset: 0x0015FDD4
		private X509Certificate GenerateJcaObject(TbsCertificateStructure tbsCert, AlgorithmIdentifier sigAlg, byte[] signature)
		{
			return new X509Certificate(new X509CertificateStructure(tbsCert, sigAlg, new DerBitString(signature)));
		}

		// Token: 0x17000ADD RID: 2781
		// (get) Token: 0x0600405A RID: 16474 RVA: 0x0015FDE8 File Offset: 0x0015FDE8
		public IEnumerable SignatureAlgNames
		{
			get
			{
				return X509Utilities.GetAlgNames();
			}
		}

		// Token: 0x040020DE RID: 8414
		private readonly X509ExtensionsGenerator extGenerator = new X509ExtensionsGenerator();

		// Token: 0x040020DF RID: 8415
		private V3TbsCertificateGenerator tbsGen;

		// Token: 0x040020E0 RID: 8416
		private DerObjectIdentifier sigOid;

		// Token: 0x040020E1 RID: 8417
		private AlgorithmIdentifier sigAlgId;

		// Token: 0x040020E2 RID: 8418
		private string signatureAlgorithm;
	}
}
