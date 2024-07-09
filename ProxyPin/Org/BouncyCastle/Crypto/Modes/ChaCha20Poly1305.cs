using System;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Macs;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Utilities;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Modes
{
	// Token: 0x020003FD RID: 1021
	public class ChaCha20Poly1305 : IAeadCipher
	{
		// Token: 0x06002072 RID: 8306 RVA: 0x000BC8AC File Offset: 0x000BC8AC
		public ChaCha20Poly1305() : this(new Poly1305())
		{
		}

		// Token: 0x06002073 RID: 8307 RVA: 0x000BC8BC File Offset: 0x000BC8BC
		public ChaCha20Poly1305(IMac poly1305)
		{
			if (poly1305 == null)
			{
				throw new ArgumentNullException("poly1305");
			}
			if (16 != poly1305.GetMacSize())
			{
				throw new ArgumentException("must be a 128-bit MAC", "poly1305");
			}
			this.mChacha20 = new ChaCha7539Engine();
			this.mPoly1305 = poly1305;
		}

		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x06002074 RID: 8308 RVA: 0x000BC950 File Offset: 0x000BC950
		public virtual string AlgorithmName
		{
			get
			{
				return "ChaCha20Poly1305";
			}
		}

		// Token: 0x06002075 RID: 8309 RVA: 0x000BC958 File Offset: 0x000BC958
		public virtual void Init(bool forEncryption, ICipherParameters parameters)
		{
			KeyParameter keyParameter;
			byte[] array;
			ICipherParameters parameters2;
			if (parameters is AeadParameters)
			{
				AeadParameters aeadParameters = (AeadParameters)parameters;
				int macSize = aeadParameters.MacSize;
				if (128 != macSize)
				{
					throw new ArgumentException("Invalid value for MAC size: " + macSize);
				}
				keyParameter = aeadParameters.Key;
				array = aeadParameters.GetNonce();
				parameters2 = new ParametersWithIV(keyParameter, array);
				this.mInitialAad = aeadParameters.GetAssociatedText();
			}
			else
			{
				if (!(parameters is ParametersWithIV))
				{
					throw new ArgumentException("invalid parameters passed to ChaCha20Poly1305", "parameters");
				}
				ParametersWithIV parametersWithIV = (ParametersWithIV)parameters;
				keyParameter = (KeyParameter)parametersWithIV.Parameters;
				array = parametersWithIV.GetIV();
				parameters2 = parametersWithIV;
				this.mInitialAad = null;
			}
			if (keyParameter == null)
			{
				if (this.mState == ChaCha20Poly1305.State.Uninitialized)
				{
					throw new ArgumentException("Key must be specified in initial init");
				}
			}
			else if (32 != keyParameter.GetKey().Length)
			{
				throw new ArgumentException("Key must be 256 bits");
			}
			if (array == null || 12 != array.Length)
			{
				throw new ArgumentException("Nonce must be 96 bits");
			}
			if (this.mState != ChaCha20Poly1305.State.Uninitialized && forEncryption && Arrays.AreEqual(this.mNonce, array) && (keyParameter == null || Arrays.AreEqual(this.mKey, keyParameter.GetKey())))
			{
				throw new ArgumentException("cannot reuse nonce for ChaCha20Poly1305 encryption");
			}
			if (keyParameter != null)
			{
				Array.Copy(keyParameter.GetKey(), 0, this.mKey, 0, 32);
			}
			Array.Copy(array, 0, this.mNonce, 0, 12);
			this.mChacha20.Init(true, parameters2);
			this.mState = (forEncryption ? ChaCha20Poly1305.State.EncInit : ChaCha20Poly1305.State.DecInit);
			this.Reset(true, false);
		}

		// Token: 0x06002076 RID: 8310 RVA: 0x000BCB04 File Offset: 0x000BCB04
		public virtual int GetOutputSize(int len)
		{
			int num = Math.Max(0, len) + this.mBufPos;
			switch (this.mState)
			{
			case ChaCha20Poly1305.State.EncInit:
			case ChaCha20Poly1305.State.EncAad:
			case ChaCha20Poly1305.State.EncData:
				return num + 16;
			case ChaCha20Poly1305.State.DecInit:
			case ChaCha20Poly1305.State.DecAad:
			case ChaCha20Poly1305.State.DecData:
				return Math.Max(0, num - 16);
			}
			throw new InvalidOperationException();
		}

		// Token: 0x06002077 RID: 8311 RVA: 0x000BCB6C File Offset: 0x000BCB6C
		public virtual int GetUpdateOutputSize(int len)
		{
			int num = Math.Max(0, len) + this.mBufPos;
			switch (this.mState)
			{
			case ChaCha20Poly1305.State.EncInit:
			case ChaCha20Poly1305.State.EncAad:
			case ChaCha20Poly1305.State.EncData:
				goto IL_55;
			case ChaCha20Poly1305.State.DecInit:
			case ChaCha20Poly1305.State.DecAad:
			case ChaCha20Poly1305.State.DecData:
				num = Math.Max(0, num - 16);
				goto IL_55;
			}
			throw new InvalidOperationException();
			IL_55:
			return num - num % 64;
		}

		// Token: 0x06002078 RID: 8312 RVA: 0x000BCBD8 File Offset: 0x000BCBD8
		public virtual void ProcessAadByte(byte input)
		{
			this.CheckAad();
			this.mAadCount = this.IncrementCount(this.mAadCount, 1U, ulong.MaxValue);
			this.mPoly1305.Update(input);
		}

		// Token: 0x06002079 RID: 8313 RVA: 0x000BCC10 File Offset: 0x000BCC10
		public virtual void ProcessAadBytes(byte[] inBytes, int inOff, int len)
		{
			if (inBytes == null)
			{
				throw new ArgumentNullException("inBytes");
			}
			if (inOff < 0)
			{
				throw new ArgumentException("cannot be negative", "inOff");
			}
			if (len < 0)
			{
				throw new ArgumentException("cannot be negative", "len");
			}
			Check.DataLength(inBytes, inOff, len, "input buffer too short");
			this.CheckAad();
			if (len > 0)
			{
				this.mAadCount = this.IncrementCount(this.mAadCount, (uint)len, ulong.MaxValue);
				this.mPoly1305.BlockUpdate(inBytes, inOff, len);
			}
		}

		// Token: 0x0600207A RID: 8314 RVA: 0x000BCCA0 File Offset: 0x000BCCA0
		public virtual int ProcessByte(byte input, byte[] outBytes, int outOff)
		{
			this.CheckData();
			ChaCha20Poly1305.State state = this.mState;
			if (state != ChaCha20Poly1305.State.EncData)
			{
				if (state != ChaCha20Poly1305.State.DecData)
				{
					throw new InvalidOperationException();
				}
				this.mBuf[this.mBufPos] = input;
				if (++this.mBufPos == this.mBuf.Length)
				{
					this.mPoly1305.BlockUpdate(this.mBuf, 0, 64);
					this.ProcessData(this.mBuf, 0, 64, outBytes, outOff);
					Array.Copy(this.mBuf, 64, this.mBuf, 0, 16);
					this.mBufPos = 16;
					return 64;
				}
				return 0;
			}
			else
			{
				this.mBuf[this.mBufPos] = input;
				if (++this.mBufPos == 64)
				{
					this.ProcessData(this.mBuf, 0, 64, outBytes, outOff);
					this.mPoly1305.BlockUpdate(outBytes, outOff, 64);
					this.mBufPos = 0;
					return 64;
				}
				return 0;
			}
		}

		// Token: 0x0600207B RID: 8315 RVA: 0x000BCD98 File Offset: 0x000BCD98
		public virtual int ProcessBytes(byte[] inBytes, int inOff, int len, byte[] outBytes, int outOff)
		{
			if (inBytes == null)
			{
				throw new ArgumentNullException("inBytes");
			}
			if (outBytes == null)
			{
				throw new ArgumentNullException("outBytes");
			}
			if (inOff < 0)
			{
				throw new ArgumentException("cannot be negative", "inOff");
			}
			if (len < 0)
			{
				throw new ArgumentException("cannot be negative", "len");
			}
			Check.DataLength(inBytes, inOff, len, "input buffer too short");
			if (outOff < 0)
			{
				throw new ArgumentException("cannot be negative", "outOff");
			}
			this.CheckData();
			int num = 0;
			ChaCha20Poly1305.State state = this.mState;
			if (state != ChaCha20Poly1305.State.EncData)
			{
				if (state != ChaCha20Poly1305.State.DecData)
				{
					throw new InvalidOperationException();
				}
				for (int i = 0; i < len; i++)
				{
					this.mBuf[this.mBufPos] = inBytes[inOff + i];
					if (++this.mBufPos == this.mBuf.Length)
					{
						this.mPoly1305.BlockUpdate(this.mBuf, 0, 64);
						this.ProcessData(this.mBuf, 0, 64, outBytes, outOff + num);
						Array.Copy(this.mBuf, 64, this.mBuf, 0, 16);
						this.mBufPos = 16;
						num += 64;
					}
				}
			}
			else
			{
				if (this.mBufPos != 0)
				{
					while (len > 0)
					{
						len--;
						this.mBuf[this.mBufPos] = inBytes[inOff++];
						if (++this.mBufPos == 64)
						{
							this.ProcessData(this.mBuf, 0, 64, outBytes, outOff);
							this.mPoly1305.BlockUpdate(outBytes, outOff, 64);
							this.mBufPos = 0;
							num = 64;
							break;
						}
					}
				}
				while (len >= 64)
				{
					this.ProcessData(inBytes, inOff, 64, outBytes, outOff + num);
					this.mPoly1305.BlockUpdate(outBytes, outOff + num, 64);
					inOff += 64;
					len -= 64;
					num += 64;
				}
				if (len > 0)
				{
					Array.Copy(inBytes, inOff, this.mBuf, 0, len);
					this.mBufPos = len;
				}
			}
			return num;
		}

		// Token: 0x0600207C RID: 8316 RVA: 0x000BCFB0 File Offset: 0x000BCFB0
		public virtual int DoFinal(byte[] outBytes, int outOff)
		{
			if (outBytes == null)
			{
				throw new ArgumentNullException("outBytes");
			}
			if (outOff < 0)
			{
				throw new ArgumentException("cannot be negative", "outOff");
			}
			this.CheckData();
			Array.Clear(this.mMac, 0, 16);
			ChaCha20Poly1305.State state = this.mState;
			int num;
			if (state != ChaCha20Poly1305.State.EncData)
			{
				if (state != ChaCha20Poly1305.State.DecData)
				{
					throw new InvalidOperationException();
				}
				if (this.mBufPos < 16)
				{
					throw new InvalidCipherTextException("data too short");
				}
				num = this.mBufPos - 16;
				Check.OutputLength(outBytes, outOff, num, "output buffer too short");
				if (num > 0)
				{
					this.mPoly1305.BlockUpdate(this.mBuf, 0, num);
					this.ProcessData(this.mBuf, 0, num, outBytes, outOff);
				}
				this.FinishData(ChaCha20Poly1305.State.DecFinal);
				if (!Arrays.ConstantTimeAreEqual(16, this.mMac, 0, this.mBuf, num))
				{
					throw new InvalidCipherTextException("mac check in ChaCha20Poly1305 failed");
				}
			}
			else
			{
				num = this.mBufPos + 16;
				Check.OutputLength(outBytes, outOff, num, "output buffer too short");
				if (this.mBufPos > 0)
				{
					this.ProcessData(this.mBuf, 0, this.mBufPos, outBytes, outOff);
					this.mPoly1305.BlockUpdate(outBytes, outOff, this.mBufPos);
				}
				this.FinishData(ChaCha20Poly1305.State.EncFinal);
				Array.Copy(this.mMac, 0, outBytes, outOff + this.mBufPos, 16);
			}
			this.Reset(false, true);
			return num;
		}

		// Token: 0x0600207D RID: 8317 RVA: 0x000BD118 File Offset: 0x000BD118
		public virtual byte[] GetMac()
		{
			return Arrays.Clone(this.mMac);
		}

		// Token: 0x0600207E RID: 8318 RVA: 0x000BD128 File Offset: 0x000BD128
		public virtual void Reset()
		{
			this.Reset(true, true);
		}

		// Token: 0x0600207F RID: 8319 RVA: 0x000BD134 File Offset: 0x000BD134
		private void CheckAad()
		{
			switch (this.mState)
			{
			case ChaCha20Poly1305.State.EncInit:
				this.mState = ChaCha20Poly1305.State.EncAad;
				return;
			case ChaCha20Poly1305.State.EncAad:
			case ChaCha20Poly1305.State.DecAad:
				return;
			case ChaCha20Poly1305.State.EncFinal:
				throw new InvalidOperationException("ChaCha20Poly1305 cannot be reused for encryption");
			case ChaCha20Poly1305.State.DecInit:
				this.mState = ChaCha20Poly1305.State.DecAad;
				return;
			}
			throw new InvalidOperationException();
		}

		// Token: 0x06002080 RID: 8320 RVA: 0x000BD194 File Offset: 0x000BD194
		private void CheckData()
		{
			switch (this.mState)
			{
			case ChaCha20Poly1305.State.EncInit:
			case ChaCha20Poly1305.State.EncAad:
				this.FinishAad(ChaCha20Poly1305.State.EncData);
				return;
			case ChaCha20Poly1305.State.EncData:
			case ChaCha20Poly1305.State.DecData:
				return;
			case ChaCha20Poly1305.State.EncFinal:
				throw new InvalidOperationException("ChaCha20Poly1305 cannot be reused for encryption");
			case ChaCha20Poly1305.State.DecInit:
			case ChaCha20Poly1305.State.DecAad:
				this.FinishAad(ChaCha20Poly1305.State.DecData);
				return;
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x06002081 RID: 8321 RVA: 0x000BD1F8 File Offset: 0x000BD1F8
		private void FinishAad(ChaCha20Poly1305.State nextState)
		{
			this.PadMac(this.mAadCount);
			this.mState = nextState;
		}

		// Token: 0x06002082 RID: 8322 RVA: 0x000BD210 File Offset: 0x000BD210
		private void FinishData(ChaCha20Poly1305.State nextState)
		{
			this.PadMac(this.mDataCount);
			byte[] array = new byte[16];
			Pack.UInt64_To_LE(this.mAadCount, array, 0);
			Pack.UInt64_To_LE(this.mDataCount, array, 8);
			this.mPoly1305.BlockUpdate(array, 0, 16);
			this.mPoly1305.DoFinal(this.mMac, 0);
			this.mState = nextState;
		}

		// Token: 0x06002083 RID: 8323 RVA: 0x000BD278 File Offset: 0x000BD278
		private ulong IncrementCount(ulong count, uint increment, ulong limit)
		{
			if (count > limit - (ulong)increment)
			{
				throw new InvalidOperationException("Limit exceeded");
			}
			return count + (ulong)increment;
		}

		// Token: 0x06002084 RID: 8324 RVA: 0x000BD294 File Offset: 0x000BD294
		private void InitMac()
		{
			byte[] array = new byte[64];
			try
			{
				this.mChacha20.ProcessBytes(array, 0, 64, array, 0);
				this.mPoly1305.Init(new KeyParameter(array, 0, 32));
			}
			finally
			{
				Array.Clear(array, 0, 64);
			}
		}

		// Token: 0x06002085 RID: 8325 RVA: 0x000BD2F0 File Offset: 0x000BD2F0
		private void PadMac(ulong count)
		{
			int num = (int)count % 16;
			if (num != 0)
			{
				this.mPoly1305.BlockUpdate(ChaCha20Poly1305.Zeroes, 0, 16 - num);
			}
		}

		// Token: 0x06002086 RID: 8326 RVA: 0x000BD324 File Offset: 0x000BD324
		private void ProcessData(byte[] inBytes, int inOff, int inLen, byte[] outBytes, int outOff)
		{
			Check.OutputLength(outBytes, outOff, inLen, "output buffer too short");
			this.mChacha20.ProcessBytes(inBytes, inOff, inLen, outBytes, outOff);
			this.mDataCount = this.IncrementCount(this.mDataCount, (uint)inLen, 274877906880UL);
		}

		// Token: 0x06002087 RID: 8327 RVA: 0x000BD364 File Offset: 0x000BD364
		private void Reset(bool clearMac, bool resetCipher)
		{
			Array.Clear(this.mBuf, 0, this.mBuf.Length);
			if (clearMac)
			{
				Array.Clear(this.mMac, 0, this.mMac.Length);
			}
			this.mAadCount = 0UL;
			this.mDataCount = 0UL;
			this.mBufPos = 0;
			switch (this.mState)
			{
			case ChaCha20Poly1305.State.EncInit:
			case ChaCha20Poly1305.State.DecInit:
				break;
			case ChaCha20Poly1305.State.EncAad:
			case ChaCha20Poly1305.State.EncData:
			case ChaCha20Poly1305.State.EncFinal:
				this.mState = ChaCha20Poly1305.State.EncFinal;
				return;
			case ChaCha20Poly1305.State.DecAad:
			case ChaCha20Poly1305.State.DecData:
			case ChaCha20Poly1305.State.DecFinal:
				this.mState = ChaCha20Poly1305.State.DecInit;
				break;
			default:
				throw new InvalidOperationException();
			}
			if (resetCipher)
			{
				this.mChacha20.Reset();
			}
			this.InitMac();
			if (this.mInitialAad != null)
			{
				this.ProcessAadBytes(this.mInitialAad, 0, this.mInitialAad.Length);
			}
		}

		// Token: 0x04001528 RID: 5416
		private const int BufSize = 64;

		// Token: 0x04001529 RID: 5417
		private const int KeySize = 32;

		// Token: 0x0400152A RID: 5418
		private const int NonceSize = 12;

		// Token: 0x0400152B RID: 5419
		private const int MacSize = 16;

		// Token: 0x0400152C RID: 5420
		private const ulong AadLimit = 18446744073709551615UL;

		// Token: 0x0400152D RID: 5421
		private const ulong DataLimit = 274877906880UL;

		// Token: 0x0400152E RID: 5422
		private static readonly byte[] Zeroes = new byte[15];

		// Token: 0x0400152F RID: 5423
		private readonly ChaCha7539Engine mChacha20;

		// Token: 0x04001530 RID: 5424
		private readonly IMac mPoly1305;

		// Token: 0x04001531 RID: 5425
		private readonly byte[] mKey = new byte[32];

		// Token: 0x04001532 RID: 5426
		private readonly byte[] mNonce = new byte[12];

		// Token: 0x04001533 RID: 5427
		private readonly byte[] mBuf = new byte[80];

		// Token: 0x04001534 RID: 5428
		private readonly byte[] mMac = new byte[16];

		// Token: 0x04001535 RID: 5429
		private byte[] mInitialAad;

		// Token: 0x04001536 RID: 5430
		private ulong mAadCount;

		// Token: 0x04001537 RID: 5431
		private ulong mDataCount;

		// Token: 0x04001538 RID: 5432
		private ChaCha20Poly1305.State mState = ChaCha20Poly1305.State.Uninitialized;

		// Token: 0x04001539 RID: 5433
		private int mBufPos;

		// Token: 0x02000E0C RID: 3596
		private enum State
		{
			// Token: 0x04004138 RID: 16696
			Uninitialized,
			// Token: 0x04004139 RID: 16697
			EncInit,
			// Token: 0x0400413A RID: 16698
			EncAad,
			// Token: 0x0400413B RID: 16699
			EncData,
			// Token: 0x0400413C RID: 16700
			EncFinal,
			// Token: 0x0400413D RID: 16701
			DecInit,
			// Token: 0x0400413E RID: 16702
			DecAad,
			// Token: 0x0400413F RID: 16703
			DecData,
			// Token: 0x04004140 RID: 16704
			DecFinal
		}
	}
}
