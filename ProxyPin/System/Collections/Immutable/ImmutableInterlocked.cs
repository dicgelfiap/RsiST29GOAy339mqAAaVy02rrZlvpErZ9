using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Collections.Immutable
{
	// Token: 0x02000CAF RID: 3247
	[ComVisible(true)]
	public static class ImmutableInterlocked
	{
		// Token: 0x06008264 RID: 33380 RVA: 0x002654D0 File Offset: 0x002654D0
		public static bool Update<T>(ref T location, Func<T, T> transformer) where T : class
		{
			Requires.NotNull<Func<T, T>>(transformer, "transformer");
			T t = Volatile.Read<T>(ref location);
			for (;;)
			{
				T t2 = transformer(t);
				if (t == t2)
				{
					break;
				}
				T t3 = Interlocked.CompareExchange<T>(ref location, t2, t);
				bool flag = t == t3;
				t = t3;
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06008265 RID: 33381 RVA: 0x0026552C File Offset: 0x0026552C
		public static bool Update<T, TArg>(ref T location, Func<T, TArg, T> transformer, TArg transformerArgument) where T : class
		{
			Requires.NotNull<Func<T, TArg, T>>(transformer, "transformer");
			T t = Volatile.Read<T>(ref location);
			for (;;)
			{
				T t2 = transformer(t, transformerArgument);
				if (t == t2)
				{
					break;
				}
				T t3 = Interlocked.CompareExchange<T>(ref location, t2, t);
				bool flag = t == t3;
				t = t3;
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06008266 RID: 33382 RVA: 0x0026558C File Offset: 0x0026558C
		public static ImmutableArray<T> InterlockedExchange<T>(ref ImmutableArray<T> location, ImmutableArray<T> value)
		{
			return new ImmutableArray<T>(Interlocked.Exchange<T[]>(ref location.array, value.array));
		}

		// Token: 0x06008267 RID: 33383 RVA: 0x002655A4 File Offset: 0x002655A4
		public static ImmutableArray<T> InterlockedCompareExchange<T>(ref ImmutableArray<T> location, ImmutableArray<T> value, ImmutableArray<T> comparand)
		{
			return new ImmutableArray<T>(Interlocked.CompareExchange<T[]>(ref location.array, value.array, comparand.array));
		}

		// Token: 0x06008268 RID: 33384 RVA: 0x002655C4 File Offset: 0x002655C4
		public static bool InterlockedInitialize<T>(ref ImmutableArray<T> location, ImmutableArray<T> value)
		{
			return ImmutableInterlocked.InterlockedCompareExchange<T>(ref location, value, default(ImmutableArray<T>)).IsDefault;
		}

		// Token: 0x06008269 RID: 33385 RVA: 0x002655F0 File Offset: 0x002655F0
		public static TValue GetOrAdd<TKey, TValue, TArg>(ref ImmutableDictionary<TKey, TValue> location, TKey key, Func<TKey, TArg, TValue> valueFactory, TArg factoryArgument)
		{
			Requires.NotNull<Func<TKey, TArg, TValue>>(valueFactory, "valueFactory");
			ImmutableDictionary<TKey, TValue> immutableDictionary = Volatile.Read<ImmutableDictionary<TKey, TValue>>(ref location);
			Requires.NotNull<ImmutableDictionary<TKey, TValue>>(immutableDictionary, "location");
			TValue tvalue;
			if (immutableDictionary.TryGetValue(key, out tvalue))
			{
				return tvalue;
			}
			tvalue = valueFactory(key, factoryArgument);
			return ImmutableInterlocked.GetOrAdd<TKey, TValue>(ref location, key, tvalue);
		}

		// Token: 0x0600826A RID: 33386 RVA: 0x00265640 File Offset: 0x00265640
		public static TValue GetOrAdd<TKey, TValue>(ref ImmutableDictionary<TKey, TValue> location, TKey key, Func<TKey, TValue> valueFactory)
		{
			Requires.NotNull<Func<TKey, TValue>>(valueFactory, "valueFactory");
			ImmutableDictionary<TKey, TValue> immutableDictionary = Volatile.Read<ImmutableDictionary<TKey, TValue>>(ref location);
			Requires.NotNull<ImmutableDictionary<TKey, TValue>>(immutableDictionary, "location");
			TValue tvalue;
			if (immutableDictionary.TryGetValue(key, out tvalue))
			{
				return tvalue;
			}
			tvalue = valueFactory(key);
			return ImmutableInterlocked.GetOrAdd<TKey, TValue>(ref location, key, tvalue);
		}

		// Token: 0x0600826B RID: 33387 RVA: 0x00265690 File Offset: 0x00265690
		public static TValue GetOrAdd<TKey, TValue>(ref ImmutableDictionary<TKey, TValue> location, TKey key, TValue value)
		{
			ImmutableDictionary<TKey, TValue> immutableDictionary = Volatile.Read<ImmutableDictionary<TKey, TValue>>(ref location);
			TValue result;
			for (;;)
			{
				Requires.NotNull<ImmutableDictionary<TKey, TValue>>(immutableDictionary, "location");
				if (immutableDictionary.TryGetValue(key, out result))
				{
					break;
				}
				ImmutableDictionary<TKey, TValue> value2 = immutableDictionary.Add(key, value);
				ImmutableDictionary<TKey, TValue> immutableDictionary2 = Interlocked.CompareExchange<ImmutableDictionary<TKey, TValue>>(ref location, value2, immutableDictionary);
				bool flag = immutableDictionary == immutableDictionary2;
				immutableDictionary = immutableDictionary2;
				if (flag)
				{
					return value;
				}
			}
			return result;
		}

		// Token: 0x0600826C RID: 33388 RVA: 0x002656E4 File Offset: 0x002656E4
		public static TValue AddOrUpdate<TKey, TValue>(ref ImmutableDictionary<TKey, TValue> location, TKey key, Func<TKey, TValue> addValueFactory, Func<TKey, TValue, TValue> updateValueFactory)
		{
			Requires.NotNull<Func<TKey, TValue>>(addValueFactory, "addValueFactory");
			Requires.NotNull<Func<TKey, TValue, TValue>>(updateValueFactory, "updateValueFactory");
			ImmutableDictionary<TKey, TValue> immutableDictionary = Volatile.Read<ImmutableDictionary<TKey, TValue>>(ref location);
			TValue tvalue;
			bool flag;
			do
			{
				Requires.NotNull<ImmutableDictionary<TKey, TValue>>(immutableDictionary, "location");
				TValue arg;
				if (immutableDictionary.TryGetValue(key, out arg))
				{
					tvalue = updateValueFactory(key, arg);
				}
				else
				{
					tvalue = addValueFactory(key);
				}
				ImmutableDictionary<TKey, TValue> value = immutableDictionary.SetItem(key, tvalue);
				ImmutableDictionary<TKey, TValue> immutableDictionary2 = Interlocked.CompareExchange<ImmutableDictionary<TKey, TValue>>(ref location, value, immutableDictionary);
				flag = (immutableDictionary == immutableDictionary2);
				immutableDictionary = immutableDictionary2;
			}
			while (!flag);
			return tvalue;
		}

		// Token: 0x0600826D RID: 33389 RVA: 0x00265764 File Offset: 0x00265764
		public static TValue AddOrUpdate<TKey, TValue>(ref ImmutableDictionary<TKey, TValue> location, TKey key, TValue addValue, Func<TKey, TValue, TValue> updateValueFactory)
		{
			Requires.NotNull<Func<TKey, TValue, TValue>>(updateValueFactory, "updateValueFactory");
			ImmutableDictionary<TKey, TValue> immutableDictionary = Volatile.Read<ImmutableDictionary<TKey, TValue>>(ref location);
			TValue tvalue;
			bool flag;
			do
			{
				Requires.NotNull<ImmutableDictionary<TKey, TValue>>(immutableDictionary, "location");
				TValue arg;
				if (immutableDictionary.TryGetValue(key, out arg))
				{
					tvalue = updateValueFactory(key, arg);
				}
				else
				{
					tvalue = addValue;
				}
				ImmutableDictionary<TKey, TValue> value = immutableDictionary.SetItem(key, tvalue);
				ImmutableDictionary<TKey, TValue> immutableDictionary2 = Interlocked.CompareExchange<ImmutableDictionary<TKey, TValue>>(ref location, value, immutableDictionary);
				flag = (immutableDictionary == immutableDictionary2);
				immutableDictionary = immutableDictionary2;
			}
			while (!flag);
			return tvalue;
		}

		// Token: 0x0600826E RID: 33390 RVA: 0x002657D4 File Offset: 0x002657D4
		public static bool TryAdd<TKey, TValue>(ref ImmutableDictionary<TKey, TValue> location, TKey key, TValue value)
		{
			ImmutableDictionary<TKey, TValue> immutableDictionary = Volatile.Read<ImmutableDictionary<TKey, TValue>>(ref location);
			for (;;)
			{
				Requires.NotNull<ImmutableDictionary<TKey, TValue>>(immutableDictionary, "location");
				if (immutableDictionary.ContainsKey(key))
				{
					break;
				}
				ImmutableDictionary<TKey, TValue> value2 = immutableDictionary.Add(key, value);
				ImmutableDictionary<TKey, TValue> immutableDictionary2 = Interlocked.CompareExchange<ImmutableDictionary<TKey, TValue>>(ref location, value2, immutableDictionary);
				bool flag = immutableDictionary == immutableDictionary2;
				immutableDictionary = immutableDictionary2;
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600826F RID: 33391 RVA: 0x00265824 File Offset: 0x00265824
		public static bool TryUpdate<TKey, TValue>(ref ImmutableDictionary<TKey, TValue> location, TKey key, TValue newValue, TValue comparisonValue)
		{
			EqualityComparer<TValue> @default = EqualityComparer<TValue>.Default;
			ImmutableDictionary<TKey, TValue> immutableDictionary = Volatile.Read<ImmutableDictionary<TKey, TValue>>(ref location);
			for (;;)
			{
				Requires.NotNull<ImmutableDictionary<TKey, TValue>>(immutableDictionary, "location");
				TValue x;
				if (!immutableDictionary.TryGetValue(key, out x) || !@default.Equals(x, comparisonValue))
				{
					break;
				}
				ImmutableDictionary<TKey, TValue> value = immutableDictionary.SetItem(key, newValue);
				ImmutableDictionary<TKey, TValue> immutableDictionary2 = Interlocked.CompareExchange<ImmutableDictionary<TKey, TValue>>(ref location, value, immutableDictionary);
				bool flag = immutableDictionary == immutableDictionary2;
				immutableDictionary = immutableDictionary2;
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06008270 RID: 33392 RVA: 0x0026588C File Offset: 0x0026588C
		public static bool TryRemove<TKey, TValue>(ref ImmutableDictionary<TKey, TValue> location, TKey key, out TValue value)
		{
			ImmutableDictionary<TKey, TValue> immutableDictionary = Volatile.Read<ImmutableDictionary<TKey, TValue>>(ref location);
			for (;;)
			{
				Requires.NotNull<ImmutableDictionary<TKey, TValue>>(immutableDictionary, "location");
				if (!immutableDictionary.TryGetValue(key, out value))
				{
					break;
				}
				ImmutableDictionary<TKey, TValue> value2 = immutableDictionary.Remove(key);
				ImmutableDictionary<TKey, TValue> immutableDictionary2 = Interlocked.CompareExchange<ImmutableDictionary<TKey, TValue>>(ref location, value2, immutableDictionary);
				bool flag = immutableDictionary == immutableDictionary2;
				immutableDictionary = immutableDictionary2;
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06008271 RID: 33393 RVA: 0x002658DC File Offset: 0x002658DC
		public static bool TryPop<T>(ref ImmutableStack<T> location, out T value)
		{
			ImmutableStack<T> immutableStack = Volatile.Read<ImmutableStack<T>>(ref location);
			for (;;)
			{
				Requires.NotNull<ImmutableStack<T>>(immutableStack, "location");
				if (immutableStack.IsEmpty)
				{
					break;
				}
				ImmutableStack<T> value2 = immutableStack.Pop(out value);
				ImmutableStack<T> immutableStack2 = Interlocked.CompareExchange<ImmutableStack<T>>(ref location, value2, immutableStack);
				bool flag = immutableStack == immutableStack2;
				immutableStack = immutableStack2;
				if (flag)
				{
					return true;
				}
			}
			value = default(T);
			return false;
		}

		// Token: 0x06008272 RID: 33394 RVA: 0x00265930 File Offset: 0x00265930
		public static void Push<T>(ref ImmutableStack<T> location, T value)
		{
			ImmutableStack<T> immutableStack = Volatile.Read<ImmutableStack<T>>(ref location);
			bool flag;
			do
			{
				Requires.NotNull<ImmutableStack<T>>(immutableStack, "location");
				ImmutableStack<T> value2 = immutableStack.Push(value);
				ImmutableStack<T> immutableStack2 = Interlocked.CompareExchange<ImmutableStack<T>>(ref location, value2, immutableStack);
				flag = (immutableStack == immutableStack2);
				immutableStack = immutableStack2;
			}
			while (!flag);
		}

		// Token: 0x06008273 RID: 33395 RVA: 0x00265970 File Offset: 0x00265970
		public static bool TryDequeue<T>(ref ImmutableQueue<T> location, out T value)
		{
			ImmutableQueue<T> immutableQueue = Volatile.Read<ImmutableQueue<T>>(ref location);
			for (;;)
			{
				Requires.NotNull<ImmutableQueue<T>>(immutableQueue, "location");
				if (immutableQueue.IsEmpty)
				{
					break;
				}
				ImmutableQueue<T> value2 = immutableQueue.Dequeue(out value);
				ImmutableQueue<T> immutableQueue2 = Interlocked.CompareExchange<ImmutableQueue<T>>(ref location, value2, immutableQueue);
				bool flag = immutableQueue == immutableQueue2;
				immutableQueue = immutableQueue2;
				if (flag)
				{
					return true;
				}
			}
			value = default(T);
			return false;
		}

		// Token: 0x06008274 RID: 33396 RVA: 0x002659C4 File Offset: 0x002659C4
		public static void Enqueue<T>(ref ImmutableQueue<T> location, T value)
		{
			ImmutableQueue<T> immutableQueue = Volatile.Read<ImmutableQueue<T>>(ref location);
			bool flag;
			do
			{
				Requires.NotNull<ImmutableQueue<T>>(immutableQueue, "location");
				ImmutableQueue<T> value2 = immutableQueue.Enqueue(value);
				ImmutableQueue<T> immutableQueue2 = Interlocked.CompareExchange<ImmutableQueue<T>>(ref location, value2, immutableQueue);
				flag = (immutableQueue == immutableQueue2);
				immutableQueue = immutableQueue2;
			}
			while (!flag);
		}
	}
}
