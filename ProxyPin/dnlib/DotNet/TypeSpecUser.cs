using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x02000880 RID: 2176
	[ComVisible(true)]
	public class TypeSpecUser : TypeSpec
	{
		// Token: 0x06005331 RID: 21297 RVA: 0x00196A40 File Offset: 0x00196A40
		public TypeSpecUser()
		{
		}

		// Token: 0x06005332 RID: 21298 RVA: 0x00196A48 File Offset: 0x00196A48
		public TypeSpecUser(TypeSig typeSig)
		{
			this.typeSig = typeSig;
			this.extraData = null;
			this.typeSigAndExtraData_isInitialized = true;
		}
	}
}
