using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x020007EC RID: 2028
	[ComVisible(true)]
	public struct Importer
	{
		// Token: 0x17000DF4 RID: 3572
		// (get) Token: 0x0600490B RID: 18699 RVA: 0x001776C4 File Offset: 0x001776C4
		private bool TryToUseTypeDefs
		{
			get
			{
				return (this.options & ImporterOptions.TryToUseTypeDefs) > (ImporterOptions)0;
			}
		}

		// Token: 0x17000DF5 RID: 3573
		// (get) Token: 0x0600490C RID: 18700 RVA: 0x001776D4 File Offset: 0x001776D4
		private bool TryToUseMethodDefs
		{
			get
			{
				return (this.options & ImporterOptions.TryToUseMethodDefs) > (ImporterOptions)0;
			}
		}

		// Token: 0x17000DF6 RID: 3574
		// (get) Token: 0x0600490D RID: 18701 RVA: 0x001776E4 File Offset: 0x001776E4
		private bool TryToUseFieldDefs
		{
			get
			{
				return (this.options & ImporterOptions.TryToUseFieldDefs) > (ImporterOptions)0;
			}
		}

		// Token: 0x17000DF7 RID: 3575
		// (get) Token: 0x0600490E RID: 18702 RVA: 0x001776F4 File Offset: 0x001776F4
		// (set) Token: 0x0600490F RID: 18703 RVA: 0x00177708 File Offset: 0x00177708
		private bool FixSignature
		{
			get
			{
				return (this.options & ImporterOptions.FixSignature) > (ImporterOptions)0;
			}
			set
			{
				if (value)
				{
					this.options |= ImporterOptions.FixSignature;
					return;
				}
				this.options &= (ImporterOptions)2147483647;
			}
		}

		// Token: 0x06004910 RID: 18704 RVA: 0x00177738 File Offset: 0x00177738
		public Importer(ModuleDef module)
		{
			this = new Importer(module, (ImporterOptions)0, default(GenericParamContext), null);
		}

		// Token: 0x06004911 RID: 18705 RVA: 0x0017775C File Offset: 0x0017775C
		public Importer(ModuleDef module, GenericParamContext gpContext)
		{
			this = new Importer(module, (ImporterOptions)0, gpContext, null);
		}

		// Token: 0x06004912 RID: 18706 RVA: 0x00177768 File Offset: 0x00177768
		public Importer(ModuleDef module, ImporterOptions options)
		{
			this = new Importer(module, options, default(GenericParamContext), null);
		}

		// Token: 0x06004913 RID: 18707 RVA: 0x0017778C File Offset: 0x0017778C
		public Importer(ModuleDef module, ImporterOptions options, GenericParamContext gpContext)
		{
			this = new Importer(module, options, gpContext, null);
		}

		// Token: 0x06004914 RID: 18708 RVA: 0x00177798 File Offset: 0x00177798
		public Importer(ModuleDef module, ImporterOptions options, GenericParamContext gpContext, ImportMapper mapper)
		{
			this.module = module;
			this.recursionCounter = default(RecursionCounter);
			this.options = options;
			this.gpContext = gpContext;
			this.mapper = mapper;
		}

		// Token: 0x06004915 RID: 18709 RVA: 0x001777C4 File Offset: 0x001777C4
		public ITypeDefOrRef Import(Type type)
		{
			return this.module.UpdateRowId<ITypeDefOrRef>(this.ImportAsTypeSig(type).ToTypeDefOrRef());
		}

		// Token: 0x06004916 RID: 18710 RVA: 0x001777E0 File Offset: 0x001777E0
		public ITypeDefOrRef ImportDeclaringType(Type type)
		{
			return this.module.UpdateRowId<ITypeDefOrRef>(this.ImportAsTypeSig(type, type.IsGenericTypeDefinition).ToTypeDefOrRef());
		}

		// Token: 0x06004917 RID: 18711 RVA: 0x00177800 File Offset: 0x00177800
		public ITypeDefOrRef Import(Type type, IList<Type> requiredModifiers, IList<Type> optionalModifiers)
		{
			return this.module.UpdateRowId<ITypeDefOrRef>(this.ImportAsTypeSig(type, requiredModifiers, optionalModifiers).ToTypeDefOrRef());
		}

		// Token: 0x06004918 RID: 18712 RVA: 0x0017781C File Offset: 0x0017781C
		public TypeSig ImportAsTypeSig(Type type)
		{
			return this.ImportAsTypeSig(type, false);
		}

		// Token: 0x06004919 RID: 18713 RVA: 0x00177828 File Offset: 0x00177828
		private TypeSig ImportAsTypeSig(Type type, bool treatAsGenericInst)
		{
			if (type == null)
			{
				return null;
			}
			switch (treatAsGenericInst ? ElementType.GenericInst : type.GetElementType2())
			{
			case ElementType.Void:
				return this.module.CorLibTypes.Void;
			case ElementType.Boolean:
				return this.module.CorLibTypes.Boolean;
			case ElementType.Char:
				return this.module.CorLibTypes.Char;
			case ElementType.I1:
				return this.module.CorLibTypes.SByte;
			case ElementType.U1:
				return this.module.CorLibTypes.Byte;
			case ElementType.I2:
				return this.module.CorLibTypes.Int16;
			case ElementType.U2:
				return this.module.CorLibTypes.UInt16;
			case ElementType.I4:
				return this.module.CorLibTypes.Int32;
			case ElementType.U4:
				return this.module.CorLibTypes.UInt32;
			case ElementType.I8:
				return this.module.CorLibTypes.Int64;
			case ElementType.U8:
				return this.module.CorLibTypes.UInt64;
			case ElementType.R4:
				return this.module.CorLibTypes.Single;
			case ElementType.R8:
				return this.module.CorLibTypes.Double;
			case ElementType.String:
				return this.module.CorLibTypes.String;
			case ElementType.Ptr:
				return new PtrSig(this.ImportAsTypeSig(type.GetElementType(), treatAsGenericInst));
			case ElementType.ByRef:
				return new ByRefSig(this.ImportAsTypeSig(type.GetElementType(), treatAsGenericInst));
			case ElementType.ValueType:
				return new ValueTypeSig(this.CreateTypeRef(type));
			case ElementType.Class:
				return new ClassSig(this.CreateTypeRef(type));
			case ElementType.Var:
				return new GenericVar((uint)type.GenericParameterPosition, this.gpContext.Type);
			case ElementType.Array:
			{
				int[] lowerBounds = new int[type.GetArrayRank()];
				uint[] sizes = Array2.Empty<uint>();
				this.FixSignature = true;
				return new ArraySig(this.ImportAsTypeSig(type.GetElementType(), treatAsGenericInst), (uint)type.GetArrayRank(), sizes, lowerBounds);
			}
			case ElementType.GenericInst:
			{
				Type[] genericArguments = type.GetGenericArguments();
				GenericInstSig genericInstSig = new GenericInstSig(this.ImportAsTypeSig(type.GetGenericTypeDefinition()) as ClassOrValueTypeSig, (uint)genericArguments.Length);
				foreach (Type type2 in genericArguments)
				{
					genericInstSig.GenericArguments.Add(this.ImportAsTypeSig(type2));
				}
				return genericInstSig;
			}
			case ElementType.TypedByRef:
				return this.module.CorLibTypes.TypedReference;
			case ElementType.I:
				this.FixSignature = true;
				return this.module.CorLibTypes.IntPtr;
			case ElementType.U:
				return this.module.CorLibTypes.UIntPtr;
			case ElementType.Object:
				return this.module.CorLibTypes.Object;
			case ElementType.SZArray:
				return new SZArraySig(this.ImportAsTypeSig(type.GetElementType(), treatAsGenericInst));
			case ElementType.MVar:
				return new GenericMVar((uint)type.GenericParameterPosition, this.gpContext.Method);
			}
			return null;
		}

		// Token: 0x0600491A RID: 18714 RVA: 0x00177BC4 File Offset: 0x00177BC4
		private ITypeDefOrRef TryResolve(TypeRef tr)
		{
			if (!this.TryToUseTypeDefs || tr == null)
			{
				return tr;
			}
			if (!this.IsThisModule(tr))
			{
				return tr;
			}
			TypeDef typeDef = tr.Resolve();
			if (typeDef == null || typeDef.Module != this.module)
			{
				return tr;
			}
			return typeDef;
		}

		// Token: 0x0600491B RID: 18715 RVA: 0x00177C18 File Offset: 0x00177C18
		private IMethodDefOrRef TryResolveMethod(IMethodDefOrRef mdr)
		{
			if (!this.TryToUseMethodDefs || mdr == null)
			{
				return mdr;
			}
			MemberRef memberRef = mdr as MemberRef;
			if (memberRef == null)
			{
				return mdr;
			}
			if (!memberRef.IsMethodRef)
			{
				return memberRef;
			}
			TypeDef declaringType = this.GetDeclaringType(memberRef);
			if (declaringType == null)
			{
				return memberRef;
			}
			if (declaringType.Module != this.module)
			{
				return memberRef;
			}
			IMethodDefOrRef methodDefOrRef = declaringType.ResolveMethod(memberRef);
			return methodDefOrRef ?? memberRef;
		}

		// Token: 0x0600491C RID: 18716 RVA: 0x00177C8C File Offset: 0x00177C8C
		private IField TryResolveField(MemberRef mr)
		{
			if (!this.TryToUseFieldDefs || mr == null)
			{
				return mr;
			}
			if (!mr.IsFieldRef)
			{
				return mr;
			}
			TypeDef declaringType = this.GetDeclaringType(mr);
			if (declaringType == null)
			{
				return mr;
			}
			if (declaringType.Module != this.module)
			{
				return mr;
			}
			IField field = declaringType.ResolveField(mr);
			return field ?? mr;
		}

		// Token: 0x0600491D RID: 18717 RVA: 0x00177CF4 File Offset: 0x00177CF4
		private TypeDef GetDeclaringType(MemberRef mr)
		{
			if (mr == null)
			{
				return null;
			}
			TypeDef typeDef = mr.Class as TypeDef;
			if (typeDef != null)
			{
				return typeDef;
			}
			typeDef = (this.TryResolve(mr.Class as TypeRef) as TypeDef);
			if (typeDef != null)
			{
				return typeDef;
			}
			ModuleRef modRef = mr.Class as ModuleRef;
			if (this.IsThisModule(modRef))
			{
				return this.module.GlobalType;
			}
			return null;
		}

		// Token: 0x0600491E RID: 18718 RVA: 0x00177D68 File Offset: 0x00177D68
		private bool IsThisModule(TypeRef tr)
		{
			if (tr == null)
			{
				return false;
			}
			TypeRef typeRef = tr.ScopeType.GetNonNestedTypeRefScope() as TypeRef;
			if (typeRef == null)
			{
				return false;
			}
			if (this.module == typeRef.ResolutionScope)
			{
				return true;
			}
			ModuleRef moduleRef = typeRef.ResolutionScope as ModuleRef;
			if (moduleRef != null)
			{
				return this.IsThisModule(moduleRef);
			}
			AssemblyRef b = typeRef.ResolutionScope as AssemblyRef;
			return Importer.Equals(this.module.Assembly, b);
		}

		// Token: 0x0600491F RID: 18719 RVA: 0x00177DE4 File Offset: 0x00177DE4
		private bool IsThisModule(ModuleRef modRef)
		{
			return modRef != null && this.module.Name == modRef.Name && Importer.Equals(this.module.Assembly, modRef.DefinitionAssembly);
		}

		// Token: 0x06004920 RID: 18720 RVA: 0x00177E20 File Offset: 0x00177E20
		private static bool Equals(IAssembly a, IAssembly b)
		{
			return a == b || (a != null && b != null && (Utils.Equals(a.Version, b.Version) && PublicKeyBase.TokenEquals(a.PublicKeyOrToken, b.PublicKeyOrToken) && UTF8String.Equals(a.Name, b.Name)) && UTF8String.CaseInsensitiveEquals(a.Culture, b.Culture));
		}

		// Token: 0x06004921 RID: 18721 RVA: 0x00177EA0 File Offset: 0x00177EA0
		private ITypeDefOrRef CreateTypeRef(Type type)
		{
			ImportMapper importMapper = this.mapper;
			return this.TryResolve(((importMapper != null) ? importMapper.Map(type) : null) ?? this.CreateTypeRef2(type));
		}

		// Token: 0x06004922 RID: 18722 RVA: 0x00177ED0 File Offset: 0x00177ED0
		private TypeRef CreateTypeRef2(Type type)
		{
			if (!type.IsNested)
			{
				return this.module.UpdateRowId<TypeRefUser>(new TypeRefUser(this.module, type.Namespace ?? string.Empty, type.Name ?? string.Empty, this.CreateScopeReference(type)));
			}
			string text;
			string text2;
			type.GetTypeNamespaceAndName_TypeDefOrRef(out text, out text2);
			return this.module.UpdateRowId<TypeRefUser>(new TypeRefUser(this.module, text ?? string.Empty, text2 ?? string.Empty, this.CreateTypeRef2(type.DeclaringType)));
		}

		// Token: 0x06004923 RID: 18723 RVA: 0x00177F88 File Offset: 0x00177F88
		private IResolutionScope CreateScopeReference(Type type)
		{
			if (type == null)
			{
				return null;
			}
			AssemblyName name = type.Assembly.GetName();
			AssemblyDef assembly = this.module.Assembly;
			if (assembly == null || !UTF8String.ToSystemStringOrEmpty(assembly.Name).Equals(name.Name, StringComparison.OrdinalIgnoreCase))
			{
				byte[] array = name.GetPublicKeyToken();
				if (array == null || array.Length == 0)
				{
					array = null;
				}
				return this.module.UpdateRowId<AssemblyRefUser>(new AssemblyRefUser(name.Name, name.Version, PublicKeyBase.CreatePublicKeyToken(array), name.CultureInfo.Name));
			}
			if (UTF8String.ToSystemStringOrEmpty(this.module.Name).Equals(type.Module.ScopeName, StringComparison.OrdinalIgnoreCase))
			{
				return this.module;
			}
			return this.module.UpdateRowId<ModuleRefUser>(new ModuleRefUser(this.module, type.Module.ScopeName));
		}

		// Token: 0x06004924 RID: 18724 RVA: 0x0017807C File Offset: 0x0017807C
		public TypeSig ImportAsTypeSig(Type type, IList<Type> requiredModifiers, IList<Type> optionalModifiers)
		{
			return this.ImportAsTypeSig(type, requiredModifiers, optionalModifiers, false);
		}

		// Token: 0x06004925 RID: 18725 RVA: 0x00178088 File Offset: 0x00178088
		private TypeSig ImportAsTypeSig(Type type, IList<Type> requiredModifiers, IList<Type> optionalModifiers, bool treatAsGenericInst)
		{
			if (type == null)
			{
				return null;
			}
			if (Importer.IsEmpty<Type>(requiredModifiers) && Importer.IsEmpty<Type>(optionalModifiers))
			{
				return this.ImportAsTypeSig(type, treatAsGenericInst);
			}
			this.FixSignature = true;
			TypeSig typeSig = this.ImportAsTypeSig(type, treatAsGenericInst);
			if (requiredModifiers != null)
			{
				foreach (Type type2 in requiredModifiers)
				{
					typeSig = new CModReqdSig(this.Import(type2), typeSig);
				}
			}
			if (optionalModifiers != null)
			{
				foreach (Type type3 in optionalModifiers)
				{
					typeSig = new CModOptSig(this.Import(type3), typeSig);
				}
			}
			return typeSig;
		}

		// Token: 0x06004926 RID: 18726 RVA: 0x00178170 File Offset: 0x00178170
		private static bool IsEmpty<T>(IList<T> list)
		{
			return list == null || list.Count == 0;
		}

		// Token: 0x06004927 RID: 18727 RVA: 0x00178184 File Offset: 0x00178184
		public IMethod Import(MethodBase methodBase)
		{
			return this.Import(methodBase, false);
		}

		// Token: 0x06004928 RID: 18728 RVA: 0x00178190 File Offset: 0x00178190
		public IMethod Import(MethodBase methodBase, bool forceFixSignature)
		{
			this.FixSignature = false;
			return this.ImportInternal(methodBase, forceFixSignature);
		}

		// Token: 0x06004929 RID: 18729 RVA: 0x001781A4 File Offset: 0x001781A4
		private IMethod ImportInternal(MethodBase methodBase)
		{
			return this.ImportInternal(methodBase, false);
		}

		// Token: 0x0600492A RID: 18730 RVA: 0x001781B0 File Offset: 0x001781B0
		private IMethod ImportInternal(MethodBase methodBase, bool forceFixSignature)
		{
			if (methodBase == null)
			{
				return null;
			}
			if (methodBase.IsGenericButNotGenericMethodDefinition())
			{
				MethodBase methodBase2 = methodBase.Module.ResolveMethod(methodBase.MetadataToken);
				IMethodDefOrRef methodDefOrRef;
				if (methodBase.DeclaringType.GetElementType2() == ElementType.GenericInst)
				{
					methodDefOrRef = this.module.UpdateRowId<MemberRefUser>(new MemberRefUser(this.module, methodBase.Name, this.CreateMethodSig(methodBase2), this.ImportDeclaringType(methodBase.DeclaringType)));
				}
				else
				{
					methodDefOrRef = (this.ImportInternal(methodBase2) as IMethodDefOrRef);
				}
				methodDefOrRef = this.TryResolveMethod(methodDefOrRef);
				GenericInstMethodSig sig = this.CreateGenericInstMethodSig(methodBase);
				IMethod result = this.module.UpdateRowId<MethodSpecUser>(new MethodSpecUser(methodDefOrRef, sig));
				if (this.FixSignature)
				{
				}
				return result;
			}
			IMemberRefParent memberRefParent;
			if (methodBase.DeclaringType == null)
			{
				memberRefParent = this.GetModuleParent(methodBase.Module);
			}
			else
			{
				memberRefParent = this.ImportDeclaringType(methodBase.DeclaringType);
			}
			if (memberRefParent == null)
			{
				return null;
			}
			MethodBase mb;
			try
			{
				mb = methodBase.Module.ResolveMethod(methodBase.MetadataToken);
			}
			catch (ArgumentException)
			{
				mb = methodBase;
			}
			MethodSig sig2 = this.CreateMethodSig(mb);
			IMethodDefOrRef methodDefOrRef2 = this.module.UpdateRowId<MemberRefUser>(new MemberRefUser(this.module, methodBase.Name, sig2, memberRefParent));
			methodDefOrRef2 = this.TryResolveMethod(methodDefOrRef2);
			if (this.FixSignature)
			{
			}
			return methodDefOrRef2;
		}

		// Token: 0x0600492B RID: 18731 RVA: 0x0017831C File Offset: 0x0017831C
		private MethodSig CreateMethodSig(MethodBase mb)
		{
			MethodSig methodSig = new MethodSig(this.GetCallingConvention(mb));
			MethodInfo methodInfo = mb as MethodInfo;
			if (methodInfo != null)
			{
				methodSig.RetType = this.ImportAsTypeSig(methodInfo.ReturnParameter, mb.DeclaringType);
			}
			else
			{
				methodSig.RetType = this.module.CorLibTypes.Void;
			}
			foreach (ParameterInfo p in mb.GetParameters())
			{
				methodSig.Params.Add(this.ImportAsTypeSig(p, mb.DeclaringType));
			}
			if (mb.IsGenericMethodDefinition)
			{
				methodSig.GenParamCount = (uint)mb.GetGenericArguments().Length;
			}
			return methodSig;
		}

		// Token: 0x0600492C RID: 18732 RVA: 0x001783D0 File Offset: 0x001783D0
		private TypeSig ImportAsTypeSig(ParameterInfo p, Type declaringType)
		{
			return this.ImportAsTypeSig(p.ParameterType, p.GetRequiredCustomModifiers(), p.GetOptionalCustomModifiers(), declaringType.MustTreatTypeAsGenericInstType(p.ParameterType));
		}

		// Token: 0x0600492D RID: 18733 RVA: 0x00178408 File Offset: 0x00178408
		private CallingConvention GetCallingConvention(MethodBase mb)
		{
			CallingConvention callingConvention = CallingConvention.Default;
			CallingConventions callingConvention2 = mb.CallingConvention;
			if (mb.IsGenericMethodDefinition)
			{
				callingConvention |= CallingConvention.Generic;
			}
			if ((callingConvention2 & CallingConventions.HasThis) != (CallingConventions)0)
			{
				callingConvention |= CallingConvention.HasThis;
			}
			if ((callingConvention2 & CallingConventions.ExplicitThis) != (CallingConventions)0)
			{
				callingConvention |= CallingConvention.ExplicitThis;
			}
			switch (callingConvention2 & CallingConventions.Any)
			{
			case CallingConventions.Standard:
				return callingConvention | CallingConvention.Default;
			case CallingConventions.VarArgs:
				return callingConvention | CallingConvention.VarArg;
			}
			this.FixSignature = true;
			return callingConvention | CallingConvention.Default;
		}

		// Token: 0x0600492E RID: 18734 RVA: 0x00178488 File Offset: 0x00178488
		private GenericInstMethodSig CreateGenericInstMethodSig(MethodBase mb)
		{
			Type[] genericArguments = mb.GetGenericArguments();
			GenericInstMethodSig genericInstMethodSig = new GenericInstMethodSig(CallingConvention.GenericInst, (uint)genericArguments.Length);
			foreach (Type type in genericArguments)
			{
				genericInstMethodSig.GenericArguments.Add(this.ImportAsTypeSig(type));
			}
			return genericInstMethodSig;
		}

		// Token: 0x0600492F RID: 18735 RVA: 0x001784DC File Offset: 0x001784DC
		private IMemberRefParent GetModuleParent(Module module2)
		{
			AssemblyDef assembly = this.module.Assembly;
			if (assembly != null && !UTF8String.ToSystemStringOrEmpty(assembly.Name).Equals(module2.Assembly.GetName().Name, StringComparison.OrdinalIgnoreCase))
			{
				return null;
			}
			return this.module.UpdateRowId<ModuleRefUser>(new ModuleRefUser(this.module, this.module.Name));
		}

		// Token: 0x06004930 RID: 18736 RVA: 0x00178550 File Offset: 0x00178550
		public IField Import(FieldInfo fieldInfo)
		{
			return this.Import(fieldInfo, false);
		}

		// Token: 0x06004931 RID: 18737 RVA: 0x0017855C File Offset: 0x0017855C
		public IField Import(FieldInfo fieldInfo, bool forceFixSignature)
		{
			this.FixSignature = false;
			if (fieldInfo == null)
			{
				return null;
			}
			IMemberRefParent memberRefParent;
			if (fieldInfo.DeclaringType == null)
			{
				memberRefParent = this.GetModuleParent(fieldInfo.Module);
			}
			else
			{
				memberRefParent = this.ImportDeclaringType(fieldInfo.DeclaringType);
			}
			if (memberRefParent == null)
			{
				return null;
			}
			FieldInfo fieldInfo2;
			try
			{
				fieldInfo2 = fieldInfo.Module.ResolveField(fieldInfo.MetadataToken);
			}
			catch (ArgumentException)
			{
				fieldInfo2 = fieldInfo;
			}
			MemberRef mr;
			if (fieldInfo2.FieldType.ContainsGenericParameters)
			{
				Type declaringType = fieldInfo2.DeclaringType;
				AssemblyDef assemblyDef = this.module.Context.AssemblyResolver.Resolve(declaringType.Module.Assembly.GetName(), this.module);
				if (assemblyDef == null || assemblyDef.FullName != declaringType.Assembly.FullName)
				{
					throw new Exception("Couldn't resolve the correct assembly");
				}
				ModuleDefMD moduleDefMD = assemblyDef.FindModule(declaringType.Module.ScopeName) as ModuleDefMD;
				if (moduleDefMD == null)
				{
					throw new Exception("Couldn't resolve the correct module");
				}
				FieldDef fieldDef = moduleDefMD.ResolveField((uint)(fieldInfo2.MetadataToken & 16777215));
				if (fieldDef == null)
				{
					throw new Exception("Couldn't resolve the correct field");
				}
				FieldSig sig = new FieldSig(this.Import(fieldDef.FieldSig.GetFieldType()));
				mr = this.module.UpdateRowId<MemberRefUser>(new MemberRefUser(this.module, fieldInfo.Name, sig, memberRefParent));
			}
			else
			{
				FieldSig sig2 = new FieldSig(this.ImportAsTypeSig(fieldInfo.FieldType, fieldInfo.GetRequiredCustomModifiers(), fieldInfo.GetOptionalCustomModifiers()));
				mr = this.module.UpdateRowId<MemberRefUser>(new MemberRefUser(this.module, fieldInfo.Name, sig2, memberRefParent));
			}
			IField result = this.TryResolveField(mr);
			if (this.FixSignature)
			{
			}
			return result;
		}

		// Token: 0x06004932 RID: 18738 RVA: 0x0017873C File Offset: 0x0017873C
		public IType Import(IType type)
		{
			if (type == null)
			{
				return null;
			}
			if (!this.recursionCounter.Increment())
			{
				return null;
			}
			TypeDef type2;
			IType result;
			TypeRef type3;
			TypeSpec type4;
			TypeSig type5;
			if ((type2 = (type as TypeDef)) != null)
			{
				result = this.Import(type2);
			}
			else if ((type3 = (type as TypeRef)) != null)
			{
				result = this.Import(type3);
			}
			else if ((type4 = (type as TypeSpec)) != null)
			{
				result = this.Import(type4);
			}
			else if ((type5 = (type as TypeSig)) != null)
			{
				result = this.Import(type5);
			}
			else
			{
				result = null;
			}
			this.recursionCounter.Decrement();
			return result;
		}

		// Token: 0x06004933 RID: 18739 RVA: 0x001787E0 File Offset: 0x001787E0
		public ITypeDefOrRef Import(TypeDef type)
		{
			if (type == null)
			{
				return null;
			}
			if (this.TryToUseTypeDefs && type.Module == this.module)
			{
				return type;
			}
			ImportMapper importMapper = this.mapper;
			ITypeDefOrRef typeDefOrRef = (importMapper != null) ? importMapper.Map(type) : null;
			if (typeDefOrRef != null)
			{
				return typeDefOrRef;
			}
			return this.Import2(type);
		}

		// Token: 0x06004934 RID: 18740 RVA: 0x00178840 File Offset: 0x00178840
		private TypeRef Import2(TypeDef type)
		{
			if (type == null)
			{
				return null;
			}
			if (!this.recursionCounter.Increment())
			{
				return null;
			}
			TypeDef declaringType = type.DeclaringType;
			TypeRef result;
			if (declaringType != null)
			{
				result = this.module.UpdateRowId<TypeRefUser>(new TypeRefUser(this.module, type.Namespace, type.Name, this.Import2(declaringType)));
			}
			else
			{
				result = this.module.UpdateRowId<TypeRefUser>(new TypeRefUser(this.module, type.Namespace, type.Name, this.CreateScopeReference(type.DefinitionAssembly, type.Module)));
			}
			this.recursionCounter.Decrement();
			return result;
		}

		// Token: 0x06004935 RID: 18741 RVA: 0x001788E8 File Offset: 0x001788E8
		private IResolutionScope CreateScopeReference(IAssembly defAsm, ModuleDef defMod)
		{
			if (defAsm == null)
			{
				return null;
			}
			AssemblyDef assembly = this.module.Assembly;
			if (defMod == null || defAsm == null || assembly == null || !UTF8String.CaseInsensitiveEquals(assembly.Name, defAsm.Name))
			{
				PublicKeyToken publicKeyToken = PublicKeyBase.ToPublicKeyToken(defAsm.PublicKeyOrToken);
				if (PublicKeyBase.IsNullOrEmpty2(publicKeyToken))
				{
					publicKeyToken = null;
				}
				return this.module.UpdateRowId<AssemblyRefUser>(new AssemblyRefUser(defAsm.Name, defAsm.Version, publicKeyToken, defAsm.Culture)
				{
					Attributes = (defAsm.Attributes & ~AssemblyAttributes.PublicKey)
				});
			}
			if (UTF8String.CaseInsensitiveEquals(this.module.Name, defMod.Name))
			{
				return this.module;
			}
			return this.module.UpdateRowId<ModuleRefUser>(new ModuleRefUser(this.module, defMod.Name));
		}

		// Token: 0x06004936 RID: 18742 RVA: 0x001789C0 File Offset: 0x001789C0
		public ITypeDefOrRef Import(TypeRef type)
		{
			ImportMapper importMapper = this.mapper;
			ITypeDefOrRef typeDefOrRef = (importMapper != null) ? importMapper.Map(type) : null;
			if (typeDefOrRef != null)
			{
				return typeDefOrRef;
			}
			return this.TryResolve(this.Import2(type));
		}

		// Token: 0x06004937 RID: 18743 RVA: 0x00178A00 File Offset: 0x00178A00
		private TypeRef Import2(TypeRef type)
		{
			if (type == null)
			{
				return null;
			}
			if (!this.recursionCounter.Increment())
			{
				return null;
			}
			TypeRef declaringType = type.DeclaringType;
			TypeRef result;
			if (declaringType != null)
			{
				result = this.module.UpdateRowId<TypeRefUser>(new TypeRefUser(this.module, type.Namespace, type.Name, this.Import2(declaringType)));
			}
			else
			{
				result = this.module.UpdateRowId<TypeRefUser>(new TypeRefUser(this.module, type.Namespace, type.Name, this.CreateScopeReference(type.DefinitionAssembly, type.Module)));
			}
			this.recursionCounter.Decrement();
			return result;
		}

		// Token: 0x06004938 RID: 18744 RVA: 0x00178AA8 File Offset: 0x00178AA8
		public TypeSpec Import(TypeSpec type)
		{
			if (type == null)
			{
				return null;
			}
			return this.module.UpdateRowId<TypeSpecUser>(new TypeSpecUser(this.Import(type.TypeSig)));
		}

		// Token: 0x06004939 RID: 18745 RVA: 0x00178AD0 File Offset: 0x00178AD0
		public TypeSig Import(TypeSig type)
		{
			if (type == null)
			{
				return null;
			}
			if (!this.recursionCounter.Increment())
			{
				return null;
			}
			TypeSig result;
			switch (type.ElementType)
			{
			case ElementType.Void:
				result = this.module.CorLibTypes.Void;
				goto IL_51D;
			case ElementType.Boolean:
				result = this.module.CorLibTypes.Boolean;
				goto IL_51D;
			case ElementType.Char:
				result = this.module.CorLibTypes.Char;
				goto IL_51D;
			case ElementType.I1:
				result = this.module.CorLibTypes.SByte;
				goto IL_51D;
			case ElementType.U1:
				result = this.module.CorLibTypes.Byte;
				goto IL_51D;
			case ElementType.I2:
				result = this.module.CorLibTypes.Int16;
				goto IL_51D;
			case ElementType.U2:
				result = this.module.CorLibTypes.UInt16;
				goto IL_51D;
			case ElementType.I4:
				result = this.module.CorLibTypes.Int32;
				goto IL_51D;
			case ElementType.U4:
				result = this.module.CorLibTypes.UInt32;
				goto IL_51D;
			case ElementType.I8:
				result = this.module.CorLibTypes.Int64;
				goto IL_51D;
			case ElementType.U8:
				result = this.module.CorLibTypes.UInt64;
				goto IL_51D;
			case ElementType.R4:
				result = this.module.CorLibTypes.Single;
				goto IL_51D;
			case ElementType.R8:
				result = this.module.CorLibTypes.Double;
				goto IL_51D;
			case ElementType.String:
				result = this.module.CorLibTypes.String;
				goto IL_51D;
			case ElementType.Ptr:
				result = new PtrSig(this.Import(type.Next));
				goto IL_51D;
			case ElementType.ByRef:
				result = new ByRefSig(this.Import(type.Next));
				goto IL_51D;
			case ElementType.ValueType:
				result = this.CreateClassOrValueType((type as ClassOrValueTypeSig).TypeDefOrRef, true);
				goto IL_51D;
			case ElementType.Class:
				result = this.CreateClassOrValueType((type as ClassOrValueTypeSig).TypeDefOrRef, false);
				goto IL_51D;
			case ElementType.Var:
				result = new GenericVar((type as GenericVar).Number, this.gpContext.Type);
				goto IL_51D;
			case ElementType.Array:
			{
				ArraySig arraySig = (ArraySig)type;
				List<uint> sizes = new List<uint>(arraySig.Sizes);
				List<int> lowerBounds = new List<int>(arraySig.LowerBounds);
				result = new ArraySig(this.Import(type.Next), arraySig.Rank, sizes, lowerBounds);
				goto IL_51D;
			}
			case ElementType.GenericInst:
			{
				GenericInstSig genericInstSig = (GenericInstSig)type;
				List<TypeSig> list = new List<TypeSig>(genericInstSig.GenericArguments.Count);
				foreach (TypeSig type2 in genericInstSig.GenericArguments)
				{
					list.Add(this.Import(type2));
				}
				result = new GenericInstSig(this.Import(genericInstSig.GenericType) as ClassOrValueTypeSig, list);
				goto IL_51D;
			}
			case ElementType.TypedByRef:
				result = this.module.CorLibTypes.TypedReference;
				goto IL_51D;
			case ElementType.ValueArray:
				result = new ValueArraySig(this.Import(type.Next), (type as ValueArraySig).Size);
				goto IL_51D;
			case ElementType.I:
				result = this.module.CorLibTypes.IntPtr;
				goto IL_51D;
			case ElementType.U:
				result = this.module.CorLibTypes.UIntPtr;
				goto IL_51D;
			case ElementType.FnPtr:
				result = new FnPtrSig(this.Import((type as FnPtrSig).Signature));
				goto IL_51D;
			case ElementType.Object:
				result = this.module.CorLibTypes.Object;
				goto IL_51D;
			case ElementType.SZArray:
				result = new SZArraySig(this.Import(type.Next));
				goto IL_51D;
			case ElementType.MVar:
				result = new GenericMVar((type as GenericMVar).Number, this.gpContext.Method);
				goto IL_51D;
			case ElementType.CModReqd:
				result = new CModReqdSig(this.Import((type as ModifierSig).Modifier), this.Import(type.Next));
				goto IL_51D;
			case ElementType.CModOpt:
				result = new CModOptSig(this.Import((type as ModifierSig).Modifier), this.Import(type.Next));
				goto IL_51D;
			case ElementType.Module:
				result = new ModuleSig((type as ModuleSig).Index, this.Import(type.Next));
				goto IL_51D;
			case ElementType.Sentinel:
				result = new SentinelSig();
				goto IL_51D;
			case ElementType.Pinned:
				result = new PinnedSig(this.Import(type.Next));
				goto IL_51D;
			}
			result = null;
			IL_51D:
			this.recursionCounter.Decrement();
			return result;
		}

		// Token: 0x0600493A RID: 18746 RVA: 0x00179018 File Offset: 0x00179018
		public ITypeDefOrRef Import(ITypeDefOrRef type)
		{
			return (ITypeDefOrRef)this.Import(type);
		}

		// Token: 0x0600493B RID: 18747 RVA: 0x00179028 File Offset: 0x00179028
		private TypeSig CreateClassOrValueType(ITypeDefOrRef type, bool isValueType)
		{
			CorLibTypeSig corLibTypeSig = this.module.CorLibTypes.GetCorLibTypeSig(type);
			if (corLibTypeSig != null)
			{
				return corLibTypeSig;
			}
			if (isValueType)
			{
				return new ValueTypeSig(this.Import(type));
			}
			return new ClassSig(this.Import(type));
		}

		// Token: 0x0600493C RID: 18748 RVA: 0x00179074 File Offset: 0x00179074
		public CallingConventionSig Import(CallingConventionSig sig)
		{
			if (sig == null)
			{
				return null;
			}
			if (!this.recursionCounter.Increment())
			{
				return null;
			}
			Type type = sig.GetType();
			CallingConventionSig result;
			if (type == typeof(MethodSig))
			{
				result = this.Import((MethodSig)sig);
			}
			else if (type == typeof(FieldSig))
			{
				result = this.Import((FieldSig)sig);
			}
			else if (type == typeof(GenericInstMethodSig))
			{
				result = this.Import((GenericInstMethodSig)sig);
			}
			else if (type == typeof(PropertySig))
			{
				result = this.Import((PropertySig)sig);
			}
			else if (type == typeof(LocalSig))
			{
				result = this.Import((LocalSig)sig);
			}
			else
			{
				result = null;
			}
			this.recursionCounter.Decrement();
			return result;
		}

		// Token: 0x0600493D RID: 18749 RVA: 0x00179178 File Offset: 0x00179178
		public FieldSig Import(FieldSig sig)
		{
			if (sig == null)
			{
				return null;
			}
			if (!this.recursionCounter.Increment())
			{
				return null;
			}
			FieldSig result = new FieldSig(sig.GetCallingConvention(), this.Import(sig.Type));
			this.recursionCounter.Decrement();
			return result;
		}

		// Token: 0x0600493E RID: 18750 RVA: 0x001791C8 File Offset: 0x001791C8
		public MethodSig Import(MethodSig sig)
		{
			if (sig == null)
			{
				return null;
			}
			if (!this.recursionCounter.Increment())
			{
				return null;
			}
			MethodSig result = this.Import<MethodSig>(new MethodSig(sig.GetCallingConvention()), sig);
			this.recursionCounter.Decrement();
			return result;
		}

		// Token: 0x0600493F RID: 18751 RVA: 0x00179204 File Offset: 0x00179204
		private T Import<T>(T sig, T old) where T : MethodBaseSig
		{
			sig.RetType = this.Import(old.RetType);
			foreach (TypeSig type in old.Params)
			{
				sig.Params.Add(this.Import(type));
			}
			sig.GenParamCount = old.GenParamCount;
			IList<TypeSig> paramsAfterSentinel = sig.ParamsAfterSentinel;
			if (paramsAfterSentinel != null)
			{
				foreach (TypeSig type2 in old.ParamsAfterSentinel)
				{
					paramsAfterSentinel.Add(this.Import(type2));
				}
			}
			return sig;
		}

		// Token: 0x06004940 RID: 18752 RVA: 0x00179304 File Offset: 0x00179304
		public PropertySig Import(PropertySig sig)
		{
			if (sig == null)
			{
				return null;
			}
			if (!this.recursionCounter.Increment())
			{
				return null;
			}
			PropertySig result = this.Import<PropertySig>(new PropertySig(sig.GetCallingConvention()), sig);
			this.recursionCounter.Decrement();
			return result;
		}

		// Token: 0x06004941 RID: 18753 RVA: 0x00179340 File Offset: 0x00179340
		public LocalSig Import(LocalSig sig)
		{
			if (sig == null)
			{
				return null;
			}
			if (!this.recursionCounter.Increment())
			{
				return null;
			}
			LocalSig localSig = new LocalSig(sig.GetCallingConvention(), (uint)sig.Locals.Count);
			foreach (TypeSig type in sig.Locals)
			{
				localSig.Locals.Add(this.Import(type));
			}
			this.recursionCounter.Decrement();
			return localSig;
		}

		// Token: 0x06004942 RID: 18754 RVA: 0x001793E0 File Offset: 0x001793E0
		public GenericInstMethodSig Import(GenericInstMethodSig sig)
		{
			if (sig == null)
			{
				return null;
			}
			if (!this.recursionCounter.Increment())
			{
				return null;
			}
			GenericInstMethodSig genericInstMethodSig = new GenericInstMethodSig(sig.GetCallingConvention(), (uint)sig.GenericArguments.Count);
			foreach (TypeSig type in sig.GenericArguments)
			{
				genericInstMethodSig.GenericArguments.Add(this.Import(type));
			}
			this.recursionCounter.Decrement();
			return genericInstMethodSig;
		}

		// Token: 0x06004943 RID: 18755 RVA: 0x00179480 File Offset: 0x00179480
		public IField Import(IField field)
		{
			if (field == null)
			{
				return null;
			}
			if (!this.recursionCounter.Increment())
			{
				return null;
			}
			FieldDef field2;
			IField result;
			MemberRef memberRef;
			if ((field2 = (field as FieldDef)) != null)
			{
				result = this.Import(field2);
			}
			else if ((memberRef = (field as MemberRef)) != null)
			{
				result = this.Import(memberRef);
			}
			else
			{
				result = null;
			}
			this.recursionCounter.Decrement();
			return result;
		}

		// Token: 0x06004944 RID: 18756 RVA: 0x001794F0 File Offset: 0x001794F0
		public IMethod Import(IMethod method)
		{
			if (method == null)
			{
				return null;
			}
			if (!this.recursionCounter.Increment())
			{
				return null;
			}
			MethodDef method2;
			IMethod result;
			MethodSpec method3;
			MemberRef memberRef;
			if ((method2 = (method as MethodDef)) != null)
			{
				result = this.Import(method2);
			}
			else if ((method3 = (method as MethodSpec)) != null)
			{
				result = this.Import(method3);
			}
			else if ((memberRef = (method as MemberRef)) != null)
			{
				result = this.Import(memberRef);
			}
			else
			{
				result = null;
			}
			this.recursionCounter.Decrement();
			return result;
		}

		// Token: 0x06004945 RID: 18757 RVA: 0x00179578 File Offset: 0x00179578
		public IField Import(FieldDef field)
		{
			if (field == null)
			{
				return null;
			}
			if (this.TryToUseFieldDefs && field.Module == this.module)
			{
				return field;
			}
			if (!this.recursionCounter.Increment())
			{
				return null;
			}
			ImportMapper importMapper = this.mapper;
			IField field2 = (importMapper != null) ? importMapper.Map(field) : null;
			if (field2 != null)
			{
				this.recursionCounter.Decrement();
				return field2;
			}
			MemberRefUser memberRefUser = this.module.UpdateRowId<MemberRefUser>(new MemberRefUser(this.module, field.Name));
			memberRefUser.Signature = this.Import(field.Signature);
			memberRefUser.Class = this.ImportParent(field.DeclaringType);
			this.recursionCounter.Decrement();
			return memberRefUser;
		}

		// Token: 0x06004946 RID: 18758 RVA: 0x0017963C File Offset: 0x0017963C
		private IMemberRefParent ImportParent(TypeDef type)
		{
			if (type == null)
			{
				return null;
			}
			if (type.IsGlobalModuleType)
			{
				ModuleDef moduleDef = this.module;
				ModuleDef moduleDef2 = this.module;
				ModuleDef moduleDef3 = type.Module;
				return moduleDef.UpdateRowId<ModuleRefUser>(new ModuleRefUser(moduleDef2, (moduleDef3 != null) ? moduleDef3.Name : null));
			}
			return this.Import(type);
		}

		// Token: 0x06004947 RID: 18759 RVA: 0x00179698 File Offset: 0x00179698
		public IMethod Import(MethodDef method)
		{
			if (method == null)
			{
				return null;
			}
			if (this.TryToUseMethodDefs && method.Module == this.module)
			{
				return method;
			}
			if (!this.recursionCounter.Increment())
			{
				return null;
			}
			ImportMapper importMapper = this.mapper;
			IMethod method2 = (importMapper != null) ? importMapper.Map(method) : null;
			if (method2 != null)
			{
				this.recursionCounter.Decrement();
				return method2;
			}
			MemberRefUser memberRefUser = this.module.UpdateRowId<MemberRefUser>(new MemberRefUser(this.module, method.Name));
			memberRefUser.Signature = this.Import(method.Signature);
			memberRefUser.Class = this.ImportParent(method.DeclaringType);
			this.recursionCounter.Decrement();
			return memberRefUser;
		}

		// Token: 0x06004948 RID: 18760 RVA: 0x0017975C File Offset: 0x0017975C
		public MethodSpec Import(MethodSpec method)
		{
			if (method == null)
			{
				return null;
			}
			if (!this.recursionCounter.Increment())
			{
				return null;
			}
			MethodSpecUser methodSpecUser = this.module.UpdateRowId<MethodSpecUser>(new MethodSpecUser((IMethodDefOrRef)this.Import(method.Method)));
			methodSpecUser.Instantiation = this.Import(method.Instantiation);
			this.recursionCounter.Decrement();
			return methodSpecUser;
		}

		// Token: 0x06004949 RID: 18761 RVA: 0x001797C8 File Offset: 0x001797C8
		public MemberRef Import(MemberRef memberRef)
		{
			if (memberRef == null)
			{
				return null;
			}
			if (!this.recursionCounter.Increment())
			{
				return null;
			}
			ImportMapper importMapper = this.mapper;
			MemberRef memberRef2 = (importMapper != null) ? importMapper.Map(memberRef) : null;
			if (memberRef2 != null)
			{
				this.recursionCounter.Decrement();
				return memberRef2;
			}
			MemberRef memberRef3 = this.module.UpdateRowId<MemberRefUser>(new MemberRefUser(this.module, memberRef.Name));
			memberRef3.Signature = this.Import(memberRef.Signature);
			memberRef3.Class = this.Import(memberRef.Class);
			if (memberRef3.Class == null)
			{
				memberRef3 = null;
			}
			this.recursionCounter.Decrement();
			return memberRef3;
		}

		// Token: 0x0600494A RID: 18762 RVA: 0x0017987C File Offset: 0x0017987C
		private IMemberRefParent Import(IMemberRefParent parent)
		{
			ITypeDefOrRef typeDefOrRef = parent as ITypeDefOrRef;
			if (typeDefOrRef != null)
			{
				TypeDef typeDef = typeDefOrRef as TypeDef;
				if (typeDef != null && typeDef.IsGlobalModuleType)
				{
					ModuleDef moduleDef = this.module;
					ModuleDef moduleDef2 = this.module;
					ModuleDef moduleDef3 = typeDef.Module;
					return moduleDef.UpdateRowId<ModuleRefUser>(new ModuleRefUser(moduleDef2, (moduleDef3 != null) ? moduleDef3.Name : null));
				}
				return this.Import(typeDefOrRef);
			}
			else
			{
				ModuleRef moduleRef = parent as ModuleRef;
				if (moduleRef != null)
				{
					return this.module.UpdateRowId<ModuleRefUser>(new ModuleRefUser(this.module, moduleRef.Name));
				}
				MethodDef methodDef = parent as MethodDef;
				if (methodDef == null)
				{
					return null;
				}
				TypeDef declaringType = methodDef.DeclaringType;
				if (declaringType != null && declaringType.Module == this.module)
				{
					return methodDef;
				}
				return null;
			}
		}

		// Token: 0x04002526 RID: 9510
		private readonly ModuleDef module;

		// Token: 0x04002527 RID: 9511
		internal readonly GenericParamContext gpContext;

		// Token: 0x04002528 RID: 9512
		private readonly ImportMapper mapper;

		// Token: 0x04002529 RID: 9513
		private RecursionCounter recursionCounter;

		// Token: 0x0400252A RID: 9514
		private ImporterOptions options;
	}
}
