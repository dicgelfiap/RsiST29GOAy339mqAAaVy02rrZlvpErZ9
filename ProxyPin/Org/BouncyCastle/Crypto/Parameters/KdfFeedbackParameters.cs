using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x02000460 RID: 1120
	public class KdfFeedbackParameters : IDerivationParameters
	{
		// Token: 0x06002300 RID: 8960 RVA: 0x000C5F84 File Offset: 0x000C5F84
		private KdfFeedbackParameters(byte[] ki, byte[] iv, byte[] fixedInputData, int r, bool useCounter)
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
			this.r = r;
			if (iv == null)
			{
				this.iv = new byte[0];
			}
			else
			{
				this.iv = Arrays.Clone(iv);
			}
			this.useCounter = useCounter;
		}

		// Token: 0x06002301 RID: 8961 RVA: 0x000C6010 File Offset: 0x000C6010
		public static KdfFeedbackParameters CreateWithCounter(byte[] ki, byte[] iv, byte[] fixedInputData, int r)
		{
			if (r != 8 && r != 16 && r != 24 && r != 32)
			{
				throw new ArgumentException("Length of counter should be 8, 16, 24 or 32");
			}
			return new KdfFeedbackParameters(ki, iv, fixedInputData, r, true);
		}

		// Token: 0x06002302 RID: 8962 RVA: 0x000C6048 File Offset: 0x000C6048
		public static KdfFeedbackParameters CreateWithoutCounter(byte[] ki, byte[] iv, byte[] fixedInputData)
		{
			return new KdfFeedbackParameters(ki, iv, fixedInputData, KdfFeedbackParameters.UNUSED_R, false);
		}

		// Token: 0x170006B1 RID: 1713
		// (get) Token: 0x06002303 RID: 8963 RVA: 0x000C6058 File Offset: 0x000C6058
		public byte[] Ki
		{
			get
			{
				return Arrays.Clone(this.ki);
			}
		}

		// Token: 0x170006B2 RID: 1714
		// (get) Token: 0x06002304 RID: 8964 RVA: 0x000C6068 File Offset: 0x000C6068
		public byte[] Iv
		{
			get
			{
				return Arrays.Clone(this.iv);
			}
		}

		// Token: 0x170006B3 RID: 1715
		// (get) Token: 0x06002305 RID: 8965 RVA: 0x000C6078 File Offset: 0x000C6078
		public bool UseCounter
		{
			get
			{
				return this.useCounter;
			}
		}

		// Token: 0x170006B4 RID: 1716
		// (get) Token: 0x06002306 RID: 8966 RVA: 0x000C6080 File Offset: 0x000C6080
		public int R
		{
			get
			{
				return this.r;
			}
		}

		// Token: 0x170006B5 RID: 1717
		// (get) Token: 0x06002307 RID: 8967 RVA: 0x000C6088 File Offset: 0x000C6088
		public byte[] FixedInputData
		{
			get
			{
				return Arrays.Clone(this.fixedInputData);
			}
		}

		// Token: 0x04001645 RID: 5701
		private static readonly int UNUSED_R = -1;

		// Token: 0x04001646 RID: 5702
		private readonly byte[] ki;

		// Token: 0x04001647 RID: 5703
		private readonly byte[] iv;

		// Token: 0x04001648 RID: 5704
		private readonly bool useCounter;

		// Token: 0x04001649 RID: 5705
		private readonly int r;

		// Token: 0x0400164A RID: 5706
		private readonly byte[] fixedInputData;
	}
}
