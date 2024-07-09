using System;
using System.Runtime.InteropServices;

namespace dnlib.IO
{
	// Token: 0x0200076E RID: 1902
	[ComVisible(true)]
	public interface IFileSection
	{
		// Token: 0x17000BAA RID: 2986
		// (get) Token: 0x060042AD RID: 17069
		FileOffset StartOffset { get; }

		// Token: 0x17000BAB RID: 2987
		// (get) Token: 0x060042AE RID: 17070
		FileOffset EndOffset { get; }
	}
}
