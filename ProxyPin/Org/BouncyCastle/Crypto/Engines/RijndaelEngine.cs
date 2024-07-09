﻿using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Engines
{
	// Token: 0x0200039C RID: 924
	public class RijndaelEngine : IBlockCipher
	{
		// Token: 0x06001D43 RID: 7491 RVA: 0x000A6C9C File Offset: 0x000A6C9C
		private byte Mul0x2(int b)
		{
			if (b != 0)
			{
				return RijndaelEngine.Alogtable[(int)(25 + (RijndaelEngine.Logtable[b] & byte.MaxValue))];
			}
			return 0;
		}

		// Token: 0x06001D44 RID: 7492 RVA: 0x000A6CBC File Offset: 0x000A6CBC
		private byte Mul0x3(int b)
		{
			if (b != 0)
			{
				return RijndaelEngine.Alogtable[(int)(1 + (RijndaelEngine.Logtable[b] & byte.MaxValue))];
			}
			return 0;
		}

		// Token: 0x06001D45 RID: 7493 RVA: 0x000A6CDC File Offset: 0x000A6CDC
		private byte Mul0x9(int b)
		{
			if (b >= 0)
			{
				return RijndaelEngine.Alogtable[199 + b];
			}
			return 0;
		}

		// Token: 0x06001D46 RID: 7494 RVA: 0x000A6CF4 File Offset: 0x000A6CF4
		private byte Mul0xb(int b)
		{
			if (b >= 0)
			{
				return RijndaelEngine.Alogtable[104 + b];
			}
			return 0;
		}

		// Token: 0x06001D47 RID: 7495 RVA: 0x000A6D0C File Offset: 0x000A6D0C
		private byte Mul0xd(int b)
		{
			if (b >= 0)
			{
				return RijndaelEngine.Alogtable[238 + b];
			}
			return 0;
		}

		// Token: 0x06001D48 RID: 7496 RVA: 0x000A6D24 File Offset: 0x000A6D24
		private byte Mul0xe(int b)
		{
			if (b >= 0)
			{
				return RijndaelEngine.Alogtable[223 + b];
			}
			return 0;
		}

		// Token: 0x06001D49 RID: 7497 RVA: 0x000A6D3C File Offset: 0x000A6D3C
		private void KeyAddition(long[] rk)
		{
			this.A0 ^= rk[0];
			this.A1 ^= rk[1];
			this.A2 ^= rk[2];
			this.A3 ^= rk[3];
		}

		// Token: 0x06001D4A RID: 7498 RVA: 0x000A6D90 File Offset: 0x000A6D90
		private long Shift(long r, int shift)
		{
			ulong num = (ulong)r >> shift;
			if (shift > 31)
			{
				num &= (ulong)-1;
			}
			return (long)((num | (ulong)((ulong)r << this.BC - shift)) & (ulong)this.BC_MASK);
		}

		// Token: 0x06001D4B RID: 7499 RVA: 0x000A6DCC File Offset: 0x000A6DCC
		private void ShiftRow(byte[] shiftsSC)
		{
			this.A1 = this.Shift(this.A1, (int)shiftsSC[1]);
			this.A2 = this.Shift(this.A2, (int)shiftsSC[2]);
			this.A3 = this.Shift(this.A3, (int)shiftsSC[3]);
		}

		// Token: 0x06001D4C RID: 7500 RVA: 0x000A6E1C File Offset: 0x000A6E1C
		private long ApplyS(long r, byte[] box)
		{
			long num = 0L;
			for (int i = 0; i < this.BC; i += 8)
			{
				num |= (long)(box[(int)(r >> i & 255L)] & byte.MaxValue) << i;
			}
			return num;
		}

		// Token: 0x06001D4D RID: 7501 RVA: 0x000A6E64 File Offset: 0x000A6E64
		private void Substitution(byte[] box)
		{
			this.A0 = this.ApplyS(this.A0, box);
			this.A1 = this.ApplyS(this.A1, box);
			this.A2 = this.ApplyS(this.A2, box);
			this.A3 = this.ApplyS(this.A3, box);
		}

		// Token: 0x06001D4E RID: 7502 RVA: 0x000A6EC4 File Offset: 0x000A6EC4
		private void MixColumn()
		{
			long num4;
			long num3;
			long num2;
			long num = num2 = (num3 = (num4 = 0L));
			for (int i = 0; i < this.BC; i += 8)
			{
				int num5 = (int)(this.A0 >> i & 255L);
				int num6 = (int)(this.A1 >> i & 255L);
				int num7 = (int)(this.A2 >> i & 255L);
				int num8 = (int)(this.A3 >> i & 255L);
				num2 |= (long)(((int)(this.Mul0x2(num5) ^ this.Mul0x3(num6)) ^ num7 ^ num8) & 255) << i;
				num |= (long)(((int)(this.Mul0x2(num6) ^ this.Mul0x3(num7)) ^ num8 ^ num5) & 255) << i;
				num3 |= (long)(((int)(this.Mul0x2(num7) ^ this.Mul0x3(num8)) ^ num5 ^ num6) & 255) << i;
				num4 |= (long)(((int)(this.Mul0x2(num8) ^ this.Mul0x3(num5)) ^ num6 ^ num7) & 255) << i;
			}
			this.A0 = num2;
			this.A1 = num;
			this.A2 = num3;
			this.A3 = num4;
		}

		// Token: 0x06001D4F RID: 7503 RVA: 0x000A700C File Offset: 0x000A700C
		private void InvMixColumn()
		{
			long num4;
			long num3;
			long num2;
			long num = num2 = (num3 = (num4 = 0L));
			for (int i = 0; i < this.BC; i += 8)
			{
				int num5 = (int)(this.A0 >> i & 255L);
				int num6 = (int)(this.A1 >> i & 255L);
				int num7 = (int)(this.A2 >> i & 255L);
				int num8 = (int)(this.A3 >> i & 255L);
				num5 = ((num5 != 0) ? ((int)(RijndaelEngine.Logtable[num5 & 255] & byte.MaxValue)) : -1);
				num6 = ((num6 != 0) ? ((int)(RijndaelEngine.Logtable[num6 & 255] & byte.MaxValue)) : -1);
				num7 = ((num7 != 0) ? ((int)(RijndaelEngine.Logtable[num7 & 255] & byte.MaxValue)) : -1);
				num8 = ((num8 != 0) ? ((int)(RijndaelEngine.Logtable[num8 & 255] & byte.MaxValue)) : -1);
				num2 |= (long)((this.Mul0xe(num5) ^ this.Mul0xb(num6) ^ this.Mul0xd(num7) ^ this.Mul0x9(num8)) & byte.MaxValue) << i;
				num |= (long)((this.Mul0xe(num6) ^ this.Mul0xb(num7) ^ this.Mul0xd(num8) ^ this.Mul0x9(num5)) & byte.MaxValue) << i;
				num3 |= (long)((this.Mul0xe(num7) ^ this.Mul0xb(num8) ^ this.Mul0xd(num5) ^ this.Mul0x9(num6)) & byte.MaxValue) << i;
				num4 |= (long)((this.Mul0xe(num8) ^ this.Mul0xb(num5) ^ this.Mul0xd(num6) ^ this.Mul0x9(num7)) & byte.MaxValue) << i;
			}
			this.A0 = num2;
			this.A1 = num;
			this.A2 = num3;
			this.A3 = num4;
		}

		// Token: 0x06001D50 RID: 7504 RVA: 0x000A7210 File Offset: 0x000A7210
		private long[][] GenerateWorkingKey(byte[] key)
		{
			int num = 0;
			int num2 = key.Length * 8;
			byte[,] array = new byte[4, RijndaelEngine.MAXKC];
			long[][] array2 = new long[RijndaelEngine.MAXROUNDS + 1][];
			for (int i = 0; i < RijndaelEngine.MAXROUNDS + 1; i++)
			{
				array2[i] = new long[4];
			}
			int num3 = num2;
			int num4;
			if (num3 <= 160)
			{
				if (num3 == 128)
				{
					num4 = 4;
					goto IL_D0;
				}
				if (num3 == 160)
				{
					num4 = 5;
					goto IL_D0;
				}
			}
			else
			{
				if (num3 == 192)
				{
					num4 = 6;
					goto IL_D0;
				}
				if (num3 == 224)
				{
					num4 = 7;
					goto IL_D0;
				}
				if (num3 == 256)
				{
					num4 = 8;
					goto IL_D0;
				}
			}
			throw new ArgumentException("Key length not 128/160/192/224/256 bits.");
			IL_D0:
			if (num2 >= this.blockBits)
			{
				this.ROUNDS = num4 + 6;
			}
			else
			{
				this.ROUNDS = this.BC / 8 + 6;
			}
			int num5 = 0;
			for (int j = 0; j < key.Length; j++)
			{
				array[j % 4, j / 4] = key[num5++];
			}
			int k = 0;
			int l = 0;
			while (l < num4)
			{
				if (k >= (this.ROUNDS + 1) * (this.BC / 8))
				{
					break;
				}
				for (int m = 0; m < 4; m++)
				{
					long[] array3;
					IntPtr intPtr;
					(array3 = array2[k / (this.BC / 8)])[(int)(intPtr = (IntPtr)m)] = (array3[(int)intPtr] | (long)(array[m, l] & byte.MaxValue) << k * 8 % this.BC);
				}
				l++;
				k++;
			}
			while (k < (this.ROUNDS + 1) * (this.BC / 8))
			{
				byte[,] array4;
				for (int n = 0; n < 4; n++)
				{
					IntPtr intPtr;
					(array4 = array)[(int)(intPtr = (IntPtr)n), 0] = (array4[(int)intPtr, 0] ^ RijndaelEngine.S[(int)(array[(n + 1) % 4, num4 - 1] & byte.MaxValue)]);
				}
				(array4 = array)[0, 0] = (array4[0, 0] ^ RijndaelEngine.rcon[num++]);
				if (num4 <= 6)
				{
					for (int num6 = 1; num6 < num4; num6++)
					{
						for (int num7 = 0; num7 < 4; num7++)
						{
							IntPtr intPtr;
							IntPtr intPtr2;
							(array4 = array)[(int)(intPtr = (IntPtr)num7), (int)(intPtr2 = (IntPtr)num6)] = (array4[(int)intPtr, (int)intPtr2] ^ array[num7, num6 - 1]);
						}
					}
				}
				else
				{
					for (int num8 = 1; num8 < 4; num8++)
					{
						for (int num9 = 0; num9 < 4; num9++)
						{
							IntPtr intPtr;
							IntPtr intPtr2;
							(array4 = array)[(int)(intPtr = (IntPtr)num9), (int)(intPtr2 = (IntPtr)num8)] = (array4[(int)intPtr, (int)intPtr2] ^ array[num9, num8 - 1]);
						}
					}
					for (int num10 = 0; num10 < 4; num10++)
					{
						IntPtr intPtr;
						(array4 = array)[(int)(intPtr = (IntPtr)num10), 4] = (array4[(int)intPtr, 4] ^ RijndaelEngine.S[(int)(array[num10, 3] & byte.MaxValue)]);
					}
					for (int num11 = 5; num11 < num4; num11++)
					{
						for (int num12 = 0; num12 < 4; num12++)
						{
							IntPtr intPtr;
							IntPtr intPtr2;
							(array4 = array)[(int)(intPtr = (IntPtr)num12), (int)(intPtr2 = (IntPtr)num11)] = (array4[(int)intPtr, (int)intPtr2] ^ array[num12, num11 - 1]);
						}
					}
				}
				int num13 = 0;
				while (num13 < num4 && k < (this.ROUNDS + 1) * (this.BC / 8))
				{
					for (int num14 = 0; num14 < 4; num14++)
					{
						long[] array3;
						IntPtr intPtr;
						(array3 = array2[k / (this.BC / 8)])[(int)(intPtr = (IntPtr)num14)] = (array3[(int)intPtr] | (long)(array[num14, num13] & byte.MaxValue) << k * 8 % this.BC);
					}
					num13++;
					k++;
				}
			}
			return array2;
		}

		// Token: 0x06001D51 RID: 7505 RVA: 0x000A762C File Offset: 0x000A762C
		public RijndaelEngine() : this(128)
		{
		}

		// Token: 0x06001D52 RID: 7506 RVA: 0x000A763C File Offset: 0x000A763C
		public RijndaelEngine(int blockBits)
		{
			if (blockBits <= 160)
			{
				if (blockBits == 128)
				{
					this.BC = 32;
					this.BC_MASK = (long)((ulong)-1);
					this.shifts0SC = RijndaelEngine.shifts0[0];
					this.shifts1SC = RijndaelEngine.shifts1[0];
					goto IL_187;
				}
				if (blockBits == 160)
				{
					this.BC = 40;
					this.BC_MASK = 1099511627775L;
					this.shifts0SC = RijndaelEngine.shifts0[1];
					this.shifts1SC = RijndaelEngine.shifts1[1];
					goto IL_187;
				}
			}
			else
			{
				if (blockBits == 192)
				{
					this.BC = 48;
					this.BC_MASK = 281474976710655L;
					this.shifts0SC = RijndaelEngine.shifts0[2];
					this.shifts1SC = RijndaelEngine.shifts1[2];
					goto IL_187;
				}
				if (blockBits == 224)
				{
					this.BC = 56;
					this.BC_MASK = 72057594037927935L;
					this.shifts0SC = RijndaelEngine.shifts0[3];
					this.shifts1SC = RijndaelEngine.shifts1[3];
					goto IL_187;
				}
				if (blockBits == 256)
				{
					this.BC = 64;
					this.BC_MASK = -1L;
					this.shifts0SC = RijndaelEngine.shifts0[4];
					this.shifts1SC = RijndaelEngine.shifts1[4];
					goto IL_187;
				}
			}
			throw new ArgumentException("unknown blocksize to Rijndael");
			IL_187:
			this.blockBits = blockBits;
		}

		// Token: 0x06001D53 RID: 7507 RVA: 0x000A77DC File Offset: 0x000A77DC
		public virtual void Init(bool forEncryption, ICipherParameters parameters)
		{
			if (typeof(KeyParameter).IsInstanceOfType(parameters))
			{
				this.workingKey = this.GenerateWorkingKey(((KeyParameter)parameters).GetKey());
				this.forEncryption = forEncryption;
				return;
			}
			throw new ArgumentException("invalid parameter passed to Rijndael init - " + Platform.GetTypeName(parameters));
		}

		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x06001D54 RID: 7508 RVA: 0x000A7838 File Offset: 0x000A7838
		public virtual string AlgorithmName
		{
			get
			{
				return "Rijndael";
			}
		}

		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x06001D55 RID: 7509 RVA: 0x000A7840 File Offset: 0x000A7840
		public virtual bool IsPartialBlockOkay
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001D56 RID: 7510 RVA: 0x000A7844 File Offset: 0x000A7844
		public virtual int GetBlockSize()
		{
			return this.BC / 2;
		}

		// Token: 0x06001D57 RID: 7511 RVA: 0x000A7850 File Offset: 0x000A7850
		public virtual int ProcessBlock(byte[] input, int inOff, byte[] output, int outOff)
		{
			if (this.workingKey == null)
			{
				throw new InvalidOperationException("Rijndael engine not initialised");
			}
			Check.DataLength(input, inOff, this.BC / 2, "input buffer too short");
			Check.OutputLength(output, outOff, this.BC / 2, "output buffer too short");
			this.UnPackBlock(input, inOff);
			if (this.forEncryption)
			{
				this.EncryptBlock(this.workingKey);
			}
			else
			{
				this.DecryptBlock(this.workingKey);
			}
			this.PackBlock(output, outOff);
			return this.BC / 2;
		}

		// Token: 0x06001D58 RID: 7512 RVA: 0x000A78E4 File Offset: 0x000A78E4
		public virtual void Reset()
		{
		}

		// Token: 0x06001D59 RID: 7513 RVA: 0x000A78E8 File Offset: 0x000A78E8
		private void UnPackBlock(byte[] bytes, int off)
		{
			int num = off + 1;
			this.A0 = (long)(bytes[off] & byte.MaxValue);
			this.A1 = (long)(bytes[num++] & byte.MaxValue);
			this.A2 = (long)(bytes[num++] & byte.MaxValue);
			this.A3 = (long)(bytes[num++] & byte.MaxValue);
			for (int num2 = 8; num2 != this.BC; num2 += 8)
			{
				this.A0 |= (long)(bytes[num++] & byte.MaxValue) << num2;
				this.A1 |= (long)(bytes[num++] & byte.MaxValue) << num2;
				this.A2 |= (long)(bytes[num++] & byte.MaxValue) << num2;
				this.A3 |= (long)(bytes[num++] & byte.MaxValue) << num2;
			}
		}

		// Token: 0x06001D5A RID: 7514 RVA: 0x000A79E4 File Offset: 0x000A79E4
		private void PackBlock(byte[] bytes, int off)
		{
			int num = off;
			for (int num2 = 0; num2 != this.BC; num2 += 8)
			{
				bytes[num++] = (byte)(this.A0 >> num2);
				bytes[num++] = (byte)(this.A1 >> num2);
				bytes[num++] = (byte)(this.A2 >> num2);
				bytes[num++] = (byte)(this.A3 >> num2);
			}
		}

		// Token: 0x06001D5B RID: 7515 RVA: 0x000A7A58 File Offset: 0x000A7A58
		private void EncryptBlock(long[][] rk)
		{
			this.KeyAddition(rk[0]);
			for (int i = 1; i < this.ROUNDS; i++)
			{
				this.Substitution(RijndaelEngine.S);
				this.ShiftRow(this.shifts0SC);
				this.MixColumn();
				this.KeyAddition(rk[i]);
			}
			this.Substitution(RijndaelEngine.S);
			this.ShiftRow(this.shifts0SC);
			this.KeyAddition(rk[this.ROUNDS]);
		}

		// Token: 0x06001D5C RID: 7516 RVA: 0x000A7AE0 File Offset: 0x000A7AE0
		private void DecryptBlock(long[][] rk)
		{
			this.KeyAddition(rk[this.ROUNDS]);
			this.Substitution(RijndaelEngine.Si);
			this.ShiftRow(this.shifts1SC);
			for (int i = this.ROUNDS - 1; i > 0; i--)
			{
				this.KeyAddition(rk[i]);
				this.InvMixColumn();
				this.Substitution(RijndaelEngine.Si);
				this.ShiftRow(this.shifts1SC);
			}
			this.KeyAddition(rk[0]);
		}

		// Token: 0x04001362 RID: 4962
		private static readonly int MAXROUNDS = 14;

		// Token: 0x04001363 RID: 4963
		private static readonly int MAXKC = 64;

		// Token: 0x04001364 RID: 4964
		private static readonly byte[] Logtable = new byte[]
		{
			0,
			0,
			25,
			1,
			50,
			2,
			26,
			198,
			75,
			199,
			27,
			104,
			51,
			238,
			223,
			3,
			100,
			4,
			224,
			14,
			52,
			141,
			129,
			239,
			76,
			113,
			8,
			200,
			248,
			105,
			28,
			193,
			125,
			194,
			29,
			181,
			249,
			185,
			39,
			106,
			77,
			228,
			166,
			114,
			154,
			201,
			9,
			120,
			101,
			47,
			138,
			5,
			33,
			15,
			225,
			36,
			18,
			240,
			130,
			69,
			53,
			147,
			218,
			142,
			150,
			143,
			219,
			189,
			54,
			208,
			206,
			148,
			19,
			92,
			210,
			241,
			64,
			70,
			131,
			56,
			102,
			221,
			253,
			48,
			191,
			6,
			139,
			98,
			179,
			37,
			226,
			152,
			34,
			136,
			145,
			16,
			126,
			110,
			72,
			195,
			163,
			182,
			30,
			66,
			58,
			107,
			40,
			84,
			250,
			133,
			61,
			186,
			43,
			121,
			10,
			21,
			155,
			159,
			94,
			202,
			78,
			212,
			172,
			229,
			243,
			115,
			167,
			87,
			175,
			88,
			168,
			80,
			244,
			234,
			214,
			116,
			79,
			174,
			233,
			213,
			231,
			230,
			173,
			232,
			44,
			215,
			117,
			122,
			235,
			22,
			11,
			245,
			89,
			203,
			95,
			176,
			156,
			169,
			81,
			160,
			127,
			12,
			246,
			111,
			23,
			196,
			73,
			236,
			216,
			67,
			31,
			45,
			164,
			118,
			123,
			183,
			204,
			187,
			62,
			90,
			251,
			96,
			177,
			134,
			59,
			82,
			161,
			108,
			170,
			85,
			41,
			157,
			151,
			178,
			135,
			144,
			97,
			190,
			220,
			252,
			188,
			149,
			207,
			205,
			55,
			63,
			91,
			209,
			83,
			57,
			132,
			60,
			65,
			162,
			109,
			71,
			20,
			42,
			158,
			93,
			86,
			242,
			211,
			171,
			68,
			17,
			146,
			217,
			35,
			32,
			46,
			137,
			180,
			124,
			184,
			38,
			119,
			153,
			227,
			165,
			103,
			74,
			237,
			222,
			197,
			49,
			254,
			24,
			13,
			99,
			140,
			128,
			192,
			247,
			112,
			7
		};

		// Token: 0x04001365 RID: 4965
		private static readonly byte[] Alogtable = new byte[]
		{
			0,
			3,
			5,
			15,
			17,
			51,
			85,
			byte.MaxValue,
			26,
			46,
			114,
			150,
			161,
			248,
			19,
			53,
			95,
			225,
			56,
			72,
			216,
			115,
			149,
			164,
			247,
			2,
			6,
			10,
			30,
			34,
			102,
			170,
			229,
			52,
			92,
			228,
			55,
			89,
			235,
			38,
			106,
			190,
			217,
			112,
			144,
			171,
			230,
			49,
			83,
			245,
			4,
			12,
			20,
			60,
			68,
			204,
			79,
			209,
			104,
			184,
			211,
			110,
			178,
			205,
			76,
			212,
			103,
			169,
			224,
			59,
			77,
			215,
			98,
			166,
			241,
			8,
			24,
			40,
			120,
			136,
			131,
			158,
			185,
			208,
			107,
			189,
			220,
			127,
			129,
			152,
			179,
			206,
			73,
			219,
			118,
			154,
			181,
			196,
			87,
			249,
			16,
			48,
			80,
			240,
			11,
			29,
			39,
			105,
			187,
			214,
			97,
			163,
			254,
			25,
			43,
			125,
			135,
			146,
			173,
			236,
			47,
			113,
			147,
			174,
			233,
			32,
			96,
			160,
			251,
			22,
			58,
			78,
			210,
			109,
			183,
			194,
			93,
			231,
			50,
			86,
			250,
			21,
			63,
			65,
			195,
			94,
			226,
			61,
			71,
			201,
			64,
			192,
			91,
			237,
			44,
			116,
			156,
			191,
			218,
			117,
			159,
			186,
			213,
			100,
			172,
			239,
			42,
			126,
			130,
			157,
			188,
			223,
			122,
			142,
			137,
			128,
			155,
			182,
			193,
			88,
			232,
			35,
			101,
			175,
			234,
			37,
			111,
			177,
			200,
			67,
			197,
			84,
			252,
			31,
			33,
			99,
			165,
			244,
			7,
			9,
			27,
			45,
			119,
			153,
			176,
			203,
			70,
			202,
			69,
			207,
			74,
			222,
			121,
			139,
			134,
			145,
			168,
			227,
			62,
			66,
			198,
			81,
			243,
			14,
			18,
			54,
			90,
			238,
			41,
			123,
			141,
			140,
			143,
			138,
			133,
			148,
			167,
			242,
			13,
			23,
			57,
			75,
			221,
			124,
			132,
			151,
			162,
			253,
			28,
			36,
			108,
			180,
			199,
			82,
			246,
			1,
			3,
			5,
			15,
			17,
			51,
			85,
			byte.MaxValue,
			26,
			46,
			114,
			150,
			161,
			248,
			19,
			53,
			95,
			225,
			56,
			72,
			216,
			115,
			149,
			164,
			247,
			2,
			6,
			10,
			30,
			34,
			102,
			170,
			229,
			52,
			92,
			228,
			55,
			89,
			235,
			38,
			106,
			190,
			217,
			112,
			144,
			171,
			230,
			49,
			83,
			245,
			4,
			12,
			20,
			60,
			68,
			204,
			79,
			209,
			104,
			184,
			211,
			110,
			178,
			205,
			76,
			212,
			103,
			169,
			224,
			59,
			77,
			215,
			98,
			166,
			241,
			8,
			24,
			40,
			120,
			136,
			131,
			158,
			185,
			208,
			107,
			189,
			220,
			127,
			129,
			152,
			179,
			206,
			73,
			219,
			118,
			154,
			181,
			196,
			87,
			249,
			16,
			48,
			80,
			240,
			11,
			29,
			39,
			105,
			187,
			214,
			97,
			163,
			254,
			25,
			43,
			125,
			135,
			146,
			173,
			236,
			47,
			113,
			147,
			174,
			233,
			32,
			96,
			160,
			251,
			22,
			58,
			78,
			210,
			109,
			183,
			194,
			93,
			231,
			50,
			86,
			250,
			21,
			63,
			65,
			195,
			94,
			226,
			61,
			71,
			201,
			64,
			192,
			91,
			237,
			44,
			116,
			156,
			191,
			218,
			117,
			159,
			186,
			213,
			100,
			172,
			239,
			42,
			126,
			130,
			157,
			188,
			223,
			122,
			142,
			137,
			128,
			155,
			182,
			193,
			88,
			232,
			35,
			101,
			175,
			234,
			37,
			111,
			177,
			200,
			67,
			197,
			84,
			252,
			31,
			33,
			99,
			165,
			244,
			7,
			9,
			27,
			45,
			119,
			153,
			176,
			203,
			70,
			202,
			69,
			207,
			74,
			222,
			121,
			139,
			134,
			145,
			168,
			227,
			62,
			66,
			198,
			81,
			243,
			14,
			18,
			54,
			90,
			238,
			41,
			123,
			141,
			140,
			143,
			138,
			133,
			148,
			167,
			242,
			13,
			23,
			57,
			75,
			221,
			124,
			132,
			151,
			162,
			253,
			28,
			36,
			108,
			180,
			199,
			82,
			246,
			1
		};

		// Token: 0x04001366 RID: 4966
		private static readonly byte[] S = new byte[]
		{
			99,
			124,
			119,
			123,
			242,
			107,
			111,
			197,
			48,
			1,
			103,
			43,
			254,
			215,
			171,
			118,
			202,
			130,
			201,
			125,
			250,
			89,
			71,
			240,
			173,
			212,
			162,
			175,
			156,
			164,
			114,
			192,
			183,
			253,
			147,
			38,
			54,
			63,
			247,
			204,
			52,
			165,
			229,
			241,
			113,
			216,
			49,
			21,
			4,
			199,
			35,
			195,
			24,
			150,
			5,
			154,
			7,
			18,
			128,
			226,
			235,
			39,
			178,
			117,
			9,
			131,
			44,
			26,
			27,
			110,
			90,
			160,
			82,
			59,
			214,
			179,
			41,
			227,
			47,
			132,
			83,
			209,
			0,
			237,
			32,
			252,
			177,
			91,
			106,
			203,
			190,
			57,
			74,
			76,
			88,
			207,
			208,
			239,
			170,
			251,
			67,
			77,
			51,
			133,
			69,
			249,
			2,
			127,
			80,
			60,
			159,
			168,
			81,
			163,
			64,
			143,
			146,
			157,
			56,
			245,
			188,
			182,
			218,
			33,
			16,
			byte.MaxValue,
			243,
			210,
			205,
			12,
			19,
			236,
			95,
			151,
			68,
			23,
			196,
			167,
			126,
			61,
			100,
			93,
			25,
			115,
			96,
			129,
			79,
			220,
			34,
			42,
			144,
			136,
			70,
			238,
			184,
			20,
			222,
			94,
			11,
			219,
			224,
			50,
			58,
			10,
			73,
			6,
			36,
			92,
			194,
			211,
			172,
			98,
			145,
			149,
			228,
			121,
			231,
			200,
			55,
			109,
			141,
			213,
			78,
			169,
			108,
			86,
			244,
			234,
			101,
			122,
			174,
			8,
			186,
			120,
			37,
			46,
			28,
			166,
			180,
			198,
			232,
			221,
			116,
			31,
			75,
			189,
			139,
			138,
			112,
			62,
			181,
			102,
			72,
			3,
			246,
			14,
			97,
			53,
			87,
			185,
			134,
			193,
			29,
			158,
			225,
			248,
			152,
			17,
			105,
			217,
			142,
			148,
			155,
			30,
			135,
			233,
			206,
			85,
			40,
			223,
			140,
			161,
			137,
			13,
			191,
			230,
			66,
			104,
			65,
			153,
			45,
			15,
			176,
			84,
			187,
			22
		};

		// Token: 0x04001367 RID: 4967
		private static readonly byte[] Si = new byte[]
		{
			82,
			9,
			106,
			213,
			48,
			54,
			165,
			56,
			191,
			64,
			163,
			158,
			129,
			243,
			215,
			251,
			124,
			227,
			57,
			130,
			155,
			47,
			byte.MaxValue,
			135,
			52,
			142,
			67,
			68,
			196,
			222,
			233,
			203,
			84,
			123,
			148,
			50,
			166,
			194,
			35,
			61,
			238,
			76,
			149,
			11,
			66,
			250,
			195,
			78,
			8,
			46,
			161,
			102,
			40,
			217,
			36,
			178,
			118,
			91,
			162,
			73,
			109,
			139,
			209,
			37,
			114,
			248,
			246,
			100,
			134,
			104,
			152,
			22,
			212,
			164,
			92,
			204,
			93,
			101,
			182,
			146,
			108,
			112,
			72,
			80,
			253,
			237,
			185,
			218,
			94,
			21,
			70,
			87,
			167,
			141,
			157,
			132,
			144,
			216,
			171,
			0,
			140,
			188,
			211,
			10,
			247,
			228,
			88,
			5,
			184,
			179,
			69,
			6,
			208,
			44,
			30,
			143,
			202,
			63,
			15,
			2,
			193,
			175,
			189,
			3,
			1,
			19,
			138,
			107,
			58,
			145,
			17,
			65,
			79,
			103,
			220,
			234,
			151,
			242,
			207,
			206,
			240,
			180,
			230,
			115,
			150,
			172,
			116,
			34,
			231,
			173,
			53,
			133,
			226,
			249,
			55,
			232,
			28,
			117,
			223,
			110,
			71,
			241,
			26,
			113,
			29,
			41,
			197,
			137,
			111,
			183,
			98,
			14,
			170,
			24,
			190,
			27,
			252,
			86,
			62,
			75,
			198,
			210,
			121,
			32,
			154,
			219,
			192,
			254,
			120,
			205,
			90,
			244,
			31,
			221,
			168,
			51,
			136,
			7,
			199,
			49,
			177,
			18,
			16,
			89,
			39,
			128,
			236,
			95,
			96,
			81,
			127,
			169,
			25,
			181,
			74,
			13,
			45,
			229,
			122,
			159,
			147,
			201,
			156,
			239,
			160,
			224,
			59,
			77,
			174,
			42,
			245,
			176,
			200,
			235,
			187,
			60,
			131,
			83,
			153,
			97,
			23,
			43,
			4,
			126,
			186,
			119,
			214,
			38,
			225,
			105,
			20,
			99,
			85,
			33,
			12,
			125
		};

		// Token: 0x04001368 RID: 4968
		private static readonly byte[] rcon = new byte[]
		{
			1,
			2,
			4,
			8,
			16,
			32,
			64,
			128,
			27,
			54,
			108,
			216,
			171,
			77,
			154,
			47,
			94,
			188,
			99,
			198,
			151,
			53,
			106,
			212,
			179,
			125,
			250,
			239,
			197,
			145
		};

		// Token: 0x04001369 RID: 4969
		private static readonly byte[][] shifts0 = new byte[][]
		{
			new byte[]
			{
				0,
				8,
				16,
				24
			},
			new byte[]
			{
				0,
				8,
				16,
				24
			},
			new byte[]
			{
				0,
				8,
				16,
				24
			},
			new byte[]
			{
				0,
				8,
				16,
				32
			},
			new byte[]
			{
				0,
				8,
				24,
				32
			}
		};

		// Token: 0x0400136A RID: 4970
		private static readonly byte[][] shifts1 = new byte[][]
		{
			new byte[]
			{
				0,
				24,
				16,
				8
			},
			new byte[]
			{
				0,
				32,
				24,
				16
			},
			new byte[]
			{
				0,
				40,
				32,
				24
			},
			new byte[]
			{
				0,
				48,
				40,
				24
			},
			new byte[]
			{
				0,
				56,
				40,
				32
			}
		};

		// Token: 0x0400136B RID: 4971
		private int BC;

		// Token: 0x0400136C RID: 4972
		private long BC_MASK;

		// Token: 0x0400136D RID: 4973
		private int ROUNDS;

		// Token: 0x0400136E RID: 4974
		private int blockBits;

		// Token: 0x0400136F RID: 4975
		private long[][] workingKey;

		// Token: 0x04001370 RID: 4976
		private long A0;

		// Token: 0x04001371 RID: 4977
		private long A1;

		// Token: 0x04001372 RID: 4978
		private long A2;

		// Token: 0x04001373 RID: 4979
		private long A3;

		// Token: 0x04001374 RID: 4980
		private bool forEncryption;

		// Token: 0x04001375 RID: 4981
		private byte[] shifts0SC;

		// Token: 0x04001376 RID: 4982
		private byte[] shifts1SC;
	}
}
