using System;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x02000307 RID: 775
	public class KeyTransRecipientInformation : RecipientInformation
	{
		// Token: 0x0600176C RID: 5996 RVA: 0x0007A718 File Offset: 0x0007A718
		internal KeyTransRecipientInformation(KeyTransRecipientInfo info, CmsSecureReadable secureReadable) : base(info.KeyEncryptionAlgorithm, secureReadable)
		{
			this.info = info;
			this.rid = new RecipientID();
			RecipientIdentifier recipientIdentifier = info.RecipientIdentifier;
			try
			{
				if (recipientIdentifier.IsTagged)
				{
					Asn1OctetString instance = Asn1OctetString.GetInstance(recipientIdentifier.ID);
					this.rid.SubjectKeyIdentifier = instance.GetOctets();
				}
				else
				{
					Org.BouncyCastle.Asn1.Cms.IssuerAndSerialNumber instance2 = Org.BouncyCastle.Asn1.Cms.IssuerAndSerialNumber.GetInstance(recipientIdentifier.ID);
					this.rid.Issuer = instance2.Name;
					this.rid.SerialNumber = instance2.SerialNumber.Value;
				}
			}
			catch (IOException)
			{
				throw new ArgumentException("invalid rid in KeyTransRecipientInformation");
			}
		}

		// Token: 0x0600176D RID: 5997 RVA: 0x0007A7CC File Offset: 0x0007A7CC
		private string GetExchangeEncryptionAlgorithmName(AlgorithmIdentifier algo)
		{
			DerObjectIdentifier algorithm = algo.Algorithm;
			if (PkcsObjectIdentifiers.RsaEncryption.Equals(algorithm))
			{
				return "RSA//PKCS1Padding";
			}
			if (PkcsObjectIdentifiers.IdRsaesOaep.Equals(algorithm))
			{
				RsaesOaepParameters instance = RsaesOaepParameters.GetInstance(algo.Parameters);
				return "RSA//OAEPWITH" + DigestUtilities.GetAlgorithmName(instance.HashAlgorithm.Algorithm) + "ANDMGF1Padding";
			}
			return algorithm.Id;
		}

		// Token: 0x0600176E RID: 5998 RVA: 0x0007A83C File Offset: 0x0007A83C
		internal KeyParameter UnwrapKey(ICipherParameters key)
		{
			byte[] octets = this.info.EncryptedKey.GetOctets();
			string exchangeEncryptionAlgorithmName = this.GetExchangeEncryptionAlgorithmName(this.keyEncAlg);
			KeyParameter result;
			try
			{
				IWrapper wrapper = WrapperUtilities.GetWrapper(exchangeEncryptionAlgorithmName);
				wrapper.Init(false, key);
				result = ParameterUtilities.CreateKeyParameter(base.GetContentAlgorithmName(), wrapper.Unwrap(octets, 0, octets.Length));
			}
			catch (SecurityUtilityException e)
			{
				throw new CmsException("couldn't create cipher.", e);
			}
			catch (InvalidKeyException e2)
			{
				throw new CmsException("key invalid in message.", e2);
			}
			catch (DataLengthException e3)
			{
				throw new CmsException("illegal blocksize in message.", e3);
			}
			catch (InvalidCipherTextException e4)
			{
				throw new CmsException("bad padding in message.", e4);
			}
			return result;
		}

		// Token: 0x0600176F RID: 5999 RVA: 0x0007A904 File Offset: 0x0007A904
		public override CmsTypedStream GetContentStream(ICipherParameters key)
		{
			KeyParameter sKey = this.UnwrapKey(key);
			return base.GetContentFromSessionKey(sKey);
		}

		// Token: 0x04000FBC RID: 4028
		private KeyTransRecipientInfo info;
	}
}
