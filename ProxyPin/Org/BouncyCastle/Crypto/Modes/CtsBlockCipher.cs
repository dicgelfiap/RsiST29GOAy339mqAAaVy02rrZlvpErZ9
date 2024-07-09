using System;

namespace Org.BouncyCastle.Crypto.Modes
{
	// Token: 0x02000401 RID: 1025
	public class CtsBlockCipher : BufferedBlockCipher
	{
		// Token: 0x060020BE RID: 8382 RVA: 0x000BDA9C File Offset: 0x000BDA9C
		public CtsBlockCipher(IBlockCipher cipher)
		{
			if (cipher is OfbBlockCipher || cipher is CfbBlockCipher)
			{
				throw new ArgumentException("CtsBlockCipher can only accept ECB, or CBC ciphers");
			}
			this.cipher = cipher;
			this.blockSize = cipher.GetBlockSize();
			this.buf = new byte[this.blockSize * 2];
			this.bufOff = 0;
		}

		// Token: 0x060020BF RID: 8383 RVA: 0x000BDB04 File Offset: 0x000BDB04
		public override int GetUpdateOutputSize(int length)
		{
			int num = length + this.bufOff;
			int num2 = num % this.buf.Length;
			if (num2 == 0)
			{
				return num - this.buf.Length;
			}
			return num - num2;
		}

		// Token: 0x060020C0 RID: 8384 RVA: 0x000BDB40 File Offset: 0x000BDB40
		public override int GetOutputSize(int length)
		{
			return length + this.bufOff;
		}

		// Token: 0x060020C1 RID: 8385 RVA: 0x000BDB4C File Offset: 0x000BDB4C
		public override int ProcessByte(byte input, byte[] output, int outOff)
		{
			int result = 0;
			if (this.bufOff == this.buf.Length)
			{
				result = this.cipher.ProcessBlock(this.buf, 0, output, outOff);
				Array.Copy(this.buf, this.blockSize, this.buf, 0, this.blockSize);
				this.bufOff = this.blockSize;
			}
			this.buf[this.bufOff++] = input;
			return result;
		}

		// Token: 0x060020C2 RID: 8386 RVA: 0x000BDBCC File Offset: 0x000BDBCC
		public override int ProcessBytes(byte[] input, int inOff, int length, byte[] output, int outOff)
		{
			if (length < 0)
			{
				throw new ArgumentException("Can't have a negative input outLength!");
			}
			int num = this.GetBlockSize();
			int updateOutputSize = this.GetUpdateOutputSize(length);
			if (updateOutputSize > 0 && outOff + updateOutputSize > output.Length)
			{
				throw new DataLengthException("output buffer too short");
			}
			int num2 = 0;
			int num3 = this.buf.Length - this.bufOff;
			if (length > num3)
			{
				Array.Copy(input, inOff, this.buf, this.bufOff, num3);
				num2 += this.cipher.ProcessBlock(this.buf, 0, output, outOff);
				Array.Copy(this.buf, num, this.buf, 0, num);
				this.bufOff = num;
				length -= num3;
				inOff += num3;
				while (length > num)
				{
					Array.Copy(input, inOff, this.buf, this.bufOff, num);
					num2 += this.cipher.ProcessBlock(this.buf, 0, output, outOff + num2);
					Array.Copy(this.buf, num, this.buf, 0, num);
					length -= num;
					inOff += num;
				}
			}
			Array.Copy(input, inOff, this.buf, this.bufOff, length);
			this.bufOff += length;
			return num2;
		}

		// Token: 0x060020C3 RID: 8387 RVA: 0x000BDD04 File Offset: 0x000BDD04
		public override int DoFinal(byte[] output, int outOff)
		{
			if (this.bufOff + outOff > output.Length)
			{
				throw new DataLengthException("output buffer too small in doFinal");
			}
			int num = this.cipher.GetBlockSize();
			int length = this.bufOff - num;
			byte[] array = new byte[num];
			if (this.forEncryption)
			{
				this.cipher.ProcessBlock(this.buf, 0, array, 0);
				if (this.bufOff < num)
				{
					throw new DataLengthException("need at least one block of input for CTS");
				}
				for (int num2 = this.bufOff; num2 != this.buf.Length; num2++)
				{
					this.buf[num2] = array[num2 - num];
				}
				for (int num3 = num; num3 != this.bufOff; num3++)
				{
					byte[] buf;
					IntPtr intPtr;
					(buf = this.buf)[(int)(intPtr = (IntPtr)num3)] = (buf[(int)intPtr] ^ array[num3 - num]);
				}
				IBlockCipher blockCipher = (this.cipher is CbcBlockCipher) ? ((CbcBlockCipher)this.cipher).GetUnderlyingCipher() : this.cipher;
				blockCipher.ProcessBlock(this.buf, num, output, outOff);
				Array.Copy(array, 0, output, outOff + num, length);
			}
			else
			{
				byte[] array2 = new byte[num];
				IBlockCipher blockCipher2 = (this.cipher is CbcBlockCipher) ? ((CbcBlockCipher)this.cipher).GetUnderlyingCipher() : this.cipher;
				blockCipher2.ProcessBlock(this.buf, 0, array, 0);
				for (int num4 = num; num4 != this.bufOff; num4++)
				{
					array2[num4 - num] = (array[num4 - num] ^ this.buf[num4]);
				}
				Array.Copy(this.buf, num, array, 0, length);
				this.cipher.ProcessBlock(array, 0, output, outOff);
				Array.Copy(array2, 0, output, outOff + num, length);
			}
			int bufOff = this.bufOff;
			this.Reset();
			return bufOff;
		}

		// Token: 0x0400153F RID: 5439
		private readonly int blockSize;
	}
}
