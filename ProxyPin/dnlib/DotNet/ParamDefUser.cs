using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x02000829 RID: 2089
	[ComVisible(true)]
	public class ParamDefUser : ParamDef
	{
		// Token: 0x06004E00 RID: 19968 RVA: 0x0018521C File Offset: 0x0018521C
		public ParamDefUser()
		{
		}

		// Token: 0x06004E01 RID: 19969 RVA: 0x00185224 File Offset: 0x00185224
		public ParamDefUser(UTF8String name) : this(name, 0)
		{
		}

		// Token: 0x06004E02 RID: 19970 RVA: 0x00185230 File Offset: 0x00185230
		public ParamDefUser(UTF8String name, ushort sequence) : this(name, sequence, (ParamAttributes)0)
		{
		}

		// Token: 0x06004E03 RID: 19971 RVA: 0x0018523C File Offset: 0x0018523C
		public ParamDefUser(UTF8String name, ushort sequence, ParamAttributes flags)
		{
			this.name = name;
			this.sequence = sequence;
			this.attributes = (int)flags;
		}
	}
}
