using System;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Kisa;
using Org.BouncyCastle.Asn1.Nist;
using Org.BouncyCastle.Asn1.Ntt;
using Org.BouncyCastle.Asn1.Oiw;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Utilities
{
	// Token: 0x02000559 RID: 1369
	public class CipherKeyGeneratorFactory
	{
		// Token: 0x06002A6F RID: 10863 RVA: 0x000E3E04 File Offset: 0x000E3E04
		private CipherKeyGeneratorFactory()
		{
		}

		// Token: 0x06002A70 RID: 10864 RVA: 0x000E3E0C File Offset: 0x000E3E0C
		public static CipherKeyGenerator CreateKeyGenerator(DerObjectIdentifier algorithm, SecureRandom random)
		{
			if (NistObjectIdentifiers.IdAes128Cbc.Equals(algorithm))
			{
				return CipherKeyGeneratorFactory.CreateCipherKeyGenerator(random, 128);
			}
			if (NistObjectIdentifiers.IdAes192Cbc.Equals(algorithm))
			{
				return CipherKeyGeneratorFactory.CreateCipherKeyGenerator(random, 192);
			}
			if (NistObjectIdentifiers.IdAes256Cbc.Equals(algorithm))
			{
				return CipherKeyGeneratorFactory.CreateCipherKeyGenerator(random, 256);
			}
			if (PkcsObjectIdentifiers.DesEde3Cbc.Equals(algorithm))
			{
				DesEdeKeyGenerator desEdeKeyGenerator = new DesEdeKeyGenerator();
				desEdeKeyGenerator.Init(new KeyGenerationParameters(random, 192));
				return desEdeKeyGenerator;
			}
			if (NttObjectIdentifiers.IdCamellia128Cbc.Equals(algorithm))
			{
				return CipherKeyGeneratorFactory.CreateCipherKeyGenerator(random, 128);
			}
			if (NttObjectIdentifiers.IdCamellia192Cbc.Equals(algorithm))
			{
				return CipherKeyGeneratorFactory.CreateCipherKeyGenerator(random, 192);
			}
			if (NttObjectIdentifiers.IdCamellia256Cbc.Equals(algorithm))
			{
				return CipherKeyGeneratorFactory.CreateCipherKeyGenerator(random, 256);
			}
			if (KisaObjectIdentifiers.IdSeedCbc.Equals(algorithm))
			{
				return CipherKeyGeneratorFactory.CreateCipherKeyGenerator(random, 128);
			}
			if (AlgorithmIdentifierFactory.CAST5_CBC.Equals(algorithm))
			{
				return CipherKeyGeneratorFactory.CreateCipherKeyGenerator(random, 128);
			}
			if (OiwObjectIdentifiers.DesCbc.Equals(algorithm))
			{
				DesKeyGenerator desKeyGenerator = new DesKeyGenerator();
				desKeyGenerator.Init(new KeyGenerationParameters(random, 64));
				return desKeyGenerator;
			}
			if (PkcsObjectIdentifiers.rc4.Equals(algorithm))
			{
				return CipherKeyGeneratorFactory.CreateCipherKeyGenerator(random, 128);
			}
			if (PkcsObjectIdentifiers.RC2Cbc.Equals(algorithm))
			{
				return CipherKeyGeneratorFactory.CreateCipherKeyGenerator(random, 128);
			}
			throw new InvalidOperationException("cannot recognise cipher: " + algorithm);
		}

		// Token: 0x06002A71 RID: 10865 RVA: 0x000E3F94 File Offset: 0x000E3F94
		private static CipherKeyGenerator CreateCipherKeyGenerator(SecureRandom random, int keySize)
		{
			CipherKeyGenerator cipherKeyGenerator = new CipherKeyGenerator();
			cipherKeyGenerator.Init(new KeyGenerationParameters(random, keySize));
			return cipherKeyGenerator;
		}
	}
}
