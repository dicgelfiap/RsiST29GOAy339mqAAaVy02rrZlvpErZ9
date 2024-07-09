using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace dnlib.W32Resources
{
	// Token: 0x02000737 RID: 1847
	[ComVisible(true)]
	public class Win32ResourcesUser : Win32Resources
	{
		// Token: 0x17000AF3 RID: 2803
		// (get) Token: 0x060040D3 RID: 16595 RVA: 0x00161C48 File Offset: 0x00161C48
		// (set) Token: 0x060040D4 RID: 16596 RVA: 0x00161C50 File Offset: 0x00161C50
		public override ResourceDirectory Root
		{
			get
			{
				return this.root;
			}
			set
			{
				Interlocked.Exchange<ResourceDirectory>(ref this.root, value);
			}
		}

		// Token: 0x0400227C RID: 8828
		private ResourceDirectory root = new ResourceDirectoryUser(new ResourceName("root"));
	}
}
