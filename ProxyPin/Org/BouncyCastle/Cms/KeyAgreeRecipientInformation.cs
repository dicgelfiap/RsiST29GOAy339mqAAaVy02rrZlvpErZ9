using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Asn1.Cms.Ecc;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Asn1.X9;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x02000305 RID: 773
	public class KeyAgreeRecipientInformation : RecipientInformation
	{
		// Token: 0x06001759 RID: 5977 RVA: 0x0007A174 File Offset: 0x0007A174
		internal static void ReadRecipientInfo(IList infos, KeyAgreeRecipientInfo info, CmsSecureReadable secureReadable)
		{
			try
			{
				foreach (object obj in info.RecipientEncryptedKeys)
				{
					Asn1Encodable asn1Encodable = (Asn1Encodable)obj;
					RecipientEncryptedKey instance = RecipientEncryptedKey.GetInstance(asn1Encodable.ToAsn1Object());
					RecipientID recipientID = new RecipientID();
					KeyAgreeRecipientIdentifier identifier = instance.Identifier;
					Org.BouncyCastle.Asn1.Cms.IssuerAndSerialNumber issuerAndSerialNumber = identifier.IssuerAndSerialNumber;
					if (issuerAndSerialNumber != null)
					{
						recipientID.Issuer = issuerAndSerialNumber.Name;
						recipientID.SerialNumber = issuerAndSerialNumber.SerialNumber.Value;
					}
					else
					{
						RecipientKeyIdentifier rkeyID = identifier.RKeyID;
						recipientID.SubjectKeyIdentifier = rkeyID.SubjectKeyIdentifier.GetOctets();
					}
					infos.Add(new KeyAgreeRecipientInformation(info, recipientID, instance.EncryptedKey, secureReadable));
				}
			}
			catch (IOException innerException)
			{
				throw new ArgumentException("invalid rid in KeyAgreeRecipientInformation", innerException);
			}
		}

		// Token: 0x0600175A RID: 5978 RVA: 0x0007A274 File Offset: 0x0007A274
		internal KeyAgreeRecipientInformation(KeyAgreeRecipientInfo info, RecipientID rid, Asn1OctetString encryptedKey, CmsSecureReadable secureReadable) : base(info.KeyEncryptionAlgorithm, secureReadable)
		{
			this.info = info;
			this.rid = rid;
			this.encryptedKey = encryptedKey;
		}

		// Token: 0x0600175B RID: 5979 RVA: 0x0007A29C File Offset: 0x0007A29C
		private AsymmetricKeyParameter GetSenderPublicKey(AsymmetricKeyParameter receiverPrivateKey, OriginatorIdentifierOrKey originator)
		{
			OriginatorPublicKey originatorPublicKey = originator.OriginatorPublicKey;
			if (originatorPublicKey != null)
			{
				return this.GetPublicKeyFromOriginatorPublicKey(receiverPrivateKey, originatorPublicKey);
			}
			OriginatorID originatorID = new OriginatorID();
			Org.BouncyCastle.Asn1.Cms.IssuerAndSerialNumber issuerAndSerialNumber = originator.IssuerAndSerialNumber;
			if (issuerAndSerialNumber != null)
			{
				originatorID.Issuer = issuerAndSerialNumber.Name;
				originatorID.SerialNumber = issuerAndSerialNumber.SerialNumber.Value;
			}
			else
			{
				SubjectKeyIdentifier subjectKeyIdentifier = originator.SubjectKeyIdentifier;
				originatorID.SubjectKeyIdentifier = subjectKeyIdentifier.GetKeyIdentifier();
			}
			return this.GetPublicKeyFromOriginatorID(originatorID);
		}

		// Token: 0x0600175C RID: 5980 RVA: 0x0007A314 File Offset: 0x0007A314
		private AsymmetricKeyParameter GetPublicKeyFromOriginatorPublicKey(AsymmetricKeyParameter receiverPrivateKey, OriginatorPublicKey originatorPublicKey)
		{
			PrivateKeyInfo privateKeyInfo = PrivateKeyInfoFactory.CreatePrivateKeyInfo(receiverPrivateKey);
			SubjectPublicKeyInfo keyInfo = new SubjectPublicKeyInfo(privateKeyInfo.PrivateKeyAlgorithm, originatorPublicKey.PublicKey.GetBytes());
			return PublicKeyFactory.CreateKey(keyInfo);
		}

		// Token: 0x0600175D RID: 5981 RVA: 0x0007A34C File Offset: 0x0007A34C
		private AsymmetricKeyParameter GetPublicKeyFromOriginatorID(OriginatorID origID)
		{
			throw new CmsException("No support for 'originator' as IssuerAndSerialNumber or SubjectKeyIdentifier");
		}

		// Token: 0x0600175E RID: 5982 RVA: 0x0007A358 File Offset: 0x0007A358
		private KeyParameter CalculateAgreedWrapKey(string wrapAlg, AsymmetricKeyParameter senderPublicKey, AsymmetricKeyParameter receiverPrivateKey)
		{
			DerObjectIdentifier algorithm = this.keyEncAlg.Algorithm;
			ICipherParameters cipherParameters = senderPublicKey;
			ICipherParameters cipherParameters2 = receiverPrivateKey;
			if (algorithm.Id.Equals(CmsEnvelopedGenerator.ECMqvSha1Kdf))
			{
				byte[] octets = this.info.UserKeyingMaterial.GetOctets();
				MQVuserKeyingMaterial instance = MQVuserKeyingMaterial.GetInstance(Asn1Object.FromByteArray(octets));
				AsymmetricKeyParameter publicKeyFromOriginatorPublicKey = this.GetPublicKeyFromOriginatorPublicKey(receiverPrivateKey, instance.EphemeralPublicKey);
				cipherParameters = new MqvPublicParameters((ECPublicKeyParameters)cipherParameters, (ECPublicKeyParameters)publicKeyFromOriginatorPublicKey);
				cipherParameters2 = new MqvPrivateParameters((ECPrivateKeyParameters)cipherParameters2, (ECPrivateKeyParameters)cipherParameters2);
			}
			IBasicAgreement basicAgreementWithKdf = AgreementUtilities.GetBasicAgreementWithKdf(algorithm, wrapAlg);
			basicAgreementWithKdf.Init(cipherParameters2);
			BigInteger s = basicAgreementWithKdf.CalculateAgreement(cipherParameters);
			int qLength = GeneratorUtilities.GetDefaultKeySize(wrapAlg) / 8;
			byte[] keyBytes = X9IntegerConverter.IntegerToBytes(s, qLength);
			return ParameterUtilities.CreateKeyParameter(wrapAlg, keyBytes);
		}

		// Token: 0x0600175F RID: 5983 RVA: 0x0007A41C File Offset: 0x0007A41C
		private KeyParameter UnwrapSessionKey(string wrapAlg, KeyParameter agreedKey)
		{
			byte[] octets = this.encryptedKey.GetOctets();
			IWrapper wrapper = WrapperUtilities.GetWrapper(wrapAlg);
			wrapper.Init(false, agreedKey);
			byte[] keyBytes = wrapper.Unwrap(octets, 0, octets.Length);
			return ParameterUtilities.CreateKeyParameter(base.GetContentAlgorithmName(), keyBytes);
		}

		// Token: 0x06001760 RID: 5984 RVA: 0x0007A460 File Offset: 0x0007A460
		internal KeyParameter GetSessionKey(AsymmetricKeyParameter receiverPrivateKey)
		{
			KeyParameter result;
			try
			{
				string id = DerObjectIdentifier.GetInstance(Asn1Sequence.GetInstance(this.keyEncAlg.Parameters)[0]).Id;
				AsymmetricKeyParameter senderPublicKey = this.GetSenderPublicKey(receiverPrivateKey, this.info.Originator);
				KeyParameter agreedKey = this.CalculateAgreedWrapKey(id, senderPublicKey, receiverPrivateKey);
				result = this.UnwrapSessionKey(id, agreedKey);
			}
			catch (SecurityUtilityException e)
			{
				throw new CmsException("couldn't create cipher.", e);
			}
			catch (InvalidKeyException e2)
			{
				throw new CmsException("key invalid in message.", e2);
			}
			catch (Exception e3)
			{
				throw new CmsException("originator key invalid.", e3);
			}
			return result;
		}

		// Token: 0x06001761 RID: 5985 RVA: 0x0007A510 File Offset: 0x0007A510
		public override CmsTypedStream GetContentStream(ICipherParameters key)
		{
			if (!(key is AsymmetricKeyParameter))
			{
				throw new ArgumentException("KeyAgreement requires asymmetric key", "key");
			}
			AsymmetricKeyParameter asymmetricKeyParameter = (AsymmetricKeyParameter)key;
			if (!asymmetricKeyParameter.IsPrivate)
			{
				throw new ArgumentException("Expected private key", "key");
			}
			KeyParameter sessionKey = this.GetSessionKey(asymmetricKeyParameter);
			return base.GetContentFromSessionKey(sessionKey);
		}

		// Token: 0x04000FB3 RID: 4019
		private KeyAgreeRecipientInfo info;

		// Token: 0x04000FB4 RID: 4020
		private Asn1OctetString encryptedKey;
	}
}
