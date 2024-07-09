using System;
using System.Runtime.InteropServices;

namespace System.Buffers
{
	// Token: 0x02000CEE RID: 3310
	[ComVisible(true)]
	public interface IPinnable
	{
		// Token: 0x060085E3 RID: 34275
		MemoryHandle Pin(int elementIndex);

		// Token: 0x060085E4 RID: 34276
		void Unpin();
	}
}
