using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.Pdb
{
	// Token: 0x020008F8 RID: 2296
	[ComVisible(true)]
	public sealed class PdbForwardMethodInfoCustomDebugInfo : PdbCustomDebugInfo
	{
		// Token: 0x17001276 RID: 4726
		// (get) Token: 0x0600591D RID: 22813 RVA: 0x001B58F8 File Offset: 0x001B58F8
		public override PdbCustomDebugInfoKind Kind
		{
			get
			{
				return PdbCustomDebugInfoKind.ForwardMethodInfo;
			}
		}

		// Token: 0x17001277 RID: 4727
		// (get) Token: 0x0600591E RID: 22814 RVA: 0x001B58FC File Offset: 0x001B58FC
		public override Guid Guid
		{
			get
			{
				return Guid.Empty;
			}
		}

		// Token: 0x17001278 RID: 4728
		// (get) Token: 0x0600591F RID: 22815 RVA: 0x001B5904 File Offset: 0x001B5904
		// (set) Token: 0x06005920 RID: 22816 RVA: 0x001B590C File Offset: 0x001B590C
		public IMethodDefOrRef Method
		{
			get
			{
				return this.method;
			}
			set
			{
				this.method = value;
			}
		}

		// Token: 0x06005921 RID: 22817 RVA: 0x001B5918 File Offset: 0x001B5918
		public PdbForwardMethodInfoCustomDebugInfo()
		{
		}

		// Token: 0x06005922 RID: 22818 RVA: 0x001B5920 File Offset: 0x001B5920
		public PdbForwardMethodInfoCustomDebugInfo(IMethodDefOrRef method)
		{
			this.method = method;
		}

		// Token: 0x04002B3C RID: 11068
		private IMethodDefOrRef method;
	}
}
