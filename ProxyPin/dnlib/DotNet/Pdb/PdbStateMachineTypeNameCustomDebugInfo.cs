using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.Pdb
{
	// Token: 0x020008FC RID: 2300
	[ComVisible(true)]
	public sealed class PdbStateMachineTypeNameCustomDebugInfo : PdbCustomDebugInfo
	{
		// Token: 0x17001280 RID: 4736
		// (get) Token: 0x06005930 RID: 22832 RVA: 0x001B59CC File Offset: 0x001B59CC
		public override PdbCustomDebugInfoKind Kind
		{
			get
			{
				return PdbCustomDebugInfoKind.StateMachineTypeName;
			}
		}

		// Token: 0x17001281 RID: 4737
		// (get) Token: 0x06005931 RID: 22833 RVA: 0x001B59D0 File Offset: 0x001B59D0
		public override Guid Guid
		{
			get
			{
				return Guid.Empty;
			}
		}

		// Token: 0x17001282 RID: 4738
		// (get) Token: 0x06005932 RID: 22834 RVA: 0x001B59D8 File Offset: 0x001B59D8
		// (set) Token: 0x06005933 RID: 22835 RVA: 0x001B59E0 File Offset: 0x001B59E0
		public TypeDef Type { get; set; }

		// Token: 0x06005934 RID: 22836 RVA: 0x001B59EC File Offset: 0x001B59EC
		public PdbStateMachineTypeNameCustomDebugInfo()
		{
		}

		// Token: 0x06005935 RID: 22837 RVA: 0x001B59F4 File Offset: 0x001B59F4
		public PdbStateMachineTypeNameCustomDebugInfo(TypeDef type)
		{
			this.Type = type;
		}
	}
}
