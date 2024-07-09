using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.Pdb
{
	// Token: 0x020008F7 RID: 2295
	[ComVisible(true)]
	public sealed class PdbUsingGroupsCustomDebugInfo : PdbCustomDebugInfo
	{
		// Token: 0x17001273 RID: 4723
		// (get) Token: 0x06005918 RID: 22808 RVA: 0x001B58BC File Offset: 0x001B58BC
		public override PdbCustomDebugInfoKind Kind
		{
			get
			{
				return PdbCustomDebugInfoKind.UsingGroups;
			}
		}

		// Token: 0x17001274 RID: 4724
		// (get) Token: 0x06005919 RID: 22809 RVA: 0x001B58C0 File Offset: 0x001B58C0
		public override Guid Guid
		{
			get
			{
				return Guid.Empty;
			}
		}

		// Token: 0x17001275 RID: 4725
		// (get) Token: 0x0600591A RID: 22810 RVA: 0x001B58C8 File Offset: 0x001B58C8
		public IList<ushort> UsingCounts
		{
			get
			{
				return this.usingCounts;
			}
		}

		// Token: 0x0600591B RID: 22811 RVA: 0x001B58D0 File Offset: 0x001B58D0
		public PdbUsingGroupsCustomDebugInfo()
		{
			this.usingCounts = new List<ushort>();
		}

		// Token: 0x0600591C RID: 22812 RVA: 0x001B58E4 File Offset: 0x001B58E4
		public PdbUsingGroupsCustomDebugInfo(int capacity)
		{
			this.usingCounts = new List<ushort>(capacity);
		}

		// Token: 0x04002B3B RID: 11067
		private readonly IList<ushort> usingCounts;
	}
}
