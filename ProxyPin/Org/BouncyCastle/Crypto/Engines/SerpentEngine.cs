﻿using System;
using Org.BouncyCastle.Crypto.Utilities;

namespace Org.BouncyCastle.Crypto.Engines
{
	// Token: 0x020003A5 RID: 933
	public sealed class SerpentEngine : SerpentEngineBase
	{
		// Token: 0x06001DB6 RID: 7606 RVA: 0x000A9138 File Offset: 0x000A9138
		protected override int[] MakeWorkingKey(byte[] key)
		{
			int[] array = new int[16];
			int num = 0;
			int num2 = 0;
			while (num2 + 4 < key.Length)
			{
				array[num++] = (int)Pack.LE_To_UInt32(key, num2);
				num2 += 4;
			}
			if (num2 % 4 == 0)
			{
				array[num++] = (int)Pack.LE_To_UInt32(key, num2);
				if (num < 8)
				{
					array[num] = 1;
				}
				int num3 = 132;
				int[] array2 = new int[num3];
				for (int i = 8; i < 16; i++)
				{
					array[i] = SerpentEngineBase.RotateLeft(array[i - 8] ^ array[i - 5] ^ array[i - 3] ^ array[i - 1] ^ -1640531527 ^ i - 8, 11);
				}
				Array.Copy(array, 8, array2, 0, 8);
				for (int j = 8; j < num3; j++)
				{
					array2[j] = SerpentEngineBase.RotateLeft(array2[j - 8] ^ array2[j - 5] ^ array2[j - 3] ^ array2[j - 1] ^ -1640531527 ^ j, 11);
				}
				base.Sb3(array2[0], array2[1], array2[2], array2[3]);
				array2[0] = this.X0;
				array2[1] = this.X1;
				array2[2] = this.X2;
				array2[3] = this.X3;
				base.Sb2(array2[4], array2[5], array2[6], array2[7]);
				array2[4] = this.X0;
				array2[5] = this.X1;
				array2[6] = this.X2;
				array2[7] = this.X3;
				base.Sb1(array2[8], array2[9], array2[10], array2[11]);
				array2[8] = this.X0;
				array2[9] = this.X1;
				array2[10] = this.X2;
				array2[11] = this.X3;
				base.Sb0(array2[12], array2[13], array2[14], array2[15]);
				array2[12] = this.X0;
				array2[13] = this.X1;
				array2[14] = this.X2;
				array2[15] = this.X3;
				base.Sb7(array2[16], array2[17], array2[18], array2[19]);
				array2[16] = this.X0;
				array2[17] = this.X1;
				array2[18] = this.X2;
				array2[19] = this.X3;
				base.Sb6(array2[20], array2[21], array2[22], array2[23]);
				array2[20] = this.X0;
				array2[21] = this.X1;
				array2[22] = this.X2;
				array2[23] = this.X3;
				base.Sb5(array2[24], array2[25], array2[26], array2[27]);
				array2[24] = this.X0;
				array2[25] = this.X1;
				array2[26] = this.X2;
				array2[27] = this.X3;
				base.Sb4(array2[28], array2[29], array2[30], array2[31]);
				array2[28] = this.X0;
				array2[29] = this.X1;
				array2[30] = this.X2;
				array2[31] = this.X3;
				base.Sb3(array2[32], array2[33], array2[34], array2[35]);
				array2[32] = this.X0;
				array2[33] = this.X1;
				array2[34] = this.X2;
				array2[35] = this.X3;
				base.Sb2(array2[36], array2[37], array2[38], array2[39]);
				array2[36] = this.X0;
				array2[37] = this.X1;
				array2[38] = this.X2;
				array2[39] = this.X3;
				base.Sb1(array2[40], array2[41], array2[42], array2[43]);
				array2[40] = this.X0;
				array2[41] = this.X1;
				array2[42] = this.X2;
				array2[43] = this.X3;
				base.Sb0(array2[44], array2[45], array2[46], array2[47]);
				array2[44] = this.X0;
				array2[45] = this.X1;
				array2[46] = this.X2;
				array2[47] = this.X3;
				base.Sb7(array2[48], array2[49], array2[50], array2[51]);
				array2[48] = this.X0;
				array2[49] = this.X1;
				array2[50] = this.X2;
				array2[51] = this.X3;
				base.Sb6(array2[52], array2[53], array2[54], array2[55]);
				array2[52] = this.X0;
				array2[53] = this.X1;
				array2[54] = this.X2;
				array2[55] = this.X3;
				base.Sb5(array2[56], array2[57], array2[58], array2[59]);
				array2[56] = this.X0;
				array2[57] = this.X1;
				array2[58] = this.X2;
				array2[59] = this.X3;
				base.Sb4(array2[60], array2[61], array2[62], array2[63]);
				array2[60] = this.X0;
				array2[61] = this.X1;
				array2[62] = this.X2;
				array2[63] = this.X3;
				base.Sb3(array2[64], array2[65], array2[66], array2[67]);
				array2[64] = this.X0;
				array2[65] = this.X1;
				array2[66] = this.X2;
				array2[67] = this.X3;
				base.Sb2(array2[68], array2[69], array2[70], array2[71]);
				array2[68] = this.X0;
				array2[69] = this.X1;
				array2[70] = this.X2;
				array2[71] = this.X3;
				base.Sb1(array2[72], array2[73], array2[74], array2[75]);
				array2[72] = this.X0;
				array2[73] = this.X1;
				array2[74] = this.X2;
				array2[75] = this.X3;
				base.Sb0(array2[76], array2[77], array2[78], array2[79]);
				array2[76] = this.X0;
				array2[77] = this.X1;
				array2[78] = this.X2;
				array2[79] = this.X3;
				base.Sb7(array2[80], array2[81], array2[82], array2[83]);
				array2[80] = this.X0;
				array2[81] = this.X1;
				array2[82] = this.X2;
				array2[83] = this.X3;
				base.Sb6(array2[84], array2[85], array2[86], array2[87]);
				array2[84] = this.X0;
				array2[85] = this.X1;
				array2[86] = this.X2;
				array2[87] = this.X3;
				base.Sb5(array2[88], array2[89], array2[90], array2[91]);
				array2[88] = this.X0;
				array2[89] = this.X1;
				array2[90] = this.X2;
				array2[91] = this.X3;
				base.Sb4(array2[92], array2[93], array2[94], array2[95]);
				array2[92] = this.X0;
				array2[93] = this.X1;
				array2[94] = this.X2;
				array2[95] = this.X3;
				base.Sb3(array2[96], array2[97], array2[98], array2[99]);
				array2[96] = this.X0;
				array2[97] = this.X1;
				array2[98] = this.X2;
				array2[99] = this.X3;
				base.Sb2(array2[100], array2[101], array2[102], array2[103]);
				array2[100] = this.X0;
				array2[101] = this.X1;
				array2[102] = this.X2;
				array2[103] = this.X3;
				base.Sb1(array2[104], array2[105], array2[106], array2[107]);
				array2[104] = this.X0;
				array2[105] = this.X1;
				array2[106] = this.X2;
				array2[107] = this.X3;
				base.Sb0(array2[108], array2[109], array2[110], array2[111]);
				array2[108] = this.X0;
				array2[109] = this.X1;
				array2[110] = this.X2;
				array2[111] = this.X3;
				base.Sb7(array2[112], array2[113], array2[114], array2[115]);
				array2[112] = this.X0;
				array2[113] = this.X1;
				array2[114] = this.X2;
				array2[115] = this.X3;
				base.Sb6(array2[116], array2[117], array2[118], array2[119]);
				array2[116] = this.X0;
				array2[117] = this.X1;
				array2[118] = this.X2;
				array2[119] = this.X3;
				base.Sb5(array2[120], array2[121], array2[122], array2[123]);
				array2[120] = this.X0;
				array2[121] = this.X1;
				array2[122] = this.X2;
				array2[123] = this.X3;
				base.Sb4(array2[124], array2[125], array2[126], array2[127]);
				array2[124] = this.X0;
				array2[125] = this.X1;
				array2[126] = this.X2;
				array2[127] = this.X3;
				base.Sb3(array2[128], array2[129], array2[130], array2[131]);
				array2[128] = this.X0;
				array2[129] = this.X1;
				array2[130] = this.X2;
				array2[131] = this.X3;
				return array2;
			}
			throw new ArgumentException("key must be a multiple of 4 bytes");
		}

