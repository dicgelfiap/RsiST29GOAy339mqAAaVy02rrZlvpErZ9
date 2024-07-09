using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x02000477 RID: 1143
	public class TweakableBlockCipherParameters : ICipherParameters
	{
		// Token: 0x0600236E RID: 9070 RVA: 0x000C6CEC File Offset: 0x000C6CEC
		public TweakableBlockCipherParameters(KeyParameter key, byte[] tweak)
		{
			this.key = key;
			this.tweak = Arrays.Clone(tweak);
		}

		// Token: 0x170006E1 RID: 1761
		// (get) Token: 0x0600236F RID: 9071 RVA: 0x000C6D08 File Offset: 0x000C6D08
		public KeyParameter Key
		{
			get
			{
				return this.key;
			}
		}

		// Token: 0x170006E2 RID: 1762
		// (get) Token: 0x06002370 RID: 9072 RVA: 0x000C6D10 File Offset: 0x000C6D10
		public byte[] Tweak
		{
			get
			{
				return this.tweak;
			}
		}

		// Token: 0x04001685 RID: 5765
		private readonly byte[] tweak;

		// Token: 0x04001686 RID: 5766
		private readonly KeyParameter key;
	}
}
