using System;
using System.Runtime.InteropServices;

namespace dnlib.W32Resources
{
	// Token: 0x02000736 RID: 1846
	[ComVisible(true)]
	public abstract class Win32Resources : IDisposable
	{
		// Token: 0x17000AF2 RID: 2802
		// (get) Token: 0x060040CB RID: 16587
		// (set) Token: 0x060040CC RID: 16588
		public abstract ResourceDirectory Root { get; set; }

		// Token: 0x060040CD RID: 16589 RVA: 0x00161BA4 File Offset: 0x00161BA4
		public ResourceDirectory Find(ResourceName type)
		{
			ResourceDirectory root = this.Root;
			if (root == null)
			{
				return null;
			}
			return root.FindDirectory(type);
		}

		// Token: 0x060040CE RID: 16590 RVA: 0x00161BCC File Offset: 0x00161BCC
		public ResourceDirectory Find(ResourceName type, ResourceName name)
		{
			ResourceDirectory resourceDirectory = this.Find(type);
			if (resourceDirectory == null)
			{
				return null;
			}
			return resourceDirectory.FindDirectory(name);
		}

		// Token: 0x060040CF RID: 16591 RVA: 0x00161BF4 File Offset: 0x00161BF4
		public ResourceData Find(ResourceName type, ResourceName name, ResourceName langId)
		{
			ResourceDirectory resourceDirectory = this.Find(type, name);
			if (resourceDirectory == null)
			{
				return null;
			}
			return resourceDirectory.FindData(langId);
		}

		// Token: 0x060040D0 RID: 16592 RVA: 0x00161C20 File Offset: 0x00161C20
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060040D1 RID: 16593 RVA: 0x00161C30 File Offset: 0x00161C30
		protected virtual void Dispose(bool disposing)
		{
			if (!disposing)
			{
				return;
			}
			this.Root = null;
		}
	}
}
