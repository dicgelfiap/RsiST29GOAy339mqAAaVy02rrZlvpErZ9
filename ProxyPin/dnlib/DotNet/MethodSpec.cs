using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using dnlib.DotNet.MD;
using dnlib.DotNet.Pdb;

namespace dnlib.DotNet
{
	// Token: 0x02000816 RID: 2070
	[ComVisible(true)]
	public abstract class MethodSpec : IHasCustomAttribute, ICodedToken, IMDTokenProvider, IHasCustomDebugInformation, IMethod, ITokenOperand, IFullName, IGenericParameterProvider, IIsTypeOrMethod, IMemberRef, IOwnerModule, IContainsGenericParameter
	{
		// Token: 0x17000EDF RID: 3807
		// (get) Token: 0x06004B76 RID: 19318 RVA: 0x0017E418 File Offset: 0x0017E418
		public MDToken MDToken
		{
			get
			{
				return new MDToken(Table.MethodSpec, this.rid);
			}
		}

		// Token: 0x17000EE0 RID: 3808
		// (get) Token: 0x06004B77 RID: 19319 RVA: 0x0017E428 File Offset: 0x0017E428
		// (set) Token: 0x06004B78 RID: 19320 RVA: 0x0017E430 File Offset: 0x0017E430
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

		// Token: 0x17000EE1 RID: 3809
		// (get) Token: 0x06004B79 RID: 19321 RVA: 0x0017E43C File Offset: 0x0017E43C
		public int HasCustomAttributeTag
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17000EE2 RID: 3810
		// (get) Token: 0x06004B7A RID: 19322 RVA: 0x0017E440 File Offset: 0x0017E440
		// (set) Token: 0x06004B7B RID: 19323 RVA: 0x0017E448 File Offset: 0x0017E448
		public IMethodDefOrRef Method
		{
			get
			{
				return this.method;
			}
			set
			{
				this.method = value;
			}
		}

		// Token: 0x17000EE3 RID: 3811
		// (get) Token: 0x06004B7C RID: 19324 RVA: 0x0017E454 File Offset: 0x0017E454
		// (set) Token: 0x06004B7D RID: 19325 RVA: 0x0017E45C File Offset: 0x0017E45C
		public CallingConventionSig Instantiation
		{
			get
			{
				return this.instantiation;
			}
			set
			{
				this.instantiation = value;
			}
		}

		// Token: 0x17000EE4 RID: 3812
		// (get) Token: 0x06004B7E RID: 19326 RVA: 0x0017E468 File Offset: 0x0017E468
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

		// Token: 0x06004B7F RID: 19327 RVA: 0x0017E484 File Offset: 0x0017E484
		protected virtual void InitializeCustomAttributes()
		{
			Interlocked.CompareExchange<CustomAttributeCollection>(ref this.customAttributes, new CustomAttributeCollection(), null);
		}

		// Token: 0x17000EE5 RID: 3813
		// (get) Token: 0x06004B80 RID: 19328 RVA: 0x0017E498 File Offset: 0x0017E498
		public bool HasCustomAttributes
		{
			get
			{
				return this.CustomAttributes.Count > 0;
			}
		}

		// Token: 0x17000EE6 RID: 3814
		// (get) Token: 0x06004B81 RID: 19329 RVA: 0x0017E4A8 File Offset: 0x0017E4A8
		public int HasCustomDebugInformationTag
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17000EE7 RID: 3815
		// (get) Token: 0x06004B82 RID: 19330 RVA: 0x0017E4AC File Offset: 0x0017E4AC
		public bool HasCustomDebugInfos
		{
			get
			{
				return this.CustomDebugInfos.Count > 0;
			}
		}

		// Token: 0x17000EE8 RID: 3816
		// (get) Token: 0x06004B83 RID: 19331 RVA: 0x0017E4BC File Offset: 0x0017E4BC
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

		// Token: 0x06004B84 RID: 19332 RVA: 0x0017E4D8 File Offset: 0x0017E4D8
		protected virtual void InitializeCustomDebugInfos()
		{
			Interlocked.CompareExchange<IList<PdbCustomDebugInfo>>(ref this.customDebugInfos, new List<PdbCustomDebugInfo>(), null);
		}

