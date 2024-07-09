using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using dnlib.PE;

namespace dnlib.DotNet.MD
{
	// Token: 0x02000995 RID: 2453
	[ComVisible(true)]
	public abstract class Metadata : IDisposable
	{
		// Token: 0x170013B4 RID: 5044
		// (get) Token: 0x06005E4E RID: 24142
		public abstract bool IsCompressed { get; }

		// Token: 0x170013B5 RID: 5045
		// (get) Token: 0x06005E4F RID: 24143
		public abstract bool IsStandalonePortablePdb { get; }

		// Token: 0x170013B6 RID: 5046
		// (get) Token: 0x06005E50 RID: 24144
		public abstract ImageCor20Header ImageCor20Header { get; }

		// Token: 0x170013B7 RID: 5047
		// (get) Token: 0x06005E51 RID: 24145
		public abstract uint Version { get; }

		// Token: 0x170013B8 RID: 5048
		// (get) Token: 0x06005E52 RID: 24146
		public abstract string VersionString { get; }

		// Token: 0x170013B9 RID: 5049
		// (get) Token: 0x06005E53 RID: 24147
		public abstract IPEImage PEImage { get; }

		// Token: 0x170013BA RID: 5050
		// (get) Token: 0x06005E54 RID: 24148
		public abstract MetadataHeader MetadataHeader { get; }

		// Token: 0x170013BB RID: 5051
		// (get) Token: 0x06005E55 RID: 24149
		public abstract StringsStream StringsStream { get; }

		// Token: 0x170013BC RID: 5052
		// (get) Token: 0x06005E56 RID: 24150
		public abstract USStream USStream { get; }

		// Token: 0x170013BD RID: 5053
		// (get) Token: 0x06005E57 RID: 24151
		public abstract BlobStream BlobStream { get; }

		// Token: 0x170013BE RID: 5054
		// (get) Token: 0x06005E58 RID: 24152
		public abstract GuidStream GuidStream { get; }

		// Token: 0x170013BF RID: 5055
		// (get) Token: 0x06005E59 RID: 24153
		public abstract TablesStream TablesStream { get; }

		// Token: 0x170013C0 RID: 5056
		// (get) Token: 0x06005E5A RID: 24154
		public abstract PdbStream PdbStream { get; }

		// Token: 0x170013C1 RID: 5057
		// (get) Token: 0x06005E5B RID: 24155
		public abstract IList<DotNetStream> AllStreams { get; }

		// Token: 0x06005E5C RID: 24156
		public abstract RidList GetTypeDefRidList();

		// Token: 0x06005E5D RID: 24157
		public abstract RidList GetExportedTypeRidList();

		// Token: 0x06005E5E RID: 24158
		public abstract RidList GetFieldRidList(uint typeDefRid);

		// Token: 0x06005E5F RID: 24159
		public abstract RidList GetMethodRidList(uint typeDefRid);

		// Token: 0x06005E60 RID: 24160
		public abstract RidList GetParamRidList(uint methodRid);

		// Token: 0x06005E61 RID: 24161
		public abstract RidList GetEventRidList(uint eventMapRid);

		// Token: 0x06005E62 RID: 24162
		public abstract RidList GetPropertyRidList(uint propertyMapRid);

		// Token: 0x06005E63 RID: 24163
		public abstract RidList GetInterfaceImplRidList(uint typeDefRid);

		// Token: 0x06005E64 RID: 24164
		public abstract RidList GetGenericParamRidList(Table table, uint rid);

		// Token: 0x06005E65 RID: 24165
		public abstract RidList GetGenericParamConstraintRidList(uint genericParamRid);

		// Token: 0x06005E66 RID: 24166
		public abstract RidList GetCustomAttributeRidList(Table table, uint rid);

		// Token: 0x06005E67 RID: 24167
		public abstract RidList GetDeclSecurityRidList(Table table, uint rid);

		// Token: 0x06005E68 RID: 24168
		public abstract RidList GetMethodSemanticsRidList(Table table, uint rid);

		// Token: 0x06005E69 RID: 24169
		public abstract RidList GetMethodImplRidList(uint typeDefRid);

		// Token: 0x06005E6A RID: 24170
		public abstract uint GetClassLayoutRid(uint typeDefRid);

		// Token: 0x06005E6B RID: 24171
		public abstract uint GetFieldLayoutRid(uint fieldRid);

		// Token: 0x06005E6C RID: 24172
		public abstract uint GetFieldMarshalRid(Table table, uint rid);

		// Token: 0x06005E6D RID: 24173
		public abstract uint GetFieldRVARid(uint fieldRid);

		// Token: 0x06005E6E RID: 24174
		public abstract uint GetImplMapRid(Table table, uint rid);

		// Token: 0x06005E6F RID: 24175
		public abstract uint GetNestedClassRid(uint typeDefRid);

		// Token: 0x06005E70 RID: 24176
		public abstract uint GetEventMapRid(uint typeDefRid);

		// Token: 0x06005E71 RID: 24177
		public abstract uint GetPropertyMapRid(uint typeDefRid);

		// Token: 0x06005E72 RID: 24178
		public abstract uint GetConstantRid(Table table, uint rid);

		// Token: 0x06005E73 RID: 24179
		public abstract uint GetOwnerTypeOfField(uint fieldRid);

		// Token: 0x06005E74 RID: 24180
		public abstract uint GetOwnerTypeOfMethod(uint methodRid);

		// Token: 0x06005E75 RID: 24181
		public abstract uint GetOwnerTypeOfEvent(uint eventRid);

		// Token: 0x06005E76 RID: 24182
		public abstract uint GetOwnerTypeOfProperty(uint propertyRid);

		// Token: 0x06005E77 RID: 24183
		public abstract uint GetOwnerOfGenericParam(uint gpRid);

		// Token: 0x06005E78 RID: 24184
		public abstract uint GetOwnerOfGenericParamConstraint(uint gpcRid);

		// Token: 0x06005E79 RID: 24185
		public abstract uint GetOwnerOfParam(uint paramRid);

		// Token: 0x06005E7A RID: 24186
		public abstract RidList GetNestedClassRidList(uint typeDefRid);

		// Token: 0x06005E7B RID: 24187
		public abstract RidList GetNonNestedClassRidList();

		// Token: 0x06005E7C RID: 24188
		public abstract RidList GetLocalScopeRidList(uint methodRid);

		// Token: 0x06005E7D RID: 24189
		public abstract uint GetStateMachineMethodRid(uint methodRid);

		// Token: 0x06005E7E RID: 24190
		public abstract RidList GetCustomDebugInformationRidList(Table table, uint rid);

		// Token: 0x06005E7F RID: 24191
		public abstract void Dispose();
	}
}
