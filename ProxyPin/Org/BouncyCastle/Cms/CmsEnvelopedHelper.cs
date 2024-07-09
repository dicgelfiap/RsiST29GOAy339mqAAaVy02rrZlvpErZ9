using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.IO;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.IO;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x020002E8 RID: 744
	internal class CmsEnvelopedHelper
	{
		// Token: 0x06001665 RID: 5733 RVA: 0x00074DD4 File Offset: 0x00074DD4
		static CmsEnvelopedHelper()
		{
			CmsEnvelopedHelper.KeySizes.Add(CmsEnvelopedGenerator.DesEde3Cbc, 192);
			CmsEnvelopedHelper.KeySizes.Add(CmsEnvelopedGenerator.Aes128Cbc, 128);
			CmsEnvelopedHelper.KeySizes.Add(CmsEnvelopedGenerator.Aes192Cbc, 192);
			CmsEnvelopedHelper.KeySizes.Add(CmsEnvelopedGenerator.Aes256Cbc, 256);
			CmsEnvelopedHelper.BaseCipherNames.Add(CmsEnvelopedGenerator.DesEde3Cbc, "DESEDE");
			CmsEnvelopedHelper.BaseCipherNames.Add(CmsEnvelopedGenerator.Aes128Cbc, "AES");
			CmsEnvelopedHelper.BaseCipherNames.Add(CmsEnvelopedGenerator.Aes192Cbc, "AES");
			CmsEnvelopedHelper.BaseCipherNames.Add(CmsEnvelopedGenerator.Aes256Cbc, "AES");
		}

		// Token: 0x06001666 RID: 5734 RVA: 0x00074EB8 File Offset: 0x00074EB8
		private string GetAsymmetricEncryptionAlgName(string encryptionAlgOid)
		{
			if (PkcsObjectIdentifiers.RsaEncryption.Id.Equals(encryptionAlgOid))
			{
				return "RSA/ECB/PKCS1Padding";
			}
			return encryptionAlgOid;
		}

		// Token: 0x06001667 RID: 5735 RVA: 0x00074ED8 File Offset: 0x00074ED8
		internal IBufferedCipher CreateAsymmetricCipher(string encryptionOid)
		{
			string asymmetricEncryptionAlgName = this.GetAsymmetricEncryptionAlgName(encryptionOid);
			if (!asymmetricEncryptionAlgName.Equals(encryptionOid))
			{
				try
				{
					return CipherUtilities.GetCipher(asymmetricEncryptionAlgName);
				}
				catch (SecurityUtilityException)
				{
				}
			}
			return CipherUtilities.GetCipher(encryptionOid);
		}

		// Token: 0x06001668 RID: 5736 RVA: 0x00074F24 File Offset: 0x00074F24
		internal IWrapper CreateWrapper(string encryptionOid)
		{
			IWrapper wrapper;
			try
			{
				wrapper = WrapperUtilities.GetWrapper(encryptionOid);
			}
			catch (SecurityUtilityException)
			{
				wrapper = WrapperUtilities.GetWrapper(this.GetAsymmetricEncryptionAlgName(encryptionOid));
			}
			return wrapper;
		}

		// Token: 0x06001669 RID: 5737 RVA: 0x00074F64 File Offset: 0x00074F64
		internal string GetRfc3211WrapperName(string oid)
		{
			if (oid == null)
			{
				throw new ArgumentNullException("oid");
			}
			string text = (string)CmsEnvelopedHelper.BaseCipherNames[oid];
			if (text == null)
			{
				throw new ArgumentException("no name for " + oid, "oid");
			}
			return text + "RFC3211Wrap";
		}

		// Token: 0x0600166A RID: 5738 RVA: 0x00074FC0 File Offset: 0x00074FC0
		internal int GetKeySize(string oid)
		{
			if (!CmsEnvelopedHelper.KeySizes.Contains(oid))
			{
				throw new ArgumentException("no keysize for " + oid, "oid");
			}
			return (int)CmsEnvelopedHelper.KeySizes[oid];
		}

		// Token: 0x0600166B RID: 5739 RVA: 0x00074FFC File Offset: 0x00074FFC
		internal static RecipientInformationStore BuildRecipientInformationStore(Asn1Set recipientInfos, CmsSecureReadable secureReadable)
		{
			IList list = Platform.CreateArrayList();
			for (int num = 0; num != recipientInfos.Count; num++)
			{
				RecipientInfo instance = RecipientInfo.GetInstance(recipientInfos[num]);
				CmsEnvelopedHelper.ReadRecipientInfo(list, instance, secureReadable);
			}
			return new RecipientInformationStore(list);
		}

		// Token: 0x0600166C RID: 5740 RVA: 0x00075044 File Offset: 0x00075044
		private static void ReadRecipientInfo(IList infos, RecipientInfo info, CmsSecureReadable secureReadable)
		{
			Asn1Encodable info2 = info.Info;
			if (info2 is KeyTransRecipientInfo)
			{
				infos.Add(new KeyTransRecipientInformation((KeyTransRecipientInfo)info2, secureReadable));
				return;
			}
			if (info2 is KekRecipientInfo)
			{
				infos.Add(new KekRecipientInformation((KekRecipientInfo)info2, secureReadable));
				return;
			}
			if (info2 is KeyAgreeRecipientInfo)
			{
				KeyAgreeRecipientInformation.ReadRecipientInfo(infos, (KeyAgreeRecipientInfo)info2, secureReadable);
				return;
			}
			if (info2 is PasswordRecipientInfo)
			{
				infos.Add(new PasswordRecipientInformation((PasswordRecipientInfo)info2, secureReadable));
			}
		}

		// Token: 0x04000F3C RID: 3900
		internal static readonly CmsEnvelopedHelper Instance = new CmsEnvelopedHelper();

		// Token: 0x04000F3D RID: 3901
		private static readonly IDictionary KeySizes = Platform.CreateHashtable();

		// Token: 0x04000F3E RID: 3902
		private static readonly IDictionary BaseCipherNames = Platform.CreateHashtable();

		// Token: 0x02000DDD RID: 3549
		internal class CmsAuthenticatedSecureReadable : CmsSecureReadable
		{
			// Token: 0x06008B78 RID: 35704 RVA: 0x0029DB50 File Offset: 0x0029DB50
			internal CmsAuthenticatedSecureReadable(AlgorithmIdentifier algorithm, CmsReadable readable)
			{
				this.algorithm = algorithm;
				this.readable = readable;
			}

			// Token: 0x17001D69 RID: 7529
			// (get) Token: 0x06008B79 RID: 35705 RVA: 0x0029DB68 File Offset: 0x0029DB68
			public AlgorithmIdentifier Algorithm
			{
				get
				{
					return this.algorithm;
				}
			}

			// Token: 0x17001D6A RID: 7530
			// (get) Token: 0x06008B7A RID: 35706 RVA: 0x0029DB70 File Offset: 0x0029DB70
			public object CryptoObject
			{
				get
				{
					return this.mac;
				}
			}

			// Token: 0x06008B7B RID: 35707 RVA: 0x0029DB78 File Offset: 0x0029DB78
			public CmsReadable GetReadable(KeyParameter sKey)
			{
				string id = this.algorithm.Algorithm.Id;
				try
				{
					this.mac = MacUtilities.GetMac(id);
					this.mac.Init(sKey);
				}
				catch (SecurityUtilityException e)
				{
					throw new CmsException("couldn't create cipher.", e);
				}
				catch (InvalidKeyException e2)
				{
					throw new CmsException("key invalid in message.", e2);
				}
				catch (IOException e3)
				{
					throw new CmsException("error decoding algorithm parameters.", e3);
				}
				CmsReadable result;
				try
				{
					result = new CmsProcessableInputStream(new TeeInputStream(this.readable.GetInputStream(), new MacSink(this.mac)));
				}
				catch (IOException e4)
				{
					throw new CmsException("error reading content.", e4);
				}
				return result;
			}

			// Token: 0x0400407A RID: 16506
			private AlgorithmIdentifier algorithm;

			// Token: 0x0400407B RID: 16507
			private IMac mac;

			// Token: 0x0400407C RID: 16508
			private CmsReadable readable;
		}

		// Token: 0x02000DDE RID: 3550
		internal class CmsEnvelopedSecureReadable : CmsSecureReadable
		{
			// Token: 0x06008B7C RID: 35708 RVA: 0x0029DC48 File Offset: 0x0029DC48
			internal CmsEnvelopedSecureReadable(AlgorithmIdentifier algorithm, CmsReadable readable)
			{
				this.algorithm = algorithm;
				this.readable = readable;
			}

			// Token: 0x17001D6B RID: 7531
			// (get) Token: 0x06008B7D RID: 35709 RVA: 0x0029DC60 File Offset: 0x0029DC60
			public AlgorithmIdentifier Algorithm
			{
				get
				{
					return this.algorithm;
				}
			}

			// Token: 0x17001D6C RID: 7532
			// (get) Token: 0x06008B7E RID: 35710 RVA: 0x0029DC68 File Offset: 0x0029DC68
			public object CryptoObject
			{
				get
				{
					return this.cipher;
				}
			}

			// Token: 0x06008B7F RID: 35711 RVA: 0x0029DC70 File Offset: 0x0029DC70
			public CmsReadable GetReadable(KeyParameter sKey)
			{
				try
				{
					this.cipher = CipherUtilities.GetCipher(this.algorithm.Algorithm);
					Asn1Encodable parameters = this.algorithm.Parameters;
					Asn1Object asn1Object = (parameters == null) ? null : parameters.ToAsn1Object();
					ICipherParameters cipherParameters = sKey;
					if (asn1Object != null && !(asn1Object is Asn1Null))
					{
						cipherParameters = ParameterUtilities.GetCipherParameters(this.algorithm.Algorithm, cipherParameters, asn1Object);
					}
					else
					{
						string id = this.algorithm.Algorithm.Id;
						if (id.Equals(CmsEnvelopedGenerator.DesEde3Cbc) || id.Equals("1.3.6.1.4.1.188.7.1.1.2") || id.Equals("1.2.840.113533.7.66.10"))
						{
							cipherParameters = new ParametersWithIV(cipherParameters, new byte[8]);
						}
					}
					this.cipher.Init(false, cipherParameters);
				}
				catch (SecurityUtilityException e)
				{
					throw new CmsException("couldn't create cipher.", e);
				}
				catch (InvalidKeyException e2)
				{
					throw new CmsException("key invalid in message.", e2);
				}
				catch (IOException e3)
				{
					throw new CmsException("error decoding algorithm parameters.", e3);
				}
				CmsReadable result;
				try
				{
					result = new CmsProcessableInputStream(new CipherStream(this.readable.GetInputStream(), this.cipher, null));
				}
				catch (IOException e4)
				{
					throw new CmsException("error reading content.", e4);
				}
				return result;
			}

			// Token: 0x0400407D RID: 16509
			private AlgorithmIdentifier algorithm;

			// Token: 0x0400407E RID: 16510
			private IBufferedCipher cipher;

			// Token: 0x0400407F RID: 16511
			private CmsReadable readable;
		}
	}
}
