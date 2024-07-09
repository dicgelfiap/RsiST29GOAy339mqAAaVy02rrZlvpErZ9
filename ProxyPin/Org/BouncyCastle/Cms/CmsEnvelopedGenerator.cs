using System;
using System.Collections;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Asn1.Kisa;
using Org.BouncyCastle.Asn1.Nist;
using Org.BouncyCastle.Asn1.Ntt;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Asn1.X9;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.X509;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x020002D7 RID: 727
	public class CmsEnvelopedGenerator
	{
		// Token: 0x06001606 RID: 5638 RVA: 0x000732C0 File Offset: 0x000732C0
		public CmsEnvelopedGenerator() : this(new SecureRandom())
		{
		}

		// Token: 0x06001607 RID: 5639 RVA: 0x000732D0 File Offset: 0x000732D0
		public CmsEnvelopedGenerator(SecureRandom rand)
		{
			this.rand = rand;
		}

		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x06001608 RID: 5640 RVA: 0x000732F4 File Offset: 0x000732F4
		// (set) Token: 0x06001609 RID: 5641 RVA: 0x000732FC File Offset: 0x000732FC
		public CmsAttributeTableGenerator UnprotectedAttributeGenerator
		{
			get
			{
				return this.unprotectedAttributeGenerator;
			}
			set
			{
				this.unprotectedAttributeGenerator = value;
			}
		}

		// Token: 0x0600160A RID: 5642 RVA: 0x00073308 File Offset: 0x00073308
		public void AddKeyTransRecipient(X509Certificate cert)
		{
			KeyTransRecipientInfoGenerator keyTransRecipientInfoGenerator = new KeyTransRecipientInfoGenerator();
			keyTransRecipientInfoGenerator.RecipientCert = cert;
			this.recipientInfoGenerators.Add(keyTransRecipientInfoGenerator);
		}

		// Token: 0x0600160B RID: 5643 RVA: 0x00073334 File Offset: 0x00073334
		public void AddKeyTransRecipient(AsymmetricKeyParameter pubKey, byte[] subKeyId)
		{
			KeyTransRecipientInfoGenerator keyTransRecipientInfoGenerator = new KeyTransRecipientInfoGenerator();
			keyTransRecipientInfoGenerator.RecipientPublicKey = pubKey;
			keyTransRecipientInfoGenerator.SubjectKeyIdentifier = new DerOctetString(subKeyId);
			this.recipientInfoGenerators.Add(keyTransRecipientInfoGenerator);
		}

		// Token: 0x0600160C RID: 5644 RVA: 0x0007336C File Offset: 0x0007336C
		public void AddKekRecipient(string keyAlgorithm, KeyParameter key, byte[] keyIdentifier)
		{
			this.AddKekRecipient(keyAlgorithm, key, new KekIdentifier(keyIdentifier, null, null));
		}

		// Token: 0x0600160D RID: 5645 RVA: 0x00073380 File Offset: 0x00073380
		public void AddKekRecipient(string keyAlgorithm, KeyParameter key, KekIdentifier kekIdentifier)
		{
			KekRecipientInfoGenerator kekRecipientInfoGenerator = new KekRecipientInfoGenerator();
			kekRecipientInfoGenerator.KekIdentifier = kekIdentifier;
			kekRecipientInfoGenerator.KeyEncryptionKeyOID = keyAlgorithm;
			kekRecipientInfoGenerator.KeyEncryptionKey = key;
			this.recipientInfoGenerators.Add(kekRecipientInfoGenerator);
		}

		// Token: 0x0600160E RID: 5646 RVA: 0x000733BC File Offset: 0x000733BC
		public void AddPasswordRecipient(CmsPbeKey pbeKey, string kekAlgorithmOid)
		{
			Pbkdf2Params parameters = new Pbkdf2Params(pbeKey.Salt, pbeKey.IterationCount);
			PasswordRecipientInfoGenerator passwordRecipientInfoGenerator = new PasswordRecipientInfoGenerator();
			passwordRecipientInfoGenerator.KeyDerivationAlgorithm = new AlgorithmIdentifier(PkcsObjectIdentifiers.IdPbkdf2, parameters);
			passwordRecipientInfoGenerator.KeyEncryptionKeyOID = kekAlgorithmOid;
			passwordRecipientInfoGenerator.KeyEncryptionKey = pbeKey.GetEncoded(kekAlgorithmOid);
			this.recipientInfoGenerators.Add(passwordRecipientInfoGenerator);
		}

		// Token: 0x0600160F RID: 5647 RVA: 0x00073418 File Offset: 0x00073418
		public void AddKeyAgreementRecipient(string agreementAlgorithm, AsymmetricKeyParameter senderPrivateKey, AsymmetricKeyParameter senderPublicKey, X509Certificate recipientCert, string cekWrapAlgorithm)
		{
			IList list = Platform.CreateArrayList(1);
			list.Add(recipientCert);
			this.AddKeyAgreementRecipients(agreementAlgorithm, senderPrivateKey, senderPublicKey, list, cekWrapAlgorithm);
		}

		// Token: 0x06001610 RID: 5648 RVA: 0x00073448 File Offset: 0x00073448
		public void AddKeyAgreementRecipients(string agreementAlgorithm, AsymmetricKeyParameter senderPrivateKey, AsymmetricKeyParameter senderPublicKey, ICollection recipientCerts, string cekWrapAlgorithm)
		{
			if (!senderPrivateKey.IsPrivate)
			{
				throw new ArgumentException("Expected private key", "senderPrivateKey");
			}
			if (senderPublicKey.IsPrivate)
			{
				throw new ArgumentException("Expected public key", "senderPublicKey");
			}
			KeyAgreeRecipientInfoGenerator keyAgreeRecipientInfoGenerator = new KeyAgreeRecipientInfoGenerator();
			keyAgreeRecipientInfoGenerator.KeyAgreementOID = new DerObjectIdentifier(agreementAlgorithm);
			keyAgreeRecipientInfoGenerator.KeyEncryptionOID = new DerObjectIdentifier(cekWrapAlgorithm);
			keyAgreeRecipientInfoGenerator.RecipientCerts = recipientCerts;
			keyAgreeRecipientInfoGenerator.SenderKeyPair = new AsymmetricCipherKeyPair(senderPublicKey, senderPrivateKey);
			this.recipientInfoGenerators.Add(keyAgreeRecipientInfoGenerator);
		}

		// Token: 0x06001611 RID: 5649 RVA: 0x000734D0 File Offset: 0x000734D0
		public void AddRecipientInfoGenerator(RecipientInfoGenerator recipientInfoGenerator)
		{
			this.recipientInfoGenerators.Add(recipientInfoGenerator);
		}

		// Token: 0x06001612 RID: 5650 RVA: 0x000734E0 File Offset: 0x000734E0
		protected internal virtual AlgorithmIdentifier GetAlgorithmIdentifier(string encryptionOid, KeyParameter encKey, Asn1Encodable asn1Params, out ICipherParameters cipherParameters)
		{
			Asn1Object asn1Object;
			if (asn1Params != null)
			{
				asn1Object = asn1Params.ToAsn1Object();
				cipherParameters = ParameterUtilities.GetCipherParameters(encryptionOid, encKey, asn1Object);
			}
			else
			{
				asn1Object = DerNull.Instance;
				cipherParameters = encKey;
			}
			return new AlgorithmIdentifier(new DerObjectIdentifier(encryptionOid), asn1Object);
		}

		// Token: 0x06001613 RID: 5651 RVA: 0x00073524 File Offset: 0x00073524
		protected internal virtual Asn1Encodable GenerateAsn1Parameters(string encryptionOid, byte[] encKeyBytes)
		{
			Asn1Encodable result = null;
			try
			{
				if (encryptionOid.Equals(CmsEnvelopedGenerator.RC2Cbc))
				{
					byte[] array = new byte[8];
					this.rand.NextBytes(array);
					int num = encKeyBytes.Length * 8;
					int parameterVersion;
					if (num < 256)
					{
						parameterVersion = (int)CmsEnvelopedGenerator.rc2Table[num];
					}
					else
					{
						parameterVersion = num;
					}
					result = new RC2CbcParameter(parameterVersion, array);
				}
				else
				{
					result = ParameterUtilities.GenerateParameters(encryptionOid, this.rand);
				}
			}
			catch (SecurityUtilityException)
			{
			}
			return result;
		}

		// Token: 0x04000EF9 RID: 3833
		public const string IdeaCbc = "1.3.6.1.4.1.188.7.1.1.2";

		// Token: 0x04000EFA RID: 3834
		public const string Cast5Cbc = "1.2.840.113533.7.66.10";

		// Token: 0x04000EFB RID: 3835
		internal static readonly short[] rc2Table = new short[]
		{
			189,
			86,
			234,
			242,
			162,
			241,
			172,
			42,
			176,
			147,
			209,
			156,
			27,
			51,
			253,
			208,
			48,
			4,
			182,
			220,
			125,
			223,
			50,
			75,
			247,
			203,
			69,
			155,
			49,
			187,
			33,
			90,
			65,
			159,
			225,
			217,
			74,
			77,
			158,
			218,
			160,
			104,
			44,
			195,
			39,
			95,
			128,
			54,
			62,
			238,
			251,
			149,
			26,
			254,
			206,
			168,
			52,
			169,
			19,
			240,
			166,
			63,
			216,
			12,
			120,
			36,
			175,
			35,
			82,
			193,
			103,
			23,
			245,
			102,
			144,
			231,
			232,
			7,
			184,
			96,
			72,
			230,
			30,
			83,
			243,
			146,
			164,
			114,
			140,
			8,
			21,
			110,
			134,
			0,
			132,
			250,
			244,
			127,
			138,
			66,
			25,
			246,
			219,
			205,
			20,
			141,
			80,
			18,
			186,
			60,
			6,
			78,
			236,
			179,
			53,
			17,
			161,
			136,
			142,
			43,
			148,
			153,
			183,
			113,
			116,
			211,
			228,
			191,
			58,
			222,
			150,
			14,
			188,
			10,
			237,
			119,
			252,
			55,
			107,
			3,
			121,
			137,
			98,
			198,
			215,
			192,
			210,
			124,
			106,
			139,
			34,
			163,
			91,
			5,
			93,
			2,
			117,
			213,
			97,
			227,
			24,
			143,
			85,
			81,
			173,
			31,
			11,
			94,
			133,
			229,
			194,
			87,
			99,
			202,
			61,
			108,
			180,
			197,
			204,
			112,
			178,
			145,
			89,
			13,
			71,
			32,
			200,
			79,
			88,
			224,
			1,
			226,
			22,
			56,
			196,
			111,
			59,
			15,
			101,
			70,
			190,
			126,
			45,
			123,
			130,
			249,
			64,
			181,
			29,
			115,
			248,
			235,
			38,
			199,
			135,
			151,
			37,
			84,
			177,
			40,
			170,
			152,
			157,
			165,
			100,
			109,
			122,
			212,
			16,
			129,
			68,
			239,
			73,
			214,
			174,
			46,
			221,
			118,
			92,
			47,
			167,
			28,
			201,
			9,
			105,
			154,
			131,
			207,
			41,
			57,
			185,
			233,
			76,
			255,
			67,
			171
		};

		// Token: 0x04000EFC RID: 3836
		public static readonly string DesEde3Cbc = PkcsObjectIdentifiers.DesEde3Cbc.Id;

		// Token: 0x04000EFD RID: 3837
		public static readonly string RC2Cbc = PkcsObjectIdentifiers.RC2Cbc.Id;

		// Token: 0x04000EFE RID: 3838
		public static readonly string Aes128Cbc = NistObjectIdentifiers.IdAes128Cbc.Id;

		// Token: 0x04000EFF RID: 3839
		public static readonly string Aes192Cbc = NistObjectIdentifiers.IdAes192Cbc.Id;

		// Token: 0x04000F00 RID: 3840
		public static readonly string Aes256Cbc = NistObjectIdentifiers.IdAes256Cbc.Id;

		// Token: 0x04000F01 RID: 3841
		public static readonly string Camellia128Cbc = NttObjectIdentifiers.IdCamellia128Cbc.Id;

		// Token: 0x04000F02 RID: 3842
		public static readonly string Camellia192Cbc = NttObjectIdentifiers.IdCamellia192Cbc.Id;

		// Token: 0x04000F03 RID: 3843
		public static readonly string Camellia256Cbc = NttObjectIdentifiers.IdCamellia256Cbc.Id;

		// Token: 0x04000F04 RID: 3844
		public static readonly string SeedCbc = KisaObjectIdentifiers.IdSeedCbc.Id;

		// Token: 0x04000F05 RID: 3845
		public static readonly string DesEde3Wrap = PkcsObjectIdentifiers.IdAlgCms3DesWrap.Id;

		// Token: 0x04000F06 RID: 3846
		public static readonly string Aes128Wrap = NistObjectIdentifiers.IdAes128Wrap.Id;

		// Token: 0x04000F07 RID: 3847
		public static readonly string Aes192Wrap = NistObjectIdentifiers.IdAes192Wrap.Id;

		// Token: 0x04000F08 RID: 3848
		public static readonly string Aes256Wrap = NistObjectIdentifiers.IdAes256Wrap.Id;

		// Token: 0x04000F09 RID: 3849
		public static readonly string Camellia128Wrap = NttObjectIdentifiers.IdCamellia128Wrap.Id;

		// Token: 0x04000F0A RID: 3850
		public static readonly string Camellia192Wrap = NttObjectIdentifiers.IdCamellia192Wrap.Id;

		// Token: 0x04000F0B RID: 3851
		public static readonly string Camellia256Wrap = NttObjectIdentifiers.IdCamellia256Wrap.Id;

		// Token: 0x04000F0C RID: 3852
		public static readonly string SeedWrap = KisaObjectIdentifiers.IdNpkiAppCmsSeedWrap.Id;

		// Token: 0x04000F0D RID: 3853
		public static readonly string ECDHSha1Kdf = X9ObjectIdentifiers.DHSinglePassStdDHSha1KdfScheme.Id;

		// Token: 0x04000F0E RID: 3854
		public static readonly string ECMqvSha1Kdf = X9ObjectIdentifiers.MqvSinglePassSha1KdfScheme.Id;

		// Token: 0x04000F0F RID: 3855
		internal readonly IList recipientInfoGenerators = Platform.CreateArrayList();

		// Token: 0x04000F10 RID: 3856
		internal readonly SecureRandom rand;

		// Token: 0x04000F11 RID: 3857
		internal CmsAttributeTableGenerator unprotectedAttributeGenerator = null;
	}
}
