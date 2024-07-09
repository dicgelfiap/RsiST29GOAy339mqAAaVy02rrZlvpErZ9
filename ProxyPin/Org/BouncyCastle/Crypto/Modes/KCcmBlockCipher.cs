using System;
using System.IO;
using System.Text;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Modes
{
	// Token: 0x02000405 RID: 1029
	public class KCcmBlockCipher : IAeadBlockCipher, IAeadCipher
	{
		// Token: 0x060020FA RID: 8442 RVA: 0x000BF7A0 File Offset: 0x000BF7A0
		private void setNb(int Nb)
		{
			if (Nb == 4 || Nb == 6 || Nb == 8)
			{
				this.Nb_ = Nb;
				return;
			}
			throw new ArgumentException("Nb = 4 is recommended by DSTU7624 but can be changed to only 6 or 8 in this implementation");
		}

		// Token: 0x060020FB RID: 8443 RVA: 0x000BF7CC File Offset: 0x000BF7CC
		public KCcmBlockCipher(IBlockCipher engine) : this(engine, 4)
		{
		}

		// Token: 0x060020FC RID: 8444 RVA: 0x000BF7D8 File Offset: 0x000BF7D8
		public KCcmBlockCipher(IBlockCipher engine, int Nb)
		{
			this.engine = engine;
			this.macSize = engine.GetBlockSize();
			this.nonce = new byte[engine.GetBlockSize()];
			this.initialAssociatedText = new byte[engine.GetBlockSize()];
			this.mac = new byte[engine.GetBlockSize()];
			this.macBlock = new byte[engine.GetBlockSize()];
			this.G1 = new byte[engine.GetBlockSize()];
			this.buffer = new byte[engine.GetBlockSize()];
			this.s = new byte[engine.GetBlockSize()];
			this.counter = new byte[engine.GetBlockSize()];
			this.setNb(Nb);
		}

		// Token: 0x060020FD RID: 8445 RVA: 0x000BF8B0 File Offset: 0x000BF8B0
		public virtual void Init(bool forEncryption, ICipherParameters parameters)
		{
			ICipherParameters parameters2;
			if (parameters is AeadParameters)
			{
				AeadParameters aeadParameters = (AeadParameters)parameters;
				if (aeadParameters.MacSize > KCcmBlockCipher.MAX_MAC_BIT_LENGTH || aeadParameters.MacSize < KCcmBlockCipher.MIN_MAC_BIT_LENGTH || aeadParameters.MacSize % 8 != 0)
				{
					throw new ArgumentException("Invalid mac size specified");
				}
				this.nonce = aeadParameters.GetNonce();
				this.macSize = aeadParameters.MacSize / KCcmBlockCipher.BITS_IN_BYTE;
				this.initialAssociatedText = aeadParameters.GetAssociatedText();
				parameters2 = aeadParameters.Key;
			}
			else
			{
				if (!(parameters is ParametersWithIV))
				{
					throw new ArgumentException("Invalid parameters specified");
				}
				this.nonce = ((ParametersWithIV)parameters).GetIV();
				this.macSize = this.engine.GetBlockSize();
				this.initialAssociatedText = null;
				parameters2 = ((ParametersWithIV)parameters).Parameters;
			}
			this.mac = new byte[this.macSize];
			this.forEncryption = forEncryption;
			this.engine.Init(true, parameters2);
			this.counter[0] = 1;
			if (this.initialAssociatedText != null)
			{
				this.ProcessAadBytes(this.initialAssociatedText, 0, this.initialAssociatedText.Length);
			}
		}

		// Token: 0x17000639 RID: 1593
		// (get) Token: 0x060020FE RID: 8446 RVA: 0x000BF9E0 File Offset: 0x000BF9E0
		public virtual string AlgorithmName
		{
			get
			{
				return this.engine.AlgorithmName + "/KCCM";
			}
		}

		// Token: 0x060020FF RID: 8447 RVA: 0x000BF9F8 File Offset: 0x000BF9F8
		public virtual int GetBlockSize()
		{
			return this.engine.GetBlockSize();
		}

		// Token: 0x06002100 RID: 8448 RVA: 0x000BFA08 File Offset: 0x000BFA08
		public virtual IBlockCipher GetUnderlyingCipher()
		{
			return this.engine;
		}

		// Token: 0x06002101 RID: 8449 RVA: 0x000BFA10 File Offset: 0x000BFA10
		public virtual void ProcessAadByte(byte input)
		{
			this.associatedText.WriteByte(input);
		}

		// Token: 0x06002102 RID: 8450 RVA: 0x000BFA20 File Offset: 0x000BFA20
		public virtual void ProcessAadBytes(byte[] input, int inOff, int len)
		{
			this.associatedText.Write(input, inOff, len);
		}

		// Token: 0x06002103 RID: 8451 RVA: 0x000BFA30 File Offset: 0x000BFA30
		private void ProcessAAD(byte[] assocText, int assocOff, int assocLen, int dataLen)
		{
			if (assocLen - assocOff < this.engine.GetBlockSize())
			{
				throw new ArgumentException("authText buffer too short");
			}
			if (assocLen % this.engine.GetBlockSize() != 0)
			{
				throw new ArgumentException("padding not supported");
			}
			Array.Copy(this.nonce, 0, this.G1, 0, this.nonce.Length - this.Nb_ - 1);
			this.intToBytes(dataLen, this.buffer, 0);
			Array.Copy(this.buffer, 0, this.G1, this.nonce.Length - this.Nb_ - 1, KCcmBlockCipher.BYTES_IN_INT);
			this.G1[this.G1.Length - 1] = this.getFlag(true, this.macSize);
			this.engine.ProcessBlock(this.G1, 0, this.macBlock, 0);
			this.intToBytes(assocLen, this.buffer, 0);
			if (assocLen <= this.engine.GetBlockSize() - this.Nb_)
			{
				for (int i = 0; i < assocLen; i++)
				{
					byte[] array;
					IntPtr intPtr;
					(array = this.buffer)[(int)(intPtr = (IntPtr)(i + this.Nb_))] = (array[(int)intPtr] ^ assocText[assocOff + i]);
				}
				for (int j = 0; j < this.engine.GetBlockSize(); j++)
				{
					byte[] array;
					IntPtr intPtr;
					(array = this.macBlock)[(int)(intPtr = (IntPtr)j)] = (array[(int)intPtr] ^ this.buffer[j]);
				}
				this.engine.ProcessBlock(this.macBlock, 0, this.macBlock, 0);
				return;
			}
			for (int k = 0; k < this.engine.GetBlockSize(); k++)
			{
				byte[] array;
				IntPtr intPtr;
				(array = this.macBlock)[(int)(intPtr = (IntPtr)k)] = (array[(int)intPtr] ^ this.buffer[k]);
			}
			this.engine.ProcessBlock(this.macBlock, 0, this.macBlock, 0);
			for (int num = assocLen; num != 0; num -= this.engine.GetBlockSize())
			{
				for (int l = 0; l < this.engine.GetBlockSize(); l++)
				{
					byte[] array;
					IntPtr intPtr;
					(array = this.macBlock)[(int)(intPtr = (IntPtr)l)] = (array[(int)intPtr] ^ assocText[l + assocOff]);
				}
				this.engine.ProcessBlock(this.macBlock, 0, this.macBlock, 0);
				assocOff += this.engine.GetBlockSize();
			}
		}

		// Token: 0x06002104 RID: 8452 RVA: 0x000BFC7C File Offset: 0x000BFC7C
		public virtual int ProcessByte(byte input, byte[] output, int outOff)
		{
			this.data.WriteByte(input);
			return 0;
		}

		// Token: 0x06002105 RID: 8453 RVA: 0x000BFC8C File Offset: 0x000BFC8C
		public virtual int ProcessBytes(byte[] input, int inOff, int inLen, byte[] output, int outOff)
		{
			Check.DataLength(input, inOff, inLen, "input buffer too short");
			this.data.Write(input, inOff, inLen);
			return 0;
		}

		// Token: 0x06002106 RID: 8454 RVA: 0x000BFCAC File Offset: 0x000BFCAC
		public int ProcessPacket(byte[] input, int inOff, int len, byte[] output, int outOff)
		{
			Check.DataLength(input, inOff, len, "input buffer too short");
			Check.OutputLength(output, outOff, len, "output buffer too short");
			if (this.associatedText.Length > 0L)
			{
				byte[] assocText = this.associatedText.GetBuffer();
				int assocLen = (int)this.associatedText.Length;
				int dataLen = this.forEncryption ? ((int)this.data.Length) : ((int)this.data.Length - this.macSize);
				this.ProcessAAD(assocText, 0, assocLen, dataLen);
			}
			if (this.forEncryption)
			{
				Check.DataLength(len % this.engine.GetBlockSize() != 0, "partial blocks not supported");
				this.CalculateMac(input, inOff, len);
				this.engine.ProcessBlock(this.nonce, 0, this.s, 0);
				int i = len;
				while (i > 0)
				{
					this.ProcessBlock(input, inOff, len, output, outOff);
					i -= this.engine.GetBlockSize();
					inOff += this.engine.GetBlockSize();
					outOff += this.engine.GetBlockSize();
				}
				for (int j = 0; j < this.counter.Length; j++)
				{
					byte[] array;
					IntPtr intPtr;
					(array = this.s)[(int)(intPtr = (IntPtr)j)] = array[(int)intPtr] + this.counter[j];
				}
				this.engine.ProcessBlock(this.s, 0, this.buffer, 0);
				for (int k = 0; k < this.macSize; k++)
				{
					output[outOff + k] = (this.buffer[k] ^ this.macBlock[k]);
				}
				Array.Copy(this.macBlock, 0, this.mac, 0, this.macSize);
				this.Reset();
				return len + this.macSize;
			}
			Check.DataLength((len - this.macSize) % this.engine.GetBlockSize() != 0, "partial blocks not supported");
			this.engine.ProcessBlock(this.nonce, 0, this.s, 0);
			int num = len / this.engine.GetBlockSize();
			for (int l = 0; l < num; l++)
			{
				this.ProcessBlock(input, inOff, len, output, outOff);
				inOff += this.engine.GetBlockSize();
				outOff += this.engine.GetBlockSize();
			}
			if (len > inOff)
			{
				for (int m = 0; m < this.counter.Length; m++)
				{
					byte[] array;
					IntPtr intPtr;
					(array = this.s)[(int)(intPtr = (IntPtr)m)] = array[(int)intPtr] + this.counter[m];
				}
				this.engine.ProcessBlock(this.s, 0, this.buffer, 0);
				for (int n = 0; n < this.macSize; n++)
				{
					output[outOff + n] = (this.buffer[n] ^ input[inOff + n]);
				}
				outOff += this.macSize;
			}
			for (int num2 = 0; num2 < this.counter.Length; num2++)
			{
				byte[] array;
				IntPtr intPtr;
				(array = this.s)[(int)(intPtr = (IntPtr)num2)] = array[(int)intPtr] + this.counter[num2];
			}
			this.engine.ProcessBlock(this.s, 0, this.buffer, 0);
			Array.Copy(output, outOff - this.macSize, this.buffer, 0, this.macSize);
			this.CalculateMac(output, 0, outOff - this.macSize);
			Array.Copy(this.macBlock, 0, this.mac, 0, this.macSize);
			byte[] array2 = new byte[this.macSize];
			Array.Copy(this.buffer, 0, array2, 0, this.macSize);
			if (!Arrays.ConstantTimeAreEqual(this.mac, array2))
			{
				throw new InvalidCipherTextException("mac check failed");
			}
			this.Reset();
			return len - this.macSize;
		}

		// Token: 0x06002107 RID: 8455 RVA: 0x000C0088 File Offset: 0x000C0088
		private void ProcessBlock(byte[] input, int inOff, int len, byte[] output, int outOff)
		{
			for (int i = 0; i < this.counter.Length; i++)
			{
				byte[] array;
				IntPtr intPtr;
				(array = this.s)[(int)(intPtr = (IntPtr)i)] = array[(int)intPtr] + this.counter[i];
			}
			this.engine.ProcessBlock(this.s, 0, this.buffer, 0);
			for (int j = 0; j < this.engine.GetBlockSize(); j++)
			{
				output[outOff + j] = (this.buffer[j] ^ input[inOff + j]);
			}
		}

		// Token: 0x06002108 RID: 8456 RVA: 0x000C0114 File Offset: 0x000C0114
		private void CalculateMac(byte[] authText, int authOff, int len)
		{
			int i = len;
			while (i > 0)
			{
				for (int j = 0; j < this.engine.GetBlockSize(); j++)
				{
					byte[] array;
					IntPtr intPtr;
					(array = this.macBlock)[(int)(intPtr = (IntPtr)j)] = (array[(int)intPtr] ^ authText[authOff + j]);
				}
				this.engine.ProcessBlock(this.macBlock, 0, this.macBlock, 0);
				i -= this.engine.GetBlockSize();
				authOff += this.engine.GetBlockSize();
			}
		}

		// Token: 0x06002109 RID: 8457 RVA: 0x000C0198 File Offset: 0x000C0198
		public virtual int DoFinal(byte[] output, int outOff)
		{
			byte[] input = this.data.GetBuffer();
			int len = (int)this.data.Length;
			int result = this.ProcessPacket(input, 0, len, output, outOff);
			this.Reset();
			return result;
		}

		// Token: 0x0600210A RID: 8458 RVA: 0x000C01D8 File Offset: 0x000C01D8
		public virtual byte[] GetMac()
		{
			return Arrays.Clone(this.mac);
		}

		// Token: 0x0600210B RID: 8459 RVA: 0x000C01E8 File Offset: 0x000C01E8
		public virtual int GetUpdateOutputSize(int len)
		{
			return len;
		}

		// Token: 0x0600210C RID: 8460 RVA: 0x000C01EC File Offset: 0x000C01EC
		public virtual int GetOutputSize(int len)
		{
			return len + this.macSize;
		}

		// Token: 0x0600210D RID: 8461 RVA: 0x000C01F8 File Offset: 0x000C01F8
		public virtual void Reset()
		{
			Arrays.Fill(this.G1, 0);
			Arrays.Fill(this.buffer, 0);
			Arrays.Fill(this.counter, 0);
			Arrays.Fill(this.macBlock, 0);
			this.counter[0] = 1;
			this.data.SetLength(0L);
			this.associatedText.SetLength(0L);
			if (this.initialAssociatedText != null)
			{
				this.ProcessAadBytes(this.initialAssociatedText, 0, this.initialAssociatedText.Length);
			}
		}

		// Token: 0x0600210E RID: 8462 RVA: 0x000C027C File Offset: 0x000C027C
		private void intToBytes(int num, byte[] outBytes, int outOff)
		{
			outBytes[outOff + 3] = (byte)(num >> 24);
			outBytes[outOff + 2] = (byte)(num >> 16);
			outBytes[outOff + 1] = (byte)(num >> 8);
			outBytes[outOff] = (byte)num;
		}

		// Token: 0x0600210F RID: 8463 RVA: 0x000C02A0 File Offset: 0x000C02A0
		private byte getFlag(bool authTextPresents, int macSize)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (authTextPresents)
			{
				stringBuilder.Append("1");
			}
			else
			{
				stringBuilder.Append("0");
			}
			if (macSize <= 16)
			{
				if (macSize != 8)
				{
					if (macSize == 16)
					{
						stringBuilder.Append("011");
					}
				}
				else
				{
					stringBuilder.Append("010");
				}
			}
			else if (macSize != 32)
			{
				if (macSize != 48)
				{
					if (macSize == 64)
					{
						stringBuilder.Append("110");
					}
				}
				else
				{
					stringBuilder.Append("101");
				}
			}
			else
			{
				stringBuilder.Append("100");
			}
			string text = Convert.ToString(this.Nb_ - 1, 2);
			while (text.Length < 4)
			{
				text = new StringBuilder(text).Insert(0, "0").ToString();
			}
			stringBuilder.Append(text);
			return (byte)Convert.ToInt32(stringBuilder.ToString(), 2);
		}

		// Token: 0x0400156F RID: 5487
		private static readonly int BYTES_IN_INT = 4;

		// Token: 0x04001570 RID: 5488
		private static readonly int BITS_IN_BYTE = 8;

		// Token: 0x04001571 RID: 5489
		private static readonly int MAX_MAC_BIT_LENGTH = 512;

		// Token: 0x04001572 RID: 5490
		private static readonly int MIN_MAC_BIT_LENGTH = 64;

		// Token: 0x04001573 RID: 5491
		private IBlockCipher engine;

		// Token: 0x04001574 RID: 5492
		private int macSize;

		// Token: 0x04001575 RID: 5493
		private bool forEncryption;

		// Token: 0x04001576 RID: 5494
		private byte[] initialAssociatedText;

		// Token: 0x04001577 RID: 5495
		private byte[] mac;

		// Token: 0x04001578 RID: 5496
		private byte[] macBlock;

		// Token: 0x04001579 RID: 5497
		private byte[] nonce;

		// Token: 0x0400157A RID: 5498
		private byte[] G1;

		// Token: 0x0400157B RID: 5499
		private byte[] buffer;

		// Token: 0x0400157C RID: 5500
		private byte[] s;

		// Token: 0x0400157D RID: 5501
		private byte[] counter;

		// Token: 0x0400157E RID: 5502
		private readonly MemoryStream associatedText = new MemoryStream();

		// Token: 0x0400157F RID: 5503
		private readonly MemoryStream data = new MemoryStream();

		// Token: 0x04001580 RID: 5504
		private int Nb_ = 4;
	}
}