		// Token: 0x06001DB7 RID: 7607 RVA: 0x000A9B5C File Offset: 0x000A9B5C
		protected override void EncryptBlock(byte[] input, int inOff, byte[] output, int outOff)
		{
			this.X0 = (int)Pack.LE_To_UInt32(input, inOff);
			this.X1 = (int)Pack.LE_To_UInt32(input, inOff + 4);
			this.X2 = (int)Pack.LE_To_UInt32(input, inOff + 8);
			this.X3 = (int)Pack.LE_To_UInt32(input, inOff + 12);
			base.Sb0(this.wKey[0] ^ this.X0, this.wKey[1] ^ this.X1, this.wKey[2] ^ this.X2, this.wKey[3] ^ this.X3);
			base.LT();
			base.Sb1(this.wKey[4] ^ this.X0, this.wKey[5] ^ this.X1, this.wKey[6] ^ this.X2, this.wKey[7] ^ this.X3);
			base.LT();
			base.Sb2(this.wKey[8] ^ this.X0, this.wKey[9] ^ this.X1, this.wKey[10] ^ this.X2, this.wKey[11] ^ this.X3);
			base.LT();
			base.Sb3(this.wKey[12] ^ this.X0, this.wKey[13] ^ this.X1, this.wKey[14] ^ this.X2, this.wKey[15] ^ this.X3);
			base.LT();
			base.Sb4(this.wKey[16] ^ this.X0, this.wKey[17] ^ this.X1, this.wKey[18] ^ this.X2, this.wKey[19] ^ this.X3);
			base.LT();
			base.Sb5(this.wKey[20] ^ this.X0, this.wKey[21] ^ this.X1, this.wKey[22] ^ this.X2, this.wKey[23] ^ this.X3);
			base.LT();
			base.Sb6(this.wKey[24] ^ this.X0, this.wKey[25] ^ this.X1, this.wKey[26] ^ this.X2, this.wKey[27] ^ this.X3);
			base.LT();
			base.Sb7(this.wKey[28] ^ this.X0, this.wKey[29] ^ this.X1, this.wKey[30] ^ this.X2, this.wKey[31] ^ this.X3);
			base.LT();
			base.Sb0(this.wKey[32] ^ this.X0, this.wKey[33] ^ this.X1, this.wKey[34] ^ this.X2, this.wKey[35] ^ this.X3);
			base.LT();
			base.Sb1(this.wKey[36] ^ this.X0, this.wKey[37] ^ this.X1, this.wKey[38] ^ this.X2, this.wKey[39] ^ this.X3);
			base.LT();
			base.Sb2(this.wKey[40] ^ this.X0, this.wKey[41] ^ this.X1, this.wKey[42] ^ this.X2, this.wKey[43] ^ this.X3);
			base.LT();
			base.Sb3(this.wKey[44] ^ this.X0, this.wKey[45] ^ this.X1, this.wKey[46] ^ this.X2, this.wKey[47] ^ this.X3);
			base.LT();
			base.Sb4(this.wKey[48] ^ this.X0, this.wKey[49] ^ this.X1, this.wKey[50] ^ this.X2, this.wKey[51] ^ this.X3);
			base.LT();
			base.Sb5(this.wKey[52] ^ this.X0, this.wKey[53] ^ this.X1, this.wKey[54] ^ this.X2, this.wKey[55] ^ this.X3);
			base.LT();
			base.Sb6(this.wKey[56] ^ this.X0, this.wKey[57] ^ this.X1, this.wKey[58] ^ this.X2, this.wKey[59] ^ this.X3);
			base.LT();
			base.Sb7(this.wKey[60] ^ this.X0, this.wKey[61] ^ this.X1, this.wKey[62] ^ this.X2, this.wKey[63] ^ this.X3);
			base.LT();
			base.Sb0(this.wKey[64] ^ this.X0, this.wKey[65] ^ this.X1, this.wKey[66] ^ this.X2, this.wKey[67] ^ this.X3);
			base.LT();
			base.Sb1(this.wKey[68] ^ this.X0, this.wKey[69] ^ this.X1, this.wKey[70] ^ this.X2, this.wKey[71] ^ this.X3);
			base.LT();
			base.Sb2(this.wKey[72] ^ this.X0, this.wKey[73] ^ this.X1, this.wKey[74] ^ this.X2, this.wKey[75] ^ this.X3);
			base.LT();
			base.Sb3(this.wKey[76] ^ this.X0, this.wKey[77] ^ this.X1, this.wKey[78] ^ this.X2, this.wKey[79] ^ this.X3);
			base.LT();
			base.Sb4(this.wKey[80] ^ this.X0, this.wKey[81] ^ this.X1, this.wKey[82] ^ this.X2, this.wKey[83] ^ this.X3);
			base.LT();
			base.Sb5(this.wKey[84] ^ this.X0, this.wKey[85] ^ this.X1, this.wKey[86] ^ this.X2, this.wKey[87] ^ this.X3);
			base.LT();
			base.Sb6(this.wKey[88] ^ this.X0, this.wKey[89] ^ this.X1, this.wKey[90] ^ this.X2, this.wKey[91] ^ this.X3);
			base.LT();
			base.Sb7(this.wKey[92] ^ this.X0, this.wKey[93] ^ this.X1, this.wKey[94] ^ this.X2, this.wKey[95] ^ this.X3);
			base.LT();
			base.Sb0(this.wKey[96] ^ this.X0, this.wKey[97] ^ this.X1, this.wKey[98] ^ this.X2, this.wKey[99] ^ this.X3);
			base.LT();
			base.Sb1(this.wKey[100] ^ this.X0, this.wKey[101] ^ this.X1, this.wKey[102] ^ this.X2, this.wKey[103] ^ this.X3);
			base.LT();
			base.Sb2(this.wKey[104] ^ this.X0, this.wKey[105] ^ this.X1, this.wKey[106] ^ this.X2, this.wKey[107] ^ this.X3);
			base.LT();
			base.Sb3(this.wKey[108] ^ this.X0, this.wKey[109] ^ this.X1, this.wKey[110] ^ this.X2, this.wKey[111] ^ this.X3);
			base.LT();
			base.Sb4(this.wKey[112] ^ this.X0, this.wKey[113] ^ this.X1, this.wKey[114] ^ this.X2, this.wKey[115] ^ this.X3);
			base.LT();
			base.Sb5(this.wKey[116] ^ this.X0, this.wKey[117] ^ this.X1, this.wKey[118] ^ this.X2, this.wKey[119] ^ this.X3);
			base.LT();
			base.Sb6(this.wKey[120] ^ this.X0, this.wKey[121] ^ this.X1, this.wKey[122] ^ this.X2, this.wKey[123] ^ this.X3);
			base.LT();
			base.Sb7(this.wKey[124] ^ this.X0, this.wKey[125] ^ this.X1, this.wKey[126] ^ this.X2, this.wKey[127] ^ this.X3);
			Pack.UInt32_To_LE((uint)(this.wKey[128] ^ this.X0), output, outOff);
			Pack.UInt32_To_LE((uint)(this.wKey[129] ^ this.X1), output, outOff + 4);
			Pack.UInt32_To_LE((uint)(this.wKey[130] ^ this.X2), output, outOff + 8);
			Pack.UInt32_To_LE((uint)(this.wKey[131] ^ this.X3), output, outOff + 12);
		}

