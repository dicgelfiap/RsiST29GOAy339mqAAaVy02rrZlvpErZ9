using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.Writer
{
	// Token: 0x020008A1 RID: 2209
	[ComVisible(true)]
	public interface IHeap : IChunk
	{
		// Token: 0x17001165 RID: 4453
		// (get) Token: 0x06005481 RID: 21633
		string Name { get; }

		// Token: 0x17001166 RID: 4454
		// (get) Token: 0x06005482 RID: 21634
		bool IsEmpty { get; }

		// Token: 0x06005483 RID: 21635
		void SetReadOnly();
	}
}
