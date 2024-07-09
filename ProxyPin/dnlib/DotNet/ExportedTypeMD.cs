using System;
using System.Collections.Generic;
using System.Threading;
using dnlib.DotNet.MD;
using dnlib.DotNet.Pdb;

namespace dnlib.DotNet
{
	// Token: 0x020007A9 RID: 1961
	internal sealed class ExportedTypeMD : ExportedType, IMDTokenProviderMD, IMDTokenProvider
	{
		// Token: 0x17000CE9 RID: 3305
		// (get) Token: 0x06004695 RID: 18069 RVA: 0x00170390 File Offset: 0x00170390
		public uint OrigRid
		{
			get
			{
				return this.origRid;
			}
		}

		// Token: 0x06004696 RID: 18070 RVA: 0x00170398 File Offset: 0x00170398
		protected override void InitializeCustomAttributes()
		{
			RidList list = this.readerModule.Metadata.GetCustomAttributeRidList(Table.ExportedType, this.origRid);
			CustomAttributeCollection value = new CustomAttributeCollection(list.Count, list, (object list2, int index) => this.readerModule.ReadCustomAttribute(list[index]));
			Interlocked.CompareExchange<CustomAttributeCollection>(ref this.customAttributes, value, null);
		}

		// Token: 0x06004697 RID: 18071 RVA: 0x0017040C File Offset: 0x0017040C
		protected override void InitializeCustomDebugInfos()
		{
			List<PdbCustomDebugInfo> list = new List<PdbCustomDebugInfo>();
			this.readerModule.InitializeCustomDebugInfos(new MDToken(base.MDToken.Table, this.origRid), default(GenericParamContext), list);
			Interlocked.CompareExchange<IList<PdbCustomDebugInfo>>(ref this.customDebugInfos, list, null);
		}

		// Token: 0x06004698 RID: 18072 RVA: 0x00170460 File Offset: 0x00170460
		protected override IImplementation GetImplementation_NoLock()
		{
			return this.readerModule.ResolveImplementation(this.implementationRid);
		}

		// Token: 0x06004699 RID: 18073 RVA: 0x00170474 File Offset: 0x00170474
		public ExportedTypeMD(ModuleDefMD readerModule, uint rid)
		{
			this.origRid = rid;
			this.rid = rid;
			this.readerModule = readerModule;
			this.module = readerModule;
			RawExportedTypeRow rawExportedTypeRow;
			readerModule.TablesStream.TryReadExportedTypeRow(this.origRid, out rawExportedTypeRow);
			this.implementationRid = rawExportedTypeRow.Implementation;
			this.attributes = (int)rawExportedTypeRow.Flags;
			this.typeDefId = rawExportedTypeRow.TypeDefId;
			this.typeName = readerModule.StringsStream.ReadNoNull(rawExportedTypeRow.TypeName);
			this.typeNamespace = readerModule.StringsStream.ReadNoNull(rawExportedTypeRow.TypeNamespace);
		}

		// Token: 0x040024A4 RID: 9380
		private readonly ModuleDefMD readerModule;

		// Token: 0x040024A5 RID: 9381
		private readonly uint origRid;

		// Token: 0x040024A6 RID: 9382
		private readonly uint implementationRid;
	}
}
