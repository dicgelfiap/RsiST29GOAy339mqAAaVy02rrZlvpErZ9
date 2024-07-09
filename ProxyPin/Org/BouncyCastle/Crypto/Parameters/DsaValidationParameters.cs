using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x02000441 RID: 1089
	public class DsaValidationParameters
	{
		// Token: 0x06002247 RID: 8775 RVA: 0x000C4540 File Offset: 0x000C4540
		public DsaValidationParameters(byte[] seed, int counter) : this(seed, counter, -1)
		{
		}

		// Token: 0x06002248 RID: 8776 RVA: 0x000C454C File Offset: 0x000C454C
		public DsaValidationParameters(byte[] seed, int counter, int usageIndex)
		{
			if (seed == null)
			{
				throw new ArgumentNullException("seed");
			}
			this.seed = (byte[])seed.Clone();
			this.counter = counter;
			this.usageIndex = usageIndex;
		}

		// Token: 0x06002249 RID: 8777 RVA: 0x000C4584 File Offset: 0x000C4584
		public virtual byte[] GetSeed()
		{
			return (byte[])this.seed.Clone();
		}

		// Token: 0x1700067E RID: 1662
		// (get) Token: 0x0600224A RID: 8778 RVA: 0x000C4598 File Offset: 0x000C4598
		public virtual int Counter
		{
			get
			{
				return this.counter;
			}
		}

		// Token: 0x1700067F RID: 1663
		// (get) Token: 0x0600224B RID: 8779 RVA: 0x000C45A0 File Offset: 0x000C45A0
		public virtual int UsageIndex
		{
			get
			{
				return this.usageIndex;
			}
		}

		// Token: 0x0600224C RID: 8780 RVA: 0x000C45A8 File Offset: 0x000C45A8
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DsaValidationParameters dsaValidationParameters = obj as DsaValidationParameters;
			return dsaValidationParameters != null && this.Equals(dsaValidationParameters);
		}

		// Token: 0x0600224D RID: 8781 RVA: 0x000C45D8 File Offset: 0x000C45D8
		protected virtual bool Equals(DsaValidationParameters other)
		{
			return this.counter == other.counter && Arrays.AreEqual(this.seed, other.seed);
		}

		// Token: 0x0600224E RID: 8782 RVA: 0x000C4600 File Offset: 0x000C4600
		public override int GetHashCode()
		{
			return this.counter.GetHashCode() ^ Arrays.GetHashCode(this.seed);
		}

		// Token: 0x040015FD RID: 5629
		private readonly byte[] seed;

		// Token: 0x040015FE RID: 5630
		private readonly int counter;

		// Token: 0x040015FF RID: 5631
		private readonly int usageIndex;
	}
}
