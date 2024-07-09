using System;

namespace Org.BouncyCastle.Utilities.Encoders
{
	// Token: 0x020006DA RID: 1754
	public class BufferedEncoder
	{
		// Token: 0x06003D63 RID: 15715 RVA: 0x001506F0 File Offset: 0x001506F0
		public BufferedEncoder(ITranslator translator, int bufferSize)
		{
			this.translator = translator;
			if (bufferSize % translator.GetEncodedBlockSize() != 0)
			{
				throw new ArgumentException("buffer size not multiple of input block size");
			}
			this.Buffer = new byte[bufferSize];
		}

		// Token: 0x06003D64 RID: 15716 RVA: 0x00150724 File Offset: 0x00150724
		public int ProcessByte(byte input, byte[] outBytes, int outOff)
		{
			int result = 0;
			this.Buffer[this.bufOff++] = input;
			if (this.bufOff == this.Buffer.Length)
			{
				result = this.translator.Encode(this.Buffer, 0, this.Buffer.Length, outBytes, outOff);
				this.bufOff = 0;
			}
			return result;
		}

		// Token: 0x06003D65 RID: 15717 RVA: 0x00150788 File Offset: 0x00150788
		public int ProcessBytes(byte[] input, int inOff, int len, byte[] outBytes, int outOff)
		{
			if (len < 0)
			{
				throw new ArgumentException("Can't have a negative input length!");
			}
			int num = 0;
			int num2 = this.Buffer.Length - this.bufOff;
			if (len > num2)
			{
				Array.Copy(input, inOff, this.Buffer, this.bufOff, num2);
				num += this.translator.Encode(this.Buffer, 0, this.Buffer.Length, outBytes, outOff);
				this.bufOff = 0;
				len -= num2;
				inOff += num2;
				outOff += num;
				int num3 = len - len % this.Buffer.Length;
				num += this.translator.Encode(input, inOff, num3, outBytes, outOff);
				len -= num3;
				inOff += num3;
			}
			if (len != 0)
			{
				Array.Copy(input, inOff, this.Buffer, this.bufOff, len);
				this.bufOff += len;
			}
			return num;
		}

		// Token: 0x04001EF2 RID: 7922
		internal byte[] Buffer;

		// Token: 0x04001EF3 RID: 7923
		internal int bufOff;

		// Token: 0x04001EF4 RID: 7924
		internal ITranslator translator;
	}
}
