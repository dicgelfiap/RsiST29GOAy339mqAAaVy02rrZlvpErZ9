using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.Pdb
{
	// Token: 0x020008FB RID: 2299
	[ComVisible(true)]
	public sealed class PdbStateMachineHoistedLocalScopesCustomDebugInfo : PdbCustomDebugInfo
	{
		// Token: 0x1700127D RID: 4733
		// (get) Token: 0x0600592B RID: 22827 RVA: 0x001B5990 File Offset: 0x001B5990
		public override PdbCustomDebugInfoKind Kind
		{
			get
			{
				return PdbCustomDebugInfoKind.StateMachineHoistedLocalScopes;
			}
		}

		// Token: 0x1700127E RID: 4734
		// (get) Token: 0x0600592C RID: 22828 RVA: 0x001B5994 File Offset: 0x001B5994
		public override Guid Guid
		{
			get
			{
				return CustomDebugInfoGuids.StateMachineHoistedLocalScopes;
			}
		}

		// Token: 0x1700127F RID: 4735
		// (get) Token: 0x0600592D RID: 22829 RVA: 0x001B599C File Offset: 0x001B599C
		public IList<StateMachineHoistedLocalScope> Scopes
		{
			get
			{
				return this.scopes;
			}
		}

		// Token: 0x0600592E RID: 22830 RVA: 0x001B59A4 File Offset: 0x001B59A4
		public PdbStateMachineHoistedLocalScopesCustomDebugInfo()
		{
			this.scopes = new List<StateMachineHoistedLocalScope>();
		}

		// Token: 0x0600592F RID: 22831 RVA: 0x001B59B8 File Offset: 0x001B59B8
		public PdbStateMachineHoistedLocalScopesCustomDebugInfo(int capacity)
		{
			this.scopes = new List<StateMachineHoistedLocalScope>(capacity);
		}

		// Token: 0x04002B40 RID: 11072
		private readonly IList<StateMachineHoistedLocalScope> scopes;
	}
}
