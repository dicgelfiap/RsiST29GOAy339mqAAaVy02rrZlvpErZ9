using System;
using System.Diagnostics;

namespace System
{
	// Token: 0x02000CD5 RID: 3285
	internal sealed class MemoryDebugView<T>
	{
		// Token: 0x06008473 RID: 33907 RVA: 0x0026A480 File Offset: 0x0026A480
		public MemoryDebugView(Memory<T> memory)
		{
			this._memory = memory;
		}

		// Token: 0x06008474 RID: 33908 RVA: 0x0026A494 File Offset: 0x0026A494
		public MemoryDebugView(ReadOnlyMemory<T> memory)
		{
			this._memory = memory;
		}

		// Token: 0x17001C75 RID: 7285
		// (get) Token: 0x06008475 RID: 33909 RVA: 0x0026A4A4 File Offset: 0x0026A4A4
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public T[] Items
		{
			get
			{
				return this._memory.ToArray();
			}
		}

		// Token: 0x04003DAD RID: 15789
		private readonly ReadOnlyMemory<T> _memory;
	}
}
