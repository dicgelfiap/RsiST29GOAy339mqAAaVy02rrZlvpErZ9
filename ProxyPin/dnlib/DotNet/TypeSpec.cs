using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using dnlib.DotNet.MD;
using dnlib.DotNet.Pdb;
using dnlib.Threading;

namespace dnlib.DotNet
{
	// Token: 0x0200087F RID: 2175
	[ComVisible(true)]
	public abstract class TypeSpec : ITypeDefOrRef, ICodedToken, IMDTokenProvider, IHasCustomAttribute, IMemberRefParent, IFullName, IType, IOwnerModule, IGenericParameterProvider, IIsTypeOrMethod, IContainsGenericParameter, ITokenOperand, IMemberRef, IHasCustomDebugInformation
	{
		// Token: 0x17001115 RID: 4373
		// (get) Token: 0x060052FD RID: 21245 RVA: 0x00196670 File Offset: 0x00196670
		public MDToken MDToken
		{
			get
			{
				return new MDToken(Table.TypeSpec, this.rid);
			}
		}

		// Token: 0x17001116 RID: 4374
		// (get) Token: 0x060052FE RID: 21246 RVA: 0x00196680 File Offset: 0x00196680
		// (set) Token: 0x060052FF RID: 21247 RVA: 0x00196688 File Offset: 0x00196688
		public uint Rid
		{
			get
			{
				return this.rid;
			}
			set
			{
				this.rid = value;
			}
		}

		// Token: 0x17001117 RID: 4375
		// (get) Token: 0x06005300 RID: 21248 RVA: 0x00196694 File Offset: 0x00196694
		public int TypeDefOrRefTag
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x17001118 RID: 4376
		// (get) Token: 0x06005301 RID: 21249 RVA: 0x00196698 File Offset: 0x00196698
		public int HasCustomAttributeTag
		{
			get
			{
				return 13;
			}
		}

		// Token: 0x17001119 RID: 4377
		// (get) Token: 0x06005302 RID: 21250 RVA: 0x0019669C File Offset: 0x0019669C
		public int MemberRefParentTag
		{
			get
			{
				return 4;
			}
		}

		// Token: 0x1700111A RID: 4378
		// (get) Token: 0x06005303 RID: 21251 RVA: 0x001966A0 File Offset: 0x001966A0
		int IGenericParameterProvider.NumberOfGenericParameters
		{
			get
			{
				TypeSig typeSig = this.TypeSig;
				if (typeSig != null)
				{
					return ((IGenericParameterProvider)typeSig).NumberOfGenericParameters;
				}
				return 0;
			}
		}

		// Token: 0x1700111B RID: 4379
		// (get) Token: 0x06005304 RID: 21252 RVA: 0x001966C8 File Offset: 0x001966C8
		// (set) Token: 0x06005305 RID: 21253 RVA: 0x001966F4 File Offset: 0x001966F4
		UTF8String IFullName.Name
		{
			get
			{
				ITypeDefOrRef scopeType = this.ScopeType;
				if (scopeType != null)
				{
					return scopeType.Name;
				}
				return UTF8String.Empty;
			}
			set
			{
				ITypeDefOrRef scopeType = this.ScopeType;
				if (scopeType != null)
				{
					scopeType.Name = value;
				}
			}
		}

		// Token: 0x1700111C RID: 4380
		// (get) Token: 0x06005306 RID: 21254 RVA: 0x0019671C File Offset: 0x0019671C
		ITypeDefOrRef IMemberRef.DeclaringType
		{
			get
			{
				TypeSig typeSig = this.TypeSig.RemovePinnedAndModifiers();
				GenericInstSig genericInstSig = typeSig as GenericInstSig;
				if (genericInstSig != null)
				{
					typeSig = genericInstSig.GenericType;
				}
				TypeDefOrRefSig typeDefOrRefSig = typeSig as TypeDefOrRefSig;
				if (typeDefOrRefSig == null)
				{
					return null;
				}
				if (typeDefOrRefSig.IsTypeDef || typeDefOrRefSig.IsTypeRef)
				{
					return typeDefOrRefSig.TypeDefOrRef.DeclaringType;
				}
				return null;
			}
		}

