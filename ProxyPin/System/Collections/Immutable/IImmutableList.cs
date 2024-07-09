using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace System.Collections.Immutable
{
	// Token: 0x02000C9C RID: 3228
	[ComVisible(true)]
	public interface IImmutableList<T> : IReadOnlyList<T>, IReadOnlyCollection<T>, IEnumerable<!0>, IEnumerable
	{
		// Token: 0x060080E3 RID: 32995
		IImmutableList<T> Clear();

		// Token: 0x060080E4 RID: 32996
		int IndexOf(T item, int index, int count, IEqualityComparer<T> equalityComparer);

		// Token: 0x060080E5 RID: 32997
		int LastIndexOf(T item, int index, int count, IEqualityComparer<T> equalityComparer);

		// Token: 0x060080E6 RID: 32998
		IImmutableList<T> Add(T value);

		// Token: 0x060080E7 RID: 32999
		IImmutableList<T> AddRange(IEnumerable<T> items);

		// Token: 0x060080E8 RID: 33000
		IImmutableList<T> Insert(int index, T element);

		// Token: 0x060080E9 RID: 33001
		IImmutableList<T> InsertRange(int index, IEnumerable<T> items);

		// Token: 0x060080EA RID: 33002
		IImmutableList<T> Remove(T value, IEqualityComparer<T> equalityComparer);

		// Token: 0x060080EB RID: 33003
		IImmutableList<T> RemoveAll(Predicate<T> match);

		// Token: 0x060080EC RID: 33004
		IImmutableList<T> RemoveRange(IEnumerable<T> items, IEqualityComparer<T> equalityComparer);

		// Token: 0x060080ED RID: 33005
		IImmutableList<T> RemoveRange(int index, int count);

		// Token: 0x060080EE RID: 33006
		IImmutableList<T> RemoveAt(int index);

		// Token: 0x060080EF RID: 33007
		IImmutableList<T> SetItem(int index, T value);

		// Token: 0x060080F0 RID: 33008
		IImmutableList<T> Replace(T oldValue, T newValue, IEqualityComparer<T> equalityComparer);
	}
}
