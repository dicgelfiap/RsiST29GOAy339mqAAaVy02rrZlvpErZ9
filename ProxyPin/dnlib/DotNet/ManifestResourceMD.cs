using System;
using System.Collections.Generic;
using System.Threading;
using dnlib.DotNet.MD;
using dnlib.DotNet.Pdb;

namespace dnlib.DotNet
{
	// Token: 0x020007FB RID: 2043
	internal sealed class ManifestResourceMD : ManifestResource, IMDTokenProviderMD, IMDTokenProvider
	{
		// Token: 0x17000E23 RID: 3619
		// (get) Token: 0x06004994 RID: 18836 RVA: 0x00179CFC File Offset: 0x00179CFC
		public uint OrigRid
		{
			get
			{
				return this.origRid;
			}
		}

		// Token: 0x06004995 RID: 18837 RVA: 0x00179D04 File Offset: 0x00179D04
		protected override void InitializeCustomAttributes()
		{
			RidList list = this.readerModule.Metadata.GetCustomAttributeRidList(Table.ManifestResource, this.origRid);
			CustomAttributeCollection value = new CustomAttributeCollection(list.Count, list, (object list2, int index) => this.readerModule.ReadCustomAttribute(list[index]));
			Interlocked.CompareExchange<CustomAttributeCollection>(ref this.customAttributes, value, null);
		}

		// Token: 0x06004996 RID: 18838 RVA: 0x00179D78 File Offset: 0x00179D78
		protected override void InitializeCustomDebugInfos()
		{
			List<PdbCustomDebugInfo> list = new List<PdbCustomDebugInfo>();
			this.readerModule.InitializeCustomDebugInfos(new MDToken(base.MDToken.Table, this.origRid), default(GenericParamContext), list);
			Interlocked.CompareExchange<IList<PdbCustomDebugInfo>>(ref this.customDebugInfos, list, null);
		}

		// Token: 0x06004997 RID: 18839 RVA: 0x00179DCC File Offset: 0x00179DCC
		public ManifestResourceMD(ModuleDefMD readerModule, uint rid)
		{
			this.origRid = rid;
			this.rid = rid;
			this.readerModule = readerModule;
			RawManifestResourceRow rawManifestResourceRow;
			readerModule.TablesStream.TryReadManifestResourceRow(this.origRid, out rawManifestResourceRow);
			this.offset = rawManifestResourceRow.Offset;
			this.attributes = (int)rawManifestResourceRow.Flags;
			this.name = readerModule.StringsStream.ReadNoNull(rawManifestResourceRow.Name);
			this.implementation = readerModule.ResolveImplementation(rawManifestResourceRow.Implementation);
		}

		// Token: 0x04002539 RID: 9529
		private readonly ModuleDefMD readerModule;

		// Token: 0x0400253A RID: 9530
		private readonly uint origRid;
	}
}
