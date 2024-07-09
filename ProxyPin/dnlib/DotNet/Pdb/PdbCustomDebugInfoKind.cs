using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.Pdb
{
	// Token: 0x020008F4 RID: 2292
	[ComVisible(true)]
	public enum PdbCustomDebugInfoKind
	{
		// Token: 0x04002B26 RID: 11046
		UsingGroups,
		// Token: 0x04002B27 RID: 11047
		ForwardMethodInfo,
		// Token: 0x04002B28 RID: 11048
		ForwardModuleInfo,
		// Token: 0x04002B29 RID: 11049
		StateMachineHoistedLocalScopes,
		// Token: 0x04002B2A RID: 11050
		StateMachineTypeName,
		// Token: 0x04002B2B RID: 11051
		DynamicLocals,
		// Token: 0x04002B2C RID: 11052
		EditAndContinueLocalSlotMap,
		// Token: 0x04002B2D RID: 11053
		EditAndContinueLambdaMap,
		// Token: 0x04002B2E RID: 11054
		TupleElementNames,
		// Token: 0x04002B2F RID: 11055
		Unknown = -2147483648,
		// Token: 0x04002B30 RID: 11056
		TupleElementNames_PortablePdb,
		// Token: 0x04002B31 RID: 11057
		DefaultNamespace,
		// Token: 0x04002B32 RID: 11058
		DynamicLocalVariables,
		// Token: 0x04002B33 RID: 11059
		EmbeddedSource,
		// Token: 0x04002B34 RID: 11060
		SourceLink,
		// Token: 0x04002B35 RID: 11061
		SourceServer,
		// Token: 0x04002B36 RID: 11062
		AsyncMethod,
		// Token: 0x04002B37 RID: 11063
		IteratorMethod
	}
}
