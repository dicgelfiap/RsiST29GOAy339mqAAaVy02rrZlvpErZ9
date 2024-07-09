using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x02000830 RID: 2096
	[ComVisible(true)]
	public class PropertyDefUser : PropertyDef
	{
		// Token: 0x06004E7F RID: 20095 RVA: 0x0018637C File Offset: 0x0018637C
		public PropertyDefUser()
		{
		}

		// Token: 0x06004E80 RID: 20096 RVA: 0x00186384 File Offset: 0x00186384
		public PropertyDefUser(UTF8String name) : this(name, null)
		{
		}

		// Token: 0x06004E81 RID: 20097 RVA: 0x00186390 File Offset: 0x00186390
		public PropertyDefUser(UTF8String name, PropertySig sig) : this(name, sig, (PropertyAttributes)0)
		{
		}

		// Token: 0x06004E82 RID: 20098 RVA: 0x0018639C File Offset: 0x0018639C
		public PropertyDefUser(UTF8String name, PropertySig sig, PropertyAttributes flags)
		{
			this.name = name;
			this.type = sig;
			this.attributes = (int)flags;
		}
	}
}
