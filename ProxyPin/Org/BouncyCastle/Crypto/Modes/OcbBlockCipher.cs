using System;
using System.Collections;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Modes
{
	// Token: 0x02000407 RID: 1031
	public class OcbBlockCipher : IAeadBlockCipher, IAeadCipher
	{
		// Token: 0x0600211E RID: 8478 RVA: 0x000C06E8 File Offset: 0x000C06E8
		public OcbBlockCipher(IBlockCipher hashCipher, IBlockCipher mainCipher)
		{
			if (hashCipher == null)
			{
				throw new ArgumentNullException("hashCipher");
			}
			if (hashCipher.GetBlockSize() != 16)
			{
				throw new ArgumentException("must have a block size of " + 16, "hashCipher");
			}
			if (mainCipher == null)
			{
				throw new ArgumentNullException("mainCipher");
			}
			if (mainCipher.GetBlockSize() != 16)
			{
				throw new ArgumentException("must have a block size of " + 16, "mainCipher");
			}
			if (!hashCipher.AlgorithmName.Equals(mainCipher.AlgorithmName))
			{
				throw new ArgumentException("'hashCipher' and 'mainCipher' must be the same algorithm");
			}
			this.hashCipher = hashCipher;
			this.mainCipher = mainCipher;
		}

		// Token: 0x0600211F RID: 8479 RVA: 0x000C07D0 File Offset: 0x000C07D0
		public virtual IBlockCipher GetUnderlyingCipher()
		{
			return this.mainCipher;
		}

		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x06002120 RID: 8480 RVA: 0x000C07D8 File Offset: 0x000C07D8
		public virtual string AlgorithmName
		{
			get
			{
				return this.mainCipher.AlgorithmName + "/OCB";
			}
		}

		// Token: 0x06002121 RID: 8481 RVA: 0x000C07F0 File Offset: 0x000C07F0
		public virtual void Init(bool forEncryption, ICipherParameters parameters)
		{
			bool flag = this.forEncryption;
			this.forEncryption = forEncryption;
			this.macBlock = null;
			byte[] array;
			KeyParameter keyParameter;
			if (parameters is AeadParameters)
			{
				AeadParameters aeadParameters = (AeadParameters)parameters;
				array = aeadParameters.GetNonce();
				this.initialAssociatedText = aeadParameters.GetAssociatedText();
				int num = aeadParameters.MacSize;
				if (num < 64 || num > 128 || num % 8 != 0)
				{
					throw new ArgumentException("Invalid value for MAC size: " + num);
				}
				this.macSize = num / 8;
				keyParameter = aeadParameters.Key;
			}
			else
			{
				if (!(parameters is ParametersWithIV))
				{
					throw new ArgumentException("invalid parameters passed to OCB");
				}
				ParametersWithIV parametersWithIV = (ParametersWithIV)parameters;
				array = parametersWithIV.GetIV();
				this.initialAssociatedText = null;
				this.macSize = 16;
				keyParameter = (KeyParameter)parametersWithIV.Parameters;
			}
			this.hashBlock = new byte[16];
			this.mainBlock = new byte[forEncryption ? 16 : (16 + this.macSize)];
			if (array == null)
			{
				array = new byte[0];
			}
			if (array.Length > 15)
			{
				throw new ArgumentException("IV must be no more than 15 bytes");
			}
			if (keyParameter != null)
			{
				this.hashCipher.Init(true, keyParameter);
				this.mainCipher.Init(forEncryption, keyParameter);
				this.KtopInput = null;
			}
			else if (flag != forEncryption)
			{
				throw new ArgumentException("cannot change encrypting state without providing key.");
			}
			this.L_Asterisk = new byte[16];
			this.hashCipher.ProcessBlock(this.L_Asterisk, 0, this.L_Asterisk, 0);
			this.L_Dollar = OcbBlockCipher.OCB_double(this.L_Asterisk);
			this.L = Platform.CreateArrayList();
			this.L.Add(OcbBlockCipher.OCB_double(this.L_Dollar));
			int num2 = this.ProcessNonce(array);
			int num3 = num2 % 8;
			int num4 = num2 / 8;
			if (num3 == 0)
			{
				Array.Copy(this.Stretch, num4, this.OffsetMAIN_0, 0, 16);
			}
			else
			{
				for (int i = 0; i < 16; i++)
				{
					uint num5 = (uint)this.Stretch[num4];
					uint num6 = (uint)this.Stretch[++num4];
					this.OffsetMAIN_0[i] = (byte)(num5 << num3 | num6 >> 8 - num3);
				}
			}
			this.hashBlockPos = 0;
			this.mainBlockPos = 0;
			this.hashBlockCount = 0L;
			this.mainBlockCount = 0L;
			this.OffsetHASH = new byte[16];
			this.Sum = new byte[16];
			Array.Copy(this.OffsetMAIN_0, 0, this.OffsetMAIN, 0, 16);
			this.Checksum = new byte[16];
			if (this.initialAssociatedText != null)
			{
				this.ProcessAadBytes(this.initialAssociatedText, 0, this.initialAssociatedText.Length);
			}
		}

		// Token: 0x06002122 RID: 8482 RVA: 0x000C0ABC File Offset: 0x000C0ABC
		protected virtual int ProcessNonce(byte[] N)
		{
			byte[] array = new byte[16];
			Array.Copy(N, 0, array, array.Length - N.Length, N.Length);
			array[0] = (byte)(this.macSize << 4);
			byte[] array2;
			IntPtr intPtr;
			(array2 = array)[(int)(intPtr = (IntPtr)(15 - N.Length))] = (array2[(int)intPtr] | 1);
			int result = (int)(array[15] & 63);
			(array2 = array)[15] = (array2[15] & 192);
			if (this.KtopInput == null || !Arrays.AreEqual(array, this.KtopInput))
			{
				byte[] array3 = new byte[16];
				this.KtopInput = array;
				this.hashCipher.ProcessBlock(this.KtopInput, 0, array3, 0);
				Array.Copy(array3, 0, this.Stretch, 0, 16);
				for (int i = 0; i < 8; i++)
				{
					this.Stretch[16 + i] = (array3[i] ^ array3[i + 1]);
				}
			}
			return result;
		}

		// Token: 0x06002123 RID: 8483 RVA: 0x000C0BA0 File Offset: 0x000C0BA0
		public virtual int GetBlockSize()
		{
			return 16;
		}

		// Token: 0x06002124 RID: 8484 RVA: 0x000C0BA4 File Offset: 0x000C0BA4
		public virtual byte[] GetMac()
		{
			if (this.macBlock != null)
			{
				return Arrays.Clone(this.macBlock);
			}
			return new byte[this.macSize];
		}

		// Token: 0x06002125 RID: 8485 RVA: 0x000C0BC8 File Offset: 0x000C0BC8
		public virtual int GetOutputSize(int len)
		{
			int num = len + this.mainBlockPos;
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

		// Token: 0x06002126 RID: 8486 RVA: 0x000C0C0C File Offset: 0x000C0C0C
		public virtual int GetUpdateOutputSize(int len)
		{
			int num = len + this.mainBlockPos;
			if (!this.forEncryption)
			{
				if (num < this.macSize)
				{
					return 0;
				}
				num -= this.macSize;
			}
			return num - num % 16;
		}

		// Token: 0x06002127 RID: 8487 RVA: 0x000C0C50 File Offset: 0x000C0C50
		public virtual void ProcessAadByte(byte input)
		{
			this.hashBlock[this.hashBlockPos] = input;
			if (++this.hashBlockPos == this.hashBlock.Length)
			{
				this.ProcessHashBlock();
			}
		}

		// Token: 0x06002128 RID: 8488 RVA: 0x000C0C94 File Offset: 0x000C0C94
		public virtual void ProcessAadBytes(byte[] input, int off, int len)
		{
			for (int i = 0; i < len; i++)
			{
				this.hashBlock[this.hashBlockPos] = input[off + i];
				if (++this.hashBlockPos == this.hashBlock.Length)
				{
					this.ProcessHashBlock();
				}
			}
		}

		// Token: 0x06002129 RID: 8489 RVA: 0x000C0CEC File Offset: 0x000C0CEC
		public virtual int ProcessByte(byte input, byte[] output, int outOff)
		{
			this.mainBlock[this.mainBlockPos] = input;
			if (++this.mainBlockPos == this.mainBlock.Length)
			{
				this.ProcessMainBlock(output, outOff);
				return 16;
			}
			return 0;
		}

		// Token: 0x0600212A RID: 8490 RVA: 0x000C0D38 File Offset: 0x000C0D38
		public virtual int ProcessBytes(byte[] input, int inOff, int len, byte[] output, int outOff)
		{
			int num = 0;
			for (int i = 0; i < len; i++)
			{
				this.mainBlock[this.mainBlockPos] = input[inOff + i];
				if (++this.mainBlockPos == this.mainBlock.Length)
				{
					this.ProcessMainBlock(output, outOff + num);
					num += 16;
				}
			}
			return num;
		}

		// Token: 0x0600212B RID: 8491 RVA: 0x000C0D9C File Offset: 0x000C0D9C
		public virtual int DoFinal(byte[] output, int outOff)
		{
			byte[] array = null;
			if (!this.forEncryption)
			{
				if (this.mainBlockPos < this.macSize)
				{
					throw new InvalidCipherTextException("data too short");
				}
				this.mainBlockPos -= this.macSize;
				array = new byte[this.macSize];
				Array.Copy(this.mainBlock, this.mainBlockPos, array, 0, this.macSize);
			}
			if (this.hashBlockPos > 0)
			{
				OcbBlockCipher.OCB_extend(this.hashBlock, this.hashBlockPos);
				this.UpdateHASH(this.L_Asterisk);
			}
			if (this.mainBlockPos > 0)
			{
				if (this.forEncryption)
				{
					OcbBlockCipher.OCB_extend(this.mainBlock, this.mainBlockPos);
					OcbBlockCipher.Xor(this.Checksum, this.mainBlock);
				}
				OcbBlockCipher.Xor(this.OffsetMAIN, this.L_Asterisk);
				byte[] array2 = new byte[16];
				this.hashCipher.ProcessBlock(this.OffsetMAIN, 0, array2, 0);
				OcbBlockCipher.Xor(this.mainBlock, array2);
				Check.OutputLength(output, outOff, this.mainBlockPos, "Output buffer too short");
				Array.Copy(this.mainBlock, 0, output, outOff, this.mainBlockPos);
				if (!this.forEncryption)
				{
					OcbBlockCipher.OCB_extend(this.mainBlock, this.mainBlockPos);
					OcbBlockCipher.Xor(this.Checksum, this.mainBlock);
				}
			}
			OcbBlockCipher.Xor(this.Checksum, this.OffsetMAIN);
			OcbBlockCipher.Xor(this.Checksum, this.L_Dollar);
			this.hashCipher.ProcessBlock(this.Checksum, 0, this.Checksum, 0);
			OcbBlockCipher.Xor(this.Checksum, this.Sum);
			this.macBlock = new byte[this.macSize];
			Array.Copy(this.Checksum, 0, this.macBlock, 0, this.macSize);
			int num = this.mainBlockPos;
			if (this.forEncryption)
			{
				Check.OutputLength(output, outOff, num + this.macSize, "Output buffer too short");
				Array.Copy(this.macBlock, 0, output, outOff + num, this.macSize);
				num += this.macSize;
			}
			else if (!Arrays.ConstantTimeAreEqual(this.macBlock, array))
			{
				throw new InvalidCipherTextException("mac check in OCB failed");
			}
			this.Reset(false);
			return num;
		}

		// Token: 0x0600212C RID: 8492 RVA: 0x000C0FE4 File Offset: 0x000C0FE4
		public virtual void Reset()
		{
			this.Reset(true);
		}

		// Token: 0x0600212D RID: 8493 RVA: 0x000C0FF0 File Offset: 0x000C0FF0
		protected virtual void Clear(byte[] bs)
		{
			if (bs != null)
			{
				Array.Clear(bs, 0, bs.Length);
			}
		}

		// Token: 0x0600212E RID: 8494 RVA: 0x000C1004 File Offset: 0x000C1004
		protected virtual byte[] GetLSub(int n)
		{
			while (n >= this.L.Count)
			{
				this.L.Add(OcbBlockCipher.OCB_double((byte[])this.L[this.L.Count - 1]));
			}
			return (byte[])this.L[n];
		}

		// Token: 0x0600212F RID: 8495 RVA: 0x000C1068 File Offset: 0x000C1068
		protected virtual void ProcessHashBlock()
		{
			this.UpdateHASH(this.GetLSub(OcbBlockCipher.OCB_ntz(this.hashBlockCount += 1L)));
			this.hashBlockPos = 0;
		}

		// Token: 0x06002130 RID: 8496 RVA: 0x000C10A4 File Offset: 0x000C10A4
		protected virtual void ProcessMainBlock(byte[] output, int outOff)
		{
			Check.DataLength(output, outOff, 16, "Output buffer too short");
			if (this.forEncryption)
			{
				OcbBlockCipher.Xor(this.Checksum, this.mainBlock);
				this.mainBlockPos = 0;
			}
			OcbBlockCipher.Xor(this.OffsetMAIN, this.GetLSub(OcbBlockCipher.OCB_ntz(this.mainBlockCount += 1L)));
			OcbBlockCipher.Xor(this.mainBlock, this.OffsetMAIN);
			this.mainCipher.ProcessBlock(this.mainBlock, 0, this.mainBlock, 0);
			OcbBlockCipher.Xor(this.mainBlock, this.OffsetMAIN);
			Array.Copy(this.mainBlock, 0, output, outOff, 16);
			if (!this.forEncryption)
			{
				OcbBlockCipher.Xor(this.Checksum, this.mainBlock);
				Array.Copy(this.mainBlock, 16, this.mainBlock, 0, this.macSize);
				this.mainBlockPos = this.macSize;
			}
		}

		// Token: 0x06002131 RID: 8497 RVA: 0x000C119C File Offset: 0x000C119C
		protected virtual void Reset(bool clearMac)
		{
			this.hashCipher.Reset();
			this.mainCipher.Reset();
			this.Clear(this.hashBlock);
			this.Clear(this.mainBlock);
			this.hashBlockPos = 0;
			this.mainBlockPos = 0;
			this.hashBlockCount = 0L;
			this.mainBlockCount = 0L;
			this.Clear(this.OffsetHASH);
			this.Clear(this.Sum);
			Array.Copy(this.OffsetMAIN_0, 0, this.OffsetMAIN, 0, 16);
			this.Clear(this.Checksum);
			if (clearMac)
			{
				this.macBlock = null;
			}
			if (this.initialAssociatedText != null)
			{
				this.ProcessAadBytes(this.initialAssociatedText, 0, this.initialAssociatedText.Length);
			}
		}

		// Token: 0x06002132 RID: 8498 RVA: 0x000C1260 File Offset: 0x000C1260
		protected virtual void UpdateHASH(byte[] LSub)
		{
			OcbBlockCipher.Xor(this.OffsetHASH, LSub);
			OcbBlockCipher.Xor(this.hashBlock, this.OffsetHASH);
			this.hashCipher.ProcessBlock(this.hashBlock, 0, this.hashBlock, 0);
			OcbBlockCipher.Xor(this.Sum, this.hashBlock);
		}

		// Token: 0x06002133 RID: 8499 RVA: 0x000C12BC File Offset: 0x000C12BC
		protected static byte[] OCB_double(byte[] block)
		{
			byte[] array = new byte[16];
			int num = OcbBlockCipher.ShiftLeft(block, array);
			byte[] array2;
			(array2 = array)[15] = (array2[15] ^ (byte)(135 >> (1 - num << 3)));
			return array;
		}

		// Token: 0x06002134 RID: 8500 RVA: 0x000C12FC File Offset: 0x000C12FC
		protected static void OCB_extend(byte[] block, int pos)
		{
			block[pos] = 128;
			while (++pos < 16)
			{
				block[pos] = 0;
			}
		}

		// Token: 0x06002135 RID: 8501 RVA: 0x000C131C File Offset: 0x000C131C
		protected static int OCB_ntz(long x)
		{
			if (x == 0L)
			{
				return 64;
			}
			int num = 0;
			ulong num2 = (ulong)x;
			while ((num2 & 1UL) == 0UL)
			{
				num++;
				num2 >>= 1;
			}
			return num;
		}

		// Token: 0x06002136 RID: 8502 RVA: 0x000C1354 File Offset: 0x000C1354
		protected static int ShiftLeft(byte[] block, byte[] output)
		{
			int num = 16;
			uint num2 = 0U;
			while (--num >= 0)
			{
				uint num3 = (uint)block[num];
				output[num] = (byte)(num3 << 1 | num2);
				num2 = (num3 >> 7 & 1U);
			}
			return (int)num2;
		}

		// Token: 0x06002137 RID: 8503 RVA: 0x000C138C File Offset: 0x000C138C
		protected static void Xor(byte[] block, byte[] val)
		{
			for (int i = 15; i >= 0; i--)
			{
				IntPtr intPtr;
				block[(int)(intPtr = (IntPtr)i)] = (block[(int)intPtr] ^ val[i]);
			}
		}

		// Token: 0x04001588 RID: 5512
		private const int BLOCK_SIZE = 16;

		// Token: 0x04001589 RID: 5513
		private readonly IBlockCipher hashCipher;

		// Token: 0x0400158A RID: 5514
		private readonly IBlockCipher mainCipher;

		// Token: 0x0400158B RID: 5515
		private bool forEncryption;

		// Token: 0x0400158C RID: 5516
		private int macSize;

		// Token: 0x0400158D RID: 5517
		private byte[] initialAssociatedText;

		// Token: 0x0400158E RID: 5518
		private IList L;

		// Token: 0x0400158F RID: 5519
		private byte[] L_Asterisk;

		// Token: 0x04001590 RID: 5520
		private byte[] L_Dollar;

		// Token: 0x04001591 RID: 5521
		private byte[] KtopInput = null;

		// Token: 0x04001592 RID: 5522
		private byte[] Stretch = new byte[24];

		// Token: 0x04001593 RID: 5523
		private byte[] OffsetMAIN_0 = new byte[16];

		// Token: 0x04001594 RID: 5524
		private byte[] hashBlock;

		// Token: 0x04001595 RID: 5525
		private byte[] mainBlock;

		// Token: 0x04001596 RID: 5526
		private int hashBlockPos;

		// Token: 0x04001597 RID: 5527
		private int mainBlockPos;

		// Token: 0x04001598 RID: 5528
		private long hashBlockCount;

		// Token: 0x04001599 RID: 5529
		private long mainBlockCount;

		// Token: 0x0400159A RID: 5530
		private byte[] OffsetHASH;

		// Token: 0x0400159B RID: 5531
		private byte[] Sum;

		// Token: 0x0400159C RID: 5532
		private byte[] OffsetMAIN = new byte[16];

		// Token: 0x0400159D RID: 5533
		private byte[] Checksum;

		// Token: 0x0400159E RID: 5534
		private byte[] macBlock;
	}
}
