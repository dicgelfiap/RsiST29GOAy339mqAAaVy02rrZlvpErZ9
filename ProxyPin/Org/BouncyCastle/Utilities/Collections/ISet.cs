using System;
using System.Collections;

namespace Org.BouncyCastle.Utilities.Collections
{
	// Token: 0x020006CA RID: 1738
	public interface ISet : ICollection, IEnumerable
	{
		// Token: 0x06003CBF RID: 15551
		void Add(object o);

		// Token: 0x06003CC0 RID: 15552
		void AddAll(IEnumerable e);

		// Token: 0x06003CC1 RID: 15553
		void Clear();

		// Token: 0x06003CC2 RID: 15554
		bool Contains(object o);

		// Token: 0x17000A47 RID: 2631
		// (get) Token: 0x06003CC3 RID: 15555
		bool IsEmpty { get; }

		// Token: 0x17000A48 RID: 2632
		// (get) Token: 0x06003CC4 RID: 15556
		bool IsFixedSize { get; }

		// Token: 0x17000A49 RID: 2633
		// (get) Token: 0x06003CC5 RID: 15557
		bool IsReadOnly { get; }

		// Token: 0x06003CC6 RID: 15558
		void Remove(object o);

		// Token: 0x06003CC7 RID: 15559
		void RemoveAll(IEnumerable e);
	}
}
