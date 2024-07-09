using System;

namespace Org.BouncyCastle.Crypto
{
	// Token: 0x0200056A RID: 1386
	public class StreamBlockCipher : IStreamCipher
	{
		// Token: 0x06002B00 RID: 11008 RVA: 0x000E5274 File Offset: 0x000E5274
		public StreamBlockCipher(IBlockCipher cipher)
		{
			if (cipher == null)
			{
				throw new ArgumentNullException("cipher");
			}
			if (cipher.GetBlockSize() != 1)
			{
				throw new ArgumentException("block cipher block size != 1.", "cipher");
			}
			this.cipher = cipher;
		}

		// Token: 0x06002B01 RID: 11009 RVA: 0x000E52CC File Offset: 0x000E52CC
		public void Init(bool forEncryption, ICipherParameters parameters)
		{
			this.cipher.Init(forEncryption, parameters);
		}

		// Token: 0x170007B7 RID: 1975
		// (get) Token: 0x06002B02 RID: 11010 RVA: 0x000E52DC File Offset: 0x000E52DC
		public string AlgorithmName
		{
			get
			{
				return this.cipher.AlgorithmName;
			}
		}

		// Token: 0x06002B03 RID: 11011 RVA: 0x000E52EC File Offset: 0x000E52EC
		public byte ReturnByte(byte input)
		{
			this.oneByte[0] = input;
			this.cipher.ProcessBlock(this.oneByte, 0, this.oneByte, 0);
			return this.oneByte[0];
		}

		// Token: 0x06002B04 RID: 11012 RVA: 0x000E531C File Offset: 0x000E531C
		public void ProcessBytes(byte[] input, int inOff, int length, byte[] output, int outOff)
		{
			if (outOff + length > output.Length)
			{
				throw new DataLengthException("output buffer too small in ProcessBytes()");
			}
			for (int num = 0; num != length; num++)
			{
				this.cipher.ProcessBlock(input, inOff + num, output, outOff + num);
			}
		}

		// Token: 0x06002B05 RID: 11013 RVA: 0x000E536C File Offset: 0x000E536C
		public void Reset()
		{
			this.cipher.Reset();
		}

		// Token: 0x04001B3E RID: 6974
		private readonly IBlockCipher cipher;

		// Token: 0x04001B3F RID: 6975
		private readonly byte[] oneByte = new byte[1];
	}
}