		// Token: 0x06001DB8 RID: 7608 RVA: 0x000AA58C File Offset: 0x000AA58C
		protected override void DecryptBlock(byte[] input, int inOff, byte[] output, int outOff)
		{
			this.X0 = (this.wKey[128] ^ (int)Pack.LE_To_UInt32(input, inOff));
			this.X1 = (this.wKey[129] ^ (int)Pack.LE_To_UInt32(input, inOff + 4));
			this.X2 = (this.wKey[130] ^ (int)Pack.LE_To_UInt32(input, inOff + 8));
			this.X3 = (this.wKey[131] ^ (int)Pack.LE_To_UInt32(input, inOff + 12));
			base.Ib7(this.X0, this.X1, this.X2, this.X3);
			this.X0 ^= this.wKey[124];
			this.X1 ^= this.wKey[125];
			this.X2 ^= this.wKey[126];
			this.X3 ^= this.wKey[127];
			base.InverseLT();
			base.Ib6(this.X0, this.X1, this.X2, this.X3);
			this.X0 ^= this.wKey[120];
			this.X1 ^= this.wKey[121];
			this.X2 ^= this.wKey[122];
			this.X3 ^= this.wKey[123];
			base.InverseLT();
			base.Ib5(this.X0, this.X1, this.X2, this.X3);
			this.X0 ^= this.wKey[116];
			this.X1 ^= this.wKey[117];
			this.X2 ^= this.wKey[118];
			this.X3 ^= this.wKey[119];
			base.InverseLT();
			base.Ib4(this.X0, this.X1, this.X2, this.X3);
			this.X0 ^= this.wKey[112];
			this.X1 ^= this.wKey[113];
			this.X2 ^= this.wKey[114];
			this.X3 ^= this.wKey[115];
			base.InverseLT();
			base.Ib3(this.X0, this.X1, this.X2, this.X3);
			this.X0 ^= this.wKey[108];
			this.X1 ^= this.wKey[109];
			this.X2 ^= this.wKey[110];
			this.X3 ^= this.wKey[111];
			base.InverseLT();
			base.Ib2(this.X0, this.X1, this.X2, this.X3);
			this.X0 ^= this.wKey[104];
			this.X1 ^= this.wKey[105];
			this.X2 ^= this.wKey[106];
			this.X3 ^= this.wKey[107];
			base.InverseLT();
			base.Ib1(this.X0, this.X1, this.X2, this.X3);
			this.X0 ^= this.wKey[100];
			this.X1 ^= this.wKey[101];
			this.X2 ^= this.wKey[102];
			this.X3 ^= this.wKey[103];
			base.InverseLT();
			base.Ib0(this.X0, this.X1, this.X2, this.X3);
			this.X0 ^= this.wKey[96];
			this.X1 ^= this.wKey[97];
			this.X2 ^= this.wKey[98];
			this.X3 ^= this.wKey[99];
			base.InverseLT();
			base.Ib7(this.X0, this.X1, this.X2, this.X3);
			this.X0 ^= this.wKey[92];
			this.X1 ^= this.wKey[93];
			this.X2 ^= this.wKey[94];
			this.X3 ^= this.wKey[95];
			base.InverseLT();
			base.Ib6(this.X0, this.X1, this.X2, this.X3);
			this.X0 ^= this.wKey[88];
			this.X1 ^= this.wKey[89];
			this.X2 ^= this.wKey[90];
			this.X3 ^= this.wKey[91];
			base.InverseLT();
			base.Ib5(this.X0, this.X1, this.X2, this.X3);
			this.X0 ^= this.wKey[84];
			this.X1 ^= this.wKey[85];
			this.X2 ^= this.wKey[86];
			this.X3 ^= this.wKey[87];
			base.InverseLT();
			base.Ib4(this.X0, this.X1, this.X2, this.X3);
			this.X0 ^= this.wKey[80];
			this.X1 ^= this.wKey[81];
			this.X2 ^= this.wKey[82];
			this.X3 ^= this.wKey[83];
			base.InverseLT();
			base.Ib3(this.X0, this.X1, this.X2, this.X3);
			this.X0 ^= this.wKey[76];
			this.X1 ^= this.wKey[77];
			this.X2 ^= this.wKey[78];
			this.X3 ^= this.wKey[79];
			base.InverseLT();
			base.Ib2(this.X0, this.X1, this.X2, this.X3);
			this.X0 ^= this.wKey[72];
			this.X1 ^= this.wKey[73];
			this.X2 ^= this.wKey[74];
			this.X3 ^= this.wKey[75];
			base.InverseLT();
			base.Ib1(this.X0, this.X1, this.X2, this.X3);
			this.X0 ^= this.wKey[68];
			this.X1 ^= this.wKey[69];
			this.X2 ^= this.wKey[70];
			this.X3 ^= this.wKey[71];
			base.InverseLT();
			base.Ib0(this.X0, this.X1, this.X2, this.X3);
			this.X0 ^= this.wKey[64];
			this.X1 ^= this.wKey[65];
			this.X2 ^= this.wKey[66];
			this.X3 ^= this.wKey[67];
			base.InverseLT();
			base.Ib7(this.X0, this.X1, this.X2, this.X3);
			this.X0 ^= this.wKey[60];
			this.X1 ^= this.wKey[61];
			this.X2 ^= this.wKey[62];
			this.X3 ^= this.wKey[63];
			base.InverseLT();
			base.Ib6(this.X0, this.X1, this.X2, this.X3);
			this.X0 ^= this.wKey[56];
			this.X1 ^= this.wKey[57];
			this.X2 ^= this.wKey[58];
			this.X3 ^= this.wKey[59];
			base.InverseLT();
			base.Ib5(this.X0, this.X1, this.X2, this.X3);
			this.X0 ^= this.wKey[52];
			this.X1 ^= this.wKey[53];
			this.X2 ^= this.wKey[54];
			this.X3 ^= this.wKey[55];
			base.InverseLT();
			base.Ib4(this.X0, this.X1, this.X2, this.X3);
			this.X0 ^= this.wKey[48];
			this.X1 ^= this.wKey[49];
			this.X2 ^= this.wKey[50];
			this.X3 ^= this.wKey[51];
			base.InverseLT();
			base.Ib3(this.X0, this.X1, this.X2, this.X3);
			this.X0 ^= this.wKey[44];
			this.X1 ^= this.wKey[45];
			this.X2 ^= this.wKey[46];
			this.X3 ^= this.wKey[47];
			base.InverseLT();
			base.Ib2(this.X0, this.X1, this.X2, this.X3);
			this.X0 ^= this.wKey[40];
			this.X1 ^= this.wKey[41];
			this.X2 ^= this.wKey[42];
			this.X3 ^= this.wKey[43];
			base.InverseLT();
			base.Ib1(this.X0, this.X1, this.X2, this.X3);
			this.X0 ^= this.wKey[36];
			this.X1 ^= this.wKey[37];
			this.X2 ^= this.wKey[38];
			this.X3 ^= this.wKey[39];
			base.InverseLT();
			base.Ib0(this.X0, this.X1, this.X2, this.X3);
			this.X0 ^= this.wKey[32];
			this.X1 ^= this.wKey[33];
			this.X2 ^= this.wKey[34];
			this.X3 ^= this.wKey[35];
			base.InverseLT();
			base.Ib7(this.X0, this.X1, this.X2, this.X3);
			this.X0 ^= this.wKey[28];
			this.X1 ^= this.wKey[29];
			this.X2 ^= this.wKey[30];
			this.X3 ^= this.wKey[31];
			base.InverseLT();
			base.Ib6(this.X0, this.X1, this.X2, this.X3);
			this.X0 ^= this.wKey[24];
			this.X1 ^= this.wKey[25];
			this.X2 ^= this.wKey[26];
			this.X3 ^= this.wKey[27];
			base.InverseLT();
			base.Ib5(this.X0, this.X1, this.X2, this.X3);
			this.X0 ^= this.wKey[20];
			this.X1 ^= this.wKey[21];
			this.X2 ^= this.wKey[22];
			this.X3 ^= this.wKey[23];
			base.InverseLT();
			base.Ib4(this.X0, this.X1, this.X2, this.X3);
			this.X0 ^= this.wKey[16];
			this.X1 ^= this.wKey[17];
			this.X2 ^= this.wKey[18];
			this.X3 ^= this.wKey[19];
			base.InverseLT();
			base.Ib3(this.X0, this.X1, this.X2, this.X3);
			this.X0 ^= this.wKey[12];
			this.X1 ^= this.wKey[13];
			this.X2 ^= this.wKey[14];
			this.X3 ^= this.wKey[15];
			base.InverseLT();
			base.Ib2(this.X0, this.X1, this.X2, this.X3);
			this.X0 ^= this.wKey[8];
			this.X1 ^= this.wKey[9];
			this.X2 ^= this.wKey[10];
			this.X3 ^= this.wKey[11];
			base.InverseLT();
			base.Ib1(this.X0, this.X1, this.X2, this.X3);
			this.X0 ^= this.wKey[4];
			this.X1 ^= this.wKey[5];
			this.X2 ^= this.wKey[6];
			this.X3 ^= this.wKey[7];
			base.InverseLT();
			base.Ib0(this.X0, this.X1, this.X2, this.X3);
			Pack.UInt32_To_LE((uint)(this.X0 ^ this.wKey[0]), output, outOff);
			Pack.UInt32_To_LE((uint)(this.X1 ^ this.wKey[1]), output, outOff + 4);
			Pack.UInt32_To_LE((uint)(this.X2 ^ this.wKey[2]), output, outOff + 8);
			Pack.UInt32_To_LE((uint)(this.X3 ^ this.wKey[3]), output, outOff + 12);
		}
	}
}
