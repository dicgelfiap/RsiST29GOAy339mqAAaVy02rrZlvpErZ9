using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.MD
{
	// Token: 0x02000985 RID: 2437
	[ComVisible(true)]
	public enum ColumnSize : byte
	{
		// Token: 0x04002D94 RID: 11668
		Module,
		// Token: 0x04002D95 RID: 11669
		TypeRef,
		// Token: 0x04002D96 RID: 11670
		TypeDef,
		// Token: 0x04002D97 RID: 11671
		FieldPtr,
		// Token: 0x04002D98 RID: 11672
		Field,
		// Token: 0x04002D99 RID: 11673
		MethodPtr,
		// Token: 0x04002D9A RID: 11674
		Method,
		// Token: 0x04002D9B RID: 11675
		ParamPtr,
		// Token: 0x04002D9C RID: 11676
		Param,
		// Token: 0x04002D9D RID: 11677
		InterfaceImpl,
		// Token: 0x04002D9E RID: 11678
		MemberRef,
		// Token: 0x04002D9F RID: 11679
		Constant,
		// Token: 0x04002DA0 RID: 11680
		CustomAttribute,
		// Token: 0x04002DA1 RID: 11681
		FieldMarshal,
		// Token: 0x04002DA2 RID: 11682
		DeclSecurity,
		// Token: 0x04002DA3 RID: 11683
		ClassLayout,
		// Token: 0x04002DA4 RID: 11684
		FieldLayout,
		// Token: 0x04002DA5 RID: 11685
		StandAloneSig,
		// Token: 0x04002DA6 RID: 11686
		EventMap,
		// Token: 0x04002DA7 RID: 11687
		EventPtr,
		// Token: 0x04002DA8 RID: 11688
		Event,
		// Token: 0x04002DA9 RID: 11689
		PropertyMap,
		// Token: 0x04002DAA RID: 11690
		PropertyPtr,
		// Token: 0x04002DAB RID: 11691
		Property,
		// Token: 0x04002DAC RID: 11692
		MethodSemantics,
		// Token: 0x04002DAD RID: 11693
		MethodImpl,
		// Token: 0x04002DAE RID: 11694
		ModuleRef,
		// Token: 0x04002DAF RID: 11695
		TypeSpec,
		// Token: 0x04002DB0 RID: 11696
		ImplMap,
		// Token: 0x04002DB1 RID: 11697
		FieldRVA,
		// Token: 0x04002DB2 RID: 11698
		ENCLog,
		// Token: 0x04002DB3 RID: 11699
		ENCMap,
		// Token: 0x04002DB4 RID: 11700
		Assembly,
		// Token: 0x04002DB5 RID: 11701
		AssemblyProcessor,
		// Token: 0x04002DB6 RID: 11702
		AssemblyOS,
		// Token: 0x04002DB7 RID: 11703
		AssemblyRef,
		// Token: 0x04002DB8 RID: 11704
		AssemblyRefProcessor,
		// Token: 0x04002DB9 RID: 11705
		AssemblyRefOS,
		// Token: 0x04002DBA RID: 11706
		File,
		// Token: 0x04002DBB RID: 11707
		ExportedType,
		// Token: 0x04002DBC RID: 11708
		ManifestResource,
		// Token: 0x04002DBD RID: 11709
		NestedClass,
		// Token: 0x04002DBE RID: 11710
		GenericParam,
		// Token: 0x04002DBF RID: 11711
		MethodSpec,
		// Token: 0x04002DC0 RID: 11712
		GenericParamConstraint,
		// Token: 0x04002DC1 RID: 11713
		Document = 48,
		// Token: 0x04002DC2 RID: 11714
		MethodDebugInformation,
		// Token: 0x04002DC3 RID: 11715
		LocalScope,
		// Token: 0x04002DC4 RID: 11716
		LocalVariable,
		// Token: 0x04002DC5 RID: 11717
		LocalConstant,
		// Token: 0x04002DC6 RID: 11718
		ImportScope,
		// Token: 0x04002DC7 RID: 11719
		StateMachineMethod,
		// Token: 0x04002DC8 RID: 11720
		CustomDebugInformation,
		// Token: 0x04002DC9 RID: 11721
		Byte = 64,
		// Token: 0x04002DCA RID: 11722
		Int16,
		// Token: 0x04002DCB RID: 11723
		UInt16,
		// Token: 0x04002DCC RID: 11724
		Int32,
		// Token: 0x04002DCD RID: 11725
		UInt32,
		// Token: 0x04002DCE RID: 11726
		Strings,
		// Token: 0x04002DCF RID: 11727
		GUID,
		// Token: 0x04002DD0 RID: 11728
		Blob,
		// Token: 0x04002DD1 RID: 11729
		TypeDefOrRef,
		// Token: 0x04002DD2 RID: 11730
		HasConstant,
		// Token: 0x04002DD3 RID: 11731
		HasCustomAttribute,
		// Token: 0x04002DD4 RID: 11732
		HasFieldMarshal,
		// Token: 0x04002DD5 RID: 11733
		HasDeclSecurity,
		// Token: 0x04002DD6 RID: 11734
		MemberRefParent,
		// Token: 0x04002DD7 RID: 11735
		HasSemantic,
		// Token: 0x04002DD8 RID: 11736
		MethodDefOrRef,
		// Token: 0x04002DD9 RID: 11737
		MemberForwarded,
		// Token: 0x04002DDA RID: 11738
		Implementation,
		// Token: 0x04002DDB RID: 11739
		CustomAttributeType,
		// Token: 0x04002DDC RID: 11740
		ResolutionScope,
		// Token: 0x04002DDD RID: 11741
		TypeOrMethodDef,
		// Token: 0x04002DDE RID: 11742
		HasCustomDebugInformation
	}
}
