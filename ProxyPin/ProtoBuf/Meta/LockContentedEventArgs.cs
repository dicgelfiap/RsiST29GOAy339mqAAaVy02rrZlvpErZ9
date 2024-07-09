using System;
using System.Runtime.InteropServices;

namespace ProtoBuf.Meta
{
	// Token: 0x02000C77 RID: 3191
	[ComVisible(true)]
	public sealed class LockContentedEventArgs : EventArgs
	{
		// Token: 0x06007F68 RID: 32616 RVA: 0x0025AB74 File Offset: 0x0025AB74
		internal LockContentedEventArgs(string ownerStackTrace)
		{
			this.ownerStackTrace = ownerStackTrace;
		}

		// Token: 0x17001BAD RID: 7085
		// (get) Token: 0x06007F69 RID: 32617 RVA: 0x0025AB84 File Offset: 0x0025AB84
		public string OwnerStackTrace
		{
			get
			{
				return this.ownerStackTrace;
			}
		}

		// Token: 0x04003CD8 RID: 15576
		private readonly string ownerStackTrace;
	}
}
