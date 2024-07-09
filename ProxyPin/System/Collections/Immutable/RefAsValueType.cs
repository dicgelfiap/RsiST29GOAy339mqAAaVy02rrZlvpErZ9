using System;
using System.Diagnostics;

namespace System.Collections.Immutable
{
	// Token: 0x02000CC0 RID: 3264
	[DebuggerDisplay("{Value,nq}")]
	internal struct RefAsValueType<T>
	{
		// Token: 0x060083D6 RID: 33750 RVA: 0x002685AC File Offset: 0x002685AC
		internal RefAsValueType(T value)
		{
			this.Value = value;
		}

		// Token: 0x04003D50 RID: 15696
		internal T Value;
	}
}
