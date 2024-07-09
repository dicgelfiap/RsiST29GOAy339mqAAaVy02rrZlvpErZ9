using System;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Macs;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Generators
{
	// Token: 0x020003D3 RID: 979
	public class Pkcs5S2ParametersGenerator : PbeParametersGenerator
	{
		// Token: 0x06001EF1 RID: 7921 RVA: 0x000B5AAC File Offset: 0x000B5AAC
		public Pkcs5S2ParametersGenerator() : this(new Sha1Digest())
		{
		}

		// Token: 0x06001EF2 RID: 7922 RVA: 0x000B5ABC File Offset: 0x000B5ABC
		public Pkcs5S2ParametersGenerator(IDigest digest)
		{
			this.hMac = new HMac(digest);
			this.state = new byte[this.hMac.GetMacSize()];
		}

		// Token: 0x06001EF3 RID: 7923 RVA: 0x000B5AE8 File Offset: 0x000B5AE8
		private void F(byte[] S, int c, byte[] iBuf, byte[] outBytes, int outOff)
		{
			if (c == 0)
			{
				throw new ArgumentException("iteration count must be at least 1.");
			}
			if (S != null)
			{
				this.hMac.BlockUpdate(S, 0, S.Length);
			}
			this.hMac.BlockUpdate(iBuf, 0, iBuf.Length);
			this.hMac.DoFinal(this.state, 0);
			Array.Copy(this.state, 0, outBytes, outOff, this.state.Length);
			for (int i = 1; i < c; i++)
			{
				this.hMac.BlockUpdate(this.state, 0, this.state.Length);
				this.hMac.DoFinal(this.state, 0);
				for (int j = 0; j < this.state.Length; j++)
				{
					IntPtr intPtr;
					outBytes[(int)(intPtr = (IntPtr)(outOff + j))] = (outBytes[(int)intPtr] ^ this.state[j]);
				}
			}
		}

		// Token: 0x06001EF4 RID: 7924 RVA: 0x000B5BC8 File Offset: 0x000B5BC8
		private byte[] GenerateDerivedKey(int dkLen)
		{
			int macSize = this.hMac.GetMacSize();
			int num = (dkLen + macSize - 1) / macSize;
			byte[] array = new byte[4];
			byte[] array2 = new byte[num * macSize];
			int num2 = 0;
			ICipherParameters parameters = new KeyParameter(this.mPassword);
			this.hMac.Init(parameters);
			for (int i = 1; i <= num; i++)
			{
				int num3 = 3;
				byte[] array3;
				IntPtr intPtr;
				while (((array3 = array)[(int)(intPtr = (IntPtr)num3)] = array3[(int)intPtr] + 1) == 0)
				{
					num3--;
				}
				this.F(this.mSalt, this.mIterationCount, array, array2, num2);
				num2 += macSize;
			}
			return array2;
		}

		// Token: 0x06001EF5 RID: 7925 RVA: 0x000B5C74 File Offset: 0x000B5C74
		public override ICipherParameters GenerateDerivedParameters(int keySize)
		{
			return this.GenerateDerivedMacParameters(keySize);
		}

		// Token: 0x06001EF6 RID: 7926 RVA: 0x000B5C80 File Offset: 0x000B5C80
		public override ICipherParameters GenerateDerivedParameters(string algorithm, int keySize)
		{
			keySize /= 8;
			byte[] keyBytes = this.GenerateDerivedKey(keySize);
			return ParameterUtilities.CreateKeyParameter(algorithm, keyBytes, 0, keySize);
		}

		// Token: 0x06001EF7 RID: 7927 RVA: 0x000B5CA8 File Offset: 0x000B5CA8
		public override ICipherParameters GenerateDerivedParameters(int keySize, int ivSize)
		{
			keySize /= 8;
			ivSize /= 8;
			byte[] array = this.GenerateDerivedKey(keySize + ivSize);
			return new ParametersWithIV(new KeyParameter(array, 0, keySize), array, keySize, ivSize);
		}

		// Token: 0x06001EF8 RID: 7928 RVA: 0x000B5CE0 File Offset: 0x000B5CE0
		public override ICipherParameters GenerateDerivedParameters(string algorithm, int keySize, int ivSize)
		{
			keySize /= 8;
			ivSize /= 8;
			byte[] array = this.GenerateDerivedKey(keySize + ivSize);
			KeyParameter parameters = ParameterUtilities.CreateKeyParameter(algorithm, array, 0, keySize);
			return new ParametersWithIV(parameters, array, keySize, ivSize);
		}

		// Token: 0x06001EF9 RID: 7929 RVA: 0x000B5D18 File Offset: 0x000B5D18
		public override ICipherParameters GenerateDerivedMacParameters(int keySize)
		{
			keySize /= 8;
			byte[] key = this.GenerateDerivedKey(keySize);
			return new KeyParameter(key, 0, keySize);
		}

		// Token: 0x04001472 RID: 5234
		private readonly IMac hMac;

		// Token: 0x04001473 RID: 5235
		private readonly byte[] state;
	}
}
