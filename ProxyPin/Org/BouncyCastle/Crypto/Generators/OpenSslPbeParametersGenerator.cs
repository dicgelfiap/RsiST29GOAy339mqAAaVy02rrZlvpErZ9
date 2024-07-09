using System;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Generators
{
	// Token: 0x020003D0 RID: 976
	public class OpenSslPbeParametersGenerator : PbeParametersGenerator
	{
		// Token: 0x06001EDA RID: 7898 RVA: 0x000B52AC File Offset: 0x000B52AC
		public override void Init(byte[] password, byte[] salt, int iterationCount)
		{
			base.Init(password, salt, 1);
		}

		// Token: 0x06001EDB RID: 7899 RVA: 0x000B52B8 File Offset: 0x000B52B8
		public virtual void Init(byte[] password, byte[] salt)
		{
			base.Init(password, salt, 1);
		}

		// Token: 0x06001EDC RID: 7900 RVA: 0x000B52C4 File Offset: 0x000B52C4
		private byte[] GenerateDerivedKey(int bytesNeeded)
		{
			byte[] array = new byte[this.digest.GetDigestSize()];
			byte[] array2 = new byte[bytesNeeded];
			int num = 0;
			for (;;)
			{
				this.digest.BlockUpdate(this.mPassword, 0, this.mPassword.Length);
				this.digest.BlockUpdate(this.mSalt, 0, this.mSalt.Length);
				this.digest.DoFinal(array, 0);
				int num2 = (bytesNeeded > array.Length) ? array.Length : bytesNeeded;
				Array.Copy(array, 0, array2, num, num2);
				num += num2;
				bytesNeeded -= num2;
				if (bytesNeeded == 0)
				{
					break;
				}
				this.digest.Reset();
				this.digest.BlockUpdate(array, 0, array.Length);
			}
			return array2;
		}

		// Token: 0x06001EDD RID: 7901 RVA: 0x000B5380 File Offset: 0x000B5380
		[Obsolete("Use version with 'algorithm' parameter")]
		public override ICipherParameters GenerateDerivedParameters(int keySize)
		{
			return this.GenerateDerivedMacParameters(keySize);
		}

		// Token: 0x06001EDE RID: 7902 RVA: 0x000B538C File Offset: 0x000B538C
		public override ICipherParameters GenerateDerivedParameters(string algorithm, int keySize)
		{
			keySize /= 8;
			byte[] keyBytes = this.GenerateDerivedKey(keySize);
			return ParameterUtilities.CreateKeyParameter(algorithm, keyBytes, 0, keySize);
		}

		// Token: 0x06001EDF RID: 7903 RVA: 0x000B53B4 File Offset: 0x000B53B4
		[Obsolete("Use version with 'algorithm' parameter")]
		public override ICipherParameters GenerateDerivedParameters(int keySize, int ivSize)
		{
			keySize /= 8;
			ivSize /= 8;
			byte[] array = this.GenerateDerivedKey(keySize + ivSize);
			return new ParametersWithIV(new KeyParameter(array, 0, keySize), array, keySize, ivSize);
		}

		// Token: 0x06001EE0 RID: 7904 RVA: 0x000B53EC File Offset: 0x000B53EC
		public override ICipherParameters GenerateDerivedParameters(string algorithm, int keySize, int ivSize)
		{
			keySize /= 8;
			ivSize /= 8;
			byte[] array = this.GenerateDerivedKey(keySize + ivSize);
			KeyParameter parameters = ParameterUtilities.CreateKeyParameter(algorithm, array, 0, keySize);
			return new ParametersWithIV(parameters, array, keySize, ivSize);
		}

		// Token: 0x06001EE1 RID: 7905 RVA: 0x000B5424 File Offset: 0x000B5424
		public override ICipherParameters GenerateDerivedMacParameters(int keySize)
		{
			keySize /= 8;
			byte[] key = this.GenerateDerivedKey(keySize);
			return new KeyParameter(key, 0, keySize);
		}

		// Token: 0x0400146A RID: 5226
		private readonly IDigest digest = new MD5Digest();
	}
}
