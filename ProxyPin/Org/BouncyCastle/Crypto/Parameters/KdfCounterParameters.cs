using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x0200045E RID: 1118
	public class KdfCounterParameters : IDerivationParameters
	{
		// Token: 0x060022F1 RID: 8945 RVA: 0x000C5DA0 File Offset: 0x000C5DA0
		public KdfCounterParameters(byte[] ki, byte[] fixedInputDataCounterSuffix, int r) : this(ki, null, fixedInputDataCounterSuffix, r)
		{
		}

		// Token: 0x060022F2 RID: 8946 RVA: 0x000C5DAC File Offset: 0x000C5DAC
		public KdfCounterParameters(byte[] ki, byte[] fixedInputDataCounterPrefix, byte[] fixedInputDataCounterSuffix, int r)
		{
			if (ki == null)
			{
				throw new ArgumentException("A KDF requires Ki (a seed) as input");
			}
			this.ki = Arrays.Clone(ki);
			if (fixedInputDataCounterPrefix == null)
			{
				this.fixedInputDataCounterPrefix = new byte[0];
			}
			else
			{
				this.fixedInputDataCounterPrefix = Arrays.Clone(fixedInputDataCounterPrefix);
			}
			if (fixedInputDataCounterSuffix == null)
			{
				this.fixedInputDataCounterSuffix = new byte[0];
			}
			else
			{
				this.fixedInputDataCounterSuffix = Arrays.Clone(fixedInputDataCounterSuffix);
			}
			if (r != 8 && r != 16 && r != 24 && r != 32)
			{
				throw new ArgumentException("Length of counter should be 8, 16, 24 or 32");
			}
			this.r = r;
		}

		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x060022F3 RID: 8947 RVA: 0x000C5E5C File Offset: 0x000C5E5C
		public byte[] Ki
		{
			get
			{
				return this.ki;
			}
		}

		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x060022F4 RID: 8948 RVA: 0x000C5E64 File Offset: 0x000C5E64
		public byte[] FixedInputData
		{
			get
			{
				return Arrays.Clone(this.fixedInputDataCounterSuffix);
			}
		}

		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x060022F5 RID: 8949 RVA: 0x000C5E74 File Offset: 0x000C5E74
		public byte[] FixedInputDataCounterPrefix
		{
			get
			{
				return Arrays.Clone(this.fixedInputDataCounterPrefix);
			}
		}

		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x060022F6 RID: 8950 RVA: 0x000C5E84 File Offset: 0x000C5E84
		public byte[] FixedInputDataCounterSuffix
		{
			get
			{
				return Arrays.Clone(this.fixedInputDataCounterSuffix);
			}
		}

		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x060022F7 RID: 8951 RVA: 0x000C5E94 File Offset: 0x000C5E94
		public int R
		{
			get
			{
				return this.r;
			}
		}

		// Token: 0x0400163C RID: 5692
		private byte[] ki;

		// Token: 0x0400163D RID: 5693
		private byte[] fixedInputDataCounterPrefix;

		// Token: 0x0400163E RID: 5694
		private byte[] fixedInputDataCounterSuffix;

		// Token: 0x0400163F RID: 5695
		private int r;
	}
}
