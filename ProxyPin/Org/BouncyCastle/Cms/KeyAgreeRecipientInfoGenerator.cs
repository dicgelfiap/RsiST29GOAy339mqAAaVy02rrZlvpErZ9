using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Asn1.Cms.Ecc;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Asn1.X9;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.X509;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x02000304 RID: 772
	internal class KeyAgreeRecipientInfoGenerator : RecipientInfoGenerator
	{
		// Token: 0x06001751 RID: 5969 RVA: 0x00079DA4 File Offset: 0x00079DA4
		internal KeyAgreeRecipientInfoGenerator()
		{
		}

		// Token: 0x17000526 RID: 1318
		// (set) Token: 0x06001752 RID: 5970 RVA: 0x00079DAC File Offset: 0x00079DAC
		internal DerObjectIdentifier KeyAgreementOID
		{
			set
			{
				this.keyAgreementOID = value;
			}
		}

		// Token: 0x17000527 RID: 1319
		// (set) Token: 0x06001753 RID: 5971 RVA: 0x00079DB8 File Offset: 0x00079DB8
		internal DerObjectIdentifier KeyEncryptionOID
		{
			set
			{
				this.keyEncryptionOID = value;
			}
		}

		// Token: 0x17000528 RID: 1320
		// (set) Token: 0x06001754 RID: 5972 RVA: 0x00079DC4 File Offset: 0x00079DC4
		internal ICollection RecipientCerts
		{
			set
			{
				this.recipientCerts = Platform.CreateArrayList(value);
			}
		}

		// Token: 0x17000529 RID: 1321
		// (set) Token: 0x06001755 RID: 5973 RVA: 0x00079DD4 File Offset: 0x00079DD4
		internal AsymmetricCipherKeyPair SenderKeyPair
		{
			set
			{
				this.senderKeyPair = value;
			}
		}

		// Token: 0x06001756 RID: 5974 RVA: 0x00079DE0 File Offset: 0x00079DE0
		public RecipientInfo Generate(KeyParameter contentEncryptionKey, SecureRandom random)
		{
			byte[] key = contentEncryptionKey.GetKey();
			AsymmetricKeyParameter @public = this.senderKeyPair.Public;
			ICipherParameters cipherParameters = this.senderKeyPair.Private;
			OriginatorIdentifierOrKey originator;
			try
			{
				originator = new OriginatorIdentifierOrKey(KeyAgreeRecipientInfoGenerator.CreateOriginatorPublicKey(@public));
			}
			catch (IOException arg)
			{
				throw new InvalidKeyException("cannot extract originator public key: " + arg);
			}
			Asn1OctetString ukm = null;
			if (this.keyAgreementOID.Id.Equals(CmsEnvelopedGenerator.ECMqvSha1Kdf))
			{
				try
				{
					IAsymmetricCipherKeyPairGenerator keyPairGenerator = GeneratorUtilities.GetKeyPairGenerator(this.keyAgreementOID);
					keyPairGenerator.Init(((ECPublicKeyParameters)@public).CreateKeyGenerationParameters(random));
					AsymmetricCipherKeyPair asymmetricCipherKeyPair = keyPairGenerator.GenerateKeyPair();
					ukm = new DerOctetString(new MQVuserKeyingMaterial(KeyAgreeRecipientInfoGenerator.CreateOriginatorPublicKey(asymmetricCipherKeyPair.Public), null));
					cipherParameters = new MqvPrivateParameters((ECPrivateKeyParameters)cipherParameters, (ECPrivateKeyParameters)asymmetricCipherKeyPair.Private, (ECPublicKeyParameters)asymmetricCipherKeyPair.Public);
				}
				catch (IOException arg2)
				{
					throw new InvalidKeyException("cannot extract MQV ephemeral public key: " + arg2);
				}
				catch (SecurityUtilityException arg3)
				{
					throw new InvalidKeyException("cannot determine MQV ephemeral key pair parameters from public key: " + arg3);
				}
			}
			DerSequence parameters = new DerSequence(new Asn1Encodable[]
			{
				this.keyEncryptionOID,
				DerNull.Instance
			});
			AlgorithmIdentifier keyEncryptionAlgorithm = new AlgorithmIdentifier(this.keyAgreementOID, parameters);
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector();
			foreach (object obj in this.recipientCerts)
			{
				X509Certificate x509Certificate = (X509Certificate)obj;
				TbsCertificateStructure instance;
				try
				{
					instance = TbsCertificateStructure.GetInstance(Asn1Object.FromByteArray(x509Certificate.GetTbsCertificate()));
				}
				catch (Exception)
				{
					throw new ArgumentException("can't extract TBS structure from certificate");
				}
				IssuerAndSerialNumber issuerSerial = new IssuerAndSerialNumber(instance.Issuer, instance.SerialNumber.Value);
				KeyAgreeRecipientIdentifier id = new KeyAgreeRecipientIdentifier(issuerSerial);
				ICipherParameters cipherParameters2 = x509Certificate.GetPublicKey();
				if (this.keyAgreementOID.Id.Equals(CmsEnvelopedGenerator.ECMqvSha1Kdf))
				{
					cipherParameters2 = new MqvPublicParameters((ECPublicKeyParameters)cipherParameters2, (ECPublicKeyParameters)cipherParameters2);
				}
				IBasicAgreement basicAgreementWithKdf = AgreementUtilities.GetBasicAgreementWithKdf(this.keyAgreementOID, this.keyEncryptionOID.Id);
				basicAgreementWithKdf.Init(new ParametersWithRandom(cipherParameters, random));
				BigInteger s = basicAgreementWithKdf.CalculateAgreement(cipherParameters2);
				int qLength = GeneratorUtilities.GetDefaultKeySize(this.keyEncryptionOID) / 8;
				byte[] keyBytes = X9IntegerConverter.IntegerToBytes(s, qLength);
				KeyParameter parameters2 = ParameterUtilities.CreateKeyParameter(this.keyEncryptionOID, keyBytes);
				IWrapper wrapper = KeyAgreeRecipientInfoGenerator.Helper.CreateWrapper(this.keyEncryptionOID.Id);
				wrapper.Init(true, new ParametersWithRandom(parameters2, random));
				byte[] str = wrapper.Wrap(key, 0, key.Length);
				Asn1OctetString encryptedKey = new DerOctetString(str);
				asn1EncodableVector.Add(new RecipientEncryptedKey(id, encryptedKey));
			}
			return new RecipientInfo(new KeyAgreeRecipientInfo(originator, ukm, keyEncryptionAlgorithm, new DerSequence(asn1EncodableVector)));
		}

		// Token: 0x06001757 RID: 5975 RVA: 0x0007A128 File Offset: 0x0007A128
		private static OriginatorPublicKey CreateOriginatorPublicKey(AsymmetricKeyParameter publicKey)
		{
			SubjectPublicKeyInfo subjectPublicKeyInfo = SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(publicKey);
			return new OriginatorPublicKey(new AlgorithmIdentifier(subjectPublicKeyInfo.AlgorithmID.Algorithm, DerNull.Instance), subjectPublicKeyInfo.PublicKeyData.GetBytes());
		}

		// Token: 0x04000FAE RID: 4014
		private static readonly CmsEnvelopedHelper Helper = CmsEnvelopedHelper.Instance;

		// Token: 0x04000FAF RID: 4015
		private DerObjectIdentifier keyAgreementOID;

		// Token: 0x04000FB0 RID: 4016
		private DerObjectIdentifier keyEncryptionOID;

		// Token: 0x04000FB1 RID: 4017
		private IList recipientCerts;

		// Token: 0x04000FB2 RID: 4018
		private AsymmetricCipherKeyPair senderKeyPair;
	}
}
