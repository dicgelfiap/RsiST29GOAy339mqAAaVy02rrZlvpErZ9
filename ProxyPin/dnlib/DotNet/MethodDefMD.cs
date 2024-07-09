using System;
using System.Collections.Generic;
using System.Threading;
using dnlib.DotNet.Emit;
using dnlib.DotNet.MD;
using dnlib.DotNet.Pdb;
using dnlib.PE;
using dnlib.Utils;

namespace dnlib.DotNet
{
	// Token: 0x0200080F RID: 2063
	internal sealed class MethodDefMD : MethodDef, IMDTokenProviderMD, IMDTokenProvider
	{
		// Token: 0x17000EDB RID: 3803
		// (get) Token: 0x06004B52 RID: 19282 RVA: 0x0017D974 File Offset: 0x0017D974
		public uint OrigRid
		{
			get
			{
				return this.origRid;
			}
		}

		// Token: 0x06004B53 RID: 19283 RVA: 0x0017D97C File Offset: 0x0017D97C
		protected override void InitializeParamDefs()
		{
			RidList paramRidList = this.readerModule.Metadata.GetParamRidList(this.origRid);
			LazyList<ParamDef, RidList> value = new LazyList<ParamDef, RidList>(paramRidList.Count, this, paramRidList, (RidList list2, int index) => this.readerModule.ResolveParam(list2[index]));
			Interlocked.CompareExchange<LazyList<ParamDef>>(ref this.paramDefs, value, null);
		}

		// Token: 0x06004B54 RID: 19284 RVA: 0x0017D9D0 File Offset: 0x0017D9D0
		protected override void InitializeGenericParameters()
		{
			RidList genericParamRidList = this.readerModule.Metadata.GetGenericParamRidList(Table.Method, this.origRid);
			LazyList<GenericParam, RidList> value = new LazyList<GenericParam, RidList>(genericParamRidList.Count, this, genericParamRidList, (RidList list2, int index) => this.readerModule.ResolveGenericParam(list2[index]));
			Interlocked.CompareExchange<LazyList<GenericParam>>(ref this.genericParameters, value, null);
		}

		// Token: 0x06004B55 RID: 19285 RVA: 0x0017DA24 File Offset: 0x0017DA24
		protected override void InitializeDeclSecurities()
		{
			RidList declSecurityRidList = this.readerModule.Metadata.GetDeclSecurityRidList(Table.Method, this.origRid);
			LazyList<DeclSecurity, RidList> value = new LazyList<DeclSecurity, RidList>(declSecurityRidList.Count, declSecurityRidList, (RidList list2, int index) => this.readerModule.ResolveDeclSecurity(list2[index]));
			Interlocked.CompareExchange<IList<DeclSecurity>>(ref this.declSecurities, value, null);
		}

		// Token: 0x06004B56 RID: 19286 RVA: 0x0017DA78 File Offset: 0x0017DA78
		protected override ImplMap GetImplMap_NoLock()
		{
			return this.readerModule.ResolveImplMap(this.readerModule.Metadata.GetImplMapRid(Table.Method, this.origRid));
		}

		// Token: 0x06004B57 RID: 19287 RVA: 0x0017DA9C File Offset: 0x0017DA9C
		protected override MethodBody GetMethodBody_NoLock()
		{
			return this.readerModule.ReadMethodBody(this, this.origRva, this.origImplAttributes, new GenericParamContext(this.declaringType2, this));
		}

		// Token: 0x06004B58 RID: 19288 RVA: 0x0017DAC4 File Offset: 0x0017DAC4
		protected override void InitializeCustomAttributes()
		{
			RidList list = this.readerModule.Metadata.GetCustomAttributeRidList(Table.Method, this.origRid);
			CustomAttributeCollection value = new CustomAttributeCollection(list.Count, list, (object list2, int index) => this.readerModule.ReadCustomAttribute(list[index]));
			Interlocked.CompareExchange<CustomAttributeCollection>(ref this.customAttributes, value, null);
		}

		// Token: 0x06004B59 RID: 19289 RVA: 0x0017DB38 File Offset: 0x0017DB38
		protected override void InitializeCustomDebugInfos()
		{
			List<PdbCustomDebugInfo> list = new List<PdbCustomDebugInfo>();
			if (Interlocked.CompareExchange<IList<PdbCustomDebugInfo>>(ref this.customDebugInfos, list, null) == null)
			{
				CilBody body = base.Body;
				this.readerModule.InitializeCustomDebugInfos(this, body, list);
			}
		}

