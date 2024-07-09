using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.Pdb
{
	// Token: 0x020008F9 RID: 2297
	[ComVisible(true)]
	public sealed class PdbForwardModuleInfoCustomDebugInfo : PdbCustomDebugInfo
	{
		// Token: 0x17001279 RID: 4729
		// (get) Token: 0x06005923 RID: 22819 RVA: 0x001B5930 File Offset: 0x001B5930
		public override PdbCustomDebugInfoKind Kind
		{
			get
			{
				return PdbCustomDebugInfoKind.ForwardModuleInfo;
			}
		}

		// Token: 0x1700127A RID: 4730
		// (get) Token: 0x06005924 RID: 22820 RVA: 0x001B5934 File Offset: 0x001B5934
		public override Guid Guid
		{
			get
			{
				return Guid.Empty;
			}
		}

		// Token: 0x1700127B RID: 4731
		// (get) Token: 0x06005925 RID: 22821 RVA: 0x001B593C File Offset: 0x001B593C
		// (set) Token: 0x06005926 RID: 22822 RVA: 0x001B5944 File Offset: 0x001B5944
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

		// Token: 0x06005927 RID: 22823 RVA: 0x001B5950 File Offset: 0x001B5950
		public PdbForwardModuleInfoCustomDebugInfo()
		{
		}

		// Token: 0x06005928 RID: 22824 RVA: 0x001B5958 File Offset: 0x001B5958
		public PdbForwardModuleInfoCustomDebugInfo(IMethodDefOrRef method)
		{
			this.method = method;
		}

		// Token: 0x04002B3D RID: 11069
		private IMethodDefOrRef method;
	}
}