		// Token: 0x1700111D RID: 4381
		// (get) Token: 0x06005307 RID: 21255 RVA: 0x00196780 File Offset: 0x00196780
		bool IIsTypeOrMethod.IsType
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700111E RID: 4382
		// (get) Token: 0x06005308 RID: 21256 RVA: 0x00196784 File Offset: 0x00196784
		bool IIsTypeOrMethod.IsMethod
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700111F RID: 4383
		// (get) Token: 0x06005309 RID: 21257 RVA: 0x00196788 File Offset: 0x00196788
		bool IMemberRef.IsField
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001120 RID: 4384
		// (get) Token: 0x0600530A RID: 21258 RVA: 0x0019678C File Offset: 0x0019678C
		bool IMemberRef.IsTypeSpec
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001121 RID: 4385
		// (get) Token: 0x0600530B RID: 21259 RVA: 0x00196790 File Offset: 0x00196790
		bool IMemberRef.IsTypeRef
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001122 RID: 4386
		// (get) Token: 0x0600530C RID: 21260 RVA: 0x00196794 File Offset: 0x00196794
		bool IMemberRef.IsTypeDef
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001123 RID: 4387
		// (get) Token: 0x0600530D RID: 21261 RVA: 0x00196798 File Offset: 0x00196798
		bool IMemberRef.IsMethodSpec
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001124 RID: 4388
		// (get) Token: 0x0600530E RID: 21262 RVA: 0x0019679C File Offset: 0x0019679C
		bool IMemberRef.IsMethodDef
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001125 RID: 4389
		// (get) Token: 0x0600530F RID: 21263 RVA: 0x001967A0 File Offset: 0x001967A0
		bool IMemberRef.IsMemberRef
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001126 RID: 4390
		// (get) Token: 0x06005310 RID: 21264 RVA: 0x001967A4 File Offset: 0x001967A4
		bool IMemberRef.IsFieldDef
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001127 RID: 4391
		// (get) Token: 0x06005311 RID: 21265 RVA: 0x001967A8 File Offset: 0x001967A8
		bool IMemberRef.IsPropertyDef
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001128 RID: 4392
		// (get) Token: 0x06005312 RID: 21266 RVA: 0x001967AC File Offset: 0x001967AC
		bool IMemberRef.IsEventDef
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001129 RID: 4393
		// (get) Token: 0x06005313 RID: 21267 RVA: 0x001967B0 File Offset: 0x001967B0
		bool IMemberRef.IsGenericParam
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700112A RID: 4394
		// (get) Token: 0x06005314 RID: 21268 RVA: 0x001967B4 File Offset: 0x001967B4
		public bool IsValueType
		{
			get
			{
				TypeSig typeSig = this.TypeSig;
				return typeSig != null && typeSig.IsValueType;
			}
		}

		// Token: 0x1700112B RID: 4395
		// (get) Token: 0x06005315 RID: 21269 RVA: 0x001967DC File Offset: 0x001967DC
		public bool IsPrimitive
		{
			get
			{
				TypeSig typeSig = this.TypeSig;
				return typeSig != null && typeSig.IsPrimitive;
			}
		}

		// Token: 0x1700112C RID: 4396
		// (get) Token: 0x06005316 RID: 21270 RVA: 0x00196804 File Offset: 0x00196804
		public string TypeName
		{
			get
			{
				return FullNameFactory.Name(this, false, null);
			}
		}

		// Token: 0x1700112D RID: 4397
		// (get) Token: 0x06005317 RID: 21271 RVA: 0x00196810 File Offset: 0x00196810
		public string ReflectionName
		{
			get
			{
				return FullNameFactory.Name(this, true, null);
			}
		}

		// Token: 0x1700112E RID: 4398
		// (get) Token: 0x06005318 RID: 21272 RVA: 0x0019681C File Offset: 0x0019681C
		string IType.Namespace
		{
			get
			{
				return FullNameFactory.Namespace(this, false, null);
			}
		}

