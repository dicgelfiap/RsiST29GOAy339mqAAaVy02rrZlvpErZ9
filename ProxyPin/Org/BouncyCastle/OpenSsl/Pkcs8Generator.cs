using System;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities.IO.Pem;

namespace Org.BouncyCastle.OpenSsl
{
	// Token: 0x0200067B RID: 1659
	public class Pkcs8Generator : PemObjectGenerator
	{
		// Token: 0x060039F1 RID: 14833 RVA: 0x001374C4 File Offset: 0x001374C4
		public Pkcs8Generator(AsymmetricKeyParameter privKey)
		{
			this.privKey = privKey;
		}

		// Token: 0x060039F2 RID: 14834 RVA: 0x001374D4 File Offset: 0x001374D4
		public Pkcs8Generator(AsymmetricKeyParameter privKey, string algorithm)
		{
			this.privKey = privKey;
			this.algorithm = algorithm;
			this.iterationCount = 2048;
		}

		// Token: 0x170009F8 RID: 2552
		// (set) Token: 0x060039F3 RID: 14835 RVA: 0x001374F8 File Offset: 0x001374F8
		public SecureRandom SecureRandom
		{
			set
			{
				this.random = value;
			}
		}

		// Token: 0x170009F9 RID: 2553
		// (set) Token: 0x060039F4 RID: 14836 RVA: 0x00137504 File Offset: 0x00137504
		public char[] Password
		{
			set
			{
				this.password = value;
			}
		}

		// Token: 0x170009FA RID: 2554
		// (set) Token: 0x060039F5 RID: 14837 RVA: 0x00137510 File Offset: 0x00137510
		public int IterationCount
		{
			set
			{
				this.iterationCount = value;
			}
		}

		// Token: 0x060039F6 RID: 14838 RVA: 0x0013751C File Offset: 0x0013751C
		public PemObject Generate()
		{
			if (this.algorithm == null)
			{
				PrivateKeyInfo privateKeyInfo = PrivateKeyInfoFactory.CreatePrivateKeyInfo(this.privKey);
				return new PemObject("PRIVATE KEY", privateKeyInfo.GetEncoded());
			}
			byte[] array = new byte[20];
			if (this.random == null)
			{
				this.random = new SecureRandom();
			}
			this.random.NextBytes(array);
			PemObject result;
			try
			{
				EncryptedPrivateKeyInfo encryptedPrivateKeyInfo = EncryptedPrivateKeyInfoFactory.CreateEncryptedPrivateKeyInfo(this.algorithm, this.password, array, this.iterationCount, this.privKey);
				result = new PemObject("ENCRYPTED PRIVATE KEY", encryptedPrivateKeyInfo.GetEncoded());
			}
			catch (Exception exception)
			{
				throw new PemGenerationException("Couldn't encrypt private key", exception);
			}
			return result;
		}

		// Token: 0x04001E24 RID: 7716
		public static readonly string PbeSha1_RC4_128 = PkcsObjectIdentifiers.PbeWithShaAnd128BitRC4.Id;

		// Token: 0x04001E25 RID: 7717
		public static readonly string PbeSha1_RC4_40 = PkcsObjectIdentifiers.PbeWithShaAnd40BitRC4.Id;

		// Token: 0x04001E26 RID: 7718
		public static readonly string PbeSha1_3DES = PkcsObjectIdentifiers.PbeWithShaAnd3KeyTripleDesCbc.Id;

		// Token: 0x04001E27 RID: 7719
		public static readonly string PbeSha1_2DES = PkcsObjectIdentifiers.PbeWithShaAnd2KeyTripleDesCbc.Id;

		// Token: 0x04001E28 RID: 7720
		public static readonly string PbeSha1_RC2_128 = PkcsObjectIdentifiers.PbeWithShaAnd128BitRC2Cbc.Id;

		// Token: 0x04001E29 RID: 7721
		public static readonly string PbeSha1_RC2_40 = PkcsObjectIdentifiers.PbewithShaAnd40BitRC2Cbc.Id;

		// Token: 0x04001E2A RID: 7722
		private char[] password;

		// Token: 0x04001E2B RID: 7723
		private string algorithm;

		// Token: 0x04001E2C RID: 7724
		private int iterationCount;

		// Token: 0x04001E2D RID: 7725
		private AsymmetricKeyParameter privKey;

		// Token: 0x04001E2E RID: 7726
		private SecureRandom random;
	}
}
