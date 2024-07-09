using System;
using System.Collections.Generic;

namespace System.Collections.Immutable
{
	// Token: 0x02000C9D RID: 3229
	internal interface IImmutableListQueries<T> : IReadOnlyList<T>, IReadOnlyCollection<T>, IEnumerable<!0>, IEnumerable
	{
		// Token: 0x060080F1 RID: 33009
		ImmutableList<TOutput> ConvertAll<TOutput>(Func<T, TOutput> converter);

		// Token: 0x060080F2 RID: 33010
		void ForEach(Action<T> action);

		// Token: 0x060080F3 RID: 33011
		ImmutableList<T> GetRange(int index, int count);

		// Token: 0x060080F4 RID: 33012
		void CopyTo(T[] array);

		// Token: 0x060080F5 RID: 33013
		void CopyTo(T[] array, int arrayIndex);

		// Token: 0x060080F6 RID: 33014
		void CopyTo(int index, T[] array, int arrayIndex, int count);

		// Token: 0x060080F7 RID: 33015
		bool Exists(Predicate<T> match);

		// Token: 0x060080F8 RID: 33016
		T Find(Predicate<T> match);

		// Token: 0x060080F9 RID: 33017
		ImmutableList<T> FindAll(Predicate<T> match);

		// Token: 0x060080FA RID: 33018
		int FindIndex(Predicate<T> match);

		// Token: 0x060080FB RID: 33019
		int FindIndex(int startIndex, Predicate<T> match);

		// Token: 0x060080FC RID: 33020
		int FindIndex(int startIndex, int count, Predicate<T> match);

		// Token: 0x060080FD RID: 33021
		T FindLast(Predicate<T> match);

		// Token: 0x060080FE RID: 33022
		int FindLastIndex(Predicate<T> match);

		// Token: 0x060080FF RID: 33023
		int FindLastIndex(int startIndex, Predicate<T> match);

		// Token: 0x06008100 RID: 33024
		int FindLastIndex(int startIndex, int count, Predicate<T> match);

		// Token: 0x06008101 RID: 33025
		bool TrueForAll(Predicate<T> match);

		// Token: 0x06008102 RID: 33026
		int BinarySearch(T item);

		// Token: 0x06008103 RID: 33027
		int BinarySearch(T item, IComparer<T> comparer);

		// Token: 0x06008104 RID: 33028
		int BinarySearch(int index, int count, T item, IComparer<T> comparer);
	}
}
