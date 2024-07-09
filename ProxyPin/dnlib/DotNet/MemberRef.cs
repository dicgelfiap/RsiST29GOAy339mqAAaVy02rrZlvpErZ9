using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using dnlib.DotNet.MD;
using dnlib.DotNet.Pdb;

namespace dnlib.DotNet
{
	// Token: 0x02000809 RID: 2057
	[ComVisible(true)]
	public abstract class MemberRef : IHasCustomAttribute, ICodedToken, IMDTokenProvider, IMethodDefOrRef, ICustomAttributeType, IMethod, ITokenOperand, IFullName, IGenericParameterProvider, IIsTypeOrMethod, IMemberRef, IOwnerModule, IField, IContainsGenericParameter, IHasCustomDebugInformation
	{
		// Token: 0x17000E44 RID: 3652
		// (get) Token: 0x06004A44 RID: 19012 RVA: 0x0017C1B8 File Offset: 0x0017C1B8
		public MDToken MDToken
		{
			get
			{
				return new MDToken(Table.MemberRef, this.rid);
			}
		}

		// Token: 0x17000E45 RID: 3653
		// (get) Token: 0x06004A45 RID: 19013 RVA: 0x0017C1C8 File Offset: 0x0017C1C8
		// (set) Token: 0x06004A46 RID: 19014 RVA: 0x0017C1D0 File Offset: 0x0017C1D0
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

		// Token: 0x17000E46 RID: 3654
		// (get) Token: 0x06004A47 RID: 19015 RVA: 0x0017C1DC File Offset: 0x0017C1DC
		public int HasCustomAttributeTag
		{
			get
			{
				return 6;
			}
		}

		// Token: 0x17000E47 RID: 3655
		// (get) Token: 0x06004A48 RID: 19016 RVA: 0x0017C1E0 File Offset: 0x0017C1E0
		public int MethodDefOrRefTag
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000E48 RID: 3656
		// (get) Token: 0x06004A49 RID: 19017 RVA: 0x0017C1E4 File Offset: 0x0017C1E4
		public int CustomAttributeTypeTag
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x17000E49 RID: 3657
		// (get) Token: 0x06004A4A RID: 19018 RVA: 0x0017C1E8 File Offset: 0x0017C1E8
		// (set) Token: 0x06004A4B RID: 19019 RVA: 0x0017C1F0 File Offset: 0x0017C1F0
		public IMemberRefParent Class
		{
			get
			{
				return this.@class;
			}
			set
			{
				this.@class = value;
			}
		}

		// Token: 0x17000E4A RID: 3658
		// (get) Token: 0x06004A4C RID: 19020 RVA: 0x0017C1FC File Offset: 0x0017C1FC
		// (set) Token: 0x06004A4D RID: 19021 RVA: 0x0017C204 File Offset: 0x0017C204
		public UTF8String Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x17000E4B RID: 3659
		// (get) Token: 0x06004A4E RID: 19022 RVA: 0x0017C210 File Offset: 0x0017C210
		// (set) Token: 0x06004A4F RID: 19023 RVA: 0x0017C218 File Offset: 0x0017C218
		public CallingConventionSig Signature
		{
			get
			{
				return this.signature;
			}
			set
			{
				this.signature = value;
			}
		}

		// Token: 0x17000E4C RID: 3660
		// (get) Token: 0x06004A50 RID: 19024 RVA: 0x0017C224 File Offset: 0x0017C224
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

		// Token: 0x06004A51 RID: 19025 RVA: 0x0017C240 File Offset: 0x0017C240
		protected virtual void InitializeCustomAttributes()
		{
			Interlocked.CompareExchange<CustomAttributeCollection>(ref this.customAttributes, new CustomAttributeCollection(), null);
		}

		// Token: 0x17000E4D RID: 3661
		// (get) Token: 0x06004A52 RID: 19026 RVA: 0x0017C254 File Offset: 0x0017C254
		public bool HasCustomAttributes
		{
			get
			{
				return this.CustomAttributes.Count > 0;
			}
		}

