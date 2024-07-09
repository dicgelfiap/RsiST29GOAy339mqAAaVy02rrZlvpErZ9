using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Macs
{
	// Token: 0x020003E9 RID: 1001
	public class Gost28147Mac : IMac
	{
		// Token: 0x06001FB7 RID: 8119 RVA: 0x000B8448 File Offset: 0x000B8448
		public Gost28147Mac()
		{
			this.mac = new byte[8];
			this.buf = new byte[8];
			this.bufOff = 0;
		}

		// Token: 0x06001FB8 RID: 8120 RVA: 0x000B84A8 File Offset: 0x000B84A8
		private static int[] GenerateWorkingKey(byte[] userKey)
		{
			if (userKey.Length != 32)
			{
				throw new ArgumentException("Key length invalid. Key needs to be 32 byte - 256 bit!!!");
			}
			int[] array = new int[8];
			for (int num = 0; num != 8; num++)
			{
				array[num] = Gost28147Mac.bytesToint(userKey, num * 4);
			}
			return array;
		}

		// Token: 0x06001FB9 RID: 8121 RVA: 0x000B84F4 File Offset: 0x000B84F4
		public void Init(ICipherParameters parameters)
		{
			this.Reset();
			this.buf = new byte[8];
			this.macIV = null;
			if (parameters is ParametersWithSBox)
			{
				ParametersWithSBox parametersWithSBox = (ParametersWithSBox)parameters;
				parametersWithSBox.GetSBox().CopyTo(this.S, 0);
				if (parametersWithSBox.Parameters != null)
				{
					this.workingKey = Gost28147Mac.GenerateWorkingKey(((KeyParameter)parametersWithSBox.Parameters).GetKey());
					return;
				}
				return;
			}
			else
			{
				if (parameters is KeyParameter)
				{
					this.workingKey = Gost28147Mac.GenerateWorkingKey(((KeyParameter)parameters).GetKey());
					return;
				}
				if (parameters is ParametersWithIV)
				{
					ParametersWithIV parametersWithIV = (ParametersWithIV)parameters;
					this.workingKey = Gost28147Mac.GenerateWorkingKey(((KeyParameter)parametersWithIV.Parameters).GetKey());
					Array.Copy(parametersWithIV.GetIV(), 0, this.mac, 0, this.mac.Length);
					this.macIV = parametersWithIV.GetIV();
					return;
				}
				throw new ArgumentException("invalid parameter passed to Gost28147 init - " + Platform.GetTypeName(parameters));
			}
		}

		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x06001FBA RID: 8122 RVA: 0x000B85F8 File Offset: 0x000B85F8
		public string AlgorithmName
		{
			get
			{
				return "Gost28147Mac";
			}
		}

		// Token: 0x06001FBB RID: 8123 RVA: 0x000B8600 File Offset: 0x000B8600
		public int GetMacSize()
		{
			return 4;
		}

		// Token: 0x06001FBC RID: 8124 RVA: 0x000B8604 File Offset: 0x000B8604
		private int gost28147_mainStep(int n1, int key)
		{
			int num = key + n1;
			int num2 = (int)this.S[num & 15];
			num2 += (int)this.S[16 + (num >> 4 & 15)] << 4;
			num2 += (int)this.S[32 + (num >> 8 & 15)] << 8;
			num2 += (int)this.S[48 + (num >> 12 & 15)] << 12;
			num2 += (int)this.S[64 + (num >> 16 & 15)] << 16;
			num2 += (int)this.S[80 + (num >> 20 & 15)] << 20;
			num2 += (int)this.S[96 + (num >> 24 & 15)] << 24;
			num2 += (int)this.S[112 + (num >> 28 & 15)] << 28;
			int num3 = num2 << 11;
			int num4 = (int)((uint)num2 >> 21);
			return num3 | num4;
		}

		// Token: 0x06001FBD RID: 8125 RVA: 0x000B86D0 File Offset: 0x000B86D0
		private void gost28147MacFunc(int[] workingKey, byte[] input, int inOff, byte[] output, int outOff)
		{
			int num = Gost28147Mac.bytesToint(input, inOff);
			int num2 = Gost28147Mac.bytesToint(input, inOff + 4);
			for (int i = 0; i < 2; i++)
			{
				for (int j = 0; j < 8; j++)
				{
					int num3 = num;
					num = (num2 ^ this.gost28147_mainStep(num, workingKey[j]));
					num2 = num3;
				}
			}
			Gost28147Mac.intTobytes(num, output, outOff);
			Gost28147Mac.intTobytes(num2, output, outOff + 4);
		}

		// Token: 0x06001FBE RID: 8126 RVA: 0x000B873C File Offset: 0x000B873C
		private static int bytesToint(byte[] input, int inOff)
		{
			return (int)((long)((long)input[inOff + 3] << 24) & (long)((ulong)-16777216)) + ((int)input[inOff + 2] << 16 & 16711680) + ((int)input[inOff + 1] << 8 & 65280) + (int)(input[inOff] & byte.MaxValue);
		}

		// Token: 0x06001FBF RID: 8127 RVA: 0x000B8778 File Offset: 0x000B8778
		private static void intTobytes(int num, byte[] output, int outOff)
		{
			output[outOff + 3] = (byte)(num >> 24);
			output[outOff + 2] = (byte)(num >> 16);
			output[outOff + 1] = (byte)(num >> 8);
			output[outOff] = (byte)num;
		}

		// Token: 0x06001FC0 RID: 8128 RVA: 0x000B879C File Offset: 0x000B879C
		private static byte[] CM5func(byte[] buf, int bufOff, byte[] mac)
		{
			byte[] array = new byte[buf.Length - bufOff];
			Array.Copy(buf, bufOff, array, 0, mac.Length);
			for (int num = 0; num != mac.Length; num++)
			{
				array[num] ^= mac[num];
			}
			return array;
		}

		// Token: 0x06001FC1 RID: 8129 RVA: 0x000B87E4 File Offset: 0x000B87E4
		public void Update(byte input)
		{
			if (this.bufOff == this.buf.Length)
			{
				byte[] array = new byte[this.buf.Length];
				Array.Copy(this.buf, 0, array, 0, this.mac.Length);
				if (this.firstStep)
				{
					this.firstStep = false;
					if (this.macIV != null)
					{
						array = Gost28147Mac.CM5func(this.buf, 0, this.macIV);
					}
				}
				else
				{
					array = Gost28147Mac.CM5func(this.buf, 0, this.mac);
				}
				this.gost28147MacFunc(this.workingKey, array, 0, this.mac, 0);
				this.bufOff = 0;
			}
			this.buf[this.bufOff++] = input;
		}

		// Token: 0x06001FC2 RID: 8130 RVA: 0x000B88AC File Offset: 0x000B88AC
		public void BlockUpdate(byte[] input, int inOff, int len)
		{
			if (len < 0)
			{
				throw new ArgumentException("Can't have a negative input length!");
			}
			int num = 8 - this.bufOff;
			if (len > num)
			{
				Array.Copy(input, inOff, this.buf, this.bufOff, num);
				byte[] array = new byte[this.buf.Length];
				Array.Copy(this.buf, 0, array, 0, this.mac.Length);
				if (this.firstStep)
				{
					this.firstStep = false;
					if (this.macIV != null)
					{
						array = Gost28147Mac.CM5func(this.buf, 0, this.macIV);
					}
				}
				else
				{
					array = Gost28147Mac.CM5func(this.buf, 0, this.mac);
				}
				this.gost28147MacFunc(this.workingKey, array, 0, this.mac, 0);
				this.bufOff = 0;
				len -= num;
				inOff += num;
				while (len > 8)
				{
					array = Gost28147Mac.CM5func(input, inOff, this.mac);
					this.gost28147MacFunc(this.workingKey, array, 0, this.mac, 0);
					len -= 8;
					inOff += 8;
				}
			}
			Array.Copy(input, inOff, this.buf, this.bufOff, len);
			this.bufOff += len;
		}

		// Token: 0x06001FC3 RID: 8131 RVA: 0x000B89E0 File Offset: 0x000B89E0
		public int DoFinal(byte[] output, int outOff)
		{
			while (this.bufOff < 8)
			{
				this.buf[this.bufOff++] = 0;
			}
			byte[] array = new byte[this.buf.Length];
			Array.Copy(this.buf, 0, array, 0, this.mac.Length);
			if (this.firstStep)
			{
				this.firstStep = false;
			}
			else
			{
				array = Gost28147Mac.CM5func(this.buf, 0, this.mac);
			}
			this.gost28147MacFunc(this.workingKey, array, 0, this.mac, 0);
			Array.Copy(this.mac, this.mac.Length / 2 - 4, output, outOff, 4);
			this.Reset();
			return 4;
		}

		// Token: 0x06001FC4 RID: 8132 RVA: 0x000B8A9C File Offset: 0x000B8A9C
		public void Reset()
		{
			Array.Clear(this.buf, 0, this.buf.Length);
			this.bufOff = 0;
			this.firstStep = true;
		}

		// Token: 0x040014BD RID: 5309
		private const int blockSize = 8;

		// Token: 0x040014BE RID: 5310
		private const int macSize = 4;

		// Token: 0x040014BF RID: 5311
		private int bufOff;

		// Token: 0x040014C0 RID: 5312
		private byte[] buf;

		// Token: 0x040014C1 RID: 5313
		private byte[] mac;

		// Token: 0x040014C2 RID: 5314
		private bool firstStep = true;

		// Token: 0x040014C3 RID: 5315
		private int[] workingKey;

		// Token: 0x040014C4 RID: 5316
		private byte[] macIV = null;

		// Token: 0x040014C5 RID: 5317
		private byte[] S = new byte[]
		{
			9,
			6,
			3,
			2,
			8,
			11,
			1,
			7,
			10,
			4,
			14,
			15,
			12,
			0,
			13,
			5,
			3,
			7,
			14,
			9,
			8,
			10,
			15,
			0,
			5,
			2,
			6,
			12,
			11,
			4,
			13,
			1,
			14,
			4,
			6,
			2,
			11,
			3,
			13,
			8,
			12,
			15,
			5,
			10,
			0,
			7,
			1,
			9,
			14,
			7,
			10,
			12,
			13,
			1,
			3,
			9,
			0,
			2,
			11,
			4,
			15,
			8,
			5,
			6,
			11,
			5,
			1,
			9,
			8,
			13,
			15,
			0,
			14,
			4,
			2,
			3,
			12,
			7,
			10,
			6,
			3,
			10,
			13,
			12,
			1,
			2,
			0,
			11,
			7,
			5,
			9,
			4,
			8,
			15,
			14,
			6,
			1,
			13,
			2,
			9,
			7,
			10,
			6,
			0,
			8,
			12,
			4,
			5,
			15,
			3,
			11,
			14,
			11,
			10,
			15,
			5,
			0,
			12,
			14,
			8,
			6,
			2,
			3,
			9,
			1,
			7,
			13,
			4
		};
	}
}
