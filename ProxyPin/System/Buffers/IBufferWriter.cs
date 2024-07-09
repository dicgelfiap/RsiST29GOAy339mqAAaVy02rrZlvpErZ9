using System;
using System.Runtime.InteropServices;

namespace System.Buffers
{
	// Token: 0x02000CE6 RID: 3302
	[ComVisible(true)]
	public interface IBufferWriter<T>
	{
		// Token: 0x0600858F RID: 34191
		void Advance(int count);

		// Token: 0x06008590 RID: 34192
		Memory<T> GetMemory(int sizeHint = 0);

		// Token: 0x06008591 RID: 34193
		Span<T> GetSpan(int sizeHint = 0);
	}
}
