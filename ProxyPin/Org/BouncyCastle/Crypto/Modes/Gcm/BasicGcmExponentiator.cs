using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Modes.Gcm
{
	// Token: 0x020003F1 RID: 1009
	public class BasicGcmExponentiator : IGcmExponentiator
	{
		// Token: 0x06002007 RID: 8199 RVA: 0x000BAAF8 File Offset: 0x000BAAF8
		public void Init(byte[] x)
		{
			this.x = GcmUtilities.AsUints(x);
		}

		// Token: 0x06002008 RID: 8200 RVA: 0x000BAB08 File Offset: 0x000BAB08
		public void ExponentiateX(long pow, byte[] output)
		{
			uint[] array = GcmUtilities.OneAsUints();
			if (pow > 0L)
			{
				uint[] y = Arrays.Clone(this.x);
				do
				{
					if ((pow & 1L) != 0L)
					{
						GcmUtilities.Multiply(array, y);
					}
					GcmUtilities.Multiply(y, y);
					pow >>= 1;
				}
				while (pow > 0L);
			}
			GcmUtilities.AsBytes(array, output);
		}

		// Token: 0x04001508 RID: 5384
		private uint[] x;
	}
}
