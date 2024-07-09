using System;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Utilities;

namespace Org.BouncyCastle.Crypto.Generators
{
	// Token: 0x020003D7 RID: 983
	public class SCrypt
	{
		// Token: 0x06001F08 RID: 7944 RVA: 0x000B6244 File Offset: 0x000B6244
		public static byte[] Generate(byte[] P, byte[] S, int N, int r, int p, int dkLen)
		{
			if (P == null)
			{
				throw new ArgumentNullException("Passphrase P must be provided.");
			}
			if (S == null)
			{
				throw new ArgumentNullException("Salt S must be provided.");
			}
			if (N <= 1 || !SCrypt.IsPowerOf2(N))
			{
				throw new ArgumentException("Cost parameter N must be > 1 and a power of 2.");
			}
			if (r == 1 && N >= 65536)
			{
				throw new ArgumentException("Cost parameter N must be > 1 and < 65536.");
			}
			if (r < 1)
			{
				throw new ArgumentException("Block size r must be >= 1.");
			}
			int num = int.MaxValue / (128 * r * 8);
			if (p < 1 || p > num)
			{
				throw new ArgumentException(string.Concat(new object[]
				{
					"Parallelisation parameter p must be >= 1 and <= ",
					num,
					" (based on block size r of ",
					r,
					")"
				}));
			}
			if (dkLen < 1)
			{
				throw new ArgumentException("Generated key length dkLen must be >= 1.");
			}
			return SCrypt.MFcrypt(P, S, N, r, p, dkLen);
		}

		// Token: 0x06001F09 RID: 7945 RVA: 0x000B6340 File Offset: 0x000B6340
		private static byte[] MFcrypt(byte[] P, byte[] S, int N, int r, int p, int dkLen)
		{
			int num = r * 128;
			byte[] array = SCrypt.SingleIterationPBKDF2(P, S, p * num);
			uint[] array2 = null;
			byte[] result;
			try
			{
				int num2 = array.Length >> 2;
				array2 = new uint[num2];
				Pack.LE_To_UInt32(array, 0, array2);
				int num3 = num >> 2;
				for (int i = 0; i < num2; i += num3)
				{
					SCrypt.SMix(array2, i, N, r);
				}
				Pack.UInt32_To_LE(array2, array, 0);
				result = SCrypt.SingleIterationPBKDF2(P, array, dkLen);
			}
			finally
			{
				SCrypt.ClearAll(new Array[]
				{
					array,
					array2
				});
			}
			return result;
		}

		// Token: 0x06001F0A RID: 7946 RVA: 0x000B63E8 File Offset: 0x000B63E8
		private static byte[] SingleIterationPBKDF2(byte[] P, byte[] S, int dkLen)
		{
			PbeParametersGenerator pbeParametersGenerator = new Pkcs5S2ParametersGenerator(new Sha256Digest());
			pbeParametersGenerator.Init(P, S, 1);
			KeyParameter keyParameter = (KeyParameter)pbeParametersGenerator.GenerateDerivedMacParameters(dkLen * 8);
			return keyParameter.GetKey();
		}

		// Token: 0x06001F0B RID: 7947 RVA: 0x000B6424 File Offset: 0x000B6424
		private static void SMix(uint[] B, int BOff, int N, int r)
		{
			int num = r * 32;
			uint[] array = new uint[16];
			uint[] array2 = new uint[16];
			uint[] array3 = new uint[num];
			uint[] array4 = new uint[num];
			uint[] array5 = new uint[N * num];
			try
			{
				Array.Copy(B, BOff, array4, 0, num);
				int num2 = 0;
				for (int i = 0; i < N; i += 2)
				{
					Array.Copy(array4, 0, array5, num2, num);
					num2 += num;
					SCrypt.BlockMix(array4, array, array2, array3, r);
					Array.Copy(array3, 0, array5, num2, num);
					num2 += num;
					SCrypt.BlockMix(array3, array, array2, array4, r);
				}
				uint num3 = (uint)(N - 1);
				for (int j = 0; j < N; j++)
				{
					int num4 = (int)(array4[num - 16] & num3);
					Array.Copy(array5, num4 * num, array3, 0, num);
					SCrypt.Xor(array3, array4, 0, array3);
					SCrypt.BlockMix(array3, array, array2, array4, r);
				}
				Array.Copy(array4, 0, B, BOff, num);
			}
			finally
			{
				SCrypt.Clear(array5);
				SCrypt.ClearAll(new Array[]
				{
					array4,
					array,
					array2,
					array3
				});
			}
		}

		// Token: 0x06001F0C RID: 7948 RVA: 0x000B6560 File Offset: 0x000B6560
		private static void BlockMix(uint[] B, uint[] X1, uint[] X2, uint[] Y, int r)
		{
			Array.Copy(B, B.Length - 16, X1, 0, 16);
			int num = 0;
			int num2 = 0;
			int num3 = B.Length >> 1;
			for (int i = 2 * r; i > 0; i--)
			{
				SCrypt.Xor(X1, B, num, X2);
				Salsa20Engine.SalsaCore(8, X2, X1);
				Array.Copy(X1, 0, Y, num2, 16);
				num2 = num3 + num - num2;
				num += 16;
			}
		}

		// Token: 0x06001F0D RID: 7949 RVA: 0x000B65C4 File Offset: 0x000B65C4
		private static void Xor(uint[] a, uint[] b, int bOff, uint[] output)
		{
			for (int i = output.Length - 1; i >= 0; i--)
			{
				output[i] = (a[i] ^ b[bOff + i]);
			}
		}

		// Token: 0x06001F0E RID: 7950 RVA: 0x000B65F4 File Offset: 0x000B65F4
		private static void Clear(Array array)
		{
			if (array != null)
			{
				Array.Clear(array, 0, array.Length);
			}
		}

		// Token: 0x06001F0F RID: 7951 RVA: 0x000B660C File Offset: 0x000B660C
		private static void ClearAll(params Array[] arrays)
		{
			foreach (Array array in arrays)
			{
				SCrypt.Clear(array);
			}
		}

		// Token: 0x06001F10 RID: 7952 RVA: 0x000B6640 File Offset: 0x000B6640
		private static bool IsPowerOf2(int x)
		{
			return (x & x - 1) == 0;
		}
	}
}
