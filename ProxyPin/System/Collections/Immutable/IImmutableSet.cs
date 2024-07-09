using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace System.Collections.Immutable
{
	// Token: 0x02000C9F RID: 3231
	[ComVisible(true)]
	public interface IImmutableSet<T> : IReadOnlyCollection<T>, IEnumerable<!0>, IEnumerable
	{
		// Token: 0x0600810A RID: 33034
		IImmutableSet<T> Clear();

		// Token: 0x0600810B RID: 33035
		bool Contains(T value);

		// Token: 0x0600810C RID: 33036
		IImmutableSet<T> Add(T value);

		// Token: 0x0600810D RID: 33037
		IImmutableSet<T> Remove(T value);

		// Token: 0x0600810E RID: 33038
		bool TryGetValue(T equalValue, out T actualValue);

		// Token: 0x0600810F RID: 33039
		IImmutableSet<T> Intersect(IEnumerable<T> other);

		// Token: 0x06008110 RID: 33040
		IImmutableSet<T> Except(IEnumerable<T> other);

		// Token: 0x06008111 RID: 33041
		IImmutableSet<T> SymmetricExcept(IEnumerable<T> other);

		// Token: 0x06008112 RID: 33042
		IImmutableSet<T> Union(IEnumerable<T> other);

		// Token: 0x06008113 RID: 33043
		bool SetEquals(IEnumerable<T> other);

		// Token: 0x06008114 RID: 33044
		bool IsProperSubsetOf(IEnumerable<T> other);

		// Token: 0x06008115 RID: 33045
		bool IsProperSupersetOf(IEnumerable<T> other);

		// Token: 0x06008116 RID: 33046
		bool IsSubsetOf(IEnumerable<T> other);

		// Token: 0x06008117 RID: 33047
		bool IsSupersetOf(IEnumerable<T> other);

		// Token: 0x06008118 RID: 33048
		bool Overlaps(IEnumerable<T> other);
	}
}
