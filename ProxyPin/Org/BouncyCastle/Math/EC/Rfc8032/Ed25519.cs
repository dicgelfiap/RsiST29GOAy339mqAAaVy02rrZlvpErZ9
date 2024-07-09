using System;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Math.EC.Rfc7748;
using Org.BouncyCastle.Math.Raw;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Math.EC.Rfc8032
{
	// Token: 0x02000609 RID: 1545
	public abstract class Ed25519
	{
		// Token: 0x060033C8 RID: 13256 RVA: 0x0010D728 File Offset: 0x0010D728
		private static byte[] CalculateS(byte[] r, byte[] k, byte[] s)
		{
			uint[] array = new uint[16];
			Ed25519.DecodeScalar(r, 0, array);
			uint[] array2 = new uint[8];
			Ed25519.DecodeScalar(k, 0, array2);
			uint[] array3 = new uint[8];
			Ed25519.DecodeScalar(s, 0, array3);
			Nat256.MulAddTo(array2, array3, array);
			byte[] array4 = new byte[64];
			for (int i = 0; i < array.Length; i++)
			{
				Ed25519.Encode32(array[i], array4, i * 4);
			}
			return Ed25519.ReduceScalar(array4);
		}

		// Token: 0x060033C9 RID: 13257 RVA: 0x0010D7A4 File Offset: 0x0010D7A4
		private static bool CheckContextVar(byte[] ctx, byte phflag)
		{
			return (ctx == null && phflag == 0) || (ctx != null && ctx.Length < 256);
		}

		// Token: 0x060033CA RID: 13258 RVA: 0x0010D7C8 File Offset: 0x0010D7C8
		private static int CheckPoint(int[] x, int[] y)
		{
			int[] array = X25519Field.Create();
			int[] array2 = X25519Field.Create();
			int[] array3 = X25519Field.Create();
			X25519Field.Sqr(x, array2);
			X25519Field.Sqr(y, array3);
			X25519Field.Mul(array2, array3, array);
			X25519Field.Sub(array3, array2, array3);
			X25519Field.Mul(array, Ed25519.C_d, array);
			X25519Field.AddOne(array);
			X25519Field.Sub(array, array3, array);
			X25519Field.Normalize(array);
			return X25519Field.IsZero(array);
		}

		// Token: 0x060033CB RID: 13259 RVA: 0x0010D830 File Offset: 0x0010D830
		private static int CheckPoint(int[] x, int[] y, int[] z)
		{
			int[] array = X25519Field.Create();
			int[] array2 = X25519Field.Create();
			int[] array3 = X25519Field.Create();
			int[] array4 = X25519Field.Create();
			X25519Field.Sqr(x, array2);
			X25519Field.Sqr(y, array3);
			X25519Field.Sqr(z, array4);
			X25519Field.Mul(array2, array3, array);
			X25519Field.Sub(array3, array2, array3);
			X25519Field.Mul(array3, array4, array3);
			X25519Field.Sqr(array4, array4);
			X25519Field.Mul(array, Ed25519.C_d, array);
			X25519Field.Add(array, array4, array);
			X25519Field.Sub(array, array3, array);
			X25519Field.Normalize(array);
			return X25519Field.IsZero(array);
		}

		// Token: 0x060033CC RID: 13260 RVA: 0x0010D8B8 File Offset: 0x0010D8B8
		private static bool CheckPointVar(byte[] p)
		{
			uint[] array = new uint[8];
			Ed25519.Decode32(p, 0, array, 0, 8);
			uint[] array2;
			(array2 = array)[7] = (array2[7] & 2147483647U);
			return !Nat256.Gte(array, Ed25519.P);
		}

		// Token: 0x060033CD RID: 13261 RVA: 0x0010D8F8 File Offset: 0x0010D8F8
		private static bool CheckScalarVar(byte[] s)
		{
			uint[] array = new uint[8];
			Ed25519.DecodeScalar(s, 0, array);
			return !Nat256.Gte(array, Ed25519.L);
		}

		// Token: 0x060033CE RID: 13262 RVA: 0x0010D928 File Offset: 0x0010D928
		private static IDigest CreateDigest()
		{
			return new Sha512Digest();
		}

		// Token: 0x060033CF RID: 13263 RVA: 0x0010D930 File Offset: 0x0010D930
		public static IDigest CreatePrehash()
		{
			return Ed25519.CreateDigest();
		}

		// Token: 0x060033D0 RID: 13264 RVA: 0x0010D938 File Offset: 0x0010D938
		private static uint Decode24(byte[] bs, int off)
		{
			uint num = (uint)bs[off];
			num |= (uint)((uint)bs[++off] << 8);
			return num | (uint)((uint)bs[++off] << 16);
		}

		// Token: 0x060033D1 RID: 13265 RVA: 0x0010D96C File Offset: 0x0010D96C
		private static uint Decode32(byte[] bs, int off)
		{
			uint num = (uint)bs[off];
			num |= (uint)((uint)bs[++off] << 8);
			num |= (uint)((uint)bs[++off] << 16);
			return num | (uint)((uint)bs[++off] << 24);
		}

		// Token: 0x060033D2 RID: 13266 RVA: 0x0010D9AC File Offset: 0x0010D9AC
		private static void Decode32(byte[] bs, int bsOff, uint[] n, int nOff, int nLen)
		{
			for (int i = 0; i < nLen; i++)
			{
				n[nOff + i] = Ed25519.Decode32(bs, bsOff + i * 4);
			}
		}

		// Token: 0x060033D3 RID: 13267 RVA: 0x0010D9E0 File Offset: 0x0010D9E0
		private static bool DecodePointVar(byte[] p, int pOff, bool negate, Ed25519.PointExt r)
		{
			byte[] array = Arrays.CopyOfRange(p, pOff, pOff + 32);
			if (!Ed25519.CheckPointVar(array))
			{
				return false;
			}
			int num = (array[31] & 128) >> 7;
			byte[] array2;
			(array2 = array)[31] = (array2[31] & 127);
			X25519Field.Decode(array, 0, r.y);
			int[] array3 = X25519Field.Create();
			int[] array4 = X25519Field.Create();
			X25519Field.Sqr(r.y, array3);
			X25519Field.Mul(Ed25519.C_d, array3, array4);
			X25519Field.SubOne(array3);
			X25519Field.AddOne(array4);
			if (!X25519Field.SqrtRatioVar(array3, array4, r.x))
			{
				return false;
			}
			X25519Field.Normalize(r.x);
			if (num == 1 && X25519Field.IsZeroVar(r.x))
			{
				return false;
			}
			if (negate ^ num != (r.x[0] & 1))
			{
				X25519Field.Negate(r.x, r.x);
			}
			Ed25519.PointExtendXY(r);
			return true;
		}

		// Token: 0x060033D4 RID: 13268 RVA: 0x0010DAD0 File Offset: 0x0010DAD0
		private static void DecodeScalar(byte[] k, int kOff, uint[] n)
		{
			Ed25519.Decode32(k, kOff, n, 0, 8);
		}

		// Token: 0x060033D5 RID: 13269 RVA: 0x0010DADC File Offset: 0x0010DADC
		private static void Dom2(IDigest d, byte phflag, byte[] ctx)
		{
			if (ctx != null)
			{
				d.BlockUpdate(Ed25519.Dom2Prefix, 0, Ed25519.Dom2Prefix.Length);
				d.Update(phflag);
				d.Update((byte)ctx.Length);
				d.BlockUpdate(ctx, 0, ctx.Length);
			}
		}

		// Token: 0x060033D6 RID: 13270 RVA: 0x0010DB24 File Offset: 0x0010DB24
		private static void Encode24(uint n, byte[] bs, int off)
		{
			bs[off] = (byte)n;
			bs[++off] = (byte)(n >> 8);
			bs[++off] = (byte)(n >> 16);
		}

		// Token: 0x060033D7 RID: 13271 RVA: 0x0010DB44 File Offset: 0x0010DB44
		private static void Encode32(uint n, byte[] bs, int off)
		{
			bs[off] = (byte)n;
			bs[++off] = (byte)(n >> 8);
			bs[++off] = (byte)(n >> 16);
			bs[++off] = (byte)(n >> 24);
		}

		// Token: 0x060033D8 RID: 13272 RVA: 0x0010DB74 File Offset: 0x0010DB74
		private static void Encode56(ulong n, byte[] bs, int off)
		{
			Ed25519.Encode32((uint)n, bs, off);
			Ed25519.Encode24((uint)(n >> 32), bs, off + 4);
		}

		// Token: 0x060033D9 RID: 13273 RVA: 0x0010DB90 File Offset: 0x0010DB90
		private static int EncodePoint(Ed25519.PointAccum p, byte[] r, int rOff)
		{
			int[] array = X25519Field.Create();
			int[] array2 = X25519Field.Create();
			X25519Field.Inv(p.z, array2);
			X25519Field.Mul(p.x, array2, array);
			X25519Field.Mul(p.y, array2, array2);
			X25519Field.Normalize(array);
			X25519Field.Normalize(array2);
			int result = Ed25519.CheckPoint(array, array2);
			X25519Field.Encode(array2, r, rOff);
			IntPtr intPtr;
			r[(int)(intPtr = (IntPtr)(rOff + 32 - 1))] = (r[(int)intPtr] | (byte)((array[0] & 1) << 7));
			return result;
		}

		// Token: 0x060033DA RID: 13274 RVA: 0x0010DC0C File Offset: 0x0010DC0C
		public static void GeneratePrivateKey(SecureRandom random, byte[] k)
		{
			random.NextBytes(k);
		}

		// Token: 0x060033DB RID: 13275 RVA: 0x0010DC18 File Offset: 0x0010DC18
		public static void GeneratePublicKey(byte[] sk, int skOff, byte[] pk, int pkOff)
		{
			IDigest digest = Ed25519.CreateDigest();
			byte[] array = new byte[digest.GetDigestSize()];
			digest.BlockUpdate(sk, skOff, Ed25519.SecretKeySize);
			digest.DoFinal(array, 0);
			byte[] array2 = new byte[32];
			Ed25519.PruneScalar(array, 0, array2);
			Ed25519.ScalarMultBaseEncoded(array2, pk, pkOff);
		}

		// Token: 0x060033DC RID: 13276 RVA: 0x0010DC6C File Offset: 0x0010DC6C
		private static sbyte[] GetWnafVar(uint[] n, int width)
		{
			uint[] array = new uint[16];
			uint num = 0U;
			int num2 = array.Length;
			int num3 = 8;
			while (--num3 >= 0)
			{
				uint num4 = n[num3];
				array[--num2] = (num4 >> 16 | num << 16);
				num = (array[--num2] = num4);
			}
			sbyte[] array2 = new sbyte[253];
			uint num5 = 1U << width;
			uint num6 = num5 - 1U;
			uint num7 = num5 >> 1;
			uint num8 = 0U;
			int i = 0;
			int j = 0;
			while (j < array.Length)
			{
				uint num9 = array[j];
				while (i < 16)
				{
					uint num10 = num9 >> i;
					uint num11 = num10 & 1U;
					if (num11 == num8)
					{
						i++;
					}
					else
					{
						uint num12 = (num10 & num6) + num8;
						num8 = (num12 & num7);
						num12 -= num8 << 1;
						num8 >>= width - 1;
						array2[(j << 4) + i] = (sbyte)num12;
						i += width;
					}
				}
				j++;
				i -= 16;
			}
			return array2;
		}

		// Token: 0x060033DD RID: 13277 RVA: 0x0010DD70 File Offset: 0x0010DD70
		private static void ImplSign(IDigest d, byte[] h, byte[] s, byte[] pk, int pkOff, byte[] ctx, byte phflag, byte[] m, int mOff, int mLen, byte[] sig, int sigOff)
		{
			Ed25519.Dom2(d, phflag, ctx);
			d.BlockUpdate(h, 32, 32);
			d.BlockUpdate(m, mOff, mLen);
			d.DoFinal(h, 0);
			byte[] array = Ed25519.ReduceScalar(h);
			byte[] array2 = new byte[32];
			Ed25519.ScalarMultBaseEncoded(array, array2, 0);
			Ed25519.Dom2(d, phflag, ctx);
			d.BlockUpdate(array2, 0, 32);
			d.BlockUpdate(pk, pkOff, 32);
			d.BlockUpdate(m, mOff, mLen);
			d.DoFinal(h, 0);
			byte[] k = Ed25519.ReduceScalar(h);
			byte[] sourceArray = Ed25519.CalculateS(array, k, s);
			Array.Copy(array2, 0, sig, sigOff, 32);
			Array.Copy(sourceArray, 0, sig, sigOff + 32, 32);
		}

		// Token: 0x060033DE RID: 13278 RVA: 0x0010DE24 File Offset: 0x0010DE24
		private static void ImplSign(byte[] sk, int skOff, byte[] ctx, byte phflag, byte[] m, int mOff, int mLen, byte[] sig, int sigOff)
		{
			if (!Ed25519.CheckContextVar(ctx, phflag))
			{
				throw new ArgumentException("ctx");
			}
			IDigest digest = Ed25519.CreateDigest();
			byte[] array = new byte[digest.GetDigestSize()];
			digest.BlockUpdate(sk, skOff, Ed25519.SecretKeySize);
			digest.DoFinal(array, 0);
			byte[] array2 = new byte[32];
			Ed25519.PruneScalar(array, 0, array2);
			byte[] array3 = new byte[32];
			Ed25519.ScalarMultBaseEncoded(array2, array3, 0);
			Ed25519.ImplSign(digest, array, array2, array3, 0, ctx, phflag, m, mOff, mLen, sig, sigOff);
		}

		// Token: 0x060033DF RID: 13279 RVA: 0x0010DEAC File Offset: 0x0010DEAC
		private static void ImplSign(byte[] sk, int skOff, byte[] pk, int pkOff, byte[] ctx, byte phflag, byte[] m, int mOff, int mLen, byte[] sig, int sigOff)
		{
			if (!Ed25519.CheckContextVar(ctx, phflag))
			{
				throw new ArgumentException("ctx");
			}
			IDigest digest = Ed25519.CreateDigest();
			byte[] array = new byte[digest.GetDigestSize()];
			digest.BlockUpdate(sk, skOff, Ed25519.SecretKeySize);
			digest.DoFinal(array, 0);
			byte[] array2 = new byte[32];
			Ed25519.PruneScalar(array, 0, array2);
			Ed25519.ImplSign(digest, array, array2, pk, pkOff, ctx, phflag, m, mOff, mLen, sig, sigOff);
		}

		// Token: 0x060033E0 RID: 13280 RVA: 0x0010DF28 File Offset: 0x0010DF28
		private static bool ImplVerify(byte[] sig, int sigOff, byte[] pk, int pkOff, byte[] ctx, byte phflag, byte[] m, int mOff, int mLen)
		{
			if (!Ed25519.CheckContextVar(ctx, phflag))
			{
				throw new ArgumentException("ctx");
			}
			byte[] array = Arrays.CopyOfRange(sig, sigOff, sigOff + 32);
			byte[] array2 = Arrays.CopyOfRange(sig, sigOff + 32, sigOff + Ed25519.SignatureSize);
			if (!Ed25519.CheckPointVar(array))
			{
				return false;
			}
			if (!Ed25519.CheckScalarVar(array2))
			{
				return false;
			}
			Ed25519.PointExt pointExt = new Ed25519.PointExt();
			if (!Ed25519.DecodePointVar(pk, pkOff, true, pointExt))
			{
				return false;
			}
			IDigest digest = Ed25519.CreateDigest();
			byte[] array3 = new byte[digest.GetDigestSize()];
			Ed25519.Dom2(digest, phflag, ctx);
			digest.BlockUpdate(array, 0, 32);
			digest.BlockUpdate(pk, pkOff, 32);
			digest.BlockUpdate(m, mOff, mLen);
			digest.DoFinal(array3, 0);
			byte[] k = Ed25519.ReduceScalar(array3);
			uint[] array4 = new uint[8];
			Ed25519.DecodeScalar(array2, 0, array4);
			uint[] array5 = new uint[8];
			Ed25519.DecodeScalar(k, 0, array5);
			Ed25519.PointAccum pointAccum = new Ed25519.PointAccum();
			Ed25519.ScalarMultStrausVar(array4, array5, pointExt, pointAccum);
			byte[] array6 = new byte[32];
			return Ed25519.EncodePoint(pointAccum, array6, 0) != 0 && Arrays.AreEqual(array6, array);
		}

		// Token: 0x060033E1 RID: 13281 RVA: 0x0010E048 File Offset: 0x0010E048
		private static void PointAddVar(bool negate, Ed25519.PointExt p, Ed25519.PointAccum r)
		{
			int[] array = X25519Field.Create();
			int[] array2 = X25519Field.Create();
			int[] array3 = X25519Field.Create();
			int[] array4 = X25519Field.Create();
			int[] u = r.u;
			int[] array5 = X25519Field.Create();
			int[] array6 = X25519Field.Create();
			int[] v = r.v;
			int[] zm;
			int[] zp;
			int[] zm2;
			int[] array7;
			if (negate)
			{
				zm = array4;
				zp = array3;
				zm2 = array6;
				array7 = array5;
			}
			else
			{
				zm = array3;
				zp = array4;
				zm2 = array5;
				array7 = array6;
			}
			X25519Field.Apm(r.y, r.x, array2, array);
			X25519Field.Apm(p.y, p.x, zp, zm);
			X25519Field.Mul(array, array3, array);
			X25519Field.Mul(array2, array4, array2);
			X25519Field.Mul(r.u, r.v, array3);
			X25519Field.Mul(array3, p.t, array3);
			X25519Field.Mul(array3, Ed25519.C_d2, array3);
			X25519Field.Mul(r.z, p.z, array4);
			X25519Field.Add(array4, array4, array4);
			X25519Field.Apm(array2, array, v, u);
			X25519Field.Apm(array4, array3, array7, zm2);
			X25519Field.Carry(array7);
			X25519Field.Mul(u, array5, r.x);
			X25519Field.Mul(array6, v, r.y);
			X25519Field.Mul(array5, array6, r.z);
		}

		// Token: 0x060033E2 RID: 13282 RVA: 0x0010E180 File Offset: 0x0010E180
		private static void PointAddVar(bool negate, Ed25519.PointExt p, Ed25519.PointExt q, Ed25519.PointExt r)
		{
			int[] array = X25519Field.Create();
			int[] array2 = X25519Field.Create();
			int[] array3 = X25519Field.Create();
			int[] array4 = X25519Field.Create();
			int[] array5 = X25519Field.Create();
			int[] array6 = X25519Field.Create();
			int[] array7 = X25519Field.Create();
			int[] array8 = X25519Field.Create();
			int[] zm;
			int[] zp;
			int[] zm2;
			int[] array9;
			if (negate)
			{
				zm = array4;
				zp = array3;
				zm2 = array7;
				array9 = array6;
			}
			else
			{
				zm = array3;
				zp = array4;
				zm2 = array6;
				array9 = array7;
			}
			X25519Field.Apm(p.y, p.x, array2, array);
			X25519Field.Apm(q.y, q.x, zp, zm);
			X25519Field.Mul(array, array3, array);
			X25519Field.Mul(array2, array4, array2);
			X25519Field.Mul(p.t, q.t, array3);
			X25519Field.Mul(array3, Ed25519.C_d2, array3);
			X25519Field.Mul(p.z, q.z, array4);
			X25519Field.Add(array4, array4, array4);
			X25519Field.Apm(array2, array, array8, array5);
			X25519Field.Apm(array4, array3, array9, zm2);
			X25519Field.Carry(array9);
			X25519Field.Mul(array5, array6, r.x);
			X25519Field.Mul(array7, array8, r.y);
			X25519Field.Mul(array6, array7, r.z);
			X25519Field.Mul(array5, array8, r.t);
		}

		// Token: 0x060033E3 RID: 13283 RVA: 0x0010E2B8 File Offset: 0x0010E2B8
		private static void PointAddPrecomp(Ed25519.PointPrecomp p, Ed25519.PointAccum r)
		{
			int[] array = X25519Field.Create();
			int[] array2 = X25519Field.Create();
			int[] array3 = X25519Field.Create();
			int[] u = r.u;
			int[] array4 = X25519Field.Create();
			int[] array5 = X25519Field.Create();
			int[] v = r.v;
			X25519Field.Apm(r.y, r.x, array2, array);
			X25519Field.Mul(array, p.ymx_h, array);
			X25519Field.Mul(array2, p.ypx_h, array2);
			X25519Field.Mul(r.u, r.v, array3);
			X25519Field.Mul(array3, p.xyd, array3);
			X25519Field.Apm(array2, array, v, u);
			X25519Field.Apm(r.z, array3, array5, array4);
			X25519Field.Carry(array5);
			X25519Field.Mul(u, array4, r.x);
			X25519Field.Mul(array5, v, r.y);
			X25519Field.Mul(array4, array5, r.z);
		}

		// Token: 0x060033E4 RID: 13284 RVA: 0x0010E394 File Offset: 0x0010E394
		private static Ed25519.PointExt PointCopy(Ed25519.PointAccum p)
		{
			Ed25519.PointExt pointExt = new Ed25519.PointExt();
			X25519Field.Copy(p.x, 0, pointExt.x, 0);
			X25519Field.Copy(p.y, 0, pointExt.y, 0);
			X25519Field.Copy(p.z, 0, pointExt.z, 0);
			X25519Field.Mul(p.u, p.v, pointExt.t);
			return pointExt;
		}

		// Token: 0x060033E5 RID: 13285 RVA: 0x0010E3FC File Offset: 0x0010E3FC
		private static Ed25519.PointExt PointCopy(Ed25519.PointExt p)
		{
			Ed25519.PointExt pointExt = new Ed25519.PointExt();
			X25519Field.Copy(p.x, 0, pointExt.x, 0);
			X25519Field.Copy(p.y, 0, pointExt.y, 0);
			X25519Field.Copy(p.z, 0, pointExt.z, 0);
			X25519Field.Copy(p.t, 0, pointExt.t, 0);
			return pointExt;
		}

		// Token: 0x060033E6 RID: 13286 RVA: 0x0010E460 File Offset: 0x0010E460
		private static void PointDouble(Ed25519.PointAccum r)
		{
			int[] array = X25519Field.Create();
			int[] array2 = X25519Field.Create();
			int[] array3 = X25519Field.Create();
			int[] u = r.u;
			int[] array4 = X25519Field.Create();
			int[] array5 = X25519Field.Create();
			int[] v = r.v;
			X25519Field.Sqr(r.x, array);
			X25519Field.Sqr(r.y, array2);
			X25519Field.Sqr(r.z, array3);
			X25519Field.Add(array3, array3, array3);
			X25519Field.Apm(array, array2, v, array5);
			X25519Field.Add(r.x, r.y, u);
			X25519Field.Sqr(u, u);
			X25519Field.Sub(v, u, u);
			X25519Field.Add(array3, array5, array4);
			X25519Field.Carry(array4);
			X25519Field.Mul(u, array4, r.x);
			X25519Field.Mul(array5, v, r.y);
			X25519Field.Mul(array4, array5, r.z);
		}

		// Token: 0x060033E7 RID: 13287 RVA: 0x0010E538 File Offset: 0x0010E538
		private static void PointExtendXY(Ed25519.PointAccum p)
		{
			X25519Field.One(p.z);
			X25519Field.Copy(p.x, 0, p.u, 0);
			X25519Field.Copy(p.y, 0, p.v, 0);
		}

		// Token: 0x060033E8 RID: 13288 RVA: 0x0010E56C File Offset: 0x0010E56C
		private static void PointExtendXY(Ed25519.PointExt p)
		{
			X25519Field.One(p.z);
			X25519Field.Mul(p.x, p.y, p.t);
		}

		// Token: 0x060033E9 RID: 13289 RVA: 0x0010E590 File Offset: 0x0010E590
		private static void PointLookup(int block, int index, Ed25519.PointPrecomp p)
		{
			int num = block * 8 * 3 * 10;
			for (int i = 0; i < 8; i++)
			{
				int cond = (i ^ index) - 1 >> 31;
				X25519Field.CMov(cond, Ed25519.precompBase, num, p.ypx_h, 0);
				num += 10;
				X25519Field.CMov(cond, Ed25519.precompBase, num, p.ymx_h, 0);
				num += 10;
				X25519Field.CMov(cond, Ed25519.precompBase, num, p.xyd, 0);
				num += 10;
			}
		}

		// Token: 0x060033EA RID: 13290 RVA: 0x0010E60C File Offset: 0x0010E60C
		private static Ed25519.PointExt[] PointPrecompVar(Ed25519.PointExt p, int count)
		{
			Ed25519.PointExt pointExt = new Ed25519.PointExt();
			Ed25519.PointAddVar(false, p, p, pointExt);
			Ed25519.PointExt[] array = new Ed25519.PointExt[count];
			array[0] = Ed25519.PointCopy(p);
			for (int i = 1; i < count; i++)
			{
				Ed25519.PointAddVar(false, array[i - 1], pointExt, array[i] = new Ed25519.PointExt());
			}
			return array;
		}

		// Token: 0x060033EB RID: 13291 RVA: 0x0010E66C File Offset: 0x0010E66C
		private static void PointSetNeutral(Ed25519.PointAccum p)
		{
			X25519Field.Zero(p.x);
			X25519Field.One(p.y);
			X25519Field.One(p.z);
			X25519Field.Zero(p.u);
			X25519Field.One(p.v);
		}

		// Token: 0x060033EC RID: 13292 RVA: 0x0010E6A8 File Offset: 0x0010E6A8
		private static void PointSetNeutral(Ed25519.PointExt p)
		{
			X25519Field.Zero(p.x);
			X25519Field.One(p.y);
			X25519Field.One(p.z);
			X25519Field.Zero(p.t);
		}

		// Token: 0x060033ED RID: 13293 RVA: 0x0010E6D8 File Offset: 0x0010E6D8
		public static void Precompute()
		{
			lock (Ed25519.precompLock)
			{
				if (Ed25519.precompBase == null)
				{
					Ed25519.PointExt pointExt = new Ed25519.PointExt();
					X25519Field.Copy(Ed25519.B_x, 0, pointExt.x, 0);
					X25519Field.Copy(Ed25519.B_y, 0, pointExt.y, 0);
					Ed25519.PointExtendXY(pointExt);
					Ed25519.precompBaseTable = Ed25519.PointPrecompVar(pointExt, 32);
					Ed25519.PointAccum pointAccum = new Ed25519.PointAccum();
					X25519Field.Copy(Ed25519.B_x, 0, pointAccum.x, 0);
					X25519Field.Copy(Ed25519.B_y, 0, pointAccum.y, 0);
					Ed25519.PointExtendXY(pointAccum);
					Ed25519.precompBase = new int[1920];
					int num = 0;
					for (int i = 0; i < 8; i++)
					{
						Ed25519.PointExt[] array = new Ed25519.PointExt[4];
						Ed25519.PointExt pointExt2 = new Ed25519.PointExt();
						Ed25519.PointSetNeutral(pointExt2);
						for (int j = 0; j < 4; j++)
						{
							Ed25519.PointExt q = Ed25519.PointCopy(pointAccum);
							Ed25519.PointAddVar(true, pointExt2, q, pointExt2);
							Ed25519.PointDouble(pointAccum);
							array[j] = Ed25519.PointCopy(pointAccum);
							if (i + j != 10)
							{
								for (int k = 1; k < 8; k++)
								{
									Ed25519.PointDouble(pointAccum);
								}
							}
						}
						Ed25519.PointExt[] array2 = new Ed25519.PointExt[8];
						int num2 = 0;
						array2[num2++] = pointExt2;
						for (int l = 0; l < 3; l++)
						{
							int num3 = 1 << l;
							int m = 0;
							while (m < num3)
							{
								Ed25519.PointAddVar(false, array2[num2 - num3], array[l], array2[num2] = new Ed25519.PointExt());
								m++;
								num2++;
							}
						}
						for (int n = 0; n < 8; n++)
						{
							Ed25519.PointExt pointExt3 = array2[n];
							int[] array3 = X25519Field.Create();
							int[] array4 = X25519Field.Create();
							X25519Field.Add(pointExt3.z, pointExt3.z, array3);
							X25519Field.Inv(array3, array4);
							X25519Field.Mul(pointExt3.x, array4, array3);
							X25519Field.Mul(pointExt3.y, array4, array4);
							Ed25519.PointPrecomp pointPrecomp = new Ed25519.PointPrecomp();
							X25519Field.Apm(array4, array3, pointPrecomp.ypx_h, pointPrecomp.ymx_h);
							X25519Field.Mul(array3, array4, pointPrecomp.xyd);
							X25519Field.Mul(pointPrecomp.xyd, Ed25519.C_d4, pointPrecomp.xyd);
							X25519Field.Normalize(pointPrecomp.ypx_h);
							X25519Field.Normalize(pointPrecomp.ymx_h);
							X25519Field.Copy(pointPrecomp.ypx_h, 0, Ed25519.precompBase, num);
							num += 10;
							X25519Field.Copy(pointPrecomp.ymx_h, 0, Ed25519.precompBase, num);
							num += 10;
							X25519Field.Copy(pointPrecomp.xyd, 0, Ed25519.precompBase, num);
							num += 10;
						}
					}
				}
			}
		}

		// Token: 0x060033EE RID: 13294 RVA: 0x0010E9C0 File Offset: 0x0010E9C0
		private static void PruneScalar(byte[] n, int nOff, byte[] r)
		{
			Array.Copy(n, nOff, r, 0, 32);
			r[0] = (r[0] & 248);
			r[31] = (r[31] & 127);
			r[31] = (r[31] | 64);
		}

		// Token: 0x060033EF RID: 13295 RVA: 0x0010EA08 File Offset: 0x0010EA08
		private static byte[] ReduceScalar(byte[] n)
		{
			long num = (long)((ulong)Ed25519.Decode32(n, 0) & (ulong)-1);
			long num2 = (long)((ulong)((ulong)Ed25519.Decode24(n, 4) << 4) & (ulong)-1);
			long num3 = (long)((ulong)Ed25519.Decode32(n, 7) & (ulong)-1);
			long num4 = (long)((ulong)((ulong)Ed25519.Decode24(n, 11) << 4) & (ulong)-1);
			long num5 = (long)((ulong)Ed25519.Decode32(n, 14) & (ulong)-1);
			long num6 = (long)((ulong)((ulong)Ed25519.Decode24(n, 18) << 4) & (ulong)-1);
			long num7 = (long)((ulong)Ed25519.Decode32(n, 21) & (ulong)-1);
			long num8 = (long)((ulong)((ulong)Ed25519.Decode24(n, 25) << 4) & (ulong)-1);
			long num9 = (long)((ulong)Ed25519.Decode32(n, 28) & (ulong)-1);
			long num10 = (long)((ulong)((ulong)Ed25519.Decode24(n, 32) << 4) & (ulong)-1);
			long num11 = (long)((ulong)Ed25519.Decode32(n, 35) & (ulong)-1);
			long num12 = (long)((ulong)((ulong)Ed25519.Decode24(n, 39) << 4) & (ulong)-1);
			long num13 = (long)((ulong)Ed25519.Decode32(n, 42) & (ulong)-1);
			long num14 = (long)((ulong)((ulong)Ed25519.Decode24(n, 46) << 4) & (ulong)-1);
			long num15 = (long)((ulong)Ed25519.Decode32(n, 49) & (ulong)-1);
			long num16 = (long)((ulong)((ulong)Ed25519.Decode24(n, 53) << 4) & (ulong)-1);
			long num17 = (long)((ulong)Ed25519.Decode32(n, 56) & (ulong)-1);
			long num18 = (long)((ulong)((ulong)Ed25519.Decode24(n, 60) << 4) & (ulong)-1);
			long num19 = (long)((ulong)n[63] & 255UL);
			num10 -= num19 * -50998291L;
			num11 -= num19 * 19280294L;
			num12 -= num19 * 127719000L;
			num13 -= num19 * -6428113L;
			num14 -= num19 * 5343L;
			num18 += num17 >> 28;
			num17 &= 268435455L;
			num9 -= num18 * -50998291L;
			num10 -= num18 * 19280294L;
			num11 -= num18 * 127719000L;
			num12 -= num18 * -6428113L;
			num13 -= num18 * 5343L;
			num8 -= num17 * -50998291L;
			num9 -= num17 * 19280294L;
			num10 -= num17 * 127719000L;
			num11 -= num17 * -6428113L;
			num12 -= num17 * 5343L;
			num16 += num15 >> 28;
			num15 &= 268435455L;
			num7 -= num16 * -50998291L;
			num8 -= num16 * 19280294L;
			num9 -= num16 * 127719000L;
			num10 -= num16 * -6428113L;
			num11 -= num16 * 5343L;
			num6 -= num15 * -50998291L;
			num7 -= num15 * 19280294L;
			num8 -= num15 * 127719000L;
			num9 -= num15 * -6428113L;
			num10 -= num15 * 5343L;
			num14 += num13 >> 28;
			num13 &= 268435455L;
			num5 -= num14 * -50998291L;
			num6 -= num14 * 19280294L;
			num7 -= num14 * 127719000L;
			num8 -= num14 * -6428113L;
			num9 -= num14 * 5343L;
			num13 += num12 >> 28;
			num12 &= 268435455L;
			num4 -= num13 * -50998291L;
			num5 -= num13 * 19280294L;
			num6 -= num13 * 127719000L;
			num7 -= num13 * -6428113L;
			num8 -= num13 * 5343L;
			num12 += num11 >> 28;
			num11 &= 268435455L;
			num3 -= num12 * -50998291L;
			num4 -= num12 * 19280294L;
			num5 -= num12 * 127719000L;
			num6 -= num12 * -6428113L;
			num7 -= num12 * 5343L;
			num11 += num10 >> 28;
			num10 &= 268435455L;
			num2 -= num11 * -50998291L;
			num3 -= num11 * 19280294L;
			num4 -= num11 * 127719000L;
			num5 -= num11 * -6428113L;
			num6 -= num11 * 5343L;
			num9 += num8 >> 28;
			num8 &= 268435455L;
			num10 += num9 >> 28;
			num9 &= 268435455L;
			long num20 = num9 >> 27 & 1L;
			num10 += num20;
			num -= num10 * -50998291L;
			num2 -= num10 * 19280294L;
			num3 -= num10 * 127719000L;
			num4 -= num10 * -6428113L;
			num5 -= num10 * 5343L;
			num2 += num >> 28;
			num &= 268435455L;
			num3 += num2 >> 28;
			num2 &= 268435455L;
			num4 += num3 >> 28;
			num3 &= 268435455L;
			num5 += num4 >> 28;
			num4 &= 268435455L;
			num6 += num5 >> 28;
			num5 &= 268435455L;
			num7 += num6 >> 28;
			num6 &= 268435455L;
			num8 += num7 >> 28;
			num7 &= 268435455L;
			num9 += num8 >> 28;
			num8 &= 268435455L;
			num10 = num9 >> 28;
			num9 &= 268435455L;
			num10 -= num20;
			num += (num10 & -50998291L);
			num2 += (num10 & 19280294L);
			num3 += (num10 & 127719000L);
			num4 += (num10 & -6428113L);
			num5 += (num10 & 5343L);
			num2 += num >> 28;
			num &= 268435455L;
			num3 += num2 >> 28;
			num2 &= 268435455L;
			num4 += num3 >> 28;
			num3 &= 268435455L;
			num5 += num4 >> 28;
			num4 &= 268435455L;
			num6 += num5 >> 28;
			num5 &= 268435455L;
			num7 += num6 >> 28;
			num6 &= 268435455L;
			num8 += num7 >> 28;
			num7 &= 268435455L;
			num9 += num8 >> 28;
			num8 &= 268435455L;
			byte[] array = new byte[32];
			Ed25519.Encode56((ulong)(num | num2 << 28), array, 0);
			Ed25519.Encode56((ulong)(num3 | num4 << 28), array, 7);
			Ed25519.Encode56((ulong)(num5 | num6 << 28), array, 14);
			Ed25519.Encode56((ulong)(num7 | num8 << 28), array, 21);
			Ed25519.Encode32((uint)num9, array, 28);
			return array;
		}

		// Token: 0x060033F0 RID: 13296 RVA: 0x0010F068 File Offset: 0x0010F068
		private static void ScalarMultBase(byte[] k, Ed25519.PointAccum r)
		{
			Ed25519.Precompute();
			Ed25519.PointSetNeutral(r);
			uint[] array = new uint[8];
			Ed25519.DecodeScalar(k, 0, array);
			uint num = Nat.CAdd(8, (int)(~array[0] & 1U), array, Ed25519.L, array);
			uint num2 = Nat.ShiftDownBit(8, array, 1U);
			for (int i = 0; i < 8; i++)
			{
				array[i] = Interleave.Shuffle2(array[i]);
			}
			Ed25519.PointPrecomp pointPrecomp = new Ed25519.PointPrecomp();
			int num3 = 28;
			for (;;)
			{
				for (int j = 0; j < 8; j++)
				{
					uint num4 = array[j] >> num3;
					int num5 = (int)(num4 >> 3 & 1U);
					int index = (int)((num4 ^ (uint)(-(uint)num5)) & 7U);
					Ed25519.PointLookup(j, index, pointPrecomp);
					X25519Field.CSwap(num5, pointPrecomp.ypx_h, pointPrecomp.ymx_h);
					X25519Field.CNegate(num5, pointPrecomp.xyd);
					Ed25519.PointAddPrecomp(pointPrecomp, r);
				}
				if ((num3 -= 4) < 0)
				{
					break;
				}
				Ed25519.PointDouble(r);
			}
		}

		// Token: 0x060033F1 RID: 13297 RVA: 0x0010F154 File Offset: 0x0010F154
		private static void ScalarMultBaseEncoded(byte[] k, byte[] r, int rOff)
		{
			Ed25519.PointAccum pointAccum = new Ed25519.PointAccum();
			Ed25519.ScalarMultBase(k, pointAccum);
			if (Ed25519.EncodePoint(pointAccum, r, rOff) == 0)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x060033F2 RID: 13298 RVA: 0x0010F188 File Offset: 0x0010F188
		internal static void ScalarMultBaseYZ(byte[] k, int kOff, int[] y, int[] z)
		{
			byte[] array = new byte[32];
			Ed25519.PruneScalar(k, kOff, array);
			Ed25519.PointAccum pointAccum = new Ed25519.PointAccum();
			Ed25519.ScalarMultBase(array, pointAccum);
			if (Ed25519.CheckPoint(pointAccum.x, pointAccum.y, pointAccum.z) == 0)
			{
				throw new InvalidOperationException();
			}
			X25519Field.Copy(pointAccum.y, 0, y, 0);
			X25519Field.Copy(pointAccum.z, 0, z, 0);
		}

		// Token: 0x060033F3 RID: 13299 RVA: 0x0010F1F4 File Offset: 0x0010F1F4
		private static void ScalarMultStrausVar(uint[] nb, uint[] np, Ed25519.PointExt p, Ed25519.PointAccum r)
		{
			Ed25519.Precompute();
			int num = 5;
			sbyte[] wnafVar = Ed25519.GetWnafVar(nb, 7);
			sbyte[] wnafVar2 = Ed25519.GetWnafVar(np, num);
			Ed25519.PointExt[] array = Ed25519.PointPrecompVar(p, 1 << num - 2);
			Ed25519.PointSetNeutral(r);
			int num2 = 252;
			for (;;)
			{
				int num3 = (int)wnafVar[num2];
				if (num3 != 0)
				{
					int num4 = num3 >> 31;
					int num5 = (num3 ^ num4) >> 1;
					Ed25519.PointAddVar(num4 != 0, Ed25519.precompBaseTable[num5], r);
				}
				int num6 = (int)wnafVar2[num2];
				if (num6 != 0)
				{
					int num7 = num6 >> 31;
					int num8 = (num6 ^ num7) >> 1;
					Ed25519.PointAddVar(num7 != 0, array[num8], r);
				}
				if (--num2 < 0)
				{
					break;
				}
				Ed25519.PointDouble(r);
			}
		}

		// Token: 0x060033F4 RID: 13300 RVA: 0x0010F2B8 File Offset: 0x0010F2B8
		public static void Sign(byte[] sk, int skOff, byte[] m, int mOff, int mLen, byte[] sig, int sigOff)
		{
			byte[] ctx = null;
			byte phflag = 0;
			Ed25519.ImplSign(sk, skOff, ctx, phflag, m, mOff, mLen, sig, sigOff);
		}

		// Token: 0x060033F5 RID: 13301 RVA: 0x0010F2E0 File Offset: 0x0010F2E0
		public static void Sign(byte[] sk, int skOff, byte[] pk, int pkOff, byte[] m, int mOff, int mLen, byte[] sig, int sigOff)
		{
			byte[] ctx = null;
			byte phflag = 0;
			Ed25519.ImplSign(sk, skOff, pk, pkOff, ctx, phflag, m, mOff, mLen, sig, sigOff);
		}

		// Token: 0x060033F6 RID: 13302 RVA: 0x0010F30C File Offset: 0x0010F30C
		public static void Sign(byte[] sk, int skOff, byte[] ctx, byte[] m, int mOff, int mLen, byte[] sig, int sigOff)
		{
			byte phflag = 0;
			Ed25519.ImplSign(sk, skOff, ctx, phflag, m, mOff, mLen, sig, sigOff);
		}

		// Token: 0x060033F7 RID: 13303 RVA: 0x0010F334 File Offset: 0x0010F334
		public static void Sign(byte[] sk, int skOff, byte[] pk, int pkOff, byte[] ctx, byte[] m, int mOff, int mLen, byte[] sig, int sigOff)
		{
			byte phflag = 0;
			Ed25519.ImplSign(sk, skOff, pk, pkOff, ctx, phflag, m, mOff, mLen, sig, sigOff);
		}

		// Token: 0x060033F8 RID: 13304 RVA: 0x0010F360 File Offset: 0x0010F360
		public static void SignPrehash(byte[] sk, int skOff, byte[] ctx, byte[] ph, int phOff, byte[] sig, int sigOff)
		{
			byte phflag = 1;
			Ed25519.ImplSign(sk, skOff, ctx, phflag, ph, phOff, Ed25519.PrehashSize, sig, sigOff);
		}

		// Token: 0x060033F9 RID: 13305 RVA: 0x0010F388 File Offset: 0x0010F388
		public static void SignPrehash(byte[] sk, int skOff, byte[] pk, int pkOff, byte[] ctx, byte[] ph, int phOff, byte[] sig, int sigOff)
		{
			byte phflag = 1;
			Ed25519.ImplSign(sk, skOff, pk, pkOff, ctx, phflag, ph, phOff, Ed25519.PrehashSize, sig, sigOff);
		}

		// Token: 0x060033FA RID: 13306 RVA: 0x0010F3B4 File Offset: 0x0010F3B4
		public static void SignPrehash(byte[] sk, int skOff, byte[] ctx, IDigest ph, byte[] sig, int sigOff)
		{
			byte[] array = new byte[Ed25519.PrehashSize];
			if (Ed25519.PrehashSize != ph.DoFinal(array, 0))
			{
				throw new ArgumentException("ph");
			}
			byte phflag = 1;
			Ed25519.ImplSign(sk, skOff, ctx, phflag, array, 0, array.Length, sig, sigOff);
		}

		// Token: 0x060033FB RID: 13307 RVA: 0x0010F404 File Offset: 0x0010F404
		public static void SignPrehash(byte[] sk, int skOff, byte[] pk, int pkOff, byte[] ctx, IDigest ph, byte[] sig, int sigOff)
		{
			byte[] array = new byte[Ed25519.PrehashSize];
			if (Ed25519.PrehashSize != ph.DoFinal(array, 0))
			{
				throw new ArgumentException("ph");
			}
			byte phflag = 1;
			Ed25519.ImplSign(sk, skOff, pk, pkOff, ctx, phflag, array, 0, array.Length, sig, sigOff);
		}

		// Token: 0x060033FC RID: 13308 RVA: 0x0010F458 File Offset: 0x0010F458
		public static bool Verify(byte[] sig, int sigOff, byte[] pk, int pkOff, byte[] m, int mOff, int mLen)
		{
			byte[] ctx = null;
			byte phflag = 0;
			return Ed25519.ImplVerify(sig, sigOff, pk, pkOff, ctx, phflag, m, mOff, mLen);
		}

		// Token: 0x060033FD RID: 13309 RVA: 0x0010F480 File Offset: 0x0010F480
		public static bool Verify(byte[] sig, int sigOff, byte[] pk, int pkOff, byte[] ctx, byte[] m, int mOff, int mLen)
		{
			byte phflag = 0;
			return Ed25519.ImplVerify(sig, sigOff, pk, pkOff, ctx, phflag, m, mOff, mLen);
		}

		// Token: 0x060033FE RID: 13310 RVA: 0x0010F4A8 File Offset: 0x0010F4A8
		public static bool VerifyPrehash(byte[] sig, int sigOff, byte[] pk, int pkOff, byte[] ctx, byte[] ph, int phOff)
		{
			byte phflag = 1;
			return Ed25519.ImplVerify(sig, sigOff, pk, pkOff, ctx, phflag, ph, phOff, Ed25519.PrehashSize);
		}

		// Token: 0x060033FF RID: 13311 RVA: 0x0010F4D0 File Offset: 0x0010F4D0
		public static bool VerifyPrehash(byte[] sig, int sigOff, byte[] pk, int pkOff, byte[] ctx, IDigest ph)
		{
			byte[] array = new byte[Ed25519.PrehashSize];
			if (Ed25519.PrehashSize != ph.DoFinal(array, 0))
			{
				throw new ArgumentException("ph");
			}
			byte phflag = 1;
			return Ed25519.ImplVerify(sig, sigOff, pk, pkOff, ctx, phflag, array, 0, array.Length);
		}

		// Token: 0x04001CAE RID: 7342
		private const long M28L = 268435455L;

		// Token: 0x04001CAF RID: 7343
		private const long M32L = 4294967295L;

		// Token: 0x04001CB0 RID: 7344
		private const int PointBytes = 32;

		// Token: 0x04001CB1 RID: 7345
		private const int ScalarUints = 8;

		// Token: 0x04001CB2 RID: 7346
		private const int ScalarBytes = 32;

		// Token: 0x04001CB3 RID: 7347
		private const int L0 = -50998291;

		// Token: 0x04001CB4 RID: 7348
		private const int L1 = 19280294;

		// Token: 0x04001CB5 RID: 7349
		private const int L2 = 127719000;

		// Token: 0x04001CB6 RID: 7350
		private const int L3 = -6428113;

		// Token: 0x04001CB7 RID: 7351
		private const int L4 = 5343;

		// Token: 0x04001CB8 RID: 7352
		private const int WnafWidthBase = 7;

		// Token: 0x04001CB9 RID: 7353
		private const int PrecompBlocks = 8;

		// Token: 0x04001CBA RID: 7354
		private const int PrecompTeeth = 4;

		// Token: 0x04001CBB RID: 7355
		private const int PrecompSpacing = 8;

		// Token: 0x04001CBC RID: 7356
		private const int PrecompPoints = 8;

		// Token: 0x04001CBD RID: 7357
		private const int PrecompMask = 7;

		// Token: 0x04001CBE RID: 7358
		public static readonly int PrehashSize = 64;

		// Token: 0x04001CBF RID: 7359
		public static readonly int PublicKeySize = 32;

		// Token: 0x04001CC0 RID: 7360
		public static readonly int SecretKeySize = 32;

		// Token: 0x04001CC1 RID: 7361
		public static readonly int SignatureSize = 64;

		// Token: 0x04001CC2 RID: 7362
		private static readonly byte[] Dom2Prefix = Strings.ToByteArray("SigEd25519 no Ed25519 collisions");

		// Token: 0x04001CC3 RID: 7363
		private static readonly uint[] P = new uint[]
		{
			4294967277U,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			2147483647U
		};

		// Token: 0x04001CC4 RID: 7364
		private static readonly uint[] L = new uint[]
		{
			1559614445U,
			1477600026U,
			2734136534U,
			350157278U,
			0U,
			0U,
			0U,
			268435456U
		};

		// Token: 0x04001CC5 RID: 7365
		private static readonly int[] B_x = new int[]
		{
			52811034,
			25909283,
			8072341,
			50637101,
			13785486,
			30858332,
			20483199,
			20966410,
			43936626,
			4379245
		};

		// Token: 0x04001CC6 RID: 7366
		private static readonly int[] B_y = new int[]
		{
			40265304,
			26843545,
			6710886,
			53687091,
			13421772,
			40265318,
			26843545,
			6710886,
			53687091,
			13421772
		};

		// Token: 0x04001CC7 RID: 7367
		private static readonly int[] C_d = new int[]
		{
			56195235,
			47411844,
			25868126,
			40503822,
			57364,
			58321048,
			30416477,
			31930572,
			57760639,
			10749657
		};

		// Token: 0x04001CC8 RID: 7368
		private static readonly int[] C_d2 = new int[]
		{
			45281625,
			27714825,
			18181821,
			13898781,
			114729,
			49533232,
			60832955,
			30306712,
			48412415,
			4722099
		};

		// Token: 0x04001CC9 RID: 7369
		private static readonly int[] C_d4 = new int[]
		{
			23454386,
			55429651,
			2809210,
			27797563,
			229458,
			31957600,
			54557047,
			27058993,
			29715967,
			9444199
		};

		// Token: 0x04001CCA RID: 7370
		private static readonly object precompLock = new object();

		// Token: 0x04001CCB RID: 7371
		private static Ed25519.PointExt[] precompBaseTable = null;

		// Token: 0x04001CCC RID: 7372
		private static int[] precompBase = null;

		// Token: 0x02000E51 RID: 3665
		public enum Algorithm
		{
			// Token: 0x0400420E RID: 16910
			Ed25519,
			// Token: 0x0400420F RID: 16911
			Ed25519ctx,
			// Token: 0x04004210 RID: 16912
			Ed25519ph
		}

		// Token: 0x02000E52 RID: 3666
		private class PointAccum
		{
			// Token: 0x04004211 RID: 16913
			internal int[] x = X25519Field.Create();

			// Token: 0x04004212 RID: 16914
			internal int[] y = X25519Field.Create();

			// Token: 0x04004213 RID: 16915
			internal int[] z = X25519Field.Create();

			// Token: 0x04004214 RID: 16916
			internal int[] u = X25519Field.Create();

			// Token: 0x04004215 RID: 16917
			internal int[] v = X25519Field.Create();
		}

		// Token: 0x02000E53 RID: 3667
		private class PointExt
		{
			// Token: 0x04004216 RID: 16918
			internal int[] x = X25519Field.Create();

			// Token: 0x04004217 RID: 16919
			internal int[] y = X25519Field.Create();

			// Token: 0x04004218 RID: 16920
			internal int[] z = X25519Field.Create();

			// Token: 0x04004219 RID: 16921
			internal int[] t = X25519Field.Create();
		}

		// Token: 0x02000E54 RID: 3668
		private class PointPrecomp
		{
			// Token: 0x0400421A RID: 16922
			internal int[] ypx_h = X25519Field.Create();

			// Token: 0x0400421B RID: 16923
			internal int[] ymx_h = X25519Field.Create();

			// Token: 0x0400421C RID: 16924
			internal int[] xyd = X25519Field.Create();
		}
	}
}
