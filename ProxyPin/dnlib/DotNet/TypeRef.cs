using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using dnlib.DotNet.MD;
using dnlib.DotNet.Pdb;
using dnlib.Threading;

namespace dnlib.DotNet
{
	// Token: 0x02000863 RID: 2147
	[ComVisible(true)]
	public abstract class TypeRef : ITypeDefOrRef, ICodedToken, IMDTokenProvider, IHasCustomAttribute, IMemberRefParent, IFullName, IType, IOwnerModule, IGenericParameterProvider, IIsTypeOrMethod, IContainsGenericParameter, ITokenOperand, IMemberRef, IHasCustomDebugInformation, IResolutionScope
	{
		// Token: 0x1700108B RID: 4235
		// (get) Token: 0x06005221 RID: 21025 RVA: 0x001958F0 File Offset: 0x001958F0
		public MDToken MDToken
		{
			get
			{
				return new MDToken(Table.TypeRef, this.rid);
			}
		}

		// Token: 0x1700108C RID: 4236
		// (get) Token: 0x06005222 RID: 21026 RVA: 0x00195900 File Offset: 0x00195900
		// (set) Token: 0x06005223 RID: 21027 RVA: 0x00195908 File Offset: 0x00195908
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

		// Token: 0x1700108D RID: 4237
		// (get) Token: 0x06005224 RID: 21028 RVA: 0x00195914 File Offset: 0x00195914
		public int TypeDefOrRefTag
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x1700108E RID: 4238
		// (get) Token: 0x06005225 RID: 21029 RVA: 0x00195918 File Offset: 0x00195918
		public int HasCustomAttributeTag
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x1700108F RID: 4239
		// (get) Token: 0x06005226 RID: 21030 RVA: 0x0019591C File Offset: 0x0019591C
		public int MemberRefParentTag
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17001090 RID: 4240
		// (get) Token: 0x06005227 RID: 21031 RVA: 0x00195920 File Offset: 0x00195920
		public int ResolutionScopeTag
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x17001091 RID: 4241
		// (get) Token: 0x06005228 RID: 21032 RVA: 0x00195924 File Offset: 0x00195924
		int IGenericParameterProvider.NumberOfGenericParameters
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17001092 RID: 4242
		// (get) Token: 0x06005229 RID: 21033 RVA: 0x00195928 File Offset: 0x00195928
		string IType.TypeName
		{
			get
			{
				return FullNameFactory.Name(this, false, null);
			}
		}

		// Token: 0x17001093 RID: 4243
		// (get) Token: 0x0600522A RID: 21034 RVA: 0x00195934 File Offset: 0x00195934
		public string ReflectionName
		{
			get
			{
				return FullNameFactory.Name(this, true, null);
			}
		}

		// Token: 0x17001094 RID: 4244
		// (get) Token: 0x0600522B RID: 21035 RVA: 0x00195940 File Offset: 0x00195940
		string IType.Namespace
		{
			get
			{
				return FullNameFactory.Namespace(this, false, null);
			}
		}

		// Token: 0x17001095 RID: 4245
		// (get) Token: 0x0600522C RID: 21036 RVA: 0x0019594C File Offset: 0x0019594C
		public string ReflectionNamespace
		{
			get
			{
				return FullNameFactory.Namespace(this, true, null);
			}
		}

		// Token: 0x17001096 RID: 4246
		// (get) Token: 0x0600522D RID: 21037 RVA: 0x00195958 File Offset: 0x00195958
		public string FullName
		{
			get
			{
				return FullNameFactory.FullName(this, false, null, null);
			}
		}

		// Token: 0x17001097 RID: 4247
		// (get) Token: 0x0600522E RID: 21038 RVA: 0x00195964 File Offset: 0x00195964
		public string ReflectionFullName
		{
			get
			{
				return FullNameFactory.FullName(this, true, null, null);
			}
		}

		// Token: 0x17001098 RID: 4248
		// (get) Token: 0x0600522F RID: 21039 RVA: 0x00195970 File Offset: 0x00195970
		public string AssemblyQualifiedName
		{
			get
			{
				return FullNameFactory.AssemblyQualifiedName(this, null, null);
			}
		}

