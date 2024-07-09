using System;
using Org.BouncyCastle.Crypto.Macs;
using Org.BouncyCastle.Crypto.Parameters;

namespace Org.BouncyCastle.Crypto.Modes
{
	// Token: 0x02000402 RID: 1026
	public class EaxBlockCipher : IAeadBlockCipher, IAeadCipher
	{
		// Token: 0x060020C4 RID: 8388 RVA: 0x000BDEE0 File Offset: 0x000BDEE0
		public EaxBlockCipher(IBlockCipher cipher)
		{
			this.blockSize = cipher.GetBlockSize();
			this.mac = new CMac(cipher);
			this.macBlock = new byte[this.blockSize];
			this.associatedTextMac = new byte[this.mac.GetMacSize()];
			this.nonceMac = new byte[this.mac.GetMacSize()];
			this.cipher = new SicBlockCipher(cipher);
		}

		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x060020C5 RID: 8389 RVA: 0x000BDF58 File Offset: 0x000BDF58
		public virtual string AlgorithmName
		{
			get
			{
				return this.cipher.GetUnderlyingCipher().AlgorithmName + "/EAX";
			}
		}

		// Token: 0x060020C6 RID: 8390 RVA: 0x000BDF74 File Offset: 0x000BDF74
		public virtual IBlockCipher GetUnderlyingCipher()
		{
			return this.cipher;
		}

		// Token: 0x060020C7 RID: 8391 RVA: 0x000BDF7C File Offset: 0x000BDF7C
		public virtual int GetBlockSize()
		{
			return this.cipher.GetBlockSize();
		}

		// Token: 0x060020C8 RID: 8392 RVA: 0x000BDF8C File Offset: 0x000BDF8C
		public virtual void Init(bool forEncryption, ICipherParameters parameters)
		{
			this.forEncryption = forEncryption;
			byte[] array;
			ICipherParameters parameters2;
			if (parameters is AeadParameters)
			{
				AeadParameters aeadParameters = (AeadParameters)parameters;
				array = aeadParameters.GetNonce();
				this.initialAssociatedText = aeadParameters.GetAssociatedText();
				this.macSize = aeadParameters.MacSize / 8;
				parameters2 = aeadParameters.Key;
			}
			else
			{
				if (!(parameters is ParametersWithIV))
				{
					throw new ArgumentException("invalid parameters passed to EAX");
				}
				ParametersWithIV parametersWithIV = (ParametersWithIV)parameters;
				array = parametersWithIV.GetIV();
				this.initialAssociatedText = null;
				this.macSize = this.mac.GetMacSize() / 2;
				parameters2 = parametersWithIV.Parameters;
			}
			this.bufBlock = new byte[forEncryption ? this.blockSize : (this.blockSize + this.macSize)];
			byte[] array2 = new byte[this.blockSize];
			this.mac.Init(parameters2);
			array2[this.blockSize - 1] = 0;
			this.mac.BlockUpdate(array2, 0, this.blockSize);
			this.mac.BlockUpdate(array, 0, array.Length);
			this.mac.DoFinal(this.nonceMac, 0);
			this.cipher.Init(true, new ParametersWithIV(null, this.nonceMac));
			this.Reset();
		}

		// Token: 0x060020C9 RID: 8393 RVA: 0x000BE0D0 File Offset: 0x000BE0D0
		private void InitCipher()
		{
			if (this.cipherInitialized)
			{
				return;
			}
			this.cipherInitialized = true;
			this.mac.DoFinal(this.associatedTextMac, 0);
			byte[] array = new byte[this.blockSize];
			array[this.blockSize - 1] = 2;
			this.mac.BlockUpdate(array, 0, this.blockSize);
		}

		// Token: 0x060020CA RID: 8394 RVA: 0x000BE134 File Offset: 0x000BE134
		private void CalculateMac()
		{
			byte[] array = new byte[this.blockSize];
			this.mac.DoFinal(array, 0);
			for (int i = 0; i < this.macBlock.Length; i++)
			{
				this.macBlock[i] = (this.nonceMac[i] ^ this.associatedTextMac[i] ^ array[i]);
			}
		}

