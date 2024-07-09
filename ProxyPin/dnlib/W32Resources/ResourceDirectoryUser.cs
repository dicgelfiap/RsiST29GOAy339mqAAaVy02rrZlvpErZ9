using System;
using System.Runtime.InteropServices;
using dnlib.Utils;

namespace dnlib.W32Resources
{
	// Token: 0x02000732 RID: 1842
	[ComVisible(true)]
	public class ResourceDirectoryUser : ResourceDirectory
	{
		// Token: 0x060040AB RID: 16555 RVA: 0x0016164C File Offset: 0x0016164C
		public ResourceDirectoryUser(ResourceName name) : base(name)
		{
			this.directories = new LazyList<ResourceDirectory>();
			this.data = new LazyList<ResourceData>();
		}
	}
}
