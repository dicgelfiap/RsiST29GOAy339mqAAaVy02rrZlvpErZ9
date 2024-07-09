using System;
using System.Runtime.InteropServices;

namespace System.Buffers
{
	// Token: 0x02000CEC RID: 3308
	[ComVisible(true)]
	public abstract class ReadOnlySequenceSegment<T>
	{
		// Token: 0x17001CA4 RID: 7332
		// (get) Token: 0x060085CE RID: 34254 RVA: 0x00273220 File Offset: 0x00273220
		// (set) Token: 0x060085CF RID: 34255 RVA: 0x00273228 File Offset: 0x00273228
		public ReadOnlyMemory<T> Memory { get; protected set; }

		// Token: 0x17001CA5 RID: 7333
		// (get) Token: 0x060085D0 RID: 34256 RVA: 0x00273234 File Offset: 0x00273234
		// (set) Token: 0x060085D1 RID: 34257 RVA: 0x0027323C File Offset: 0x0027323C
		public ReadOnlySequenceSegment<T> Next { get; protected set; }

		// Token: 0x17001CA6 RID: 7334
		// (get) Token: 0x060085D2 RID: 34258 RVA: 0x00273248 File Offset: 0x00273248
		// (set) Token: 0x060085D3 RID: 34259 RVA: 0x00273250 File Offset: 0x00273250
		public long RunningIndex { get; protected set; }
	}
}
