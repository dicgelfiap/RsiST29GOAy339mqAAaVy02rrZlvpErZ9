using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.Pdb
{
	// Token: 0x020008FD RID: 2301
	[ComVisible(true)]
	public sealed class PdbDynamicLocalsCustomDebugInfo : PdbCustomDebugInfo
	{
		// Token: 0x17001283 RID: 4739
		// (get) Token: 0x06005936 RID: 22838 RVA: 0x001B5A04 File Offset: 0x001B5A04
		public override PdbCustomDebugInfoKind Kind
		{
			get
			{
				return PdbCustomDebugInfoKind.DynamicLocals;
			}
		}

		// Token: 0x17001284 RID: 4740
		// (get) Token: 0x06005937 RID: 22839 RVA: 0x001B5A08 File Offset: 0x001B5A08
		public override Guid Guid
		{
			get
			{
				return Guid.Empty;
			}
		}

		// Token: 0x17001285 RID: 4741
		// (get) Token: 0x06005938 RID: 22840 RVA: 0x001B5A10 File Offset: 0x001B5A10
		public IList<PdbDynamicLocal> Locals
		{
			get
			{
				return this.locals;
			}
		}

		// Token: 0x06005939 RID: 22841 RVA: 0x001B5A18 File Offset: 0x001B5A18
		public PdbDynamicLocalsCustomDebugInfo()
		{
			this.locals = new List<PdbDynamicLocal>();
		}

		// Token: 0x0600593A RID: 22842 RVA: 0x001B5A2C File Offset: 0x001B5A2C
		public PdbDynamicLocalsCustomDebugInfo(int capacity)
		{
			this.locals = new List<PdbDynamicLocal>(capacity);
		}

		// Token: 0x04002B42 RID: 11074
		private readonly IList<PdbDynamicLocal> locals;
	}
}
