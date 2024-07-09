using System;
using Org.BouncyCastle.Math.EC.Rfc8032;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Math.EC.Rfc7748
{
	// Token: 0x02000605 RID: 1541
	public abstract class X25519
	{
		// Token: 0x0600336F RID: 13167 RVA: 0x00109C88 File Offset: 0x00109C88
		public static bool CalculateAgreement(byte[] k, int kOff, byte[] u, int uOff, byte[] r, int rOff)
		{
			X25519.ScalarMult(k, kOff, u, uOff, r, rOff);
			return !Arrays.AreAllZeroes(r, rOff, 32);
		}

		// Token: 0x06003370 RID: 13168 RVA: 0x00109CA8 File Offset: 0x00109CA8
		private static uint Decode32(byte[] bs, int off)
		{
			uint num = (uint)bs[off];
			num |= (uint)((uint)bs[++off] << 8);
			num |= (uint)((uint)bs[++off] << 16);
			return num | (uint)((uint)bs[++off] << 24);
		}

		// Token: 0x06003371 RID: 13169 RVA: 0x00109CE8 File Offset: 0x00109CE8
		private static void DecodeScalar(byte[] k, int kOff, uint[] n)
		{
			for (int i = 0; i < 8; i++)
			{
				n[i] = X25519.Decode32(k, kOff + i * 4);
			}
			n[0] = (n[0] & 4294967288U);
			n[7] = (n[7] & 2147483647U);
			n[7] = (n[7] | 1073741824U);
		}

		// Token: 0x06003372 RID: 13170 RVA: 0x00109D40 File Offset: 0x00109D40
		public static void GeneratePrivateKey(SecureRandom random, byte[] k)
		{
			random.NextBytes(k);
			k[0] = (k[0] & 248);
			k[31] = (k[31] & 127);
			k[31] = (k[31] | 64);
		}

		// Token: 0x06003373 RID: 13171 RVA: 0x00109D84 File Offset: 0x00109D84
		public static void GeneratePublicKey(byte[] k, int kOff, byte[] r, int rOff)
		{
			X25519.ScalarMultBase(k, kOff, r, rOff);
		}

		// Token: 0x06003374 RID: 13172 RVA: 0x00109D90 File Offset: 0x00109D90
		private static void PointDouble(int[] x, int[] z)
		{
			int[] array = X25519Field.Create();
			int[] array2 = X25519Field.Create();
			X25519Field.Apm(x, z, array, array2);
			X25519Field.Sqr(array, array);
			X25519Field.Sqr(array2, array2);
			X25519Field.Mul(array, array2, x);
			X25519Field.Sub(array, array2, array);
			X25519Field.Mul(array, 121666, z);
			X25519Field.Add(z, array2, z);
			X25519Field.Mul(z, array, z);
		}

		// Token: 0x06003375 RID: 13173 RVA: 0x00109DF0 File Offset: 0x00109DF0
		public static void Precompute()
		{
			Ed25519.Precompute();
		}

		// Token: 0x06003376 RID: 13174 RVA: 0x00109DF8 File Offset: 0x00109DF8
		public static void ScalarMult(byte[] k, int kOff, byte[] u, int uOff, byte[] r, int rOff)
		{
			uint[] array = new uint[8];
			X25519.DecodeScalar(k, kOff, array);
			int[] array2 = X25519Field.Create();
			X25519Field.Decode(u, uOff, array2);
			int[] array3 = X25519Field.Create();
			X25519Field.Copy(array2, 0, array3, 0);
			int[] array4 = X25519Field.Create();
			array4[0] = 1;
			int[] array5 = X25519Field.Create();
			array5[0] = 1;
			int[] array6 = X25519Field.Create();
			int[] array7 = X25519Field.Create();
			int[] array8 = X25519Field.Create();
			int num = 254;
			int num2 = 1;
			do
			{
				X25519Field.Apm(array5, array6, array7, array5);
				X25519Field.Apm(array3, array4, array6, array3);
				X25519Field.Mul(array7, array3, array7);
				X25519Field.Mul(array5, array6, array5);
				X25519Field.Sqr(array6, array6);
				X25519Field.Sqr(array3, array3);
				X25519Field.Sub(array6, array3, array8);
				X25519Field.Mul(array8, 121666, array4);
				X25519Field.Add(array4, array3, array4);
				X25519Field.Mul(array4, array8, array4);
				X25519Field.Mul(array3, array6, array3);
				X25519Field.Apm(array7, array5, array5, array6);
				X25519Field.Sqr(array5, array5);
				X25519Field.Sqr(array6, array6);
				X25519Field.Mul(array6, array2, array6);
				num--;
				int num3 = num >> 5;
				int num4 = num & 31;
				int num5 = (int)(array[num3] >> num4 & 1U);
				num2 ^= num5;
				X25519Field.CSwap(num2, array3, array5);
				X25519Field.CSwap(num2, array4, array6);
				num2 = num5;
			}
			while (num >= 3);
			for (int i = 0; i < 3; i++)
			{
				X25519.PointDouble(array3, array4);
			}
			X25519Field.Inv(array4, array4);
			X25519Field.Mul(array3, array4, array3);
			X25519Field.Normalize(array3);
			X25519Field.Encode(array3, r, rOff);
		}

		// Token: 0x06003377 RID: 13175 RVA: 0x00109F84 File Offset: 0x00109F84
		public static void ScalarMultBase(byte[] k, int kOff, byte[] r, int rOff)
		{
			int[] array = X25519Field.Create();
			int[] array2 = X25519Field.Create();
			Ed25519.ScalarMultBaseYZ(k, kOff, array, array2);
			X25519Field.Apm(array2, array, array, array2);
			X25519Field.Inv(array2, array2);
			X25519Field.Mul(array, array2, array);
			X25519Field.Normalize(array);
			X25519Field.Encode(array, r, rOff);
		}

		// Token: 0x04001C9F RID: 7327
		public const int PointSize = 32;

		// Token: 0x04001CA0 RID: 7328
		public const int ScalarSize = 32;

		// Token: 0x04001CA1 RID: 7329
		private const int C_A = 486662;

		// Token: 0x04001CA2 RID: 7330
		private const int C_A24 = 121666;
	}
}
