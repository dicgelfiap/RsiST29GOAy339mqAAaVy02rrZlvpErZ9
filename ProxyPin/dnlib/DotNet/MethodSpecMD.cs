using System;
using System.Collections.Generic;
using System.Threading;
using dnlib.DotNet.MD;
using dnlib.DotNet.Pdb;

namespace dnlib.DotNet
{
	// Token: 0x02000818 RID: 2072
	internal sealed class MethodSpecMD : MethodSpec, IMDTokenProviderMD, IMDTokenProvider, IContainsGenericParameter2
	{
		// Token: 0x17000EFE RID: 3838
		// (get) Token: 0x06004BA2 RID: 19362 RVA: 0x0017E748 File Offset: 0x0017E748
		public uint OrigRid
		{
			get
			{
				return this.origRid;
			}
		}

		// Token: 0x17000EFF RID: 3839
		// (get) Token: 0x06004BA3 RID: 19363 RVA: 0x0017E750 File Offset: 0x0017E750
		bool IContainsGenericParameter2.ContainsGenericParameter
		{
			get
			{
				return TypeHelper.ContainsGenericParameter(this);
			}
		}

		// Token: 0x06004BA4 RID: 19364 RVA: 0x0017E758 File Offset: 0x0017E758
		protected override void InitializeCustomAttributes()
		{
			RidList list = this.readerModule.Metadata.GetCustomAttributeRidList(Table.MethodSpec, this.origRid);
			CustomAttributeCollection value = new CustomAttributeCollection(list.Count, list, (object list2, int index) => this.readerModule.ReadCustomAttribute(list[index]));
			Interlocked.CompareExchange<CustomAttributeCollection>(ref this.customAttributes, value, null);
		}

		// Token: 0x06004BA5 RID: 19365 RVA: 0x0017E7CC File Offset: 0x0017E7CC
		protected override void InitializeCustomDebugInfos()
		{
			List<PdbCustomDebugInfo> list = new List<PdbCustomDebugInfo>();
			this.readerModule.InitializeCustomDebugInfos(new MDToken(base.MDToken.Table, this.origRid), this.gpContext, list);
			Interlocked.CompareExchange<IList<PdbCustomDebugInfo>>(ref this.customDebugInfos, list, null);
		}

		// Token: 0x06004BA6 RID: 19366 RVA: 0x0017E81C File Offset: 0x0017E81C
		public MethodSpecMD(ModuleDefMD readerModule, uint rid, GenericParamContext gpContext)
		{
			this.origRid = rid;
			this.rid = rid;
			this.readerModule = readerModule;
			this.gpContext = gpContext;
			RawMethodSpecRow rawMethodSpecRow;
			readerModule.TablesStream.TryReadMethodSpecRow(this.origRid, out rawMethodSpecRow);
			this.method = readerModule.ResolveMethodDefOrRef(rawMethodSpecRow.Method, gpContext);
			this.instantiation = readerModule.ReadSignature(rawMethodSpecRow.Instantiation, gpContext);
		}

		// Token: 0x040025D1 RID: 9681
		private readonly ModuleDefMD readerModule;

		// Token: 0x040025D2 RID: 9682
		private readonly uint origRid;

		// Token: 0x040025D3 RID: 9683
		private readonly GenericParamContext gpContext;
	}
}
