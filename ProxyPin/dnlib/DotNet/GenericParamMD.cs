using System;
using System.Collections.Generic;
using System.Threading;
using dnlib.DotNet.MD;
using dnlib.DotNet.Pdb;
using dnlib.Utils;

namespace dnlib.DotNet
{
	// Token: 0x020007B9 RID: 1977
	internal sealed class GenericParamMD : GenericParam, IMDTokenProviderMD, IMDTokenProvider
	{
		// Token: 0x17000D66 RID: 3430
		// (get) Token: 0x0600482C RID: 18476 RVA: 0x00176BE4 File Offset: 0x00176BE4
		public uint OrigRid
		{
			get
			{
				return this.origRid;
			}
		}

		// Token: 0x0600482D RID: 18477 RVA: 0x00176BEC File Offset: 0x00176BEC
		protected override void InitializeCustomAttributes()
		{
			RidList list = this.readerModule.Metadata.GetCustomAttributeRidList(Table.GenericParam, this.origRid);
			CustomAttributeCollection value = new CustomAttributeCollection(list.Count, list, (object list2, int index) => this.readerModule.ReadCustomAttribute(list[index]));
			Interlocked.CompareExchange<CustomAttributeCollection>(ref this.customAttributes, value, null);
		}

		// Token: 0x0600482E RID: 18478 RVA: 0x00176C60 File Offset: 0x00176C60
		protected override void InitializeCustomDebugInfos()
		{
			List<PdbCustomDebugInfo> list = new List<PdbCustomDebugInfo>();
			this.readerModule.InitializeCustomDebugInfos(new MDToken(base.MDToken.Table, this.origRid), GenericParamMD.GetGenericParamContext(this.owner), list);
			Interlocked.CompareExchange<IList<PdbCustomDebugInfo>>(ref this.customDebugInfos, list, null);
		}

		// Token: 0x0600482F RID: 18479 RVA: 0x00176CB8 File Offset: 0x00176CB8
		protected override void InitializeGenericParamConstraints()
		{
			RidList genericParamConstraintRidList = this.readerModule.Metadata.GetGenericParamConstraintRidList(this.origRid);
			LazyList<GenericParamConstraint, RidList> value = new LazyList<GenericParamConstraint, RidList>(genericParamConstraintRidList.Count, this, genericParamConstraintRidList, (RidList list2, int index) => this.readerModule.ResolveGenericParamConstraint(list2[index], GenericParamMD.GetGenericParamContext(this.owner)));
			Interlocked.CompareExchange<LazyList<GenericParamConstraint>>(ref this.genericParamConstraints, value, null);
		}

		// Token: 0x06004830 RID: 18480 RVA: 0x00176D0C File Offset: 0x00176D0C
		private static GenericParamContext GetGenericParamContext(ITypeOrMethodDef tmOwner)
		{
			MethodDef methodDef = tmOwner as MethodDef;
			if (methodDef != null)
			{
				return GenericParamContext.Create(methodDef);
			}
			return new GenericParamContext(tmOwner as TypeDef);
		}

		// Token: 0x06004831 RID: 18481 RVA: 0x00176D3C File Offset: 0x00176D3C
		public GenericParamMD(ModuleDefMD readerModule, uint rid)
		{
			this.origRid = rid;
			this.rid = rid;
			this.readerModule = readerModule;
			RawGenericParamRow rawGenericParamRow;
			readerModule.TablesStream.TryReadGenericParamRow(this.origRid, out rawGenericParamRow);
			this.number = rawGenericParamRow.Number;
			this.attributes = (int)rawGenericParamRow.Flags;
			this.name = readerModule.StringsStream.ReadNoNull(rawGenericParamRow.Name);
			this.owner = readerModule.GetOwner(this);
			if (rawGenericParamRow.Kind != 0U)
			{
				this.kind = readerModule.ResolveTypeDefOrRef(rawGenericParamRow.Kind, GenericParamMD.GetGenericParamContext(this.owner));
			}
		}

		// Token: 0x06004832 RID: 18482 RVA: 0x00176DE0 File Offset: 0x00176DE0
		internal GenericParamMD InitializeAll()
		{
			MemberMDInitializer.Initialize(base.Owner);
			MemberMDInitializer.Initialize(base.Number);
			MemberMDInitializer.Initialize(base.Flags);
			MemberMDInitializer.Initialize(base.Name);
			MemberMDInitializer.Initialize(base.Kind);
			MemberMDInitializer.Initialize<CustomAttribute>(base.CustomAttributes);
			MemberMDInitializer.Initialize<GenericParamConstraint>(base.GenericParamConstraints);
			return this;
		}

		// Token: 0x06004833 RID: 18483 RVA: 0x00176E4C File Offset: 0x00176E4C
		internal override void OnLazyAdd2(int index, ref GenericParamConstraint value)
		{
			if (value.Owner != this)
			{
				value = this.readerModule.ForceUpdateRowId<GenericParamConstraintMD>(this.readerModule.ReadGenericParamConstraint(value.Rid, GenericParamMD.GetGenericParamContext(this.owner)).InitializeAll());
				value.Owner = this;
			}
		}

		// Token: 0x040024F7 RID: 9463
		private readonly ModuleDefMD readerModule;

		// Token: 0x040024F8 RID: 9464
		private readonly uint origRid;
	}
}
