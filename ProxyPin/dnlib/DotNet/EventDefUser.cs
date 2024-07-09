using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x020007A5 RID: 1957
	[ComVisible(true)]
	public class EventDefUser : EventDef
	{
		// Token: 0x06004623 RID: 17955 RVA: 0x0016F7F0 File Offset: 0x0016F7F0
		public EventDefUser()
		{
		}

		// Token: 0x06004624 RID: 17956 RVA: 0x0016F7F8 File Offset: 0x0016F7F8
		public EventDefUser(UTF8String name) : this(name, null, (EventAttributes)0)
		{
		}

		// Token: 0x06004625 RID: 17957 RVA: 0x0016F804 File Offset: 0x0016F804
		public EventDefUser(UTF8String name, ITypeDefOrRef type) : this(name, type, (EventAttributes)0)
		{
		}

		// Token: 0x06004626 RID: 17958 RVA: 0x0016F810 File Offset: 0x0016F810
		public EventDefUser(UTF8String name, ITypeDefOrRef type, EventAttributes flags)
		{
			this.name = name;
			this.eventType = type;
			this.attributes = (int)flags;
		}
	}
}
