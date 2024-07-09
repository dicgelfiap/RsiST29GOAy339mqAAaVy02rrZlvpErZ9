using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Digests
{
	// Token: 0x0200036F RID: 879
	public sealed class WhirlpoolDigest : IDigest, IMemoable
	{
		// Token: 0x06001B28 RID: 6952 RVA: 0x00092D10 File Offset: 0x00092D10
		static WhirlpoolDigest()
		{
			WhirlpoolDigest.EIGHT[31] = 8;
			for (int i = 0; i < 256; i++)
			{
				int num = WhirlpoolDigest.SBOX[i];
				int num2 = WhirlpoolDigest.maskWithReductionPolynomial(num << 1);
				int num3 = WhirlpoolDigest.maskWithReductionPolynomial(num2 << 1);
				int num4 = num3 ^ num;
				int num5 = WhirlpoolDigest.maskWithReductionPolynomial(num3 << 1);
				int num6 = num5 ^ num;
				WhirlpoolDigest.C0[i] = WhirlpoolDigest.packIntoLong(num, num, num3, num, num5, num4, num2, num6);
				WhirlpoolDigest.C1[i] = WhirlpoolDigest.packIntoLong(num6, num, num, num3, num, num5, num4, num2);
				WhirlpoolDigest.C2[i] = WhirlpoolDigest.packIntoLong(num2, num6, num, num, num3, num, num5, num4);
				WhirlpoolDigest.C3[i] = WhirlpoolDigest.packIntoLong(num4, num2, num6, num, num, num3, num, num5);
				WhirlpoolDigest.C4[i] = WhirlpoolDigest.packIntoLong(num5, num4, num2, num6, num, num, num3, num);
				WhirlpoolDigest.C5[i] = WhirlpoolDigest.packIntoLong(num, num5, num4, num2, num6, num, num, num3);
				WhirlpoolDigest.C6[i] = WhirlpoolDigest.packIntoLong(num3, num, num5, num4, num2, num6, num, num);
				WhirlpoolDigest.C7[i] = WhirlpoolDigest.packIntoLong(num, num3, num, num5, num4, num2, num6, num);
			}
		}

		// Token: 0x06001B29 RID: 6953 RVA: 0x00092EC8 File Offset: 0x00092EC8
		public WhirlpoolDigest()
		{
			this._rc[0] = 0L;
			for (int i = 1; i <= 10; i++)
			{
				int num = 8 * (i - 1);
				this._rc[i] = ((WhirlpoolDigest.C0[num] & -72057594037927936L) ^ (WhirlpoolDigest.C1[num + 1] & 71776119061217280L) ^ (WhirlpoolDigest.C2[num + 2] & 280375465082880L) ^ (WhirlpoolDigest.C3[num + 3] & 1095216660480L) ^ (WhirlpoolDigest.C4[num + 4] & (long)((ulong)-16777216)) ^ (WhirlpoolDigest.C5[num + 5] & 16711680L) ^ (WhirlpoolDigest.C6[num + 6] & 65280L) ^ (WhirlpoolDigest.C7[num + 7] & 255L));
			}
		}

		// Token: 0x06001B2A RID: 6954 RVA: 0x00093000 File Offset: 0x00093000
		private static long packIntoLong(int b7, int b6, int b5, int b4, int b3, int b2, int b1, int b0)
		{
			return (long)b7 << 56 ^ (long)b6 << 48 ^ (long)b5 << 40 ^ (long)b4 << 32 ^ (long)b3 << 24 ^ (long)b2 << 16 ^ (long)b1 << 8 ^ (long)b0;
		}

		// Token: 0x06001B2B RID: 6955 RVA: 0x00093034 File Offset: 0x00093034
		private static int maskWithReductionPolynomial(int input)
		{
			int num = input;
			if ((long)num >= 256L)
			{
				num ^= 285;
			}
			return num;
		}

		// Token: 0x06001B2C RID: 6956 RVA: 0x00093060 File Offset: 0x00093060
		public WhirlpoolDigest(WhirlpoolDigest originalDigest)
		{
			this.Reset(originalDigest);
		}

		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x06001B2D RID: 6957 RVA: 0x000930E4 File Offset: 0x000930E4
		public string AlgorithmName
		{
			get
			{
				return "Whirlpool";
			}
		}

		// Token: 0x06001B2E RID: 6958 RVA: 0x000930EC File Offset: 0x000930EC
		public int GetDigestSize()
		{
			return 64;
		}

		// Token: 0x06001B2F RID: 6959 RVA: 0x000930F0 File Offset: 0x000930F0
		public int DoFinal(byte[] output, int outOff)
		{
			this.finish();
			for (int i = 0; i < 8; i++)
			{
				WhirlpoolDigest.convertLongToByteArray(this._hash[i], output, outOff + i * 8);
			}
			this.Reset();
			return this.GetDigestSize();
		}

		// Token: 0x06001B30 RID: 6960 RVA: 0x00093138 File Offset: 0x00093138
		public void Reset()
		{
			this._bufferPos = 0;
			Array.Clear(this._bitCount, 0, this._bitCount.Length);
			Array.Clear(this._buffer, 0, this._buffer.Length);
			Array.Clear(this._hash, 0, this._hash.Length);
			Array.Clear(this._K, 0, this._K.Length);
			Array.Clear(this._L, 0, this._L.Length);
			Array.Clear(this._block, 0, this._block.Length);
			Array.Clear(this._state, 0, this._state.Length);
		}

		// Token: 0x06001B31 RID: 6961 RVA: 0x000931DC File Offset: 0x000931DC
		private void processFilledBuffer()
		{
			for (int i = 0; i < this._state.Length; i++)
			{
				this._block[i] = WhirlpoolDigest.bytesToLongFromBuffer(this._buffer, i * 8);
			}
			this.processBlock();
			this._bufferPos = 0;
			Array.Clear(this._buffer, 0, this._buffer.Length);
		}

		// Token: 0x06001B32 RID: 6962 RVA: 0x0009323C File Offset: 0x0009323C
		private static long bytesToLongFromBuffer(byte[] buffer, int startPos)
		{
			return (long)(((ulong)buffer[startPos] & 255UL) << 56 | ((ulong)buffer[startPos + 1] & 255UL) << 48 | ((ulong)buffer[startPos + 2] & 255UL) << 40 | ((ulong)buffer[startPos + 3] & 255UL) << 32 | ((ulong)buffer[startPos + 4] & 255UL) << 24 | ((ulong)buffer[startPos + 5] & 255UL) << 16 | ((ulong)buffer[startPos + 6] & 255UL) << 8 | ((ulong)buffer[startPos + 7] & 255UL));
		}

		// Token: 0x06001B33 RID: 6963 RVA: 0x000932D0 File Offset: 0x000932D0
		private static void convertLongToByteArray(long inputLong, byte[] outputArray, int offSet)
		{
			for (int i = 0; i < 8; i++)
			{
				outputArray[offSet + i] = (byte)(inputLong >> 56 - i * 8 & 255L);
			}
		}

		// Token: 0x06001B34 RID: 6964 RVA: 0x00093308 File Offset: 0x00093308
		private void processBlock()
		{
			for (int i = 0; i < 8; i++)
			{
				this._state[i] = (this._block[i] ^ (this._K[i] = this._hash[i]));
			}
			for (int j = 1; j <= 10; j++)
			{
				long[] array;
				for (int k = 0; k < 8; k++)
				{
					this._L[k] = 0L;
					IntPtr intPtr;
					(array = this._L)[(int)(intPtr = (IntPtr)k)] = (array[(int)intPtr] ^ WhirlpoolDigest.C0[(int)(this._K[k & 7] >> 56) & 255]);
					(array = this._L)[(int)(intPtr = (IntPtr)k)] = (array[(int)intPtr] ^ WhirlpoolDigest.C1[(int)(this._K[k - 1 & 7] >> 48) & 255]);
					(array = this._L)[(int)(intPtr = (IntPtr)k)] = (array[(int)intPtr] ^ WhirlpoolDigest.C2[(int)(this._K[k - 2 & 7] >> 40) & 255]);
					(array = this._L)[(int)(intPtr = (IntPtr)k)] = (array[(int)intPtr] ^ WhirlpoolDigest.C3[(int)(this._K[k - 3 & 7] >> 32) & 255]);
					(array = this._L)[(int)(intPtr = (IntPtr)k)] = (array[(int)intPtr] ^ WhirlpoolDigest.C4[(int)(this._K[k - 4 & 7] >> 24) & 255]);
					(array = this._L)[(int)(intPtr = (IntPtr)k)] = (array[(int)intPtr] ^ WhirlpoolDigest.C5[(int)(this._K[k - 5 & 7] >> 16) & 255]);
					(array = this._L)[(int)(intPtr = (IntPtr)k)] = (array[(int)intPtr] ^ WhirlpoolDigest.C6[(int)(this._K[k - 6 & 7] >> 8) & 255]);
					(array = this._L)[(int)(intPtr = (IntPtr)k)] = (array[(int)intPtr] ^ WhirlpoolDigest.C7[(int)this._K[k - 7 & 7] & 255]);
				}
				Array.Copy(this._L, 0, this._K, 0, this._K.Length);
				(array = this._K)[0] = (array[0] ^ this._rc[j]);
				for (int l = 0; l < 8; l++)
				{
					this._L[l] = this._K[l];
					IntPtr intPtr;
					(array = this._L)[(int)(intPtr = (IntPtr)l)] = (array[(int)intPtr] ^ WhirlpoolDigest.C0[(int)(this._state[l & 7] >> 56) & 255]);
					(array = this._L)[(int)(intPtr = (IntPtr)l)] = (array[(int)intPtr] ^ WhirlpoolDigest.C1[(int)(this._state[l - 1 & 7] >> 48) & 255]);
					(array = this._L)[(int)(intPtr = (IntPtr)l)] = (array[(int)intPtr] ^ WhirlpoolDigest.C2[(int)(this._state[l - 2 & 7] >> 40) & 255]);
					(array = this._L)[(int)(intPtr = (IntPtr)l)] = (array[(int)intPtr] ^ WhirlpoolDigest.C3[(int)(this._state[l - 3 & 7] >> 32) & 255]);
					(array = this._L)[(int)(intPtr = (IntPtr)l)] = (array[(int)intPtr] ^ WhirlpoolDigest.C4[(int)(this._state[l - 4 & 7] >> 24) & 255]);
					(array = this._L)[(int)(intPtr = (IntPtr)l)] = (array[(int)intPtr] ^ WhirlpoolDigest.C5[(int)(this._state[l - 5 & 7] >> 16) & 255]);
					(array = this._L)[(int)(intPtr = (IntPtr)l)] = (array[(int)intPtr] ^ WhirlpoolDigest.C6[(int)(this._state[l - 6 & 7] >> 8) & 255]);
					(array = this._L)[(int)(intPtr = (IntPtr)l)] = (array[(int)intPtr] ^ WhirlpoolDigest.C7[(int)this._state[l - 7 & 7] & 255]);
				}
				Array.Copy(this._L, 0, this._state, 0, this._state.Length);
			}
			for (int m = 0; m < 8; m++)
			{
				long[] array;
				IntPtr intPtr;
				(array = this._hash)[(int)(intPtr = (IntPtr)m)] = (array[(int)intPtr] ^ (this._state[m] ^ this._block[m]));
			}
		}

		// Token: 0x06001B35 RID: 6965 RVA: 0x00093730 File Offset: 0x00093730
		public void Update(byte input)
		{
			this._buffer[this._bufferPos] = input;
			this._bufferPos++;
			if (this._bufferPos == this._buffer.Length)
			{
				this.processFilledBuffer();
			}
			this.increment();
		}

		// Token: 0x06001B36 RID: 6966 RVA: 0x00093770 File Offset: 0x00093770
		private void increment()
		{
			int num = 0;
			for (int i = this._bitCount.Length - 1; i >= 0; i--)
			{
				int num2 = (int)((this._bitCount[i] & 255) + WhirlpoolDigest.EIGHT[i]) + num;
				num = num2 >> 8;
				this._bitCount[i] = (short)(num2 & 255);
			}
		}

		// Token: 0x06001B37 RID: 6967 RVA: 0x000937C8 File Offset: 0x000937C8
		public void BlockUpdate(byte[] input, int inOff, int length)
		{
			while (length > 0)
			{
				this.Update(input[inOff]);
				inOff++;
				length--;
			}
		}

		// Token: 0x06001B38 RID: 6968 RVA: 0x000937E8 File Offset: 0x000937E8
		private void finish()
		{
			byte[] array = this.copyBitLength();
			byte[] buffer;
			IntPtr intPtr;
			(buffer = this._buffer)[(int)(intPtr = (IntPtr)(this._bufferPos++))] = (buffer[(int)intPtr] | 128);
			if (this._bufferPos == this._buffer.Length)
			{
				this.processFilledBuffer();
			}
			if (this._bufferPos > 32)
			{
				while (this._bufferPos != 0)
				{
					this.Update(0);
				}
			}
			while (this._bufferPos <= 32)
			{
				this.Update(0);
			}
			Array.Copy(array, 0, this._buffer, 32, array.Length);
			this.processFilledBuffer();
		}

		// Token: 0x06001B39 RID: 6969 RVA: 0x00093890 File Offset: 0x00093890
		private byte[] copyBitLength()
		{
			byte[] array = new byte[32];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = (byte)(this._bitCount[i] & 255);
			}
			return array;
		}

		// Token: 0x06001B3A RID: 6970 RVA: 0x000938D0 File Offset: 0x000938D0
		public int GetByteLength()
		{
			return 64;
		}

		// Token: 0x06001B3B RID: 6971 RVA: 0x000938D4 File Offset: 0x000938D4
		public IMemoable Copy()
		{
			return new WhirlpoolDigest(this);
		}

		// Token: 0x06001B3C RID: 6972 RVA: 0x000938DC File Offset: 0x000938DC
		public void Reset(IMemoable other)
		{
			WhirlpoolDigest whirlpoolDigest = (WhirlpoolDigest)other;
			Array.Copy(whirlpoolDigest._rc, 0, this._rc, 0, this._rc.Length);
			Array.Copy(whirlpoolDigest._buffer, 0, this._buffer, 0, this._buffer.Length);
			this._bufferPos = whirlpoolDigest._bufferPos;
			Array.Copy(whirlpoolDigest._bitCount, 0, this._bitCount, 0, this._bitCount.Length);
			Array.Copy(whirlpoolDigest._hash, 0, this._hash, 0, this._hash.Length);
			Array.Copy(whirlpoolDigest._K, 0, this._K, 0, this._K.Length);
			Array.Copy(whirlpoolDigest._L, 0, this._L, 0, this._L.Length);
			Array.Copy(whirlpoolDigest._block, 0, this._block, 0, this._block.Length);
			Array.Copy(whirlpoolDigest._state, 0, this._state, 0, this._state.Length);
		}

		// Token: 0x04001200 RID: 4608
		private const int BYTE_LENGTH = 64;

		// Token: 0x04001201 RID: 4609
		private const int DIGEST_LENGTH_BYTES = 64;

		// Token: 0x04001202 RID: 4610
		private const int ROUNDS = 10;

		// Token: 0x04001203 RID: 4611
		private const int REDUCTION_POLYNOMIAL = 285;

		// Token: 0x04001204 RID: 4612
		private const int BITCOUNT_ARRAY_SIZE = 32;

		// Token: 0x04001205 RID: 4613
		private static readonly int[] SBOX = new int[]
		{
			24,
			35,
			198,
			232,
			135,
			184,
			1,
			79,
			54,
			166,
			210,
			245,
			121,
			111,
			145,
			82,
			96,
			188,
			155,
			142,
			163,
			12,
			123,
			53,
			29,
			224,
			215,
			194,
			46,
			75,
			254,
			87,
			21,
			119,
			55,
			229,
			159,
			240,
			74,
			218,
			88,
			201,
			41,
			10,
			177,
			160,
			107,
			133,
			189,
			93,
			16,
			244,
			203,
			62,
			5,
			103,
			228,
			39,
			65,
			139,
			167,
			125,
			149,
			216,
			251,
			238,
			124,
			102,
			221,
			23,
			71,
			158,
			202,
			45,
			191,
			7,
			173,
			90,
			131,
			51,
			99,
			2,
			170,
			113,
			200,
			25,
			73,
			217,
			242,
			227,
			91,
			136,
			154,
			38,
			50,
			176,
			233,
			15,
			213,
			128,
			190,
			205,
			52,
			72,
			255,
			122,
			144,
			95,
			32,
			104,
			26,
			174,
			180,
			84,
			147,
			34,
			100,
			241,
			115,
			18,
			64,
			8,
			195,
			236,
			219,
			161,
			141,
			61,
			151,
			0,
			207,
			43,
			118,
			130,
			214,
			27,
			181,
			175,
			106,
			80,
			69,
			243,
			48,
			239,
			63,
			85,
			162,
			234,
			101,
			186,
			47,
			192,
			222,
			28,
			253,
			77,
			146,
			117,
			6,
			138,
			178,
			230,
			14,
			31,
			98,
			212,
			168,
			150,
			249,
			197,
			37,
			89,
			132,
			114,
			57,
			76,
			94,
			120,
			56,
			140,
			209,
			165,
			226,
			97,
			179,
			33,
			156,
			30,
			67,
			199,
			252,
			4,
			81,
			153,
			109,
			13,
			250,
			223,
			126,
			36,
			59,
			171,
			206,
			17,
			143,
			78,
			183,
			235,
			60,
			129,
			148,
			247,
			185,
			19,
			44,
			211,
			231,
			110,
			196,
			3,
			86,
			68,
			127,
			169,
			42,
			187,
			193,
			83,
			220,
			11,
			157,
			108,
			49,
			116,
			246,
			70,
			172,
			137,
			20,
			225,
			22,
			58,
			105,
			9,
			112,
			182,
			208,
			237,
			204,
			66,
			152,
			164,
			40,
			92,
			248,
			134
		};

		// Token: 0x04001206 RID: 4614
		private static readonly long[] C0 = new long[256];

		// Token: 0x04001207 RID: 4615
		private static readonly long[] C1 = new long[256];

		// Token: 0x04001208 RID: 4616
		private static readonly long[] C2 = new long[256];

		// Token: 0x04001209 RID: 4617
		private static readonly long[] C3 = new long[256];

		// Token: 0x0400120A RID: 4618
		private static readonly long[] C4 = new long[256];

		// Token: 0x0400120B RID: 4619
		private static readonly long[] C5 = new long[256];

		// Token: 0x0400120C RID: 4620
		private static readonly long[] C6 = new long[256];

		// Token: 0x0400120D RID: 4621
		private static readonly long[] C7 = new long[256];

		// Token: 0x0400120E RID: 4622
		private readonly long[] _rc = new long[11];

		// Token: 0x0400120F RID: 4623
		private static readonly short[] EIGHT = new short[32];

		// Token: 0x04001210 RID: 4624
		private byte[] _buffer = new byte[64];

		// Token: 0x04001211 RID: 4625
		private int _bufferPos;

		// Token: 0x04001212 RID: 4626
		private short[] _bitCount = new short[32];

		// Token: 0x04001213 RID: 4627
		private long[] _hash = new long[8];

		// Token: 0x04001214 RID: 4628
		private long[] _K = new long[8];

		// Token: 0x04001215 RID: 4629
		private long[] _L = new long[8];

		// Token: 0x04001216 RID: 4630
		private long[] _block = new long[8];

		// Token: 0x04001217 RID: 4631
		private long[] _state = new long[8];
	}
}
