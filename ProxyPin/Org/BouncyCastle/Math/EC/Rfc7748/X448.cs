using System;
using Org.BouncyCastle.Math.EC.Rfc8032;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Math.EC.Rfc7748
{
	// Token: 0x02000607 RID: 1543
	public abstract class X448
	{
		// Token: 0x0600339C RID: 13212 RVA: 0x0010B3B0 File Offset: 0x0010B3B0
		public static bool CalculateAgreement(byte[] k, int kOff, byte[] u, int uOff, byte[] r, int rOff)
		{
			X448.ScalarMult(k, kOff, u, uOff, r, rOff);
			return !Arrays.AreAllZeroes(r, rOff, 56);
		}

		// Token: 0x0600339D RID: 13213 RVA: 0x0010B3D0 File Offset: 0x0010B3D0
		private static uint Decode32(byte[] bs, int off)
		{
			uint num = (uint)bs[off];
			num |= (uint)((uint)bs[++off] << 8);
			num |= (uint)((uint)bs[++off] << 16);
			return num | (uint)((uint)bs[++off] << 24);
		}

		// Token: 0x0600339E RID: 13214 RVA: 0x0010B410 File Offset: 0x0010B410
		private static void DecodeScalar(byte[] k, int kOff, uint[] n)
		{
			for (int i = 0; i < 14; i++)
			{
				n[i] = X448.Decode32(k, kOff + i * 4);
			}
			n[0] = (n[0] & 4294967292U);
			n[13] = (n[13] | 2147483648U);
		}

		// Token: 0x0600339F RID: 13215 RVA: 0x0010B45C File Offset: 0x0010B45C
		public static void GeneratePrivateKey(SecureRandom random, byte[] k)
		{
			random.NextBytes(k);
			k[0] = (k[0] & 252);
			k[55] = (k[55] | 128);
		}

		// Token: 0x060033A0 RID: 13216 RVA: 0x0010B494 File Offset: 0x0010B494
		public static void GeneratePublicKey(byte[] k, int kOff, byte[] r, int rOff)
		{
			X448.ScalarMultBase(k, kOff, r, rOff);
		}

		// Token: 0x060033A1 RID: 13217 RVA: 0x0010B4A0 File Offset: 0x0010B4A0
		private static void PointDouble(uint[] x, uint[] z)
		{
			uint[] array = X448Field.Create();
			uint[] array2 = X448Field.Create();
			X448Field.Add(x, z, array);
			X448Field.Sub(x, z, array2);
			X448Field.Sqr(array, array);
			X448Field.Sqr(array2, array2);
			X448Field.Mul(array, array2, x);
			X448Field.Sub(array, array2, array);
			X448Field.Mul(array, 39082U, z);
			X448Field.Add(z, array2, z);
			X448Field.Mul(z, array, z);
		}

		// Token: 0x060033A2 RID: 13218 RVA: 0x0010B508 File Offset: 0x0010B508
		public static void Precompute()
		{
			Ed448.Precompute();
		}

		// Token: 0x060033A3 RID: 13219 RVA: 0x0010B510 File Offset: 0x0010B510
		public static void ScalarMult(byte[] k, int kOff, byte[] u, int uOff, byte[] r, int rOff)
		{
			uint[] array = new uint[14];
			X448.DecodeScalar(k, kOff, array);
			uint[] array2 = X448Field.Create();
			X448Field.Decode(u, uOff, array2);
			uint[] array3 = X448Field.Create();
			X448Field.Copy(array2, 0, array3, 0);
			uint[] array4 = X448Field.Create();
			array4[0] = 1U;
			uint[] array5 = X448Field.Create();
			array5[0] = 1U;
			uint[] array6 = X448Field.Create();
			uint[] array7 = X448Field.Create();
			uint[] array8 = X448Field.Create();
			int num = 447;
			int num2 = 1;
			do
			{
				X448Field.Add(array5, array6, array7);
				X448Field.Sub(array5, array6, array5);
				X448Field.Add(array3, array4, array6);
				X448Field.Sub(array3, array4, array3);
				X448Field.Mul(array7, array3, array7);
				X448Field.Mul(array5, array6, array5);
				X448Field.Sqr(array6, array6);
				X448Field.Sqr(array3, array3);
				X448Field.Sub(array6, array3, array8);
				X448Field.Mul(array8, 39082U, array4);
				X448Field.Add(array4, array3, array4);
				X448Field.Mul(array4, array8, array4);
				X448Field.Mul(array3, array6, array3);
				X448Field.Sub(array7, array5, array6);
				X448Field.Add(array7, array5, array5);
				X448Field.Sqr(array5, array5);
				X448Field.Sqr(array6, array6);
				X448Field.Mul(array6, array2, array6);
				num--;
				int num3 = num >> 5;
				int num4 = num & 31;
				int num5 = (int)(array[num3] >> num4 & 1U);
				num2 ^= num5;
				X448Field.CSwap(num2, array3, array5);
				X448Field.CSwap(num2, array4, array6);
				num2 = num5;
			}
			while (num >= 2);
			for (int i = 0; i < 2; i++)
			{
				X448.PointDouble(array3, array4);
			}
			X448Field.Inv(array4, array4);
			X448Field.Mul(array3, array4, array3);
			X448Field.Normalize(array3);
			X448Field.Encode(array3, r, rOff);
		}

		// Token: 0x060033A4 RID: 13220 RVA: 0x0010B6B4 File Offset: 0x0010B6B4
		public static void ScalarMultBase(byte[] k, int kOff, byte[] r, int rOff)
		{
			uint[] array = X448Field.Create();
			uint[] y = X448Field.Create();
			Ed448.ScalarMultBaseXY(k, kOff, array, y);
			X448Field.Inv(array, array);
			X448Field.Mul(array, y, array);
			X448Field.Sqr(array, array);
			X448Field.Normalize(array);
			X448Field.Encode(array, r, rOff);
		}

		// Token: 0x04001CA8 RID: 7336
		public const int PointSize = 56;

		// Token: 0x04001CA9 RID: 7337
		public const int ScalarSize = 56;

		// Token: 0x04001CAA RID: 7338
		private const uint C_A = 156326U;

		// Token: 0x04001CAB RID: 7339
		private const uint C_A24 = 39082U;
	}
}
