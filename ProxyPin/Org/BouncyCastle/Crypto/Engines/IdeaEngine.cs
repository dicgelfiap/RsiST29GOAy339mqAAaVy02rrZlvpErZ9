using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Engines
{
	// Token: 0x0200038F RID: 911
	public class IdeaEngine : IBlockCipher
	{
		// Token: 0x06001CB2 RID: 7346 RVA: 0x000A2D40 File Offset: 0x000A2D40
		public virtual void Init(bool forEncryption, ICipherParameters parameters)
		{
			if (!(parameters is KeyParameter))
			{
				throw new ArgumentException("invalid parameter passed to IDEA init - " + Platform.GetTypeName(parameters));
			}
			this.workingKey = this.GenerateWorkingKey(forEncryption, ((KeyParameter)parameters).GetKey());
		}

		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x06001CB3 RID: 7347 RVA: 0x000A2D7C File Offset: 0x000A2D7C
		public virtual string AlgorithmName
		{
			get
			{
				return "IDEA";
			}
		}

		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x06001CB4 RID: 7348 RVA: 0x000A2D84 File Offset: 0x000A2D84
		public virtual bool IsPartialBlockOkay
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001CB5 RID: 7349 RVA: 0x000A2D88 File Offset: 0x000A2D88
		public virtual int GetBlockSize()
		{
			return 8;
		}

		// Token: 0x06001CB6 RID: 7350 RVA: 0x000A2D8C File Offset: 0x000A2D8C
		public virtual int ProcessBlock(byte[] input, int inOff, byte[] output, int outOff)
		{
			if (this.workingKey == null)
			{
				throw new InvalidOperationException("IDEA engine not initialised");
			}
			Check.DataLength(input, inOff, 8, "input buffer too short");
			Check.OutputLength(output, outOff, 8, "output buffer too short");
			this.IdeaFunc(this.workingKey, input, inOff, output, outOff);
			return 8;
		}

		// Token: 0x06001CB7 RID: 7351 RVA: 0x000A2DE0 File Offset: 0x000A2DE0
		public virtual void Reset()
		{
		}

		// Token: 0x06001CB8 RID: 7352 RVA: 0x000A2DE4 File Offset: 0x000A2DE4
		private int BytesToWord(byte[] input, int inOff)
		{
			return ((int)input[inOff] << 8 & 65280) + (int)(input[inOff + 1] & byte.MaxValue);
		}

		// Token: 0x06001CB9 RID: 7353 RVA: 0x000A2E00 File Offset: 0x000A2E00
		private void WordToBytes(int word, byte[] outBytes, int outOff)
		{
			outBytes[outOff] = (byte)((uint)word >> 8);
			outBytes[outOff + 1] = (byte)word;
		}

		// Token: 0x06001CBA RID: 7354 RVA: 0x000A2E10 File Offset: 0x000A2E10
		private int Mul(int x, int y)
		{
			if (x == 0)
			{
				x = IdeaEngine.BASE - y;
			}
			else if (y == 0)
			{
				x = IdeaEngine.BASE - x;
			}
			else
			{
				int num = x * y;
				y = (num & IdeaEngine.MASK);
				x = (int)((uint)num >> 16);
				x = y - x + ((y < x) ? 1 : 0);
			}
			return x & IdeaEngine.MASK;
		}

		// Token: 0x06001CBB RID: 7355 RVA: 0x000A2E78 File Offset: 0x000A2E78
		private void IdeaFunc(int[] workingKey, byte[] input, int inOff, byte[] outBytes, int outOff)
		{
			int num = 0;
			int num2 = this.BytesToWord(input, inOff);
			int num3 = this.BytesToWord(input, inOff + 2);
			int num4 = this.BytesToWord(input, inOff + 4);
			int num5 = this.BytesToWord(input, inOff + 6);
			for (int i = 0; i < 8; i++)
			{
				num2 = this.Mul(num2, workingKey[num++]);
				num3 += workingKey[num++];
				num3 &= IdeaEngine.MASK;
				num4 += workingKey[num++];
				num4 &= IdeaEngine.MASK;
				num5 = this.Mul(num5, workingKey[num++]);
				int num6 = num3;
				int num7 = num4;
				num4 ^= num2;
				num3 ^= num5;
				num4 = this.Mul(num4, workingKey[num++]);
				num3 += num4;
				num3 &= IdeaEngine.MASK;
				num3 = this.Mul(num3, workingKey[num++]);
				num4 += num3;
				num4 &= IdeaEngine.MASK;
				num2 ^= num3;
				num5 ^= num4;
				num3 ^= num7;
				num4 ^= num6;
			}
			this.WordToBytes(this.Mul(num2, workingKey[num++]), outBytes, outOff);
			this.WordToBytes(num4 + workingKey[num++], outBytes, outOff + 2);
			this.WordToBytes(num3 + workingKey[num++], outBytes, outOff + 4);
			this.WordToBytes(this.Mul(num5, workingKey[num]), outBytes, outOff + 6);
		}

		// Token: 0x06001CBC RID: 7356 RVA: 0x000A2FC4 File Offset: 0x000A2FC4
		private int[] ExpandKey(byte[] uKey)
		{
			int[] array = new int[52];
			if (uKey.Length < 16)
			{
				byte[] array2 = new byte[16];
				Array.Copy(uKey, 0, array2, array2.Length - uKey.Length, uKey.Length);
				uKey = array2;
			}
			for (int i = 0; i < 8; i++)
			{
				array[i] = this.BytesToWord(uKey, i * 2);
			}
			for (int j = 8; j < 52; j++)
			{
				if ((j & 7) < 6)
				{
					array[j] = (((array[j - 7] & 127) << 9 | array[j - 6] >> 7) & IdeaEngine.MASK);
				}
				else if ((j & 7) == 6)
				{
					array[j] = (((array[j - 7] & 127) << 9 | array[j - 14] >> 7) & IdeaEngine.MASK);
				}
				else
				{
					array[j] = (((array[j - 15] & 127) << 9 | array[j - 14] >> 7) & IdeaEngine.MASK);
				}
			}
			return array;
		}

		// Token: 0x06001CBD RID: 7357 RVA: 0x000A30A4 File Offset: 0x000A30A4
		private int MulInv(int x)
		{
			if (x < 2)
			{
				return x;
			}
			int num = 1;
			int num2 = IdeaEngine.BASE / x;
			int num3 = IdeaEngine.BASE % x;
			while (num3 != 1)
			{
				int num4 = x / num3;
				x %= num3;
				num = (num + num2 * num4 & IdeaEngine.MASK);
				if (x == 1)
				{
					return num;
				}
				num4 = num3 / x;
				num3 %= x;
				num2 = (num2 + num * num4 & IdeaEngine.MASK);
			}
			return 1 - num2 & IdeaEngine.MASK;
		}

		// Token: 0x06001CBE RID: 7358 RVA: 0x000A3114 File Offset: 0x000A3114
		private int AddInv(int x)
		{
			return -x & IdeaEngine.MASK;
		}

		// Token: 0x06001CBF RID: 7359 RVA: 0x000A3120 File Offset: 0x000A3120
		private int[] InvertKey(int[] inKey)
		{
			int num = 52;
			int[] array = new int[52];
			int num2 = 0;
			int num3 = this.MulInv(inKey[num2++]);
			int num4 = this.AddInv(inKey[num2++]);
			int num5 = this.AddInv(inKey[num2++]);
			int num6 = this.MulInv(inKey[num2++]);
			array[--num] = num6;
			array[--num] = num5;
			array[--num] = num4;
			array[--num] = num3;
			for (int i = 1; i < 8; i++)
			{
				num3 = inKey[num2++];
				num4 = inKey[num2++];
				array[--num] = num4;
				array[--num] = num3;
				num3 = this.MulInv(inKey[num2++]);
				num4 = this.AddInv(inKey[num2++]);
				num5 = this.AddInv(inKey[num2++]);
				num6 = this.MulInv(inKey[num2++]);
				array[--num] = num6;
				array[--num] = num4;
				array[--num] = num5;
				array[--num] = num3;
			}
			num3 = inKey[num2++];
			num4 = inKey[num2++];
			array[--num] = num4;
			array[--num] = num3;
			num3 = this.MulInv(inKey[num2++]);
			num4 = this.AddInv(inKey[num2++]);
			num5 = this.AddInv(inKey[num2++]);
			num6 = this.MulInv(inKey[num2]);
			array[--num] = num6;
			array[--num] = num5;
			array[--num] = num4;
			array[num - 1] = num3;
			return array;
		}

		// Token: 0x06001CC0 RID: 7360 RVA: 0x000A32B0 File Offset: 0x000A32B0
		private int[] GenerateWorkingKey(bool forEncryption, byte[] userKey)
		{
			if (forEncryption)
			{
				return this.ExpandKey(userKey);
			}
			return this.InvertKey(this.ExpandKey(userKey));
		}

		// Token: 0x04001313 RID: 4883
		private const int BLOCK_SIZE = 8;

		// Token: 0x04001314 RID: 4884
		private int[] workingKey;

		// Token: 0x04001315 RID: 4885
		private static readonly int MASK = 65535;

		// Token: 0x04001316 RID: 4886
		private static readonly int BASE = 65537;
	}
}
