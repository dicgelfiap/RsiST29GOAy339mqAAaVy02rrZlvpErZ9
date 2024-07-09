using System;
using System.Collections;

namespace Org.BouncyCastle.Utilities.Collections
{
	// Token: 0x020006C9 RID: 1737
	public sealed class EnumerableProxy : IEnumerable
	{
		// Token: 0x06003CBD RID: 15549 RVA: 0x0014F55C File Offset: 0x0014F55C
		public EnumerableProxy(IEnumerable inner)
		{
			if (inner == null)
			{
				throw new ArgumentNullException("inner");
			}
			this.inner = inner;
		}

		// Token: 0x06003CBE RID: 15550 RVA: 0x0014F57C File Offset: 0x0014F57C
		public IEnumerator GetEnumerator()
		{
			return this.inner.GetEnumerator();
		}

		// Token: 0x04001EE1 RID: 7905
		private readonly IEnumerable inner;
	}
}
