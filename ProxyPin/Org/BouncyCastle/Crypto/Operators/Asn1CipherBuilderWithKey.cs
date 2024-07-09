using System;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Cms;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Utilities;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Operators
{
	// Token: 0x0200040D RID: 1037
	public class Asn1CipherBuilderWithKey : ICipherBuilderWithKey, ICipherBuilder
	{
		// Token: 0x06002157 RID: 8535 RVA: 0x000C1EB0 File Offset: 0x000C1EB0
		public Asn1CipherBuilderWithKey(DerObjectIdentifier encryptionOID, int keySize, SecureRandom random)
		{
			if (random == null)
			{
				random = new SecureRandom();
			}
			CipherKeyGenerator cipherKeyGenerator = CipherKeyGeneratorFactory.CreateKeyGenerator(encryptionOID, random);
			this.encKey = new KeyParameter(cipherKeyGenerator.GenerateKey());
			this.algorithmIdentifier = AlgorithmIdentifierFactory.GenerateEncryptionAlgID(encryptionOID, this.encKey.GetKey().Length * 8, random);
		}

		// Token: 0x17000645 RID: 1605
		// (get) Token: 0x06002158 RID: 8536 RVA: 0x000C1F0C File Offset: 0x000C1F0C
		public object AlgorithmDetails
		{
			get
			{
				return this.algorithmIdentifier;
			}
		}

		// Token: 0x06002159 RID: 8537 RVA: 0x000C1F14 File Offset: 0x000C1F14
		public int GetMaxOutputSize(int inputLen)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600215A RID: 8538 RVA: 0x000C1F1C File Offset: 0x000C1F1C
		public ICipher BuildCipher(Stream stream)
		{
			object obj = EnvelopedDataHelper.CreateContentCipher(true, this.encKey, this.algorithmIdentifier);
			if (obj is IStreamCipher)
			{
				obj = new BufferedStreamCipher((IStreamCipher)obj);
			}
			if (stream == null)
			{
				stream = new MemoryStream();
			}
			return new BufferedCipherWrapper((IBufferedCipher)obj, stream);
		}

		// Token: 0x17000646 RID: 1606
		// (get) Token: 0x0600215B RID: 8539 RVA: 0x000C1F70 File Offset: 0x000C1F70
		public ICipherParameters Key
		{
			get
			{
				return this.encKey;
			}
		}

		// Token: 0x040015B0 RID: 5552
		private readonly KeyParameter encKey;

		// Token: 0x040015B1 RID: 5553
		private AlgorithmIdentifier algorithmIdentifier;
	}
}
