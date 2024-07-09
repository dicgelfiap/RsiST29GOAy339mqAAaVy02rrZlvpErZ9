using System;
using System.Collections.Generic;
using System.Threading;
using dnlib.DotNet.MD;
using dnlib.DotNet.Pdb;

namespace dnlib.DotNet
{
	// Token: 0x02000824 RID: 2084
	internal sealed class ModuleRefMD : ModuleRef, IMDTokenProviderMD, IMDTokenProvider
	{
		// Token: 0x17000F70 RID: 3952
		// (get) Token: 0x06004DC4 RID: 19908 RVA: 0x00184C8C File Offset: 0x00184C8C
		public uint OrigRid
		{
			get
			{
				return this.origRid;
			}
		}

		// Token: 0x06004DC5 RID: 19909 RVA: 0x00184C94 File Offset: 0x00184C94
		protected override void InitializeCustomAttributes()
		{
			RidList list = this.readerModule.Metadata.GetCustomAttributeRidList(Table.ModuleRef, this.origRid);
			CustomAttributeCollection value = new CustomAttributeCollection(list.Count, list, (object list2, int index) => this.readerModule.ReadCustomAttribute(list[index]));
			Interlocked.CompareExchange<CustomAttributeCollection>(ref this.customAttributes, value, null);
		}

		// Token: 0x06004DC6 RID: 19910 RVA: 0x00184D08 File Offset: 0x00184D08
		protected override void InitializeCustomDebugInfos()
		{
			List<PdbCustomDebugInfo> list = new List<PdbCustomDebugInfo>();
			this.readerModule.InitializeCustomDebugInfos(new MDToken(base.MDToken.Table, this.origRid), default(GenericParamContext), list);
			Interlocked.CompareExchange<IList<PdbCustomDebugInfo>>(ref this.customDebugInfos, list, null);
		}

		// Token: 0x06004DC7 RID: 19911 RVA: 0x00184D5C File Offset: 0x00184D5C
		public ModuleRefMD(ModuleDefMD readerModule, uint rid)
		{
			this.origRid = rid;
			this.rid = rid;
			this.readerModule = readerModule;
			this.module = readerModule;
			RawModuleRefRow rawModuleRefRow;
			readerModule.TablesStream.TryReadModuleRefRow(this.origRid, out rawModuleRefRow);
			this.name = readerModule.StringsStream.ReadNoNull(rawModuleRefRow.Name);
		}

		// Token: 0x0400263D RID: 9789
		private readonly ModuleDefMD readerModule;

		// Token: 0x0400263E RID: 9790
		private readonly uint origRid;
	}
}
