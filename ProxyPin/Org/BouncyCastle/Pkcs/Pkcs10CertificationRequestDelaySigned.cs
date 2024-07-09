using System;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.X509;

namespace Org.BouncyCastle.Pkcs
{
	// Token: 0x02000680 RID: 1664
	public class Pkcs10CertificationRequestDelaySigned : Pkcs10CertificationRequest
	{
		// Token: 0x06003A1B RID: 14875 RVA: 0x00138354 File Offset: 0x00138354
		protected Pkcs10CertificationRequestDelaySigned()
		{
		}

		// Token: 0x06003A1C RID: 14876 RVA: 0x0013835C File Offset: 0x0013835C
		public Pkcs10CertificationRequestDelaySigned(byte[] encoded) : base(encoded)
		{
		}

		// Token: 0x06003A1D RID: 14877 RVA: 0x00138368 File Offset: 0x00138368
		public Pkcs10CertificationRequestDelaySigned(Asn1Sequence seq) : base(seq)
		{
		}

		// Token: 0x06003A1E RID: 14878 RVA: 0x00138374 File Offset: 0x00138374
		public Pkcs10CertificationRequestDelaySigned(Stream input) : base(input)
		{
		}

		// Token: 0x06003A1F RID: 14879 RVA: 0x00138380 File Offset: 0x00138380
		public Pkcs10CertificationRequestDelaySigned(string signatureAlgorithm, X509Name subject, AsymmetricKeyParameter publicKey, Asn1Set attributes, AsymmetricKeyParameter signingKey) : base(signatureAlgorithm, subject, publicKey, attributes, signingKey)
		{
		}

		// Token: 0x06003A20 RID: 14880 RVA: 0x00138390 File Offset: 0x00138390
		public Pkcs10CertificationRequestDelaySigned(string signatureAlgorithm, X509Name subject, AsymmetricKeyParameter publicKey, Asn1Set attributes)
		{
			if (signatureAlgorithm == null)
			{
				throw new ArgumentNullException("signatureAlgorithm");
			}
			if (subject == null)
			{
				throw new ArgumentNullException("subject");
			}
			if (publicKey == null)
			{
				throw new ArgumentNullException("publicKey");
			}
			if (publicKey.IsPrivate)
			{
				throw new ArgumentException("expected public key", "publicKey");
			}
			string text = Platform.ToUpperInvariant(signatureAlgorithm);
			DerObjectIdentifier derObjectIdentifier = (DerObjectIdentifier)Pkcs10CertificationRequest.algorithms[text];
			if (derObjectIdentifier == null)
			{
				try
				{
					derObjectIdentifier = new DerObjectIdentifier(text);
				}
				catch (Exception innerException)
				{
					throw new ArgumentException("Unknown signature type requested", innerException);
				}
			}
			if (Pkcs10CertificationRequest.noParams.Contains(derObjectIdentifier))
			{
				this.sigAlgId = new AlgorithmIdentifier(derObjectIdentifier);
			}
			else if (Pkcs10CertificationRequest.exParams.Contains(text))
			{
				this.sigAlgId = new AlgorithmIdentifier(derObjectIdentifier, (Asn1Encodable)Pkcs10CertificationRequest.exParams[text]);
			}
			else
			{
				this.sigAlgId = new AlgorithmIdentifier(derObjectIdentifier, DerNull.Instance);
			}
			SubjectPublicKeyInfo pkInfo = SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(publicKey);
			this.reqInfo = new CertificationRequestInfo(subject, pkInfo, attributes);
		}

		// Token: 0x06003A21 RID: 14881 RVA: 0x001384B4 File Offset: 0x001384B4
		public byte[] GetDataToSign()
		{
			return this.reqInfo.GetDerEncoded();
		}

		// Token: 0x06003A22 RID: 14882 RVA: 0x001384C4 File Offset: 0x001384C4
		public void SignRequest(byte[] signedData)
		{
			this.sigBits = new DerBitString(signedData);
		}

		// Token: 0x06003A23 RID: 14883 RVA: 0x001384D4 File Offset: 0x001384D4
		public void SignRequest(DerBitString signedData)
		{
			this.sigBits = signedData;
		}
	}
}
