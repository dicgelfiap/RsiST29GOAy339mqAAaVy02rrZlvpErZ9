using System;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x0200030D RID: 781
	internal class PasswordRecipientInfoGenerator : RecipientInfoGenerator
	{
		// Token: 0x060017A5 RID: 6053 RVA: 0x0007B2F8 File Offset: 0x0007B2F8
		internal PasswordRecipientInfoGenerator()
		{
		}

		// Token: 0x1700053F RID: 1343
		// (set) Token: 0x060017A6 RID: 6054 RVA: 0x0007B300 File Offset: 0x0007B300
		internal AlgorithmIdentifier KeyDerivationAlgorithm
		{
			set
			{
				this.keyDerivationAlgorithm = value;
			}
		}

		// Token: 0x17000540 RID: 1344
		// (set) Token: 0x060017A7 RID: 6055 RVA: 0x0007B30C File Offset: 0x0007B30C
		internal KeyParameter KeyEncryptionKey
		{
			set
			{
				this.keyEncryptionKey = value;
			}
		}

		// Token: 0x17000541 RID: 1345
		// (set) Token: 0x060017A8 RID: 6056 RVA: 0x0007B318 File Offset: 0x0007B318
		internal string KeyEncryptionKeyOID
		{
			set
			{
				this.keyEncryptionKeyOID = value;
			}
		}

		// Token: 0x060017A9 RID: 6057 RVA: 0x0007B324 File Offset: 0x0007B324
		public RecipientInfo Generate(KeyParameter contentEncryptionKey, SecureRandom random)
		{
			byte[] key = contentEncryptionKey.GetKey();
			string rfc3211WrapperName = PasswordRecipientInfoGenerator.Helper.GetRfc3211WrapperName(this.keyEncryptionKeyOID);
			IWrapper wrapper = PasswordRecipientInfoGenerator.Helper.CreateWrapper(rfc3211WrapperName);
			int num = Platform.StartsWith(rfc3211WrapperName, "DESEDE") ? 8 : 16;
			byte[] array = new byte[num];
			random.NextBytes(array);
			ICipherParameters parameters = new ParametersWithIV(this.keyEncryptionKey, array);
			wrapper.Init(true, new ParametersWithRandom(parameters, random));
			Asn1OctetString encryptedKey = new DerOctetString(wrapper.Wrap(key, 0, key.Length));
			DerSequence parameters2 = new DerSequence(new Asn1Encodable[]
			{
				new DerObjectIdentifier(this.keyEncryptionKeyOID),
				new DerOctetString(array)
			});
			AlgorithmIdentifier keyEncryptionAlgorithm = new AlgorithmIdentifier(PkcsObjectIdentifiers.IdAlgPwriKek, parameters2);
			return new RecipientInfo(new PasswordRecipientInfo(this.keyDerivationAlgorithm, keyEncryptionAlgorithm, encryptedKey));
		}

		// Token: 0x04000FCF RID: 4047
		private static readonly CmsEnvelopedHelper Helper = CmsEnvelopedHelper.Instance;

		// Token: 0x04000FD0 RID: 4048
		private AlgorithmIdentifier keyDerivationAlgorithm;

		// Token: 0x04000FD1 RID: 4049
		private KeyParameter keyEncryptionKey;

		// Token: 0x04000FD2 RID: 4050
		private string keyEncryptionKeyOID;
	}
}
