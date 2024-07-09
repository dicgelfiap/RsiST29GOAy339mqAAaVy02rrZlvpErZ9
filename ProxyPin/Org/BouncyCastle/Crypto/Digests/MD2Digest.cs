using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Digests
{
	// Token: 0x02000358 RID: 856
	public class MD2Digest : IDigest, IMemoable
	{
		// Token: 0x060019D5 RID: 6613 RVA: 0x00086BF0 File Offset: 0x00086BF0
		public MD2Digest()
		{
			this.Reset();
		}

		// Token: 0x060019D6 RID: 6614 RVA: 0x00086C28 File Offset: 0x00086C28
		public MD2Digest(MD2Digest t)
		{
			this.CopyIn(t);
		}

		// Token: 0x060019D7 RID: 6615 RVA: 0x00086C60 File Offset: 0x00086C60
		private void CopyIn(MD2Digest t)
		{
			Array.Copy(t.X, 0, this.X, 0, t.X.Length);
			this.xOff = t.xOff;
			Array.Copy(t.M, 0, this.M, 0, t.M.Length);
			this.mOff = t.mOff;
			Array.Copy(t.C, 0, this.C, 0, t.C.Length);
			this.COff = t.COff;
		}

		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x060019D8 RID: 6616 RVA: 0x00086CE8 File Offset: 0x00086CE8
		public string AlgorithmName
		{
			get
			{
				return "MD2";
			}
		}

		// Token: 0x060019D9 RID: 6617 RVA: 0x00086CF0 File Offset: 0x00086CF0
		public int GetDigestSize()
		{
			return 16;
		}

		// Token: 0x060019DA RID: 6618 RVA: 0x00086CF4 File Offset: 0x00086CF4
		public int GetByteLength()
		{
			return 16;
		}

		// Token: 0x060019DB RID: 6619 RVA: 0x00086CF8 File Offset: 0x00086CF8
		public int DoFinal(byte[] output, int outOff)
		{
			byte b = (byte)(this.M.Length - this.mOff);
			for (int i = this.mOff; i < this.M.Length; i++)
			{
				this.M[i] = b;
			}
			this.ProcessChecksum(this.M);
			this.ProcessBlock(this.M);
			this.ProcessBlock(this.C);
			Array.Copy(this.X, this.xOff, output, outOff, 16);
			this.Reset();
			return 16;
		}

		// Token: 0x060019DC RID: 6620 RVA: 0x00086D80 File Offset: 0x00086D80
		public void Reset()
		{
			this.xOff = 0;
			for (int num = 0; num != this.X.Length; num++)
			{
				this.X[num] = 0;
			}
			this.mOff = 0;
			for (int num2 = 0; num2 != this.M.Length; num2++)
			{
				this.M[num2] = 0;
			}
			this.COff = 0;
			for (int num3 = 0; num3 != this.C.Length; num3++)
			{
				this.C[num3] = 0;
			}
		}

		// Token: 0x060019DD RID: 6621 RVA: 0x00086E04 File Offset: 0x00086E04
		public void Update(byte input)
		{
			this.M[this.mOff++] = input;
			if (this.mOff == 16)
			{
				this.ProcessChecksum(this.M);
				this.ProcessBlock(this.M);
				this.mOff = 0;
			}
		}

		// Token: 0x060019DE RID: 6622 RVA: 0x00086E5C File Offset: 0x00086E5C
		public void BlockUpdate(byte[] input, int inOff, int length)
		{
			while (this.mOff != 0)
			{
				if (length <= 0)
				{
					break;
				}
				this.Update(input[inOff]);
				inOff++;
				length--;
			}
			while (length > 16)
			{
				Array.Copy(input, inOff, this.M, 0, 16);
				this.ProcessChecksum(this.M);
				this.ProcessBlock(this.M);
				length -= 16;
				inOff += 16;
			}
			while (length > 0)
			{
				this.Update(input[inOff]);
				inOff++;
				length--;
			}
		}

		// Token: 0x060019DF RID: 6623 RVA: 0x00086EF0 File Offset: 0x00086EF0
		internal void ProcessChecksum(byte[] m)
		{
			int num = (int)this.C[15];
			for (int i = 0; i < 16; i++)
			{
				byte[] c;
				IntPtr intPtr;
				(c = this.C)[(int)(intPtr = (IntPtr)i)] = (c[(int)intPtr] ^ MD2Digest.S[((int)m[i] ^ num) & 255]);
				num = (int)this.C[i];
			}
		}

		// Token: 0x060019E0 RID: 6624 RVA: 0x00086F48 File Offset: 0x00086F48
		internal void ProcessBlock(byte[] m)
		{
			for (int i = 0; i < 16; i++)
			{
				this.X[i + 16] = m[i];
				this.X[i + 32] = (m[i] ^ this.X[i]);
			}
			int num = 0;
			for (int j = 0; j < 18; j++)
			{
				for (int k = 0; k < 48; k++)
				{
					byte[] x;
					IntPtr intPtr;
					num = (int)((x = this.X)[(int)(intPtr = (IntPtr)k)] = (x[(int)intPtr] ^ MD2Digest.S[num]));
					num &= 255;
				}
				num = (num + j) % 256;
			}
		}

		// Token: 0x060019E1 RID: 6625 RVA: 0x00086FE8 File Offset: 0x00086FE8
		public IMemoable Copy()
		{
			return new MD2Digest(this);
		}

		// Token: 0x060019E2 RID: 6626 RVA: 0x00086FF0 File Offset: 0x00086FF0
		public void Reset(IMemoable other)
		{
			MD2Digest t = (MD2Digest)other;
			this.CopyIn(t);
		}

		// Token: 0x04001148 RID: 4424
		private const int DigestLength = 16;

		// Token: 0x04001149 RID: 4425
		private const int BYTE_LENGTH = 16;

		// Token: 0x0400114A RID: 4426
		private byte[] X = new byte[48];

		// Token: 0x0400114B RID: 4427
		private int xOff;

		// Token: 0x0400114C RID: 4428
		private byte[] M = new byte[16];

		// Token: 0x0400114D RID: 4429
		private int mOff;

		// Token: 0x0400114E RID: 4430
		private byte[] C = new byte[16];

		// Token: 0x0400114F RID: 4431
		private int COff;

		// Token: 0x04001150 RID: 4432
		private static readonly byte[] S = new byte[]
		{
			41,
			46,
			67,
			201,
			162,
			216,
			124,
			1,
			61,
			54,
			84,
			161,
			236,
			240,
			6,
			19,
			98,
			167,
			5,
			243,
			192,
			199,
			115,
			140,
			152,
			147,
			43,
			217,
			188,
			76,
			130,
			202,
			30,
			155,
			87,
			60,
			253,
			212,
			224,
			22,
			103,
			66,
			111,
			24,
			138,
			23,
			229,
			18,
			190,
			78,
			196,
			214,
			218,
			158,
			222,
			73,
			160,
			251,
			245,
			142,
			187,
			47,
			238,
			122,
			169,
			104,
			121,
			145,
			21,
			178,
			7,
			63,
			148,
			194,
			16,
			137,
			11,
			34,
			95,
			33,
			128,
			127,
			93,
			154,
			90,
			144,
			50,
			39,
			53,
			62,
			204,
			231,
			191,
			247,
			151,
			3,
			byte.MaxValue,
			25,
			48,
			179,
			72,
			165,
			181,
			209,
			215,
			94,
			146,
			42,
			172,
			86,
			170,
			198,
			79,
			184,
			56,
			210,
			150,
			164,
			125,
			182,
			118,
			252,
			107,
			226,
			156,
			116,
			4,
			241,
			69,
			157,
			112,
			89,
			100,
			113,
			135,
			32,
			134,
			91,
			207,
			101,
			230,
			45,
			168,
			2,
			27,
			96,
			37,
			173,
			174,
			176,
			185,
			246,
			28,
			70,
			97,
			105,
			52,
			64,
			126,
			15,
			85,
			71,
			163,
			35,
			221,
			81,
			175,
			58,
			195,
			92,
			249,
			206,
			186,
			197,
			234,
			38,
			44,
			83,
			13,
			110,
			133,
			40,
			132,
			9,
			211,
			223,
			205,
			244,
			65,
			129,
			77,
			82,
			106,
			220,
			55,
			200,
			108,
			193,
			171,
			250,
			36,
			225,
			123,
			8,
			12,
			189,
			177,
			74,
			120,
			136,
			149,
			139,
			227,
			99,
			232,
			109,
			233,
			203,
			213,
			254,
			59,
			0,
			29,
			57,
			242,
			239,
			183,
			14,
			102,
			88,
			208,
			228,
			166,
			119,
			114,
			248,
			235,
			117,
			75,
			10,
			49,
			68,
			80,
			180,
			143,
			237,
			31,
			26,
			219,
			153,
			141,
			51,
			159,
			17,
			131,
			20
		};
	}
}
