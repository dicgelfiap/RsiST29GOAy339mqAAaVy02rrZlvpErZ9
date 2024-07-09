using System;
using System.Runtime.InteropServices;

namespace IP2Region.Models
{
	// Token: 0x02000A5E RID: 2654
	[ComVisible(true)]
	public class DbMakerConfigException : Exception
	{
		// Token: 0x170015F4 RID: 5620
		// (get) Token: 0x06006817 RID: 26647 RVA: 0x001FB010 File Offset: 0x001FB010
		// (set) Token: 0x06006818 RID: 26648 RVA: 0x001FB018 File Offset: 0x001FB018
		public string ErrMsg { get; private set; }

		// Token: 0x06006819 RID: 26649 RVA: 0x001FB024 File Offset: 0x001FB024
		public DbMakerConfigException(string errMsg)
		{
			this.ErrMsg = errMsg;
		}
	}
}
