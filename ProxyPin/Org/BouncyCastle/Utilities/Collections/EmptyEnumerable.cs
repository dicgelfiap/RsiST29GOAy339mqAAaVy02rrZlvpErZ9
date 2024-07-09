using System;
using System.Collections;

namespace Org.BouncyCastle.Utilities.Collections
{
	// Token: 0x020006C7 RID: 1735
	public sealed class EmptyEnumerable : IEnumerable
	{
		// Token: 0x06003CB5 RID: 15541 RVA: 0x0014F518 File Offset: 0x0014F518
		private EmptyEnumerable()
		{
		}

		// Token: 0x06003CB6 RID: 15542 RVA: 0x0014F520 File Offset: 0x0014F520
		public IEnumerator GetEnumerator()
		{
			return EmptyEnumerator.Instance;
		}

		// Token: 0x04001EDF RID: 7903
		public static readonly IEnumerable Instance = new EmptyEnumerable();
	}
}
