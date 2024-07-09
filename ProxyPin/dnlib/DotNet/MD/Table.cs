using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.MD
{
	// Token: 0x020009D4 RID: 2516
	[ComVisible(true)]
	public enum Table : byte
	{
		// Token: 0x04002EFE RID: 12030
		Module,
		// Token: 0x04002EFF RID: 12031
		TypeRef,
		// Token: 0x04002F00 RID: 12032
		TypeDef,
		// Token: 0x04002F01 RID: 12033
		FieldPtr,
		// Token: 0x04002F02 RID: 12034
		Field,
		// Token: 0x04002F03 RID: 12035
		MethodPtr,
		// Token: 0x04002F04 RID: 12036
		Method,
		// Token: 0x04002F05 RID: 12037
		ParamPtr,
		// Token: 0x04002F06 RID: 12038
		Param,
		// Token: 0x04002F07 RID: 12039
		InterfaceImpl,
		// Token: 0x04002F08 RID: 12040
		MemberRef,
		// Token: 0x04002F09 RID: 12041
		Constant,
		// Token: 0x04002F0A RID: 12042
		CustomAttribute,
		// Token: 0x04002F0B RID: 12043
		FieldMarshal,
		// Token: 0x04002F0C RID: 12044
		DeclSecurity,
		// Token: 0x04002F0D RID: 12045
		ClassLayout,
		// Token: 0x04002F0E RID: 12046
		FieldLayout,
		// Token: 0x04002F0F RID: 12047
		StandAloneSig,
		// Token: 0x04002F10 RID: 12048
		EventMap,
		// Token: 0x04002F11 RID: 12049
		EventPtr,
		// Token: 0x04002F12 RID: 12050
		Event,
		// Token: 0x04002F13 RID: 12051
		PropertyMap,
		// Token: 0x04002F14 RID: 12052
		PropertyPtr,
		// Token: 0x04002F15 RID: 12053
		Property,
		// Token: 0x04002F16 RID: 12054
		MethodSemantics,
		// Token: 0x04002F17 RID: 12055
		MethodImpl,
		// Token: 0x04002F18 RID: 12056
		ModuleRef,
		// Token: 0x04002F19 RID: 12057
		TypeSpec,
		// Token: 0x04002F1A RID: 12058
		ImplMap,
		// Token: 0x04002F1B RID: 12059
		FieldRVA,
		// Token: 0x04002F1C RID: 12060
		ENCLog,
		// Token: 0x04002F1D RID: 12061
		ENCMap,
		// Token: 0x04002F1E RID: 12062
		Assembly,
		// Token: 0x04002F1F RID: 12063
		AssemblyProcessor,
		// Token: 0x04002F20 RID: 12064
		AssemblyOS,
		// Token: 0x04002F21 RID: 12065
		AssemblyRef,
		// Token: 0x04002F22 RID: 12066
		AssemblyRefProcessor,
		// Token: 0x04002F23 RID: 12067
		AssemblyRefOS,
		// Token: 0x04002F24 RID: 12068
		File,
		// Token: 0x04002F25 RID: 12069
		ExportedType,
		// Token: 0x04002F26 RID: 12070
		ManifestResource,
		// Token: 0x04002F27 RID: 12071
		NestedClass,
		// Token: 0x04002F28 RID: 12072
		GenericParam,
		// Token: 0x04002F29 RID: 12073
		MethodSpec,
		// Token: 0x04002F2A RID: 12074
		GenericParamConstraint,
		// Token: 0x04002F2B RID: 12075
		Document = 48,
		// Token: 0x04002F2C RID: 12076
		MethodDebugInformation,
		// Token: 0x04002F2D RID: 12077
		LocalScope,
		// Token: 0x04002F2E RID: 12078
		LocalVariable,
		// Token: 0x04002F2F RID: 12079
		LocalConstant,
		// Token: 0x04002F30 RID: 12080
		ImportScope,
		// Token: 0x04002F31 RID: 12081
		StateMachineMethod,
		// Token: 0x04002F32 RID: 12082
		CustomDebugInformation
	}
}