		// Token: 0x17000E4E RID: 3662
		// (get) Token: 0x06004A53 RID: 19027 RVA: 0x0017C264 File Offset: 0x0017C264
		public int HasCustomDebugInformationTag
		{
			get
			{
				return 6;
			}
		}

		// Token: 0x17000E4F RID: 3663
		// (get) Token: 0x06004A54 RID: 19028 RVA: 0x0017C268 File Offset: 0x0017C268
		public bool HasCustomDebugInfos
		{
			get
			{
				return this.CustomDebugInfos.Count > 0;
			}
		}

		// Token: 0x17000E50 RID: 3664
		// (get) Token: 0x06004A55 RID: 19029 RVA: 0x0017C278 File Offset: 0x0017C278
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

		// Token: 0x06004A56 RID: 19030 RVA: 0x0017C294 File Offset: 0x0017C294
		protected virtual void InitializeCustomDebugInfos()
		{
			Interlocked.CompareExchange<IList<PdbCustomDebugInfo>>(ref this.customDebugInfos, new List<PdbCustomDebugInfo>(), null);
		}

		// Token: 0x17000E51 RID: 3665
		// (get) Token: 0x06004A57 RID: 19031 RVA: 0x0017C2A8 File Offset: 0x0017C2A8
		public ITypeDefOrRef DeclaringType
		{
			get
			{
				IMemberRefParent memberRefParent = this.@class;
				ITypeDefOrRef typeDefOrRef = memberRefParent as ITypeDefOrRef;
				if (typeDefOrRef != null)
				{
					return typeDefOrRef;
				}
				MethodDef methodDef = memberRefParent as MethodDef;
				if (methodDef != null)
				{
					return methodDef.DeclaringType;
				}
				ModuleRef moduleRef = memberRefParent as ModuleRef;
				if (moduleRef == null)
				{
					return null;
				}
				TypeRefUser globalTypeRef = this.GetGlobalTypeRef(moduleRef);
				if (this.module != null)
				{
					return this.module.UpdateRowId<TypeRefUser>(globalTypeRef);
				}
				return globalTypeRef;
			}
		}

		// Token: 0x06004A58 RID: 19032 RVA: 0x0017C318 File Offset: 0x0017C318
		private TypeRefUser GetGlobalTypeRef(ModuleRef mr)
		{
			if (this.module == null)
			{
				return this.CreateDefaultGlobalTypeRef(mr);
			}
			TypeDef globalType = this.module.GlobalType;
			if (globalType != null && default(SigComparer).Equals(this.module, mr))
			{
				return new TypeRefUser(this.module, globalType.Namespace, globalType.Name, mr);
			}
			AssemblyDef assembly = this.module.Assembly;
			if (assembly == null)
			{
				return this.CreateDefaultGlobalTypeRef(mr);
			}
			ModuleDef moduleDef = assembly.FindModule(mr.Name);
			if (moduleDef == null)
			{
				return this.CreateDefaultGlobalTypeRef(mr);
			}
			globalType = moduleDef.GlobalType;
			if (globalType == null)
			{
				return this.CreateDefaultGlobalTypeRef(mr);
			}
			return new TypeRefUser(this.module, globalType.Namespace, globalType.Name, mr);
		}

		// Token: 0x06004A59 RID: 19033 RVA: 0x0017C3E4 File Offset: 0x0017C3E4
		private TypeRefUser CreateDefaultGlobalTypeRef(ModuleRef mr)
		{
			TypeRefUser typeRefUser = new TypeRefUser(this.module, string.Empty, "<Module>", mr);
			if (this.module != null)
			{
				this.module.UpdateRowId<TypeRefUser>(typeRefUser);
			}
			return typeRefUser;
		}

		// Token: 0x17000E52 RID: 3666
		// (get) Token: 0x06004A5A RID: 19034 RVA: 0x0017C430 File Offset: 0x0017C430
		bool IIsTypeOrMethod.IsType
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000E53 RID: 3667
		// (get) Token: 0x06004A5B RID: 19035 RVA: 0x0017C434 File Offset: 0x0017C434
		bool IIsTypeOrMethod.IsMethod
		{
			get
			{
				return this.IsMethodRef;
			}
		}

