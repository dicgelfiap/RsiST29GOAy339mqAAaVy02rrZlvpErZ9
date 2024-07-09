using System;
using Org.BouncyCastle.Crypto.Utilities;

namespace Org.BouncyCastle.Crypto.Engines
{
	// Token: 0x020003AF RID: 943
	public class XSalsa20Engine : Salsa20Engine
	{
		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x06001E23 RID: 7715 RVA: 0x000B0798 File Offset: 0x000B0798
		public override string AlgorithmName
		{
			get
			{
				return "XSalsa20";
			}
		}

		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x06001E24 RID: 7716 RVA: 0x000B07A0 File Offset: 0x000B07A0
		protected override int NonceSize
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x06001E25 RID: 7717 RVA: 0x000B07A4 File Offset: 0x000B07A4
		protected override void SetKey(byte[] keyBytes, byte[] ivBytes)
		{
			if (keyBytes == null)
			{
				throw new ArgumentException(this.AlgorithmName + " doesn't support re-init with null key");
			}
			if (keyBytes.Length != 32)
			{
				throw new ArgumentException(this.AlgorithmName + " requires a 256 bit key");
			}
			base.SetKey(keyBytes, ivBytes);
			Pack.LE_To_UInt32(ivBytes, 8, this.engineState, 8, 2);
			uint[] array = new uint[this.engineState.Length];
			Salsa20Engine.SalsaCore(20, this.engineState, array);
			this.engineState[1] = array[0] - this.engineState[0];
			this.engineState[2] = array[5] - this.engineState[5];
			this.engineState[3] = array[10] - this.engineState[10];
			this.engineState[4] = array[15] - this.engineState[15];
			this.engineState[11] = array[6] - this.engineState[6];
			this.engineState[12] = array[7] - this.engineState[7];
			this.engineState[13] = array[8] - this.engineState[8];
			this.engineState[14] = array[9] - this.engineState[9];
			Pack.LE_To_UInt32(ivBytes, 16, this.engineState, 6, 2);
		}
	}
}
