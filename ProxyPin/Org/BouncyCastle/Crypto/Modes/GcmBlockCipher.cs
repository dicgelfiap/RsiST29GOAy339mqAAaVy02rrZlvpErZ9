using System;
using Org.BouncyCastle.Crypto.Modes.Gcm;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Utilities;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Modes
{
	// Token: 0x02000403 RID: 1027
	public class GcmBlockCipher : IAeadBlockCipher, IAeadCipher
	{
		// Token: 0x060020D7 RID: 8407 RVA: 0x000BE63C File Offset: 0x000BE63C
		public GcmBlockCipher(IBlockCipher c) : this(c, null)
		{
		}

		// Token: 0x060020D8 RID: 8408 RVA: 0x000BE648 File Offset: 0x000BE648
		public GcmBlockCipher(IBlockCipher c, IGcmMultiplier m)
		{
			if (c.GetBlockSize() != 16)
			{
				throw new ArgumentException("cipher required with a block size of " + 16 + ".");
			}
			if (m == null)
			{
				m = new Tables8kGcmMultiplier();
			}
			this.cipher = c;
			this.multiplier = m;
		}

		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x060020D9 RID: 8409 RVA: 0x000BE6A4 File Offset: 0x000BE6A4
		public virtual string AlgorithmName
		{
			get
			{
				return this.cipher.AlgorithmName + "/GCM";
			}
		}

		// Token: 0x060020DA RID: 8410 RVA: 0x000BE6BC File Offset: 0x000BE6BC
		public IBlockCipher GetUnderlyingCipher()
		{
			return this.cipher;
		}

		// Token: 0x060020DB RID: 8411 RVA: 0x000BE6C4 File Offset: 0x000BE6C4
		public virtual int GetBlockSize()
		{
			return 16;
		}

		// Token: 0x060020DC RID: 8412 RVA: 0x000BE6C8 File Offset: 0x000BE6C8
		public virtual void Init(bool forEncryption, ICipherParameters parameters)
		{
			this.forEncryption = forEncryption;
			this.macBlock = null;
			this.initialised = true;
			byte[] iv;
			KeyParameter keyParameter;
			if (parameters is AeadParameters)
			{
				AeadParameters aeadParameters = (AeadParameters)parameters;
				iv = aeadParameters.GetNonce();
				this.initialAssociatedText = aeadParameters.GetAssociatedText();
				int num = aeadParameters.MacSize;
				if (num < 32 || num > 128 || num % 8 != 0)
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
					throw new ArgumentException("invalid parameters passed to GCM");
				}
				ParametersWithIV parametersWithIV = (ParametersWithIV)parameters;
				iv = parametersWithIV.GetIV();
				this.initialAssociatedText = null;
				this.macSize = 16;
				keyParameter = (KeyParameter)parametersWithIV.Parameters;
			}
			int num2 = forEncryption ? 16 : (16 + this.macSize);
			this.bufBlock = new byte[num2];
			if (iv == null || iv.Length < 1)
			{
				throw new ArgumentException("IV must be at least 1 byte");
			}
			if (forEncryption && this.nonce != null && Arrays.AreEqual(this.nonce, iv))
			{
				if (keyParameter == null)
				{
					throw new ArgumentException("cannot reuse nonce for GCM encryption");
				}
				if (this.lastKey != null && Arrays.AreEqual(this.lastKey, keyParameter.GetKey()))
				{
					throw new ArgumentException("cannot reuse nonce for GCM encryption");
				}
			}
			this.nonce = iv;
			if (keyParameter != null)
			{
				this.lastKey = keyParameter.GetKey();
			}
			if (keyParameter != null)
			{
				this.cipher.Init(true, keyParameter);
				this.H = new byte[16];
				this.cipher.ProcessBlock(this.H, 0, this.H, 0);
				this.multiplier.Init(this.H);
				this.exp = null;
			}
			else if (this.H == null)
			{
				throw new ArgumentException("Key must be specified in initial init");
			}
			this.J0 = new byte[16];
			if (this.nonce.Length == 12)
			{
				Array.Copy(this.nonce, 0, this.J0, 0, this.nonce.Length);
				this.J0[15] = 1;
			}
			else
			{
				this.gHASH(this.J0, this.nonce, this.nonce.Length);
				byte[] array = new byte[16];
				Pack.UInt64_To_BE((ulong)((long)this.nonce.Length * 8L), array, 8);
				this.gHASHBlock(this.J0, array);
			}
			this.S = new byte[16];
			this.S_at = new byte[16];
			this.S_atPre = new byte[16];
			this.atBlock = new byte[16];
			this.atBlockPos = 0;
			this.atLength = 0UL;
			this.atLengthPre = 0UL;
			this.counter = Arrays.Clone(this.J0);
			this.blocksRemaining = 4294967294U;
			this.bufOff = 0;
			this.totalLength = 0UL;
			if (this.initialAssociatedText != null)
			{
				this.ProcessAadBytes(this.initialAssociatedText, 0, this.initialAssociatedText.Length);
			}
		}

		// Token: 0x060020DD RID: 8413 RVA: 0x000BE9EC File Offset: 0x000BE9EC
		public virtual byte[] GetMac()
		{
			if (this.macBlock != null)
			{
				return Arrays.Clone(this.macBlock);
			}
			return new byte[this.macSize];
		}

		// Token: 0x060020DE RID: 8414 RVA: 0x000BEA10 File Offset: 0x000BEA10
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

		// Token: 0x060020DF RID: 8415 RVA: 0x000BEA54 File Offset: 0x000BEA54
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
			return num - num % 16;
		}

		// Token: 0x060020E0 RID: 8416 RVA: 0x000BEA98 File Offset: 0x000BEA98
		public virtual void ProcessAadByte(byte input)
		{
			this.CheckStatus();
			this.atBlock[this.atBlockPos] = input;
			if (++this.atBlockPos == 16)
			{
				this.gHASHBlock(this.S_at, this.atBlock);
				this.atBlockPos = 0;
				this.atLength += 16UL;
			}
		}

		// Token: 0x060020E1 RID: 8417 RVA: 0x000BEB00 File Offset: 0x000BEB00
		public virtual void ProcessAadBytes(byte[] inBytes, int inOff, int len)
		{
			this.CheckStatus();
			for (int i = 0; i < len; i++)
			{
				this.atBlock[this.atBlockPos] = inBytes[inOff + i];
				if (++this.atBlockPos == 16)
				{
					this.gHASHBlock(this.S_at, this.atBlock);
					this.atBlockPos = 0;
					this.atLength += 16UL;
				}
			}
		}

		// Token: 0x060020E2 RID: 8418 RVA: 0x000BEB7C File Offset: 0x000BEB7C
		private void InitCipher()
		{
			if (this.atLength > 0UL)
			{
				Array.Copy(this.S_at, 0, this.S_atPre, 0, 16);
				this.atLengthPre = this.atLength;
			}
			if (this.atBlockPos > 0)
			{
				this.gHASHPartial(this.S_atPre, this.atBlock, 0, this.atBlockPos);
				this.atLengthPre += (ulong)this.atBlockPos;
			}
			if (this.atLengthPre > 0UL)
			{
				Array.Copy(this.S_atPre, 0, this.S, 0, 16);
			}
		}

		// Token: 0x060020E3 RID: 8419 RVA: 0x000BEC18 File Offset: 0x000BEC18
		public virtual int ProcessByte(byte input, byte[] output, int outOff)
		{
			this.CheckStatus();
			this.bufBlock[this.bufOff] = input;
			if (++this.bufOff == this.bufBlock.Length)
			{
				this.ProcessBlock(this.bufBlock, 0, output, outOff);
				if (this.forEncryption)
				{
					this.bufOff = 0;
				}
				else
				{
					Array.Copy(this.bufBlock, 16, this.bufBlock, 0, this.macSize);
					this.bufOff = this.macSize;
				}
				return 16;
			}
			return 0;
		}

		// Token: 0x060020E4 RID: 8420 RVA: 0x000BECAC File Offset: 0x000BECAC
		public virtual int ProcessBytes(byte[] input, int inOff, int len, byte[] output, int outOff)
		{
			this.CheckStatus();
			Check.DataLength(input, inOff, len, "input buffer too short");
			int num = 0;
			if (this.forEncryption)
			{
				if (this.bufOff != 0)
				{
					while (len > 0)
					{
						len--;
						this.bufBlock[this.bufOff] = input[inOff++];
						if (++this.bufOff == 16)
						{
							this.ProcessBlock(this.bufBlock, 0, output, outOff);
							this.bufOff = 0;
							num += 16;
							break;
						}
					}
				}
				while (len >= 16)
				{
					this.ProcessBlock(input, inOff, output, outOff + num);
					inOff += 16;
					len -= 16;
					num += 16;
				}
				if (len > 0)
				{
					Array.Copy(input, inOff, this.bufBlock, 0, len);
					this.bufOff = len;
				}
			}
			else
			{
				for (int i = 0; i < len; i++)
				{
					this.bufBlock[this.bufOff] = input[inOff + i];
					if (++this.bufOff == this.bufBlock.Length)
					{
						this.ProcessBlock(this.bufBlock, 0, output, outOff + num);
						Array.Copy(this.bufBlock, 16, this.bufBlock, 0, this.macSize);
						this.bufOff = this.macSize;
						num += 16;
					}
				}
			}
			return num;
		}

		// Token: 0x060020E5 RID: 8421 RVA: 0x000BEE10 File Offset: 0x000BEE10
		public int DoFinal(byte[] output, int outOff)
		{
			this.CheckStatus();
			if (this.totalLength == 0UL)
			{
				this.InitCipher();
			}
			int num = this.bufOff;
			if (this.forEncryption)
			{
				Check.OutputLength(output, outOff, num + this.macSize, "Output buffer too short");
			}
			else
			{
				if (num < this.macSize)
				{
					throw new InvalidCipherTextException("data too short");
				}
				num -= this.macSize;
				Check.OutputLength(output, outOff, num, "Output buffer too short");
			}
			if (num > 0)
			{
				this.ProcessPartial(this.bufBlock, 0, num, output, outOff);
			}
			this.atLength += (ulong)this.atBlockPos;
			if (this.atLength > this.atLengthPre)
			{
				if (this.atBlockPos > 0)
				{
					this.gHASHPartial(this.S_at, this.atBlock, 0, this.atBlockPos);
				}
				if (this.atLengthPre > 0UL)
				{
					GcmUtilities.Xor(this.S_at, this.S_atPre);
				}
				long pow = (long)(this.totalLength * 8UL + 127UL >> 7);
				byte[] array = new byte[16];
				if (this.exp == null)
				{
					this.exp = new Tables1kGcmExponentiator();
					this.exp.Init(this.H);
				}
				this.exp.ExponentiateX(pow, array);
				GcmUtilities.Multiply(this.S_at, array);
				GcmUtilities.Xor(this.S, this.S_at);
			}
			byte[] array2 = new byte[16];
			Pack.UInt64_To_BE(this.atLength * 8UL, array2, 0);
			Pack.UInt64_To_BE(this.totalLength * 8UL, array2, 8);
			this.gHASHBlock(this.S, array2);
			byte[] array3 = new byte[16];
			this.cipher.ProcessBlock(this.J0, 0, array3, 0);
			GcmUtilities.Xor(array3, this.S);
			int num2 = num;
			this.macBlock = new byte[this.macSize];
			Array.Copy(array3, 0, this.macBlock, 0, this.macSize);
			if (this.forEncryption)
			{
				Array.Copy(this.macBlock, 0, output, outOff + this.bufOff, this.macSize);
				num2 += this.macSize;
			}
			else
			{
				byte[] array4 = new byte[this.macSize];
				Array.Copy(this.bufBlock, num, array4, 0, this.macSize);
				if (!Arrays.ConstantTimeAreEqual(this.macBlock, array4))
				{
					throw new InvalidCipherTextException("mac check in GCM failed");
				}
			}
			this.Reset(false);
			return num2;
		}

		// Token: 0x060020E6 RID: 8422 RVA: 0x000BF084 File Offset: 0x000BF084
		public virtual void Reset()
		{
			this.Reset(true);
		}

		// Token: 0x060020E7 RID: 8423 RVA: 0x000BF090 File Offset: 0x000BF090
		private void Reset(bool clearMac)
		{
			this.cipher.Reset();
			this.S = new byte[16];
			this.S_at = new byte[16];
			this.S_atPre = new byte[16];
			this.atBlock = new byte[16];
			this.atBlockPos = 0;
			this.atLength = 0UL;
			this.atLengthPre = 0UL;
			this.counter = Arrays.Clone(this.J0);
			this.blocksRemaining = 4294967294U;
			this.bufOff = 0;
			this.totalLength = 0UL;
			if (this.bufBlock != null)
			{
				Arrays.Fill(this.bufBlock, 0);
			}
			if (clearMac)
			{
				this.macBlock = null;
			}
			if (this.forEncryption)
			{
				this.initialised = false;
				return;
			}
			if (this.initialAssociatedText != null)
			{
				this.ProcessAadBytes(this.initialAssociatedText, 0, this.initialAssociatedText.Length);
			}
		}

		// Token: 0x060020E8 RID: 8424 RVA: 0x000BF178 File Offset: 0x000BF178
		private void ProcessBlock(byte[] buf, int bufOff, byte[] output, int outOff)
		{
			Check.OutputLength(output, outOff, 16, "Output buffer too short");
			if (this.totalLength == 0UL)
			{
				this.InitCipher();
			}
			byte[] array = new byte[16];
			this.GetNextCtrBlock(array);
			if (this.forEncryption)
			{
				GcmUtilities.Xor(array, buf, bufOff);
				this.gHASHBlock(this.S, array);
				Array.Copy(array, 0, output, outOff, 16);
			}
			else
			{
				this.gHASHBlock(this.S, buf, bufOff);
				GcmUtilities.Xor(array, 0, buf, bufOff, output, outOff);
			}
			this.totalLength += 16UL;
		}

		// Token: 0x060020E9 RID: 8425 RVA: 0x000BF218 File Offset: 0x000BF218
		private void ProcessPartial(byte[] buf, int off, int len, byte[] output, int outOff)
		{
			byte[] array = new byte[16];
			this.GetNextCtrBlock(array);
			if (this.forEncryption)
			{
				GcmUtilities.Xor(buf, off, array, 0, len);
				this.gHASHPartial(this.S, buf, off, len);
			}
			else
			{
				this.gHASHPartial(this.S, buf, off, len);
				GcmUtilities.Xor(buf, off, array, 0, len);
			}
			Array.Copy(buf, off, output, outOff, len);
			this.totalLength += (ulong)len;
		}

		// Token: 0x060020EA RID: 8426 RVA: 0x000BF298 File Offset: 0x000BF298
		private void gHASH(byte[] Y, byte[] b, int len)
		{
			for (int i = 0; i < len; i += 16)
			{
				int len2 = Math.Min(len - i, 16);
				this.gHASHPartial(Y, b, i, len2);
			}
		}

		// Token: 0x060020EB RID: 8427 RVA: 0x000BF2D0 File Offset: 0x000BF2D0
		private void gHASHBlock(byte[] Y, byte[] b)
		{
			GcmUtilities.Xor(Y, b);
			this.multiplier.MultiplyH(Y);
		}

		// Token: 0x060020EC RID: 8428 RVA: 0x000BF2E8 File Offset: 0x000BF2E8
		private void gHASHBlock(byte[] Y, byte[] b, int off)
		{
			GcmUtilities.Xor(Y, b, off);
			this.multiplier.MultiplyH(Y);
		}

		// Token: 0x060020ED RID: 8429 RVA: 0x000BF300 File Offset: 0x000BF300
		private void gHASHPartial(byte[] Y, byte[] b, int off, int len)
		{
			GcmUtilities.Xor(Y, b, off, len);
			this.multiplier.MultiplyH(Y);
		}

		// Token: 0x060020EE RID: 8430 RVA: 0x000BF318 File Offset: 0x000BF318
		private void GetNextCtrBlock(byte[] block)
		{
			if (this.blocksRemaining == 0U)
			{
				throw new InvalidOperationException("Attempt to process too many blocks");
			}
			this.blocksRemaining -= 1U;
			uint num = 1U;
			num += (uint)this.counter[15];
			this.counter[15] = (byte)num;
			num >>= 8;
			num += (uint)this.counter[14];
			this.counter[14] = (byte)num;
			num >>= 8;
			num += (uint)this.counter[13];
			this.counter[13] = (byte)num;
			num >>= 8;
			num += (uint)this.counter[12];
			this.counter[12] = (byte)num;
			this.cipher.ProcessBlock(this.counter, 0, block, 0);
		}

		// Token: 0x060020EF RID: 8431 RVA: 0x000BF3CC File Offset: 0x000BF3CC
		private void CheckStatus()
		{
			if (this.initialised)
			{
				return;
			}
			if (this.forEncryption)
			{
				throw new InvalidOperationException("GCM cipher cannot be reused for encryption");
			}
			throw new InvalidOperationException("GCM cipher needs to be initialised");
		}

		// Token: 0x0400154C RID: 5452
		private const int BlockSize = 16;

		// Token: 0x0400154D RID: 5453
		private readonly IBlockCipher cipher;

		// Token: 0x0400154E RID: 5454
		private readonly IGcmMultiplier multiplier;

		// Token: 0x0400154F RID: 5455
		private IGcmExponentiator exp;

		// Token: 0x04001550 RID: 5456
		private bool forEncryption;

		// Token: 0x04001551 RID: 5457
		private bool initialised;

		// Token: 0x04001552 RID: 5458
		private int macSize;

		// Token: 0x04001553 RID: 5459
		private byte[] lastKey;

		// Token: 0x04001554 RID: 5460
		private byte[] nonce;

		// Token: 0x04001555 RID: 5461
		private byte[] initialAssociatedText;

		// Token: 0x04001556 RID: 5462
		private byte[] H;

		// Token: 0x04001557 RID: 5463
		private byte[] J0;

		// Token: 0x04001558 RID: 5464
		private byte[] bufBlock;

		// Token: 0x04001559 RID: 5465
		private byte[] macBlock;

		// Token: 0x0400155A RID: 5466
		private byte[] S;

		// Token: 0x0400155B RID: 5467
		private byte[] S_at;

		// Token: 0x0400155C RID: 5468
		private byte[] S_atPre;

		// Token: 0x0400155D RID: 5469
		private byte[] counter;

		// Token: 0x0400155E RID: 5470
		private uint blocksRemaining;

		// Token: 0x0400155F RID: 5471
		private int bufOff;

		// Token: 0x04001560 RID: 5472
		private ulong totalLength;

		// Token: 0x04001561 RID: 5473
		private byte[] atBlock;

		// Token: 0x04001562 RID: 5474
		private int atBlockPos;

		// Token: 0x04001563 RID: 5475
		private ulong atLength;

		// Token: 0x04001564 RID: 5476
		private ulong atLengthPre;
	}
}