		// Token: 0x17000E54 RID: 3668
		// (get) Token: 0x06004A5C RID: 19036 RVA: 0x0017C43C File Offset: 0x0017C43C
		bool IMemberRef.IsField
		{
			get
			{
				return this.IsFieldRef;
			}
		}

		// Token: 0x17000E55 RID: 3669
		// (get) Token: 0x06004A5D RID: 19037 RVA: 0x0017C444 File Offset: 0x0017C444
		bool IMemberRef.IsTypeSpec
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000E56 RID: 3670
		// (get) Token: 0x06004A5E RID: 19038 RVA: 0x0017C448 File Offset: 0x0017C448
		bool IMemberRef.IsTypeRef
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000E57 RID: 3671
		// (get) Token: 0x06004A5F RID: 19039 RVA: 0x0017C44C File Offset: 0x0017C44C
		bool IMemberRef.IsTypeDef
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000E58 RID: 3672
		// (get) Token: 0x06004A60 RID: 19040 RVA: 0x0017C450 File Offset: 0x0017C450
		bool IMemberRef.IsMethodSpec
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000E59 RID: 3673
		// (get) Token: 0x06004A61 RID: 19041 RVA: 0x0017C454 File Offset: 0x0017C454
		bool IMemberRef.IsMethodDef
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000E5A RID: 3674
		// (get) Token: 0x06004A62 RID: 19042 RVA: 0x0017C458 File Offset: 0x0017C458
		bool IMemberRef.IsMemberRef
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000E5B RID: 3675
		// (get) Token: 0x06004A63 RID: 19043 RVA: 0x0017C45C File Offset: 0x0017C45C
		bool IMemberRef.IsFieldDef
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000E5C RID: 3676
		// (get) Token: 0x06004A64 RID: 19044 RVA: 0x0017C460 File Offset: 0x0017C460
		bool IMemberRef.IsPropertyDef
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000E5D RID: 3677
		// (get) Token: 0x06004A65 RID: 19045 RVA: 0x0017C464 File Offset: 0x0017C464
		bool IMemberRef.IsEventDef
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000E5E RID: 3678
		// (get) Token: 0x06004A66 RID: 19046 RVA: 0x0017C468 File Offset: 0x0017C468
		bool IMemberRef.IsGenericParam
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000E5F RID: 3679
		// (get) Token: 0x06004A67 RID: 19047 RVA: 0x0017C46C File Offset: 0x0017C46C
		public bool IsMethodRef
		{
			get
			{
				return this.MethodSig != null;
			}
		}

		// Token: 0x17000E60 RID: 3680
		// (get) Token: 0x06004A68 RID: 19048 RVA: 0x0017C47C File Offset: 0x0017C47C
		public bool IsFieldRef
		{
			get
			{
				return this.FieldSig != null;
			}
		}

		// Token: 0x17000E61 RID: 3681
		// (get) Token: 0x06004A69 RID: 19049 RVA: 0x0017C48C File Offset: 0x0017C48C
		// (set) Token: 0x06004A6A RID: 19050 RVA: 0x0017C49C File Offset: 0x0017C49C
		public MethodSig MethodSig
		{
			get
			{
				return this.signature as MethodSig;
			}
			set
			{
				this.signature = value;
			}
		}

		// Token: 0x17000E62 RID: 3682
		// (get) Token: 0x06004A6B RID: 19051 RVA: 0x0017C4A8 File Offset: 0x0017C4A8
		// (set) Token: 0x06004A6C RID: 19052 RVA: 0x0017C4B8 File Offset: 0x0017C4B8
		public FieldSig FieldSig
		{
			get
			{
				return this.signature as FieldSig;
			}
			set
			{
				this.signature = value;
			}
		}

		// Token: 0x17000E63 RID: 3683
		// (get) Token: 0x06004A6D RID: 19053 RVA: 0x0017C4C4 File Offset: 0x0017C4C4
		public ModuleDef Module
		{
			get
			{
				return this.module;
			}
		}

