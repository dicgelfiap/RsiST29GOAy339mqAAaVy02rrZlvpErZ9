using System;
using Org.BouncyCastle.Crypto.Utilities;

namespace Org.BouncyCastle.Crypto.Engines
{
	// Token: 0x02000384 RID: 900
	public class ChaCha7539Engine : Salsa20Engine
	{
		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x06001C32 RID: 7218 RVA: 0x0009E5D8 File Offset: 0x0009E5D8
		public override string AlgorithmName
		{
			get
			{
				return "ChaCha7539";
			}
		}

		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x06001C33 RID: 7219 RVA: 0x0009E5E0 File Offset: 0x0009E5E0
		protected override int NonceSize
		{
			get
			{
				return 12;
			}
		}

		// Token: 0x06001C34 RID: 7220 RVA: 0x0009E5E4 File Offset: 0x0009E5E4
		protected override void AdvanceCounter()
		{
			uint[] engineState;
			if (((engineState = this.engineState)[12] = engineState[12] + 1U) == 0U)
			{
				throw new InvalidOperationException("attempt to increase counter past 2^32.");
			}
		}

		// Token: 0x06001C35 RID: 7221 RVA: 0x0009E61C File Offset: 0x0009E61C
		protected override void ResetCounter()
		{
			this.engineState[12] = 0U;
		}

		// Token: 0x06001C36 RID: 7222 RVA: 0x0009E628 File Offset: 0x0009E628
		protected override void SetKey(byte[] keyBytes, byte[] ivBytes)
		{
			if (keyBytes != null)
			{
				if (keyBytes.Length != 32)
				{
					throw new ArgumentException(this.AlgorithmName + " requires 256 bit key");
				}
				base.PackTauOrSigma(keyBytes.Length, this.engineState, 0);
				Pack.LE_To_UInt32(keyBytes, 0, this.engineState, 4, 8);
			}
			Pack.LE_To_UInt32(ivBytes, 0, this.engineState, 13, 3);
		}

		// Token: 0x06001C37 RID: 7223 RVA: 0x0009E690 File Offset: 0x0009E690
		protected override void GenerateKeyStream(byte[] output)
		{
			ChaChaEngine.ChachaCore(this.rounds, this.engineState, this.x);
			Pack.UInt32_To_LE(this.x, output, 0);
		}
	}
}
