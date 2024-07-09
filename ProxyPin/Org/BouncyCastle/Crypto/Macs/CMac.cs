using System;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Paddings;
using Org.BouncyCastle.Crypto.Parameters;

namespace Org.BouncyCastle.Crypto.Macs
{
	// Token: 0x020003E5 RID: 997
	public class CMac : IMac
	{
		// Token: 0x06001F8F RID: 8079 RVA: 0x000B786C File Offset: 0x000B786C
		public CMac(IBlockCipher cipher) : this(cipher, cipher.GetBlockSize() * 8)
		{
		}

		// Token: 0x06001F90 RID: 8080 RVA: 0x000B7880 File Offset: 0x000B7880
		public CMac(IBlockCipher cipher, int macSizeInBits)
		{
			if (macSizeInBits % 8 != 0)
			{
				throw new ArgumentException("MAC size must be multiple of 8");
			}
			if (macSizeInBits > cipher.GetBlockSize() * 8)
			{
				throw new ArgumentException("MAC size must be less or equal to " + cipher.GetBlockSize() * 8);
			}
			if (cipher.GetBlockSize() != 8 && cipher.GetBlockSize() != 16)
			{
				throw new ArgumentException("Block size must be either 64 or 128 bits");
			}
			this.cipher = new CbcBlockCipher(cipher);
			this.macSize = macSizeInBits / 8;
			this.mac = new byte[cipher.GetBlockSize()];
			this.buf = new byte[cipher.GetBlockSize()];
			this.ZEROES = new byte[cipher.GetBlockSize()];
			this.bufOff = 0;
		}

		// Token: 0x17000620 RID: 1568
		// (get) Token: 0x06001F91 RID: 8081 RVA: 0x000B7948 File Offset: 0x000B7948
		public string AlgorithmName
		{
			get
			{
				return this.cipher.AlgorithmName;
			}
		}

		// Token: 0x06001F92 RID: 8082 RVA: 0x000B7958 File Offset: 0x000B7958
		private static int ShiftLeft(byte[] block, byte[] output)
		{
			int num = block.Length;
			uint num2 = 0U;
			while (--num >= 0)
			{
				uint num3 = (uint)block[num];
				output[num] = (byte)(num3 << 1 | num2);
				num2 = (num3 >> 7 & 1U);
			}
			return (int)num2;
		}

		// Token: 0x06001F93 RID: 8083 RVA: 0x000B7990 File Offset: 0x000B7990
		private static byte[] DoubleLu(byte[] input)
		{
			byte[] array = new byte[input.Length];
			int num = CMac.ShiftLeft(input, array);
			int num2 = (input.Length == 16) ? 135 : 27;
			byte[] array2;
			IntPtr intPtr;
			(array2 = array)[(int)(intPtr = (IntPtr)(input.Length - 1))] = (array2[(int)intPtr] ^ (byte)(num2 >> (1 - num << 3)));
			return array;
		}

		// Token: 0x06001F94 RID: 8084 RVA: 0x000B79E8 File Offset: 0x000B79E8
		public void Init(ICipherParameters parameters)
		{
			if (parameters is KeyParameter)
			{
				this.cipher.Init(true, parameters);
				this.L = new byte[this.ZEROES.Length];
				this.cipher.ProcessBlock(this.ZEROES, 0, this.L, 0);
				this.Lu = CMac.DoubleLu(this.L);
				this.Lu2 = CMac.DoubleLu(this.Lu);
			}
			else if (parameters != null)
			{
				throw new ArgumentException("CMac mode only permits key to be set.", "parameters");
			}
			this.Reset();
		}

		// Token: 0x06001F95 RID: 8085 RVA: 0x000B7A84 File Offset: 0x000B7A84
		public int GetMacSize()
		{
			return this.macSize;
		}

		// Token: 0x06001F96 RID: 8086 RVA: 0x000B7A8C File Offset: 0x000B7A8C
		public void Update(byte input)
		{
			if (this.bufOff == this.buf.Length)
			{
				this.cipher.ProcessBlock(this.buf, 0, this.mac, 0);
				this.bufOff = 0;
			}
			this.buf[this.bufOff++] = input;
		}

		// Token: 0x06001F97 RID: 8087 RVA: 0x000B7AEC File Offset: 0x000B7AEC
		public void BlockUpdate(byte[] inBytes, int inOff, int len)
		{
			if (len < 0)
			{
				throw new ArgumentException("Can't have a negative input length!");
			}
			int blockSize = this.cipher.GetBlockSize();
			int num = blockSize - this.bufOff;
			if (len > num)
			{
				Array.Copy(inBytes, inOff, this.buf, this.bufOff, num);
				this.cipher.ProcessBlock(this.buf, 0, this.mac, 0);
				this.bufOff = 0;
				len -= num;
				inOff += num;
				while (len > blockSize)
				{
					this.cipher.ProcessBlock(inBytes, inOff, this.mac, 0);
					len -= blockSize;
					inOff += blockSize;
				}
			}
			Array.Copy(inBytes, inOff, this.buf, this.bufOff, len);
			this.bufOff += len;
		}

		// Token: 0x06001F98 RID: 8088 RVA: 0x000B7BB4 File Offset: 0x000B7BB4
		public int DoFinal(byte[] outBytes, int outOff)
		{
			int blockSize = this.cipher.GetBlockSize();
			byte[] array;
			if (this.bufOff == blockSize)
			{
				array = this.Lu;
			}
			else
			{
				new ISO7816d4Padding().AddPadding(this.buf, this.bufOff);
				array = this.Lu2;
			}
			for (int i = 0; i < this.mac.Length; i++)
			{
				byte[] array2;
				IntPtr intPtr;
				(array2 = this.buf)[(int)(intPtr = (IntPtr)i)] = (array2[(int)intPtr] ^ array[i]);
			}
			this.cipher.ProcessBlock(this.buf, 0, this.mac, 0);
			Array.Copy(this.mac, 0, outBytes, outOff, this.macSize);
			this.Reset();
			return this.macSize;
		}

		// Token: 0x06001F99 RID: 8089 RVA: 0x000B7C70 File Offset: 0x000B7C70
		public void Reset()
		{
			Array.Clear(this.buf, 0, this.buf.Length);
			this.bufOff = 0;
			this.cipher.Reset();
		}

		// Token: 0x040014A3 RID: 5283
		private const byte CONSTANT_128 = 135;

		// Token: 0x040014A4 RID: 5284
		private const byte CONSTANT_64 = 27;

		// Token: 0x040014A5 RID: 5285
		private byte[] ZEROES;

		// Token: 0x040014A6 RID: 5286
		private byte[] mac;

		// Token: 0x040014A7 RID: 5287
		private byte[] buf;

		// Token: 0x040014A8 RID: 5288
		private int bufOff;

		// Token: 0x040014A9 RID: 5289
		private IBlockCipher cipher;

		// Token: 0x040014AA RID: 5290
		private int macSize;

		// Token: 0x040014AB RID: 5291
		private byte[] L;

		// Token: 0x040014AC RID: 5292
		private byte[] Lu;

		// Token: 0x040014AD RID: 5293
		private byte[] Lu2;
	}
}