		// Token: 0x17001099 RID: 4249
		// (get) Token: 0x06005230 RID: 21040 RVA: 0x0019597C File Offset: 0x0019597C
		public IAssembly DefinitionAssembly
		{
			get
			{
				return FullNameFactory.DefinitionAssembly(this);
			}
		}

		// Token: 0x1700109A RID: 4250
		// (get) Token: 0x06005231 RID: 21041 RVA: 0x00195984 File Offset: 0x00195984
		public IScope Scope
		{
			get
			{
				return FullNameFactory.Scope(this);
			}
		}

		// Token: 0x1700109B RID: 4251
		// (get) Token: 0x06005232 RID: 21042 RVA: 0x0019598C File Offset: 0x0019598C
		public ITypeDefOrRef ScopeType
		{
			get
			{
				return this;
			}
		}

		// Token: 0x1700109C RID: 4252
		// (get) Token: 0x06005233 RID: 21043 RVA: 0x00195990 File Offset: 0x00195990
		public bool ContainsGenericParameter
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700109D RID: 4253
		// (get) Token: 0x06005234 RID: 21044 RVA: 0x00195994 File Offset: 0x00195994
		public ModuleDef Module
		{
			get
			{
				return this.module;
			}
		}

		// Token: 0x1700109E RID: 4254
		// (get) Token: 0x06005235 RID: 21045 RVA: 0x0019599C File Offset: 0x0019599C
		// (set) Token: 0x06005236 RID: 21046 RVA: 0x001959B8 File Offset: 0x001959B8
		public IResolutionScope ResolutionScope
		{
			get
			{
				if (!this.resolutionScope_isInitialized)
				{
					this.InitializeResolutionScope();
				}
				return this.resolutionScope;
			}
			set
			{
				this.theLock.EnterWriteLock();
				try
				{
					this.resolutionScope = value;
					this.resolutionScope_isInitialized = true;
				}
				finally
				{
					this.theLock.ExitWriteLock();
				}
			}
		}

		// Token: 0x06005237 RID: 21047 RVA: 0x00195A00 File Offset: 0x00195A00
		private void InitializeResolutionScope()
		{
			this.theLock.EnterWriteLock();
			try
			{
				if (!this.resolutionScope_isInitialized)
				{
					this.resolutionScope = this.GetResolutionScope_NoLock();
					this.resolutionScope_isInitialized = true;
				}
			}
			finally
			{
				this.theLock.ExitWriteLock();
			}
		}

		// Token: 0x06005238 RID: 21048 RVA: 0x00195A5C File Offset: 0x00195A5C
		protected virtual IResolutionScope GetResolutionScope_NoLock()
		{
			return null;
		}

		// Token: 0x1700109F RID: 4255
		// (get) Token: 0x06005239 RID: 21049 RVA: 0x00195A60 File Offset: 0x00195A60
		// (set) Token: 0x0600523A RID: 21050 RVA: 0x00195A68 File Offset: 0x00195A68
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

		// Token: 0x170010A0 RID: 4256
		// (get) Token: 0x0600523B RID: 21051 RVA: 0x00195A74 File Offset: 0x00195A74
		// (set) Token: 0x0600523C RID: 21052 RVA: 0x00195A7C File Offset: 0x00195A7C
		public UTF8String Namespace
		{
			get
			{
				return this.@namespace;
			}
			set
			{
				this.@namespace = value;
			}
		}

		// Token: 0x170010A1 RID: 4257
		// (get) Token: 0x0600523D RID: 21053 RVA: 0x00195A88 File Offset: 0x00195A88
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

		// Token: 0x0600523E RID: 21054 RVA: 0x00195AA4 File Offset: 0x00195AA4
		protected virtual void InitializeCustomAttributes()
		{
			Interlocked.CompareExchange<CustomAttributeCollection>(ref this.customAttributes, new CustomAttributeCollection(), null);
		}