		// Token: 0x060020CB RID: 8395 RVA: 0x000BE194 File Offset: 0x000BE194
		public virtual void Reset()
		{
			this.Reset(true);
		}

		// Token: 0x060020CC RID: 8396 RVA: 0x000BE1A0 File Offset: 0x000BE1A0
		private void Reset(bool clearMac)
		{
			this.cipher.Reset();
			this.mac.Reset();
			this.bufOff = 0;
			Array.Clear(this.bufBlock, 0, this.bufBlock.Length);
			if (clearMac)
			{
				Array.Clear(this.macBlock, 0, this.macBlock.Length);
			}
			byte[] array = new byte[this.blockSize];
			array[this.blockSize - 1] = 1;
			this.mac.BlockUpdate(array, 0, this.blockSize);
			this.cipherInitialized = false;
			if (this.initialAssociatedText != null)
			{
				this.ProcessAadBytes(this.initialAssociatedText, 0, this.initialAssociatedText.Length);
			}
		}

		// Token: 0x060020CD RID: 8397 RVA: 0x000BE250 File Offset: 0x000BE250
		public virtual void ProcessAadByte(byte input)
		{
			if (this.cipherInitialized)
			{
				throw new InvalidOperationException("AAD data cannot be added after encryption/decryption processing has begun.");
			}
			this.mac.Update(input);
		}

		// Token: 0x060020CE RID: 8398 RVA: 0x000BE274 File Offset: 0x000BE274
		public virtual void ProcessAadBytes(byte[] inBytes, int inOff, int len)
		{
			if (this.cipherInitialized)
			{
				throw new InvalidOperationException("AAD data cannot be added after encryption/decryption processing has begun.");
			}
			this.mac.BlockUpdate(inBytes, inOff, len);
		}

		// Token: 0x060020CF RID: 8399 RVA: 0x000BE29C File Offset: 0x000BE29C
		public virtual int ProcessByte(byte input, byte[] outBytes, int outOff)
		{
			this.InitCipher();
			return this.Process(input, outBytes, outOff);
		}

		// Token: 0x060020D0 RID: 8400 RVA: 0x000BE2B0 File Offset: 0x000BE2B0
		public virtual int ProcessBytes(byte[] inBytes, int inOff, int len, byte[] outBytes, int outOff)
		{
			this.InitCipher();
			int num = 0;
			for (int num2 = 0; num2 != len; num2++)
			{
				num += this.Process(inBytes[inOff + num2], outBytes, outOff + num);
			}
			return num;
		}

		// Token: 0x060020D1 RID: 8401 RVA: 0x000BE2F0 File Offset: 0x000BE2F0
		public virtual int DoFinal(byte[] outBytes, int outOff)
		{
			this.InitCipher();
			int num = this.bufOff;
			byte[] array = new byte[this.bufBlock.Length];
			this.bufOff = 0;
			if (this.forEncryption)
			{
				Check.OutputLength(outBytes, outOff, num + this.macSize, "Output buffer too short");
				this.cipher.ProcessBlock(this.bufBlock, 0, array, 0);
				Array.Copy(array, 0, outBytes, outOff, num);
				this.mac.BlockUpdate(array, 0, num);
				this.CalculateMac();
				Array.Copy(this.macBlock, 0, outBytes, outOff + num, this.macSize);
				this.Reset(false);
				return num + this.macSize;
			}
			if (num < this.macSize)
			{
				throw new InvalidCipherTextException("data too short");
			}
			Check.OutputLength(outBytes, outOff, num - this.macSize, "Output buffer too short");
			if (num > this.macSize)
			{
				this.mac.BlockUpdate(this.bufBlock, 0, num - this.macSize);
				this.cipher.ProcessBlock(this.bufBlock, 0, array, 0);
				Array.Copy(array, 0, outBytes, outOff, num - this.macSize);
			}
			this.CalculateMac();
			if (!this.VerifyMac(this.bufBlock, num - this.macSize))
			{
				throw new InvalidCipherTextException("mac check in EAX failed");
			}
			this.Reset(false);
			return num - this.macSize;
		}

