using System;
using System.Collections.Generic;
using System.Threading;
using dnlib.DotNet.MD;
using dnlib.DotNet.Pdb;

namespace dnlib.DotNet
{
	// Token: 0x020007BD RID: 1981
	internal sealed class GenericParamConstraintMD : GenericParamConstraint, IMDTokenProviderMD, IMDTokenProvider, IContainsGenericParameter2
	{
		// Token: 0x17000D72 RID: 3442
		// (get) Token: 0x06004848 RID: 18504 RVA: 0x00176FC0 File Offset: 0x00176FC0
		public uint OrigRid
		{
			get
			{
				return this.origRid;
			}
		}

		// Token: 0x17000D73 RID: 3443
		// (get) Token: 0x06004849 RID: 18505 RVA: 0x00176FC8 File Offset: 0x00176FC8
		bool IContainsGenericParameter2.ContainsGenericParameter
		{
			get
			{
				return TypeHelper.ContainsGenericParameter(this);
			}
		}

		// Token: 0x0600484A RID: 18506 RVA: 0x00176FD0 File Offset: 0x00176FD0
		protected override void InitializeCustomAttributes()
		{
			RidList list = this.readerModule.Metadata.GetCustomAttributeRidList(Table.GenericParamConstraint, this.origRid);
			CustomAttributeCollection value = new CustomAttributeCollection(list.Count, list, (object list2, int index) => this.readerModule.ReadCustomAttribute(list[index]));
			Interlocked.CompareExchange<CustomAttributeCollection>(ref this.customAttributes, value, null);
		}

		// Token: 0x0600484B RID: 18507 RVA: 0x00177044 File Offset: 0x00177044
		protected override void InitializeCustomDebugInfos()
		{
			List<PdbCustomDebugInfo> list = new List<PdbCustomDebugInfo>();
			this.readerModule.InitializeCustomDebugInfos(new MDToken(base.MDToken.Table, this.origRid), this.gpContext, list);
			Interlocked.CompareExchange<IList<PdbCustomDebugInfo>>(ref this.customDebugInfos, list, null);
		}

		// Token: 0x0600484C RID: 18508 RVA: 0x00177094 File Offset: 0x00177094
		public GenericParamConstraintMD(ModuleDefMD readerModule, uint rid, GenericParamContext gpContext)
		{
			this.origRid = rid;
			this.rid = rid;
			this.readerModule = readerModule;
			this.gpContext = gpContext;
			RawGenericParamConstraintRow rawGenericParamConstraintRow;
			readerModule.TablesStream.TryReadGenericParamConstraintRow(this.origRid, out rawGenericParamConstraintRow);
			this.constraint = readerModule.ResolveTypeDefOrRef(rawGenericParamConstraintRow.Constraint, gpContext);
			this.owner = readerModule.GetOwner(this);
		}

		// Token: 0x0600484D RID: 18509 RVA: 0x001770FC File Offset: 0x001770FC
		internal GenericParamConstraintMD InitializeAll()
		{
			MemberMDInitializer.Initialize(base.Owner);
			MemberMDInitializer.Initialize(base.Constraint);
			MemberMDInitializer.Initialize<CustomAttribute>(base.CustomAttributes);
			return this;
		}

		// Token: 0x04002508 RID: 9480
		private readonly ModuleDefMD readerModule;

		// Token: 0x04002509 RID: 9481
		private readonly uint origRid;

		// Token: 0x0400250A RID: 9482
		private readonly GenericParamContext gpContext;
	}
}
