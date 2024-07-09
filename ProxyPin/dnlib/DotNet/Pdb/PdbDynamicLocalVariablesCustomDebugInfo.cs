using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.Pdb
{
	// Token: 0x02000906 RID: 2310
	[ComVisible(true)]
	public sealed class PdbDynamicLocalVariablesCustomDebugInfo : PdbCustomDebugInfo
	{
		// Token: 0x170012A5 RID: 4773
		// (get) Token: 0x0600596F RID: 22895 RVA: 0x001B5D10 File Offset: 0x001B5D10
		public override PdbCustomDebugInfoKind Kind
		{
			get
			{
				return PdbCustomDebugInfoKind.DynamicLocalVariables;
			}
		}

		// Token: 0x170012A6 RID: 4774
		// (get) Token: 0x06005970 RID: 22896 RVA: 0x001B5D18 File Offset: 0x001B5D18
		public override Guid Guid
		{
			get
			{
				return CustomDebugInfoGuids.DynamicLocalVariables;
			}
		}

		// Token: 0x170012A7 RID: 4775
		// (get) Token: 0x06005971 RID: 22897 RVA: 0x001B5D20 File Offset: 0x001B5D20
		// (set) Token: 0x06005972 RID: 22898 RVA: 0x001B5D28 File Offset: 0x001B5D28
		public bool[] Flags { get; set; }

		// Token: 0x06005973 RID: 22899 RVA: 0x001B5D34 File Offset: 0x001B5D34
		public PdbDynamicLocalVariablesCustomDebugInfo()
		{
		}

		// Token: 0x06005974 RID: 22900 RVA: 0x001B5D3C File Offset: 0x001B5D3C
		public PdbDynamicLocalVariablesCustomDebugInfo(bool[] flags)
		{
			this.Flags = flags;
		}
	}
}
