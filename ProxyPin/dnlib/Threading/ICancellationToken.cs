using System;
using System.Runtime.InteropServices;

namespace dnlib.Threading
{
	// Token: 0x02000744 RID: 1860
	[ComVisible(true)]
	public interface ICancellationToken
	{
		// Token: 0x0600411C RID: 16668
		void ThrowIfCancellationRequested();
	}
}
