using System;

namespace Org.BouncyCastle.Crypto
{
	// Token: 0x020003FF RID: 1023
	public abstract class BufferedCipherBase : IBufferedCipher
	{
		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x0600209B RID: 8347
		public abstract string AlgorithmName { get; }

		// Token: 0x0600209C RID: 8348
		public abstract void Init(bool forEncryption, ICipherParameters parameters);

		// Token: 0x0600209D RID: 8349
		public abstract int GetBlockSize();

		// Token: 0x0600209E RID: 8350
		public abstract int GetOutputSize(int inputLen);

		// Token: 0x0600209F RID: 8351
		public abstract int GetUpdateOutputSize(int inputLen);

		// Token: 0x060020A0 RID: 8352
		public abstract byte[] ProcessByte(byte input);

		// Token: 0x060020A1 RID: 8353 RVA: 0x000BD450 File Offset: 0x000BD450
		public virtual int ProcessByte(byte input, byte[] output, int outOff)
		{
			byte[] array = this.ProcessByte(input);
			if (array == null)
			{
				return 0;
			}
			if (outOff + array.Length > output.Length)
			{
				throw new DataLengthException("output buffer too short");
			}
			array.CopyTo(output, outOff);
			return array.Length;
		}

		// Token: 0x060020A2 RID: 8354 RVA: 0x000BD494 File Offset: 0x000BD494
		public virtual byte[] ProcessBytes(byte[] input)
		{
			return this.ProcessBytes(input, 0, input.Length);
		}

		// Token: 0x060020A3 RID: 8355
		public abstract byte[] ProcessBytes(byte[] input, int inOff, int length);

		// Token: 0x060020A4 RID: 8356 RVA: 0x000BD4A4 File Offset: 0x000BD4A4
		public virtual int ProcessBytes(byte[] input, byte[] output, int outOff)
		{
			return this.ProcessBytes(input, 0, input.Length, output, outOff);
		}

		// Token: 0x060020A5 RID: 8357 RVA: 0x000BD4B4 File Offset: 0x000BD4B4
		public virtual int ProcessBytes(byte[] input, int inOff, int length, byte[] output, int outOff)
		{
			byte[] array = this.ProcessBytes(input, inOff, length);
			if (array == null)
			{
				return 0;
			}
			if (outOff + array.Length > output.Length)
			{
				throw new DataLengthException("output buffer too short");
			}
			array.CopyTo(output, outOff);
			return array.Length;
		}

		// Token: 0x060020A6 RID: 8358
		public abstract byte[] DoFinal();

		// Token: 0x060020A7 RID: 8359 RVA: 0x000BD500 File Offset: 0x000BD500
		public virtual byte[] DoFinal(byte[] input)
		{
			return this.DoFinal(input, 0, input.Length);
		}

		// Token: 0x060020A8 RID: 8360
		public abstract byte[] DoFinal(byte[] input, int inOff, int length);

		// Token: 0x060020A9 RID: 8361 RVA: 0x000BD510 File Offset: 0x000BD510
		public virtual int DoFinal(byte[] output, int outOff)
		{
			byte[] array = this.DoFinal();
			if (outOff + array.Length > output.Length)
			{
				throw new DataLengthException("output buffer too short");
			}
			array.CopyTo(output, outOff);
			return array.Length;
		}

		// Token: 0x060020AA RID: 8362 RVA: 0x000BD54C File Offset: 0x000BD54C
		public virtual int DoFinal(byte[] input, byte[] output, int outOff)
		{
			return this.DoFinal(input, 0, input.Length, output, outOff);
		}

		// Token: 0x060020AB RID: 8363 RVA: 0x000BD55C File Offset: 0x000BD55C
		public virtual int DoFinal(byte[] input, int inOff, int length, byte[] output, int outOff)
		{
			int num = this.ProcessBytes(input, inOff, length, output, outOff);
			return num + this.DoFinal(output, outOff + num);
		}

		// Token: 0x060020AC RID: 8364
		public abstract void Reset();

		// Token: 0x0400153A RID: 5434
		protected static readonly byte[] EmptyBuffer = new byte[0];
	}
}
