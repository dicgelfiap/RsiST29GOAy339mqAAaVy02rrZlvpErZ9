using System;
using System.Collections;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Nist;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Utilities;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x020002FF RID: 767
	internal class EnvelopedDataHelper
	{
		// Token: 0x06001738 RID: 5944 RVA: 0x00079814 File Offset: 0x00079814
		static EnvelopedDataHelper()
		{
			EnvelopedDataHelper.BaseCipherNames.Add(PkcsObjectIdentifiers.DesEde3Cbc, "DESEDE");
			EnvelopedDataHelper.BaseCipherNames.Add(NistObjectIdentifiers.IdAes128Cbc, "AES");
			EnvelopedDataHelper.BaseCipherNames.Add(NistObjectIdentifiers.IdAes192Cbc, "AES");
			EnvelopedDataHelper.BaseCipherNames.Add(NistObjectIdentifiers.IdAes256Cbc, "AES");
			EnvelopedDataHelper.MacAlgNames.Add(PkcsObjectIdentifiers.DesEde3Cbc, "DESEDEMac");
			EnvelopedDataHelper.MacAlgNames.Add(NistObjectIdentifiers.IdAes128Cbc, "AESMac");
			EnvelopedDataHelper.MacAlgNames.Add(NistObjectIdentifiers.IdAes192Cbc, "AESMac");
			EnvelopedDataHelper.MacAlgNames.Add(NistObjectIdentifiers.IdAes256Cbc, "AESMac");
			EnvelopedDataHelper.MacAlgNames.Add(PkcsObjectIdentifiers.RC2Cbc, "RC2Mac");
		}

		// Token: 0x06001739 RID: 5945 RVA: 0x000798F0 File Offset: 0x000798F0
		public static object CreateContentCipher(bool forEncryption, ICipherParameters encKey, AlgorithmIdentifier encryptionAlgID)
		{
			return CipherFactory.CreateContentCipher(forEncryption, encKey, encryptionAlgID);
		}

		// Token: 0x0600173A RID: 5946 RVA: 0x000798FC File Offset: 0x000798FC
		public AlgorithmIdentifier GenerateEncryptionAlgID(DerObjectIdentifier encryptionOID, KeyParameter encKey, SecureRandom random)
		{
			return AlgorithmIdentifierFactory.GenerateEncryptionAlgID(encryptionOID, encKey.GetKey().Length * 8, random);
		}

		// Token: 0x0600173B RID: 5947 RVA: 0x00079910 File Offset: 0x00079910
		public CipherKeyGenerator CreateKeyGenerator(DerObjectIdentifier algorithm, SecureRandom random)
		{
			return CipherKeyGeneratorFactory.CreateKeyGenerator(algorithm, random);
		}

		// Token: 0x04000FA2 RID: 4002
		private static readonly IDictionary BaseCipherNames = Platform.CreateHashtable();

		// Token: 0x04000FA3 RID: 4003
		private static readonly IDictionary MacAlgNames = Platform.CreateHashtable();
	}
}
