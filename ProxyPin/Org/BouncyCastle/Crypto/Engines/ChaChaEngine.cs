﻿using System;
using Org.BouncyCastle.Crypto.Utilities;

namespace Org.BouncyCastle.Crypto.Engines
{
	// Token: 0x02000385 RID: 901
	public class ChaChaEngine : Salsa20Engine
	{
		// Token: 0x06001C38 RID: 7224 RVA: 0x0009E6B8 File Offset: 0x0009E6B8
		public ChaChaEngine()
		{
		}

		// Token: 0x06001C39 RID: 7225 RVA: 0x0009E6C0 File Offset: 0x0009E6C0
		public ChaChaEngine(int rounds) : base(rounds)
		{
		}

		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x06001C3A RID: 7226 RVA: 0x0009E6CC File Offset: 0x0009E6CC
		public override string AlgorithmName
		{
			get
			{
				return "ChaCha" + this.rounds;
			}
		}

		// Token: 0x06001C3B RID: 7227 RVA: 0x0009E6E4 File Offset: 0x0009E6E4
		protected override void AdvanceCounter()
		{
			uint[] engineState;
			if (((engineState = this.engineState)[12] = engineState[12] + 1U) == 0U)
			{
				(engineState = this.engineState)[13] = engineState[13] + 1U;
			}
		}

		// Token: 0x06001C3C RID: 7228 RVA: 0x0009E720 File Offset: 0x0009E720
		protected override void ResetCounter()
		{
			this.engineState[12] = (this.engineState[13] = 0U);
		}

		// Token: 0x06001C3D RID: 7229 RVA: 0x0009E748 File Offset: 0x0009E748
		protected override void SetKey(byte[] keyBytes, byte[] ivBytes)
		{
			if (keyBytes != null)
			{
				if (keyBytes.Length != 16 && keyBytes.Length != 32)
				{
					throw new ArgumentException(this.AlgorithmName + " requires 128 bit or 256 bit key");
				}
				base.PackTauOrSigma(keyBytes.Length, this.engineState, 0);
				Pack.LE_To_UInt32(keyBytes, 0, this.engineState, 4, 4);
				Pack.LE_To_UInt32(keyBytes, keyBytes.Length - 16, this.engineState, 8, 4);
			}
			Pack.LE_To_UInt32(ivBytes, 0, this.engineState, 14, 2);
		}

		// Token: 0x06001C3E RID: 7230 RVA: 0x0009E7CC File Offset: 0x0009E7CC
		protected override void GenerateKeyStream(byte[] output)
		{
			ChaChaEngine.ChachaCore(this.rounds, this.engineState, this.x);
			Pack.UInt32_To_LE(this.x, output, 0);
		}

		// Token: 0x06001C3F RID: 7231 RVA: 0x0009E7F4 File Offset: 0x0009E7F4
		internal static void ChachaCore(int rounds, uint[] input, uint[] x)
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
				num += num5;
				num13 = Salsa20Engine.R(num13 ^ num, 16);
				num9 += num13;
				num5 = Salsa20Engine.R(num5 ^ num9, 12);
				num += num5;
				num13 = Salsa20Engine.R(num13 ^ num, 8);
				num9 += num13;
				num5 = Salsa20Engine.R(num5 ^ num9, 7);
				num2 += num6;
				num14 = Salsa20Engine.R(num14 ^ num2, 16);
				num10 += num14;
				num6 = Salsa20Engine.R(num6 ^ num10, 12);
				num2 += num6;
				num14 = Salsa20Engine.R(num14 ^ num2, 8);
				num10 += num14;
				num6 = Salsa20Engine.R(num6 ^ num10, 7);
				num3 += num7;
				num15 = Salsa20Engine.R(num15 ^ num3, 16);
				num11 += num15;
				num7 = Salsa20Engine.R(num7 ^ num11, 12);
				num3 += num7;
				num15 = Salsa20Engine.R(num15 ^ num3, 8);
				num11 += num15;
				num7 = Salsa20Engine.R(num7 ^ num11, 7);
				num4 += num8;
				num16 = Salsa20Engine.R(num16 ^ num4, 16);
				num12 += num16;
				num8 = Salsa20Engine.R(num8 ^ num12, 12);
				num4 += num8;
				num16 = Salsa20Engine.R(num16 ^ num4, 8);
				num12 += num16;
				num8 = Salsa20Engine.R(num8 ^ num12, 7);
				num += num6;
				num16 = Salsa20Engine.R(num16 ^ num, 16);
				num11 += num16;
				num6 = Salsa20Engine.R(num6 ^ num11, 12);
				num += num6;
				num16 = Salsa20Engine.R(num16 ^ num, 8);
				num11 += num16;
				num6 = Salsa20Engine.R(num6 ^ num11, 7);
				num2 += num7;
				num13 = Salsa20Engine.R(num13 ^ num2, 16);
				num12 += num13;
				num7 = Salsa20Engine.R(num7 ^ num12, 12);
				num2 += num7;
				num13 = Salsa20Engine.R(num13 ^ num2, 8);
				num12 += num13;
				num7 = Salsa20Engine.R(num7 ^ num12, 7);
				num3 += num8;
				num14 = Salsa20Engine.R(num14 ^ num3, 16);
				num9 += num14;
				num8 = Salsa20Engine.R(num8 ^ num9, 12);
				num3 += num8;
				num14 = Salsa20Engine.R(num14 ^ num3, 8);
				num9 += num14;
				num8 = Salsa20Engine.R(num8 ^ num9, 7);
				num4 += num5;
				num15 = Salsa20Engine.R(num15 ^ num4, 16);
				num10 += num15;
				num5 = Salsa20Engine.R(num5 ^ num10, 12);
				num4 += num5;
				num15 = Salsa20Engine.R(num15 ^ num4, 8);
				num10 += num15;
				num5 = Salsa20Engine.R(num5 ^ num10, 7);
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
	}
}
