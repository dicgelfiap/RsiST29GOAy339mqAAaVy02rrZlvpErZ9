using System;
using System.Collections;

namespace Org.BouncyCastle.Utilities.Collections
{
	// Token: 0x020006C8 RID: 1736
	public sealed class EmptyEnumerator : IEnumerator
	{
		// Token: 0x06003CB8 RID: 15544 RVA: 0x0014F534 File Offset: 0x0014F534
		private EmptyEnumerator()
		{
		}

		// Token: 0x06003CB9 RID: 15545 RVA: 0x0014F53C File Offset: 0x0014F53C
		public bool MoveNext()
		{
			return false;
		}

		// Token: 0x06003CBA RID: 15546 RVA: 0x0014F540 File Offset: 0x0014F540
		public void Reset()
		{
		}

		// Token: 0x17000A46 RID: 2630
		// (get) Token: 0x06003CBB RID: 15547 RVA: 0x0014F544 File Offset: 0x0014F544
		public object Current
		{
			get
			{
				throw new InvalidOperationException("No elements");
			}
		}

		// Token: 0x04001EE0 RID: 7904
		public static readonly IEnumerator Instance = new EmptyEnumerator();
	}
}
