using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x0200080A RID: 2058
	[ComVisible(true)]
	public class MemberRefUser : MemberRef
	{
		// Token: 0x06004A81 RID: 19073 RVA: 0x0017C828 File Offset: 0x0017C828
		public MemberRefUser(ModuleDef module)
		{
			this.module = module;
		}

		// Token: 0x06004A82 RID: 19074 RVA: 0x0017C838 File Offset: 0x0017C838
		public MemberRefUser(ModuleDef module, UTF8String name)
		{
			this.module = module;
			this.name = name;
		}

		// Token: 0x06004A83 RID: 19075 RVA: 0x0017C850 File Offset: 0x0017C850
		public MemberRefUser(ModuleDef module, UTF8String name, FieldSig sig) : this(module, name, sig, null)
		{
		}

		// Token: 0x06004A84 RID: 19076 RVA: 0x0017C85C File Offset: 0x0017C85C
		public MemberRefUser(ModuleDef module, UTF8String name, FieldSig sig, IMemberRefParent @class)
		{
			this.module = module;
			this.name = name;
			this.@class = @class;
			this.signature = sig;
		}

		// Token: 0x06004A85 RID: 19077 RVA: 0x0017C884 File Offset: 0x0017C884
		public MemberRefUser(ModuleDef module, UTF8String name, MethodSig sig) : this(module, name, sig, null)
		{
		}

		// Token: 0x06004A86 RID: 19078 RVA: 0x0017C890 File Offset: 0x0017C890
		public MemberRefUser(ModuleDef module, UTF8String name, MethodSig sig, IMemberRefParent @class)
		{
			this.module = module;
			this.name = name;
			this.@class = @class;
			this.signature = sig;
		}
	}
}