		// Token: 0x060020D2 RID: 8402 RVA: 0x000BE44C File Offset: 0x000BE44C
		public virtual byte[] GetMac()
		{
			byte[] array = new byte[this.macSize];
			Array.Copy(this.macBlock, 0, array, 0, this.macSize);
			return array;
		}

		// Token: 0x060020D3 RID: 8403 RVA: 0x000BE480 File Offset: 0x000BE480
		public virtual int GetUpdateOutputSize(int len)
		{
			int num = len + this.bufOff;
			if (!this.forEncryption)
			{
				if (num < this.macSize)
				{
					return 0;
				}
				num -= this.macSize;
			}
			return num - num % this.blockSize;
		}

		// Token: 0x060020D4 RID: 8404 RVA: 0x000BE4C8 File Offset: 0x000BE4C8
		public virtual int GetOutputSize(int len)
		{
			int num = len + this.bufOff;
			if (this.forEncryption)
			{
				return num + this.macSize;
			}
			if (num >= this.macSize)
			{
				return num - this.macSize;
			}
			return 0;
		}

		// Token: 0x060020D5 RID: 8405 RVA: 0x000BE50C File Offset: 0x000BE50C
		private int Process(byte b, byte[] outBytes, int outOff)
		{
			this.bufBlock[this.bufOff++] = b;
			if (this.bufOff == this.bufBlock.Length)
			{
				Check.OutputLength(outBytes, outOff, this.blockSize, "Output buffer is too short");
				int result;
				if (this.forEncryption)
				{
					result = this.cipher.ProcessBlock(this.bufBlock, 0, outBytes, outOff);
					this.mac.BlockUpdate(outBytes, outOff, this.blockSize);
				}
				else
				{
					this.mac.BlockUpdate(this.bufBlock, 0, this.blockSize);
					result = this.cipher.ProcessBlock(this.bufBlock, 0, outBytes, outOff);
				}
				this.bufOff = 0;
				if (!this.forEncryption)
				{
					Array.Copy(this.bufBlock, this.blockSize, this.bufBlock, 0, this.macSize);
					this.bufOff = this.macSize;
				}
				return result;
			}
			return 0;
		}

		// Token: 0x060020D6 RID: 8406 RVA: 0x000BE600 File Offset: 0x000BE600
		private bool VerifyMac(byte[] mac, int off)
		{
			int num = 0;
			for (int i = 0; i < this.macSize; i++)
			{
				num |= (int)(this.macBlock[i] ^ mac[off + i]);
			}
			return num == 0;
		}

		// Token: 0x04001540 RID: 5440
		private SicBlockCipher cipher;

		// Token: 0x04001541 RID: 5441
		private bool forEncryption;

		// Token: 0x04001542 RID: 5442
		private int blockSize;

		// Token: 0x04001543 RID: 5443
		private IMac mac;

		// Token: 0x04001544 RID: 5444
		private byte[] nonceMac;

		// Token: 0x04001545 RID: 5445
		private byte[] associatedTextMac;

		// Token: 0x04001546 RID: 5446
		private byte[] macBlock;

		// Token: 0x04001547 RID: 5447
		private int macSize;

		// Token: 0x04001548 RID: 5448
		private byte[] bufBlock;

		// Token: 0x04001549 RID: 5449
		private int bufOff;

		// Token: 0x0400154A RID: 5450
		private bool cipherInitialized;

		// Token: 0x0400154B RID: 5451
		private byte[] initialAssociatedText;

		// Token: 0x02000E0D RID: 3597
		private enum Tag : byte
		{
			// Token: 0x04004142 RID: 16706
			N,
			// Token: 0x04004143 RID: 16707
			H,
			// Token: 0x04004144 RID: 16708
			C
		}
	}
}
