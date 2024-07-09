using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace System.Collections.Immutable
{
	// Token: 0x02000CBB RID: 3259
	[ComVisible(true)]
	public static class ImmutableStack
	{
		// Token: 0x060083AD RID: 33709 RVA: 0x00268144 File Offset: 0x00268144
		public static ImmutableStack<T> Create<T>()
		{
			return ImmutableStack<T>.Empty;
		}

		// Token: 0x060083AE RID: 33710 RVA: 0x0026814C File Offset: 0x0026814C
		public static ImmutableStack<T> Create<T>(T item)
		{
			return ImmutableStack<T>.Empty.Push(item);
		}

		// Token: 0x060083AF RID: 33711 RVA: 0x0026815C File Offset: 0x0026815C
		public static ImmutableStack<T> CreateRange<T>(IEnumerable<T> items)
		{
			Requires.NotNull<IEnumerable<T>>(items, "items");
			ImmutableStack<T> immutableStack = ImmutableStack<T>.Empty;
			foreach (T value in items)
			{
				immutableStack = immutableStack.Push(value);
			}
			return immutableStack;
		}

		// Token: 0x060083B0 RID: 33712 RVA: 0x002681C0 File Offset: 0x002681C0
		public static ImmutableStack<T> Create<T>(params T[] items)
		{
			Requires.NotNull<T[]>(items, "items");
			ImmutableStack<T> immutableStack = ImmutableStack<T>.Empty;
			foreach (T value in items)
			{
				immutableStack = immutableStack.Push(value);
			}
			return immutableStack;
		}

		// Token: 0x060083B1 RID: 33713 RVA: 0x00268208 File Offset: 0x00268208
		public static IImmutableStack<T> Pop<T>(this IImmutableStack<T> stack, out T value)
		{
			Requires.NotNull<IImmutableStack<T>>(stack, "stack");
			value = stack.Peek();
			return stack.Pop();
		}
	}
}
