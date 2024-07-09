using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Engines
{
	// Token: 0x02000398 RID: 920
	public class RC532Engine : IBlockCipher
	{
		// Token: 0x06001D11 RID: 7441 RVA: 0x000A5AD8 File Offset: 0x000A5AD8
		public RC532Engine()
		{
			this._noRounds = 12;
		}

		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x06001D12 RID: 7442 RVA: 0x000A5AE8 File Offset: 0x000A5AE8
		public virtual string AlgorithmName
		{
			get
			{
				return "RC5-32";
			}
		}

		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x06001D13 RID: 7443 RVA: 0x000A5AF0 File Offset: 0x000A5AF0
		public virtual bool IsPartialBlockOkay
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001D14 RID: 7444 RVA: 0x000A5AF4 File Offset: 0x000A5AF4
		public virtual int GetBlockSize()
		{
			return 8;
		}

		// Token: 0x06001D15 RID: 7445 RVA: 0x000A5AF8 File Offset: 0x000A5AF8
		public virtual void Init(bool forEncryption, ICipherParameters parameters)
		{
			if (typeof(RC5Parameters).IsInstanceOfType(parameters))
			{
				RC5Parameters rc5Parameters = (RC5Parameters)parameters;
				this._noRounds = rc5Parameters.Rounds;
				this.SetKey(rc5Parameters.GetKey());
			}
			else
			{
				if (!typeof(KeyParameter).IsInstanceOfType(parameters))
				{
					throw new ArgumentException("invalid parameter passed to RC532 init - " + Platform.GetTypeName(parameters));
				}
				KeyParameter keyParameter = (KeyParameter)parameters;
				this.SetKey(keyParameter.GetKey());
			}
			this.forEncryption = forEncryption;
		}

		// Token: 0x06001D16 RID: 7446 RVA: 0x000A5B8C File Offset: 0x000A5B8C
		public virtual int ProcessBlock(byte[] input, int inOff, byte[] output, int outOff)
		{
			if (!this.forEncryption)
			{
				return this.DecryptBlock(input, inOff, output, outOff);
			}
			return this.EncryptBlock(input, inOff, output, outOff);
		}

		// Token: 0x06001D17 RID: 7447 RVA: 0x000A5BB0 File Offset: 0x000A5BB0
		public virtual void Reset()
		{
		}

		// Token: 0x06001D18 RID: 7448 RVA: 0x000A5BB4 File Offset: 0x000A5BB4
		private void SetKey(byte[] key)
		{
			int[] array = new int[(key.Length + 3) / 4];
			for (int num = 0; num != key.Length; num++)
			{
				int[] array2;
				IntPtr intPtr;
				(array2 = array)[(int)(intPtr = (IntPtr)(num / 4))] = array2[(int)intPtr] + ((int)(key[num] & byte.MaxValue) << 8 * (num % 4));
			}
			this._S = new int[2 * (this._noRounds + 1)];
			this._S[0] = RC532Engine.P32;
			for (int i = 1; i < this._S.Length; i++)
			{
				this._S[i] = this._S[i - 1] + RC532Engine.Q32;
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
			int num3 = 0;
			int num4 = 0;
			int num5 = 0;
			int num6 = 0;
			for (int j = 0; j < num2; j++)
			{
				num3 = (this._S[num5] = this.RotateLeft(this._S[num5] + num3 + num4, 3));
				num4 = (array[num6] = this.RotateLeft(array[num6] + num3 + num4, num3 + num4));
				num5 = (num5 + 1) % this._S.Length;
				num6 = (num6 + 1) % array.Length;
			}
		}

		// Token: 0x06001D19 RID: 7449 RVA: 0x000A5D00 File Offset: 0x000A5D00
		private int EncryptBlock(byte[] input, int inOff, byte[] outBytes, int outOff)
		{
			int num = this.BytesToWord(input, inOff) + this._S[0];
			int num2 = this.BytesToWord(input, inOff + 4) + this._S[1];
			for (int i = 1; i <= this._noRounds; i++)
			{
				num = this.RotateLeft(num ^ num2, num2) + this._S[2 * i];
				num2 = this.RotateLeft(num2 ^ num, num) + this._S[2 * i + 1];
			}
			this.WordToBytes(num, outBytes, outOff);
			this.WordToBytes(num2, outBytes, outOff + 4);
			return 8;
		}

		// Token: 0x06001D1A RID: 7450 RVA: 0x000A5D90 File Offset: 0x000A5D90
		private int DecryptBlock(byte[] input, int inOff, byte[] outBytes, int outOff)
		{
			int num = this.BytesToWord(input, inOff);
			int num2 = this.BytesToWord(input, inOff + 4);
			for (int i = this._noRounds; i >= 1; i--)
			{
				num2 = (this.RotateRight(num2 - this._S[2 * i + 1], num) ^ num);
				num = (this.RotateRight(num - this._S[2 * i], num2) ^ num2);
			}
			this.WordToBytes(num - this._S[0], outBytes, outOff);
			this.WordToBytes(num2 - this._S[1], outBytes, outOff + 4);
			return 8;
		}

		// Token: 0x06001D1B RID: 7451 RVA: 0x000A5E20 File Offset: 0x000A5E20
		private int RotateLeft(int x, int y)
		{
			return x << y | (int)((uint)x >> 32 - (y & 31));
		}

		// Token: 0x06001D1C RID: 7452 RVA: 0x000A5E38 File Offset: 0x000A5E38
		private int RotateRight(int x, int y)
		{
			return (int)((uint)x >> y | (uint)((uint)x << 32 - (y & 31)));
		}

		// Token: 0x06001D1D RID: 7453 RVA: 0x000A5E50 File Offset: 0x000A5E50
		private int BytesToWord(byte[] src, int srcOff)
		{
			return (int)(src[srcOff] & byte.MaxValue) | (int)(src[srcOff + 1] & byte.MaxValue) << 8 | (int)(src[srcOff + 2] & byte.MaxValue) << 16 | (int)(src[srcOff + 3] & byte.MaxValue) << 24;
		}

		// Token: 0x06001D1E RID: 7454 RVA: 0x000A5E88 File Offset: 0x000A5E88
		private void WordToBytes(int word, byte[] dst, int dstOff)
		{
			dst[dstOff] = (byte)word;
			dst[dstOff + 1] = (byte)(word >> 8);
			dst[dstOff + 2] = (byte)(word >> 16);
			dst[dstOff + 3] = (byte)(word >> 24);
		}

		// Token: 0x0400134A RID: 4938
		private int _noRounds;

		// Token: 0x0400134B RID: 4939
		private int[] _S;

		// Token: 0x0400134C RID: 4940
		private static readonly int P32 = -1209970333;

		// Token: 0x0400134D RID: 4941
		private static readonly int Q32 = -1640531527;

		// Token: 0x0400134E RID: 4942
		private bool forEncryption;
	}
}
