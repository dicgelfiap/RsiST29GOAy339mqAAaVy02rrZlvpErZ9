using System;
using System.Runtime.InteropServices;
using dnlib.Utils;

namespace dnlib.DotNet
{
	// Token: 0x0200080E RID: 2062
	[ComVisible(true)]
	public class MethodDefUser : MethodDef
	{
		// Token: 0x06004B4C RID: 19276 RVA: 0x0017D8A0 File Offset: 0x0017D8A0
		public MethodDefUser()
		{
			this.paramDefs = new LazyList<ParamDef>(this);
			this.genericParameters = new LazyList<GenericParam>(this);
			this.parameterList = new ParameterList(this, null);
			this.semAttrs = (0 | MethodDef.SEMATTRS_INITD);
		}

		// Token: 0x06004B4D RID: 19277 RVA: 0x0017D8DC File Offset: 0x0017D8DC
		public MethodDefUser(UTF8String name) : this(name, null, MethodImplAttributes.IL, MethodAttributes.PrivateScope)
		{
		}

		// Token: 0x06004B4E RID: 19278 RVA: 0x0017D8E8 File Offset: 0x0017D8E8
		public MethodDefUser(UTF8String name, MethodSig methodSig) : this(name, methodSig, MethodImplAttributes.IL, MethodAttributes.PrivateScope)
		{
		}

		// Token: 0x06004B4F RID: 19279 RVA: 0x0017D8F4 File Offset: 0x0017D8F4
		public MethodDefUser(UTF8String name, MethodSig methodSig, MethodAttributes flags) : this(name, methodSig, MethodImplAttributes.IL, flags)
		{
		}

		// Token: 0x06004B50 RID: 19280 RVA: 0x0017D900 File Offset: 0x0017D900
		public MethodDefUser(UTF8String name, MethodSig methodSig, MethodImplAttributes implFlags) : this(name, methodSig, implFlags, MethodAttributes.PrivateScope)
		{
		}

		// Token: 0x06004B51 RID: 19281 RVA: 0x0017D90C File Offset: 0x0017D90C
		public MethodDefUser(UTF8String name, MethodSig methodSig, MethodImplAttributes implFlags, MethodAttributes flags)
		{
			this.name = name;
			this.signature = methodSig;
			this.paramDefs = new LazyList<ParamDef>(this);
			this.genericParameters = new LazyList<GenericParam>(this);
			this.implAttributes = (int)implFlags;
			this.attributes = (int)flags;
			this.parameterList = new ParameterList(this, null);
			this.semAttrs = (0 | MethodDef.SEMATTRS_INITD);
		}
	}
}