		// Token: 0x06004B5A RID: 19290 RVA: 0x0017DB78 File Offset: 0x0017DB78
		protected override void InitializeOverrides()
		{
			TypeDefMD typeDefMD = this.declaringType2 as TypeDefMD;
			IList<MethodOverride> list;
			if (typeDefMD != null)
			{
				list = typeDefMD.GetMethodOverrides(this, new GenericParamContext(this.declaringType2, this));
			}
			else
			{
				IList<MethodOverride> list2 = new List<MethodOverride>();
				list = list2;
			}
			IList<MethodOverride> value = list;
			Interlocked.CompareExchange<IList<MethodOverride>>(ref this.overrides, value, null);
		}

		// Token: 0x06004B5B RID: 19291 RVA: 0x0017DBCC File Offset: 0x0017DBCC
		protected override void InitializeSemanticsAttributes()
		{
			TypeDefMD typeDefMD = base.DeclaringType as TypeDefMD;
			if (typeDefMD != null)
			{
				typeDefMD.InitializeMethodSemanticsAttributes();
			}
			this.semAttrs |= MethodDef.SEMATTRS_INITD;
		}

		// Token: 0x06004B5C RID: 19292 RVA: 0x0017DC08 File Offset: 0x0017DC08
		public MethodDefMD(ModuleDefMD readerModule, uint rid)
		{
			this.origRid = rid;
			this.rid = rid;
			this.readerModule = readerModule;
			RawMethodRow rawMethodRow;
			readerModule.TablesStream.TryReadMethodRow(this.origRid, out rawMethodRow);
			this.rva = (RVA)rawMethodRow.RVA;
			this.implAttributes = (int)rawMethodRow.ImplFlags;
			this.attributes = (int)rawMethodRow.Flags;
			this.name = readerModule.StringsStream.ReadNoNull(rawMethodRow.Name);
			this.origRva = this.rva;
			this.origImplAttributes = (MethodImplAttributes)this.implAttributes;
			this.declaringType2 = readerModule.GetOwnerType(this);
			this.signature = readerModule.ReadSignature(rawMethodRow.Signature, new GenericParamContext(this.declaringType2, this));
			this.parameterList = new ParameterList(this, this.declaringType2);
			this.exportInfo = readerModule.GetExportInfo(rid);
		}

		// Token: 0x06004B5D RID: 19293 RVA: 0x0017DCE8 File Offset: 0x0017DCE8
		internal MethodDefMD InitializeAll()
		{
			MemberMDInitializer.Initialize(base.RVA);
			MemberMDInitializer.Initialize(base.Attributes);
			MemberMDInitializer.Initialize(base.ImplAttributes);
			MemberMDInitializer.Initialize(base.Name);
			MemberMDInitializer.Initialize(base.Signature);
			MemberMDInitializer.Initialize(base.ImplMap);
			MemberMDInitializer.Initialize(base.MethodBody);
			MemberMDInitializer.Initialize(base.DeclaringType);
			MemberMDInitializer.Initialize<CustomAttribute>(base.CustomAttributes);
			MemberMDInitializer.Initialize<MethodOverride>(base.Overrides);
			MemberMDInitializer.Initialize<ParamDef>(base.ParamDefs);
			MemberMDInitializer.Initialize<GenericParam>(base.GenericParameters);
			MemberMDInitializer.Initialize<DeclSecurity>(base.DeclSecurities);
			return this;
		}

		// Token: 0x06004B5E RID: 19294 RVA: 0x0017DD98 File Offset: 0x0017DD98
		internal override void OnLazyAdd2(int index, ref GenericParam value)
		{
			if (value.Owner != this)
			{
				value = this.readerModule.ForceUpdateRowId<GenericParamMD>(this.readerModule.ReadGenericParam(value.Rid).InitializeAll());
				value.Owner = this;
			}
		}

		// Token: 0x06004B5F RID: 19295 RVA: 0x0017DDE4 File Offset: 0x0017DDE4
		internal override void OnLazyAdd2(int index, ref ParamDef value)
		{
			if (value.DeclaringMethod != this)
			{
				value = this.readerModule.ForceUpdateRowId<ParamDefMD>(this.readerModule.ReadParam(value.Rid).InitializeAll());
				value.DeclaringMethod = this;
			}
		}

		// Token: 0x040025A2 RID: 9634
		private readonly ModuleDefMD readerModule;

		// Token: 0x040025A3 RID: 9635
		private readonly uint origRid;

		// Token: 0x040025A4 RID: 9636
		private readonly RVA origRva;

		// Token: 0x040025A5 RID: 9637
		private readonly MethodImplAttributes origImplAttributes;
	}
}