		// Token: 0x170010A2 RID: 4258
		// (get) Token: 0x0600523F RID: 21055 RVA: 0x00195AB8 File Offset: 0x00195AB8
		public bool HasCustomAttributes
		{
			get
			{
				return this.CustomAttributes.Count > 0;
			}
		}

		// Token: 0x170010A3 RID: 4259
		// (get) Token: 0x06005240 RID: 21056 RVA: 0x00195AC8 File Offset: 0x00195AC8
		public int HasCustomDebugInformationTag
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x170010A4 RID: 4260
		// (get) Token: 0x06005241 RID: 21057 RVA: 0x00195ACC File Offset: 0x00195ACC
		public bool HasCustomDebugInfos
		{
			get
			{
				return this.CustomDebugInfos.Count > 0;
			}
		}

		// Token: 0x170010A5 RID: 4261
		// (get) Token: 0x06005242 RID: 21058 RVA: 0x00195ADC File Offset: 0x00195ADC
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

		// Token: 0x06005243 RID: 21059 RVA: 0x00195AF8 File Offset: 0x00195AF8
		protected virtual void InitializeCustomDebugInfos()
		{
			Interlocked.CompareExchange<IList<PdbCustomDebugInfo>>(ref this.customDebugInfos, new List<PdbCustomDebugInfo>(), null);
		}

		// Token: 0x170010A6 RID: 4262
		// (get) Token: 0x06005244 RID: 21060 RVA: 0x00195B0C File Offset: 0x00195B0C
		public bool IsNested
		{
			get
			{
				return this.DeclaringType != null;
			}
		}

		// Token: 0x170010A7 RID: 4263
		// (get) Token: 0x06005245 RID: 21061 RVA: 0x00195B1C File Offset: 0x00195B1C
		public bool IsValueType
		{
			get
			{
				TypeDef typeDef = this.Resolve();
				return typeDef != null && typeDef.IsValueType;
			}
		}

		// Token: 0x170010A8 RID: 4264
		// (get) Token: 0x06005246 RID: 21062 RVA: 0x00195B44 File Offset: 0x00195B44
		public bool IsPrimitive
		{
			get
			{
				return this.IsPrimitive();
			}
		}

		// Token: 0x170010A9 RID: 4265
		// (get) Token: 0x06005247 RID: 21063 RVA: 0x00195B4C File Offset: 0x00195B4C
		public TypeRef DeclaringType
		{
			get
			{
				return this.ResolutionScope as TypeRef;
			}
		}

		// Token: 0x170010AA RID: 4266
		// (get) Token: 0x06005248 RID: 21064 RVA: 0x00195B5C File Offset: 0x00195B5C
		ITypeDefOrRef IMemberRef.DeclaringType
		{
			get
			{
				return this.DeclaringType;
			}
		}

