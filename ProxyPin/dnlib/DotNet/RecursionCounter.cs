using System;

namespace dnlib.DotNet
{
	// Token: 0x02000835 RID: 2101
	internal struct RecursionCounter
	{
		// Token: 0x17000FD2 RID: 4050
		// (get) Token: 0x06004EAC RID: 20140 RVA: 0x00186930 File Offset: 0x00186930
		public int Counter
		{
			get
			{
				return this.counter;
			}
		}

		// Token: 0x06004EAD RID: 20141 RVA: 0x00186938 File Offset: 0x00186938
		public bool Increment()
		{
			if (this.counter >= 100)
			{
				return false;
			}
			this.counter++;
			return true;
		}

		// Token: 0x06004EAE RID: 20142 RVA: 0x00186958 File Offset: 0x00186958
		public void Decrement()
		{
			this.counter--;
		}

		// Token: 0x06004EAF RID: 20143 RVA: 0x00186968 File Offset: 0x00186968
		public override string ToString()
		{
			return this.counter.ToString();
		}

		// Token: 0x040026C4 RID: 9924
		public const int MAX_RECURSION_COUNT = 100;

		// Token: 0x040026C5 RID: 9925
		private int counter;
	}
}
