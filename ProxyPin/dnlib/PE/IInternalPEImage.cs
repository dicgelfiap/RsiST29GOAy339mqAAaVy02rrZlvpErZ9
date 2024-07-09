using System;
using System.Runtime.InteropServices;

namespace dnlib.PE
{
	// Token: 0x02000755 RID: 1877
	[ComVisible(true)]
	public interface IInternalPEImage : IPEImage, IRvaFileOffsetConverter, IDisposable
	{
		// Token: 0x060041BF RID: 16831
		void UnsafeDisableMemoryMappedIO();

		// Token: 0x17000B89 RID: 2953
		// (get) Token: 0x060041C0 RID: 16832
		bool IsMemoryMappedIO { get; }
	}
}
