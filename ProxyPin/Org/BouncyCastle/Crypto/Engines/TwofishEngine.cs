using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Engines
{
	// Token: 0x020003AC RID: 940
	public sealed class TwofishEngine : IBlockCipher
	{
		// Token: 0x06001DFF RID: 7679 RVA: 0x000AF200 File Offset: 0x000AF200
		public TwofishEngine()
		{
			int[] array = new int[2];
			int[] array2 = new int[2];
			int[] array3 = new int[2];
			for (int i = 0; i < 256; i++)
			{
				int num = (int)(TwofishEngine.P[0, i] & byte.MaxValue);
				array[0] = num;
				array2[0] = (this.Mx_X(num) & 255);
				array3[0] = (this.Mx_Y(num) & 255);
				num = (int)(TwofishEngine.P[1, i] & byte.MaxValue);
				array[1] = num;
				array2[1] = (this.Mx_X(num) & 255);
				array3[1] = (this.Mx_Y(num) & 255);
				this.gMDS0[i] = (array[1] | array2[1] << 8 | array3[1] << 16 | array3[1] << 24);
				this.gMDS1[i] = (array3[0] | array3[0] << 8 | array2[0] << 16 | array[0] << 24);
				this.gMDS2[i] = (array2[1] | array3[1] << 8 | array[1] << 16 | array3[1] << 24);
				this.gMDS3[i] = (array2[0] | array[0] << 8 | array3[0] << 16 | array2[0] << 24);
			}
		}

		// Token: 0x06001E00 RID: 7680 RVA: 0x000AF374 File Offset: 0x000AF374
		public void Init(bool forEncryption, ICipherParameters parameters)
		{
			if (!(parameters is KeyParameter))
			{
				throw new ArgumentException("invalid parameter passed to Twofish init - " + Platform.GetTypeName(parameters));
			}
			this.encrypting = forEncryption;
			this.workingKey = ((KeyParameter)parameters).GetKey();
			this.k64Cnt = this.workingKey.Length / 8;
			this.SetKey(this.workingKey);
		}

		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x06001E01 RID: 7681 RVA: 0x000AF3DC File Offset: 0x000AF3DC
		public string AlgorithmName
		{
			get
			{
				return "Twofish";
			}
		}

		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x06001E02 RID: 7682 RVA: 0x000AF3E4 File Offset: 0x000AF3E4
		public bool IsPartialBlockOkay
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001E03 RID: 7683 RVA: 0x000AF3E8 File Offset: 0x000AF3E8
		public int ProcessBlock(byte[] input, int inOff, byte[] output, int outOff)
		{
			if (this.workingKey == null)
			{
				throw new InvalidOperationException("Twofish not initialised");
			}
			Check.DataLength(input, inOff, 16, "input buffer too short");
			Check.OutputLength(output, outOff, 16, "output buffer too short");
			if (this.encrypting)
			{
				this.EncryptBlock(input, inOff, output, outOff);
			}
			else
			{
				this.DecryptBlock(input, inOff, output, outOff);
			}
			return 16;
		}

		// Token: 0x06001E04 RID: 7684 RVA: 0x000AF454 File Offset: 0x000AF454
		public void Reset()
		{
			if (this.workingKey != null)
			{
				this.SetKey(this.workingKey);
			}
		}

		// Token: 0x06001E05 RID: 7685 RVA: 0x000AF470 File Offset: 0x000AF470
		public int GetBlockSize()
		{
			return 16;
		}

		// Token: 0x06001E06 RID: 7686 RVA: 0x000AF474 File Offset: 0x000AF474
		private void SetKey(byte[] key)
		{
			int[] array = new int[4];
			int[] array2 = new int[4];
			int[] array3 = new int[4];
			this.gSubKeys = new int[40];
			if (this.k64Cnt < 1)
			{
				throw new ArgumentException("Key size less than 64 bits");
			}
			if (this.k64Cnt > 4)
			{
				throw new ArgumentException("Key size larger than 256 bits");
			}
			for (int i = 0; i < this.k64Cnt; i++)
			{
				int num = i * 8;
				array[i] = this.BytesTo32Bits(key, num);
				array2[i] = this.BytesTo32Bits(key, num + 4);
				array3[this.k64Cnt - 1 - i] = this.RS_MDS_Encode(array[i], array2[i]);
			}
			for (int j = 0; j < 20; j++)
			{
				int num2 = j * 33686018;
				int num3 = this.F32(num2, array);
				int num4 = this.F32(num2 + 16843009, array2);
				num4 = (num4 << 8 | (int)((uint)num4 >> 24));
				num3 += num4;
				this.gSubKeys[j * 2] = num3;
				num3 += num4;
				this.gSubKeys[j * 2 + 1] = (num3 << 9 | (int)((uint)num3 >> 23));
			}
			int x = array3[0];
			int x2 = array3[1];
			int x3 = array3[2];
			int x4 = array3[3];
			this.gSBox = new int[1024];
			int k = 0;
			while (k < 256)
			{
				int num8;
				int num7;
				int num6;
				int num5 = num6 = (num7 = (num8 = k));
				switch (this.k64Cnt & 3)
				{
				case 0:
					num6 = ((int)(TwofishEngine.P[1, num6] & byte.MaxValue) ^ this.M_b0(x4));
					num5 = ((int)(TwofishEngine.P[0, num5] & byte.MaxValue) ^ this.M_b1(x4));
					num7 = ((int)(TwofishEngine.P[0, num7] & byte.MaxValue) ^ this.M_b2(x4));
					num8 = ((int)(TwofishEngine.P[1, num8] & byte.MaxValue) ^ this.M_b3(x4));
					goto IL_2B9;
				case 1:
					this.gSBox[k * 2] = this.gMDS0[(int)(TwofishEngine.P[0, num6] & byte.MaxValue) ^ this.M_b0(x)];
					this.gSBox[k * 2 + 1] = this.gMDS1[(int)(TwofishEngine.P[0, num5] & byte.MaxValue) ^ this.M_b1(x)];
					this.gSBox[k * 2 + 512] = this.gMDS2[(int)(TwofishEngine.P[1, num7] & byte.MaxValue) ^ this.M_b2(x)];
					this.gSBox[k * 2 + 513] = this.gMDS3[(int)(TwofishEngine.P[1, num8] & byte.MaxValue) ^ this.M_b3(x)];
					break;
				case 2:
					goto IL_331;
				case 3:
					goto IL_2B9;
				}
				IL_45F:
				k++;
				continue;
				IL_331:
				this.gSBox[k * 2] = this.gMDS0[(int)(TwofishEngine.P[0, (int)(TwofishEngine.P[0, num6] & byte.MaxValue) ^ this.M_b0(x2)] & byte.MaxValue) ^ this.M_b0(x)];
				this.gSBox[k * 2 + 1] = this.gMDS1[(int)(TwofishEngine.P[0, (int)(TwofishEngine.P[1, num5] & byte.MaxValue) ^ this.M_b1(x2)] & byte.MaxValue) ^ this.M_b1(x)];
				this.gSBox[k * 2 + 512] = this.gMDS2[(int)(TwofishEngine.P[1, (int)(TwofishEngine.P[0, num7] & byte.MaxValue) ^ this.M_b2(x2)] & byte.MaxValue) ^ this.M_b2(x)];
				this.gSBox[k * 2 + 513] = this.gMDS3[(int)(TwofishEngine.P[1, (int)(TwofishEngine.P[1, num8] & byte.MaxValue) ^ this.M_b3(x2)] & byte.MaxValue) ^ this.M_b3(x)];
				goto IL_45F;
				IL_2B9:
				num6 = ((int)(TwofishEngine.P[1, num6] & byte.MaxValue) ^ this.M_b0(x3));
				num5 = ((int)(TwofishEngine.P[1, num5] & byte.MaxValue) ^ this.M_b1(x3));
				num7 = ((int)(TwofishEngine.P[0, num7] & byte.MaxValue) ^ this.M_b2(x3));
				num8 = ((int)(TwofishEngine.P[0, num8] & byte.MaxValue) ^ this.M_b3(x3));
				goto IL_331;
			}
		}

		// Token: 0x06001E07 RID: 7687 RVA: 0x000AF8F8 File Offset: 0x000AF8F8
		private void EncryptBlock(byte[] src, int srcIndex, byte[] dst, int dstIndex)
		{
			int num = this.BytesTo32Bits(src, srcIndex) ^ this.gSubKeys[0];
			int num2 = this.BytesTo32Bits(src, srcIndex + 4) ^ this.gSubKeys[1];
			int num3 = this.BytesTo32Bits(src, srcIndex + 8) ^ this.gSubKeys[2];
			int num4 = this.BytesTo32Bits(src, srcIndex + 12) ^ this.gSubKeys[3];
			int num5 = 8;
			for (int i = 0; i < 16; i += 2)
			{
				int num6 = this.Fe32_0(num);
				int num7 = this.Fe32_3(num2);
				num3 ^= num6 + num7 + this.gSubKeys[num5++];
				num3 = (int)((uint)num3 >> 1 | (uint)((uint)num3 << 31));
				num4 = ((num4 << 1 | (int)((uint)num4 >> 31)) ^ num6 + 2 * num7 + this.gSubKeys[num5++]);
				num6 = this.Fe32_0(num3);
				num7 = this.Fe32_3(num4);
				num ^= num6 + num7 + this.gSubKeys[num5++];
				num = (int)((uint)num >> 1 | (uint)((uint)num << 31));
				num2 = ((num2 << 1 | (int)((uint)num2 >> 31)) ^ num6 + 2 * num7 + this.gSubKeys[num5++]);
			}
			this.Bits32ToBytes(num3 ^ this.gSubKeys[4], dst, dstIndex);
			this.Bits32ToBytes(num4 ^ this.gSubKeys[5], dst, dstIndex + 4);
			this.Bits32ToBytes(num ^ this.gSubKeys[6], dst, dstIndex + 8);
			this.Bits32ToBytes(num2 ^ this.gSubKeys[7], dst, dstIndex + 12);
		}

		// Token: 0x06001E08 RID: 7688 RVA: 0x000AFA6C File Offset: 0x000AFA6C
		private void DecryptBlock(byte[] src, int srcIndex, byte[] dst, int dstIndex)
		{
			int num = this.BytesTo32Bits(src, srcIndex) ^ this.gSubKeys[4];
			int num2 = this.BytesTo32Bits(src, srcIndex + 4) ^ this.gSubKeys[5];
			int num3 = this.BytesTo32Bits(src, srcIndex + 8) ^ this.gSubKeys[6];
			int num4 = this.BytesTo32Bits(src, srcIndex + 12) ^ this.gSubKeys[7];
			int num5 = 39;
			for (int i = 0; i < 16; i += 2)
			{
				int num6 = this.Fe32_0(num);
				int num7 = this.Fe32_3(num2);
				num4 ^= num6 + 2 * num7 + this.gSubKeys[num5--];
				num3 = ((num3 << 1 | (int)((uint)num3 >> 31)) ^ num6 + num7 + this.gSubKeys[num5--]);
				num4 = (int)((uint)num4 >> 1 | (uint)((uint)num4 << 31));
				num6 = this.Fe32_0(num3);
				num7 = this.Fe32_3(num4);
				num2 ^= num6 + 2 * num7 + this.gSubKeys[num5--];
				num = ((num << 1 | (int)((uint)num >> 31)) ^ num6 + num7 + this.gSubKeys[num5--]);
				num2 = (int)((uint)num2 >> 1 | (uint)((uint)num2 << 31));
			}
			this.Bits32ToBytes(num3 ^ this.gSubKeys[0], dst, dstIndex);
			this.Bits32ToBytes(num4 ^ this.gSubKeys[1], dst, dstIndex + 4);
			this.Bits32ToBytes(num ^ this.gSubKeys[2], dst, dstIndex + 8);
			this.Bits32ToBytes(num2 ^ this.gSubKeys[3], dst, dstIndex + 12);
		}

		// Token: 0x06001E09 RID: 7689 RVA: 0x000AFBE0 File Offset: 0x000AFBE0
		private int F32(int x, int[] k32)
		{
			int num = this.M_b0(x);
			int num2 = this.M_b1(x);
			int num3 = this.M_b2(x);
			int num4 = this.M_b3(x);
			int x2 = k32[0];
			int x3 = k32[1];
			int x4 = k32[2];
			int x5 = k32[3];
			int result = 0;
			switch (this.k64Cnt & 3)
			{
			case 0:
				num = ((int)(TwofishEngine.P[1, num] & byte.MaxValue) ^ this.M_b0(x5));
				num2 = ((int)(TwofishEngine.P[0, num2] & byte.MaxValue) ^ this.M_b1(x5));
				num3 = ((int)(TwofishEngine.P[0, num3] & byte.MaxValue) ^ this.M_b2(x5));
				num4 = ((int)(TwofishEngine.P[1, num4] & byte.MaxValue) ^ this.M_b3(x5));
				break;
			case 1:
				return this.gMDS0[(int)(TwofishEngine.P[0, num] & byte.MaxValue) ^ this.M_b0(x2)] ^ this.gMDS1[(int)(TwofishEngine.P[0, num2] & byte.MaxValue) ^ this.M_b1(x2)] ^ this.gMDS2[(int)(TwofishEngine.P[1, num3] & byte.MaxValue) ^ this.M_b2(x2)] ^ this.gMDS3[(int)(TwofishEngine.P[1, num4] & byte.MaxValue) ^ this.M_b3(x2)];
			case 2:
				goto IL_1CF;
			case 3:
				break;
			default:
				return result;
			}
			num = ((int)(TwofishEngine.P[1, num] & byte.MaxValue) ^ this.M_b0(x4));
			num2 = ((int)(TwofishEngine.P[1, num2] & byte.MaxValue) ^ this.M_b1(x4));
			num3 = ((int)(TwofishEngine.P[0, num3] & byte.MaxValue) ^ this.M_b2(x4));
			num4 = ((int)(TwofishEngine.P[0, num4] & byte.MaxValue) ^ this.M_b3(x4));
			IL_1CF:
			result = (this.gMDS0[(int)(TwofishEngine.P[0, (int)(TwofishEngine.P[0, num] & byte.MaxValue) ^ this.M_b0(x3)] & byte.MaxValue) ^ this.M_b0(x2)] ^ this.gMDS1[(int)(TwofishEngine.P[0, (int)(TwofishEngine.P[1, num2] & byte.MaxValue) ^ this.M_b1(x3)] & byte.MaxValue) ^ this.M_b1(x2)] ^ this.gMDS2[(int)(TwofishEngine.P[1, (int)(TwofishEngine.P[0, num3] & byte.MaxValue) ^ this.M_b2(x3)] & byte.MaxValue) ^ this.M_b2(x2)] ^ this.gMDS3[(int)(TwofishEngine.P[1, (int)(TwofishEngine.P[1, num4] & byte.MaxValue) ^ this.M_b3(x3)] & byte.MaxValue) ^ this.M_b3(x2)]);
			return result;
		}

		// Token: 0x06001E0A RID: 7690 RVA: 0x000AFEB8 File Offset: 0x000AFEB8
		private int RS_MDS_Encode(int k0, int k1)
		{
			int num = k1;
			for (int i = 0; i < 4; i++)
			{
				num = this.RS_rem(num);
			}
			num ^= k0;
			for (int j = 0; j < 4; j++)
			{
				num = this.RS_rem(num);
			}
			return num;
		}

		// Token: 0x06001E0B RID: 7691 RVA: 0x000AFF00 File Offset: 0x000AFF00
		private int RS_rem(int x)
		{
			int num = (int)((uint)x >> 24 & 255U);
			int num2 = (num << 1 ^ (((num & 128) != 0) ? 333 : 0)) & 255;
			int num3 = (int)((uint)num >> 1 ^ (((num & 1) != 0) ? 166U : 0U) ^ (uint)num2);
			return x << 8 ^ num3 << 24 ^ num2 << 16 ^ num3 << 8 ^ num;
		}

		// Token: 0x06001E0C RID: 7692 RVA: 0x000AFF6C File Offset: 0x000AFF6C
		private int LFSR1(int x)
		{
			return x >> 1 ^ (((x & 1) != 0) ? 180 : 0);
		}

		// Token: 0x06001E0D RID: 7693 RVA: 0x000AFF88 File Offset: 0x000AFF88
		private int LFSR2(int x)
		{
			return x >> 2 ^ (((x & 2) != 0) ? 180 : 0) ^ (((x & 1) != 0) ? 90 : 0);
		}

		// Token: 0x06001E0E RID: 7694 RVA: 0x000AFFB4 File Offset: 0x000AFFB4
		private int Mx_X(int x)
		{
			return x ^ this.LFSR2(x);
		}

		// Token: 0x06001E0F RID: 7695 RVA: 0x000AFFC0 File Offset: 0x000AFFC0
		private int Mx_Y(int x)
		{
			return x ^ this.LFSR1(x) ^ this.LFSR2(x);
		}

		// Token: 0x06001E10 RID: 7696 RVA: 0x000AFFD4 File Offset: 0x000AFFD4
		private int M_b0(int x)
		{
			return x & 255;
		}

		// Token: 0x06001E11 RID: 7697 RVA: 0x000AFFE0 File Offset: 0x000AFFE0
		private int M_b1(int x)
		{
			return (int)((uint)x >> 8 & 255U);
		}

		// Token: 0x06001E12 RID: 7698 RVA: 0x000AFFEC File Offset: 0x000AFFEC
		private int M_b2(int x)
		{
			return (int)((uint)x >> 16 & 255U);
		}

		// Token: 0x06001E13 RID: 7699 RVA: 0x000AFFF8 File Offset: 0x000AFFF8
		private int M_b3(int x)
		{
			return (int)((uint)x >> 24 & 255U);
		}

		// Token: 0x06001E14 RID: 7700 RVA: 0x000B0004 File Offset: 0x000B0004
		private int Fe32_0(int x)
		{
			return this.gSBox[2 * (x & 255)] ^ this.gSBox[(int)(1U + 2U * ((uint)x >> 8 & 255U))] ^ this.gSBox[(int)(512U + 2U * ((uint)x >> 16 & 255U))] ^ this.gSBox[(int)(513U + 2U * ((uint)x >> 24 & 255U))];
		}

		// Token: 0x06001E15 RID: 7701 RVA: 0x000B0070 File Offset: 0x000B0070
		private int Fe32_3(int x)
		{
			return this.gSBox[(int)(2U * ((uint)x >> 24 & 255U))] ^ this.gSBox[1 + 2 * (x & 255)] ^ this.gSBox[(int)(512U + 2U * ((uint)x >> 8 & 255U))] ^ this.gSBox[(int)(513U + 2U * ((uint)x >> 16 & 255U))];
		}

		// Token: 0x06001E16 RID: 7702 RVA: 0x000B00DC File Offset: 0x000B00DC
		private int BytesTo32Bits(byte[] b, int p)
		{
			return (int)(b[p] & byte.MaxValue) | (int)(b[p + 1] & byte.MaxValue) << 8 | (int)(b[p + 2] & byte.MaxValue) << 16 | (int)(b[p + 3] & byte.MaxValue) << 24;
		}

		// Token: 0x06001E17 RID: 7703 RVA: 0x000B0114 File Offset: 0x000B0114
		private void Bits32ToBytes(int inData, byte[] b, int offset)
		{
			b[offset] = (byte)inData;
			b[offset + 1] = (byte)(inData >> 8);
			b[offset + 2] = (byte)(inData >> 16);
			b[offset + 3] = (byte)(inData >> 24);
		}

		// Token: 0x040013C4 RID: 5060
		private const int P_00 = 1;

		// Token: 0x040013C5 RID: 5061
		private const int P_01 = 0;

		// Token: 0x040013C6 RID: 5062
		private const int P_02 = 0;

		// Token: 0x040013C7 RID: 5063
		private const int P_03 = 1;

		// Token: 0x040013C8 RID: 5064
		private const int P_04 = 1;

		// Token: 0x040013C9 RID: 5065
		private const int P_10 = 0;

		// Token: 0x040013CA RID: 5066
		private const int P_11 = 0;

		// Token: 0x040013CB RID: 5067
		private const int P_12 = 1;

		// Token: 0x040013CC RID: 5068
		private const int P_13 = 1;

		// Token: 0x040013CD RID: 5069
		private const int P_14 = 0;

		// Token: 0x040013CE RID: 5070
		private const int P_20 = 1;

		// Token: 0x040013CF RID: 5071
		private const int P_21 = 1;

		// Token: 0x040013D0 RID: 5072
		private const int P_22 = 0;

		// Token: 0x040013D1 RID: 5073
		private const int P_23 = 0;

		// Token: 0x040013D2 RID: 5074
		private const int P_24 = 0;

		// Token: 0x040013D3 RID: 5075
		private const int P_30 = 0;

		// Token: 0x040013D4 RID: 5076
		private const int P_31 = 1;

		// Token: 0x040013D5 RID: 5077
		private const int P_32 = 1;

		// Token: 0x040013D6 RID: 5078
		private const int P_33 = 0;

		// Token: 0x040013D7 RID: 5079
		private const int P_34 = 1;

		// Token: 0x040013D8 RID: 5080
		private const int GF256_FDBK = 361;

		// Token: 0x040013D9 RID: 5081
		private const int GF256_FDBK_2 = 180;

		// Token: 0x040013DA RID: 5082
		private const int GF256_FDBK_4 = 90;

		// Token: 0x040013DB RID: 5083
		private const int RS_GF_FDBK = 333;

		// Token: 0x040013DC RID: 5084
		private const int ROUNDS = 16;

		// Token: 0x040013DD RID: 5085
		private const int MAX_ROUNDS = 16;

		// Token: 0x040013DE RID: 5086
		private const int BLOCK_SIZE = 16;

		// Token: 0x040013DF RID: 5087
		private const int MAX_KEY_BITS = 256;

		// Token: 0x040013E0 RID: 5088
		private const int INPUT_WHITEN = 0;

		// Token: 0x040013E1 RID: 5089
		private const int OUTPUT_WHITEN = 4;

		// Token: 0x040013E2 RID: 5090
		private const int ROUND_SUBKEYS = 8;

		// Token: 0x040013E3 RID: 5091
		private const int TOTAL_SUBKEYS = 40;

		// Token: 0x040013E4 RID: 5092
		private const int SK_STEP = 33686018;

		// Token: 0x040013E5 RID: 5093
		private const int SK_BUMP = 16843009;

		// Token: 0x040013E6 RID: 5094
		private const int SK_ROTL = 9;

		// Token: 0x040013E7 RID: 5095
		private static readonly byte[,] P = new byte[,]
		{
			{
				169,
				103,
				179,
				232,
				4,
				253,
				163,
				118,
				154,
				146,
				128,
				120,
				228,
				221,
				209,
				56,
				13,
				198,
				53,
				152,
				24,
				247,
				236,
				108,
				67,
				117,
				55,
				38,
				250,
				19,
				148,
				72,
				242,
				208,
				139,
				48,
				132,
				84,
				223,
				35,
				25,
				91,
				61,
				89,
				243,
				174,
				162,
				130,
				99,
				1,
				131,
				46,
				217,
				81,
				155,
				124,
				166,
				235,
				165,
				190,
				22,
				12,
				227,
				97,
				192,
				140,
				58,
				245,
				115,
				44,
				37,
				11,
				187,
				78,
				137,
				107,
				83,
				106,
				180,
				241,
				225,
				230,
				189,
				69,
				226,
				244,
				182,
				102,
				204,
				149,
				3,
				86,
				212,
				28,
				30,
				215,
				251,
				195,
				142,
				181,
				233,
				207,
				191,
				186,
				234,
				119,
				57,
				175,
				51,
				201,
				98,
				113,
				129,
				121,
				9,
				173,
				36,
				205,
				249,
				216,
				229,
				197,
				185,
				77,
				68,
				8,
				134,
				231,
				161,
				29,
				170,
				237,
				6,
				112,
				178,
				210,
				65,
				123,
				160,
				17,
				49,
				194,
				39,
				144,
				32,
				246,
				96,
				byte.MaxValue,
				150,
				92,
				177,
				171,
				158,
				156,
				82,
				27,
				95,
				147,
				10,
				239,
				145,
				133,
				73,
				238,
				45,
				79,
				143,
				59,
				71,
				135,
				109,
				70,
				214,
				62,
				105,
				100,
				42,
				206,
				203,
				47,
				252,
				151,
				5,
				122,
				172,
				127,
				213,
				26,
				75,
				14,
				167,
				90,
				40,
				20,
				63,
				41,
				136,
				60,
				76,
				2,
				184,
				218,
				176,
				23,
				85,
				31,
				138,
				125,
				87,
				199,
				141,
				116,
				183,
				196,
				159,
				114,
				126,
				21,
				34,
				18,
				88,
				7,
				153,
				52,
				110,
				80,
				222,
				104,
				101,
				188,
				219,
				248,
				200,
				168,
				43,
				64,
				220,
				254,
				50,
				164,
				202,
				16,
				33,
				240,
				211,
				93,
				15,
				0,
				111,
				157,
				54,
				66,
				74,
				94,
				193,
				224
			},
			{
				117,
				243,
				198,
				244,
				219,
				123,
				251,
				200,
				74,
				211,
				230,
				107,
				69,
				125,
				232,
				75,
				214,
				50,
				216,
				253,
				55,
				113,
				241,
				225,
				48,
				15,
				248,
				27,
				135,
				250,
				6,
				63,
				94,
				186,
				174,
				91,
				138,
				0,
				188,
				157,
				109,
				193,
				177,
				14,
				128,
				93,
				210,
				213,
				160,
				132,
				7,
				20,
				181,
				144,
				44,
				163,
				178,
				115,
				76,
				84,
				146,
				116,
				54,
				81,
				56,
				176,
				189,
				90,
				252,
				96,
				98,
				150,
				108,
				66,
				247,
				16,
				124,
				40,
				39,
				140,
				19,
				149,
				156,
				199,
				36,
				70,
				59,
				112,
				202,
				227,
				133,
				203,
				17,
				208,
				147,
				184,
				166,
				131,
				32,
				byte.MaxValue,
				159,
				119,
				195,
				204,
				3,
				111,
				8,
				191,
				64,
				231,
				43,
				226,
				121,
				12,
				170,
				130,
				65,
				58,
				234,
				185,
				228,
				154,
				164,
				151,
				126,
				218,
				122,
				23,
				102,
				148,
				161,
				29,
				61,
				240,
				222,
				179,
				11,
				114,
				167,
				28,
				239,
				209,
				83,
				62,
				143,
				51,
				38,
				95,
				236,
				118,
				42,
				73,
				129,
				136,
				238,
				33,
				196,
				26,
				235,
				217,
				197,
				57,
				153,
				205,
				173,
				49,
				139,
				1,
				24,
				35,
				221,
				31,
				78,
				45,
				249,
				72,
				79,
				242,
				101,
				142,
				120,
				92,
				88,
				25,
				141,
				229,
				152,
				87,
				103,
				127,
				5,
				100,
				175,
				99,
				182,
				254,
				245,
				183,
				60,
				165,
				206,
				233,
				104,
				68,
				224,
				77,
				67,
				105,
				41,
				46,
				172,
				21,
				89,
				168,
				10,
				158,
				110,
				71,
				223,
				52,
				53,
				106,
				207,
				220,
				34,
				201,
				192,
				155,
				137,
				212,
				237,
				171,
				18,
				162,
				13,
				82,
				187,
				2,
				47,
				169,
				215,
				97,
				30,
				180,
				80,
				4,
				246,
				194,
				22,
				37,
				134,
				86,
				85,
				9,
				190,
				145
			}
		};

		// Token: 0x040013E8 RID: 5096
		private bool encrypting;

		// Token: 0x040013E9 RID: 5097
		private int[] gMDS0 = new int[256];

		// Token: 0x040013EA RID: 5098
		private int[] gMDS1 = new int[256];

		// Token: 0x040013EB RID: 5099
		private int[] gMDS2 = new int[256];

		// Token: 0x040013EC RID: 5100
		private int[] gMDS3 = new int[256];

		// Token: 0x040013ED RID: 5101
		private int[] gSubKeys;

		// Token: 0x040013EE RID: 5102
		private int[] gSBox;

		// Token: 0x040013EF RID: 5103
		private int k64Cnt;

		// Token: 0x040013F0 RID: 5104
		private byte[] workingKey;
	}
}
