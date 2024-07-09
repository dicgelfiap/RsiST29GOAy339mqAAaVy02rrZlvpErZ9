using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x020007E8 RID: 2024
	[ComVisible(true)]
	public class ImplMapUser : ImplMap
	{
		// Token: 0x06004901 RID: 18689 RVA: 0x00177608 File Offset: 0x00177608
		public ImplMapUser()
		{
		}

		// Token: 0x06004902 RID: 18690 RVA: 0x00177610 File Offset: 0x00177610
		public ImplMapUser(ModuleRef scope, UTF8String name, PInvokeAttributes flags)
		{
			this.module = scope;
			this.name = name;
			this.attributes = (int)flags;
		}
	}
}