		// Token: 0x1700112F RID: 4399
		// (get) Token: 0x06005319 RID: 21273 RVA: 0x00196828 File Offset: 0x00196828
		public string ReflectionNamespace
		{
			get
			{
				return FullNameFactory.Namespace(this, true, null);
			}
		}

		// Token: 0x17001130 RID: 4400
		// (get) Token: 0x0600531A RID: 21274 RVA: 0x00196834 File Offset: 0x00196834
		public string FullName
		{
			get
			{
				return FullNameFactory.FullName(this, false, null, null);
			}
		}

		// Token: 0x17001131 RID: 4401
		// (get) Token: 0x0600531B RID: 21275 RVA: 0x00196840 File Offset: 0x00196840
		public string ReflectionFullName
		{
			get
			{
				return FullNameFactory.FullName(this, true, null, null);
			}
		}

		// Token: 0x17001132 RID: 4402
		// (get) Token: 0x0600531C RID: 21276 RVA: 0x0019684C File Offset: 0x0019684C
		public string AssemblyQualifiedName
		{
			get
			{
				return FullNameFactory.AssemblyQualifiedName(this, null, null);
			}
		}

		// Token: 0x17001133 RID: 4403
		// (get) Token: 0x0600531D RID: 21277 RVA: 0x00196858 File Offset: 0x00196858
		public IAssembly DefinitionAssembly
		{
			get
			{
				return FullNameFactory.DefinitionAssembly(this);
			}
		}

		// Token: 0x17001134 RID: 4404
		// (get) Token: 0x0600531E RID: 21278 RVA: 0x00196860 File Offset: 0x00196860
		public IScope Scope
		{
			get
			{
				return FullNameFactory.Scope(this);
			}
		}

		// Token: 0x17001135 RID: 4405
		// (get) Token: 0x0600531F RID: 21279 RVA: 0x00196868 File Offset: 0x00196868
		public ITypeDefOrRef ScopeType
		{
			get
			{
				return FullNameFactory.ScopeType(this);
			}
		}

		// Token: 0x17001136 RID: 4406
		// (get) Token: 0x06005320 RID: 21280 RVA: 0x00196870 File Offset: 0x00196870
		public bool ContainsGenericParameter
		{
			get
			{
				return TypeHelper.ContainsGenericParameter(this);
			}
		}

		// Token: 0x17001137 RID: 4407
		// (get) Token: 0x06005321 RID: 21281 RVA: 0x00196878 File Offset: 0x00196878
		public ModuleDef Module
		{
			get
			{
				return FullNameFactory.OwnerModule(this);
			}
		}

		// Token: 0x17001138 RID: 4408
		// (get) Token: 0x06005322 RID: 21282 RVA: 0x00196880 File Offset: 0x00196880
		// (set) Token: 0x06005323 RID: 21283 RVA: 0x0019689C File Offset: 0x0019689C
		public TypeSig TypeSig
		{
			get
			{
				if (!this.typeSigAndExtraData_isInitialized)
				{
					this.InitializeTypeSigAndExtraData();
				}
				return this.typeSig;
			}
			set
			{
				this.theLock.EnterWriteLock();
				try
				{
					this.typeSig = value;
					if (!this.typeSigAndExtraData_isInitialized)
					{
						this.GetTypeSigAndExtraData_NoLock(out this.extraData);
					}
					this.typeSigAndExtraData_isInitialized = true;
				}
				finally
				{
					this.theLock.ExitWriteLock();
				}
			}
		}

		// Token: 0x17001139 RID: 4409
		// (get) Token: 0x06005324 RID: 21284 RVA: 0x001968FC File Offset: 0x001968FC
		// (set) Token: 0x06005325 RID: 21285 RVA: 0x00196918 File Offset: 0x00196918
		public byte[] ExtraData
		{
			get
			{
				if (!this.typeSigAndExtraData_isInitialized)
				{
					this.InitializeTypeSigAndExtraData();
				}
				return this.extraData;
			}
			set
			{
				if (!this.typeSigAndExtraData_isInitialized)
				{
					this.InitializeTypeSigAndExtraData();
				}
				this.extraData = value;
			}
		}

