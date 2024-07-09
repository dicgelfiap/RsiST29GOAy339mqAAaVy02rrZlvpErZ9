using System;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Pkcs
{
	// Token: 0x0200067E RID: 1662
	public sealed class EncryptedPrivateKeyInfoFactory
	{
		// Token: 0x06003A05 RID: 14853 RVA: 0x00137814 File Offset: 0x00137814
		private EncryptedPrivateKeyInfoFactory()
		{
		}

		// Token: 0x06003A06 RID: 14854 RVA: 0x0013781C File Offset: 0x0013781C
		public static EncryptedPrivateKeyInfo CreateEncryptedPrivateKeyInfo(DerObjectIdentifier algorithm, char[] passPhrase, byte[] salt, int iterationCount, AsymmetricKeyParameter key)
		{
			return EncryptedPrivateKeyInfoFactory.CreateEncryptedPrivateKeyInfo(algorithm.Id, passPhrase, salt, iterationCount, PrivateKeyInfoFactory.CreatePrivateKeyInfo(key));
		}

		// Token: 0x06003A07 RID: 14855 RVA: 0x00137834 File Offset: 0x00137834
		public static EncryptedPrivateKeyInfo CreateEncryptedPrivateKeyInfo(string algorithm, char[] passPhrase, byte[] salt, int iterationCount, AsymmetricKeyParameter key)
		{
			return EncryptedPrivateKeyInfoFactory.CreateEncryptedPrivateKeyInfo(algorithm, passPhrase, salt, iterationCount, PrivateKeyInfoFactory.CreatePrivateKeyInfo(key));
		}

		// Token: 0x06003A08 RID: 14856 RVA: 0x00137848 File Offset: 0x00137848
		public static EncryptedPrivateKeyInfo CreateEncryptedPrivateKeyInfo(string algorithm, char[] passPhrase, byte[] salt, int iterationCount, PrivateKeyInfo keyInfo)
		{
			IBufferedCipher bufferedCipher = PbeUtilities.CreateEngine(algorithm) as IBufferedCipher;
			if (bufferedCipher == null)
			{
				throw new Exception("Unknown encryption algorithm: " + algorithm);
			}
			Asn1Encodable asn1Encodable = PbeUtilities.GenerateAlgorithmParameters(algorithm, salt, iterationCount);
			ICipherParameters parameters = PbeUtilities.GenerateCipherParameters(algorithm, passPhrase, asn1Encodable);
			bufferedCipher.Init(true, parameters);
			byte[] encoding = bufferedCipher.DoFinal(keyInfo.GetEncoded());
			DerObjectIdentifier objectIdentifier = PbeUtilities.GetObjectIdentifier(algorithm);
			AlgorithmIdentifier algId = new AlgorithmIdentifier(objectIdentifier, asn1Encodable);
			return new EncryptedPrivateKeyInfo(algId, encoding);
		}
	}
}
