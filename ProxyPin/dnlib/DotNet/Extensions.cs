using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x0200077A RID: 1914
	[ComVisible(true)]
	public static class Extensions
	{
		// Token: 0x0600435A RID: 17242 RVA: 0x00167E44 File Offset: 0x00167E44
		internal static string GetName(this AssemblyHashAlgorithm hashAlg)
		{
			switch (hashAlg)
			{
			case AssemblyHashAlgorithm.MD2:
				return null;
			case AssemblyHashAlgorithm.MD4:
				return null;
			case AssemblyHashAlgorithm.MD5:
				return "MD5";
			case AssemblyHashAlgorithm.SHA1:
				return "SHA1";
			case AssemblyHashAlgorithm.MAC:
				return null;
			case AssemblyHashAlgorithm.SSL3_SHAMD5:
				return null;
			case AssemblyHashAlgorithm.HMAC:
				return null;
			case AssemblyHashAlgorithm.TLS1PRF:
				return null;
			case AssemblyHashAlgorithm.HASH_REPLACE_OWF:
				return null;
			case AssemblyHashAlgorithm.SHA_256:
				return "SHA256";
			case AssemblyHashAlgorithm.SHA_384:
				return "SHA384";
			case AssemblyHashAlgorithm.SHA_512:
				return "SHA512";
			}
			return null;
		}

		// Token: 0x0600435B RID: 17243 RVA: 0x00167F0C File Offset: 0x00167F0C
		public static TypeSig GetFieldType(this FieldSig sig)
		{
			if (sig == null)
			{
				return null;
			}
			return sig.Type;
		}

		// Token: 0x0600435C RID: 17244 RVA: 0x00167F1C File Offset: 0x00167F1C
		public static TypeSig GetRetType(this MethodBaseSig sig)
		{
			if (sig == null)
			{
				return null;
			}
			return sig.RetType;
		}

		// Token: 0x0600435D RID: 17245 RVA: 0x00167F2C File Offset: 0x00167F2C
		public static IList<TypeSig> GetParams(this MethodBaseSig sig)
		{
			return ((sig != null) ? sig.Params : null) ?? new List<TypeSig>();
		}

		// Token: 0x0600435E RID: 17246 RVA: 0x00167F4C File Offset: 0x00167F4C
		public static int GetParamCount(this MethodBaseSig sig)
		{
			if (sig == null)
			{
				return 0;
			}
			return sig.Params.Count;
		}

		// Token: 0x0600435F RID: 17247 RVA: 0x00167F64 File Offset: 0x00167F64
		public static uint GetGenParamCount(this MethodBaseSig sig)
		{
			if (sig == null)
			{
				return 0U;
			}
			return sig.GenParamCount;
		}

		// Token: 0x06004360 RID: 17248 RVA: 0x00167F74 File Offset: 0x00167F74
		public static IList<TypeSig> GetParamsAfterSentinel(this MethodBaseSig sig)
		{
			if (sig == null)
			{
				return null;
			}
			return sig.ParamsAfterSentinel;
		}

		// Token: 0x06004361 RID: 17249 RVA: 0x00167F84 File Offset: 0x00167F84
		public static IList<TypeSig> GetLocals(this LocalSig sig)
		{
			return ((sig != null) ? sig.Locals : null) ?? new List<TypeSig>();
		}

		// Token: 0x06004362 RID: 17250 RVA: 0x00167FA4 File Offset: 0x00167FA4
		public static IList<TypeSig> GetGenericArguments(this GenericInstMethodSig sig)
		{
			return ((sig != null) ? sig.GenericArguments : null) ?? new List<TypeSig>();
		}

		// Token: 0x06004363 RID: 17251 RVA: 0x00167FC4 File Offset: 0x00167FC4
		public static bool GetIsDefault(this CallingConventionSig sig)
		{
			return sig != null && sig.IsDefault;
		}

		// Token: 0x06004364 RID: 17252 RVA: 0x00167FD4 File Offset: 0x00167FD4
		public static bool IsPrimitive(this ElementType etype)
		{
			return etype - ElementType.Boolean <= 11 || etype - ElementType.I <= 2;
		}

		// Token: 0x06004365 RID: 17253 RVA: 0x00167FF0 File Offset: 0x00167FF0
		public static int GetPrimitiveSize(this ElementType etype, int ptrSize = -1)
		{
			switch (etype)
			{
			case ElementType.Boolean:
			case ElementType.I1:
			case ElementType.U1:
				return 1;
			case ElementType.Char:
			case ElementType.I2:
			case ElementType.U2:
				return 2;
			case ElementType.I4:
			case ElementType.U4:
			case ElementType.R4:
				return 4;
			case ElementType.I8:
			case ElementType.U8:
			case ElementType.R8:
				return 8;
			case ElementType.Ptr:
			case ElementType.I:
			case ElementType.U:
			case ElementType.FnPtr:
				return ptrSize;
			}
			return -1;
		}

		// Token: 0x06004366 RID: 17254 RVA: 0x00168084 File Offset: 0x00168084
		public static bool IsValueType(this ElementType etype)
		{
			switch (etype)
			{
			case ElementType.Void:
			case ElementType.Boolean:
			case ElementType.Char:
			case ElementType.I1:
			case ElementType.U1:
			case ElementType.I2:
			case ElementType.U2:
			case ElementType.I4:
			case ElementType.U4:
			case ElementType.I8:
			case ElementType.U8:
			case ElementType.R4:
			case ElementType.R8:
			case ElementType.ValueType:
			case ElementType.TypedByRef:
			case ElementType.ValueArray:
			case ElementType.I:
			case ElementType.U:
			case ElementType.R:
				return true;
			case ElementType.GenericInst:
				return false;
			}
			return false;
		}

		// Token: 0x06004367 RID: 17255 RVA: 0x001681C0 File Offset: 0x001681C0
		public static AssemblyDef Resolve(this IAssemblyResolver self, AssemblyName assembly, ModuleDef sourceModule)
		{
			if (assembly == null)
			{
				return null;
			}
			return self.Resolve(new AssemblyNameInfo(assembly), sourceModule);
		}

		// Token: 0x06004368 RID: 17256 RVA: 0x001681D8 File Offset: 0x001681D8
		public static AssemblyDef Resolve(this IAssemblyResolver self, string asmFullName, ModuleDef sourceModule)
		{
			if (asmFullName == null)
			{
				return null;
			}
			return self.Resolve(new AssemblyNameInfo(asmFullName), sourceModule);
		}

		// Token: 0x06004369 RID: 17257 RVA: 0x001681F0 File Offset: 0x001681F0
		public static AssemblyDef ResolveThrow(this IAssemblyResolver self, IAssembly assembly, ModuleDef sourceModule)
		{
			if (assembly == null)
			{
				return null;
			}
			AssemblyDef assemblyDef = self.Resolve(assembly, sourceModule);
			if (assemblyDef != null)
			{
				return assemblyDef;
			}
			throw new AssemblyResolveException(string.Format("Could not resolve assembly: {0}", assembly));
		}

		// Token: 0x0600436A RID: 17258 RVA: 0x0016822C File Offset: 0x0016822C
		public static AssemblyDef ResolveThrow(this IAssemblyResolver self, AssemblyName assembly, ModuleDef sourceModule)
		{
			if (assembly == null)
			{
				return null;
			}
			AssemblyDef assemblyDef = self.Resolve(new AssemblyNameInfo(assembly), sourceModule);
			if (assemblyDef != null)
			{
				return assemblyDef;
			}
			throw new AssemblyResolveException(string.Format("Could not resolve assembly: {0}", assembly));
		}

		// Token: 0x0600436B RID: 17259 RVA: 0x0016826C File Offset: 0x0016826C
		public static AssemblyDef ResolveThrow(this IAssemblyResolver self, string asmFullName, ModuleDef sourceModule)
		{
			if (asmFullName == null)
			{
				return null;
			}
			AssemblyDef assemblyDef = self.Resolve(new AssemblyNameInfo(asmFullName), sourceModule);
			if (assemblyDef != null)
			{
				return assemblyDef;
			}
			throw new AssemblyResolveException("Could not resolve assembly: " + asmFullName);
		}

		// Token: 0x0600436C RID: 17260 RVA: 0x001682AC File Offset: 0x001682AC
		public static bool IsCorLib(this IAssembly asm)
		{
			AssemblyDef assemblyDef = asm as AssemblyDef;
			if (assemblyDef != null)
			{
				ModuleDef manifestModule = assemblyDef.ManifestModule;
				if (manifestModule != null)
				{
					bool? isCoreLibraryModule = manifestModule.IsCoreLibraryModule;
					if (isCoreLibraryModule != null)
					{
						return isCoreLibraryModule.Value;
					}
				}
			}
			string text;
			return asm != null && UTF8String.IsNullOrEmpty(asm.Culture) && ((text = UTF8String.ToSystemStringOrEmpty(asm.Name)).Equals("mscorlib", StringComparison.OrdinalIgnoreCase) || text.Equals("System.Runtime", StringComparison.OrdinalIgnoreCase) || text.Equals("System.Private.CoreLib", StringComparison.OrdinalIgnoreCase) || text.Equals("netstandard", StringComparison.OrdinalIgnoreCase) || text.Equals("corefx", StringComparison.OrdinalIgnoreCase));
		}

		// Token: 0x0600436D RID: 17261 RVA: 0x00168368 File Offset: 0x00168368
		public static AssemblyRef ToAssemblyRef(this IAssembly asm)
		{
			if (asm == null)
			{
				return null;
			}
			return new AssemblyRefUser(asm.Name, asm.Version, asm.PublicKeyOrToken, asm.Culture)
			{
				Attributes = asm.Attributes
			};
		}

		// Token: 0x0600436E RID: 17262 RVA: 0x001683AC File Offset: 0x001683AC
		public static TypeSig ToTypeSig(this ITypeDefOrRef type, bool checkValueType = true)
		{
			if (type == null)
			{
				return null;
			}
			ModuleDef module = type.Module;
			if (module != null)
			{
				CorLibTypeSig corLibTypeSig = module.CorLibTypes.GetCorLibTypeSig(type);
				if (corLibTypeSig != null)
				{
					return corLibTypeSig;
				}
			}
			TypeDef typeDef = type as TypeDef;
			if (typeDef != null)
			{
				return Extensions.CreateClassOrValueType(type, checkValueType && typeDef.IsValueType);
			}
			TypeRef typeRef = type as TypeRef;
			if (typeRef != null)
			{
				if (checkValueType)
				{
					typeDef = typeRef.Resolve();
				}
				return Extensions.CreateClassOrValueType(type, typeDef != null && typeDef.IsValueType);
			}
			TypeSpec typeSpec = type as TypeSpec;
			if (typeSpec != null)
			{
				return typeSpec.TypeSig;
			}
			return null;
		}

		// Token: 0x0600436F RID: 17263 RVA: 0x00168458 File Offset: 0x00168458
		private static TypeSig CreateClassOrValueType(ITypeDefOrRef type, bool isValueType)
		{
			if (isValueType)
			{
				return new ValueTypeSig(type);
			}
			return new ClassSig(type);
		}

		// Token: 0x06004370 RID: 17264 RVA: 0x00168470 File Offset: 0x00168470
		public static TypeDefOrRefSig TryGetTypeDefOrRefSig(this ITypeDefOrRef type)
		{
			TypeSpec typeSpec = type as TypeSpec;
			if (typeSpec != null)
			{
				return typeSpec.TypeSig.RemovePinnedAndModifiers() as TypeDefOrRefSig;
			}
			return null;
		}

		// Token: 0x06004371 RID: 17265 RVA: 0x001684A0 File Offset: 0x001684A0
		public static ClassOrValueTypeSig TryGetClassOrValueTypeSig(this ITypeDefOrRef type)
		{
			TypeSpec typeSpec = type as TypeSpec;
			if (typeSpec != null)
			{
				return typeSpec.TypeSig.RemovePinnedAndModifiers() as ClassOrValueTypeSig;
			}
			return null;
		}

		// Token: 0x06004372 RID: 17266 RVA: 0x001684D0 File Offset: 0x001684D0
		public static ValueTypeSig TryGetValueTypeSig(this ITypeDefOrRef type)
		{
			TypeSpec typeSpec = type as TypeSpec;
			if (typeSpec != null)
			{
				return typeSpec.TypeSig.RemovePinnedAndModifiers() as ValueTypeSig;
			}
			return null;
		}

		// Token: 0x06004373 RID: 17267 RVA: 0x00168500 File Offset: 0x00168500
		public static ClassSig TryGetClassSig(this ITypeDefOrRef type)
		{
			TypeSpec typeSpec = type as TypeSpec;
			if (typeSpec != null)
			{
				return typeSpec.TypeSig.RemovePinnedAndModifiers() as ClassSig;
			}
			return null;
		}

		// Token: 0x06004374 RID: 17268 RVA: 0x00168530 File Offset: 0x00168530
		public static GenericSig TryGetGenericSig(this ITypeDefOrRef type)
		{
			TypeSpec typeSpec = type as TypeSpec;
			if (typeSpec != null)
			{
				return typeSpec.TypeSig.RemovePinnedAndModifiers() as GenericSig;
			}
			return null;
		}

		// Token: 0x06004375 RID: 17269 RVA: 0x00168560 File Offset: 0x00168560
		public static GenericVar TryGetGenericVar(this ITypeDefOrRef type)
		{
			TypeSpec typeSpec = type as TypeSpec;
			if (typeSpec != null)
			{
				return typeSpec.TypeSig.RemovePinnedAndModifiers() as GenericVar;
			}
			return null;
		}

		// Token: 0x06004376 RID: 17270 RVA: 0x00168590 File Offset: 0x00168590
		public static GenericMVar TryGetGenericMVar(this ITypeDefOrRef type)
		{
			TypeSpec typeSpec = type as TypeSpec;
			if (typeSpec != null)
			{
				return typeSpec.TypeSig.RemovePinnedAndModifiers() as GenericMVar;
			}
			return null;
		}

		// Token: 0x06004377 RID: 17271 RVA: 0x001685C0 File Offset: 0x001685C0
		public static GenericInstSig TryGetGenericInstSig(this ITypeDefOrRef type)
		{
			TypeSpec typeSpec = type as TypeSpec;
			if (typeSpec != null)
			{
				return typeSpec.TypeSig.RemovePinnedAndModifiers() as GenericInstSig;
			}
			return null;
		}

		// Token: 0x06004378 RID: 17272 RVA: 0x001685F0 File Offset: 0x001685F0
		public static PtrSig TryGetPtrSig(this ITypeDefOrRef type)
		{
			TypeSpec typeSpec = type as TypeSpec;
			if (typeSpec != null)
			{
				return typeSpec.TypeSig.RemovePinnedAndModifiers() as PtrSig;
			}
			return null;
		}

		// Token: 0x06004379 RID: 17273 RVA: 0x00168620 File Offset: 0x00168620
		public static ByRefSig TryGetByRefSig(this ITypeDefOrRef type)
		{
			TypeSpec typeSpec = type as TypeSpec;
			if (typeSpec != null)
			{
				return typeSpec.TypeSig.RemovePinnedAndModifiers() as ByRefSig;
			}
			return null;
		}

		// Token: 0x0600437A RID: 17274 RVA: 0x00168650 File Offset: 0x00168650
		public static ArraySig TryGetArraySig(this ITypeDefOrRef type)
		{
			TypeSpec typeSpec = type as TypeSpec;
			if (typeSpec != null)
			{
				return typeSpec.TypeSig.RemovePinnedAndModifiers() as ArraySig;
			}
			return null;
		}

		// Token: 0x0600437B RID: 17275 RVA: 0x00168680 File Offset: 0x00168680
		public static SZArraySig TryGetSZArraySig(this ITypeDefOrRef type)
		{
			TypeSpec typeSpec = type as TypeSpec;
			if (typeSpec != null)
			{
				return typeSpec.TypeSig.RemovePinnedAndModifiers() as SZArraySig;
			}
			return null;
		}

		// Token: 0x0600437C RID: 17276 RVA: 0x001686B0 File Offset: 0x001686B0
		public static ITypeDefOrRef GetBaseTypeThrow(this ITypeDefOrRef tdr)
		{
			return tdr.GetBaseType(true);
		}

		// Token: 0x0600437D RID: 17277 RVA: 0x001686BC File Offset: 0x001686BC
		public static ITypeDefOrRef GetBaseType(this ITypeDefOrRef tdr, bool throwOnResolveFailure = false)
		{
			TypeDef typeDef = tdr as TypeDef;
			if (typeDef != null)
			{
				return typeDef.BaseType;
			}
			TypeRef typeRef = tdr as TypeRef;
			if (typeRef != null)
			{
				typeDef = (throwOnResolveFailure ? typeRef.ResolveThrow() : typeRef.Resolve());
				if (typeDef == null)
				{
					return null;
				}
				return typeDef.BaseType;
			}
			else
			{
				TypeSpec typeSpec = tdr as TypeSpec;
				if (typeSpec == null)
				{
					return null;
				}
				GenericInstSig genericInstSig = typeSpec.TypeSig.ToGenericInstSig();
				if (genericInstSig != null)
				{
					ClassOrValueTypeSig genericType = genericInstSig.GenericType;
					tdr = ((genericType != null) ? genericType.TypeDefOrRef : null);
				}
				else
				{
					TypeDefOrRefSig typeDefOrRefSig = typeSpec.TypeSig.ToTypeDefOrRefSig();
					tdr = ((typeDefOrRefSig != null) ? typeDefOrRefSig.TypeDefOrRef : null);
				}
				typeDef = (tdr as TypeDef);
				if (typeDef != null)
				{
					return typeDef.BaseType;
				}
				typeRef = (tdr as TypeRef);
				if (typeRef == null)
				{
					return null;
				}
				typeDef = (throwOnResolveFailure ? typeRef.ResolveThrow() : typeRef.Resolve());
				if (typeDef == null)
				{
					return null;
				}
				return typeDef.BaseType;
			}
		}

		// Token: 0x0600437E RID: 17278 RVA: 0x001687C0 File Offset: 0x001687C0
		public static TypeDef ResolveTypeDef(this ITypeDefOrRef tdr)
		{
			TypeDef typeDef = tdr as TypeDef;
			if (typeDef != null)
			{
				return typeDef;
			}
			TypeRef typeRef = tdr as TypeRef;
			if (typeRef != null)
			{
				return typeRef.Resolve();
			}
			if (tdr == null)
			{
				return null;
			}
			tdr = tdr.ScopeType;
			typeDef = (tdr as TypeDef);
			if (typeDef != null)
			{
				return typeDef;
			}
			typeRef = (tdr as TypeRef);
			if (typeRef != null)
			{
				return typeRef.Resolve();
			}
			return null;
		}

		// Token: 0x0600437F RID: 17279 RVA: 0x00168828 File Offset: 0x00168828
		public static TypeDef ResolveTypeDefThrow(this ITypeDefOrRef tdr)
		{
			TypeDef typeDef = tdr as TypeDef;
			if (typeDef != null)
			{
				return typeDef;
			}
			TypeRef typeRef = tdr as TypeRef;
			if (typeRef != null)
			{
				return typeRef.ResolveThrow();
			}
			if (tdr == null)
			{
				throw new TypeResolveException("Can't resolve a null pointer");
			}
			tdr = tdr.ScopeType;
			typeDef = (tdr as TypeDef);
			if (typeDef != null)
			{
				return typeDef;
			}
			typeRef = (tdr as TypeRef);
			if (typeRef != null)
			{
				return typeRef.ResolveThrow();
			}
			throw new TypeResolveException(string.Format("Could not resolve type: {0} ({1})", tdr, (tdr != null) ? tdr.DefinitionAssembly : null));
		}

		// Token: 0x06004380 RID: 17280 RVA: 0x001688BC File Offset: 0x001688BC
		public static FieldDef ResolveFieldDef(this IField field)
		{
			FieldDef fieldDef = field as FieldDef;
			if (fieldDef != null)
			{
				return fieldDef;
			}
			MemberRef memberRef = field as MemberRef;
			if (memberRef != null)
			{
				return memberRef.ResolveField();
			}
			return null;
		}

		// Token: 0x06004381 RID: 17281 RVA: 0x001688F4 File Offset: 0x001688F4
		public static FieldDef ResolveFieldDefThrow(this IField field)
		{
			FieldDef fieldDef = field as FieldDef;
			if (fieldDef != null)
			{
				return fieldDef;
			}
			MemberRef memberRef = field as MemberRef;
			if (memberRef != null)
			{
				return memberRef.ResolveFieldThrow();
			}
			throw new MemberRefResolveException(string.Format("Could not resolve field: {0}", field));
		}

		// Token: 0x06004382 RID: 17282 RVA: 0x00168938 File Offset: 0x00168938
		public static MethodDef ResolveMethodDef(this IMethod method)
		{
			MethodDef methodDef = method as MethodDef;
			if (methodDef != null)
			{
				return methodDef;
			}
			MemberRef memberRef = method as MemberRef;
			if (memberRef != null)
			{
				return memberRef.ResolveMethod();
			}
			MethodSpec methodSpec = method as MethodSpec;
			if (methodSpec != null)
			{
				methodDef = (methodSpec.Method as MethodDef);
				if (methodDef != null)
				{
					return methodDef;
				}
				memberRef = (methodSpec.Method as MemberRef);
				if (memberRef != null)
				{
					return memberRef.ResolveMethod();
				}
			}
			return null;
		}

		// Token: 0x06004383 RID: 17283 RVA: 0x001689A8 File Offset: 0x001689A8
		public static MethodDef ResolveMethodDefThrow(this IMethod method)
		{
			MethodDef methodDef = method as MethodDef;
			if (methodDef != null)
			{
				return methodDef;
			}
			MemberRef memberRef = method as MemberRef;
			if (memberRef != null)
			{
				return memberRef.ResolveMethodThrow();
			}
			MethodSpec methodSpec = method as MethodSpec;
			if (methodSpec != null)
			{
				methodDef = (methodSpec.Method as MethodDef);
				if (methodDef != null)
				{
					return methodDef;
				}
				memberRef = (methodSpec.Method as MemberRef);
				if (memberRef != null)
				{
					return memberRef.ResolveMethodThrow();
				}
			}
			throw new MemberRefResolveException(string.Format("Could not resolve method: {0}", method));
		}

		// Token: 0x06004384 RID: 17284 RVA: 0x00168A28 File Offset: 0x00168A28
		internal static IAssembly GetDefinitionAssembly(this MemberRef mr)
		{
			if (mr == null)
			{
				return null;
			}
			IMemberRefParent @class = mr.Class;
			ITypeDefOrRef typeDefOrRef = @class as ITypeDefOrRef;
			if (typeDefOrRef != null)
			{
				return typeDefOrRef.DefinitionAssembly;
			}
			if (@class is ModuleRef)
			{
				ModuleDef module = mr.Module;
				if (module == null)
				{
					return null;
				}
				return module.Assembly;
			}
			else
			{
				MethodDef methodDef = @class as MethodDef;
				if (methodDef == null)
				{
					return null;
				}
				TypeDef declaringType = methodDef.DeclaringType;
				if (declaringType == null)
				{
					return null;
				}
				return declaringType.DefinitionAssembly;
			}
		}

		// Token: 0x06004385 RID: 17285 RVA: 0x00168AA0 File Offset: 0x00168AA0
		public static IList<TypeSig> GetParams(this IMethod method)
		{
			if (method == null)
			{
				return null;
			}
			return method.MethodSig.GetParams();
		}

		// Token: 0x06004386 RID: 17286 RVA: 0x00168AB8 File Offset: 0x00168AB8
		public static int GetParamCount(this IMethod method)
		{
			if (method == null)
			{
				return 0;
			}
			return method.MethodSig.GetParamCount();
		}

		// Token: 0x06004387 RID: 17287 RVA: 0x00168AD0 File Offset: 0x00168AD0
		public static bool HasParams(this IMethod method)
		{
			return method.GetParamCount() > 0;
		}

		// Token: 0x06004388 RID: 17288 RVA: 0x00168ADC File Offset: 0x00168ADC
		public static TypeSig GetParam(this IMethod method, int index)
		{
			if (method == null)
			{
				return null;
			}
			MethodSig methodSig = method.MethodSig;
			if (methodSig == null)
			{
				return null;
			}
			return methodSig.Params[index];
		}

		// Token: 0x06004389 RID: 17289 RVA: 0x00168B00 File Offset: 0x00168B00
		public static ITypeDefOrRef ToTypeDefOrRef(this TypeSig sig)
		{
			if (sig == null)
			{
				return null;
			}
			TypeDefOrRefSig typeDefOrRefSig = sig as TypeDefOrRefSig;
			if (typeDefOrRefSig != null)
			{
				return typeDefOrRefSig.TypeDefOrRef;
			}
			ModuleDef module = sig.Module;
			if (module == null)
			{
				return new TypeSpecUser(sig);
			}
			return module.UpdateRowId<TypeSpecUser>(new TypeSpecUser(sig));
		}

		// Token: 0x0600438A RID: 17290 RVA: 0x00168B50 File Offset: 0x00168B50
		internal static bool IsPrimitive(this IType tdr)
		{
			if (tdr == null)
			{
				return false;
			}
			if (!tdr.DefinitionAssembly.IsCorLib())
			{
				return false;
			}
			string fullName = tdr.FullName;
			if (fullName != null)
			{
				uint num = dnlib.<PrivateImplementationDetails>.ComputeStringHash(fullName);
				if (num <= 1764058053U)
				{
					if (num <= 875577056U)
					{
						if (num != 347085918U)
						{
							if (num != 848225627U)
							{
								if (num != 875577056U)
								{
									return false;
								}
								if (!(fullName == "System.UInt64"))
								{
									return false;
								}
							}
							else if (!(fullName == "System.Double"))
							{
								return false;
							}
						}
						else if (!(fullName == "System.Boolean"))
						{
							return false;
						}
					}
					else if (num <= 1599499907U)
					{
						if (num != 942540437U)
						{
							if (num != 1599499907U)
							{
								return false;
							}
							if (!(fullName == "System.IntPtr"))
							{
								return false;
							}
						}
						else if (!(fullName == "System.UInt16"))
						{
							return false;
						}
					}
					else if (num != 1697786220U)
					{
						if (num != 1764058053U)
						{
							return false;
						}
						if (!(fullName == "System.Int64"))
						{
							return false;
						}
					}
					else if (!(fullName == "System.Int16"))
					{
						return false;
					}
				}
				else if (num <= 2747029693U)
				{
					if (num != 2185383742U)
					{
						if (num != 2249825754U)
						{
							if (num != 2747029693U)
							{
								return false;
							}
							if (!(fullName == "System.SByte"))
							{
								return false;
							}
						}
						else if (!(fullName == "System.Char"))
						{
							return false;
						}
					}
					else if (!(fullName == "System.Single"))
					{
						return false;
					}
				}
				else if (num <= 3291009739U)
				{
					if (num != 3079944380U)
					{
						if (num != 3291009739U)
						{
							return false;
						}
						if (!(fullName == "System.UInt32"))
						{
							return false;
						}
					}
					else if (!(fullName == "System.Byte"))
					{
						return false;
					}
				}
				else if (num != 3482805428U)
				{
					if (num != 4180476474U)
					{
						return false;
					}
					if (!(fullName == "System.Int32"))
					{
						return false;
					}
				}
				else if (!(fullName == "System.UIntPtr"))
				{
					return false;
				}
				return true;
			}
			return false;
		}

		// Token: 0x0600438B RID: 17291 RVA: 0x00168DA4 File Offset: 0x00168DA4
		public static CorLibTypeSig GetCorLibTypeSig(this ICorLibTypes self, ITypeDefOrRef type)
		{
			TypeDef typeDef = type as TypeDef;
			CorLibTypeSig corLibTypeSig;
			if (typeDef != null && typeDef.DeclaringType == null && (corLibTypeSig = self.GetCorLibTypeSig(typeDef.Namespace, typeDef.Name, typeDef.DefinitionAssembly)) != null)
			{
				return corLibTypeSig;
			}
			TypeRef typeRef = type as TypeRef;
			if (typeRef != null && !(typeRef.ResolutionScope is TypeRef) && (corLibTypeSig = self.GetCorLibTypeSig(typeRef.Namespace, typeRef.Name, typeRef.DefinitionAssembly)) != null)
			{
				return corLibTypeSig;
			}
			return null;
		}

		// Token: 0x0600438C RID: 17292 RVA: 0x00168E30 File Offset: 0x00168E30
		public static CorLibTypeSig GetCorLibTypeSig(this ICorLibTypes self, UTF8String @namespace, UTF8String name, IAssembly defAsm)
		{
			return self.GetCorLibTypeSig(UTF8String.ToSystemStringOrEmpty(@namespace), UTF8String.ToSystemStringOrEmpty(name), defAsm);
		}

		// Token: 0x0600438D RID: 17293 RVA: 0x00168E48 File Offset: 0x00168E48
		public static CorLibTypeSig GetCorLibTypeSig(this ICorLibTypes self, string @namespace, string name, IAssembly defAsm)
		{
			if (@namespace != "System")
			{
				return null;
			}
			if (defAsm == null || !defAsm.IsCorLib())
			{
				return null;
			}
			if (name != null)
			{
				uint num = dnlib.<PrivateImplementationDetails>.ComputeStringHash(name);
				if (num <= 2187444805U)
				{
					if (num <= 765439473U)
					{
						if (num <= 679076413U)
						{
							if (num != 423635464U)
							{
								if (num == 679076413U)
								{
									if (name == "Char")
									{
										return self.Char;
									}
								}
							}
							else if (name == "SByte")
							{
								return self.SByte;
							}
						}
						else if (num != 697196164U)
						{
							if (num == 765439473U)
							{
								if (name == "Int16")
								{
									return self.Int16;
								}
							}
						}
						else if (name == "Int64")
						{
							return self.Int64;
						}
					}
					else if (num <= 1324880019U)
					{
						if (num != 1323747186U)
						{
							if (num == 1324880019U)
							{
								if (name == "UInt64")
								{
									return self.UInt64;
								}
							}
						}
						else if (name == "UInt16")
						{
							return self.UInt16;
						}
					}
					else if (num != 1489158872U)
					{
						if (num != 1615808600U)
						{
							if (num == 2187444805U)
							{
								if (name == "UIntPtr")
								{
									return self.UIntPtr;
								}
							}
						}
						else if (name == "String")
						{
							return self.String;
						}
					}
					else if (name == "IntPtr")
					{
						return self.IntPtr;
					}
				}
				else if (num <= 3370340735U)
				{
					if (num <= 2711245919U)
					{
						if (num != 2386971688U)
						{
							if (num == 2711245919U)
							{
								if (name == "Int32")
								{
									return self.Int32;
								}
							}
						}
						else if (name == "Double")
						{
							return self.Double;
						}
					}
					else if (num != 3145356080U)
					{
						if (num == 3370340735U)
						{
							if (name == "Void")
							{
								return self.Void;
							}
						}
					}
					else if (name == "TypedReference")
					{
						return self.TypedReference;
					}
				}
				else if (num <= 3538687084U)
				{
					if (num != 3409549631U)
					{
						if (num == 3538687084U)
						{
							if (name == "UInt32")
							{
								return self.UInt32;
							}
						}
					}
					else if (name == "Byte")
					{
						return self.Byte;
					}
				}
				else if (num != 3851314394U)
				{
					if (num != 3969205087U)
					{
						if (num == 4051133705U)
						{
							if (name == "Single")
							{
								return self.Single;
							}
						}
					}
					else if (name == "Boolean")
					{
						return self.Boolean;
					}
				}
				else if (name == "Object")
				{
					return self.Object;
				}
			}
			return null;
		}

		// Token: 0x0600438E RID: 17294 RVA: 0x0016921C File Offset: 0x0016921C
		public static void Error(this ILogger logger, object sender, string message)
		{
			logger.Log(sender, LoggerEvent.Error, "{0}", new object[]
			{
				message
			});
		}

		// Token: 0x0600438F RID: 17295 RVA: 0x00169244 File Offset: 0x00169244
		public static void Error(this ILogger logger, object sender, string message, object arg1)
		{
			logger.Log(sender, LoggerEvent.Error, message, new object[]
			{
				arg1
			});
		}

		// Token: 0x06004390 RID: 17296 RVA: 0x00169268 File Offset: 0x00169268
		public static void Error(this ILogger logger, object sender, string message, object arg1, object arg2)
		{
			logger.Log(sender, LoggerEvent.Error, message, new object[]
			{
				arg1,
				arg2
			});
		}

		// Token: 0x06004391 RID: 17297 RVA: 0x00169294 File Offset: 0x00169294
		public static void Error(this ILogger logger, object sender, string message, object arg1, object arg2, object arg3)
		{
			logger.Log(sender, LoggerEvent.Error, message, new object[]
			{
				arg1,
				arg2,
				arg3
			});
		}

		// Token: 0x06004392 RID: 17298 RVA: 0x001692C4 File Offset: 0x001692C4
		public static void Error(this ILogger logger, object sender, string message, object arg1, object arg2, object arg3, object arg4)
		{
			logger.Log(sender, LoggerEvent.Error, message, new object[]
			{
				arg1,
				arg2,
				arg3,
				arg4
			});
		}

		// Token: 0x06004393 RID: 17299 RVA: 0x001692F8 File Offset: 0x001692F8
		public static void Error(this ILogger logger, object sender, string message, params object[] args)
		{
			logger.Log(sender, LoggerEvent.Error, message, args);
		}

		// Token: 0x06004394 RID: 17300 RVA: 0x00169304 File Offset: 0x00169304
		public static void Warning(this ILogger logger, object sender, string message)
		{
			logger.Log(sender, LoggerEvent.Warning, "{0}", new object[]
			{
				message
			});
		}

		// Token: 0x06004395 RID: 17301 RVA: 0x0016932C File Offset: 0x0016932C
		public static void Warning(this ILogger logger, object sender, string message, object arg1)
		{
			logger.Log(sender, LoggerEvent.Warning, message, new object[]
			{
				arg1
			});
		}

		// Token: 0x06004396 RID: 17302 RVA: 0x00169350 File Offset: 0x00169350
		public static void Warning(this ILogger logger, object sender, string message, object arg1, object arg2)
		{
			logger.Log(sender, LoggerEvent.Warning, message, new object[]
			{
				arg1,
				arg2
			});
		}

		// Token: 0x06004397 RID: 17303 RVA: 0x0016937C File Offset: 0x0016937C
		public static void Warning(this ILogger logger, object sender, string message, object arg1, object arg2, object arg3)
		{
			logger.Log(sender, LoggerEvent.Warning, message, new object[]
			{
				arg1,
				arg2,
				arg3
			});
		}

		// Token: 0x06004398 RID: 17304 RVA: 0x001693AC File Offset: 0x001693AC
		public static void Warning(this ILogger logger, object sender, string message, object arg1, object arg2, object arg3, object arg4)
		{
			logger.Log(sender, LoggerEvent.Warning, message, new object[]
			{
				arg1,
				arg2,
				arg3,
				arg4
			});
		}

		// Token: 0x06004399 RID: 17305 RVA: 0x001693E0 File Offset: 0x001693E0
		public static void Warning(this ILogger logger, object sender, string message, params object[] args)
		{
			logger.Log(sender, LoggerEvent.Warning, message, args);
		}

		// Token: 0x0600439A RID: 17306 RVA: 0x001693EC File Offset: 0x001693EC
		public static void Info(this ILogger logger, object sender, string message)
		{
			logger.Log(sender, LoggerEvent.Info, "{0}", new object[]
			{
				message
			});
		}

		// Token: 0x0600439B RID: 17307 RVA: 0x00169414 File Offset: 0x00169414
		public static void Info(this ILogger logger, object sender, string message, object arg1)
		{
			logger.Log(sender, LoggerEvent.Info, message, new object[]
			{
				arg1
			});
		}

		// Token: 0x0600439C RID: 17308 RVA: 0x00169438 File Offset: 0x00169438
		public static void Info(this ILogger logger, object sender, string message, object arg1, object arg2)
		{
			logger.Log(sender, LoggerEvent.Info, message, new object[]
			{
				arg1,
				arg2
			});
		}

		// Token: 0x0600439D RID: 17309 RVA: 0x00169464 File Offset: 0x00169464
		public static void Info(this ILogger logger, object sender, string message, object arg1, object arg2, object arg3)
		{
			logger.Log(sender, LoggerEvent.Info, message, new object[]
			{
				arg1,
				arg2,
				arg3
			});
		}

		// Token: 0x0600439E RID: 17310 RVA: 0x00169494 File Offset: 0x00169494
		public static void Info(this ILogger logger, object sender, string message, object arg1, object arg2, object arg3, object arg4)
		{
			logger.Log(sender, LoggerEvent.Info, message, new object[]
			{
				arg1,
				arg2,
				arg3,
				arg4
			});
		}

		// Token: 0x0600439F RID: 17311 RVA: 0x001694C8 File Offset: 0x001694C8
		public static void Info(this ILogger logger, object sender, string message, params object[] args)
		{
			logger.Log(sender, LoggerEvent.Info, message, args);
		}

		// Token: 0x060043A0 RID: 17312 RVA: 0x001694D4 File Offset: 0x001694D4
		public static void Verbose(this ILogger logger, object sender, string message)
		{
			logger.Log(sender, LoggerEvent.Verbose, "{0}", new object[]
			{
				message
			});
		}

		// Token: 0x060043A1 RID: 17313 RVA: 0x001694FC File Offset: 0x001694FC
		public static void Verbose(this ILogger logger, object sender, string message, object arg1)
		{
			logger.Log(sender, LoggerEvent.Verbose, message, new object[]
			{
				arg1
			});
		}

		// Token: 0x060043A2 RID: 17314 RVA: 0x00169520 File Offset: 0x00169520
		public static void Verbose(this ILogger logger, object sender, string message, object arg1, object arg2)
		{
			logger.Log(sender, LoggerEvent.Verbose, message, new object[]
			{
				arg1,
				arg2
			});
		}

		// Token: 0x060043A3 RID: 17315 RVA: 0x0016954C File Offset: 0x0016954C
		public static void Verbose(this ILogger logger, object sender, string message, object arg1, object arg2, object arg3)
		{
			logger.Log(sender, LoggerEvent.Verbose, message, new object[]
			{
				arg1,
				arg2,
				arg3
			});
		}

		// Token: 0x060043A4 RID: 17316 RVA: 0x0016957C File Offset: 0x0016957C
		public static void Verbose(this ILogger logger, object sender, string message, object arg1, object arg2, object arg3, object arg4)
		{
			logger.Log(sender, LoggerEvent.Verbose, message, new object[]
			{
				arg1,
				arg2,
				arg3,
				arg4
			});
		}

		// Token: 0x060043A5 RID: 17317 RVA: 0x001695B0 File Offset: 0x001695B0
		public static void Verbose(this ILogger logger, object sender, string message, params object[] args)
		{
			logger.Log(sender, LoggerEvent.Verbose, message, args);
		}

		// Token: 0x060043A6 RID: 17318 RVA: 0x001695BC File Offset: 0x001695BC
		public static void VeryVerbose(this ILogger logger, object sender, string message)
		{
			logger.Log(sender, LoggerEvent.VeryVerbose, "{0}", new object[]
			{
				message
			});
		}

		// Token: 0x060043A7 RID: 17319 RVA: 0x001695E4 File Offset: 0x001695E4
		public static void VeryVerbose(this ILogger logger, object sender, string message, object arg1)
		{
			logger.Log(sender, LoggerEvent.VeryVerbose, message, new object[]
			{
				arg1
			});
		}

		// Token: 0x060043A8 RID: 17320 RVA: 0x00169608 File Offset: 0x00169608
		public static void VeryVerbose(this ILogger logger, object sender, string message, object arg1, object arg2)
		{
			logger.Log(sender, LoggerEvent.VeryVerbose, message, new object[]
			{
				arg1,
				arg2
			});
		}

		// Token: 0x060043A9 RID: 17321 RVA: 0x00169634 File Offset: 0x00169634
		public static void VeryVerbose(this ILogger logger, object sender, string message, object arg1, object arg2, object arg3)
		{
			logger.Log(sender, LoggerEvent.VeryVerbose, message, new object[]
			{
				arg1,
				arg2,
				arg3
			});
		}

		// Token: 0x060043AA RID: 17322 RVA: 0x00169664 File Offset: 0x00169664
		public static void VeryVerbose(this ILogger logger, object sender, string message, object arg1, object arg2, object arg3, object arg4)
		{
			logger.Log(sender, LoggerEvent.VeryVerbose, message, new object[]
			{
				arg1,
				arg2,
				arg3,
				arg4
			});
		}

		// Token: 0x060043AB RID: 17323 RVA: 0x00169698 File Offset: 0x00169698
		public static void VeryVerbose(this ILogger logger, object sender, string message, params object[] args)
		{
			logger.Log(sender, LoggerEvent.VeryVerbose, message, args);
		}

		// Token: 0x060043AC RID: 17324 RVA: 0x001696A4 File Offset: 0x001696A4
		public static TypeDef Resolve(this ITypeResolver self, TypeRef typeRef)
		{
			return self.Resolve(typeRef, null);
		}

		// Token: 0x060043AD RID: 17325 RVA: 0x001696B0 File Offset: 0x001696B0
		public static TypeDef ResolveThrow(this ITypeResolver self, TypeRef typeRef)
		{
			return self.ResolveThrow(typeRef, null);
		}

		// Token: 0x060043AE RID: 17326 RVA: 0x001696BC File Offset: 0x001696BC
		public static TypeDef ResolveThrow(this ITypeResolver self, TypeRef typeRef, ModuleDef sourceModule)
		{
			TypeDef typeDef = self.Resolve(typeRef, sourceModule);
			if (typeDef != null)
			{
				return typeDef;
			}
			throw new TypeResolveException(string.Format("Could not resolve type: {0} ({1})", typeRef, (typeRef != null) ? typeRef.DefinitionAssembly : null));
		}

		// Token: 0x060043AF RID: 17327 RVA: 0x00169700 File Offset: 0x00169700
		public static IMemberForwarded ResolveThrow(this IMemberRefResolver self, MemberRef memberRef)
		{
			IMemberForwarded memberForwarded = self.Resolve(memberRef);
			if (memberForwarded != null)
			{
				return memberForwarded;
			}
			throw new MemberRefResolveException(string.Format("Could not resolve method/field: {0} ({1})", memberRef, (memberRef != null) ? memberRef.GetDefinitionAssembly() : null));
		}

		// Token: 0x060043B0 RID: 17328 RVA: 0x00169744 File Offset: 0x00169744
		public static FieldDef ResolveField(this IMemberRefResolver self, MemberRef memberRef)
		{
			return self.Resolve(memberRef) as FieldDef;
		}

		// Token: 0x060043B1 RID: 17329 RVA: 0x00169754 File Offset: 0x00169754
		public static FieldDef ResolveFieldThrow(this IMemberRefResolver self, MemberRef memberRef)
		{
			FieldDef fieldDef = self.Resolve(memberRef) as FieldDef;
			if (fieldDef != null)
			{
				return fieldDef;
			}
			throw new MemberRefResolveException(string.Format("Could not resolve field: {0} ({1})", memberRef, (memberRef != null) ? memberRef.GetDefinitionAssembly() : null));
		}

		// Token: 0x060043B2 RID: 17330 RVA: 0x0016979C File Offset: 0x0016979C
		public static MethodDef ResolveMethod(this IMemberRefResolver self, MemberRef memberRef)
		{
			return self.Resolve(memberRef) as MethodDef;
		}

		// Token: 0x060043B3 RID: 17331 RVA: 0x001697AC File Offset: 0x001697AC
		public static MethodDef ResolveMethodThrow(this IMemberRefResolver self, MemberRef memberRef)
		{
			MethodDef methodDef = self.Resolve(memberRef) as MethodDef;
			if (methodDef != null)
			{
				return methodDef;
			}
			throw new MemberRefResolveException(string.Format("Could not resolve method: {0} ({1})", memberRef, (memberRef != null) ? memberRef.GetDefinitionAssembly() : null));
		}

		// Token: 0x060043B4 RID: 17332 RVA: 0x001697F4 File Offset: 0x001697F4
		public static IMDTokenProvider ResolveToken(this ITokenResolver self, uint token)
		{
			return self.ResolveToken(token, default(GenericParamContext));
		}

		// Token: 0x060043B5 RID: 17333 RVA: 0x00169818 File Offset: 0x00169818
		public static ITypeDefOrRef GetNonNestedTypeRefScope(this IType type)
		{
			if (type == null)
			{
				return null;
			}
			ITypeDefOrRef scopeType = type.ScopeType;
			TypeRef typeRef = scopeType as TypeRef;
			if (typeRef == null)
			{
				return scopeType;
			}
			for (int i = 0; i < 100; i++)
			{
				TypeRef typeRef2 = typeRef.ResolutionScope as TypeRef;
				if (typeRef2 == null)
				{
					return typeRef;
				}
				typeRef = typeRef2;
			}
			return typeRef;
		}

		// Token: 0x060043B6 RID: 17334 RVA: 0x00169870 File Offset: 0x00169870
		public static TypeDef FindThrow(this ITypeDefFinder self, TypeRef typeRef)
		{
			TypeDef typeDef = self.Find(typeRef);
			if (typeDef != null)
			{
				return typeDef;
			}
			throw new TypeResolveException(string.Format("Could not find type: {0}", typeRef));
		}

		// Token: 0x060043B7 RID: 17335 RVA: 0x001698A4 File Offset: 0x001698A4
		public static TypeDef FindThrow(this ITypeDefFinder self, string fullName, bool isReflectionName)
		{
			TypeDef typeDef = self.Find(fullName, isReflectionName);
			if (typeDef != null)
			{
				return typeDef;
			}
			throw new TypeResolveException("Could not find type: " + fullName);
		}

		// Token: 0x060043B8 RID: 17336 RVA: 0x001698D8 File Offset: 0x001698D8
		public static TypeDef FindNormal(this ITypeDefFinder self, string fullName)
		{
			return self.Find(fullName, false);
		}

		// Token: 0x060043B9 RID: 17337 RVA: 0x001698E4 File Offset: 0x001698E4
		public static TypeDef FindNormalThrow(this ITypeDefFinder self, string fullName)
		{
			TypeDef typeDef = self.Find(fullName, false);
			if (typeDef != null)
			{
				return typeDef;
			}
			throw new TypeResolveException("Could not find type: " + fullName);
		}

		// Token: 0x060043BA RID: 17338 RVA: 0x00169918 File Offset: 0x00169918
		public static TypeDef FindReflection(this ITypeDefFinder self, string fullName)
		{
			return self.Find(fullName, true);
		}

		// Token: 0x060043BB RID: 17339 RVA: 0x00169924 File Offset: 0x00169924
		public static TypeDef FindReflectionThrow(this ITypeDefFinder self, string fullName)
		{
			TypeDef typeDef = self.Find(fullName, true);
			if (typeDef != null)
			{
				return typeDef;
			}
			throw new TypeResolveException("Could not find type: " + fullName);
		}

		// Token: 0x060043BC RID: 17340 RVA: 0x00169958 File Offset: 0x00169958
		public static bool TypeExists(this ITypeDefFinder self, TypeRef typeRef)
		{
			return self.Find(typeRef) != null;
		}

		// Token: 0x060043BD RID: 17341 RVA: 0x00169968 File Offset: 0x00169968
		public static bool TypeExists(this ITypeDefFinder self, string fullName, bool isReflectionName)
		{
			return self.Find(fullName, isReflectionName) != null;
		}

		// Token: 0x060043BE RID: 17342 RVA: 0x00169978 File Offset: 0x00169978
		public static bool TypeExistsNormal(this ITypeDefFinder self, string fullName)
		{
			return self.Find(fullName, false) != null;
		}

		// Token: 0x060043BF RID: 17343 RVA: 0x00169988 File Offset: 0x00169988
		public static bool TypeExistsReflection(this ITypeDefFinder self, string fullName)
		{
			return self.Find(fullName, true) != null;
		}

		// Token: 0x060043C0 RID: 17344 RVA: 0x00169998 File Offset: 0x00169998
		public static TypeSig RemoveModifiers(this TypeSig a)
		{
			if (a == null)
			{
				return null;
			}
			while (a is ModifierSig)
			{
				a = a.Next;
			}
			return a;
		}

		// Token: 0x060043C1 RID: 17345 RVA: 0x001699B8 File Offset: 0x001699B8
		public static TypeSig RemovePinned(this TypeSig a)
		{
			PinnedSig pinnedSig = a as PinnedSig;
			if (pinnedSig == null)
			{
				return a;
			}
			return pinnedSig.Next;
		}

		// Token: 0x060043C2 RID: 17346 RVA: 0x001699E0 File Offset: 0x001699E0
		public static TypeSig RemovePinnedAndModifiers(this TypeSig a)
		{
			a = a.RemoveModifiers();
			a = a.RemovePinned();
			a = a.RemoveModifiers();
			return a;
		}

		// Token: 0x060043C3 RID: 17347 RVA: 0x001699FC File Offset: 0x001699FC
		public static TypeDefOrRefSig ToTypeDefOrRefSig(this TypeSig type)
		{
			return type.RemovePinnedAndModifiers() as TypeDefOrRefSig;
		}

		// Token: 0x060043C4 RID: 17348 RVA: 0x00169A0C File Offset: 0x00169A0C
		public static ClassOrValueTypeSig ToClassOrValueTypeSig(this TypeSig type)
		{
			return type.RemovePinnedAndModifiers() as ClassOrValueTypeSig;
		}

		// Token: 0x060043C5 RID: 17349 RVA: 0x00169A1C File Offset: 0x00169A1C
		public static ValueTypeSig ToValueTypeSig(this TypeSig type)
		{
			return type.RemovePinnedAndModifiers() as ValueTypeSig;
		}

		// Token: 0x060043C6 RID: 17350 RVA: 0x00169A2C File Offset: 0x00169A2C
		public static ClassSig ToClassSig(this TypeSig type)
		{
			return type.RemovePinnedAndModifiers() as ClassSig;
		}

		// Token: 0x060043C7 RID: 17351 RVA: 0x00169A3C File Offset: 0x00169A3C
		public static GenericSig ToGenericSig(this TypeSig type)
		{
			return type.RemovePinnedAndModifiers() as GenericSig;
		}

		// Token: 0x060043C8 RID: 17352 RVA: 0x00169A4C File Offset: 0x00169A4C
		public static GenericVar ToGenericVar(this TypeSig type)
		{
			return type.RemovePinnedAndModifiers() as GenericVar;
		}

		// Token: 0x060043C9 RID: 17353 RVA: 0x00169A5C File Offset: 0x00169A5C
		public static GenericMVar ToGenericMVar(this TypeSig type)
		{
			return type.RemovePinnedAndModifiers() as GenericMVar;
		}

		// Token: 0x060043CA RID: 17354 RVA: 0x00169A6C File Offset: 0x00169A6C
		public static GenericInstSig ToGenericInstSig(this TypeSig type)
		{
			return type.RemovePinnedAndModifiers() as GenericInstSig;
		}

		// Token: 0x060043CB RID: 17355 RVA: 0x00169A7C File Offset: 0x00169A7C
		public static PtrSig ToPtrSig(this TypeSig type)
		{
			return type.RemovePinnedAndModifiers() as PtrSig;
		}

		// Token: 0x060043CC RID: 17356 RVA: 0x00169A8C File Offset: 0x00169A8C
		public static ByRefSig ToByRefSig(this TypeSig type)
		{
			return type.RemovePinnedAndModifiers() as ByRefSig;
		}

		// Token: 0x060043CD RID: 17357 RVA: 0x00169A9C File Offset: 0x00169A9C
		public static ArraySig ToArraySig(this TypeSig type)
		{
			return type.RemovePinnedAndModifiers() as ArraySig;
		}

		// Token: 0x060043CE RID: 17358 RVA: 0x00169AAC File Offset: 0x00169AAC
		public static SZArraySig ToSZArraySig(this TypeSig type)
		{
			return type.RemovePinnedAndModifiers() as SZArraySig;
		}

		// Token: 0x060043CF RID: 17359 RVA: 0x00169ABC File Offset: 0x00169ABC
		public static TypeSig GetNext(this TypeSig self)
		{
			if (self == null)
			{
				return null;
			}
			return self.Next;
		}

		// Token: 0x060043D0 RID: 17360 RVA: 0x00169ACC File Offset: 0x00169ACC
		public static bool GetIsValueType(this TypeSig self)
		{
			return self != null && self.IsValueType;
		}

		// Token: 0x060043D1 RID: 17361 RVA: 0x00169ADC File Offset: 0x00169ADC
		public static bool GetIsPrimitive(this TypeSig self)
		{
			return self != null && self.IsPrimitive;
		}

		// Token: 0x060043D2 RID: 17362 RVA: 0x00169AEC File Offset: 0x00169AEC
		public static ElementType GetElementType(this TypeSig a)
		{
			if (a != null)
			{
				return a.ElementType;
			}
			return ElementType.End;
		}

		// Token: 0x060043D3 RID: 17363 RVA: 0x00169AFC File Offset: 0x00169AFC
		public static string GetFullName(this TypeSig a)
		{
			if (a != null)
			{
				return a.FullName;
			}
			return string.Empty;
		}

		// Token: 0x060043D4 RID: 17364 RVA: 0x00169B10 File Offset: 0x00169B10
		public static string GetName(this TypeSig a)
		{
			if (a != null)
			{
				return a.TypeName;
			}
			return string.Empty;
		}

		// Token: 0x060043D5 RID: 17365 RVA: 0x00169B24 File Offset: 0x00169B24
		public static string GetNamespace(this TypeSig a)
		{
			if (a != null)
			{
				return a.Namespace;
			}
			return string.Empty;
		}

		// Token: 0x060043D6 RID: 17366 RVA: 0x00169B38 File Offset: 0x00169B38
		public static ITypeDefOrRef TryGetTypeDefOrRef(this TypeSig a)
		{
			TypeDefOrRefSig typeDefOrRefSig = a.RemovePinnedAndModifiers() as TypeDefOrRefSig;
			if (typeDefOrRefSig == null)
			{
				return null;
			}
			return typeDefOrRefSig.TypeDefOrRef;
		}

		// Token: 0x060043D7 RID: 17367 RVA: 0x00169B54 File Offset: 0x00169B54
		public static TypeRef TryGetTypeRef(this TypeSig a)
		{
			TypeDefOrRefSig typeDefOrRefSig = a.RemovePinnedAndModifiers() as TypeDefOrRefSig;
			if (typeDefOrRefSig == null)
			{
				return null;
			}
			return typeDefOrRefSig.TypeRef;
		}

		// Token: 0x060043D8 RID: 17368 RVA: 0x00169B70 File Offset: 0x00169B70
		public static TypeDef TryGetTypeDef(this TypeSig a)
		{
			TypeDefOrRefSig typeDefOrRefSig = a.RemovePinnedAndModifiers() as TypeDefOrRefSig;
			if (typeDefOrRefSig == null)
			{
				return null;
			}
			return typeDefOrRefSig.TypeDef;
		}

		// Token: 0x060043D9 RID: 17369 RVA: 0x00169B8C File Offset: 0x00169B8C
		public static TypeSpec TryGetTypeSpec(this TypeSig a)
		{
			TypeDefOrRefSig typeDefOrRefSig = a.RemovePinnedAndModifiers() as TypeDefOrRefSig;
			if (typeDefOrRefSig == null)
			{
				return null;
			}
			return typeDefOrRefSig.TypeSpec;
		}
	}
}
