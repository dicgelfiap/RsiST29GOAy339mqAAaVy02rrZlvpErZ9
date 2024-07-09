using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x020007BC RID: 1980
	[ComVisible(true)]
	public class GenericParamConstraintUser : GenericParamConstraint
	{
		// Token: 0x06004846 RID: 18502 RVA: 0x00176FA8 File Offset: 0x00176FA8
		public GenericParamConstraintUser()
		{
		}

		// Token: 0x06004847 RID: 18503 RVA: 0x00176FB0 File Offset: 0x00176FB0
		public GenericParamConstraintUser(ITypeDefOrRef constraint)
		{
			this.constraint = constraint;
		}
	}
}
