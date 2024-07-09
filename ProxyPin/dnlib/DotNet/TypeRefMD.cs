using System;
using System.Collections.Generic;
using System.Threading;
using dnlib.DotNet.MD;
using dnlib.DotNet.Pdb;

namespace dnlib.DotNet
{
	// Token: 0x02000865 RID: 2149
	internal sealed class TypeRefMD : TypeRef, IMDTokenProviderMD, IMDTokenProvider
	{
		// Token: 0x170010B8 RID: 4280
		// (get) Token: 0x06005260 RID: 21088 RVA: 0x00195CD4 File Offset: 0x00195CD4
		public uint OrigRid
		{
			get
			{
				return this.origRid;
			}
		}

		// Token: 0x06005261 RID: 21089 RVA: 0x00195CDC File Offset: 0x00195CDC
		protected override IResolutionScope GetResolutionScope_NoLock()
		{
			return this.readerModule.ResolveResolutionScope(this.resolutionScopeCodedToken);
		}

		// Token: 0x06005262 RID: 21090 RVA: 0x00195CF0 File Offset: 0x00195CF0
		protected override void InitializeCustomAttributes()
		{
			RidList list = this.readerModule.Metadata.GetCustomAttributeRidList(Table.TypeRef, this.origRid);
			CustomAttributeCollection value = new CustomAttributeCollection(list.Count, list, (object list2, int index) => this.readerModule.ReadCustomAttribute(list[index]));
			Interlocked.CompareExchange<CustomAttributeCollection>(ref this.customAttributes, value, null);
		}

		// Token: 0x06005263 RID: 21091 RVA: 0x00195D64 File Offset: 0x00195D64
		protected override void InitializeCustomDebugInfos()
		{
			List<PdbCustomDebugInfo> list = new List<PdbCustomDebugInfo>();
			this.readerModule.InitializeCustomDebugInfos(new MDToken(base.MDToken.Table, this.origRid), default(GenericParamContext), list);
			Interlocked.CompareExchange<IList<PdbCustomDebugInfo>>(ref this.customDebugInfos, list, null);
		}

		// Token: 0x06005264 RID: 21092 RVA: 0x00195DB8 File Offset: 0x00195DB8
		public TypeRefMD(ModuleDefMD readerModule, uint rid)
		{
			this.origRid = rid;
			this.rid = rid;
			this.readerModule = readerModule;
			this.module = readerModule;
			RawTypeRefRow rawTypeRefRow;
			readerModule.TablesStream.TryReadTypeRefRow(this.origRid, out rawTypeRefRow);
			this.name = readerModule.StringsStream.ReadNoNull(rawTypeRefRow.Name);
			this.@namespace = readerModule.StringsStream.ReadNoNull(rawTypeRefRow.Namespace);
			this.resolutionScopeCodedToken = rawTypeRefRow.ResolutionScope;
		}

		// Token: 0x040027C7 RID: 10183
		private readonly ModuleDefMD readerModule;

		// Token: 0x040027C8 RID: 10184
		private readonly uint origRid;

		// Token: 0x040027C9 RID: 10185
		private readonly uint resolutionScopeCodedToken;
	}
}
