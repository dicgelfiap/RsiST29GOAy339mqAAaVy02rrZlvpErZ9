using System;
using System.Runtime.InteropServices;

namespace System.Buffers
{
	// Token: 0x02000CE8 RID: 3304
	[ComVisible(true)]
	public enum OperationStatus
	{
		// Token: 0x04003DCB RID: 15819
		Done,
		// Token: 0x04003DCC RID: 15820
		DestinationTooSmall,
		// Token: 0x04003DCD RID: 15821
		NeedMoreData,
		// Token: 0x04003DCE RID: 15822
		InvalidData
	}
}
