using System;
using System.Collections.Generic;
using System.Threading;
using dnlib.DotNet.MD;
using dnlib.DotNet.Pdb;

namespace dnlib.DotNet
{
	// Token: 0x0200082A RID: 2090
	internal sealed class ParamDefMD : ParamDef, IMDTokenProviderMD, IMDTokenProvider
	{
		// Token: 0x17000F8C RID: 3980
		// (get) Token: 0x06004E04 RID: 19972 RVA: 0x0018525C File Offset: 0x0018525C
		public uint OrigRid
		{
			get
			{
				return this.origRid;
			}
		}

		// Token: 0x06004E05 RID: 19973 RVA: 0x00185264 File Offset: 0x00185264
		protected override MarshalType GetMarshalType_NoLock()
		{
			return this.readerModule.ReadMarshalType(Table.Param, this.origRid, GenericParamContext.Create(this.declaringMethod));
		}

		// Token: 0x06004E06 RID: 19974 RVA: 0x00185284 File Offset: 0x00185284
		protected override Constant GetConstant_NoLock()
		{
			return this.readerModule.ResolveConstant(this.readerModule.Metadata.GetConstantRid(Table.Param, this.origRid));
		}

		// Token: 0x06004E07 RID: 19975 RVA: 0x001852A8 File Offset: 0x001852A8
		protected override void InitializeCustomAttributes()
		{
			RidList list = this.readerModule.Metadata.GetCustomAttributeRidList(Table.Param, this.origRid);
			CustomAttributeCollection value = new CustomAttributeCollection(list.Count, list, (object list2, int index) => this.readerModule.ReadCustomAttribute(list[index]));
			Interlocked.CompareExchange<CustomAttributeCollection>(ref this.customAttributes, value, null);
		}

		// Token: 0x06004E08 RID: 19976 RVA: 0x0018531C File Offset: 0x0018531C
		protected override void InitializeCustomDebugInfos()
		{
			List<PdbCustomDebugInfo> list = new List<PdbCustomDebugInfo>();
			this.readerModule.InitializeCustomDebugInfos(new MDToken(base.MDToken.Table, this.origRid), GenericParamContext.Create(this.declaringMethod), list);
			Interlocked.CompareExchange<IList<PdbCustomDebugInfo>>(ref this.customDebugInfos, list, null);
		}

		// Token: 0x06004E09 RID: 19977 RVA: 0x00185374 File Offset: 0x00185374
		public ParamDefMD(ModuleDefMD readerModule, uint rid)
		{
			this.origRid = rid;
			this.rid = rid;
			this.readerModule = readerModule;
			RawParamRow rawParamRow;
			readerModule.TablesStream.TryReadParamRow(this.origRid, out rawParamRow);
			this.attributes = (int)rawParamRow.Flags;
			this.sequence = rawParamRow.Sequence;
			this.name = readerModule.StringsStream.ReadNoNull(rawParamRow.Name);
			this.declaringMethod = readerModule.GetOwner(this);
		}

		// Token: 0x06004E0A RID: 19978 RVA: 0x001853F0 File Offset: 0x001853F0
		internal ParamDefMD InitializeAll()
		{
			MemberMDInitializer.Initialize(base.DeclaringMethod);
			MemberMDInitializer.Initialize(base.Attributes);
			MemberMDInitializer.Initialize(base.Sequence);
			MemberMDInitializer.Initialize(base.Name);
			MemberMDInitializer.Initialize(base.MarshalType);
			MemberMDInitializer.Initialize(base.Constant);
			MemberMDInitializer.Initialize<CustomAttribute>(base.CustomAttributes);
			return this;
		}

		// Token: 0x04002687 RID: 9863
		private readonly ModuleDefMD readerModule;

		// Token: 0x04002688 RID: 9864
		private readonly uint origRid;
	}
}
