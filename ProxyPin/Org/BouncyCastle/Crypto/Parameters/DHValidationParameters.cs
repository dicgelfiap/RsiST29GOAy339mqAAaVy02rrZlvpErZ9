using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x0200043A RID: 1082
	public class DHValidationParameters
	{
		// Token: 0x0600221F RID: 8735 RVA: 0x000C4054 File Offset: 0x000C4054
		public DHValidationParameters(byte[] seed, int counter)
		{
			if (seed == null)
			{
				throw new ArgumentNullException("seed");
			}
			this.seed = (byte[])seed.Clone();
			this.counter = counter;
		}

		// Token: 0x06002220 RID: 8736 RVA: 0x000C4088 File Offset: 0x000C4088
		public byte[] GetSeed()
		{
			return (byte[])this.seed.Clone();
		}

		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x06002221 RID: 8737 RVA: 0x000C409C File Offset: 0x000C409C
		public int Counter
		{
			get
			{
				return this.counter;
			}
		}

		// Token: 0x06002222 RID: 8738 RVA: 0x000C40A4 File Offset: 0x000C40A4
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DHValidationParameters dhvalidationParameters = obj as DHValidationParameters;
			return dhvalidationParameters != null && this.Equals(dhvalidationParameters);
		}

		// Token: 0x06002223 RID: 8739 RVA: 0x000C40D4 File Offset: 0x000C40D4
		protected bool Equals(DHValidationParameters other)
		{
			return this.counter == other.counter && Arrays.AreEqual(this.seed, other.seed);
		}

		// Token: 0x06002224 RID: 8740 RVA: 0x000C40FC File Offset: 0x000C40FC
		public override int GetHashCode()
		{
			return this.counter.GetHashCode() ^ Arrays.GetHashCode(this.seed);
		}

		// Token: 0x040015EC RID: 5612
		private readonly byte[] seed;

		// Token: 0x040015ED RID: 5613
		private readonly int counter;
	}
}
