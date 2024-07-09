using System;

namespace Org.BouncyCastle.Crypto
{
	// Token: 0x0200055E RID: 1374
	public class BufferedAsymmetricBlockCipher : BufferedCipherBase
	{
		// Token: 0x06002ABE RID: 10942 RVA: 0x000E4B44 File Offset: 0x000E4B44
		public BufferedAsymmetricBlockCipher(IAsymmetricBlockCipher cipher)
		{
			this.cipher = cipher;
		}

		// Token: 0x06002ABF RID: 10943 RVA: 0x000E4B54 File Offset: 0x000E4B54
		internal int GetBufferPosition()
		{
			return this.bufOff;
		}

		// Token: 0x170007B3 RID: 1971
		// (get) Token: 0x06002AC0 RID: 10944 RVA: 0x000E4B5C File Offset: 0x000E4B5C
		public override string AlgorithmName
		{
			get
			{
				return this.cipher.AlgorithmName;
			}
		}

		// Token: 0x06002AC1 RID: 10945 RVA: 0x000E4B6C File Offset: 0x000E4B6C
		public override int GetBlockSize()
		{
			return this.cipher.GetInputBlockSize();
		}

		// Token: 0x06002AC2 RID: 10946 RVA: 0x000E4B7C File Offset: 0x000E4B7C
		public override int GetOutputSize(int length)
		{
			return this.cipher.GetOutputBlockSize();
		}

		// Token: 0x06002AC3 RID: 10947 RVA: 0x000E4B8C File Offset: 0x000E4B8C
		public override int GetUpdateOutputSize(int length)
		{
			return 0;
		}

		// Token: 0x06002AC4 RID: 10948 RVA: 0x000E4B90 File Offset: 0x000E4B90
		public override void Init(bool forEncryption, ICipherParameters parameters)
		{
			this.Reset();
			this.cipher.Init(forEncryption, parameters);
			this.buffer = new byte[this.cipher.GetInputBlockSize() + (forEncryption ? 1 : 0)];
			this.bufOff = 0;
		}

		// Token: 0x06002AC5 RID: 10949 RVA: 0x000E4BE0 File Offset: 0x000E4BE0
		public override byte[] ProcessByte(byte input)
		{
			if (this.bufOff >= this.buffer.Length)
			{
				throw new DataLengthException("attempt to process message to long for cipher");
			}
			this.buffer[this.bufOff++] = input;
			return null;
		}

		// Token: 0x06002AC6 RID: 10950 RVA: 0x000E4C2C File Offset: 0x000E4C2C
		public override byte[] ProcessBytes(byte[] input, int inOff, int length)
		{
			if (length < 1)
			{
				return null;
			}
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			if (this.bufOff + length > this.buffer.Length)
			{
				throw new DataLengthException("attempt to process message to long for cipher");
			}
			Array.Copy(input, inOff, this.buffer, this.bufOff, length);
			this.bufOff += length;
			return null;
		}

		// Token: 0x06002AC7 RID: 10951 RVA: 0x000E4C9C File Offset: 0x000E4C9C
		public override byte[] DoFinal()
		{
			byte[] result = (this.bufOff > 0) ? this.cipher.ProcessBlock(this.buffer, 0, this.bufOff) : BufferedCipherBase.EmptyBuffer;
			this.Reset();
			return result;
		}

		// Token: 0x06002AC8 RID: 10952 RVA: 0x000E4CE4 File Offset: 0x000E4CE4
		public override byte[] DoFinal(byte[] input, int inOff, int length)
		{
			this.ProcessBytes(input, inOff, length);
			return this.DoFinal();
		}

		// Token: 0x06002AC9 RID: 10953 RVA: 0x000E4CF8 File Offset: 0x000E4CF8
		public override void Reset()
		{
			if (this.buffer != null)
			{
				Array.Clear(this.buffer, 0, this.buffer.Length);
				this.bufOff = 0;
			}
		}

		// Token: 0x04001B36 RID: 6966
		private readonly IAsymmetricBlockCipher cipher;

		// Token: 0x04001B37 RID: 6967
		private byte[] buffer;

		// Token: 0x04001B38 RID: 6968
		private int bufOff;
	}
}