		// Token: 0x17000E64 RID: 3684
		// (get) Token: 0x06004A6E RID: 19054 RVA: 0x0017C4CC File Offset: 0x0017C4CC
		public bool HasThis
		{
			get
			{
				MethodSig methodSig = this.MethodSig;
				return methodSig != null && methodSig.HasThis;
			}
		}

		// Token: 0x17000E65 RID: 3685
		// (get) Token: 0x06004A6F RID: 19055 RVA: 0x0017C4F4 File Offset: 0x0017C4F4
		public bool ExplicitThis
		{
			get
			{
				MethodSig methodSig = this.MethodSig;
				return methodSig != null && methodSig.ExplicitThis;
			}
		}

		// Token: 0x17000E66 RID: 3686
		// (get) Token: 0x06004A70 RID: 19056 RVA: 0x0017C51C File Offset: 0x0017C51C
		public CallingConvention CallingConvention
		{
			get
			{
				MethodSig methodSig = this.MethodSig;
				if (methodSig != null)
				{
					return methodSig.CallingConvention & CallingConvention.Mask;
				}
				return CallingConvention.Default;
			}
		}

		// Token: 0x17000E67 RID: 3687
		// (get) Token: 0x06004A71 RID: 19057 RVA: 0x0017C548 File Offset: 0x0017C548
		// (set) Token: 0x06004A72 RID: 19058 RVA: 0x0017C560 File Offset: 0x0017C560
		public TypeSig ReturnType
		{
			get
			{
				MethodSig methodSig = this.MethodSig;
				if (methodSig == null)
				{
					return null;
				}
				return methodSig.RetType;
			}
			set
			{
				MethodSig methodSig = this.MethodSig;
				if (methodSig != null)
				{
					methodSig.RetType = value;
				}
			}
		}

		// Token: 0x17000E68 RID: 3688
		// (get) Token: 0x06004A73 RID: 19059 RVA: 0x0017C588 File Offset: 0x0017C588
		int IGenericParameterProvider.NumberOfGenericParameters
		{
			get
			{
				MethodSig methodSig = this.MethodSig;
				if (methodSig == null)
				{
					return 0;
				}
				return (int)methodSig.GenParamCount;
			}
		}

		// Token: 0x17000E69 RID: 3689
		// (get) Token: 0x06004A74 RID: 19060 RVA: 0x0017C5A0 File Offset: 0x0017C5A0
		public string FullName
		{
			get
			{
				IMemberRefParent memberRefParent = this.@class;
				IList<TypeSig> typeGenArgs = null;
				if (memberRefParent is TypeSpec)
				{
					GenericInstSig genericInstSig = ((TypeSpec)memberRefParent).TypeSig as GenericInstSig;
					if (genericInstSig != null)
					{
						typeGenArgs = genericInstSig.GenericArguments;
					}
				}
				MethodSig methodSig = this.MethodSig;
				if (methodSig != null)
				{
					return FullNameFactory.MethodFullName(this.GetDeclaringTypeFullName(memberRefParent), this.name, methodSig, typeGenArgs, null, null, null);
				}
				FieldSig fieldSig = this.FieldSig;
				if (fieldSig != null)
				{
					return FullNameFactory.FieldFullName(this.GetDeclaringTypeFullName(memberRefParent), this.name, fieldSig, typeGenArgs, null);
				}
				return string.Empty;
			}
		}

		// Token: 0x06004A75 RID: 19061 RVA: 0x0017C640 File Offset: 0x0017C640
		public string GetDeclaringTypeFullName()
		{
			return this.GetDeclaringTypeFullName(this.@class);
		}

		// Token: 0x06004A76 RID: 19062 RVA: 0x0017C650 File Offset: 0x0017C650
		private string GetDeclaringTypeFullName(IMemberRefParent parent)
		{
			if (parent == null)
			{
				return null;
			}
			if (parent is ITypeDefOrRef)
			{
				return ((ITypeDefOrRef)parent).FullName;
			}
			if (parent is ModuleRef)
			{
				return "[module:" + ((ModuleRef)parent).ToString() + "]<Module>";
			}
			if (!(parent is MethodDef))
			{
				return null;
			}
			TypeDef declaringType = ((MethodDef)parent).DeclaringType;
			if (declaringType == null)
			{
				return null;
			}
			return declaringType.FullName;
		}

