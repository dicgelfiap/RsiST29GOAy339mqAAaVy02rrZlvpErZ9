using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Generators
{
	// Token: 0x020003D2 RID: 978
	public class Pkcs5S1ParametersGenerator : PbeParametersGenerator
	{
		// Token: 0x06001EEA RID: 7914 RVA: 0x000B5884 File Offset: 0x000B5884
		public Pkcs5S1ParametersGenerator(IDigest digest)
		{
			this.digest = digest;
		}

		// Token: 0x06001EEB RID: 7915 RVA: 0x000B5894 File Offset: 0x000B5894
		private byte[] GenerateDerivedKey()
		{
			byte[] array = new byte[this.digest.GetDigestSize()];
			this.digest.BlockUpdate(this.mPassword, 0, this.mPassword.Length);
			this.digest.BlockUpdate(this.mSalt, 0, this.mSalt.Length);
			this.digest.DoFinal(array, 0);
			for (int i = 1; i < this.mIterationCount; i++)
			{
				this.digest.BlockUpdate(array, 0, array.Length);
				this.digest.DoFinal(array, 0);
			}
			return array;
		}

		// Token: 0x06001EEC RID: 7916 RVA: 0x000B592C File Offset: 0x000B592C
		public override ICipherParameters GenerateDerivedParameters(int keySize)
		{
			return this.GenerateDerivedMacParameters(keySize);
		}

		// Token: 0x06001EED RID: 7917 RVA: 0x000B5938 File Offset: 0x000B5938
		public override ICipherParameters GenerateDerivedParameters(string algorithm, int keySize)
		{
			keySize /= 8;
			if (keySize > this.digest.GetDigestSize())
			{
				throw new ArgumentException("Can't Generate a derived key " + keySize + " bytes long.");
			}
			byte[] keyBytes = this.GenerateDerivedKey();
			return ParameterUtilities.CreateKeyParameter(algorithm, keyBytes, 0, keySize);
		}

		// Token: 0x06001EEE RID: 7918 RVA: 0x000B598C File Offset: 0x000B598C
		public override ICipherParameters GenerateDerivedParameters(int keySize, int ivSize)
		{
			keySize /= 8;
			ivSize /= 8;
			if (keySize + ivSize > this.digest.GetDigestSize())
			{
				throw new ArgumentException("Can't Generate a derived key " + (keySize + ivSize) + " bytes long.");
			}
			byte[] array = this.GenerateDerivedKey();
			return new ParametersWithIV(new KeyParameter(array, 0, keySize), array, keySize, ivSize);
		}

		// Token: 0x06001EEF RID: 7919 RVA: 0x000B59F0 File Offset: 0x000B59F0
		public override ICipherParameters GenerateDerivedParameters(string algorithm, int keySize, int ivSize)
		{
			keySize /= 8;
			ivSize /= 8;
			if (keySize + ivSize > this.digest.GetDigestSize())
			{
				throw new ArgumentException("Can't Generate a derived key " + (keySize + ivSize) + " bytes long.");
			}
			byte[] array = this.GenerateDerivedKey();
			KeyParameter parameters = ParameterUtilities.CreateKeyParameter(algorithm, array, 0, keySize);
			return new ParametersWithIV(parameters, array, keySize, ivSize);
		}

		// Token: 0x06001EF0 RID: 7920 RVA: 0x000B5A58 File Offset: 0x000B5A58
		public override ICipherParameters GenerateDerivedMacParameters(int keySize)
		{
			keySize /= 8;
			if (keySize > this.digest.GetDigestSize())
			{
				throw new ArgumentException("Can't Generate a derived key " + keySize + " bytes long.");
			}
			byte[] key = this.GenerateDerivedKey();
			return new KeyParameter(key, 0, keySize);
		}

		// Token: 0x04001471 RID: 5233
		private readonly IDigest digest;
	}
}
