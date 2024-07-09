using System;
using System.IO;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.IO;

namespace Org.BouncyCastle.Pkcs
{
	// Token: 0x02000684 RID: 1668
	public class Pkcs8EncryptedPrivateKeyInfo
	{
		// Token: 0x06003A45 RID: 14917 RVA: 0x0013A104 File Offset: 0x0013A104
		private static EncryptedPrivateKeyInfo parseBytes(byte[] pkcs8Encoding)
		{
			EncryptedPrivateKeyInfo instance;
			try
			{
				instance = EncryptedPrivateKeyInfo.GetInstance(pkcs8Encoding);
			}
			catch (ArgumentException ex)
			{
				throw new PkcsIOException("malformed data: " + ex.Message, ex);
			}
			catch (Exception ex2)
			{
				throw new PkcsIOException("malformed data: " + ex2.Message, ex2);
			}
			return instance;
		}

		// Token: 0x06003A46 RID: 14918 RVA: 0x0013A16C File Offset: 0x0013A16C
		public Pkcs8EncryptedPrivateKeyInfo(EncryptedPrivateKeyInfo encryptedPrivateKeyInfo)
		{
			this.encryptedPrivateKeyInfo = encryptedPrivateKeyInfo;
		}

		// Token: 0x06003A47 RID: 14919 RVA: 0x0013A17C File Offset: 0x0013A17C
		public Pkcs8EncryptedPrivateKeyInfo(byte[] encryptedPrivateKeyInfo) : this(Pkcs8EncryptedPrivateKeyInfo.parseBytes(encryptedPrivateKeyInfo))
		{
		}

		// Token: 0x06003A48 RID: 14920 RVA: 0x0013A18C File Offset: 0x0013A18C
		public EncryptedPrivateKeyInfo ToAsn1Structure()
		{
			return this.encryptedPrivateKeyInfo;
		}

		// Token: 0x06003A49 RID: 14921 RVA: 0x0013A194 File Offset: 0x0013A194
		public byte[] GetEncryptedData()
		{
			return this.encryptedPrivateKeyInfo.GetEncryptedData();
		}

		// Token: 0x06003A4A RID: 14922 RVA: 0x0013A1A4 File Offset: 0x0013A1A4
		public byte[] GetEncoded()
		{
			return this.encryptedPrivateKeyInfo.GetEncoded();
		}

		// Token: 0x06003A4B RID: 14923 RVA: 0x0013A1B4 File Offset: 0x0013A1B4
		public PrivateKeyInfo DecryptPrivateKeyInfo(IDecryptorBuilderProvider inputDecryptorProvider)
		{
			PrivateKeyInfo instance;
			try
			{
				ICipherBuilder cipherBuilder = inputDecryptorProvider.CreateDecryptorBuilder(this.encryptedPrivateKeyInfo.EncryptionAlgorithm);
				ICipher cipher = cipherBuilder.BuildCipher(new MemoryInputStream(this.encryptedPrivateKeyInfo.GetEncryptedData()));
				Stream stream = cipher.Stream;
				byte[] obj = Streams.ReadAll(cipher.Stream);
				Platform.Dispose(stream);
				instance = PrivateKeyInfo.GetInstance(obj);
			}
			catch (Exception ex)
			{
				throw new PkcsException("unable to read encrypted data: " + ex.Message, ex);
			}
			return instance;
		}

		// Token: 0x04001E45 RID: 7749
		private EncryptedPrivateKeyInfo encryptedPrivateKeyInfo;
	}
}
