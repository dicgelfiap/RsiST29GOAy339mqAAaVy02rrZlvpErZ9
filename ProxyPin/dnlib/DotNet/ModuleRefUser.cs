using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x02000823 RID: 2083
	[ComVisible(true)]
	public class ModuleRefUser : ModuleRef
	{
		// Token: 0x06004DC2 RID: 19906 RVA: 0x00184C64 File Offset: 0x00184C64
		public ModuleRefUser(ModuleDef module) : this(module, UTF8String.Empty)
		{
		}

		// Token: 0x06004DC3 RID: 19907 RVA: 0x00184C74 File Offset: 0x00184C74
		public ModuleRefUser(ModuleDef module, UTF8String name)
		{
			this.module = module;
			this.name = name;
		}
	}
}