		// Token: 0x06005326 RID: 21286 RVA: 0x00196934 File Offset: 0x00196934
		private void InitializeTypeSigAndExtraData()
		{
			this.theLock.EnterWriteLock();
			try
			{
				if (!this.typeSigAndExtraData_isInitialized)
				{
					this.typeSig = this.GetTypeSigAndExtraData_NoLock(out this.extraData);
					this.typeSigAndExtraData_isInitialized = true;
				}
			}
			finally
			{
				this.theLock.ExitWriteLock();
			}
		}

		// Token: 0x06005327 RID: 21287 RVA: 0x00196998 File Offset: 0x00196998
		protected virtual TypeSig GetTypeSigAndExtraData_NoLock(out byte[] extraData)
		{
			extraData = null;
			return null;
		}

		// Token: 0x1700113A RID: 4410
		// (get) Token: 0x06005328 RID: 21288 RVA: 0x001969A0 File Offset: 0x001969A0
		public CustomAttributeCollection CustomAttributes
		{
			get
			{
				if (this.customAttributes == null)
				{
					this.InitializeCustomAttributes();
				}
				return this.customAttributes;
			}
		}

		// Token: 0x06005329 RID: 21289 RVA: 0x001969BC File Offset: 0x001969BC
		protected virtual void InitializeCustomAttributes()
		{
			Interlocked.CompareExchange<CustomAttributeCollection>(ref this.customAttributes, new CustomAttributeCollection(), null);
		}

		// Token: 0x1700113B RID: 4411
		// (get) Token: 0x0600532A RID: 21290 RVA: 0x001969D0 File Offset: 0x001969D0
		public bool HasCustomAttributes
		{
			get
			{
				return this.CustomAttributes.Count > 0;
			}
		}

		// Token: 0x1700113C RID: 4412
		// (get) Token: 0x0600532B RID: 21291 RVA: 0x001969E0 File Offset: 0x001969E0
		public int HasCustomDebugInformationTag
		{
			get
			{
				return 13;
			}
		}

		// Token: 0x1700113D RID: 4413
		// (get) Token: 0x0600532C RID: 21292 RVA: 0x001969E4 File Offset: 0x001969E4
		public bool HasCustomDebugInfos
		{
			get
			{
				return this.CustomDebugInfos.Count > 0;
			}
		}

		// Token: 0x1700113E RID: 4414
		// (get) Token: 0x0600532D RID: 21293 RVA: 0x001969F4 File Offset: 0x001969F4
		public IList<PdbCustomDebugInfo> CustomDebugInfos
		{
			get
			{
				if (this.customDebugInfos == null)
				{
					this.InitializeCustomDebugInfos();
				}
				return this.customDebugInfos;
			}
		}

		// Token: 0x0600532E RID: 21294 RVA: 0x00196A10 File Offset: 0x00196A10
		protected virtual void InitializeCustomDebugInfos()
		{
			Interlocked.CompareExchange<IList<PdbCustomDebugInfo>>(ref this.customDebugInfos, new List<PdbCustomDebugInfo>(), null);
		}

		// Token: 0x0600532F RID: 21295 RVA: 0x00196A24 File Offset: 0x00196A24
		public override string ToString()
		{
			return this.FullName;
		}

		// Token: 0x040027DA RID: 10202
		protected uint rid;

		// Token: 0x040027DB RID: 10203
		private readonly Lock theLock = Lock.Create();

		// Token: 0x040027DC RID: 10204
		protected TypeSig typeSig;

		// Token: 0x040027DD RID: 10205
		protected byte[] extraData;

		// Token: 0x040027DE RID: 10206
		protected bool typeSigAndExtraData_isInitialized;

		// Token: 0x040027DF RID: 10207
		protected CustomAttributeCollection customAttributes;

		// Token: 0x040027E0 RID: 10208
		protected IList<PdbCustomDebugInfo> customDebugInfos;
	}
}
