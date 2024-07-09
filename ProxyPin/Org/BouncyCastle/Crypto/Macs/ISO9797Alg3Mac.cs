using System;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Paddings;
using Org.BouncyCastle.Crypto.Parameters;

namespace Org.BouncyCastle.Crypto.Macs
{
	// Token: 0x020003EB RID: 1003
	public class ISO9797Alg3Mac : IMac
	{
		// Token: 0x06001FCF RID: 8143 RVA: 0x000B8E00 File Offset: 0x000B8E00
		public ISO9797Alg3Mac(IBlockCipher cipher) : this(cipher, cipher.GetBlockSize() * 8, null)
		{
		}

		// Token: 0x06001FD0 RID: 8144 RVA: 0x000B8E14 File Offset: 0x000B8E14
		public ISO9797Alg3Mac(IBlockCipher cipher, IBlockCipherPadding padding) : this(cipher, cipher.GetBlockSize() * 8, padding)
		{
		}

		// Token: 0x06001FD1 RID: 8145 RVA: 0x000B8E28 File Offset: 0x000B8E28
		public ISO9797Alg3Mac(IBlockCipher cipher, int macSizeInBits) : this(cipher, macSizeInBits, null)
		{
		}

		// Token: 0x06001FD2 RID: 8146 RVA: 0x000B8E34 File Offset: 0x000B8E34
		public ISO9797Alg3Mac(IBlockCipher cipher, int macSizeInBits, IBlockCipherPadding padding)
		{
			if (macSizeInBits % 8 != 0)
			{
				throw new ArgumentException("MAC size must be multiple of 8");
			}
			if (!(cipher is DesEngine))
			{
				throw new ArgumentException("cipher must be instance of DesEngine");
			}
			this.cipher = new CbcBlockCipher(cipher);
			this.padding = padding;
			this.macSize = macSizeInBits / 8;
			this.mac = new byte[cipher.GetBlockSize()];
			this.buf = new byte[cipher.GetBlockSize()];
			this.bufOff = 0;
		}

		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x06001FD3 RID: 8147 RVA: 0x000B8EBC File Offset: 0x000B8EBC
		public string AlgorithmName
		{
			get
			{
				return "ISO9797Alg3";
			}
		}

		// Token: 0x06001FD4 RID: 8148 RVA: 0x000B8EC4 File Offset: 0x000B8EC4
		public void Init(ICipherParameters parameters)
		{
			this.Reset();
			if (!(parameters is KeyParameter) && !(parameters is ParametersWithIV))
			{
				throw new ArgumentException("parameters must be an instance of KeyParameter or ParametersWithIV");
			}
			KeyParameter keyParameter;
			if (parameters is KeyParameter)
			{
				keyParameter = (KeyParameter)parameters;
			}
			else
			{
				keyParameter = (KeyParameter)((ParametersWithIV)parameters).Parameters;
			}
			byte[] key = keyParameter.GetKey();
			KeyParameter parameters2;
			if (key.Length == 16)
			{
				parameters2 = new KeyParameter(key, 0, 8);
				this.lastKey2 = new KeyParameter(key, 8, 8);
				this.lastKey3 = parameters2;
			}
			else
			{
				if (key.Length != 24)
				{
					throw new ArgumentException("Key must be either 112 or 168 bit long");
				}
				parameters2 = new KeyParameter(key, 0, 8);
				this.lastKey2 = new KeyParameter(key, 8, 8);
				this.lastKey3 = new KeyParameter(key, 16, 8);
			}
			if (parameters is ParametersWithIV)
			{
				this.cipher.Init(true, new ParametersWithIV(parameters2, ((ParametersWithIV)parameters).GetIV()));
				return;
			}
			this.cipher.Init(true, parameters2);
		}

		// Token: 0x06001FD5 RID: 8149 RVA: 0x000B8FD0 File Offset: 0x000B8FD0
		public int GetMacSize()
		{
			return this.macSize;
		}

		// Token: 0x06001FD6 RID: 8150 RVA: 0x000B8FD8 File Offset: 0x000B8FD8
		public void Update(byte input)
		{
			if (this.bufOff == this.buf.Length)
			{
				this.cipher.ProcessBlock(this.buf, 0, this.mac, 0);
				this.bufOff = 0;
			}
			this.buf[this.bufOff++] = input;
		}

		// Token: 0x06001FD7 RID: 8151 RVA: 0x000B9038 File Offset: 0x000B9038
		public void BlockUpdate(byte[] input, int inOff, int len)
		{
			if (len < 0)
			{
				throw new ArgumentException("Can't have a negative input length!");
			}
			int blockSize = this.cipher.GetBlockSize();
			int num = 0;
			int num2 = blockSize - this.bufOff;
			if (len > num2)
			{
				Array.Copy(input, inOff, this.buf, this.bufOff, num2);
				num += this.cipher.ProcessBlock(this.buf, 0, this.mac, 0);
				this.bufOff = 0;
				len -= num2;
				inOff += num2;
				while (len > blockSize)
				{
					num += this.cipher.ProcessBlock(input, inOff, this.mac, 0);
					len -= blockSize;
					inOff += blockSize;
				}
			}
			Array.Copy(input, inOff, this.buf, this.bufOff, len);
			this.bufOff += len;
		}

		// Token: 0x06001FD8 RID: 8152 RVA: 0x000B9108 File Offset: 0x000B9108
		public int DoFinal(byte[] output, int outOff)
		{
			int blockSize = this.cipher.GetBlockSize();
			if (this.padding == null)
			{
				while (this.bufOff < blockSize)
				{
					this.buf[this.bufOff++] = 0;
				}
			}
			else
			{
				if (this.bufOff == blockSize)
				{
					this.cipher.ProcessBlock(this.buf, 0, this.mac, 0);
					this.bufOff = 0;
				}
				this.padding.AddPadding(this.buf, this.bufOff);
			}
			this.cipher.ProcessBlock(this.buf, 0, this.mac, 0);
			DesEngine desEngine = new DesEngine();
			desEngine.Init(false, this.lastKey2);
			desEngine.ProcessBlock(this.mac, 0, this.mac, 0);
			desEngine.Init(true, this.lastKey3);
			desEngine.ProcessBlock(this.mac, 0, this.mac, 0);
			Array.Copy(this.mac, 0, output, outOff, this.macSize);
			this.Reset();
			return this.macSize;
		}

		// Token: 0x06001FD9 RID: 8153 RVA: 0x000B9228 File Offset: 0x000B9228
		public void Reset()
		{
			Array.Clear(this.buf, 0, this.buf.Length);
			this.bufOff = 0;
			this.cipher.Reset();
		}

		// Token: 0x040014CF RID: 5327
		private byte[] mac;

		// Token: 0x040014D0 RID: 5328
		private byte[] buf;

		// Token: 0x040014D1 RID: 5329
		private int bufOff;

		// Token: 0x040014D2 RID: 5330
		private IBlockCipher cipher;

		// Token: 0x040014D3 RID: 5331
		private IBlockCipherPadding padding;

		// Token: 0x040014D4 RID: 5332
		private int macSize;

		// Token: 0x040014D5 RID: 5333
		private KeyParameter lastKey2;

		// Token: 0x040014D6 RID: 5334
		private KeyParameter lastKey3;
	}
}
