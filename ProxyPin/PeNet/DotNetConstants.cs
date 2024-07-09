using System;
using System.Runtime.InteropServices;

namespace PeNet
{
	// Token: 0x02000B8C RID: 2956
	[ComVisible(true)]
	public static class DotNetConstants
	{
		// Token: 0x02001156 RID: 4438
		[Flags]
		public enum COMImageFlag : uint
		{
			// Token: 0x04004ABF RID: 19135
			COMIMAGE_FLAGS_ILONLY = 1U,
			// Token: 0x04004AC0 RID: 19136
			COMIMAGE_FLAGS_32BITREQUIRED = 2U,
			// Token: 0x04004AC1 RID: 19137
			COMIMAGE_FLAGS_IL_LIBRARY = 4U,
			// Token: 0x04004AC2 RID: 19138
			COMIMAGE_FLAGS_STRONGNAMESIGNED = 8U,
			// Token: 0x04004AC3 RID: 19139
			COMIMAGE_FLAGS_NATIVE_ENTRYPOINT = 16U,
			// Token: 0x04004AC4 RID: 19140
			COMIMAGE_FLAGS_TRACKDEBUGDATA = 65536U
		}

		// Token: 0x02001157 RID: 4439
		[Flags]
		public enum MaskValidFlags : ulong
		{
			// Token: 0x04004AC6 RID: 19142
			Module = 1UL,
			// Token: 0x04004AC7 RID: 19143
			TypeRef = 2UL,
			// Token: 0x04004AC8 RID: 19144
			TypeDef = 4UL,
			// Token: 0x04004AC9 RID: 19145
			Field = 16UL,
			// Token: 0x04004ACA RID: 19146
			MethodDef = 64UL,
			// Token: 0x04004ACB RID: 19147
			Param = 256UL,
			// Token: 0x04004ACC RID: 19148
			InterfaceImpl = 512UL,
			// Token: 0x04004ACD RID: 19149
			MemberRef = 1024UL,
			// Token: 0x04004ACE RID: 19150
			Constant = 2048UL,
			// Token: 0x04004ACF RID: 19151
			CustomAttribute = 4096UL,
			// Token: 0x04004AD0 RID: 19152
			FieldMarshal = 8192UL,
			// Token: 0x04004AD1 RID: 19153
			DeclSecurity = 16384UL,
			// Token: 0x04004AD2 RID: 19154
			ClassLayout = 32768UL,
			// Token: 0x04004AD3 RID: 19155
			FieldLayout = 65536UL,
			// Token: 0x04004AD4 RID: 19156
			StandAloneSig = 131072UL,
			// Token: 0x04004AD5 RID: 19157
			EventMap = 262144UL,
			// Token: 0x04004AD6 RID: 19158
			Event = 1048576UL,
			// Token: 0x04004AD7 RID: 19159
			PropertyMap = 2097152UL,
			// Token: 0x04004AD8 RID: 19160
			Property = 8388608UL,
			// Token: 0x04004AD9 RID: 19161
			MethodSemantics = 16777216UL,
			// Token: 0x04004ADA RID: 19162
			MethodImpl = 33554432UL,
			// Token: 0x04004ADB RID: 19163
			ModuleRef = 67108864UL,
			// Token: 0x04004ADC RID: 19164
			TypeSpec = 134217728UL,
			// Token: 0x04004ADD RID: 19165
			ImplMap = 268435456UL,
			// Token: 0x04004ADE RID: 19166
			FieldRVA = 536870912UL,
			// Token: 0x04004ADF RID: 19167
			Assembly = 4294967296UL,
			// Token: 0x04004AE0 RID: 19168
			AssemblyProcessor = 8589934592UL,
			// Token: 0x04004AE1 RID: 19169
			AssemblyOS = 17179869184UL,
			// Token: 0x04004AE2 RID: 19170
			AssemblyRef = 34359738368UL,
			// Token: 0x04004AE3 RID: 19171
			AssemblyRefProcessor = 68719476736UL,
			// Token: 0x04004AE4 RID: 19172
			AssemblyRefOS = 137438953472UL,
			// Token: 0x04004AE5 RID: 19173
			File = 274877906944UL,
			// Token: 0x04004AE6 RID: 19174
			ExportedType = 549755813888UL,
			// Token: 0x04004AE7 RID: 19175
			ManifestResource = 1099511627776UL,
			// Token: 0x04004AE8 RID: 19176
			NestedClass = 2199023255552UL,
			// Token: 0x04004AE9 RID: 19177
			GenericParam = 4398046511104UL,
			// Token: 0x04004AEA RID: 19178
			MethodSpec = 8796093022208UL,
			// Token: 0x04004AEB RID: 19179
			GenericParamConstraint = 17592186044416UL
		}
	}
}
