using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.Resources
{
	// Token: 0x020008EF RID: 2287
	[ComVisible(true)]
	public sealed class UserResourceType
	{
		// Token: 0x17001266 RID: 4710
		// (get) Token: 0x060058FE RID: 22782 RVA: 0x001B55B8 File Offset: 0x001B55B8
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17001267 RID: 4711
		// (get) Token: 0x060058FF RID: 22783 RVA: 0x001B55C0 File Offset: 0x001B55C0
		public ResourceTypeCode Code
		{
			get
			{
				return this.code;
			}
		}

		// Token: 0x06005900 RID: 22784 RVA: 0x001B55C8 File Offset: 0x001B55C8
		public UserResourceType(string name, ResourceTypeCode code)
		{
			this.name = name;
			this.code = code;
		}

		// Token: 0x06005901 RID: 22785 RVA: 0x001B55E0 File Offset: 0x001B55E0
		public override string ToString()
		{
			return string.Format("{0:X2} {1}", (int)this.code, this.name);
		}

		// Token: 0x04002B0E RID: 11022
		private readonly string name;

		// Token: 0x04002B0F RID: 11023
		private readonly ResourceTypeCode code;
	}
}
