using System;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.IO;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x020002E5 RID: 741
	public class CmsEnvelopedDataGenerator : CmsEnvelopedGenerator
	{
		// Token: 0x0600164F RID: 5711 RVA: 0x00074474 File Offset: 0x00074474
		public CmsEnvelopedDataGenerator()
		{
		}

		// Token: 0x06001650 RID: 5712 RVA: 0x0007447C File Offset: 0x0007447C
		public CmsEnvelopedDataGenerator(SecureRandom rand) : base(rand)
		{
		}

		// Token: 0x06001651 RID: 5713 RVA: 0x00074488 File Offset: 0x00074488
		private CmsEnvelopedData Generate(CmsProcessable content, string encryptionOid, CipherKeyGenerator keyGen)
		{
			AlgorithmIdentifier contentEncryptionAlgorithm = null;
			KeyParameter keyParameter;
			Asn1OctetString encryptedContent;
			try
			{
				byte[] array = keyGen.GenerateKey();
				keyParameter = ParameterUtilities.CreateKeyParameter(encryptionOid, array);
				Asn1Encodable asn1Params = this.GenerateAsn1Parameters(encryptionOid, array);
				ICipherParameters parameters;
				contentEncryptionAlgorithm = this.GetAlgorithmIdentifier(encryptionOid, keyParameter, asn1Params, out parameters);
				IBufferedCipher cipher = CipherUtilities.GetCipher(encryptionOid);
				cipher.Init(true, new ParametersWithRandom(parameters, this.rand));
				MemoryStream memoryStream = new MemoryStream();
				CipherStream cipherStream = new CipherStream(memoryStream, null, cipher);
				content.Write(cipherStream);
				Platform.Dispose(cipherStream);
				encryptedContent = new BerOctetString(memoryStream.ToArray());
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
				throw new CmsException("exception decoding algorithm parameters.", e3);
			}
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector();
			foreach (object obj in this.recipientInfoGenerators)
			{
				RecipientInfoGenerator recipientInfoGenerator = (RecipientInfoGenerator)obj;
				try
				{
					asn1EncodableVector.Add(recipientInfoGenerator.Generate(keyParameter, this.rand));
				}
				catch (InvalidKeyException e4)
				{
					throw new CmsException("key inappropriate for algorithm.", e4);
				}
				catch (GeneralSecurityException e5)
				{
					throw new CmsException("error making encrypted content.", e5);
				}
			}
			EncryptedContentInfo encryptedContentInfo = new EncryptedContentInfo(CmsObjectIdentifiers.Data, contentEncryptionAlgorithm, encryptedContent);
			Asn1Set unprotectedAttrs = null;
			if (this.unprotectedAttributeGenerator != null)
			{
				Org.BouncyCastle.Asn1.Cms.AttributeTable attributes = this.unprotectedAttributeGenerator.GetAttributes(Platform.CreateHashtable());
				unprotectedAttrs = new BerSet(attributes.ToAsn1EncodableVector());
			}
			ContentInfo contentInfo = new ContentInfo(CmsObjectIdentifiers.EnvelopedData, new EnvelopedData(null, new DerSet(asn1EncodableVector), encryptedContentInfo, unprotectedAttrs));
			return new CmsEnvelopedData(contentInfo);
		}

		// Token: 0x06001652 RID: 5714 RVA: 0x0007466C File Offset: 0x0007466C
		public CmsEnvelopedData Generate(CmsProcessable content, string encryptionOid)
		{
			CmsEnvelopedData result;
			try
			{
				CipherKeyGenerator keyGenerator = GeneratorUtilities.GetKeyGenerator(encryptionOid);
				keyGenerator.Init(new KeyGenerationParameters(this.rand, keyGenerator.DefaultStrength));
				result = this.Generate(content, encryptionOid, keyGenerator);
			}
			catch (SecurityUtilityException e)
			{
				throw new CmsException("can't find key generation algorithm.", e);
			}
			return result;
		}

		// Token: 0x06001653 RID: 5715 RVA: 0x000746C4 File Offset: 0x000746C4
		public CmsEnvelopedData Generate(CmsProcessable content, ICipherBuilderWithKey cipherBuilder)
		{
			KeyParameter contentEncryptionKey;
			Asn1OctetString encryptedContent;
			try
			{
				contentEncryptionKey = (KeyParameter)cipherBuilder.Key;
				MemoryStream memoryStream = new MemoryStream();
				Stream stream = cipherBuilder.BuildCipher(memoryStream).Stream;
				content.Write(stream);
				Platform.Dispose(stream);
				encryptedContent = new BerOctetString(memoryStream.ToArray());
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
				throw new CmsException("exception decoding algorithm parameters.", e3);
			}
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector();
			foreach (object obj in this.recipientInfoGenerators)
			{
				RecipientInfoGenerator recipientInfoGenerator = (RecipientInfoGenerator)obj;
				try
				{
					asn1EncodableVector.Add(recipientInfoGenerator.Generate(contentEncryptionKey, this.rand));
				}
				catch (InvalidKeyException e4)
				{
					throw new CmsException("key inappropriate for algorithm.", e4);
				}
				catch (GeneralSecurityException e5)
				{
					throw new CmsException("error making encrypted content.", e5);
				}
			}
			EncryptedContentInfo encryptedContentInfo = new EncryptedContentInfo(CmsObjectIdentifiers.Data, (AlgorithmIdentifier)cipherBuilder.AlgorithmDetails, encryptedContent);
			Asn1Set unprotectedAttrs = null;
			if (this.unprotectedAttributeGenerator != null)
			{
				Org.BouncyCastle.Asn1.Cms.AttributeTable attributes = this.unprotectedAttributeGenerator.GetAttributes(Platform.CreateHashtable());
				unprotectedAttrs = new BerSet(attributes.ToAsn1EncodableVector());
			}
			ContentInfo contentInfo = new ContentInfo(CmsObjectIdentifiers.EnvelopedData, new EnvelopedData(null, new DerSet(asn1EncodableVector), encryptedContentInfo, unprotectedAttrs));
			return new CmsEnvelopedData(contentInfo);
		}

		// Token: 0x06001654 RID: 5716 RVA: 0x00074878 File Offset: 0x00074878
		public CmsEnvelopedData Generate(CmsProcessable content, string encryptionOid, int keySize)
		{
			CmsEnvelopedData result;
			try
			{
				CipherKeyGenerator keyGenerator = GeneratorUtilities.GetKeyGenerator(encryptionOid);
				keyGenerator.Init(new KeyGenerationParameters(this.rand, keySize));
				result = this.Generate(content, encryptionOid, keyGenerator);
			}
			catch (SecurityUtilityException e)
			{
				throw new CmsException("can't find key generation algorithm.", e);
			}
			return result;
		}
	}
}
