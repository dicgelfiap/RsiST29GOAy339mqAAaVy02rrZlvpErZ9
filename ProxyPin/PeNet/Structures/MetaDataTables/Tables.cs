using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace PeNet.Structures.MetaDataTables
{
	// Token: 0x02000BEA RID: 3050
	[ComVisible(true)]
	public class Tables
	{
		// Token: 0x17001A8B RID: 6795
		// (get) Token: 0x06007A3A RID: 31290 RVA: 0x00240D78 File Offset: 0x00240D78
		// (set) Token: 0x06007A3B RID: 31291 RVA: 0x00240D80 File Offset: 0x00240D80
		public List<Module> Module { get; set; }

		// Token: 0x17001A8C RID: 6796
		// (get) Token: 0x06007A3C RID: 31292 RVA: 0x00240D8C File Offset: 0x00240D8C
		// (set) Token: 0x06007A3D RID: 31293 RVA: 0x00240D94 File Offset: 0x00240D94
		public List<TypeRef> TypeRef { get; set; }

		// Token: 0x17001A8D RID: 6797
		// (get) Token: 0x06007A3E RID: 31294 RVA: 0x00240DA0 File Offset: 0x00240DA0
		// (set) Token: 0x06007A3F RID: 31295 RVA: 0x00240DA8 File Offset: 0x00240DA8
		public List<TypeDef> TypeDef { get; set; }

		// Token: 0x17001A8E RID: 6798
		// (get) Token: 0x06007A40 RID: 31296 RVA: 0x00240DB4 File Offset: 0x00240DB4
		// (set) Token: 0x06007A41 RID: 31297 RVA: 0x00240DBC File Offset: 0x00240DBC
		public List<Field> Field { get; set; }

		// Token: 0x17001A8F RID: 6799
		// (get) Token: 0x06007A42 RID: 31298 RVA: 0x00240DC8 File Offset: 0x00240DC8
		// (set) Token: 0x06007A43 RID: 31299 RVA: 0x00240DD0 File Offset: 0x00240DD0
		public List<MethodDef> MethodDef { get; set; }

		// Token: 0x17001A90 RID: 6800
		// (get) Token: 0x06007A44 RID: 31300 RVA: 0x00240DDC File Offset: 0x00240DDC
		// (set) Token: 0x06007A45 RID: 31301 RVA: 0x00240DE4 File Offset: 0x00240DE4
		public List<Param> Param { get; set; }

		// Token: 0x17001A91 RID: 6801
		// (get) Token: 0x06007A46 RID: 31302 RVA: 0x00240DF0 File Offset: 0x00240DF0
		// (set) Token: 0x06007A47 RID: 31303 RVA: 0x00240DF8 File Offset: 0x00240DF8
		public List<InterfaceImpl> InterfaceImpl { get; set; }

		// Token: 0x17001A92 RID: 6802
		// (get) Token: 0x06007A48 RID: 31304 RVA: 0x00240E04 File Offset: 0x00240E04
		// (set) Token: 0x06007A49 RID: 31305 RVA: 0x00240E0C File Offset: 0x00240E0C
		public List<MemberRef> MemberRef { get; set; }

		// Token: 0x17001A93 RID: 6803
		// (get) Token: 0x06007A4A RID: 31306 RVA: 0x00240E18 File Offset: 0x00240E18
		// (set) Token: 0x06007A4B RID: 31307 RVA: 0x00240E20 File Offset: 0x00240E20
		public List<Constant> Constant { get; set; }

		// Token: 0x17001A94 RID: 6804
		// (get) Token: 0x06007A4C RID: 31308 RVA: 0x00240E2C File Offset: 0x00240E2C
		// (set) Token: 0x06007A4D RID: 31309 RVA: 0x00240E34 File Offset: 0x00240E34
		public List<CustomAttribute> CustomAttribute { get; set; }

		// Token: 0x17001A95 RID: 6805
		// (get) Token: 0x06007A4E RID: 31310 RVA: 0x00240E40 File Offset: 0x00240E40
		// (set) Token: 0x06007A4F RID: 31311 RVA: 0x00240E48 File Offset: 0x00240E48
		public List<FieldMarshal> FieldMarshal { get; set; }

		// Token: 0x17001A96 RID: 6806
		// (get) Token: 0x06007A50 RID: 31312 RVA: 0x00240E54 File Offset: 0x00240E54
		// (set) Token: 0x06007A51 RID: 31313 RVA: 0x00240E5C File Offset: 0x00240E5C
		public List<DeclSecurity> DeclSecurity { get; set; }

		// Token: 0x17001A97 RID: 6807
		// (get) Token: 0x06007A52 RID: 31314 RVA: 0x00240E68 File Offset: 0x00240E68
		// (set) Token: 0x06007A53 RID: 31315 RVA: 0x00240E70 File Offset: 0x00240E70
		public List<ClassLayout> ClassLayout { get; set; }

		// Token: 0x17001A98 RID: 6808
		// (get) Token: 0x06007A54 RID: 31316 RVA: 0x00240E7C File Offset: 0x00240E7C
		// (set) Token: 0x06007A55 RID: 31317 RVA: 0x00240E84 File Offset: 0x00240E84
		public List<FieldLayout> FieldLayout { get; set; }

		// Token: 0x17001A99 RID: 6809
		// (get) Token: 0x06007A56 RID: 31318 RVA: 0x00240E90 File Offset: 0x00240E90
		// (set) Token: 0x06007A57 RID: 31319 RVA: 0x00240E98 File Offset: 0x00240E98
		public List<StandAloneSig> StandAloneSig { get; set; }

		// Token: 0x17001A9A RID: 6810
		// (get) Token: 0x06007A58 RID: 31320 RVA: 0x00240EA4 File Offset: 0x00240EA4
		// (set) Token: 0x06007A59 RID: 31321 RVA: 0x00240EAC File Offset: 0x00240EAC
		public List<EventMap> EventMap { get; set; }

		// Token: 0x17001A9B RID: 6811
		// (get) Token: 0x06007A5A RID: 31322 RVA: 0x00240EB8 File Offset: 0x00240EB8
		// (set) Token: 0x06007A5B RID: 31323 RVA: 0x00240EC0 File Offset: 0x00240EC0
		public List<Event> Event { get; set; }

		// Token: 0x17001A9C RID: 6812
		// (get) Token: 0x06007A5C RID: 31324 RVA: 0x00240ECC File Offset: 0x00240ECC
		// (set) Token: 0x06007A5D RID: 31325 RVA: 0x00240ED4 File Offset: 0x00240ED4
		public List<PropertyMap> PropertyMap { get; set; }

		// Token: 0x17001A9D RID: 6813
		// (get) Token: 0x06007A5E RID: 31326 RVA: 0x00240EE0 File Offset: 0x00240EE0
		// (set) Token: 0x06007A5F RID: 31327 RVA: 0x00240EE8 File Offset: 0x00240EE8
		public List<Property> Property { get; set; }

		// Token: 0x17001A9E RID: 6814
		// (get) Token: 0x06007A60 RID: 31328 RVA: 0x00240EF4 File Offset: 0x00240EF4
		// (set) Token: 0x06007A61 RID: 31329 RVA: 0x00240EFC File Offset: 0x00240EFC
		public List<MethodSemantics> MethodSemantic { get; set; }

		// Token: 0x17001A9F RID: 6815
		// (get) Token: 0x06007A62 RID: 31330 RVA: 0x00240F08 File Offset: 0x00240F08
		// (set) Token: 0x06007A63 RID: 31331 RVA: 0x00240F10 File Offset: 0x00240F10
		public List<MethodImpl> MethodImpl { get; set; }

		// Token: 0x17001AA0 RID: 6816
		// (get) Token: 0x06007A64 RID: 31332 RVA: 0x00240F1C File Offset: 0x00240F1C
		// (set) Token: 0x06007A65 RID: 31333 RVA: 0x00240F24 File Offset: 0x00240F24
		public List<ModuleRef> ModuleRef { get; set; }

		// Token: 0x17001AA1 RID: 6817
		// (get) Token: 0x06007A66 RID: 31334 RVA: 0x00240F30 File Offset: 0x00240F30
		// (set) Token: 0x06007A67 RID: 31335 RVA: 0x00240F38 File Offset: 0x00240F38
		public List<TypeSpec> TypeSpec { get; set; }

		// Token: 0x17001AA2 RID: 6818
		// (get) Token: 0x06007A68 RID: 31336 RVA: 0x00240F44 File Offset: 0x00240F44
		// (set) Token: 0x06007A69 RID: 31337 RVA: 0x00240F4C File Offset: 0x00240F4C
		public List<ImplMap> ImplMap { get; set; }

		// Token: 0x17001AA3 RID: 6819
		// (get) Token: 0x06007A6A RID: 31338 RVA: 0x00240F58 File Offset: 0x00240F58
		// (set) Token: 0x06007A6B RID: 31339 RVA: 0x00240F60 File Offset: 0x00240F60
		public List<FieldRVA> FieldRVA { get; set; }

		// Token: 0x17001AA4 RID: 6820
		// (get) Token: 0x06007A6C RID: 31340 RVA: 0x00240F6C File Offset: 0x00240F6C
		// (set) Token: 0x06007A6D RID: 31341 RVA: 0x00240F74 File Offset: 0x00240F74
		public List<Assembly> Assembly { get; set; }

		// Token: 0x17001AA5 RID: 6821
		// (get) Token: 0x06007A6E RID: 31342 RVA: 0x00240F80 File Offset: 0x00240F80
		// (set) Token: 0x06007A6F RID: 31343 RVA: 0x00240F88 File Offset: 0x00240F88
		public List<AssemblyProcessor> AssemblyProcessor { get; set; }

		// Token: 0x17001AA6 RID: 6822
		// (get) Token: 0x06007A70 RID: 31344 RVA: 0x00240F94 File Offset: 0x00240F94
		// (set) Token: 0x06007A71 RID: 31345 RVA: 0x00240F9C File Offset: 0x00240F9C
		public List<AssemblyOS> AssemblyOS { get; set; }

		// Token: 0x17001AA7 RID: 6823
		// (get) Token: 0x06007A72 RID: 31346 RVA: 0x00240FA8 File Offset: 0x00240FA8
		// (set) Token: 0x06007A73 RID: 31347 RVA: 0x00240FB0 File Offset: 0x00240FB0
		public List<AssemblyRef> AssemblyRef { get; set; }

		// Token: 0x17001AA8 RID: 6824
		// (get) Token: 0x06007A74 RID: 31348 RVA: 0x00240FBC File Offset: 0x00240FBC
		// (set) Token: 0x06007A75 RID: 31349 RVA: 0x00240FC4 File Offset: 0x00240FC4
		public List<AssemblyRefProcessor> AssemblyRefProcessor { get; set; }

		// Token: 0x17001AA9 RID: 6825
		// (get) Token: 0x06007A76 RID: 31350 RVA: 0x00240FD0 File Offset: 0x00240FD0
		// (set) Token: 0x06007A77 RID: 31351 RVA: 0x00240FD8 File Offset: 0x00240FD8
		public List<AssemblyRefOS> AssemblyRefOS { get; set; }

		// Token: 0x17001AAA RID: 6826
		// (get) Token: 0x06007A78 RID: 31352 RVA: 0x00240FE4 File Offset: 0x00240FE4
		// (set) Token: 0x06007A79 RID: 31353 RVA: 0x00240FEC File Offset: 0x00240FEC
		public List<File> File { get; set; }

		// Token: 0x17001AAB RID: 6827
		// (get) Token: 0x06007A7A RID: 31354 RVA: 0x00240FF8 File Offset: 0x00240FF8
		// (set) Token: 0x06007A7B RID: 31355 RVA: 0x00241000 File Offset: 0x00241000
		public List<ExportedType> ExportedType { get; set; }

		// Token: 0x17001AAC RID: 6828
		// (get) Token: 0x06007A7C RID: 31356 RVA: 0x0024100C File Offset: 0x0024100C
		// (set) Token: 0x06007A7D RID: 31357 RVA: 0x00241014 File Offset: 0x00241014
		public List<ManifestResource> ManifestResource { get; set; }

		// Token: 0x17001AAD RID: 6829
		// (get) Token: 0x06007A7E RID: 31358 RVA: 0x00241020 File Offset: 0x00241020
		// (set) Token: 0x06007A7F RID: 31359 RVA: 0x00241028 File Offset: 0x00241028
		public List<NestedClass> NestedClass { get; set; }

		// Token: 0x17001AAE RID: 6830
		// (get) Token: 0x06007A80 RID: 31360 RVA: 0x00241034 File Offset: 0x00241034
		// (set) Token: 0x06007A81 RID: 31361 RVA: 0x0024103C File Offset: 0x0024103C
		public List<GenericParam> GenericParam { get; set; }

		// Token: 0x17001AAF RID: 6831
		// (get) Token: 0x06007A82 RID: 31362 RVA: 0x00241048 File Offset: 0x00241048
		// (set) Token: 0x06007A83 RID: 31363 RVA: 0x00241050 File Offset: 0x00241050
		public List<GenericParamConstraint> GenericParamConstraints { get; set; }
	}
}
