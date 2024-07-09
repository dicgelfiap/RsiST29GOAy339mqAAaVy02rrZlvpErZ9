using System;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Nist;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Math.EC;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Encoders;

namespace Org.BouncyCastle.Bcpg.OpenPgp
{
	// Token: 0x0200066D RID: 1645
	public sealed class Rfc6637Utilities
	{
		// Token: 0x060039B2 RID: 14770 RVA: 0x00135B14 File Offset: 0x00135B14
		private Rfc6637Utilities()
		{
		}

		// Token: 0x060039B3 RID: 14771 RVA: 0x00135B1C File Offset: 0x00135B1C
		public static string GetAgreementAlgorithm(PublicKeyPacket pubKeyData)
		{
			ECDHPublicBcpgKey ecdhpublicBcpgKey = (ECDHPublicBcpgKey)pubKeyData.Key;
			switch (ecdhpublicBcpgKey.HashAlgorithm)
			{
			case HashAlgorithmTag.Sha256:
				return "ECCDHwithSHA256CKDF";
			case HashAlgorithmTag.Sha384:
				return "ECCDHwithSHA384CKDF";
			case HashAlgorithmTag.Sha512:
				return "ECCDHwithSHA512CKDF";
			default:
				throw new ArgumentException("Unknown hash algorithm specified: " + ecdhpublicBcpgKey.HashAlgorithm);
			}
		}

		// Token: 0x060039B4 RID: 14772 RVA: 0x00135B88 File Offset: 0x00135B88
		public static DerObjectIdentifier GetKeyEncryptionOID(SymmetricKeyAlgorithmTag algID)
		{
			switch (algID)
			{
			case SymmetricKeyAlgorithmTag.Aes128:
				return NistObjectIdentifiers.IdAes128Wrap;
			case SymmetricKeyAlgorithmTag.Aes192:
				return NistObjectIdentifiers.IdAes192Wrap;
			case SymmetricKeyAlgorithmTag.Aes256:
				return NistObjectIdentifiers.IdAes256Wrap;
			default:
				throw new PgpException("unknown symmetric algorithm ID: " + algID);
			}
		}

		// Token: 0x060039B5 RID: 14773 RVA: 0x00135BDC File Offset: 0x00135BDC
		public static int GetKeyLength(SymmetricKeyAlgorithmTag algID)
		{
			switch (algID)
			{
			case SymmetricKeyAlgorithmTag.Aes128:
				return 16;
			case SymmetricKeyAlgorithmTag.Aes192:
				return 24;
			case SymmetricKeyAlgorithmTag.Aes256:
				return 32;
			default:
				throw new PgpException("unknown symmetric algorithm ID: " + algID);
			}
		}

		// Token: 0x060039B6 RID: 14774 RVA: 0x00135C28 File Offset: 0x00135C28
		public static byte[] CreateKey(PublicKeyPacket pubKeyData, ECPoint s)
		{
			byte[] parameters = Rfc6637Utilities.CreateUserKeyingMaterial(pubKeyData);
			ECDHPublicBcpgKey ecdhpublicBcpgKey = (ECDHPublicBcpgKey)pubKeyData.Key;
			return Rfc6637Utilities.Kdf(ecdhpublicBcpgKey.HashAlgorithm, s, Rfc6637Utilities.GetKeyLength(ecdhpublicBcpgKey.SymmetricKeyAlgorithm), parameters);
		}

		// Token: 0x060039B7 RID: 14775 RVA: 0x00135C64 File Offset: 0x00135C64
		public static byte[] CreateUserKeyingMaterial(PublicKeyPacket pubKeyData)
		{
			MemoryStream memoryStream = new MemoryStream();
			ECDHPublicBcpgKey ecdhpublicBcpgKey = (ECDHPublicBcpgKey)pubKeyData.Key;
			byte[] encoded = ecdhpublicBcpgKey.CurveOid.GetEncoded();
			memoryStream.Write(encoded, 1, encoded.Length - 1);
			memoryStream.WriteByte((byte)pubKeyData.Algorithm);
			memoryStream.WriteByte(3);
			memoryStream.WriteByte(1);
			memoryStream.WriteByte((byte)ecdhpublicBcpgKey.HashAlgorithm);
			memoryStream.WriteByte((byte)ecdhpublicBcpgKey.SymmetricKeyAlgorithm);
			memoryStream.Write(Rfc6637Utilities.ANONYMOUS_SENDER, 0, Rfc6637Utilities.ANONYMOUS_SENDER.Length);
			byte[] array = PgpPublicKey.CalculateFingerprint(pubKeyData);
			memoryStream.Write(array, 0, array.Length);
			return memoryStream.ToArray();
		}

		// Token: 0x060039B8 RID: 14776 RVA: 0x00135D00 File Offset: 0x00135D00
		private static byte[] Kdf(HashAlgorithmTag digestAlg, ECPoint s, int keyLen, byte[] parameters)
		{
			byte[] encoded = s.XCoord.GetEncoded();
			string digestName = PgpUtilities.GetDigestName(digestAlg);
			IDigest digest = DigestUtilities.GetDigest(digestName);
			digest.Update(0);
			digest.Update(0);
			digest.Update(0);
			digest.Update(1);
			digest.BlockUpdate(encoded, 0, encoded.Length);
			digest.BlockUpdate(parameters, 0, parameters.Length);
			byte[] data = DigestUtilities.DoFinal(digest);
			return Arrays.CopyOfRange(data, 0, keyLen);
		}

		// Token: 0x04001E16 RID: 7702
		private static readonly byte[] ANONYMOUS_SENDER = Hex.Decode("416E6F6E796D6F75732053656E64657220202020");
	}
}