		// Token: 0x170010AB RID: 4267
		// (get) Token: 0x06005249 RID: 21065 RVA: 0x00195B64 File Offset: 0x00195B64
		bool IIsTypeOrMethod.IsType
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170010AC RID: 4268
		// (get) Token: 0x0600524A RID: 21066 RVA: 0x00195B68 File Offset: 0x00195B68
		bool IIsTypeOrMethod.IsMethod
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170010AD RID: 4269
		// (get) Token: 0x0600524B RID: 21067 RVA: 0x00195B6C File Offset: 0x00195B6C
		bool IMemberRef.IsField
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170010AE RID: 4270
		// (get) Token: 0x0600524C RID: 21068 RVA: 0x00195B70 File Offset: 0x00195B70
		bool IMemberRef.IsTypeSpec
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170010AF RID: 4271
		// (get) Token: 0x0600524D RID: 21069 RVA: 0x00195B74 File Offset: 0x00195B74
		bool IMemberRef.IsTypeRef
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170010B0 RID: 4272
		// (get) Token: 0x0600524E RID: 21070 RVA: 0x00195B78 File Offset: 0x00195B78
		bool IMemberRef.IsTypeDef
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170010B1 RID: 4273
		// (get) Token: 0x0600524F RID: 21071 RVA: 0x00195B7C File Offset: 0x00195B7C
		bool IMemberRef.IsMethodSpec
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170010B2 RID: 4274
		// (get) Token: 0x06005250 RID: 21072 RVA: 0x00195B80 File Offset: 0x00195B80
		bool IMemberRef.IsMethodDef
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170010B3 RID: 4275
		// (get) Token: 0x06005251 RID: 21073 RVA: 0x00195B84 File Offset: 0x00195B84
		bool IMemberRef.IsMemberRef
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170010B4 RID: 4276
		// (get) Token: 0x06005252 RID: 21074 RVA: 0x00195B88 File Offset: 0x00195B88
		bool IMemberRef.IsFieldDef
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170010B5 RID: 4277
		// (get) Token: 0x06005253 RID: 21075 RVA: 0x00195B8C File Offset: 0x00195B8C
		bool IMemberRef.IsPropertyDef
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170010B6 RID: 4278
		// (get) Token: 0x06005254 RID: 21076 RVA: 0x00195B90 File Offset: 0x00195B90
		bool IMemberRef.IsEventDef
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170010B7 RID: 4279
		// (get) Token: 0x06005255 RID: 21077 RVA: 0x00195B94 File Offset: 0x00195B94
		bool IMemberRef.IsGenericParam
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06005256 RID: 21078 RVA: 0x00195B98 File Offset: 0x00195B98
		public TypeDef Resolve()
		{
			return this.Resolve(null);
		}

		// Token: 0x06005257 RID: 21079 RVA: 0x00195BA4 File Offset: 0x00195BA4
		public TypeDef Resolve(ModuleDef sourceModule)
		{
			if (this.module == null)
			{
				return null;
			}
			return this.module.Context.Resolver.Resolve(this, sourceModule ?? this.module);
		}

		// Token: 0x06005258 RID: 21080 RVA: 0x00195BE8 File Offset: 0x00195BE8
		public TypeDef ResolveThrow()
		{
			return this.ResolveThrow(null);
		}

		// Token: 0x06005259 RID: 21081 RVA: 0x00195BF4 File Offset: 0x00195BF4
		public TypeDef ResolveThrow(ModuleDef sourceModule)
		{
			TypeDef typeDef = this.Resolve(sourceModule);
			if (typeDef != null)
			{
				return typeDef;
			}
			throw new TypeResolveException(string.Format("Could not resolve type: {0} ({1})", this, this.DefinitionAssembly));
		}

		// Token: 0x0600525A RID: 21082 RVA: 0x00195C2C File Offset: 0x00195C2C
		internal static TypeRef GetNonNestedTypeRef(TypeRef typeRef)
		{
			if (typeRef == null)
			{
				return null;
			}
			for (int i = 0; i < 1000; i++)
			{
				TypeRef typeRef2 = typeRef.ResolutionScope as TypeRef;
				if (typeRef2 == null)
				{
					return typeRef;
				}
				typeRef = typeRef2;
			}
			return null;
		}

		// Token: 0x0600525B RID: 21083 RVA: 0x00195C70 File Offset: 0x00195C70
		public override string ToString()
		{
			return this.FullName;
		}

		// Token: 0x040027BE RID: 10174
		protected uint rid;

		// Token: 0x040027BF RID: 10175
		protected ModuleDef module;

		// Token: 0x040027C0 RID: 10176
		private readonly Lock theLock = Lock.Create();

		// Token: 0x040027C1 RID: 10177
		protected IResolutionScope resolutionScope;

		// Token: 0x040027C2 RID: 10178
		protected bool resolutionScope_isInitialized;

		// Token: 0x040027C3 RID: 10179
		protected UTF8String name;

		// Token: 0x040027C4 RID: 10180
		protected UTF8String @namespace;

		// Token: 0x040027C5 RID: 10181
		protected CustomAttributeCollection customAttributes;

		// Token: 0x040027C6 RID: 10182
		protected IList<PdbCustomDebugInfo> customDebugInfos;
	}
}
