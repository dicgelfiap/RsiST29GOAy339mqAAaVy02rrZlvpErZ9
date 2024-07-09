using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x02000814 RID: 2068
	[ComVisible(true)]
	public struct MethodOverride
	{
		// Token: 0x06004B75 RID: 19317 RVA: 0x0017E408 File Offset: 0x0017E408
		public MethodOverride(IMethodDefOrRef methodBody, IMethodDefOrRef methodDeclaration)
		{
			this.MethodBody = methodBody;
			this.MethodDeclaration = methodDeclaration;
		}

		// Token: 0x040025C2 RID: 9666
		public IMethodDefOrRef MethodBody;

		// Token: 0x040025C3 RID: 9667
		public IMethodDefOrRef MethodDeclaration;
	}
}
