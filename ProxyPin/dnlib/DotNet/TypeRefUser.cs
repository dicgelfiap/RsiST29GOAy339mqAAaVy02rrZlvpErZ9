using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x02000864 RID: 2148
	[ComVisible(true)]
	public class TypeRefUser : TypeRef
	{
		// Token: 0x0600525D RID: 21085 RVA: 0x00195C8C File Offset: 0x00195C8C
		public TypeRefUser(ModuleDef module, UTF8String name) : this(module, UTF8String.Empty, name)
		{
		}

		// Token: 0x0600525E RID: 21086 RVA: 0x00195C9C File Offset: 0x00195C9C
		public TypeRefUser(ModuleDef module, UTF8String @namespace, UTF8String name) : this(module, @namespace, name, null)
		{
		}

		// Token: 0x0600525F RID: 21087 RVA: 0x00195CA8 File Offset: 0x00195CA8
		public TypeRefUser(ModuleDef module, UTF8String @namespace, UTF8String name, IResolutionScope resolutionScope)
		{
			this.module = module;
			this.resolutionScope = resolutionScope;
			this.resolutionScope_isInitialized = true;
			this.name = name;
			this.@namespace = @namespace;
		}
	}
}
