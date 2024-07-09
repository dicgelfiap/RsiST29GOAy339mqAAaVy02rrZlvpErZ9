using System;
using System.Diagnostics;

namespace System
{
	// Token: 0x02000CDA RID: 3290
	internal sealed class SpanDebugView<T>
	{
		// Token: 0x06008516 RID: 34070 RVA: 0x0026D4A4 File Offset: 0x0026D4A4
		public SpanDebugView(Span<T> span)
		{
			this._array = span.ToArray();
		}

		// Token: 0x06008517 RID: 34071 RVA: 0x0026D4BC File Offset: 0x0026D4BC
		public SpanDebugView(ReadOnlySpan<T> span)
		{
			this._array = span.ToArray();
		}

		// Token: 0x17001C86 RID: 7302
		// (get) Token: 0x06008518 RID: 34072 RVA: 0x0026D4D4 File Offset: 0x0026D4D4
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public T[] Items
		{
			get
			{
				return this._array;
			}
		}

		// Token: 0x04003DB9 RID: 15801
		private readonly T[] _array;
	}
}
