using System;
using System.Runtime.InteropServices;

namespace dnlib.W32Resources
{
	// Token: 0x02000734 RID: 1844
	[ComVisible(true)]
	public abstract class ResourceDirectoryEntry
	{
		// Token: 0x17000AED RID: 2797
		// (get) Token: 0x060040B4 RID: 16564 RVA: 0x001619C8 File Offset: 0x001619C8
		// (set) Token: 0x060040B5 RID: 16565 RVA: 0x001619D0 File Offset: 0x001619D0
		public ResourceName Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x060040B6 RID: 16566 RVA: 0x001619DC File Offset: 0x001619DC
		protected ResourceDirectoryEntry(ResourceName name)
		{
			this.name = name;
		}

		// Token: 0x060040B7 RID: 16567 RVA: 0x001619EC File Offset: 0x001619EC
		public override string ToString()
		{
			return this.name.ToString();
		}

		// Token: 0x04002279 RID: 8825
		private ResourceName name;
	}
}
