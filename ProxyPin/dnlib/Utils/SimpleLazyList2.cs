using System;
using System.Diagnostics;
using System.Threading;
using dnlib.DotNet;

namespace dnlib.Utils
{
	// Token: 0x02000742 RID: 1858
	[DebuggerDisplay("Count = {Length}")]
	internal sealed class SimpleLazyList2<T> where T : class, IContainsGenericParameter2
	{
		// Token: 0x17000AFC RID: 2812
		// (get) Token: 0x06004113 RID: 16659 RVA: 0x00162888 File Offset: 0x00162888
		public uint Length
		{
			get
			{
				return this.length;
			}
		}

		// Token: 0x17000AFD RID: 2813
		public T this[uint index, GenericParamContext gpContext]
		{
			get
			{
				if (index >= this.length)
				{
					return default(T);
				}
				if (this.elements[(int)index] == null)
				{
					T t = this.readElementByRID(index + 1U, gpContext);
					if (t.ContainsGenericParameter)
					{
						return t;
					}
					Interlocked.CompareExchange<T>(ref this.elements[(int)index], t, default(T));
				}
				return this.elements[(int)index];
			}
		}

		// Token: 0x06004115 RID: 16661 RVA: 0x00162918 File Offset: 0x00162918
		public SimpleLazyList2(uint length, Func<uint, GenericParamContext, T> readElementByRID)
		{
			this.length = length;
			this.readElementByRID = readElementByRID;
			this.elements = new T[length];
		}

		// Token: 0x04002292 RID: 8850
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		private readonly T[] elements;

		// Token: 0x04002293 RID: 8851
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly Func<uint, GenericParamContext, T> readElementByRID;

		// Token: 0x04002294 RID: 8852
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly uint length;
	}
}
