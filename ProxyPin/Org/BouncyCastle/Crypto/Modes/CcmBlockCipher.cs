using System;
using System.IO;
using Org.BouncyCastle.Crypto.Macs;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Modes
{
	// Token: 0x020003FB RID: 1019
	public class CcmBlockCipher : IAeadBlockCipher, IAeadCipher
	{
		// Token: 0x06002053 RID: 8275 RVA: 0x000BBD68 File Offset: 0x000BBD68
		public CcmBlockCipher(IBlockCipher cipher)
		{
			this.cipher = cipher;
			this.macBlock = new byte[CcmBlockCipher.BlockSize];
			if (cipher.GetBlockSize() != CcmBlockCipher.BlockSize)
			{
				throw new ArgumentException("cipher required with a block size of " + CcmBlockCipher.BlockSize + ".");
			}
		}

		// Token: 0x06002054 RID: 8276 RVA: 0x000BBDDC File Offset: 0x000BBDDC
		public virtual IBlockCipher GetUnderlyingCipher()
		{
			return this.cipher;
		}

		// Token: 0x06002055 RID: 8277 RVA: 0x000BBDE4 File Offset: 0x000BBDE4
		public virtual void Init(bool forEncryption, ICipherParameters parameters)
		{
			this.forEncryption = forEncryption;
			ICipherParameters cipherParameters;
			if (parameters is AeadParameters)
			{
				AeadParameters aeadParameters = (AeadParameters)parameters;
				this.nonce = aeadParameters.GetNonce();
				this.initialAssociatedText = aeadParameters.GetAssociatedText();
				this.macSize = this.GetMacSize(forEncryption, aeadParameters.MacSize);
				cipherParameters = aeadParameters.Key;
			}
			else
			{
				if (!(parameters is ParametersWithIV))
				{
					throw new ArgumentException("invalid parameters passed to CCM");
				}
				ParametersWithIV parametersWithIV = (ParametersWithIV)parameters;
				this.nonce = parametersWithIV.GetIV();
				this.initialAssociatedText = null;
				this.macSize = this.GetMacSize(forEncryption, 64);
				cipherParameters = parametersWithIV.Parameters;
			}
			if (cipherParameters != null)
			{
				this.keyParam = cipherParameters;
			}
			if (this.nonce == null || this.nonce.Length < 7 || this.nonce.Length > 13)
			{
				throw new ArgumentException("nonce must have length from 7 to 13 octets");
			}
			this.Reset();
		}

		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x06002056 RID: 8278 RVA: 0x000BBED8 File Offset: 0x000BBED8
		public virtual string AlgorithmName
		{
			get
			{
				return this.cipher.AlgorithmName + "/CCM";
			}
		}

		// Token: 0x06002057 RID: 8279 RVA: 0x000BBEF0 File Offset: 0x000BBEF0
		public virtual int GetBlockSize()
		{
			return this.cipher.GetBlockSize();
		}

		// Token: 0x06002058 RID: 8280 RVA: 0x000BBF00 File Offset: 0x000BBF00
		public virtual void ProcessAadByte(byte input)
		{
			this.associatedText.WriteByte(input);
		}

		// Token: 0x06002059 RID: 8281 RVA: 0x000BBF10 File Offset: 0x000BBF10
		public virtual void ProcessAadBytes(byte[] inBytes, int inOff, int len)
		{
			this.associatedText.Write(inBytes, inOff, len);
		}

		// Token: 0x0600205A RID: 8282 RVA: 0x000BBF20 File Offset: 0x000BBF20
		public virtual int ProcessByte(byte input, byte[] outBytes, int outOff)
		{
			this.data.WriteByte(input);
			return 0;
		}

		// Token: 0x0600205B RID: 8283 RVA: 0x000BBF30 File Offset: 0x000BBF30
		public virtual int ProcessBytes(byte[] inBytes, int inOff, int inLen, byte[] outBytes, int outOff)
		{
			Check.DataLength(inBytes, inOff, inLen, "Input buffer too short");
			this.data.Write(inBytes, inOff, inLen);
			return 0;
		}

		// Token: 0x0600205C RID: 8284 RVA: 0x000BBF50 File Offset: 0x000BBF50
		public virtual int DoFinal(byte[] outBytes, int outOff)
		{
			byte[] buffer = this.data.GetBuffer();
			int inLen = (int)this.data.Position;
			int result = this.ProcessPacket(buffer, 0, inLen, outBytes, outOff);
			this.Reset();
			return result;
		}

		// Token: 0x0600205D RID: 8285 RVA: 0x000BBF90 File Offset: 0x000BBF90
		public virtual void Reset()
		{
			this.cipher.Reset();
			this.associatedText.SetLength(0L);
			this.data.SetLength(0L);
		}

		// Token: 0x0600205E RID: 8286 RVA: 0x000BBFB8 File Offset: 0x000BBFB8
		public virtual byte[] GetMac()
		{
			return Arrays.CopyOfRange(this.macBlock, 0, this.macSize);
		}

		// Token: 0x0600205F RID: 8287 RVA: 0x000BBFCC File Offset: 0x000BBFCC
		public virtual int GetUpdateOutputSize(int len)
		{
			return 0;
		}

		// Token: 0x06002060 RID: 8288 RVA: 0x000BBFD0 File Offset: 0x000BBFD0
		public virtual int GetOutputSize(int len)
		{
			int num = (int)this.data.Length + len;
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

		// Token: 0x06002061 RID: 8289 RVA: 0x000BC01C File Offset: 0x000BC01C
		public virtual byte[] ProcessPacket(byte[] input, int inOff, int inLen)
		{
			byte[] array;
			if (this.forEncryption)
			{
				array = new byte[inLen + this.macSize];
			}
			else
			{
				if (inLen < this.macSize)
				{
					throw new InvalidCipherTextException("data too short");
				}
				array = new byte[inLen - this.macSize];
			}
			this.ProcessPacket(input, inOff, inLen, array, 0);
			return array;
		}

		// Token: 0x06002062 RID: 8290 RVA: 0x000BC080 File Offset: 0x000BC080
		public virtual int ProcessPacket(byte[] input, int inOff, int inLen, byte[] output, int outOff)
		{
			if (this.keyParam == null)
			{
				throw new InvalidOperationException("CCM cipher unitialized.");
			}
			int num = this.nonce.Length;
			int num2 = 15 - num;
			if (num2 < 4)
			{
				int num3 = 1 << 8 * num2;
				if (inLen >= num3)
				{
					throw new InvalidOperationException("CCM packet too large for choice of q.");
				}
			}
			byte[] array = new byte[CcmBlockCipher.BlockSize];
			array[0] = (byte)(num2 - 1 & 7);
			this.nonce.CopyTo(array, 1);
			IBlockCipher blockCipher = new SicBlockCipher(this.cipher);
			blockCipher.Init(this.forEncryption, new ParametersWithIV(this.keyParam, array));
			int i = inOff;
			int num4 = outOff;
			int num5;
			if (this.forEncryption)
			{
				num5 = inLen + this.macSize;
				Check.OutputLength(output, outOff, num5, "Output buffer too short.");
				this.CalculateMac(input, inOff, inLen, this.macBlock);
				byte[] array2 = new byte[CcmBlockCipher.BlockSize];
				blockCipher.ProcessBlock(this.macBlock, 0, array2, 0);
				while (i < inOff + inLen - CcmBlockCipher.BlockSize)
				{
					blockCipher.ProcessBlock(input, i, output, num4);
					num4 += CcmBlockCipher.BlockSize;
					i += CcmBlockCipher.BlockSize;
				}
				byte[] array3 = new byte[CcmBlockCipher.BlockSize];
				Array.Copy(input, i, array3, 0, inLen + inOff - i);
				blockCipher.ProcessBlock(array3, 0, array3, 0);
				Array.Copy(array3, 0, output, num4, inLen + inOff - i);
				Array.Copy(array2, 0, output, outOff + inLen, this.macSize);
			}
			else
			{
				if (inLen < this.macSize)
				{
					throw new InvalidCipherTextException("data too short");
				}
				num5 = inLen - this.macSize;
				Check.OutputLength(output, outOff, num5, "Output buffer too short.");
				Array.Copy(input, inOff + num5, this.macBlock, 0, this.macSize);
				blockCipher.ProcessBlock(this.macBlock, 0, this.macBlock, 0);
				for (int num6 = this.macSize; num6 != this.macBlock.Length; num6++)
				{
					this.macBlock[num6] = 0;
				}
				while (i < inOff + num5 - CcmBlockCipher.BlockSize)
				{
					blockCipher.ProcessBlock(input, i, output, num4);
					num4 += CcmBlockCipher.BlockSize;
					i += CcmBlockCipher.BlockSize;
				}
				byte[] array4 = new byte[CcmBlockCipher.BlockSize];
				Array.Copy(input, i, array4, 0, num5 - (i - inOff));
				blockCipher.ProcessBlock(array4, 0, array4, 0);
				Array.Copy(array4, 0, output, num4, num5 - (i - inOff));
				byte[] b = new byte[CcmBlockCipher.BlockSize];
				this.CalculateMac(output, outOff, num5, b);
				if (!Arrays.ConstantTimeAreEqual(this.macBlock, b))
				{
					throw new InvalidCipherTextException("mac check in CCM failed");
				}
			}
			return num5;
		}

		// Token: 0x06002063 RID: 8291 RVA: 0x000BC33C File Offset: 0x000BC33C
		private int CalculateMac(byte[] data, int dataOff, int dataLen, byte[] macBlock)
		{
			IMac mac = new CbcBlockCipherMac(this.cipher, this.macSize * 8);
			mac.Init(this.keyParam);
			byte[] array = new byte[16];
			byte[] array2;
			if (this.HasAssociatedText())
			{
				(array2 = array)[0] = (array2[0] | 64);
			}
			(array2 = array)[0] = (array2[0] | (byte)(((mac.GetMacSize() - 2) / 2 & 7) << 3));
			(array2 = array)[0] = (array2[0] | (byte)(15 - this.nonce.Length - 1 & 7));
			Array.Copy(this.nonce, 0, array, 1, this.nonce.Length);
			int i = dataLen;
			int num = 1;
			while (i > 0)
			{
				array[array.Length - num] = (byte)(i & 255);
				i >>= 8;
				num++;
			}
			mac.BlockUpdate(array, 0, array.Length);
			if (this.HasAssociatedText())
			{
				int associatedTextLength = this.GetAssociatedTextLength();
				int num2;
				if (associatedTextLength < 65280)
				{
					mac.Update((byte)(associatedTextLength >> 8));
					mac.Update((byte)associatedTextLength);
					num2 = 2;
				}
				else
				{
					mac.Update(byte.MaxValue);
					mac.Update(254);
					mac.Update((byte)(associatedTextLength >> 24));
					mac.Update((byte)(associatedTextLength >> 16));
					mac.Update((byte)(associatedTextLength >> 8));
					mac.Update((byte)associatedTextLength);
					num2 = 6;
				}
				if (this.initialAssociatedText != null)
				{
					mac.BlockUpdate(this.initialAssociatedText, 0, this.initialAssociatedText.Length);
				}
				if (this.associatedText.Position > 0L)
				{
					byte[] buffer = this.associatedText.GetBuffer();
					int len = (int)this.associatedText.Position;
					mac.BlockUpdate(buffer, 0, len);
				}
				num2 = (num2 + associatedTextLength) % 16;
				if (num2 != 0)
				{
					for (int j = num2; j < 16; j++)
					{
						mac.Update(0);
					}
				}
			}
			mac.BlockUpdate(data, dataOff, dataLen);
			return mac.DoFinal(macBlock, 0);
		}

		// Token: 0x06002064 RID: 8292 RVA: 0x000BC520 File Offset: 0x000BC520
		private int GetMacSize(bool forEncryption, int requestedMacBits)
		{
			if (forEncryption && (requestedMacBits < 32 || requestedMacBits > 128 || (requestedMacBits & 15) != 0))
			{
				throw new ArgumentException("tag length in octets must be one of {4,6,8,10,12,14,16}");
			}
			return requestedMacBits >> 3;
		}

		// Token: 0x06002065 RID: 8293 RVA: 0x000BC554 File Offset: 0x000BC554
		private int GetAssociatedTextLength()
		{
			return (int)this.associatedText.Length + ((this.initialAssociatedText == null) ? 0 : this.initialAssociatedText.Length);
		}

		// Token: 0x06002066 RID: 8294 RVA: 0x000BC57C File Offset: 0x000BC57C
		private bool HasAssociatedText()
		{
			return this.GetAssociatedTextLength() > 0;
		}

		// Token: 0x04001518 RID: 5400
		private static readonly int BlockSize = 16;

		// Token: 0x04001519 RID: 5401
		private readonly IBlockCipher cipher;

		// Token: 0x0400151A RID: 5402
		private readonly byte[] macBlock;

		// Token: 0x0400151B RID: 5403
		private bool forEncryption;

		// Token: 0x0400151C RID: 5404
		private byte[] nonce;

		// Token: 0x0400151D RID: 5405
		private byte[] initialAssociatedText;

		// Token: 0x0400151E RID: 5406
		private int macSize;

		// Token: 0x0400151F RID: 5407
		private ICipherParameters keyParam;

		// Token: 0x04001520 RID: 5408
		private readonly MemoryStream associatedText = new MemoryStream();

		// Token: 0x04001521 RID: 5409
		private readonly MemoryStream data = new MemoryStream();
	}
}
