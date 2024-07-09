using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Utilities;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Engines
{
	// Token: 0x02000383 RID: 899
	public class Salsa20Engine : IStreamCipher
	{
		// Token: 0x06001C1E RID: 7198 RVA: 0x0009DCB4 File Offset: 0x0009DCB4
		internal void PackTauOrSigma(int keyLength, uint[] state, int stateOffset)
		{
			int num = (keyLength - 16) / 4;
			state[stateOffset] = Salsa20Engine.TAU_SIGMA[num];
			state[stateOffset + 1] = Salsa20Engine.TAU_SIGMA[num + 1];
			state[stateOffset + 2] = Salsa20Engine.TAU_SIGMA[num + 2];
			state[stateOffset + 3] = Salsa20Engine.TAU_SIGMA[num + 3];
		}

		// Token: 0x06001C1F RID: 7199 RVA: 0x0009DD00 File Offset: 0x0009DD00
		public Salsa20Engine() : this(Salsa20Engine.DEFAULT_ROUNDS)
		{
		}

		// Token: 0x06001C20 RID: 7200 RVA: 0x0009DD10 File Offset: 0x0009DD10
		public Salsa20Engine(int rounds)
		{
			if (rounds <= 0 || (rounds & 1) != 0)
			{
				throw new ArgumentException("'rounds' must be a positive, even number");
			}
			this.rounds = rounds;
		}

		// Token: 0x06001C21 RID: 7201 RVA: 0x0009DD80 File Offset: 0x0009DD80
		public virtual void Init(bool forEncryption, ICipherParameters parameters)
		{
			ParametersWithIV parametersWithIV = parameters as ParametersWithIV;
			if (parametersWithIV == null)
			{
				throw new ArgumentException(this.AlgorithmName + " Init requires an IV", "parameters");
			}
			byte[] iv = parametersWithIV.GetIV();
			if (iv == null || iv.Length != this.NonceSize)
			{
				throw new ArgumentException(string.Concat(new object[]
				{
					this.AlgorithmName,
					" requires exactly ",
					this.NonceSize,
					" bytes of IV"
				}));
			}
			ICipherParameters parameters2 = parametersWithIV.Parameters;
			if (parameters2 == null)
			{
				if (!this.initialised)
				{
					throw new InvalidOperationException(this.AlgorithmName + " KeyParameter can not be null for first initialisation");
				}
				this.SetKey(null, iv);
			}
			else
			{
				if (!(parameters2 is KeyParameter))
				{
					throw new ArgumentException(this.AlgorithmName + " Init parameters must contain a KeyParameter (or null for re-init)");
				}
				this.SetKey(((KeyParameter)parameters2).GetKey(), iv);
			}
			this.Reset();
			this.initialised = true;
		}

		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x06001C22 RID: 7202 RVA: 0x0009DE90 File Offset: 0x0009DE90
		protected virtual int NonceSize
		{
			get
			{
				return 8;
			}
		}

		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x06001C23 RID: 7203 RVA: 0x0009DE94 File Offset: 0x0009DE94
		public virtual string AlgorithmName
		{
			get
			{
				string text = "Salsa20";
				if (this.rounds != Salsa20Engine.DEFAULT_ROUNDS)
				{
					text = text + "/" + this.rounds;
				}
				return text;
			}
		}

		// Token: 0x06001C24 RID: 7204 RVA: 0x0009DED4 File Offset: 0x0009DED4
		public virtual byte ReturnByte(byte input)
		{
			if (this.LimitExceeded())
			{
				throw new MaxBytesExceededException("2^70 byte limit per IV; Change IV");
			}
			if (this.index == 0)
			{
				this.GenerateKeyStream(this.keyStream);
				this.AdvanceCounter();
			}
			byte result = this.keyStream[this.index] ^ input;
			this.index = (this.index + 1 & 63);
			return result;
		}

		// Token: 0x06001C25 RID: 7205 RVA: 0x0009DF3C File Offset: 0x0009DF3C
		protected virtual void AdvanceCounter()
		{
			uint[] array;
			if (((array = this.engineState)[8] = array[8] + 1U) == 0U)
			{
				(array = this.engineState)[9] = array[9] + 1U;
			}
		}

		// Token: 0x06001C26 RID: 7206 RVA: 0x0009DF78 File Offset: 0x0009DF78
		public virtual void ProcessBytes(byte[] inBytes, int inOff, int len, byte[] outBytes, int outOff)
		{
			if (!this.initialised)
			{
				throw new InvalidOperationException(this.AlgorithmName + " not initialised");
			}
			Check.DataLength(inBytes, inOff, len, "input buffer too short");
			Check.OutputLength(outBytes, outOff, len, "output buffer too short");
			if (this.LimitExceeded((uint)len))
			{
				throw new MaxBytesExceededException("2^70 byte limit per IV would be exceeded; Change IV");
			}
			for (int i = 0; i < len; i++)
			{
				if (this.index == 0)
				{
					this.GenerateKeyStream(this.keyStream);
					this.AdvanceCounter();
				}
				outBytes[i + outOff] = (this.keyStream[this.index] ^ inBytes[i + inOff]);
				this.index = (this.index + 1 & 63);
			}
		}

		// Token: 0x06001C27 RID: 7207 RVA: 0x0009E038 File Offset: 0x0009E038
		public virtual void Reset()
		{
			this.index = 0;
			this.ResetLimitCounter();
			this.ResetCounter();
		}

		// Token: 0x06001C28 RID: 7208 RVA: 0x0009E050 File Offset: 0x0009E050
		protected virtual void ResetCounter()
		{
			this.engineState[8] = (this.engineState[9] = 0U);
		}

		// Token: 0x06001C29 RID: 7209 RVA: 0x0009E078 File Offset: 0x0009E078
		protected virtual void SetKey(byte[] keyBytes, byte[] ivBytes)
		{
			if (keyBytes != null)
			{
				if (keyBytes.Length != 16 && keyBytes.Length != 32)
				{
					throw new ArgumentException(this.AlgorithmName + " requires 128 bit or 256 bit key");
				}
				int num = (keyBytes.Length - 16) / 4;
				this.engineState[0] = Salsa20Engine.TAU_SIGMA[num];
				this.engineState[5] = Salsa20Engine.TAU_SIGMA[num + 1];
				this.engineState[10] = Salsa20Engine.TAU_SIGMA[num + 2];
				this.engineState[15] = Salsa20Engine.TAU_SIGMA[num + 3];
				Pack.LE_To_UInt32(keyBytes, 0, this.engineState, 1, 4);
				Pack.LE_To_UInt32(keyBytes, keyBytes.Length - 16, this.engineState, 11, 4);
			}
			Pack.LE_To_UInt32(ivBytes, 0, this.engineState, 6, 2);
		}

		// Token: 0x06001C2A RID: 7210 RVA: 0x0009E13C File Offset: 0x0009E13C
		protected virtual void GenerateKeyStream(byte[] output)
		{
			Salsa20Engine.SalsaCore(this.rounds, this.engineState, this.x);
			Pack.UInt32_To_LE(this.x, output, 0);
		}

		// Token: 0x06001C2B RID: 7211 RVA: 0x0009E164 File Offset: 0x0009E164
		internal static void SalsaCore(int rounds, uint[] input, uint[] x)
		{
			if (input.Length != 16)
			{
				throw new ArgumentException();
			}
			if (x.Length != 16)
			{
				throw new ArgumentException();
			}
			if (rounds % 2 != 0)
			{
				throw new ArgumentException("Number of rounds must be even");
			}
			uint num = input[0];
			uint num2 = input[1];
			uint num3 = input[2];
			uint num4 = input[3];
			uint num5 = input[4];
			uint num6 = input[5];
			uint num7 = input[6];
			uint num8 = input[7];
			uint num9 = input[8];
			uint num10 = input[9];
			uint num11 = input[10];
			uint num12 = input[11];
			uint num13 = input[12];
			uint num14 = input[13];
			uint num15 = input[14];
			uint num16 = input[15];
			for (int i = rounds; i > 0; i -= 2)
			{
				num5 ^= Salsa20Engine.R(num + num13, 7);
				num9 ^= Salsa20Engine.R(num5 + num, 9);
				num13 ^= Salsa20Engine.R(num9 + num5, 13);
				num ^= Salsa20Engine.R(num13 + num9, 18);
				num10 ^= Salsa20Engine.R(num6 + num2, 7);
				num14 ^= Salsa20Engine.R(num10 + num6, 9);
				num2 ^= Salsa20Engine.R(num14 + num10, 13);
				num6 ^= Salsa20Engine.R(num2 + num14, 18);
				num15 ^= Salsa20Engine.R(num11 + num7, 7);
				num3 ^= Salsa20Engine.R(num15 + num11, 9);
				num7 ^= Salsa20Engine.R(num3 + num15, 13);
				num11 ^= Salsa20Engine.R(num7 + num3, 18);
				num4 ^= Salsa20Engine.R(num16 + num12, 7);
				num8 ^= Salsa20Engine.R(num4 + num16, 9);
				num12 ^= Salsa20Engine.R(num8 + num4, 13);
				num16 ^= Salsa20Engine.R(num12 + num8, 18);
				num2 ^= Salsa20Engine.R(num + num4, 7);
				num3 ^= Salsa20Engine.R(num2 + num, 9);
				num4 ^= Salsa20Engine.R(num3 + num2, 13);
				num ^= Salsa20Engine.R(num4 + num3, 18);
				num7 ^= Salsa20Engine.R(num6 + num5, 7);
				num8 ^= Salsa20Engine.R(num7 + num6, 9);
				num5 ^= Salsa20Engine.R(num8 + num7, 13);
				num6 ^= Salsa20Engine.R(num5 + num8, 18);
				num12 ^= Salsa20Engine.R(num11 + num10, 7);
				num9 ^= Salsa20Engine.R(num12 + num11, 9);
				num10 ^= Salsa20Engine.R(num9 + num12, 13);
				num11 ^= Salsa20Engine.R(num10 + num9, 18);
				num13 ^= Salsa20Engine.R(num16 + num15, 7);
				num14 ^= Salsa20Engine.R(num13 + num16, 9);
				num15 ^= Salsa20Engine.R(num14 + num13, 13);
				num16 ^= Salsa20Engine.R(num15 + num14, 18);
			}
			x[0] = num + input[0];
			x[1] = num2 + input[1];
			x[2] = num3 + input[2];
			x[3] = num4 + input[3];
			x[4] = num5 + input[4];
			x[5] = num6 + input[5];
			x[6] = num7 + input[6];
			x[7] = num8 + input[7];
			x[8] = num9 + input[8];
			x[9] = num10 + input[9];
			x[10] = num11 + input[10];
			x[11] = num12 + input[11];
			x[12] = num13 + input[12];
			x[13] = num14 + input[13];
			x[14] = num15 + input[14];
			x[15] = num16 + input[15];
		}

		// Token: 0x06001C2C RID: 7212 RVA: 0x0009E4A4 File Offset: 0x0009E4A4
		internal static uint R(uint x, int y)
		{
			return x << y | x >> 32 - y;
		}

		// Token: 0x06001C2D RID: 7213 RVA: 0x0009E4B8 File Offset: 0x0009E4B8
		private void ResetLimitCounter()
		{
			this.cW0 = 0U;
			this.cW1 = 0U;
			this.cW2 = 0U;
		}

		// Token: 0x06001C2E RID: 7214 RVA: 0x0009E4D0 File Offset: 0x0009E4D0
		private bool LimitExceeded()
		{
			return (this.cW0 += 1U) == 0U && (this.cW1 += 1U) == 0U && ((this.cW2 += 1U) & 32U) != 0U;
		}

		// Token: 0x06001C2F RID: 7215 RVA: 0x0009E52C File Offset: 0x0009E52C
		private bool LimitExceeded(uint len)
		{
			uint num = this.cW0;
			this.cW0 += len;
			return this.cW0 < num && (this.cW1 += 1U) == 0U && ((this.cW2 += 1U) & 32U) != 0U;
		}

		// Token: 0x040012B1 RID: 4785
		private const int StateSize = 16;

		// Token: 0x040012B2 RID: 4786
		public static readonly int DEFAULT_ROUNDS = 20;

		// Token: 0x040012B3 RID: 4787
		private static readonly uint[] TAU_SIGMA = Pack.LE_To_UInt32(Strings.ToAsciiByteArray("expand 16-byte kexpand 32-byte k"), 0, 8);

		// Token: 0x040012B4 RID: 4788
		[Obsolete]
		protected static readonly byte[] sigma = Strings.ToAsciiByteArray("expand 32-byte k");

		// Token: 0x040012B5 RID: 4789
		[Obsolete]
		protected static readonly byte[] tau = Strings.ToAsciiByteArray("expand 16-byte k");

		// Token: 0x040012B6 RID: 4790
		protected int rounds;

		// Token: 0x040012B7 RID: 4791
		private int index = 0;

		// Token: 0x040012B8 RID: 4792
		internal uint[] engineState = new uint[16];

		// Token: 0x040012B9 RID: 4793
		internal uint[] x = new uint[16];

		// Token: 0x040012BA RID: 4794
		private byte[] keyStream = new byte[64];

		// Token: 0x040012BB RID: 4795
		private bool initialised = false;

		// Token: 0x040012BC RID: 4796
		private uint cW0;

		// Token: 0x040012BD RID: 4797
		private uint cW1;

		// Token: 0x040012BE RID: 4798
		private uint cW2;
	}
}