		// Token: 0x17000EE9 RID: 3817
		// (get) Token: 0x06004B85 RID: 19333 RVA: 0x0017E4EC File Offset: 0x0017E4EC
		// (set) Token: 0x06004B86 RID: 19334 RVA: 0x0017E504 File Offset: 0x0017E504
		MethodSig IMethod.MethodSig
		{
			get
			{
				IMethodDefOrRef methodDefOrRef = this.method;
				if (methodDefOrRef == null)
				{
					return null;
				}
				return methodDefOrRef.MethodSig;
			}
			set
			{
				IMethodDefOrRef methodDefOrRef = this.method;
				if (methodDefOrRef != null)
				{
					methodDefOrRef.MethodSig = value;
				}
			}
		}

		// Token: 0x17000EEA RID: 3818
		// (get) Token: 0x06004B87 RID: 19335 RVA: 0x0017E52C File Offset: 0x0017E52C
		// (set) Token: 0x06004B88 RID: 19336 RVA: 0x0017E558 File Offset: 0x0017E558
		public UTF8String Name
		{
			get
			{
				IMethodDefOrRef methodDefOrRef = this.method;
				if (methodDefOrRef != null)
				{
					return methodDefOrRef.Name;
				}
				return UTF8String.Empty;
			}
			set
			{
				IMethodDefOrRef methodDefOrRef = this.method;
				if (methodDefOrRef != null)
				{
					methodDefOrRef.Name = value;
				}
			}
		}

		// Token: 0x17000EEB RID: 3819
		// (get) Token: 0x06004B89 RID: 19337 RVA: 0x0017E580 File Offset: 0x0017E580
		public ITypeDefOrRef DeclaringType
		{
			get
			{
				IMethodDefOrRef methodDefOrRef = this.method;
				if (methodDefOrRef == null)
				{
					return null;
				}
				return methodDefOrRef.DeclaringType;
			}
		}

		// Token: 0x17000EEC RID: 3820
		// (get) Token: 0x06004B8A RID: 19338 RVA: 0x0017E598 File Offset: 0x0017E598
		// (set) Token: 0x06004B8B RID: 19339 RVA: 0x0017E5A8 File Offset: 0x0017E5A8
		public GenericInstMethodSig GenericInstMethodSig
		{
			get
			{
				return this.instantiation as GenericInstMethodSig;
			}
			set
			{
				this.instantiation = value;
			}
		}

		// Token: 0x17000EED RID: 3821
		// (get) Token: 0x06004B8C RID: 19340 RVA: 0x0017E5B4 File Offset: 0x0017E5B4
		int IGenericParameterProvider.NumberOfGenericParameters
		{
			get
			{
				GenericInstMethodSig genericInstMethodSig = this.GenericInstMethodSig;
				if (genericInstMethodSig == null)
				{
					return 0;
				}
				return genericInstMethodSig.GenericArguments.Count;
			}
		}

		// Token: 0x17000EEE RID: 3822
		// (get) Token: 0x06004B8D RID: 19341 RVA: 0x0017E5D0 File Offset: 0x0017E5D0
		public ModuleDef Module
		{
			get
			{
				IMethodDefOrRef methodDefOrRef = this.method;
				if (methodDefOrRef == null)
				{
					return null;
				}
				return methodDefOrRef.Module;
			}
		}

		// Token: 0x17000EEF RID: 3823
		// (get) Token: 0x06004B8E RID: 19342 RVA: 0x0017E5E8 File Offset: 0x0017E5E8
		public string FullName
		{
			get
			{
				GenericInstMethodSig genericInstMethodSig = this.GenericInstMethodSig;
				IList<TypeSig> methodGenArgs = (genericInstMethodSig != null) ? genericInstMethodSig.GenericArguments : null;
				IMethodDefOrRef methodDefOrRef = this.method;
				MethodDef methodDef = methodDefOrRef as MethodDef;
				if (methodDef != null)
				{
					TypeDef declaringType = methodDef.DeclaringType;
					return FullNameFactory.MethodFullName((declaringType != null) ? declaringType.FullName : null, methodDef.Name, methodDef.MethodSig, null, methodGenArgs, null, null);
				}
				MemberRef memberRef = methodDefOrRef as MemberRef;
				if (memberRef != null)
				{
					MethodSig methodSig = memberRef.MethodSig;
					if (methodSig != null)
					{
						TypeSpec typeSpec = memberRef.Class as TypeSpec;
						GenericInstSig genericInstSig = ((typeSpec != null) ? typeSpec.TypeSig : null) as GenericInstSig;
						IList<TypeSig> typeGenArgs = (genericInstSig != null) ? genericInstSig.GenericArguments : null;
						return FullNameFactory.MethodFullName(memberRef.GetDeclaringTypeFullName(), memberRef.Name, methodSig, typeGenArgs, methodGenArgs, null, null);
					}
				}
				return string.Empty;
			}
		}

