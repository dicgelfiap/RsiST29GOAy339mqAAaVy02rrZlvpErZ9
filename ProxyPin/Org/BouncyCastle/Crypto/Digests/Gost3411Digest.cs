using System;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Utilities;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Digests
{
	// Token: 0x02000352 RID: 850
	public class Gost3411Digest : IDigest, IMemoable
	{
		// Token: 0x0600196F RID: 6511 RVA: 0x00083B70 File Offset: 0x00083B70
		private static byte[][] MakeC()
		{
			byte[][] array = new byte[4][];
			for (int i = 0; i < 4; i++)
			{
				array[i] = new byte[32];
			}
			return array;
		}

		// Token: 0x06001970 RID: 6512 RVA: 0x00083BA8 File Offset: 0x00083BA8
		public Gost3411Digest()
		{
			this.sBox = Gost28147Engine.GetSBox("D-A");
			this.cipher.Init(true, new ParametersWithSBox(null, this.sBox));
			this.Reset();
		}

		// Token: 0x06001971 RID: 6513 RVA: 0x00083CAC File Offset: 0x00083CAC
		public Gost3411Digest(byte[] sBoxParam)
		{
			this.sBox = Arrays.Clone(sBoxParam);
			this.cipher.Init(true, new ParametersWithSBox(null, this.sBox));
			this.Reset();
		}

		// Token: 0x06001972 RID: 6514 RVA: 0x00083DAC File Offset: 0x00083DAC
		public Gost3411Digest(Gost3411Digest t)
		{
			this.Reset(t);
		}

		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x06001973 RID: 6515 RVA: 0x00083E88 File Offset: 0x00083E88
		public string AlgorithmName
		{
			get
			{
				return "Gost3411";
			}
		}

		// Token: 0x06001974 RID: 6516 RVA: 0x00083E90 File Offset: 0x00083E90
		public int GetDigestSize()
		{
			return 32;
		}

		// Token: 0x06001975 RID: 6517 RVA: 0x00083E94 File Offset: 0x00083E94
		public void Update(byte input)
		{
			this.xBuf[this.xBufOff++] = input;
			if (this.xBufOff == this.xBuf.Length)
			{
				this.sumByteArray(this.xBuf);
				this.processBlock(this.xBuf, 0);
				this.xBufOff = 0;
			}
			this.byteCount += 1UL;
		}

		// Token: 0x06001976 RID: 6518 RVA: 0x00083F00 File Offset: 0x00083F00
		public void BlockUpdate(byte[] input, int inOff, int length)
		{
			while (this.xBufOff != 0)
			{
				if (length <= 0)
				{
					break;
				}
				this.Update(input[inOff]);
				inOff++;
				length--;
			}
			while (length > this.xBuf.Length)
			{
				Array.Copy(input, inOff, this.xBuf, 0, this.xBuf.Length);
				this.sumByteArray(this.xBuf);
				this.processBlock(this.xBuf, 0);
				inOff += this.xBuf.Length;
				length -= this.xBuf.Length;
				this.byteCount += (ulong)this.xBuf.Length;
			}
			while (length > 0)
			{
				this.Update(input[inOff]);
				inOff++;
				length--;
			}
		}

		// Token: 0x06001977 RID: 6519 RVA: 0x00083FC4 File Offset: 0x00083FC4
		private byte[] P(byte[] input)
		{
			int num = 0;
			for (int i = 0; i < 8; i++)
			{
				this.K[num++] = input[i];
				this.K[num++] = input[8 + i];
				this.K[num++] = input[16 + i];
				this.K[num++] = input[24 + i];
			}
			return this.K;
		}

		// Token: 0x06001978 RID: 6520 RVA: 0x00084030 File Offset: 0x00084030
		private byte[] A(byte[] input)
		{
			for (int i = 0; i < 8; i++)
			{
				this.a[i] = (input[i] ^ input[i + 8]);
			}
			Array.Copy(input, 8, input, 0, 24);
			Array.Copy(this.a, 0, input, 24, 8);
			return input;
		}

		// Token: 0x06001979 RID: 6521 RVA: 0x00084080 File Offset: 0x00084080
		private void E(byte[] key, byte[] s, int sOff, byte[] input, int inOff)
		{
			this.cipher.Init(true, new KeyParameter(key));
			this.cipher.ProcessBlock(input, inOff, s, sOff);
		}

		// Token: 0x0600197A RID: 6522 RVA: 0x000840A8 File Offset: 0x000840A8
		private void fw(byte[] input)
		{
			Gost3411Digest.cpyBytesToShort(input, this.wS);
			this.w_S[15] = (this.wS[0] ^ this.wS[1] ^ this.wS[2] ^ this.wS[3] ^ this.wS[12] ^ this.wS[15]);
			Array.Copy(this.wS, 1, this.w_S, 0, 15);
			Gost3411Digest.cpyShortToBytes(this.w_S, input);
		}

		// Token: 0x0600197B RID: 6523 RVA: 0x00084128 File Offset: 0x00084128
		private void processBlock(byte[] input, int inOff)
		{
			Array.Copy(input, inOff, this.M, 0, 32);
			this.H.CopyTo(this.U, 0);
			this.M.CopyTo(this.V, 0);
			for (int i = 0; i < 32; i++)
			{
				this.W[i] = (this.U[i] ^ this.V[i]);
			}
			this.E(this.P(this.W), this.S, 0, this.H, 0);
			for (int j = 1; j < 4; j++)
			{
				byte[] array = this.A(this.U);
				for (int k = 0; k < 32; k++)
				{
					this.U[k] = (array[k] ^ this.C[j][k]);
				}
				this.V = this.A(this.A(this.V));
				for (int l = 0; l < 32; l++)
				{
					this.W[l] = (this.U[l] ^ this.V[l]);
				}
				this.E(this.P(this.W), this.S, j * 8, this.H, j * 8);
			}
			for (int m = 0; m < 12; m++)
			{
				this.fw(this.S);
			}
			for (int n = 0; n < 32; n++)
			{
				this.S[n] = (this.S[n] ^ this.M[n]);
			}
			this.fw(this.S);
			for (int num = 0; num < 32; num++)
			{
				this.S[num] = (this.H[num] ^ this.S[num]);
			}
			for (int num2 = 0; num2 < 61; num2++)
			{
				this.fw(this.S);
			}
			Array.Copy(this.S, 0, this.H, 0, this.H.Length);
		}

		// Token: 0x0600197C RID: 6524 RVA: 0x00084338 File Offset: 0x00084338
		private void finish()
		{
			ulong n = this.byteCount * 8UL;
			Pack.UInt64_To_LE(n, this.L);
			while (this.xBufOff != 0)
			{
				this.Update(0);
			}
			this.processBlock(this.L, 0);
			this.processBlock(this.Sum, 0);
		}

		// Token: 0x0600197D RID: 6525 RVA: 0x00084390 File Offset: 0x00084390
		public int DoFinal(byte[] output, int outOff)
		{
			this.finish();
			this.H.CopyTo(output, outOff);
			this.Reset();
			return 32;
		}

		// Token: 0x0600197E RID: 6526 RVA: 0x000843B0 File Offset: 0x000843B0
		public void Reset()
		{
			this.byteCount = 0UL;
			this.xBufOff = 0;
			Array.Clear(this.H, 0, this.H.Length);
			Array.Clear(this.L, 0, this.L.Length);
			Array.Clear(this.M, 0, this.M.Length);
			Array.Clear(this.C[1], 0, this.C[1].Length);
			Array.Clear(this.C[3], 0, this.C[3].Length);
			Array.Clear(this.Sum, 0, this.Sum.Length);
			Array.Clear(this.xBuf, 0, this.xBuf.Length);
			Gost3411Digest.C2.CopyTo(this.C[2], 0);
		}

		// Token: 0x0600197F RID: 6527 RVA: 0x0008448C File Offset: 0x0008448C
		private void sumByteArray(byte[] input)
		{
			int num = 0;
			for (int num2 = 0; num2 != this.Sum.Length; num2++)
			{
				int num3 = (int)((this.Sum[num2] & byte.MaxValue) + (input[num2] & byte.MaxValue)) + num;
				this.Sum[num2] = (byte)num3;
				num = num3 >> 8;
			}
		}

		// Token: 0x06001980 RID: 6528 RVA: 0x000844E0 File Offset: 0x000844E0
		private static void cpyBytesToShort(byte[] S, short[] wS)
		{
			for (int i = 0; i < S.Length / 2; i++)
			{
				wS[i] = (short)(((int)S[i * 2 + 1] << 8 & 65280) | (int)(S[i * 2] & byte.MaxValue));
			}
		}

		// Token: 0x06001981 RID: 6529 RVA: 0x00084524 File Offset: 0x00084524
		private static void cpyShortToBytes(short[] wS, byte[] S)
		{
			for (int i = 0; i < S.Length / 2; i++)
			{
				S[i * 2 + 1] = (byte)(wS[i] >> 8);
				S[i * 2] = (byte)wS[i];
			}
		}

		// Token: 0x06001982 RID: 6530 RVA: 0x00084560 File Offset: 0x00084560
		public int GetByteLength()
		{
			return 32;
		}

		// Token: 0x06001983 RID: 6531 RVA: 0x00084564 File Offset: 0x00084564
		public IMemoable Copy()
		{
			return new Gost3411Digest(this);
		}

		// Token: 0x06001984 RID: 6532 RVA: 0x0008456C File Offset: 0x0008456C
		public void Reset(IMemoable other)
		{
			Gost3411Digest gost3411Digest = (Gost3411Digest)other;
			this.sBox = gost3411Digest.sBox;
			this.cipher.Init(true, new ParametersWithSBox(null, this.sBox));
			this.Reset();
			Array.Copy(gost3411Digest.H, 0, this.H, 0, gost3411Digest.H.Length);
			Array.Copy(gost3411Digest.L, 0, this.L, 0, gost3411Digest.L.Length);
			Array.Copy(gost3411Digest.M, 0, this.M, 0, gost3411Digest.M.Length);
			Array.Copy(gost3411Digest.Sum, 0, this.Sum, 0, gost3411Digest.Sum.Length);
			Array.Copy(gost3411Digest.C[1], 0, this.C[1], 0, gost3411Digest.C[1].Length);
			Array.Copy(gost3411Digest.C[2], 0, this.C[2], 0, gost3411Digest.C[2].Length);
			Array.Copy(gost3411Digest.C[3], 0, this.C[3], 0, gost3411Digest.C[3].Length);
			Array.Copy(gost3411Digest.xBuf, 0, this.xBuf, 0, gost3411Digest.xBuf.Length);
			this.xBufOff = gost3411Digest.xBufOff;
			this.byteCount = gost3411Digest.byteCount;
		}

		// Token: 0x0400110F RID: 4367
		private const int DIGEST_LENGTH = 32;

		// Token: 0x04001110 RID: 4368
		private byte[] H = new byte[32];

		// Token: 0x04001111 RID: 4369
		private byte[] L = new byte[32];

		// Token: 0x04001112 RID: 4370
		private byte[] M = new byte[32];

		// Token: 0x04001113 RID: 4371
		private byte[] Sum = new byte[32];

		// Token: 0x04001114 RID: 4372
		private byte[][] C = Gost3411Digest.MakeC();

		// Token: 0x04001115 RID: 4373
		private byte[] xBuf = new byte[32];

		// Token: 0x04001116 RID: 4374
		private int xBufOff;

		// Token: 0x04001117 RID: 4375
		private ulong byteCount;

		// Token: 0x04001118 RID: 4376
		private readonly IBlockCipher cipher = new Gost28147Engine();

		// Token: 0x04001119 RID: 4377
		private byte[] sBox;

		// Token: 0x0400111A RID: 4378
		private byte[] K = new byte[32];

		// Token: 0x0400111B RID: 4379
		private byte[] a = new byte[8];

		// Token: 0x0400111C RID: 4380
		internal short[] wS = new short[16];

		// Token: 0x0400111D RID: 4381
		internal short[] w_S = new short[16];

		// Token: 0x0400111E RID: 4382
		internal byte[] S = new byte[32];

		// Token: 0x0400111F RID: 4383
		internal byte[] U = new byte[32];

		// Token: 0x04001120 RID: 4384
		internal byte[] V = new byte[32];

		// Token: 0x04001121 RID: 4385
		internal byte[] W = new byte[32];

		// Token: 0x04001122 RID: 4386
		private static readonly byte[] C2 = new byte[]
		{
			0,
			byte.MaxValue,
			0,
			byte.MaxValue,
			0,
			byte.MaxValue,
			0,
			byte.MaxValue,
			byte.MaxValue,
			0,
			byte.MaxValue,
			0,
			byte.MaxValue,
			0,
			byte.MaxValue,
			0,
			0,
			byte.MaxValue,
			byte.MaxValue,
			0,
			byte.MaxValue,
			0,
			0,
			byte.MaxValue,
			byte.MaxValue,
			0,
			0,
			0,
			byte.MaxValue,
			byte.MaxValue,
			0,
			byte.MaxValue
		};
	}
}
