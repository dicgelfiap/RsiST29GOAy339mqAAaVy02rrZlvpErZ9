using System;
using System.Collections;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Modes.Gcm
{
	// Token: 0x020003F5 RID: 1013
	public class Tables1kGcmExponentiator : IGcmExponentiator
	{
		// Token: 0x06002032 RID: 8242 RVA: 0x000BB3BC File Offset: 0x000BB3BC
		public void Init(byte[] x)
		{
			uint[] array = GcmUtilities.AsUints(x);
			if (this.lookupPowX2 != null && Arrays.AreEqual(array, (uint[])this.lookupPowX2[0]))
			{
				return;
			}
			this.lookupPowX2 = Platform.CreateArrayList(8);
			this.lookupPowX2.Add(array);
		}

		// Token: 0x06002033 RID: 8243 RVA: 0x000BB418 File Offset: 0x000BB418
		public void ExponentiateX(long pow, byte[] output)
		{
			uint[] x = GcmUtilities.OneAsUints();
			int num = 0;
			while (pow > 0L)
			{
				if ((pow & 1L) != 0L)
				{
					this.EnsureAvailable(num);
					GcmUtilities.Multiply(x, (uint[])this.lookupPowX2[num]);
				}
				num++;
				pow >>= 1;
			}
			GcmUtilities.AsBytes(x, output);
		}

		// Token: 0x06002034 RID: 8244 RVA: 0x000BB474 File Offset: 0x000BB474
		private void EnsureAvailable(int bit)
		{
			int num = this.lookupPowX2.Count;
			if (num <= bit)
			{
				uint[] array = (uint[])this.lookupPowX2[num - 1];
				do
				{
					array = Arrays.Clone(array);
					GcmUtilities.Multiply(array, array);
					this.lookupPowX2.Add(array);
				}
				while (++num <= bit);
			}
		}

		// Token: 0x0400150D RID: 5389
		private IList lookupPowX2;
	}
}
