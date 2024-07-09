using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x0200045F RID: 1119
	public class KdfDoublePipelineIterationParameters : IDerivationParameters
	{
		// Token: 0x060022F8 RID: 8952 RVA: 0x000C5E9C File Offset: 0x000C5E9C
		private KdfDoublePipelineIterationParameters(byte[] ki, byte[] fixedInputData, int r, bool useCounter)
		{
			if (ki == null)
			{
				throw new ArgumentException("A KDF requires Ki (a seed) as input");
			}
			this.ki = Arrays.Clone(ki);
			if (fixedInputData == null)
			{
				this.fixedInputData = new byte[0];
			}
			else
			{
				this.fixedInputData = Arrays.Clone(fixedInputData);
			}
			if (r != 8 && r != 16 && r != 24 && r != 32)
			{
				throw new ArgumentException("Length of counter should be 8, 16, 24 or 32");
			}
			this.r = r;
			this.useCounter = useCounter;
		}

		// Token: 0x060022F9 RID: 8953 RVA: 0x000C5F2C File Offset: 0x000C5F2C
		public static KdfDoublePipelineIterationParameters CreateWithCounter(byte[] ki, byte[] fixedInputData, int r)
		{
			return new KdfDoublePipelineIterationParameters(ki, fixedInputData, r, true);
		}

		// Token: 0x060022FA RID: 8954 RVA: 0x000C5F38 File Offset: 0x000C5F38
		public static KdfDoublePipelineIterationParameters CreateWithoutCounter(byte[] ki, byte[] fixedInputData)
		{
			return new KdfDoublePipelineIterationParameters(ki, fixedInputData, KdfDoublePipelineIterationParameters.UNUSED_R, false);
		}

		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x060022FB RID: 8955 RVA: 0x000C5F48 File Offset: 0x000C5F48
		public byte[] Ki
		{
			get
			{
				return Arrays.Clone(this.ki);
			}
		}

		// Token: 0x170006AE RID: 1710
		// (get) Token: 0x060022FC RID: 8956 RVA: 0x000C5F58 File Offset: 0x000C5F58
		public bool UseCounter
		{
			get
			{
				return this.useCounter;
			}
		}

		// Token: 0x170006AF RID: 1711
		// (get) Token: 0x060022FD RID: 8957 RVA: 0x000C5F60 File Offset: 0x000C5F60
		public int R
		{
			get
			{
				return this.r;
			}
		}

		// Token: 0x170006B0 RID: 1712
		// (get) Token: 0x060022FE RID: 8958 RVA: 0x000C5F68 File Offset: 0x000C5F68
		public byte[] FixedInputData
		{
			get
			{
				return Arrays.Clone(this.fixedInputData);
			}
		}

		// Token: 0x04001640 RID: 5696
		private static readonly int UNUSED_R = 32;

		// Token: 0x04001641 RID: 5697
		private readonly byte[] ki;

		// Token: 0x04001642 RID: 5698
		private readonly bool useCounter;

		// Token: 0x04001643 RID: 5699
		private readonly int r;

		// Token: 0x04001644 RID: 5700
		private readonly byte[] fixedInputData;
	}
}
