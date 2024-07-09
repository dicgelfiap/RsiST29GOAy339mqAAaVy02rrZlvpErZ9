using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x02000817 RID: 2071
	[ComVisible(true)]
	public class MethodSpecUser : MethodSpec
	{
		// Token: 0x06004B9F RID: 19359 RVA: 0x0017E71C File Offset: 0x0017E71C
		public MethodSpecUser()
		{
		}

		// Token: 0x06004BA0 RID: 19360 RVA: 0x0017E724 File Offset: 0x0017E724
		public MethodSpecUser(IMethodDefOrRef method) : this(method, null)
		{
		}

		// Token: 0x06004BA1 RID: 19361 RVA: 0x0017E730 File Offset: 0x0017E730
		public MethodSpecUser(IMethodDefOrRef method, GenericInstMethodSig sig)
		{
			this.method = method;
			this.instantiation = sig;
		}
	}
}