		// Token: 0x17000EF0 RID: 3824
		// (get) Token: 0x06004B8F RID: 19343 RVA: 0x0017E6D0 File Offset: 0x0017E6D0
		bool IIsTypeOrMethod.IsType
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000EF1 RID: 3825
		// (get) Token: 0x06004B90 RID: 19344 RVA: 0x0017E6D4 File Offset: 0x0017E6D4
		bool IIsTypeOrMethod.IsMethod
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000EF2 RID: 3826
		// (get) Token: 0x06004B91 RID: 19345 RVA: 0x0017E6D8 File Offset: 0x0017E6D8
		bool IMemberRef.IsField
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000EF3 RID: 3827
		// (get) Token: 0x06004B92 RID: 19346 RVA: 0x0017E6DC File Offset: 0x0017E6DC
		bool IMemberRef.IsTypeSpec
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000EF4 RID: 3828
		// (get) Token: 0x06004B93 RID: 19347 RVA: 0x0017E6E0 File Offset: 0x0017E6E0
		bool IMemberRef.IsTypeRef
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000EF5 RID: 3829
		// (get) Token: 0x06004B94 RID: 19348 RVA: 0x0017E6E4 File Offset: 0x0017E6E4
		bool IMemberRef.IsTypeDef
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000EF6 RID: 3830
		// (get) Token: 0x06004B95 RID: 19349 RVA: 0x0017E6E8 File Offset: 0x0017E6E8
		bool IMemberRef.IsMethodSpec
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000EF7 RID: 3831
		// (get) Token: 0x06004B96 RID: 19350 RVA: 0x0017E6EC File Offset: 0x0017E6EC
		bool IMemberRef.IsMethodDef
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000EF8 RID: 3832
		// (get) Token: 0x06004B97 RID: 19351 RVA: 0x0017E6F0 File Offset: 0x0017E6F0
		bool IMemberRef.IsMemberRef
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000EF9 RID: 3833
		// (get) Token: 0x06004B98 RID: 19352 RVA: 0x0017E6F4 File Offset: 0x0017E6F4
		bool IMemberRef.IsFieldDef
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000EFA RID: 3834
		// (get) Token: 0x06004B99 RID: 19353 RVA: 0x0017E6F8 File Offset: 0x0017E6F8
		bool IMemberRef.IsPropertyDef
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000EFB RID: 3835
		// (get) Token: 0x06004B9A RID: 19354 RVA: 0x0017E6FC File Offset: 0x0017E6FC
		bool IMemberRef.IsEventDef
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000EFC RID: 3836
		// (get) Token: 0x06004B9B RID: 19355 RVA: 0x0017E700 File Offset: 0x0017E700
		bool IMemberRef.IsGenericParam
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000EFD RID: 3837
		// (get) Token: 0x06004B9C RID: 19356 RVA: 0x0017E704 File Offset: 0x0017E704
		bool IContainsGenericParameter.ContainsGenericParameter
		{
			get
			{
				return TypeHelper.ContainsGenericParameter(this);
			}
		}

		// Token: 0x06004B9D RID: 19357 RVA: 0x0017E70C File Offset: 0x0017E70C
		public override string ToString()
		{
			return this.FullName;
		}

		// Token: 0x040025CC RID: 9676
		protected uint rid;

		// Token: 0x040025CD RID: 9677
		protected IMethodDefOrRef method;

		// Token: 0x040025CE RID: 9678
		protected CallingConventionSig instantiation;

		// Token: 0x040025CF RID: 9679
		protected CustomAttributeCollection customAttributes;

		// Token: 0x040025D0 RID: 9680
		protected IList<PdbCustomDebugInfo> customDebugInfos;
	}
}
