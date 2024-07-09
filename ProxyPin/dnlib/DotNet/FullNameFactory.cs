using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace dnlib.DotNet
{
	// Token: 0x020007B4 RID: 1972
	[ComVisible(true)]
	public struct FullNameFactory
	{
		// Token: 0x0600474C RID: 18252 RVA: 0x0017371C File Offset: 0x0017371C
		public static bool MustUseAssemblyName(ModuleDef module, IType type)
		{
			return FullNameFactory.MustUseAssemblyName(module, type, true);
		}

		// Token: 0x0600474D RID: 18253 RVA: 0x00173728 File Offset: 0x00173728
		public static bool MustUseAssemblyName(ModuleDef module, IType type, bool allowCorlib)
		{
			TypeDef typeDef = type as TypeDef;
			if (typeDef != null)
			{
				return typeDef.Module != module;
			}
			TypeRef typeRef = type as TypeRef;
			return typeRef == null || (typeRef.ResolutionScope != AssemblyRef.CurrentAssembly && (!allowCorlib || !typeRef.DefinitionAssembly.IsCorLib() || module.Find(typeRef) != null));
		}

		// Token: 0x0600474E RID: 18254 RVA: 0x0017379C File Offset: 0x0017379C
		public static string FullName(IType type, bool isReflection, IFullNameFactoryHelper helper, StringBuilder sb)
		{
			return FullNameFactory.FullNameSB(type, isReflection, helper, sb).ToString();
		}

		// Token: 0x0600474F RID: 18255 RVA: 0x001737AC File Offset: 0x001737AC
		public static StringBuilder FullNameSB(IType type, bool isReflection, IFullNameFactoryHelper helper, StringBuilder sb)
		{
			TypeDef typeDef = type as TypeDef;
			if (typeDef != null)
			{
				return FullNameFactory.FullNameSB(typeDef, isReflection, helper, sb);
			}
			TypeRef typeRef = type as TypeRef;
			if (typeRef != null)
			{
				return FullNameFactory.FullNameSB(typeRef, isReflection, helper, sb);
			}
			TypeSpec typeSpec = type as TypeSpec;
			if (typeSpec != null)
			{
				return FullNameFactory.FullNameSB(typeSpec, isReflection, helper, sb);
			}
			TypeSig typeSig = type as TypeSig;
			if (typeSig != null)
			{
				return FullNameFactory.FullNameSB(typeSig, isReflection, helper, null, null, sb);
			}
			ExportedType exportedType = type as ExportedType;
			if (exportedType != null)
			{
				return FullNameFactory.FullNameSB(exportedType, isReflection, helper, sb);
			}
			return sb ?? new StringBuilder();
		}

		// Token: 0x06004750 RID: 18256 RVA: 0x00173844 File Offset: 0x00173844
		public static string Name(IType type, bool isReflection, StringBuilder sb)
		{
			return FullNameFactory.NameSB(type, isReflection, sb).ToString();
		}

		// Token: 0x06004751 RID: 18257 RVA: 0x00173854 File Offset: 0x00173854
		public static StringBuilder NameSB(IType type, bool isReflection, StringBuilder sb)
		{
			TypeDef typeDef = type as TypeDef;
			if (typeDef != null)
			{
				return FullNameFactory.NameSB(typeDef, isReflection, sb);
			}
			TypeRef typeRef = type as TypeRef;
			if (typeRef != null)
			{
				return FullNameFactory.NameSB(typeRef, isReflection, sb);
			}
			TypeSpec typeSpec = type as TypeSpec;
			if (typeSpec != null)
			{
				return FullNameFactory.NameSB(typeSpec, isReflection, sb);
			}
			TypeSig typeSig = type as TypeSig;
			if (typeSig != null)
			{
				return FullNameFactory.NameSB(typeSig, false, sb);
			}
			ExportedType exportedType = type as ExportedType;
			if (exportedType != null)
			{
				return FullNameFactory.NameSB(exportedType, isReflection, sb);
			}
			return sb ?? new StringBuilder();
		}

		// Token: 0x06004752 RID: 18258 RVA: 0x001738E4 File Offset: 0x001738E4
		public static string Namespace(IType type, bool isReflection, StringBuilder sb)
		{
			return FullNameFactory.NamespaceSB(type, isReflection, sb).ToString();
		}

		// Token: 0x06004753 RID: 18259 RVA: 0x001738F4 File Offset: 0x001738F4
		public static StringBuilder NamespaceSB(IType type, bool isReflection, StringBuilder sb)
		{
			TypeDef typeDef = type as TypeDef;
			if (typeDef != null)
			{
				return FullNameFactory.NamespaceSB(typeDef, isReflection, sb);
			}
			TypeRef typeRef = type as TypeRef;
			if (typeRef != null)
			{
				return FullNameFactory.NamespaceSB(typeRef, isReflection, sb);
			}
			TypeSpec typeSpec = type as TypeSpec;
			if (typeSpec != null)
			{
				return FullNameFactory.NamespaceSB(typeSpec, isReflection, sb);
			}
			TypeSig typeSig = type as TypeSig;
			if (typeSig != null)
			{
				return FullNameFactory.NamespaceSB(typeSig, false, sb);
			}
			ExportedType exportedType = type as ExportedType;
			if (exportedType != null)
			{
				return FullNameFactory.NamespaceSB(exportedType, isReflection, sb);
			}
			return sb ?? new StringBuilder();
		}

		// Token: 0x06004754 RID: 18260 RVA: 0x00173984 File Offset: 0x00173984
		public static string AssemblyQualifiedName(IType type, IFullNameFactoryHelper helper = null, StringBuilder sb = null)
		{
			return FullNameFactory.AssemblyQualifiedNameSB(type, helper, sb).ToString();
		}

		// Token: 0x06004755 RID: 18261 RVA: 0x00173994 File Offset: 0x00173994
		public static StringBuilder AssemblyQualifiedNameSB(IType type, IFullNameFactoryHelper helper, StringBuilder sb)
		{
			TypeDef typeDef = type as TypeDef;
			if (typeDef != null)
			{
				return FullNameFactory.AssemblyQualifiedNameSB(typeDef, helper, sb);
			}
			TypeRef typeRef = type as TypeRef;
			if (typeRef != null)
			{
				return FullNameFactory.AssemblyQualifiedNameSB(typeRef, helper, sb);
			}
			TypeSpec typeSpec = type as TypeSpec;
			if (typeSpec != null)
			{
				return FullNameFactory.AssemblyQualifiedNameSB(typeSpec, helper, sb);
			}
			TypeSig typeSig = type as TypeSig;
			if (typeSig != null)
			{
				return FullNameFactory.AssemblyQualifiedNameSB(typeSig, helper, sb);
			}
			ExportedType exportedType = type as ExportedType;
			if (exportedType != null)
			{
				return FullNameFactory.AssemblyQualifiedNameSB(exportedType, helper, sb);
			}
			return sb ?? new StringBuilder();
		}

		// Token: 0x06004756 RID: 18262 RVA: 0x00173A24 File Offset: 0x00173A24
		public static string PropertyFullName(string declaringType, UTF8String name, CallingConventionSig propertySig, IList<TypeSig> typeGenArgs = null, StringBuilder sb = null)
		{
			return FullNameFactory.PropertyFullNameSB(declaringType, name, propertySig, typeGenArgs, sb).ToString();
		}

		// Token: 0x06004757 RID: 18263 RVA: 0x00173A38 File Offset: 0x00173A38
		public static StringBuilder PropertyFullNameSB(string declaringType, UTF8String name, CallingConventionSig propertySig, IList<TypeSig> typeGenArgs, StringBuilder sb)
		{
			FullNameFactory fullNameFactory = new FullNameFactory(false, null, sb);
			if (typeGenArgs != null)
			{
				fullNameFactory.genericArguments = new GenericArguments();
				fullNameFactory.genericArguments.PushTypeArgs(typeGenArgs);
			}
			fullNameFactory.CreatePropertyFullName(declaringType, name, propertySig);
			return fullNameFactory.sb ?? new StringBuilder();
		}

		// Token: 0x06004758 RID: 18264 RVA: 0x00173A90 File Offset: 0x00173A90
		public static string EventFullName(string declaringType, UTF8String name, ITypeDefOrRef typeDefOrRef, IList<TypeSig> typeGenArgs = null, StringBuilder sb = null)
		{
			return FullNameFactory.EventFullNameSB(declaringType, name, typeDefOrRef, typeGenArgs, sb).ToString();
		}

		// Token: 0x06004759 RID: 18265 RVA: 0x00173AA4 File Offset: 0x00173AA4
		public static StringBuilder EventFullNameSB(string declaringType, UTF8String name, ITypeDefOrRef typeDefOrRef, IList<TypeSig> typeGenArgs, StringBuilder sb)
		{
			FullNameFactory fullNameFactory = new FullNameFactory(false, null, sb);
			if (typeGenArgs != null)
			{
				fullNameFactory.genericArguments = new GenericArguments();
				fullNameFactory.genericArguments.PushTypeArgs(typeGenArgs);
			}
			fullNameFactory.CreateEventFullName(declaringType, name, typeDefOrRef);
			return fullNameFactory.sb ?? new StringBuilder();
		}

		// Token: 0x0600475A RID: 18266 RVA: 0x00173AFC File Offset: 0x00173AFC
		public static string FieldFullName(string declaringType, string name, FieldSig fieldSig, IList<TypeSig> typeGenArgs = null, StringBuilder sb = null)
		{
			return FullNameFactory.FieldFullNameSB(declaringType, name, fieldSig, typeGenArgs, sb).ToString();
		}

		// Token: 0x0600475B RID: 18267 RVA: 0x00173B10 File Offset: 0x00173B10
		public static StringBuilder FieldFullNameSB(string declaringType, string name, FieldSig fieldSig, IList<TypeSig> typeGenArgs, StringBuilder sb)
		{
			FullNameFactory fullNameFactory = new FullNameFactory(false, null, sb);
			if (typeGenArgs != null)
			{
				fullNameFactory.genericArguments = new GenericArguments();
				fullNameFactory.genericArguments.PushTypeArgs(typeGenArgs);
			}
			fullNameFactory.CreateFieldFullName(declaringType, name, fieldSig);
			return fullNameFactory.sb ?? new StringBuilder();
		}

		// Token: 0x0600475C RID: 18268 RVA: 0x00173B68 File Offset: 0x00173B68
		public static string MethodFullName(string declaringType, string name, MethodSig methodSig, IList<TypeSig> typeGenArgs = null, IList<TypeSig> methodGenArgs = null, MethodDef gppMethod = null, StringBuilder sb = null)
		{
			return FullNameFactory.MethodFullNameSB(declaringType, name, methodSig, typeGenArgs, methodGenArgs, gppMethod, sb).ToString();
		}

		// Token: 0x0600475D RID: 18269 RVA: 0x00173B80 File Offset: 0x00173B80
		public static StringBuilder MethodFullNameSB(string declaringType, string name, MethodSig methodSig, IList<TypeSig> typeGenArgs, IList<TypeSig> methodGenArgs, MethodDef gppMethod, StringBuilder sb)
		{
			FullNameFactory fullNameFactory = new FullNameFactory(false, null, sb);
			if (typeGenArgs != null || methodGenArgs != null)
			{
				fullNameFactory.genericArguments = new GenericArguments();
			}
			if (typeGenArgs != null)
			{
				fullNameFactory.genericArguments.PushTypeArgs(typeGenArgs);
			}
			if (methodGenArgs != null)
			{
				fullNameFactory.genericArguments.PushMethodArgs(methodGenArgs);
			}
			fullNameFactory.CreateMethodFullName(declaringType, name, methodSig, gppMethod);
			return fullNameFactory.sb ?? new StringBuilder();
		}

		// Token: 0x0600475E RID: 18270 RVA: 0x00173BFC File Offset: 0x00173BFC
		public static string MethodBaseSigFullName(MethodBaseSig sig, StringBuilder sb = null)
		{
			return FullNameFactory.MethodBaseSigFullNameSB(sig, sb).ToString();
		}

		// Token: 0x0600475F RID: 18271 RVA: 0x00173C0C File Offset: 0x00173C0C
		public static StringBuilder MethodBaseSigFullNameSB(MethodBaseSig sig, StringBuilder sb)
		{
			FullNameFactory fullNameFactory = new FullNameFactory(false, null, sb);
			fullNameFactory.CreateMethodFullName(null, null, sig, null);
			return fullNameFactory.sb ?? new StringBuilder();
		}

		// Token: 0x06004760 RID: 18272 RVA: 0x00173C44 File Offset: 0x00173C44
		public static string MethodBaseSigFullName(string declType, string name, MethodBaseSig sig, MethodDef gppMethod, StringBuilder sb = null)
		{
			return FullNameFactory.MethodBaseSigFullNameSB(declType, name, sig, gppMethod, sb).ToString();
		}

		// Token: 0x06004761 RID: 18273 RVA: 0x00173C58 File Offset: 0x00173C58
		public static StringBuilder MethodBaseSigFullNameSB(string declType, string name, MethodBaseSig sig, MethodDef gppMethod, StringBuilder sb)
		{
			FullNameFactory fullNameFactory = new FullNameFactory(false, null, sb);
			fullNameFactory.CreateMethodFullName(declType, name, sig, gppMethod);
			return fullNameFactory.sb ?? new StringBuilder();
		}

		// Token: 0x06004762 RID: 18274 RVA: 0x00173C94 File Offset: 0x00173C94
		public static string Namespace(TypeRef typeRef, bool isReflection, StringBuilder sb = null)
		{
			return FullNameFactory.NamespaceSB(typeRef, isReflection, sb).ToString();
		}

		// Token: 0x06004763 RID: 18275 RVA: 0x00173CA4 File Offset: 0x00173CA4
		public static StringBuilder NamespaceSB(TypeRef typeRef, bool isReflection, StringBuilder sb)
		{
			FullNameFactory fullNameFactory = new FullNameFactory(isReflection, null, sb);
			fullNameFactory.CreateNamespace(typeRef);
			return fullNameFactory.sb ?? new StringBuilder();
		}

		// Token: 0x06004764 RID: 18276 RVA: 0x00173CDC File Offset: 0x00173CDC
		public static string Name(TypeRef typeRef, bool isReflection, StringBuilder sb = null)
		{
			return FullNameFactory.NameSB(typeRef, isReflection, sb).ToString();
		}

		// Token: 0x06004765 RID: 18277 RVA: 0x00173CEC File Offset: 0x00173CEC
		public static StringBuilder NameSB(TypeRef typeRef, bool isReflection, StringBuilder sb)
		{
			FullNameFactory fullNameFactory = new FullNameFactory(isReflection, null, sb);
			fullNameFactory.CreateName(typeRef);
			return fullNameFactory.sb ?? new StringBuilder();
		}

		// Token: 0x06004766 RID: 18278 RVA: 0x00173D24 File Offset: 0x00173D24
		public static string FullName(TypeRef typeRef, bool isReflection, IFullNameFactoryHelper helper = null, StringBuilder sb = null)
		{
			return FullNameFactory.FullNameSB(typeRef, isReflection, helper, sb).ToString();
		}

		// Token: 0x06004767 RID: 18279 RVA: 0x00173D34 File Offset: 0x00173D34
		public static StringBuilder FullNameSB(TypeRef typeRef, bool isReflection, IFullNameFactoryHelper helper, StringBuilder sb)
		{
			FullNameFactory fullNameFactory = new FullNameFactory(isReflection, helper, sb);
			fullNameFactory.CreateFullName(typeRef);
			return fullNameFactory.sb ?? new StringBuilder();
		}

		// Token: 0x06004768 RID: 18280 RVA: 0x00173D6C File Offset: 0x00173D6C
		public static string AssemblyQualifiedName(TypeRef typeRef, IFullNameFactoryHelper helper = null, StringBuilder sb = null)
		{
			return FullNameFactory.AssemblyQualifiedNameSB(typeRef, helper, sb).ToString();
		}

		// Token: 0x06004769 RID: 18281 RVA: 0x00173D7C File Offset: 0x00173D7C
		public static StringBuilder AssemblyQualifiedNameSB(TypeRef typeRef, IFullNameFactoryHelper helper, StringBuilder sb)
		{
			FullNameFactory fullNameFactory = new FullNameFactory(true, helper, sb);
			fullNameFactory.CreateAssemblyQualifiedName(typeRef);
			return fullNameFactory.sb ?? new StringBuilder();
		}

		// Token: 0x0600476A RID: 18282 RVA: 0x00173DB4 File Offset: 0x00173DB4
		public static IAssembly DefinitionAssembly(TypeRef typeRef)
		{
			return default(FullNameFactory).GetDefinitionAssembly(typeRef);
		}

		// Token: 0x0600476B RID: 18283 RVA: 0x00173DD4 File Offset: 0x00173DD4
		public static IScope Scope(TypeRef typeRef)
		{
			return default(FullNameFactory).GetScope(typeRef);
		}

		// Token: 0x0600476C RID: 18284 RVA: 0x00173DF4 File Offset: 0x00173DF4
		public static ModuleDef OwnerModule(TypeRef typeRef)
		{
			return default(FullNameFactory).GetOwnerModule(typeRef);
		}

		// Token: 0x0600476D RID: 18285 RVA: 0x00173E14 File Offset: 0x00173E14
		public static string Namespace(TypeDef typeDef, bool isReflection, StringBuilder sb = null)
		{
			return FullNameFactory.NamespaceSB(typeDef, isReflection, sb).ToString();
		}

		// Token: 0x0600476E RID: 18286 RVA: 0x00173E24 File Offset: 0x00173E24
		public static StringBuilder NamespaceSB(TypeDef typeDef, bool isReflection, StringBuilder sb)
		{
			FullNameFactory fullNameFactory = new FullNameFactory(isReflection, null, sb);
			fullNameFactory.CreateNamespace(typeDef);
			return fullNameFactory.sb ?? new StringBuilder();
		}

		// Token: 0x0600476F RID: 18287 RVA: 0x00173E5C File Offset: 0x00173E5C
		public static string Name(TypeDef typeDef, bool isReflection, StringBuilder sb = null)
		{
			return FullNameFactory.NameSB(typeDef, isReflection, sb).ToString();
		}

		// Token: 0x06004770 RID: 18288 RVA: 0x00173E6C File Offset: 0x00173E6C
		public static StringBuilder NameSB(TypeDef typeDef, bool isReflection, StringBuilder sb)
		{
			FullNameFactory fullNameFactory = new FullNameFactory(isReflection, null, sb);
			fullNameFactory.CreateName(typeDef);
			return fullNameFactory.sb ?? new StringBuilder();
		}

		// Token: 0x06004771 RID: 18289 RVA: 0x00173EA4 File Offset: 0x00173EA4
		public static string FullName(TypeDef typeDef, bool isReflection, IFullNameFactoryHelper helper = null, StringBuilder sb = null)
		{
			return FullNameFactory.FullNameSB(typeDef, isReflection, helper, sb).ToString();
		}

		// Token: 0x06004772 RID: 18290 RVA: 0x00173EB4 File Offset: 0x00173EB4
		public static StringBuilder FullNameSB(TypeDef typeDef, bool isReflection, IFullNameFactoryHelper helper, StringBuilder sb)
		{
			FullNameFactory fullNameFactory = new FullNameFactory(isReflection, helper, sb);
			fullNameFactory.CreateFullName(typeDef);
			return fullNameFactory.sb ?? new StringBuilder();
		}

		// Token: 0x06004773 RID: 18291 RVA: 0x00173EEC File Offset: 0x00173EEC
		public static string AssemblyQualifiedName(TypeDef typeDef, IFullNameFactoryHelper helper = null, StringBuilder sb = null)
		{
			return FullNameFactory.AssemblyQualifiedNameSB(typeDef, helper, sb).ToString();
		}

		// Token: 0x06004774 RID: 18292 RVA: 0x00173EFC File Offset: 0x00173EFC
		public static StringBuilder AssemblyQualifiedNameSB(TypeDef typeDef, IFullNameFactoryHelper helper, StringBuilder sb)
		{
			FullNameFactory fullNameFactory = new FullNameFactory(true, helper, sb);
			fullNameFactory.CreateAssemblyQualifiedName(typeDef);
			return fullNameFactory.sb ?? new StringBuilder();
		}

		// Token: 0x06004775 RID: 18293 RVA: 0x00173F34 File Offset: 0x00173F34
		public static IAssembly DefinitionAssembly(TypeDef typeDef)
		{
			return default(FullNameFactory).GetDefinitionAssembly(typeDef);
		}

		// Token: 0x06004776 RID: 18294 RVA: 0x00173F54 File Offset: 0x00173F54
		public static ModuleDef OwnerModule(TypeDef typeDef)
		{
			return default(FullNameFactory).GetOwnerModule(typeDef);
		}

		// Token: 0x06004777 RID: 18295 RVA: 0x00173F74 File Offset: 0x00173F74
		public static string Namespace(TypeSpec typeSpec, bool isReflection, StringBuilder sb = null)
		{
			return FullNameFactory.NamespaceSB(typeSpec, isReflection, sb).ToString();
		}

		// Token: 0x06004778 RID: 18296 RVA: 0x00173F84 File Offset: 0x00173F84
		public static StringBuilder NamespaceSB(TypeSpec typeSpec, bool isReflection, StringBuilder sb)
		{
			FullNameFactory fullNameFactory = new FullNameFactory(isReflection, null, sb);
			fullNameFactory.CreateNamespace(typeSpec);
			return fullNameFactory.sb ?? new StringBuilder();
		}

		// Token: 0x06004779 RID: 18297 RVA: 0x00173FBC File Offset: 0x00173FBC
		public static string Name(TypeSpec typeSpec, bool isReflection, StringBuilder sb = null)
		{
			return FullNameFactory.NameSB(typeSpec, isReflection, sb).ToString();
		}

		// Token: 0x0600477A RID: 18298 RVA: 0x00173FCC File Offset: 0x00173FCC
		public static StringBuilder NameSB(TypeSpec typeSpec, bool isReflection, StringBuilder sb)
		{
			FullNameFactory fullNameFactory = new FullNameFactory(isReflection, null, sb);
			fullNameFactory.CreateName(typeSpec);
			return fullNameFactory.sb ?? new StringBuilder();
		}

		// Token: 0x0600477B RID: 18299 RVA: 0x00174004 File Offset: 0x00174004
		public static string FullName(TypeSpec typeSpec, bool isReflection, IFullNameFactoryHelper helper = null, StringBuilder sb = null)
		{
			return FullNameFactory.FullNameSB(typeSpec, isReflection, helper, sb).ToString();
		}

		// Token: 0x0600477C RID: 18300 RVA: 0x00174014 File Offset: 0x00174014
		public static StringBuilder FullNameSB(TypeSpec typeSpec, bool isReflection, IFullNameFactoryHelper helper, StringBuilder sb)
		{
			FullNameFactory fullNameFactory = new FullNameFactory(isReflection, helper, sb);
			fullNameFactory.CreateFullName(typeSpec);
			return fullNameFactory.sb ?? new StringBuilder();
		}

		// Token: 0x0600477D RID: 18301 RVA: 0x0017404C File Offset: 0x0017404C
		public static string AssemblyQualifiedName(TypeSpec typeSpec, IFullNameFactoryHelper helper = null, StringBuilder sb = null)
		{
			return FullNameFactory.AssemblyQualifiedNameSB(typeSpec, helper, sb).ToString();
		}

		// Token: 0x0600477E RID: 18302 RVA: 0x0017405C File Offset: 0x0017405C
		public static StringBuilder AssemblyQualifiedNameSB(TypeSpec typeSpec, IFullNameFactoryHelper helper, StringBuilder sb)
		{
			FullNameFactory fullNameFactory = new FullNameFactory(true, helper, sb);
			fullNameFactory.CreateAssemblyQualifiedName(typeSpec);
			return fullNameFactory.sb ?? new StringBuilder();
		}

		// Token: 0x0600477F RID: 18303 RVA: 0x00174094 File Offset: 0x00174094
		public static IAssembly DefinitionAssembly(TypeSpec typeSpec)
		{
			return default(FullNameFactory).GetDefinitionAssembly(typeSpec);
		}

		// Token: 0x06004780 RID: 18304 RVA: 0x001740B4 File Offset: 0x001740B4
		public static ITypeDefOrRef ScopeType(TypeSpec typeSpec)
		{
			return default(FullNameFactory).GetScopeType(typeSpec);
		}

		// Token: 0x06004781 RID: 18305 RVA: 0x001740D4 File Offset: 0x001740D4
		public static IScope Scope(TypeSpec typeSpec)
		{
			return default(FullNameFactory).GetScope(typeSpec);
		}

		// Token: 0x06004782 RID: 18306 RVA: 0x001740F4 File Offset: 0x001740F4
		public static ModuleDef OwnerModule(TypeSpec typeSpec)
		{
			return default(FullNameFactory).GetOwnerModule(typeSpec);
		}

		// Token: 0x06004783 RID: 18307 RVA: 0x00174114 File Offset: 0x00174114
		public static string Namespace(TypeSig typeSig, bool isReflection, StringBuilder sb = null)
		{
			return FullNameFactory.NamespaceSB(typeSig, isReflection, sb).ToString();
		}

		// Token: 0x06004784 RID: 18308 RVA: 0x00174124 File Offset: 0x00174124
		public static StringBuilder NamespaceSB(TypeSig typeSig, bool isReflection, StringBuilder sb)
		{
			FullNameFactory fullNameFactory = new FullNameFactory(isReflection, null, sb);
			fullNameFactory.CreateNamespace(typeSig);
			return fullNameFactory.sb ?? new StringBuilder();
		}

		// Token: 0x06004785 RID: 18309 RVA: 0x0017415C File Offset: 0x0017415C
		public static string Name(TypeSig typeSig, bool isReflection, StringBuilder sb = null)
		{
			return FullNameFactory.NameSB(typeSig, isReflection, sb).ToString();
		}

		// Token: 0x06004786 RID: 18310 RVA: 0x0017416C File Offset: 0x0017416C
		public static StringBuilder NameSB(TypeSig typeSig, bool isReflection, StringBuilder sb)
		{
			FullNameFactory fullNameFactory = new FullNameFactory(isReflection, null, sb);
			fullNameFactory.CreateName(typeSig);
			return fullNameFactory.sb ?? new StringBuilder();
		}

		// Token: 0x06004787 RID: 18311 RVA: 0x001741A4 File Offset: 0x001741A4
		public static string FullName(TypeSig typeSig, bool isReflection, IFullNameFactoryHelper helper = null, IList<TypeSig> typeGenArgs = null, IList<TypeSig> methodGenArgs = null, StringBuilder sb = null)
		{
			return FullNameFactory.FullNameSB(typeSig, isReflection, helper, typeGenArgs, methodGenArgs, sb).ToString();
		}

		// Token: 0x06004788 RID: 18312 RVA: 0x001741B8 File Offset: 0x001741B8
		public static StringBuilder FullNameSB(TypeSig typeSig, bool isReflection, IFullNameFactoryHelper helper, IList<TypeSig> typeGenArgs, IList<TypeSig> methodGenArgs, StringBuilder sb)
		{
			FullNameFactory fullNameFactory = new FullNameFactory(isReflection, helper, sb);
			if (typeGenArgs != null || methodGenArgs != null)
			{
				fullNameFactory.genericArguments = new GenericArguments();
			}
			if (typeGenArgs != null)
			{
				fullNameFactory.genericArguments.PushTypeArgs(typeGenArgs);
			}
			if (methodGenArgs != null)
			{
				fullNameFactory.genericArguments.PushMethodArgs(methodGenArgs);
			}
			fullNameFactory.CreateFullName(typeSig);
			return fullNameFactory.sb ?? new StringBuilder();
		}

		// Token: 0x06004789 RID: 18313 RVA: 0x00174230 File Offset: 0x00174230
		public static string AssemblyQualifiedName(TypeSig typeSig, IFullNameFactoryHelper helper = null, StringBuilder sb = null)
		{
			return FullNameFactory.AssemblyQualifiedNameSB(typeSig, helper, sb).ToString();
		}

		// Token: 0x0600478A RID: 18314 RVA: 0x00174240 File Offset: 0x00174240
		public static StringBuilder AssemblyQualifiedNameSB(TypeSig typeSig, IFullNameFactoryHelper helper, StringBuilder sb)
		{
			FullNameFactory fullNameFactory = new FullNameFactory(true, helper, sb);
			fullNameFactory.CreateAssemblyQualifiedName(typeSig);
			return fullNameFactory.sb ?? new StringBuilder();
		}

		// Token: 0x0600478B RID: 18315 RVA: 0x00174278 File Offset: 0x00174278
		public static IAssembly DefinitionAssembly(TypeSig typeSig)
		{
			return default(FullNameFactory).GetDefinitionAssembly(typeSig);
		}

		// Token: 0x0600478C RID: 18316 RVA: 0x00174298 File Offset: 0x00174298
		public static IScope Scope(TypeSig typeSig)
		{
			return default(FullNameFactory).GetScope(typeSig);
		}

		// Token: 0x0600478D RID: 18317 RVA: 0x001742B8 File Offset: 0x001742B8
		public static ITypeDefOrRef ScopeType(TypeSig typeSig)
		{
			return default(FullNameFactory).GetScopeType(typeSig);
		}

		// Token: 0x0600478E RID: 18318 RVA: 0x001742D8 File Offset: 0x001742D8
		public static ModuleDef OwnerModule(TypeSig typeSig)
		{
			return default(FullNameFactory).GetOwnerModule(typeSig);
		}

		// Token: 0x0600478F RID: 18319 RVA: 0x001742F8 File Offset: 0x001742F8
		public static string Namespace(ExportedType exportedType, bool isReflection, StringBuilder sb = null)
		{
			return FullNameFactory.NamespaceSB(exportedType, isReflection, sb).ToString();
		}

		// Token: 0x06004790 RID: 18320 RVA: 0x00174308 File Offset: 0x00174308
		public static StringBuilder NamespaceSB(ExportedType exportedType, bool isReflection, StringBuilder sb)
		{
			FullNameFactory fullNameFactory = new FullNameFactory(isReflection, null, sb);
			fullNameFactory.CreateNamespace(exportedType);
			return fullNameFactory.sb ?? new StringBuilder();
		}

		// Token: 0x06004791 RID: 18321 RVA: 0x00174340 File Offset: 0x00174340
		public static string Name(ExportedType exportedType, bool isReflection, StringBuilder sb = null)
		{
			return FullNameFactory.NameSB(exportedType, isReflection, sb).ToString();
		}

		// Token: 0x06004792 RID: 18322 RVA: 0x00174350 File Offset: 0x00174350
		public static StringBuilder NameSB(ExportedType exportedType, bool isReflection, StringBuilder sb)
		{
			FullNameFactory fullNameFactory = new FullNameFactory(isReflection, null, sb);
			fullNameFactory.CreateName(exportedType);
			return fullNameFactory.sb ?? new StringBuilder();
		}

		// Token: 0x06004793 RID: 18323 RVA: 0x00174388 File Offset: 0x00174388
		public static string FullName(ExportedType exportedType, bool isReflection, IFullNameFactoryHelper helper = null, StringBuilder sb = null)
		{
			return FullNameFactory.FullNameSB(exportedType, isReflection, helper, sb).ToString();
		}

		// Token: 0x06004794 RID: 18324 RVA: 0x00174398 File Offset: 0x00174398
		public static StringBuilder FullNameSB(ExportedType exportedType, bool isReflection, IFullNameFactoryHelper helper, StringBuilder sb)
		{
			FullNameFactory fullNameFactory = new FullNameFactory(isReflection, helper, sb);
			fullNameFactory.CreateFullName(exportedType);
			return fullNameFactory.sb ?? new StringBuilder();
		}

		// Token: 0x06004795 RID: 18325 RVA: 0x001743D0 File Offset: 0x001743D0
		public static string AssemblyQualifiedName(ExportedType exportedType, IFullNameFactoryHelper helper = null, StringBuilder sb = null)
		{
			return FullNameFactory.AssemblyQualifiedNameSB(exportedType, helper, sb).ToString();
		}

		// Token: 0x06004796 RID: 18326 RVA: 0x001743E0 File Offset: 0x001743E0
		public static StringBuilder AssemblyQualifiedNameSB(ExportedType exportedType, IFullNameFactoryHelper helper, StringBuilder sb)
		{
			FullNameFactory fullNameFactory = new FullNameFactory(true, helper, sb);
			fullNameFactory.CreateAssemblyQualifiedName(exportedType);
			return fullNameFactory.sb ?? new StringBuilder();
		}

		// Token: 0x06004797 RID: 18327 RVA: 0x00174418 File Offset: 0x00174418
		public static IAssembly DefinitionAssembly(ExportedType exportedType)
		{
			return default(FullNameFactory).GetDefinitionAssembly(exportedType);
		}

		// Token: 0x06004798 RID: 18328 RVA: 0x00174438 File Offset: 0x00174438
		public static ITypeDefOrRef ScopeType(ExportedType exportedType)
		{
			return default(FullNameFactory).GetScopeType(exportedType);
		}

		// Token: 0x06004799 RID: 18329 RVA: 0x00174458 File Offset: 0x00174458
		public static IScope Scope(ExportedType exportedType)
		{
			return default(FullNameFactory).GetScope(exportedType);
		}

		// Token: 0x0600479A RID: 18330 RVA: 0x00174478 File Offset: 0x00174478
		public static ModuleDef OwnerModule(ExportedType exportedType)
		{
			return default(FullNameFactory).GetOwnerModule(exportedType);
		}

		// Token: 0x17000D3B RID: 3387
		// (get) Token: 0x0600479B RID: 18331 RVA: 0x00174498 File Offset: 0x00174498
		private string Result
		{
			get
			{
				StringBuilder stringBuilder = this.sb;
				if (stringBuilder == null)
				{
					return null;
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x0600479C RID: 18332 RVA: 0x001744B0 File Offset: 0x001744B0
		private FullNameFactory(bool isReflection, IFullNameFactoryHelper helper, StringBuilder sb)
		{
			this.sb = (sb ?? new StringBuilder());
			this.isReflection = isReflection;
			this.helper = helper;
			this.genericArguments = null;
			this.recursionCounter = default(RecursionCounter);
		}

		// Token: 0x0600479D RID: 18333 RVA: 0x001744E8 File Offset: 0x001744E8
		private bool MustUseAssemblyName(IType type)
		{
			return this.helper == null || this.helper.MustUseAssemblyName(this.GetDefinitionType(type));
		}

		// Token: 0x0600479E RID: 18334 RVA: 0x0017450C File Offset: 0x0017450C
		private IType GetDefinitionType(IType type)
		{
			if (!this.recursionCounter.Increment())
			{
				return type;
			}
			TypeSpec typeSpec = type as TypeSpec;
			if (typeSpec != null)
			{
				type = typeSpec.TypeSig;
			}
			TypeSig typeSig = type as TypeSig;
			if (typeSig != null)
			{
				TypeDefOrRefSig typeDefOrRefSig = typeSig as TypeDefOrRefSig;
				GenericInstSig genericInstSig;
				if (typeDefOrRefSig != null)
				{
					type = this.GetDefinitionType(typeDefOrRefSig.TypeDefOrRef);
				}
				else if ((genericInstSig = (typeSig as GenericInstSig)) != null)
				{
					type = this.GetDefinitionType(genericInstSig.GenericType);
				}
				else
				{
					type = this.GetDefinitionType(typeSig.Next);
				}
			}
			this.recursionCounter.Decrement();
			return type;
		}

		// Token: 0x0600479F RID: 18335 RVA: 0x001745AC File Offset: 0x001745AC
		private void CreateFullName(ITypeDefOrRef typeDefOrRef)
		{
			if (typeDefOrRef is TypeRef)
			{
				this.CreateFullName((TypeRef)typeDefOrRef);
				return;
			}
			if (typeDefOrRef is TypeDef)
			{
				this.CreateFullName((TypeDef)typeDefOrRef);
				return;
			}
			if (typeDefOrRef is TypeSpec)
			{
				this.CreateFullName((TypeSpec)typeDefOrRef);
				return;
			}
			this.sb.Append("<<<NULL>>>");
		}

		// Token: 0x060047A0 RID: 18336 RVA: 0x00174618 File Offset: 0x00174618
		private void CreateNamespace(ITypeDefOrRef typeDefOrRef)
		{
			if (typeDefOrRef is TypeRef)
			{
				this.CreateNamespace((TypeRef)typeDefOrRef);
				return;
			}
			if (typeDefOrRef is TypeDef)
			{
				this.CreateNamespace((TypeDef)typeDefOrRef);
				return;
			}
			if (typeDefOrRef is TypeSpec)
			{
				this.CreateNamespace((TypeSpec)typeDefOrRef);
				return;
			}
			this.sb.Append("<<<NULL>>>");
		}

		// Token: 0x060047A1 RID: 18337 RVA: 0x00174684 File Offset: 0x00174684
		private void CreateName(ITypeDefOrRef typeDefOrRef)
		{
			if (typeDefOrRef is TypeRef)
			{
				this.CreateName((TypeRef)typeDefOrRef);
				return;
			}
			if (typeDefOrRef is TypeDef)
			{
				this.CreateName((TypeDef)typeDefOrRef);
				return;
			}
			if (typeDefOrRef is TypeSpec)
			{
				this.CreateName((TypeSpec)typeDefOrRef);
				return;
			}
			this.sb.Append("<<<NULL>>>");
		}

		// Token: 0x060047A2 RID: 18338 RVA: 0x001746F0 File Offset: 0x001746F0
		private void CreateAssemblyQualifiedName(ITypeDefOrRef typeDefOrRef)
		{
			if (typeDefOrRef is TypeRef)
			{
				this.CreateAssemblyQualifiedName((TypeRef)typeDefOrRef);
				return;
			}
			if (typeDefOrRef is TypeDef)
			{
				this.CreateAssemblyQualifiedName((TypeDef)typeDefOrRef);
				return;
			}
			if (typeDefOrRef is TypeSpec)
			{
				this.CreateAssemblyQualifiedName((TypeSpec)typeDefOrRef);
				return;
			}
			this.sb.Append("<<<NULL>>>");
		}

		// Token: 0x060047A3 RID: 18339 RVA: 0x0017475C File Offset: 0x0017475C
		private void CreateAssemblyQualifiedName(TypeRef typeRef)
		{
			if (typeRef == null)
			{
				this.sb.Append("<<<NULL>>>");
				return;
			}
			if (!this.recursionCounter.Increment())
			{
				this.sb.Append("<<<INFRECURSION>>>");
				return;
			}
			this.CreateFullName(typeRef);
			if (this.MustUseAssemblyName(typeRef))
			{
				this.AddAssemblyName(this.GetDefinitionAssembly(typeRef));
			}
			this.recursionCounter.Decrement();
		}

		// Token: 0x060047A4 RID: 18340 RVA: 0x001747D4 File Offset: 0x001747D4
		private void CreateFullName(TypeRef typeRef)
		{
			if (typeRef == null)
			{
				this.sb.Append("<<<NULL>>>");
				return;
			}
			if (!this.recursionCounter.Increment())
			{
				this.sb.Append("<<<INFRECURSION>>>");
				return;
			}
			TypeRef typeRef2 = typeRef.ResolutionScope as TypeRef;
			if (typeRef2 != null)
			{
				this.CreateFullName(typeRef2);
				this.AddNestedTypeSeparator();
			}
			if (this.AddNamespace(typeRef.Namespace))
			{
				this.sb.Append('.');
			}
			this.AddName(typeRef.Name);
			this.recursionCounter.Decrement();
		}

		// Token: 0x060047A5 RID: 18341 RVA: 0x00174878 File Offset: 0x00174878
		private void CreateNamespace(TypeRef typeRef)
		{
			if (typeRef == null)
			{
				this.sb.Append("<<<NULL>>>");
				return;
			}
			this.AddNamespace(typeRef.Namespace);
		}

		// Token: 0x060047A6 RID: 18342 RVA: 0x001748A0 File Offset: 0x001748A0
		private void CreateName(TypeRef typeRef)
		{
			if (typeRef == null)
			{
				this.sb.Append("<<<NULL>>>");
				return;
			}
			this.AddName(typeRef.Name);
		}

		// Token: 0x060047A7 RID: 18343 RVA: 0x001748C8 File Offset: 0x001748C8
		private void CreateAssemblyQualifiedName(TypeDef typeDef)
		{
			if (typeDef == null)
			{
				this.sb.Append("<<<NULL>>>");
				return;
			}
			if (!this.recursionCounter.Increment())
			{
				this.sb.Append("<<<INFRECURSION>>>");
				return;
			}
			this.CreateFullName(typeDef);
			if (this.MustUseAssemblyName(typeDef))
			{
				this.AddAssemblyName(this.GetDefinitionAssembly(typeDef));
			}
			this.recursionCounter.Decrement();
		}

		// Token: 0x060047A8 RID: 18344 RVA: 0x00174940 File Offset: 0x00174940
		private void CreateFullName(TypeDef typeDef)
		{
			if (typeDef == null)
			{
				this.sb.Append("<<<NULL>>>");
				return;
			}
			if (!this.recursionCounter.Increment())
			{
				this.sb.Append("<<<INFRECURSION>>>");
				return;
			}
			TypeDef declaringType = typeDef.DeclaringType;
			if (declaringType != null)
			{
				this.CreateFullName(declaringType);
				this.AddNestedTypeSeparator();
			}
			if (this.AddNamespace(typeDef.Namespace))
			{
				this.sb.Append('.');
			}
			this.AddName(typeDef.Name);
			this.recursionCounter.Decrement();
		}

		// Token: 0x060047A9 RID: 18345 RVA: 0x001749DC File Offset: 0x001749DC
		private void CreateNamespace(TypeDef typeDef)
		{
			if (typeDef == null)
			{
				this.sb.Append("<<<NULL>>>");
				return;
			}
			this.AddNamespace(typeDef.Namespace);
		}

		// Token: 0x060047AA RID: 18346 RVA: 0x00174A04 File Offset: 0x00174A04
		private void CreateName(TypeDef typeDef)
		{
			if (typeDef == null)
			{
				this.sb.Append("<<<NULL>>>");
				return;
			}
			this.AddName(typeDef.Name);
		}

		// Token: 0x060047AB RID: 18347 RVA: 0x00174A2C File Offset: 0x00174A2C
		private void CreateAssemblyQualifiedName(TypeSpec typeSpec)
		{
			if (typeSpec == null)
			{
				this.sb.Append("<<<NULL>>>");
				return;
			}
			this.CreateAssemblyQualifiedName(typeSpec.TypeSig);
		}

		// Token: 0x060047AC RID: 18348 RVA: 0x00174A54 File Offset: 0x00174A54
		private void CreateFullName(TypeSpec typeSpec)
		{
			if (typeSpec == null)
			{
				this.sb.Append("<<<NULL>>>");
				return;
			}
			this.CreateFullName(typeSpec.TypeSig);
		}

		// Token: 0x060047AD RID: 18349 RVA: 0x00174A7C File Offset: 0x00174A7C
		private void CreateNamespace(TypeSpec typeSpec)
		{
			if (typeSpec == null)
			{
				this.sb.Append("<<<NULL>>>");
				return;
			}
			this.CreateNamespace(typeSpec.TypeSig);
		}

		// Token: 0x060047AE RID: 18350 RVA: 0x00174AA4 File Offset: 0x00174AA4
		private void CreateName(TypeSpec typeSpec)
		{
			if (typeSpec == null)
			{
				this.sb.Append("<<<NULL>>>");
				return;
			}
			this.CreateName(typeSpec.TypeSig);
		}

		// Token: 0x060047AF RID: 18351 RVA: 0x00174ACC File Offset: 0x00174ACC
		private void CreateAssemblyQualifiedName(TypeSig typeSig)
		{
			if (typeSig == null)
			{
				this.sb.Append("<<<NULL>>>");
				return;
			}
			if (!this.recursionCounter.Increment())
			{
				this.sb.Append("<<<INFRECURSION>>>");
				return;
			}
			this.CreateFullName(typeSig);
			if (this.MustUseAssemblyName(typeSig))
			{
				this.AddAssemblyName(this.GetDefinitionAssembly(typeSig));
			}
			this.recursionCounter.Decrement();
		}

		// Token: 0x060047B0 RID: 18352 RVA: 0x00174B44 File Offset: 0x00174B44
		private void CreateFullName(TypeSig typeSig)
		{
			this.CreateTypeSigName(typeSig, 3);
		}

		// Token: 0x060047B1 RID: 18353 RVA: 0x00174B50 File Offset: 0x00174B50
		private void CreateNamespace(TypeSig typeSig)
		{
			this.CreateTypeSigName(typeSig, 1);
		}

		// Token: 0x060047B2 RID: 18354 RVA: 0x00174B5C File Offset: 0x00174B5C
		private void CreateName(TypeSig typeSig)
		{
			this.CreateTypeSigName(typeSig, 2);
		}

		// Token: 0x060047B3 RID: 18355 RVA: 0x00174B68 File Offset: 0x00174B68
		private TypeSig ReplaceGenericArg(TypeSig typeSig)
		{
			if (this.genericArguments == null)
			{
				return typeSig;
			}
			TypeSig typeSig2 = this.genericArguments.Resolve(typeSig);
			if (typeSig2 != typeSig)
			{
				this.genericArguments = null;
			}
			return typeSig2;
		}

		// Token: 0x060047B4 RID: 18356 RVA: 0x00174B94 File Offset: 0x00174B94
		private void CreateTypeSigName(TypeSig typeSig, int flags)
		{
			if (typeSig == null)
			{
				this.sb.Append("<<<NULL>>>");
				return;
			}
			if (!this.recursionCounter.Increment())
			{
				this.sb.Append("<<<INFRECURSION>>>");
				return;
			}
			GenericArguments genericArguments = this.genericArguments;
			typeSig = this.ReplaceGenericArg(typeSig);
			bool flag = (flags & 1) != 0;
			bool flag2 = (flags & 2) != 0;
			switch (typeSig.ElementType)
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
			case ElementType.String:
			case ElementType.ValueType:
			case ElementType.Class:
			case ElementType.TypedByRef:
			case ElementType.I:
			case ElementType.U:
			case ElementType.Object:
				if (flag && flag2)
				{
					this.CreateFullName(((TypeDefOrRefSig)typeSig).TypeDefOrRef);
				}
				else if (flag)
				{
					this.CreateNamespace(((TypeDefOrRefSig)typeSig).TypeDefOrRef);
				}
				else if (flag2)
				{
					this.CreateName(((TypeDefOrRefSig)typeSig).TypeDefOrRef);
				}
				break;
			case ElementType.Ptr:
				this.CreateTypeSigName(typeSig.Next, flags);
				if (flag2)
				{
					this.sb.Append('*');
				}
				break;
			case ElementType.ByRef:
				this.CreateTypeSigName(typeSig.Next, flags);
				if (flag2)
				{
					this.sb.Append('&');
				}
				break;
			case ElementType.Var:
			case ElementType.MVar:
				if (flag2)
				{
					GenericSig genericSig = (GenericSig)typeSig;
					GenericParam genericParam = genericSig.GenericParam;
					if (genericParam == null || !this.AddName(genericParam.Name))
					{
						this.sb.Append(genericSig.IsMethodVar ? "!!" : "!");
						this.sb.Append(genericSig.Number);
					}
				}
				break;
			case ElementType.Array:
				this.CreateTypeSigName(typeSig.Next, flags);
				if (flag2)
				{
					ArraySig arraySig = (ArraySig)typeSig;
					this.sb.Append('[');
					uint num = arraySig.Rank;
					if (num > 100U)
					{
						num = 100U;
					}
					if (num == 0U)
					{
						this.sb.Append("<RANK0>");
					}
					else if (num == 1U)
					{
						this.sb.Append('*');
					}
					else
					{
						for (int i = 0; i < (int)num; i++)
						{
							if (i != 0)
							{
								this.sb.Append(',');
							}
							if (!this.isReflection)
							{
								int num2 = (i < arraySig.LowerBounds.Count) ? arraySig.LowerBounds[i] : int.MinValue;
								uint num3 = (i < arraySig.Sizes.Count) ? arraySig.Sizes[i] : uint.MaxValue;
								if (num2 != -2147483648)
								{
									this.sb.Append(num2);
									this.sb.Append("..");
									if (num3 != 4294967295U)
									{
										this.sb.Append(num2 + (int)num3 - 1);
									}
									else
									{
										this.sb.Append('.');
									}
								}
							}
						}
					}
					this.sb.Append(']');
				}
				break;
			case ElementType.GenericInst:
			{
				GenericInstSig genericInstSig = (GenericInstSig)typeSig;
				IList<TypeSig> list = genericInstSig.GenericArguments;
				this.CreateTypeSigName(genericInstSig.GenericType, flags);
				if (flag && flag2)
				{
					if (this.isReflection)
					{
						this.sb.Append('[');
						int num4 = -1;
						int count = list.Count;
						for (int j = 0; j < count; j++)
						{
							TypeSig typeSig2 = list[j];
							num4++;
							if (num4 != 0)
							{
								this.sb.Append(',');
							}
							bool flag3 = this.MustUseAssemblyName(typeSig2);
							if (flag3)
							{
								this.sb.Append('[');
							}
							this.CreateFullName(typeSig2);
							if (flag3)
							{
								this.sb.Append(", ");
								IAssembly definitionAssembly = this.GetDefinitionAssembly(typeSig2);
								if (definitionAssembly == null)
								{
									this.sb.Append("<<<NULL>>>");
								}
								else
								{
									this.sb.Append(FullNameFactory.EscapeAssemblyName(FullNameFactory.GetAssemblyName(definitionAssembly)));
								}
								this.sb.Append(']');
							}
						}
						this.sb.Append(']');
					}
					else
					{
						this.sb.Append('<');
						int num5 = -1;
						int count2 = list.Count;
						for (int k = 0; k < count2; k++)
						{
							TypeSig typeSig3 = list[k];
							num5++;
							if (num5 != 0)
							{
								this.sb.Append(',');
							}
							this.CreateFullName(typeSig3);
						}
						this.sb.Append('>');
					}
				}
				break;
			}
			case ElementType.ValueArray:
				this.CreateTypeSigName(typeSig.Next, flags);
				if (flag2)
				{
					ValueArraySig valueArraySig = (ValueArraySig)typeSig;
					this.sb.Append(" ValueArray(");
					this.sb.Append(valueArraySig.Size);
					this.sb.Append(')');
				}
				break;
			case ElementType.FnPtr:
				if (flag2)
				{
					if (this.isReflection)
					{
						this.sb.Append("(fnptr)");
					}
					else
					{
						this.CreateMethodFullName(null, null, ((FnPtrSig)typeSig).MethodSig, null);
					}
				}
				break;
			case ElementType.SZArray:
				this.CreateTypeSigName(typeSig.Next, flags);
				if (flag2)
				{
					this.sb.Append("[]");
				}
				break;
			case ElementType.CModReqd:
				this.CreateTypeSigName(typeSig.Next, flags);
				if (!this.isReflection && flag2)
				{
					this.sb.Append(" modreq(");
					if (flag)
					{
						this.CreateFullName(((ModifierSig)typeSig).Modifier);
					}
					else
					{
						this.CreateName(((ModifierSig)typeSig).Modifier);
					}
					this.sb.Append(")");
				}
				break;
			case ElementType.CModOpt:
				this.CreateTypeSigName(typeSig.Next, flags);
				if (!this.isReflection && flag2)
				{
					this.sb.Append(" modopt(");
					if (flag)
					{
						this.CreateFullName(((ModifierSig)typeSig).Modifier);
					}
					else
					{
						this.CreateName(((ModifierSig)typeSig).Modifier);
					}
					this.sb.Append(")");
				}
				break;
			case ElementType.Module:
				this.CreateTypeSigName(typeSig.Next, flags);
				if (flag2)
				{
					ModuleSig moduleSig = (ModuleSig)typeSig;
					this.sb.Append(" Module(");
					this.sb.Append(moduleSig.Index);
					this.sb.Append(')');
				}
				break;
			case ElementType.Pinned:
				this.CreateTypeSigName(typeSig.Next, flags);
				break;
			}
			this.genericArguments = genericArguments;
			this.recursionCounter.Decrement();
		}

		// Token: 0x060047B5 RID: 18357 RVA: 0x00175348 File Offset: 0x00175348
		private void CreateAssemblyQualifiedName(ExportedType exportedType)
		{
			if (exportedType == null)
			{
				this.sb.Append("<<<NULL>>>");
				return;
			}
			if (!this.recursionCounter.Increment())
			{
				this.sb.Append("<<<INFRECURSION>>>");
				return;
			}
			this.CreateFullName(exportedType);
			if (this.MustUseAssemblyName(exportedType))
			{
				this.AddAssemblyName(this.GetDefinitionAssembly(exportedType));
			}
			this.recursionCounter.Decrement();
		}

		// Token: 0x060047B6 RID: 18358 RVA: 0x001753C0 File Offset: 0x001753C0
		private void CreateFullName(ExportedType exportedType)
		{
			if (exportedType == null)
			{
				this.sb.Append("<<<NULL>>>");
				return;
			}
			if (!this.recursionCounter.Increment())
			{
				this.sb.Append("<<<INFRECURSION>>>");
				return;
			}
			ExportedType exportedType2 = exportedType.Implementation as ExportedType;
			if (exportedType2 != null)
			{
				this.CreateFullName(exportedType2);
				this.AddNestedTypeSeparator();
			}
			if (this.AddNamespace(exportedType.TypeNamespace))
			{
				this.sb.Append('.');
			}
			this.AddName(exportedType.TypeName);
			this.recursionCounter.Decrement();
		}

		// Token: 0x060047B7 RID: 18359 RVA: 0x00175464 File Offset: 0x00175464
		private void CreateNamespace(ExportedType exportedType)
		{
			if (exportedType == null)
			{
				this.sb.Append("<<<NULL>>>");
				return;
			}
			this.AddNamespace(exportedType.TypeNamespace);
		}

		// Token: 0x060047B8 RID: 18360 RVA: 0x0017548C File Offset: 0x0017548C
		private void CreateName(ExportedType exportedType)
		{
			if (exportedType == null)
			{
				this.sb.Append("<<<NULL>>>");
				return;
			}
			this.AddName(exportedType.TypeName);
		}

		// Token: 0x060047B9 RID: 18361 RVA: 0x001754B4 File Offset: 0x001754B4
		private static string GetAssemblyName(IAssembly assembly)
		{
			PublicKeyBase publicKeyBase = assembly.PublicKeyOrToken;
			if (publicKeyBase is PublicKey)
			{
				publicKeyBase = ((PublicKey)publicKeyBase).Token;
			}
			return Utils.GetAssemblyNameString(FullNameFactory.EscapeAssemblyName(assembly.Name), assembly.Version, assembly.Culture, publicKeyBase, assembly.Attributes);
		}

		// Token: 0x060047BA RID: 18362 RVA: 0x0017550C File Offset: 0x0017550C
		private static string EscapeAssemblyName(UTF8String asmSimpleName)
		{
			return FullNameFactory.EscapeAssemblyName(UTF8String.ToSystemString(asmSimpleName));
		}

		// Token: 0x060047BB RID: 18363 RVA: 0x0017551C File Offset: 0x0017551C
		private static string EscapeAssemblyName(string asmSimpleName)
		{
			if (asmSimpleName.IndexOf(']') < 0)
			{
				return asmSimpleName;
			}
			StringBuilder stringBuilder = new StringBuilder(asmSimpleName.Length);
			foreach (char c in asmSimpleName)
			{
				if (c == ']')
				{
					stringBuilder.Append('\\');
				}
				stringBuilder.Append(c);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060047BC RID: 18364 RVA: 0x00175588 File Offset: 0x00175588
		private void AddNestedTypeSeparator()
		{
			if (this.isReflection)
			{
				this.sb.Append('+');
				return;
			}
			this.sb.Append('/');
		}

		// Token: 0x060047BD RID: 18365 RVA: 0x001755B4 File Offset: 0x001755B4
		private bool AddNamespace(UTF8String @namespace)
		{
			if (UTF8String.IsNullOrEmpty(@namespace))
			{
				return false;
			}
			this.AddIdentifier(@namespace.String);
			return true;
		}

		// Token: 0x060047BE RID: 18366 RVA: 0x001755D0 File Offset: 0x001755D0
		private bool AddName(UTF8String name)
		{
			if (UTF8String.IsNullOrEmpty(name))
			{
				return false;
			}
			this.AddIdentifier(name.String);
			return true;
		}

		// Token: 0x060047BF RID: 18367 RVA: 0x001755EC File Offset: 0x001755EC
		private void AddAssemblyName(IAssembly assembly)
		{
			this.sb.Append(", ");
			if (assembly == null)
			{
				this.sb.Append("<<<NULL>>>");
				return;
			}
			PublicKeyBase publicKeyBase = assembly.PublicKeyOrToken;
			if (publicKeyBase is PublicKey)
			{
				publicKeyBase = ((PublicKey)publicKeyBase).Token;
			}
			this.sb.Append(Utils.GetAssemblyNameString(assembly.Name, assembly.Version, assembly.Culture, publicKeyBase, assembly.Attributes));
		}

		// Token: 0x060047C0 RID: 18368 RVA: 0x00175670 File Offset: 0x00175670
		private void AddIdentifier(string id)
		{
			if (this.isReflection)
			{
				int i = 0;
				while (i < id.Length)
				{
					char value = id[i];
					switch (value)
					{
					case '&':
					case '*':
					case '+':
					case ',':
						goto IL_5B;
					case '\'':
					case '(':
					case ')':
						break;
					default:
						switch (value)
						{
						case '[':
						case '\\':
						case ']':
							goto IL_5B;
						}
						break;
					}
					IL_69:
					this.sb.Append(value);
					i++;
					continue;
					IL_5B:
					this.sb.Append('\\');
					goto IL_69;
				}
				return;
			}
			this.sb.Append(id);
		}

		// Token: 0x060047C1 RID: 18369 RVA: 0x00175714 File Offset: 0x00175714
		private IAssembly GetDefinitionAssembly(ITypeDefOrRef typeDefOrRef)
		{
			TypeRef typeRef = typeDefOrRef as TypeRef;
			if (typeRef != null)
			{
				return this.GetDefinitionAssembly(typeRef);
			}
			TypeDef typeDef = typeDefOrRef as TypeDef;
			if (typeDef != null)
			{
				return this.GetDefinitionAssembly(typeDef);
			}
			TypeSpec typeSpec = typeDefOrRef as TypeSpec;
			if (typeSpec != null)
			{
				return this.GetDefinitionAssembly(typeSpec);
			}
			return null;
		}

		// Token: 0x060047C2 RID: 18370 RVA: 0x00175768 File Offset: 0x00175768
		private IScope GetScope(ITypeDefOrRef typeDefOrRef)
		{
			TypeRef typeRef = typeDefOrRef as TypeRef;
			if (typeRef != null)
			{
				return this.GetScope(typeRef);
			}
			TypeDef typeDef = typeDefOrRef as TypeDef;
			if (typeDef != null)
			{
				return typeDef.Scope;
			}
			TypeSpec typeSpec = typeDefOrRef as TypeSpec;
			if (typeSpec != null)
			{
				return this.GetScope(typeSpec);
			}
			return null;
		}

		// Token: 0x060047C3 RID: 18371 RVA: 0x001757B8 File Offset: 0x001757B8
		private ITypeDefOrRef GetScopeType(ITypeDefOrRef typeDefOrRef)
		{
			TypeRef typeRef = typeDefOrRef as TypeRef;
			if (typeRef != null)
			{
				return typeRef;
			}
			TypeDef typeDef = typeDefOrRef as TypeDef;
			if (typeDef != null)
			{
				return typeDef;
			}
			TypeSpec typeSpec = typeDefOrRef as TypeSpec;
			if (typeSpec != null)
			{
				return this.GetScopeType(typeSpec);
			}
			return null;
		}

		// Token: 0x060047C4 RID: 18372 RVA: 0x00175800 File Offset: 0x00175800
		private ModuleDef GetOwnerModule(ITypeDefOrRef typeDefOrRef)
		{
			TypeRef typeRef = typeDefOrRef as TypeRef;
			if (typeRef != null)
			{
				return this.GetOwnerModule(typeRef);
			}
			TypeDef typeDef = typeDefOrRef as TypeDef;
			if (typeDef != null)
			{
				return this.GetOwnerModule(typeDef);
			}
			TypeSpec typeSpec = typeDefOrRef as TypeSpec;
			if (typeSpec != null)
			{
				return this.GetOwnerModule(typeSpec);
			}
			return null;
		}

		// Token: 0x060047C5 RID: 18373 RVA: 0x00175854 File Offset: 0x00175854
		private IAssembly GetDefinitionAssembly(TypeRef typeRef)
		{
			if (typeRef == null)
			{
				return null;
			}
			if (!this.recursionCounter.Increment())
			{
				return null;
			}
			IResolutionScope resolutionScope = typeRef.ResolutionScope;
			IAssembly result;
			if (resolutionScope == null)
			{
				result = null;
			}
			else if (resolutionScope is TypeRef)
			{
				result = this.GetDefinitionAssembly((TypeRef)resolutionScope);
			}
			else if (resolutionScope is AssemblyRef)
			{
				result = (AssemblyRef)resolutionScope;
			}
			else if (resolutionScope is ModuleRef)
			{
				ModuleDef ownerModule = this.GetOwnerModule(typeRef);
				result = ((ownerModule != null) ? ownerModule.Assembly : null);
			}
			else if (resolutionScope is ModuleDef)
			{
				result = ((ModuleDef)resolutionScope).Assembly;
			}
			else
			{
				result = null;
			}
			this.recursionCounter.Decrement();
			return result;
		}

		// Token: 0x060047C6 RID: 18374 RVA: 0x0017591C File Offset: 0x0017591C
		private IScope GetScope(TypeRef typeRef)
		{
			if (typeRef == null)
			{
				return null;
			}
			if (!this.recursionCounter.Increment())
			{
				return null;
			}
			IResolutionScope resolutionScope = typeRef.ResolutionScope;
			IScope result;
			TypeRef typeRef2;
			AssemblyRef assemblyRef;
			ModuleRef moduleRef;
			ModuleDef moduleDef;
			if (resolutionScope == null)
			{
				result = null;
			}
			else if ((typeRef2 = (resolutionScope as TypeRef)) != null)
			{
				result = this.GetScope(typeRef2);
			}
			else if ((assemblyRef = (resolutionScope as AssemblyRef)) != null)
			{
				result = assemblyRef;
			}
			else if ((moduleRef = (resolutionScope as ModuleRef)) != null)
			{
				result = moduleRef;
			}
			else if ((moduleDef = (resolutionScope as ModuleDef)) != null)
			{
				result = moduleDef;
			}
			else
			{
				result = null;
			}
			this.recursionCounter.Decrement();
			return result;
		}

		// Token: 0x060047C7 RID: 18375 RVA: 0x001759C4 File Offset: 0x001759C4
		private ModuleDef GetOwnerModule(TypeRef typeRef)
		{
			if (typeRef == null)
			{
				return null;
			}
			return typeRef.Module;
		}

		// Token: 0x060047C8 RID: 18376 RVA: 0x001759D4 File Offset: 0x001759D4
		private IAssembly GetDefinitionAssembly(TypeDef typeDef)
		{
			ModuleDef ownerModule = this.GetOwnerModule(typeDef);
			if (ownerModule == null)
			{
				return null;
			}
			return ownerModule.Assembly;
		}

		// Token: 0x060047C9 RID: 18377 RVA: 0x001759EC File Offset: 0x001759EC
		private ModuleDef GetOwnerModule(TypeDef typeDef)
		{
			if (typeDef == null)
			{
				return null;
			}
			ModuleDef result = null;
			for (int i = this.recursionCounter.Counter; i < 100; i++)
			{
				TypeDef declaringType = typeDef.DeclaringType;
				if (declaringType == null)
				{
					result = typeDef.Module2;
					break;
				}
				typeDef = declaringType;
			}
			return result;
		}

		// Token: 0x060047CA RID: 18378 RVA: 0x00175A40 File Offset: 0x00175A40
		private IAssembly GetDefinitionAssembly(TypeSpec typeSpec)
		{
			if (typeSpec == null)
			{
				return null;
			}
			return this.GetDefinitionAssembly(typeSpec.TypeSig);
		}

		// Token: 0x060047CB RID: 18379 RVA: 0x00175A58 File Offset: 0x00175A58
		private IScope GetScope(TypeSpec typeSpec)
		{
			if (typeSpec == null)
			{
				return null;
			}
			return this.GetScope(typeSpec.TypeSig);
		}

		// Token: 0x060047CC RID: 18380 RVA: 0x00175A70 File Offset: 0x00175A70
		private ITypeDefOrRef GetScopeType(TypeSpec typeSpec)
		{
			if (typeSpec == null)
			{
				return null;
			}
			return this.GetScopeType(typeSpec.TypeSig);
		}

		// Token: 0x060047CD RID: 18381 RVA: 0x00175A88 File Offset: 0x00175A88
		private ModuleDef GetOwnerModule(TypeSpec typeSpec)
		{
			if (typeSpec == null)
			{
				return null;
			}
			return this.GetOwnerModule(typeSpec.TypeSig);
		}

		// Token: 0x060047CE RID: 18382 RVA: 0x00175AA0 File Offset: 0x00175AA0
		private IAssembly GetDefinitionAssembly(TypeSig typeSig)
		{
			if (typeSig == null)
			{
				return null;
			}
			if (!this.recursionCounter.Increment())
			{
				return null;
			}
			GenericArguments genericArguments = this.genericArguments;
			typeSig = this.ReplaceGenericArg(typeSig);
			IAssembly result;
			switch (typeSig.ElementType)
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
			case ElementType.String:
			case ElementType.ValueType:
			case ElementType.Class:
			case ElementType.TypedByRef:
			case ElementType.I:
			case ElementType.U:
			case ElementType.Object:
				result = this.GetDefinitionAssembly(((TypeDefOrRefSig)typeSig).TypeDefOrRef);
				goto IL_1A9;
			case ElementType.Ptr:
			case ElementType.ByRef:
			case ElementType.Array:
			case ElementType.ValueArray:
			case ElementType.SZArray:
			case ElementType.CModReqd:
			case ElementType.CModOpt:
			case ElementType.Module:
			case ElementType.Pinned:
				result = this.GetDefinitionAssembly(typeSig.Next);
				goto IL_1A9;
			case ElementType.GenericInst:
			{
				ClassOrValueTypeSig genericType = ((GenericInstSig)typeSig).GenericType;
				result = this.GetDefinitionAssembly((genericType != null) ? genericType.TypeDefOrRef : null);
				goto IL_1A9;
			}
			}
			result = null;
			IL_1A9:
			this.genericArguments = genericArguments;
			this.recursionCounter.Decrement();
			return result;
		}

		// Token: 0x060047CF RID: 18383 RVA: 0x00175C70 File Offset: 0x00175C70
		private ITypeDefOrRef GetScopeType(TypeSig typeSig)
		{
			if (typeSig == null)
			{
				return null;
			}
			if (!this.recursionCounter.Increment())
			{
				return null;
			}
			GenericArguments genericArguments = this.genericArguments;
			typeSig = this.ReplaceGenericArg(typeSig);
			ITypeDefOrRef result;
			switch (typeSig.ElementType)
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
			case ElementType.String:
			case ElementType.ValueType:
			case ElementType.Class:
			case ElementType.TypedByRef:
			case ElementType.I:
			case ElementType.U:
			case ElementType.Object:
				result = this.GetScopeType(((TypeDefOrRefSig)typeSig).TypeDefOrRef);
				goto IL_1A8;
			case ElementType.Ptr:
			case ElementType.ByRef:
			case ElementType.Array:
			case ElementType.ValueArray:
			case ElementType.SZArray:
			case ElementType.CModReqd:
			case ElementType.CModOpt:
			case ElementType.Module:
			case ElementType.Pinned:
				result = this.GetScopeType(typeSig.Next);
				goto IL_1A8;
			case ElementType.GenericInst:
			{
				ClassOrValueTypeSig genericType = ((GenericInstSig)typeSig).GenericType;
				result = this.GetScopeType((genericType != null) ? genericType.TypeDefOrRef : null);
				goto IL_1A8;
			}
			}
			result = null;
			IL_1A8:
			this.genericArguments = genericArguments;
			this.recursionCounter.Decrement();
			return result;
		}

		// Token: 0x060047D0 RID: 18384 RVA: 0x00175E3C File Offset: 0x00175E3C
		private IScope GetScope(TypeSig typeSig)
		{
			if (typeSig == null)
			{
				return null;
			}
			if (!this.recursionCounter.Increment())
			{
				return null;
			}
			GenericArguments genericArguments = this.genericArguments;
			typeSig = this.ReplaceGenericArg(typeSig);
			IScope result;
			switch (typeSig.ElementType)
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
			case ElementType.String:
			case ElementType.ValueType:
			case ElementType.Class:
			case ElementType.TypedByRef:
			case ElementType.I:
			case ElementType.U:
			case ElementType.Object:
				result = this.GetScope(((TypeDefOrRefSig)typeSig).TypeDefOrRef);
				goto IL_1A8;
			case ElementType.Ptr:
			case ElementType.ByRef:
			case ElementType.Array:
			case ElementType.ValueArray:
			case ElementType.SZArray:
			case ElementType.CModReqd:
			case ElementType.CModOpt:
			case ElementType.Module:
			case ElementType.Pinned:
				result = this.GetScope(typeSig.Next);
				goto IL_1A8;
			case ElementType.GenericInst:
			{
				ClassOrValueTypeSig genericType = ((GenericInstSig)typeSig).GenericType;
				result = this.GetScope((genericType != null) ? genericType.TypeDefOrRef : null);
				goto IL_1A8;
			}
			}
			result = null;
			IL_1A8:
			this.genericArguments = genericArguments;
			this.recursionCounter.Decrement();
			return result;
		}

		// Token: 0x060047D1 RID: 18385 RVA: 0x00176008 File Offset: 0x00176008
		private ModuleDef GetOwnerModule(TypeSig typeSig)
		{
			if (typeSig == null)
			{
				return null;
			}
			if (!this.recursionCounter.Increment())
			{
				return null;
			}
			GenericArguments genericArguments = this.genericArguments;
			typeSig = this.ReplaceGenericArg(typeSig);
			ModuleDef result;
			switch (typeSig.ElementType)
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
			case ElementType.String:
			case ElementType.ValueType:
			case ElementType.Class:
			case ElementType.TypedByRef:
			case ElementType.I:
			case ElementType.U:
			case ElementType.Object:
				result = this.GetOwnerModule(((TypeDefOrRefSig)typeSig).TypeDefOrRef);
				goto IL_1A8;
			case ElementType.Ptr:
			case ElementType.ByRef:
			case ElementType.Array:
			case ElementType.ValueArray:
			case ElementType.SZArray:
			case ElementType.CModReqd:
			case ElementType.CModOpt:
			case ElementType.Module:
			case ElementType.Pinned:
				result = this.GetOwnerModule(typeSig.Next);
				goto IL_1A8;
			case ElementType.GenericInst:
			{
				ClassOrValueTypeSig genericType = ((GenericInstSig)typeSig).GenericType;
				result = this.GetOwnerModule((genericType != null) ? genericType.TypeDefOrRef : null);
				goto IL_1A8;
			}
			}
			result = null;
			IL_1A8:
			this.genericArguments = genericArguments;
			this.recursionCounter.Decrement();
			return result;
		}

		// Token: 0x060047D2 RID: 18386 RVA: 0x001761D4 File Offset: 0x001761D4
		private IAssembly GetDefinitionAssembly(ExportedType exportedType)
		{
			if (exportedType == null)
			{
				return null;
			}
			if (!this.recursionCounter.Increment())
			{
				return null;
			}
			IImplementation implementation = exportedType.Implementation;
			ExportedType exportedType2 = implementation as ExportedType;
			IAssembly result;
			AssemblyRef assemblyRef;
			if (exportedType2 != null)
			{
				result = this.GetDefinitionAssembly(exportedType2);
			}
			else if ((assemblyRef = (implementation as AssemblyRef)) != null)
			{
				result = assemblyRef;
			}
			else if (implementation is FileDef)
			{
				ModuleDef ownerModule = this.GetOwnerModule(exportedType);
				result = ((ownerModule != null) ? ownerModule.Assembly : null);
			}
			else
			{
				result = null;
			}
			this.recursionCounter.Decrement();
			return result;
		}

		// Token: 0x060047D3 RID: 18387 RVA: 0x0017626C File Offset: 0x0017626C
		private ITypeDefOrRef GetScopeType(ExportedType exportedType)
		{
			return null;
		}

		// Token: 0x060047D4 RID: 18388 RVA: 0x00176270 File Offset: 0x00176270
		private IScope GetScope(ExportedType exportedType)
		{
			if (exportedType == null)
			{
				return null;
			}
			if (!this.recursionCounter.Increment())
			{
				return null;
			}
			IImplementation implementation = exportedType.Implementation;
			ExportedType exportedType2 = implementation as ExportedType;
			IScope result;
			AssemblyRef assemblyRef;
			FileDef fileDef;
			if (exportedType2 != null)
			{
				result = this.GetScope(exportedType2);
			}
			else if ((assemblyRef = (implementation as AssemblyRef)) != null)
			{
				result = assemblyRef;
			}
			else if ((fileDef = (implementation as FileDef)) != null)
			{
				ModuleDef ownerModule = this.GetOwnerModule(exportedType);
				ModuleRefUser moduleRefUser = new ModuleRefUser(ownerModule, fileDef.Name);
				if (ownerModule != null)
				{
					ownerModule.UpdateRowId<ModuleRefUser>(moduleRefUser);
				}
				result = moduleRefUser;
			}
			else
			{
				result = null;
			}
			this.recursionCounter.Decrement();
			return result;
		}

		// Token: 0x060047D5 RID: 18389 RVA: 0x00176320 File Offset: 0x00176320
		private ModuleDef GetOwnerModule(ExportedType exportedType)
		{
			if (exportedType == null)
			{
				return null;
			}
			return exportedType.Module;
		}

		// Token: 0x060047D6 RID: 18390 RVA: 0x00176330 File Offset: 0x00176330
		private void CreateFieldFullName(string declaringType, string name, FieldSig fieldSig)
		{
			this.CreateFullName((fieldSig != null) ? fieldSig.Type : null);
			this.sb.Append(' ');
			if (declaringType != null)
			{
				this.sb.Append(declaringType);
				this.sb.Append("::");
			}
			if (name != null)
			{
				this.sb.Append(name);
			}
		}

		// Token: 0x060047D7 RID: 18391 RVA: 0x001763A0 File Offset: 0x001763A0
		private void CreateMethodFullName(string declaringType, string name, MethodBaseSig methodSig, MethodDef gppMethod)
		{
			if (methodSig == null)
			{
				this.sb.Append("<<<NULL>>>");
				return;
			}
			this.CreateFullName(methodSig.RetType);
			this.sb.Append(' ');
			if (declaringType != null)
			{
				this.sb.Append(declaringType);
				this.sb.Append("::");
			}
			if (name != null)
			{
				this.sb.Append(name);
			}
			if (methodSig.Generic)
			{
				this.sb.Append('<');
				uint num = methodSig.GenParamCount;
				if (num > 200U)
				{
					num = 200U;
				}
				for (uint num2 = 0U; num2 < num; num2 += 1U)
				{
					if (num2 != 0U)
					{
						this.sb.Append(',');
					}
					this.CreateFullName(new GenericMVar(num2, gppMethod));
				}
				this.sb.Append('>');
			}
			this.sb.Append('(');
			int num3 = this.PrintMethodArgList(methodSig.Params, false, false);
			this.PrintMethodArgList(methodSig.ParamsAfterSentinel, num3 > 0, true);
			this.sb.Append(')');
		}

		// Token: 0x060047D8 RID: 18392 RVA: 0x001764C8 File Offset: 0x001764C8
		private int PrintMethodArgList(IList<TypeSig> args, bool hasPrintedArgs, bool isAfterSentinel)
		{
			if (args == null)
			{
				return 0;
			}
			if (isAfterSentinel)
			{
				if (hasPrintedArgs)
				{
					this.sb.Append(',');
				}
				this.sb.Append("...");
				hasPrintedArgs = true;
			}
			int num = 0;
			int count = args.Count;
			for (int i = 0; i < count; i++)
			{
				TypeSig typeSig = args[i];
				num++;
				if (hasPrintedArgs)
				{
					this.sb.Append(',');
				}
				this.CreateFullName(typeSig);
				hasPrintedArgs = true;
			}
			return num;
		}

		// Token: 0x060047D9 RID: 18393 RVA: 0x00176554 File Offset: 0x00176554
		private void CreatePropertyFullName(string declaringType, UTF8String name, CallingConventionSig propertySig)
		{
			this.CreateMethodFullName(declaringType, UTF8String.ToSystemString(name), propertySig as MethodBaseSig, null);
		}

		// Token: 0x060047DA RID: 18394 RVA: 0x0017656C File Offset: 0x0017656C
		private void CreateEventFullName(string declaringType, UTF8String name, ITypeDefOrRef typeDefOrRef)
		{
			this.CreateFullName(typeDefOrRef);
			this.sb.Append(' ');
			if (declaringType != null)
			{
				this.sb.Append(declaringType);
				this.sb.Append("::");
			}
			if (!UTF8String.IsNull(name))
			{
				this.sb.Append(UTF8String.ToSystemString(name));
			}
		}

		// Token: 0x060047DB RID: 18395 RVA: 0x001765D4 File Offset: 0x001765D4
		public override string ToString()
		{
			return this.Result;
		}

		// Token: 0x040024DF RID: 9439
		private const uint MaxArrayRank = 100U;

		// Token: 0x040024E0 RID: 9440
		private const uint MaxMethodGenParamCount = 200U;

		// Token: 0x040024E1 RID: 9441
		private const string RECURSION_ERROR_RESULT_STRING = "<<<INFRECURSION>>>";

		// Token: 0x040024E2 RID: 9442
		private const string NULLVALUE = "<<<NULL>>>";

		// Token: 0x040024E3 RID: 9443
		private readonly StringBuilder sb;

		// Token: 0x040024E4 RID: 9444
		private readonly bool isReflection;

		// Token: 0x040024E5 RID: 9445
		private readonly IFullNameFactoryHelper helper;

		// Token: 0x040024E6 RID: 9446
		private GenericArguments genericArguments;

		// Token: 0x040024E7 RID: 9447
		private RecursionCounter recursionCounter;

		// Token: 0x040024E8 RID: 9448
		private const int TYPESIG_NAMESPACE = 1;

		// Token: 0x040024E9 RID: 9449
		private const int TYPESIG_NAME = 2;
	}
}
