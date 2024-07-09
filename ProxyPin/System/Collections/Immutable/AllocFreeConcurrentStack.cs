using System;
using System.Collections.Generic;

namespace System.Collections.Immutable
{
	// Token: 0x02000C93 RID: 3219
	internal static class AllocFreeConcurrentStack<T>
	{
		// Token: 0x060080BF RID: 32959 RVA: 0x00261590 File Offset: 0x00261590
		public static void TryAdd(T item)
		{
			Stack<RefAsValueType<T>> threadLocalStack = AllocFreeConcurrentStack<T>.ThreadLocalStack;
			if (threadLocalStack.Count < 35)
			{
				threadLocalStack.Push(new RefAsValueType<T>(item));
			}
		}

		// Token: 0x060080C0 RID: 32960 RVA: 0x002615C0 File Offset: 0x002615C0
		public static bool TryTake(out T item)
		{
			Stack<RefAsValueType<T>> threadLocalStack = AllocFreeConcurrentStack<T>.ThreadLocalStack;
			if (threadLocalStack != null && threadLocalStack.Count > 0)
			{
				item = threadLocalStack.Pop().Value;
				return true;
			}
			item = default(T);
			return false;
		}

		// Token: 0x17001BE2 RID: 7138
		// (get) Token: 0x060080C1 RID: 32961 RVA: 0x00261604 File Offset: 0x00261604
		private static Stack<RefAsValueType<T>> ThreadLocalStack
		{
			get
			{
				Dictionary<Type, object> dictionary = AllocFreeConcurrentStack.t_stacks;
				if (dictionary == null)
				{
					dictionary = (AllocFreeConcurrentStack.t_stacks = new Dictionary<Type, object>());
				}
				object obj;
				if (!dictionary.TryGetValue(AllocFreeConcurrentStack<T>.s_typeOfT, out obj))
				{
					obj = new Stack<RefAsValueType<T>>(35);
					dictionary.Add(AllocFreeConcurrentStack<T>.s_typeOfT, obj);
				}
				return (Stack<RefAsValueType<T>>)obj;
			}
		}

		// Token: 0x04003D1E RID: 15646
		private const int MaxSize = 35;

		// Token: 0x04003D1F RID: 15647
		private static readonly Type s_typeOfT = typeof(T);
	}
}
