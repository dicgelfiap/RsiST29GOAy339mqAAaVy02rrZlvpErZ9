using System;
using System.Runtime.InteropServices;

namespace System.Buffers
{
	// Token: 0x02000CEF RID: 3311
	[ComVisible(true)]
	public interface IMemoryOwner<T> : IDisposable
	{
		// Token: 0x17001CAB RID: 7339
		// (get) Token: 0x060085E5 RID: 34277
		Memory<T> Memory { get; }
	}
}
