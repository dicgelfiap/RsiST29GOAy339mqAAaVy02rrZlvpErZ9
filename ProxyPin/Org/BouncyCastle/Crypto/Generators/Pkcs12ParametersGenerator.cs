using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Generators
{
	// Token: 0x020003D1 RID: 977
	public class Pkcs12ParametersGenerator : PbeParametersGenerator
	{
		// Token: 0x06001EE2 RID: 7906 RVA: 0x000B544C File Offset: 0x000B544C
		public Pkcs12ParametersGenerator(IDigest digest)
		{
			this.digest = digest;
			this.u = digest.GetDigestSize();
			this.v = digest.GetByteLength();
		}

		// Token: 0x06001EE3 RID: 7907 RVA: 0x000B5474 File Offset: 0x000B5474
		private void Adjust(byte[] a, int aOff, byte[] b)
		{
			int num = (int)((b[b.Length - 1] & byte.MaxValue) + (a[aOff + b.Length - 1] & byte.MaxValue) + 1);
			a[aOff + b.Length - 1] = (byte)num;
			num = (int)((uint)num >> 8);
			for (int i = b.Length - 2; i >= 0; i--)
			{
				num += (int)((b[i] & byte.MaxValue) + (a[aOff + i] & byte.MaxValue));
				a[aOff + i] = (byte)num;
				num = (int)((uint)num >> 8);
			}
		}

		// Token: 0x06001EE4 RID: 7908 RVA: 0x000B54EC File Offset: 0x000B54EC
		private byte[] GenerateDerivedKey(int idByte, int n)
		{
			byte[] array = new byte[this.v];
			byte[] array2 = new byte[n];
			for (int num = 0; num != array.Length; num++)
			{
				array[num] = (byte)idByte;
			}
			byte[] array3;
			if (this.mSalt != null && this.mSalt.Length != 0)
			{
				array3 = new byte[this.v * ((this.mSalt.Length + this.v - 1) / this.v)];
				for (int num2 = 0; num2 != array3.Length; num2++)
				{
					array3[num2] = this.mSalt[num2 % this.mSalt.Length];
				}
			}
			else
			{
				array3 = new byte[0];
			}
			byte[] array4;
			if (this.mPassword != null && this.mPassword.Length != 0)
			{
				array4 = new byte[this.v * ((this.mPassword.Length + this.v - 1) / this.v)];
				for (int num3 = 0; num3 != array4.Length; num3++)
				{
					array4[num3] = this.mPassword[num3 % this.mPassword.Length];
				}
			}
			else
			{
				array4 = new byte[0];
			}
			byte[] array5 = new byte[array3.Length + array4.Length];
			Array.Copy(array3, 0, array5, 0, array3.Length);
			Array.Copy(array4, 0, array5, array3.Length, array4.Length);
			byte[] array6 = new byte[this.v];
			int num4 = (n + this.u - 1) / this.u;
			byte[] array7 = new byte[this.u];
			for (int i = 1; i <= num4; i++)
			{
				this.digest.BlockUpdate(array, 0, array.Length);
				this.digest.BlockUpdate(array5, 0, array5.Length);
				this.digest.DoFinal(array7, 0);
				for (int num5 = 1; num5 != this.mIterationCount; num5++)
				{
					this.digest.BlockUpdate(array7, 0, array7.Length);
					this.digest.DoFinal(array7, 0);
				}
				for (int num6 = 0; num6 != array6.Length; num6++)
				{
					array6[num6] = array7[num6 % array7.Length];
				}
				for (int num7 = 0; num7 != array5.Length / this.v; num7++)
				{
					this.Adjust(array5, num7 * this.v, array6);
				}
				if (i == num4)
				{
					Array.Copy(array7, 0, array2, (i - 1) * this.u, array2.Length - (i - 1) * this.u);
				}
				else
				{
					Array.Copy(array7, 0, array2, (i - 1) * this.u, array7.Length);
				}
			}
			return array2;
		}

		// Token: 0x06001EE5 RID: 7909 RVA: 0x000B578C File Offset: 0x000B578C
		public override ICipherParameters GenerateDerivedParameters(int keySize)
		{
			keySize /= 8;
			byte[] key = this.GenerateDerivedKey(1, keySize);
			return new KeyParameter(key, 0, keySize);
		}

		// Token: 0x06001EE6 RID: 7910 RVA: 0x000B57B4 File Offset: 0x000B57B4
		public override ICipherParameters GenerateDerivedParameters(string algorithm, int keySize)
		{
			keySize /= 8;
			byte[] keyBytes = this.GenerateDerivedKey(1, keySize);
			return ParameterUtilities.CreateKeyParameter(algorithm, keyBytes, 0, keySize);
		}

		// Token: 0x06001EE7 RID: 7911 RVA: 0x000B57DC File Offset: 0x000B57DC
		public override ICipherParameters GenerateDerivedParameters(int keySize, int ivSize)
		{
			keySize /= 8;
			ivSize /= 8;
			byte[] key = this.GenerateDerivedKey(1, keySize);
			byte[] iv = this.GenerateDerivedKey(2, ivSize);
			return new ParametersWithIV(new KeyParameter(key, 0, keySize), iv, 0, ivSize);
		}

		// Token: 0x06001EE8 RID: 7912 RVA: 0x000B581C File Offset: 0x000B581C
		public override ICipherParameters GenerateDerivedParameters(string algorithm, int keySize, int ivSize)
		{
			keySize /= 8;
			ivSize /= 8;
			byte[] keyBytes = this.GenerateDerivedKey(1, keySize);
			KeyParameter parameters = ParameterUtilities.CreateKeyParameter(algorithm, keyBytes, 0, keySize);
			byte[] iv = this.GenerateDerivedKey(2, ivSize);
			return new ParametersWithIV(parameters, iv, 0, ivSize);
		}

		// Token: 0x06001EE9 RID: 7913 RVA: 0x000B585C File Offset: 0x000B585C
		public override ICipherParameters GenerateDerivedMacParameters(int keySize)
		{
			keySize /= 8;
			byte[] key = this.GenerateDerivedKey(3, keySize);
			return new KeyParameter(key, 0, keySize);
		}

		// Token: 0x0400146B RID: 5227
		public const int KeyMaterial = 1;

		// Token: 0x0400146C RID: 5228
		public const int IVMaterial = 2;

		// Token: 0x0400146D RID: 5229
		public const int MacMaterial = 3;

		// Token: 0x0400146E RID: 5230
		private readonly IDigest digest;

		// Token: 0x0400146F RID: 5231
		private readonly int u;

		// Token: 0x04001470 RID: 5232
		private readonly int v;
	}
}
