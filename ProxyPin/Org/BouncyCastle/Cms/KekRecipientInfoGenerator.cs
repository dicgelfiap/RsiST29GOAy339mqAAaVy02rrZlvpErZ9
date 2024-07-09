using System;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Asn1.Kisa;
using Org.BouncyCastle.Asn1.Nist;
using Org.BouncyCastle.Asn1.Ntt;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x02000301 RID: 769
	internal class KekRecipientInfoGenerator : RecipientInfoGenerator
	{
		// Token: 0x0600173E RID: 5950 RVA: 0x00079924 File Offset: 0x00079924
		internal KekRecipientInfoGenerator()
		{
		}

		// Token: 0x1700051F RID: 1311
		// (set) Token: 0x0600173F RID: 5951 RVA: 0x0007992C File Offset: 0x0007992C
		internal KekIdentifier KekIdentifier
		{
			set
			{
				this.kekIdentifier = value;
			}
		}

		// Token: 0x17000520 RID: 1312
		// (set) Token: 0x06001740 RID: 5952 RVA: 0x00079938 File Offset: 0x00079938
		internal KeyParameter KeyEncryptionKey
		{
			set
			{
				this.keyEncryptionKey = value;
				this.keyEncryptionAlgorithm = KekRecipientInfoGenerator.DetermineKeyEncAlg(this.keyEncryptionKeyOID, this.keyEncryptionKey);
			}
		}

		// Token: 0x17000521 RID: 1313
		// (set) Token: 0x06001741 RID: 5953 RVA: 0x00079958 File Offset: 0x00079958
		internal string KeyEncryptionKeyOID
		{
			set
			{
				this.keyEncryptionKeyOID = value;
			}
		}

		// Token: 0x06001742 RID: 5954 RVA: 0x00079964 File Offset: 0x00079964
		public RecipientInfo Generate(KeyParameter contentEncryptionKey, SecureRandom random)
		{
			byte[] key = contentEncryptionKey.GetKey();
			IWrapper wrapper = KekRecipientInfoGenerator.Helper.CreateWrapper(this.keyEncryptionAlgorithm.Algorithm.Id);
			wrapper.Init(true, new ParametersWithRandom(this.keyEncryptionKey, random));
			Asn1OctetString encryptedKey = new DerOctetString(wrapper.Wrap(key, 0, key.Length));
			return new RecipientInfo(new KekRecipientInfo(this.kekIdentifier, this.keyEncryptionAlgorithm, encryptedKey));
		}

		// Token: 0x06001743 RID: 5955 RVA: 0x000799D4 File Offset: 0x000799D4
		private static AlgorithmIdentifier DetermineKeyEncAlg(string algorithm, KeyParameter key)
		{
			if (Platform.StartsWith(algorithm, "DES"))
			{
				return new AlgorithmIdentifier(PkcsObjectIdentifiers.IdAlgCms3DesWrap, DerNull.Instance);
			}
			if (Platform.StartsWith(algorithm, "RC2"))
			{
				return new AlgorithmIdentifier(PkcsObjectIdentifiers.IdAlgCmsRC2Wrap, new DerInteger(58));
			}
			if (Platform.StartsWith(algorithm, "AES"))
			{
				int num = key.GetKey().Length * 8;
				DerObjectIdentifier algorithm2;
				if (num == 128)
				{
					algorithm2 = NistObjectIdentifiers.IdAes128Wrap;
				}
				else if (num == 192)
				{
					algorithm2 = NistObjectIdentifiers.IdAes192Wrap;
				}
				else
				{
					if (num != 256)
					{
						throw new ArgumentException("illegal keysize in AES");
					}
					algorithm2 = NistObjectIdentifiers.IdAes256Wrap;
				}
				return new AlgorithmIdentifier(algorithm2);
			}
			if (Platform.StartsWith(algorithm, "SEED"))
			{
				return new AlgorithmIdentifier(KisaObjectIdentifiers.IdNpkiAppCmsSeedWrap);
			}
			if (Platform.StartsWith(algorithm, "CAMELLIA"))
			{
				int num2 = key.GetKey().Length * 8;
				DerObjectIdentifier algorithm3;
				if (num2 == 128)
				{
					algorithm3 = NttObjectIdentifiers.IdCamellia128Wrap;
				}
				else if (num2 == 192)
				{
					algorithm3 = NttObjectIdentifiers.IdCamellia192Wrap;
				}
				else
				{
					if (num2 != 256)
					{
						throw new ArgumentException("illegal keysize in Camellia");
					}
					algorithm3 = NttObjectIdentifiers.IdCamellia256Wrap;
				}
				return new AlgorithmIdentifier(algorithm3);
			}
			throw new ArgumentException("unknown algorithm");
		}

		// Token: 0x04000FA4 RID: 4004
		private static readonly CmsEnvelopedHelper Helper = CmsEnvelopedHelper.Instance;

		// Token: 0x04000FA5 RID: 4005
		private KeyParameter keyEncryptionKey;

		// Token: 0x04000FA6 RID: 4006
		private string keyEncryptionKeyOID;

		// Token: 0x04000FA7 RID: 4007
		private KekIdentifier kekIdentifier;

		// Token: 0x04000FA8 RID: 4008
		private AlgorithmIdentifier keyEncryptionAlgorithm;
	}
}
