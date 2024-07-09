using System;

namespace Org.BouncyCastle.Utilities.Encoders
{
	// Token: 0x020006D9 RID: 1753
	public class BufferedDecoder
	{
		// Token: 0x06003D60 RID: 15712 RVA: 0x00150578 File Offset: 0x00150578
		public BufferedDecoder(ITranslator translator, int bufferSize)
		{
			this.translator = translator;
			if (bufferSize % translator.GetEncodedBlockSize() != 0)
			{
				throw new ArgumentException("buffer size not multiple of input block size");
			}
			this.buffer = new byte[bufferSize];
		}

		// Token: 0x06003D61 RID: 15713 RVA: 0x001505AC File Offset: 0x001505AC
		public int ProcessByte(byte input, byte[] output, int outOff)
		{
			int result = 0;
			this.buffer[this.bufOff++] = input;
			if (this.bufOff == this.buffer.Length)
			{
				result = this.translator.Decode(this.buffer, 0, this.buffer.Length, output, outOff);
				this.bufOff = 0;
			}
			return result;
		}

		// Token: 0x06003D62 RID: 15714 RVA: 0x00150610 File Offset: 0x00150610
		public int ProcessBytes(byte[] input, int inOff, int len, byte[] outBytes, int outOff)
		{
			if (len < 0)
			{
				throw new ArgumentException("Can't have a negative input length!");
			}
			int num = 0;
			int num2 = this.buffer.Length - this.bufOff;
			if (len > num2)
			{
				Array.Copy(input, inOff, this.buffer, this.bufOff, num2);
				num += this.translator.Decode(this.buffer, 0, this.buffer.Length, outBytes, outOff);
				this.bufOff = 0;
				len -= num2;
				inOff += num2;
				outOff += num;
				int num3 = len - len % this.buffer.Length;
				num += this.translator.Decode(input, inOff, num3, outBytes, outOff);
				len -= num3;
				inOff += num3;
			}
			if (len != 0)
			{
				Array.Copy(input, inOff, this.buffer, this.bufOff, len);
				this.bufOff += len;
			}
			return num;
		}

		// Token: 0x04001EEF RID: 7919
		internal byte[] buffer;

		// Token: 0x04001EF0 RID: 7920
		internal int bufOff;

		// Token: 0x04001EF1 RID: 7921
		internal ITranslator translator;
	}
}
