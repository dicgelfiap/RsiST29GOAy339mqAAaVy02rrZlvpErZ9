using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x0200079F RID: 1951
	[ComVisible(true)]
	public class DeclSecurityUser : DeclSecurity
	{
		// Token: 0x060045D6 RID: 17878 RVA: 0x0016EFD8 File Offset: 0x0016EFD8
		public DeclSecurityUser()
		{
		}

		// Token: 0x060045D7 RID: 17879 RVA: 0x0016EFE0 File Offset: 0x0016EFE0
		public DeclSecurityUser(SecurityAction action, IList<SecurityAttribute> securityAttrs)
		{
			this.action = action;
			this.securityAttributes = securityAttrs;
		}

		// Token: 0x060045D8 RID: 17880 RVA: 0x0016EFF8 File Offset: 0x0016EFF8
		public override byte[] GetBlob()
		{
			return null;
		}
	}
}
