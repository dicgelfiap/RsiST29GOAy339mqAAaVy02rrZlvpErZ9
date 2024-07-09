using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace System.Collections.Immutable
{
	// Token: 0x02000CAD RID: 3245
	internal static class ImmutableExtensions
	{
		// Token: 0x0600824E RID: 33358 RVA: 0x00265104 File Offset: 0x00265104
		internal static bool IsValueType<T>()
		{
			if (default(T) != null)
			{
				return true;
			}
			Type typeFromHandle = typeof(T);
			return typeFromHandle.IsConstructedGenericType && typeFromHandle.GetGenericTypeDefinition() == typeof(Nullable<>);
		}

		// Token: 0x0600824F RID: 33359 RVA: 0x00265160 File Offset: 0x00265160
		internal static IOrderedCollection<T> AsOrderedCollection<T>(this IEnumerable<T> sequence)
		{
			Requires.NotNull<IEnumerable<T>>(sequence, "sequence");
			IOrderedCollection<T> orderedCollection = sequence as IOrderedCollection<T>;
			if (orderedCollection != null)
			{
				return orderedCollection;
			}
			IList<T> list = sequence as IList<T>;
			if (list != null)
			{
				return new ImmutableExtensions.ListOfTWrapper<T>(list);
			}
			return new ImmutableExtensions.FallbackWrapper<T>(sequence);
		}

		// Token: 0x06008250 RID: 33360 RVA: 0x002651A8 File Offset: 0x002651A8
		internal static void ClearFastWhenEmpty<T>(this Stack<T> stack)
		{
			if (stack.Count > 0)
			{
				stack.Clear();
			}
		}

		// Token: 0x06008251 RID: 33361 RVA: 0x002651BC File Offset: 0x002651BC
		internal static DisposableEnumeratorAdapter<T, TEnumerator> GetEnumerableDisposable<T, TEnumerator>(this IEnumerable<T> enumerable) where TEnumerator : struct, IStrongEnumerator<T>, IEnumerator<T>
		{
			Requires.NotNull<IEnumerable<T>>(enumerable, "enumerable");
			IStrongEnumerable<T, TEnumerator> strongEnumerable = enumerable as IStrongEnumerable<T, TEnumerator>;
			if (strongEnumerable != null)
			{
				return new DisposableEnumeratorAdapter<T, TEnumerator>(strongEnumerable.GetEnumerator());
			}
			return new DisposableEnumeratorAdapter<T, TEnumerator>(enumerable.GetEnumerator());
		}

		// Token: 0x06008252 RID: 33362 RVA: 0x002651FC File Offset: 0x002651FC
		internal static bool TryGetCount<T>(this IEnumerable<T> sequence, out int count)
		{
			return sequence.TryGetCount(out count);
		}

		// Token: 0x06008253 RID: 33363 RVA: 0x00265208 File Offset: 0x00265208
		internal static bool TryGetCount<T>(this IEnumerable sequence, out int count)
		{
			ICollection collection = sequence as ICollection;
			if (collection != null)
			{
				count = collection.Count;
				return true;
			}
			ICollection<T> collection2 = sequence as ICollection<T>;
			if (collection2 != null)
			{
				count = collection2.Count;
				return true;
			}
			IReadOnlyCollection<T> readOnlyCollection = sequence as IReadOnlyCollection<T>;
			if (readOnlyCollection != null)
			{
				count = readOnlyCollection.Count;
				return true;
			}
			count = 0;
			return false;
		}

		// Token: 0x06008254 RID: 33364 RVA: 0x00265264 File Offset: 0x00265264
		internal static int GetCount<T>(ref IEnumerable<T> sequence)
		{
			int count;
			if (!sequence.TryGetCount(out count))
			{
				List<T> list = sequence.ToList<T>();
				count = list.Count;
				sequence = list;
			}
			return count;
		}

		// Token: 0x06008255 RID: 33365 RVA: 0x00265298 File Offset: 0x00265298
		internal static bool TryCopyTo<T>(this IEnumerable<T> sequence, T[] array, int arrayIndex)
		{
			IList<T> list = sequence as IList<T>;
			if (list != null)
			{
				List<T> list2 = sequence as List<T>;
				if (list2 != null)
				{
					list2.CopyTo(array, arrayIndex);
					return true;
				}
				if (sequence.GetType() == typeof(T[]))
				{
					T[] array2 = (T[])sequence;
					Array.Copy(array2, 0, array, arrayIndex, array2.Length);
					return true;
				}
				if (sequence is ImmutableArray<T>)
				{
					ImmutableArray<T> immutableArray = (ImmutableArray<T>)sequence;
					Array.Copy(immutableArray.array, 0, array, arrayIndex, immutableArray.Length);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06008256 RID: 33366 RVA: 0x00265328 File Offset: 0x00265328
		internal static T[] ToArray<T>(this IEnumerable<T> sequence, int count)
		{
			Requires.NotNull<IEnumerable<T>>(sequence, "sequence");
			Requires.Range(count >= 0, "count", null);
			if (count == 0)
			{
				return ImmutableArray<T>.Empty.array;
			}
			T[] array = new T[count];
			if (!sequence.TryCopyTo(array, 0))
			{
				int num = 0;
				foreach (T t in sequence)
				{
					Requires.Argument(num < count);
					array[num++] = t;
				}
				Requires.Argument(num == count);
			}
			return array;
		}

		// Token: 0x020011AA RID: 4522
		private class ListOfTWrapper<T> : IOrderedCollection<T>, IEnumerable<!0>, IEnumerable
		{
			// Token: 0x060094C7 RID: 38087 RVA: 0x002C69E4 File Offset: 0x002C69E4
			internal ListOfTWrapper(IList<T> collection)
			{
				Requires.NotNull<IList<T>>(collection, "collection");
				this._collection = collection;
			}

			// Token: 0x17001EDC RID: 7900
			// (get) Token: 0x060094C8 RID: 38088 RVA: 0x002C6A00 File Offset: 0x002C6A00
			public int Count
			{
				get
				{
					return this._collection.Count;
				}
			}

			// Token: 0x17001EDD RID: 7901
			public T this[int index]
			{
				get
				{
					return this._collection[index];
				}
			}

			// Token: 0x060094CA RID: 38090 RVA: 0x002C6A20 File Offset: 0x002C6A20
			public IEnumerator<T> GetEnumerator()
			{
				return this._collection.GetEnumerator();
			}

			// Token: 0x060094CB RID: 38091 RVA: 0x002C6A30 File Offset: 0x002C6A30
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x04004C02 RID: 19458
			private readonly IList<T> _collection;
		}

		// Token: 0x020011AB RID: 4523
		private class FallbackWrapper<T> : IOrderedCollection<T>, IEnumerable<!0>, IEnumerable
		{
			// Token: 0x060094CC RID: 38092 RVA: 0x002C6A38 File Offset: 0x002C6A38
			internal FallbackWrapper(IEnumerable<T> sequence)
			{
				Requires.NotNull<IEnumerable<T>>(sequence, "sequence");
				this._sequence = sequence;
			}

			// Token: 0x17001EDE RID: 7902
			// (get) Token: 0x060094CD RID: 38093 RVA: 0x002C6A54 File Offset: 0x002C6A54
			public int Count
			{
				get
				{
					if (this._collection == null)
					{
						int result;
						if (this._sequence.TryGetCount(out result))
						{
							return result;
						}
						this._collection = this._sequence.ToArray<T>();
					}
					return this._collection.Count;
				}
			}

			// Token: 0x17001EDF RID: 7903
			public T this[int index]
			{
				get
				{
					if (this._collection == null)
					{
						this._collection = this._sequence.ToArray<T>();
					}
					return this._collection[index];
				}
			}

			// Token: 0x060094CF RID: 38095 RVA: 0x002C6ACC File Offset: 0x002C6ACC
			public IEnumerator<T> GetEnumerator()
			{
				return this._sequence.GetEnumerator();
			}

			// Token: 0x060094D0 RID: 38096 RVA: 0x002C6ADC File Offset: 0x002C6ADC
			[ExcludeFromCodeCoverage]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x04004C03 RID: 19459
			private readonly IEnumerable<T> _sequence;

			// Token: 0x04004C04 RID: 19460
			private IList<T> _collection;
		}
	}
}
