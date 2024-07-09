using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Engines
{
	// Token: 0x0200039A RID: 922
	public class RC6Engine : IBlockCipher
	{
		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x06001D30 RID: 7472 RVA: 0x000A631C File Offset: 0x000A631C
		public virtual string AlgorithmName
		{
			get
			{
				return "RC6";
			}
		}

		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x06001D31 RID: 7473 RVA: 0x000A6324 File Offset: 0x000A6324
		public virtual bool IsPartialBlockOkay
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001D32 RID: 7474 RVA: 0x000A6328 File Offset: 0x000A6328
		public virtual int GetBlockSize()
		{
			return 4 * RC6Engine.bytesPerWord;
		}

		// Token: 0x06001D33 RID: 7475 RVA: 0x000A6334 File Offset: 0x000A6334
		public virtual void Init(bool forEncryption, ICipherParameters parameters)
		{
			if (!(parameters is KeyParameter))
			{
				throw new ArgumentException("invalid parameter passed to RC6 init - " + Platform.GetTypeName(parameters));
			}
			this.forEncryption = forEncryption;
			KeyParameter keyParameter = (KeyParameter)parameters;
			this.SetKey(keyParameter.GetKey());
		}

		// Token: 0x06001D34 RID: 7476 RVA: 0x000A6380 File Offset: 0x000A6380
		public virtual int ProcessBlock(byte[] input, int inOff, byte[] output, int outOff)
		{
			int blockSize = this.GetBlockSize();
			if (this._S == null)
			{
				throw new InvalidOperationException("RC6 engine not initialised");
			}
			Check.DataLength(input, inOff, blockSize, "input buffer too short");
			Check.OutputLength(output, outOff, blockSize, "output buffer too short");
			if (!this.forEncryption)
			{
				return this.DecryptBlock(input, inOff, output, outOff);
			}
			return this.EncryptBlock(input, inOff, output, outOff);
		}

		// Token: 0x06001D35 RID: 7477 RVA: 0x000A63EC File Offset: 0x000A63EC
		public virtual void Reset()
		{
		}

		// Token: 0x06001D36 RID: 7478 RVA: 0x000A63F0 File Offset: 0x000A63F0
		private void SetKey(byte[] key)
		{
			if ((key.Length + (RC6Engine.bytesPerWord - 1)) / RC6Engine.bytesPerWord == 0)
			{
			}
			int[] array = new int[(key.Length + RC6Engine.bytesPerWord - 1) / RC6Engine.bytesPerWord];
			for (int i = key.Length - 1; i >= 0; i--)
			{
				array[i / RC6Engine.bytesPerWord] = (array[i / RC6Engine.bytesPerWord] << 8) + (int)(key[i] & byte.MaxValue);
			}
			this._S = new int[2 + 2 * RC6Engine._noRounds + 2];
			this._S[0] = RC6Engine.P32;
			for (int j = 1; j < this._S.Length; j++)
			{
				this._S[j] = this._S[j - 1] + RC6Engine.Q32;
			}
			int num;
			if (array.Length > this._S.Length)
			{
				num = 3 * array.Length;
			}
			else
			{
				num = 3 * this._S.Length;
			}
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			int num5 = 0;
			for (int k = 0; k < num; k++)
			{
				num2 = (this._S[num4] = this.RotateLeft(this._S[num4] + num2 + num3, 3));
				num3 = (array[num5] = this.RotateLeft(array[num5] + num2 + num3, num2 + num3));
				num4 = (num4 + 1) % this._S.Length;
				num5 = (num5 + 1) % array.Length;
			}
		}

		// Token: 0x06001D37 RID: 7479 RVA: 0x000A655C File Offset: 0x000A655C
		private int EncryptBlock(byte[] input, int inOff, byte[] outBytes, int outOff)
		{
			int num = this.BytesToWord(input, inOff);
			int num2 = this.BytesToWord(input, inOff + RC6Engine.bytesPerWord);
			int num3 = this.BytesToWord(input, inOff + RC6Engine.bytesPerWord * 2);
			int num4 = this.BytesToWord(input, inOff + RC6Engine.bytesPerWord * 3);
			num2 += this._S[0];
			num4 += this._S[1];
			for (int i = 1; i <= RC6Engine._noRounds; i++)
			{
				int num5 = num2 * (2 * num2 + 1);
				num5 = this.RotateLeft(num5, 5);
				int num6 = num4 * (2 * num4 + 1);
				num6 = this.RotateLeft(num6, 5);
				num ^= num5;
				num = this.RotateLeft(num, num6);
				num += this._S[2 * i];
				num3 ^= num6;
				num3 = this.RotateLeft(num3, num5);
				num3 += this._S[2 * i + 1];
				int num7 = num;
				num = num2;
				num2 = num3;
				num3 = num4;
				num4 = num7;
			}
			num += this._S[2 * RC6Engine._noRounds + 2];
			num3 += this._S[2 * RC6Engine._noRounds + 3];
			this.WordToBytes(num, outBytes, outOff);
			this.WordToBytes(num2, outBytes, outOff + RC6Engine.bytesPerWord);
			this.WordToBytes(num3, outBytes, outOff + RC6Engine.bytesPerWord * 2);
			this.WordToBytes(num4, outBytes, outOff + RC6Engine.bytesPerWord * 3);
			return 4 * RC6Engine.bytesPerWord;
		}

		// Token: 0x06001D38 RID: 7480 RVA: 0x000A66B8 File Offset: 0x000A66B8
		private int DecryptBlock(byte[] input, int inOff, byte[] outBytes, int outOff)
		{
			int num = this.BytesToWord(input, inOff);
			int num2 = this.BytesToWord(input, inOff + RC6Engine.bytesPerWord);
			int num3 = this.BytesToWord(input, inOff + RC6Engine.bytesPerWord * 2);
			int num4 = this.BytesToWord(input, inOff + RC6Engine.bytesPerWord * 3);
			num3 -= this._S[2 * RC6Engine._noRounds + 3];
			num -= this._S[2 * RC6Engine._noRounds + 2];
			for (int i = RC6Engine._noRounds; i >= 1; i--)
			{
				int num5 = num4;
				num4 = num3;
				num3 = num2;
				num2 = num;
				num = num5;
				int num6 = num2 * (2 * num2 + 1);
				num6 = this.RotateLeft(num6, RC6Engine.LGW);
				int num7 = num4 * (2 * num4 + 1);
				num7 = this.RotateLeft(num7, RC6Engine.LGW);
				num3 -= this._S[2 * i + 1];
				num3 = this.RotateRight(num3, num6);
				num3 ^= num7;
				num -= this._S[2 * i];
				num = this.RotateRight(num, num7);
				num ^= num6;
			}
			num4 -= this._S[1];
			num2 -= this._S[0];
			this.WordToBytes(num, outBytes, outOff);
			this.WordToBytes(num2, outBytes, outOff + RC6Engine.bytesPerWord);
			this.WordToBytes(num3, outBytes, outOff + RC6Engine.bytesPerWord * 2);
			this.WordToBytes(num4, outBytes, outOff + RC6Engine.bytesPerWord * 3);
			return 4 * RC6Engine.bytesPerWord;
		}

		// Token: 0x06001D39 RID: 7481 RVA: 0x000A681C File Offset: 0x000A681C
		private int RotateLeft(int x, int y)
		{
			return x << (y & RC6Engine.wordSize - 1) | (int)((uint)x >> RC6Engine.wordSize - (y & RC6Engine.wordSize - 1));
		}

		// Token: 0x06001D3A RID: 7482 RVA: 0x000A6844 File Offset: 0x000A6844
		private int RotateRight(int x, int y)
		{
			return (int)((uint)x >> (y & RC6Engine.wordSize - 1) | (uint)((uint)x << RC6Engine.wordSize - (y & RC6Engine.wordSize - 1)));
		}

		// Token: 0x06001D3B RID: 7483 RVA: 0x000A686C File Offset: 0x000A686C
		private int BytesToWord(byte[] src, int srcOff)
		{
			int num = 0;
			for (int i = RC6Engine.bytesPerWord - 1; i >= 0; i--)
			{
				num = (num << 8) + (int)(src[i + srcOff] & byte.MaxValue);
			}
			return num;
		}

		// Token: 0x06001D3C RID: 7484 RVA: 0x000A68A8 File Offset: 0x000A68A8
		private void WordToBytes(int word, byte[] dst, int dstOff)
		{
			for (int i = 0; i < RC6Engine.bytesPerWord; i++)
			{
				dst[i + dstOff] = (byte)word;
				word = (int)((uint)word >> 8);
			}
		}

		// Token: 0x04001356 RID: 4950
		private static readonly int wordSize = 32;

		// Token: 0x04001357 RID: 4951
		private static readonly int bytesPerWord = RC6Engine.wordSize / 8;

		// Token: 0x04001358 RID: 4952
		private static readonly int _noRounds = 20;

		// Token: 0x04001359 RID: 4953
		private int[] _S;

		// Token: 0x0400135A RID: 4954
		private static readonly int P32 = -1209970333;

		// Token: 0x0400135B RID: 4955
		private static readonly int Q32 = -1640531527;

		// Token: 0x0400135C RID: 4956
		private static readonly int LGW = 5;

		// Token: 0x0400135D RID: 4957
		private bool forEncryption;
	}
}
