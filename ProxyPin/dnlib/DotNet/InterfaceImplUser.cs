using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x020007EE RID: 2030
	[ComVisible(true)]
	public class InterfaceImplUser : InterfaceImpl
	{
		// Token: 0x0600495A RID: 18778 RVA: 0x00179A18 File Offset: 0x00179A18
		public InterfaceImplUser()
		{
		}

		// Token: 0x0600495B RID: 18779 RVA: 0x00179A20 File Offset: 0x00179A20
		public InterfaceImplUser(ITypeDefOrRef @interface)
		{
			this.@interface = @interface;
		}
	}
}
