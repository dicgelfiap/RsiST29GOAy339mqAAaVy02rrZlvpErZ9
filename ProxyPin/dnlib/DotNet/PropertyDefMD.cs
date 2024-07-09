using System;
using System.Collections.Generic;
using System.Threading;
using dnlib.DotNet.MD;
using dnlib.DotNet.Pdb;

namespace dnlib.DotNet
{
	// Token: 0x02000831 RID: 2097
	internal sealed class PropertyDefMD : PropertyDef, IMDTokenProviderMD, IMDTokenProvider
	{
		// Token: 0x17000FCA RID: 4042
		// (get) Token: 0x06004E83 RID: 20099 RVA: 0x001863BC File Offset: 0x001863BC
		public uint OrigRid
		{
			get
			{
				return this.origRid;
			}
		}

		// Token: 0x06004E84 RID: 20100 RVA: 0x001863C4 File Offset: 0x001863C4
		protected override Constant GetConstant_NoLock()
		{
			return this.readerModule.ResolveConstant(this.readerModule.Metadata.GetConstantRid(Table.Property, this.origRid));
		}

		// Token: 0x06004E85 RID: 20101 RVA: 0x001863EC File Offset: 0x001863EC
		protected override void InitializeCustomAttributes()
		{
			RidList list = this.readerModule.Metadata.GetCustomAttributeRidList(Table.Property, this.origRid);
			CustomAttributeCollection value = new CustomAttributeCollection(list.Count, list, (object list2, int index) => this.readerModule.ReadCustomAttribute(list[index]));
			Interlocked.CompareExchange<CustomAttributeCollection>(ref this.customAttributes, value, null);
		}

		// Token: 0x06004E86 RID: 20102 RVA: 0x00186460 File Offset: 0x00186460
		protected override void InitializeCustomDebugInfos()
		{
			List<PdbCustomDebugInfo> list = new List<PdbCustomDebugInfo>();
			this.readerModule.InitializeCustomDebugInfos(new MDToken(base.MDToken.Table, this.origRid), new GenericParamContext(this.declaringType2), list);
			Interlocked.CompareExchange<IList<PdbCustomDebugInfo>>(ref this.customDebugInfos, list, null);
		}

		// Token: 0x06004E87 RID: 20103 RVA: 0x001864B8 File Offset: 0x001864B8
		public PropertyDefMD(ModuleDefMD readerModule, uint rid)
		{
			this.origRid = rid;
			this.rid = rid;
			this.readerModule = readerModule;
			RawPropertyRow rawPropertyRow;
			readerModule.TablesStream.TryReadPropertyRow(this.origRid, out rawPropertyRow);
			this.attributes = (int)rawPropertyRow.PropFlags;
			this.name = readerModule.StringsStream.ReadNoNull(rawPropertyRow.Name);
			this.declaringType2 = readerModule.GetOwnerType(this);
			this.type = readerModule.ReadSignature(rawPropertyRow.Type, new GenericParamContext(this.declaringType2));
		}

		// Token: 0x06004E88 RID: 20104 RVA: 0x00186548 File Offset: 0x00186548
		internal PropertyDefMD InitializeAll()
		{
			MemberMDInitializer.Initialize(base.Attributes);
			MemberMDInitializer.Initialize(base.Name);
			MemberMDInitializer.Initialize(base.Type);
			MemberMDInitializer.Initialize(base.Constant);
			MemberMDInitializer.Initialize<CustomAttribute>(base.CustomAttributes);
			MemberMDInitializer.Initialize(base.GetMethod);
			MemberMDInitializer.Initialize(base.SetMethod);
			MemberMDInitializer.Initialize<MethodDef>(base.OtherMethods);
			MemberMDInitializer.Initialize(base.DeclaringType);
			return this;
		}

		// Token: 0x06004E89 RID: 20105 RVA: 0x001865C4 File Offset: 0x001865C4
		protected override void InitializePropertyMethods_NoLock()
		{
			if (this.otherMethods != null)
			{
				return;
			}
			TypeDefMD typeDefMD = this.declaringType2 as TypeDefMD;
			IList<MethodDef> getMethods;
			IList<MethodDef> setMethods;
			IList<MethodDef> otherMethods;
			if (typeDefMD == null)
			{
				getMethods = new List<MethodDef>();
				setMethods = new List<MethodDef>();
				otherMethods = new List<MethodDef>();
			}
			else
			{
				typeDefMD.InitializeProperty(this, out getMethods, out setMethods, out otherMethods);
			}
			this.getMethods = getMethods;
			this.setMethods = setMethods;
			this.otherMethods = otherMethods;
		}

		// Token: 0x040026BE RID: 9918
		private readonly ModuleDefMD readerModule;

		// Token: 0x040026BF RID: 9919
		private readonly uint origRid;
	}
}
