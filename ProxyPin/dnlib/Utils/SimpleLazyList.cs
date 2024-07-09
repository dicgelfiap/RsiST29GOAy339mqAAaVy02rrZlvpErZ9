using System;
using System.Diagnostics;
using System.Threading;

namespace dnlib.Utils
{
	// Token: 0x02000741 RID: 1857
	[DebuggerDisplay("Count = {Length}")]
	internal sealed class SimpleLazyList<T> where T : class
	{
		// Token: 0x17000AFA RID: 2810
		// (get) Token: 0x06004110 RID: 16656 RVA: 0x001627E8 File Offset: 0x001627E8
		public uint Length
		{
			get
			{
				return this.length;
			}
		}

		// Token: 0x17000AFB RID: 2811
		public T this[uint index]
		{
			get
			{
				if (index >= this.length)
				{
					return default(T);
				}
				if (this.elements[(int)index] == null)
				{
					Interlocked.CompareExchange<T>(ref this.elements[(int)index], this.readElementByRID(index + 1U), default(T));
				}
				return this.elements[(int)index];
			}
		}

		// Token: 0x06004112 RID: 16658 RVA: 0x00162864 File Offset: 0x00162864
		public SimpleLazyList(uint length, Func<uint, T> readElementByRID)
		{
			this.length = length;
			this.readElementByRID = readElementByRID;
			this.elements = new T[length];
		}

		// Token: 0x0400228F RID: 8847
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		private readonly T[] elements;

		// Token: 0x04002290 RID: 8848
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly Func<uint, T> readElementByRID;

		// Token: 0x04002291 RID: 8849
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly uint length;
	}
}
