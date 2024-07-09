using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace System.Collections.Immutable
{
	// Token: 0x02000CB3 RID: 3251
	[ComVisible(true)]
	public static class ImmutableQueue
	{
		// Token: 0x060082EA RID: 33514 RVA: 0x00266620 File Offset: 0x00266620
		public static ImmutableQueue<T> Create<T>()
		{
			return ImmutableQueue<T>.Empty;
		}

		// Token: 0x060082EB RID: 33515 RVA: 0x00266628 File Offset: 0x00266628
		public static ImmutableQueue<T> Create<T>(T item)
		{
			return ImmutableQueue<T>.Empty.Enqueue(item);
		}

		// Token: 0x060082EC RID: 33516 RVA: 0x00266638 File Offset: 0x00266638
		public static ImmutableQueue<T> CreateRange<T>(IEnumerable<T> items)
		{
			Requires.NotNull<IEnumerable<T>>(items, "items");
			T[] array = items as T[];
			if (array != null)
			{
				return ImmutableQueue.Create<T>(array);
			}
			ImmutableQueue<T> result;
			using (IEnumerator<T> enumerator = items.GetEnumerator())
			{
				if (!enumerator.MoveNext())
				{
					result = ImmutableQueue<T>.Empty;
				}
				else
				{
					ImmutableStack<T> forwards = ImmutableStack.Create<T>(enumerator.Current);
					ImmutableStack<T> immutableStack = ImmutableStack<T>.Empty;
					while (enumerator.MoveNext())
					{
						T value = enumerator.Current;
						immutableStack = immutableStack.Push(value);
					}
					result = new ImmutableQueue<T>(forwards, immutableStack);
				}
			}
			return result;
		}

		// Token: 0x060082ED RID: 33517 RVA: 0x002666DC File Offset: 0x002666DC
		public static ImmutableQueue<T> Create<T>(params T[] items)
		{
			Requires.NotNull<T[]>(items, "items");
			if (items.Length == 0)
			{
				return ImmutableQueue<T>.Empty;
			}
			ImmutableStack<T> immutableStack = ImmutableStack<T>.Empty;
			for (int i = items.Length - 1; i >= 0; i--)
			{
				immutableStack = immutableStack.Push(items[i]);
			}
			return new ImmutableQueue<T>(immutableStack, ImmutableStack<T>.Empty);
		}

		// Token: 0x060082EE RID: 33518 RVA: 0x00266738 File Offset: 0x00266738
		public static IImmutableQueue<T> Dequeue<T>(this IImmutableQueue<T> queue, out T value)
		{
			Requires.NotNull<IImmutableQueue<T>>(queue, "queue");
			value = queue.Peek();
			return queue.Dequeue();
		}
	}
}
