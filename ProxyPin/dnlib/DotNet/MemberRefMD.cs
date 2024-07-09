using System;
using System.Collections.Generic;
using System.Threading;
using dnlib.DotNet.MD;
using dnlib.DotNet.Pdb;

namespace dnlib.DotNet
{
	// Token: 0x0200080B RID: 2059
	internal sealed class MemberRefMD : MemberRef, IMDTokenProviderMD, IMDTokenProvider, IContainsGenericParameter2
	{
		// Token: 0x17000E6B RID: 3691
		// (get) Token: 0x06004A87 RID: 19079 RVA: 0x0017C8B8 File Offset: 0x0017C8B8
		public uint OrigRid
		{
			get
			{
				return this.origRid;
			}
		}

		// Token: 0x17000E6C RID: 3692
		// (get) Token: 0x06004A88 RID: 19080 RVA: 0x0017C8C0 File Offset: 0x0017C8C0
		bool IContainsGenericParameter2.ContainsGenericParameter
		{
			get
			{
				return TypeHelper.ContainsGenericParameter(this);
			}
		}

		// Token: 0x06004A89 RID: 19081 RVA: 0x0017C8C8 File Offset: 0x0017C8C8
		protected override void InitializeCustomAttributes()
		{
			RidList list = this.readerModule.Metadata.GetCustomAttributeRidList(Table.MemberRef, this.origRid);
			CustomAttributeCollection value = new CustomAttributeCollection(list.Count, list, (object list2, int index) => this.readerModule.ReadCustomAttribute(list[index]));
			Interlocked.CompareExchange<CustomAttributeCollection>(ref this.customAttributes, value, null);
		}

		// Token: 0x06004A8A RID: 19082 RVA: 0x0017C93C File Offset: 0x0017C93C
		protected override void InitializeCustomDebugInfos()
		{
			List<PdbCustomDebugInfo> list = new List<PdbCustomDebugInfo>();
			this.readerModule.InitializeCustomDebugInfos(new MDToken(base.MDToken.Table, this.origRid), this.gpContext, list);
			Interlocked.CompareExchange<IList<PdbCustomDebugInfo>>(ref this.customDebugInfos, list, null);
		}

		// Token: 0x06004A8B RID: 19083 RVA: 0x0017C98C File Offset: 0x0017C98C
		public MemberRefMD(ModuleDefMD readerModule, uint rid, GenericParamContext gpContext)
		{
			this.origRid = rid;
			this.rid = rid;
			this.readerModule = readerModule;
			this.gpContext = gpContext;
			this.module = readerModule;
			RawMemberRefRow rawMemberRefRow;
			readerModule.TablesStream.TryReadMemberRefRow(this.origRid, out rawMemberRefRow);
			this.name = readerModule.StringsStream.ReadNoNull(rawMemberRefRow.Name);
			this.@class = readerModule.ResolveMemberRefParent(rawMemberRefRow.Class, gpContext);
			this.signature = readerModule.ReadSignature(rawMemberRefRow.Signature, MemberRef.GetSignatureGenericParamContext(gpContext, this.@class));
		}

		// Token: 0x0400256E RID: 9582
		private readonly ModuleDefMD readerModule;

		// Token: 0x0400256F RID: 9583
		private readonly uint origRid;

		// Token: 0x04002570 RID: 9584
		private readonly GenericParamContext gpContext;
	}
}