		// Token: 0x06004A77 RID: 19063 RVA: 0x0017C6CC File Offset: 0x0017C6CC
		public IMemberForwarded Resolve()
		{
			if (this.module == null)
			{
				return null;
			}
			return this.module.Context.Resolver.Resolve(this);
		}

		// Token: 0x06004A78 RID: 19064 RVA: 0x0017C6F4 File Offset: 0x0017C6F4
		public IMemberForwarded ResolveThrow()
		{
			IMemberForwarded memberForwarded = this.Resolve();
			if (memberForwarded != null)
			{
				return memberForwarded;
			}
			throw new MemberRefResolveException(string.Format("Could not resolve method/field: {0} ({1})", this, this.GetDefinitionAssembly()));
		}

		// Token: 0x06004A79 RID: 19065 RVA: 0x0017C72C File Offset: 0x0017C72C
		public FieldDef ResolveField()
		{
			return this.Resolve() as FieldDef;
		}

		// Token: 0x06004A7A RID: 19066 RVA: 0x0017C73C File Offset: 0x0017C73C
		public FieldDef ResolveFieldThrow()
		{
			FieldDef fieldDef = this.ResolveField();
			if (fieldDef != null)
			{
				return fieldDef;
			}
			throw new MemberRefResolveException(string.Format("Could not resolve field: {0} ({1})", this, this.GetDefinitionAssembly()));
		}

		// Token: 0x06004A7B RID: 19067 RVA: 0x0017C774 File Offset: 0x0017C774
		public MethodDef ResolveMethod()
		{
			return this.Resolve() as MethodDef;
		}

		// Token: 0x06004A7C RID: 19068 RVA: 0x0017C784 File Offset: 0x0017C784
		public MethodDef ResolveMethodThrow()
		{
			MethodDef methodDef = this.ResolveMethod();
			if (methodDef != null)
			{
				return methodDef;
			}
			throw new MemberRefResolveException(string.Format("Could not resolve method: {0} ({1})", this, this.GetDefinitionAssembly()));
		}

		// Token: 0x17000E6A RID: 3690
		// (get) Token: 0x06004A7D RID: 19069 RVA: 0x0017C7BC File Offset: 0x0017C7BC
		bool IContainsGenericParameter.ContainsGenericParameter
		{
			get
			{
				return TypeHelper.ContainsGenericParameter(this);
			}
		}

		// Token: 0x06004A7E RID: 19070 RVA: 0x0017C7C4 File Offset: 0x0017C7C4
		protected static GenericParamContext GetSignatureGenericParamContext(GenericParamContext gpContext, IMemberRefParent @class)
		{
			TypeDef type = null;
			MethodDef method = gpContext.Method;
			TypeSpec typeSpec = @class as TypeSpec;
			if (typeSpec != null)
			{
				GenericInstSig genericInstSig = typeSpec.TypeSig as GenericInstSig;
				if (genericInstSig != null)
				{
					type = genericInstSig.GenericType.ToTypeDefOrRef().ResolveTypeDef();
				}
			}
			return new GenericParamContext(type, method);
		}

		// Token: 0x06004A7F RID: 19071 RVA: 0x0017C818 File Offset: 0x0017C818
		public override string ToString()
		{
			return this.FullName;
		}

		// Token: 0x04002567 RID: 9575
		protected uint rid;

		// Token: 0x04002568 RID: 9576
		protected ModuleDef module;

		// Token: 0x04002569 RID: 9577
		protected IMemberRefParent @class;

		// Token: 0x0400256A RID: 9578
		protected UTF8String name;

		// Token: 0x0400256B RID: 9579
		protected CallingConventionSig signature;

		// Token: 0x0400256C RID: 9580
		protected CustomAttributeCollection customAttributes;

		// Token: 0x0400256D RID: 9581
		protected IList<PdbCustomDebugInfo> customDebugInfos;
	}
}
