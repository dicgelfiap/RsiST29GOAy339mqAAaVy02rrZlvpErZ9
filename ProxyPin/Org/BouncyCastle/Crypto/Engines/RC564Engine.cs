using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Engines
{
	// Token: 0x02000399 RID: 921
	public class RC564Engine : IBlockCipher
	{
		// Token: 0x06001D20 RID: 7456 RVA: 0x000A5EC4 File Offset: 0x000A5EC4
		public RC564Engine()
		{
			this._noRounds = 12;
		}

		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x06001D21 RID: 7457 RVA: 0x000A5ED4 File Offset: 0x000A5ED4
		public virtual string AlgorithmName
		{
			get
			{
				return "RC5-64";
			}
		}

		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x06001D22 RID: 7458 RVA: 0x000A5EDC File Offset: 0x000A5EDC
		public virtual bool IsPartialBlockOkay
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001D23 RID: 7459 RVA: 0x000A5EE0 File Offset: 0x000A5EE0
		public virtual int GetBlockSize()
		{
			return 2 * RC564Engine.bytesPerWord;
		}

		// Token: 0x06001D24 RID: 7460 RVA: 0x000A5EEC File Offset: 0x000A5EEC
		public virtual void Init(bool forEncryption, ICipherParameters parameters)
		{
			if (!typeof(RC5Parameters).IsInstanceOfType(parameters))
			{
				throw new ArgumentException("invalid parameter passed to RC564 init - " + Platform.GetTypeName(parameters));
			}
			RC5Parameters rc5Parameters = (RC5Parameters)parameters;
			this.forEncryption = forEncryption;
			this._noRounds = rc5Parameters.Rounds;
			this.SetKey(rc5Parameters.GetKey());
		}

		// Token: 0x06001D25 RID: 7461 RVA: 0x000A5F50 File Offset: 0x000A5F50
		public virtual int ProcessBlock(byte[] input, int inOff, byte[] output, int outOff)
		{
			if (!this.forEncryption)
			{
				return this.DecryptBlock(input, inOff, output, outOff);
			}
			return this.EncryptBlock(input, inOff, output, outOff);
		}

		// Token: 0x06001D26 RID: 7462 RVA: 0x000A5F74 File Offset: 0x000A5F74
		public virtual void Reset()
		{
		}

		// Token: 0x06001D27 RID: 7463 RVA: 0x000A5F78 File Offset: 0x000A5F78
		private void SetKey(byte[] key)
		{
			long[] array = new long[(key.Length + (RC564Engine.bytesPerWord - 1)) / RC564Engine.bytesPerWord];
			for (int num = 0; num != key.Length; num++)
			{
				long[] array2;
				IntPtr intPtr;
				(array2 = array)[(int)(intPtr = (IntPtr)(num / RC564Engine.bytesPerWord))] = array2[(int)intPtr] + ((long)(key[num] & byte.MaxValue) << 8 * (num % RC564Engine.bytesPerWord));
			}
			this._S = new long[2 * (this._noRounds + 1)];
			this._S[0] = RC564Engine.P64;
			for (int i = 1; i < this._S.Length; i++)
			{
				this._S[i] = this._S[i - 1] + RC564Engine.Q64;
			}
			int num2;
			if (array.Length > this._S.Length)
			{
				num2 = 3 * array.Length;
			}
			else
			{
				num2 = 3 * this._S.Length;
			}
			long num3 = 0L;
			long num4 = 0L;
			int num5 = 0;
			int num6 = 0;
			for (int j = 0; j < num2; j++)
			{
				num3 = (this._S[num5] = this.RotateLeft(this._S[num5] + num3 + num4, 3L));
				num4 = (array[num6] = this.RotateLeft(array[num6] + num3 + num4, num3 + num4));
				num5 = (num5 + 1) % this._S.Length;
				num6 = (num6 + 1) % array.Length;
			}
		}

		// Token: 0x06001D28 RID: 7464 RVA: 0x000A60DC File Offset: 0x000A60DC
		private int EncryptBlock(byte[] input, int inOff, byte[] outBytes, int outOff)
		{
			long num = this.BytesToWord(input, inOff) + this._S[0];
			long num2 = this.BytesToWord(input, inOff + RC564Engine.bytesPerWord) + this._S[1];
			for (int i = 1; i <= this._noRounds; i++)
			{
				num = this.RotateLeft(num ^ num2, num2) + this._S[2 * i];
				num2 = this.RotateLeft(num2 ^ num, num) + this._S[2 * i + 1];
			}
			this.WordToBytes(num, outBytes, outOff);
			this.WordToBytes(num2, outBytes, outOff + RC564Engine.bytesPerWord);
			return 2 * RC564Engine.bytesPerWord;
		}

		// Token: 0x06001D29 RID: 7465 RVA: 0x000A617C File Offset: 0x000A617C
		private int DecryptBlock(byte[] input, int inOff, byte[] outBytes, int outOff)
		{
			long num = this.BytesToWord(input, inOff);
			long num2 = this.BytesToWord(input, inOff + RC564Engine.bytesPerWord);
			for (int i = this._noRounds; i >= 1; i--)
			{
				num2 = (this.RotateRight(num2 - this._S[2 * i + 1], num) ^ num);
				num = (this.RotateRight(num - this._S[2 * i], num2) ^ num2);
			}
			this.WordToBytes(num - this._S[0], outBytes, outOff);
			this.WordToBytes(num2 - this._S[1], outBytes, outOff + RC564Engine.bytesPerWord);
			return 2 * RC564Engine.bytesPerWord;
		}

		// Token: 0x06001D2A RID: 7466 RVA: 0x000A621C File Offset: 0x000A621C
		private long RotateLeft(long x, long y)
		{
			return x << (int)(y & (long)(RC564Engine.wordSize - 1)) | (long)((ulong)x >> (int)((long)RC564Engine.wordSize - (y & (long)(RC564Engine.wordSize - 1))));
		}

		// Token: 0x06001D2B RID: 7467 RVA: 0x000A6248 File Offset: 0x000A6248
		private long RotateRight(long x, long y)
		{
			return (long)((ulong)x >> (int)(y & (long)(RC564Engine.wordSize - 1)) | (ulong)((ulong)x << (int)((long)RC564Engine.wordSize - (y & (long)(RC564Engine.wordSize - 1)))));
		}

		// Token: 0x06001D2C RID: 7468 RVA: 0x000A6274 File Offset: 0x000A6274
		private long BytesToWord(byte[] src, int srcOff)
		{
			long num = 0L;
			for (int i = RC564Engine.bytesPerWord - 1; i >= 0; i--)
			{
				num = (num << 8) + (long)(src[i + srcOff] & byte.MaxValue);
			}
			return num;
		}

		// Token: 0x06001D2D RID: 7469 RVA: 0x000A62B0 File Offset: 0x000A62B0
		private void WordToBytes(long word, byte[] dst, int dstOff)
		{
			for (int i = 0; i < RC564Engine.bytesPerWord; i++)
			{
				dst[i + dstOff] = (byte)word;
				word = (long)((ulong)word >> 8);
			}
		}

		// Token: 0x0400134F RID: 4943
		private static readonly int wordSize = 64;

		// Token: 0x04001350 RID: 4944
		private static readonly int bytesPerWord = RC564Engine.wordSize / 8;

		// Token: 0x04001351 RID: 4945
		private int _noRounds;

		// Token: 0x04001352 RID: 4946
		private long[] _S;

		// Token: 0x04001353 RID: 4947
		private static readonly long P64 = -5196783011329398165L;

		// Token: 0x04001354 RID: 4948
		private static readonly long Q64 = -7046029254386353131L;

		// Token: 0x04001355 RID: 4949
		private bool forEncryption;
	}
}
