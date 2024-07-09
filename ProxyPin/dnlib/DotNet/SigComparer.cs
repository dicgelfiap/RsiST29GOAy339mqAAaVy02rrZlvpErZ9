using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x0200084C RID: 2124
	[ComVisible(true)]
	public struct SigComparer
	{
		// Token: 0x17000FEB RID: 4075
		// (get) Token: 0x06004F43 RID: 20291 RVA: 0x00188270 File Offset: 0x00188270
		private bool DontCompareTypeScope
		{
			get
			{
				return (this.options & SigComparerOptions.DontCompareTypeScope) > (SigComparerOptions)0U;
			}
		}

		// Token: 0x17000FEC RID: 4076
		// (get) Token: 0x06004F44 RID: 20292 RVA: 0x00188280 File Offset: 0x00188280
		private bool CompareMethodFieldDeclaringType
		{
			get
			{
				return (this.options & SigComparerOptions.CompareMethodFieldDeclaringType) > (SigComparerOptions)0U;
			}
		}

		// Token: 0x17000FED RID: 4077
		// (get) Token: 0x06004F45 RID: 20293 RVA: 0x00188290 File Offset: 0x00188290
		private bool ComparePropertyDeclaringType
		{
			get
			{
				return (this.options & SigComparerOptions.ComparePropertyDeclaringType) > (SigComparerOptions)0U;
			}
		}

		// Token: 0x17000FEE RID: 4078
		// (get) Token: 0x06004F46 RID: 20294 RVA: 0x001882A0 File Offset: 0x001882A0
		private bool CompareEventDeclaringType
		{
			get
			{
				return (this.options & SigComparerOptions.CompareEventDeclaringType) > (SigComparerOptions)0U;
			}
		}

		// Token: 0x17000FEF RID: 4079
		// (get) Token: 0x06004F47 RID: 20295 RVA: 0x001882B0 File Offset: 0x001882B0
		private bool CompareSentinelParams
		{
			get
			{
				return (this.options & SigComparerOptions.CompareSentinelParams) > (SigComparerOptions)0U;
			}
		}

		// Token: 0x17000FF0 RID: 4080
		// (get) Token: 0x06004F48 RID: 20296 RVA: 0x001882C0 File Offset: 0x001882C0
		private bool CompareAssemblyPublicKeyToken
		{
			get
			{
				return (this.options & SigComparerOptions.CompareAssemblyPublicKeyToken) > (SigComparerOptions)0U;
			}
		}

		// Token: 0x17000FF1 RID: 4081
		// (get) Token: 0x06004F49 RID: 20297 RVA: 0x001882D0 File Offset: 0x001882D0
		private bool CompareAssemblyVersion
		{
			get
			{
				return (this.options & SigComparerOptions.CompareAssemblyVersion) > (SigComparerOptions)0U;
			}
		}

		// Token: 0x17000FF2 RID: 4082
		// (get) Token: 0x06004F4A RID: 20298 RVA: 0x001882E0 File Offset: 0x001882E0
		private bool CompareAssemblyLocale
		{
			get
			{
				return (this.options & SigComparerOptions.CompareAssemblyLocale) > (SigComparerOptions)0U;
			}
		}

		// Token: 0x17000FF3 RID: 4083
		// (get) Token: 0x06004F4B RID: 20299 RVA: 0x001882F4 File Offset: 0x001882F4
		private bool TypeRefCanReferenceGlobalType
		{
			get
			{
				return (this.options & SigComparerOptions.TypeRefCanReferenceGlobalType) > (SigComparerOptions)0U;
			}
		}

		// Token: 0x17000FF4 RID: 4084
		// (get) Token: 0x06004F4C RID: 20300 RVA: 0x00188308 File Offset: 0x00188308
		private bool DontCompareReturnType
		{
			get
			{
				return (this.options & SigComparerOptions.DontCompareReturnType) > (SigComparerOptions)0U;
			}
		}

		// Token: 0x17000FF5 RID: 4085
		// (get) Token: 0x06004F4D RID: 20301 RVA: 0x0018831C File Offset: 0x0018831C
		private bool SubstituteGenericParameters
		{
			get
			{
				return (this.options & (SigComparerOptions)1024U) > (SigComparerOptions)0U;
			}
		}

		// Token: 0x17000FF6 RID: 4086
		// (get) Token: 0x06004F4E RID: 20302 RVA: 0x00188330 File Offset: 0x00188330
		private bool CaseInsensitiveTypeNamespaces
		{
			get
			{
				return (this.options & SigComparerOptions.CaseInsensitiveTypeNamespaces) > (SigComparerOptions)0U;
			}
		}

		// Token: 0x17000FF7 RID: 4087
		// (get) Token: 0x06004F4F RID: 20303 RVA: 0x00188344 File Offset: 0x00188344
		private bool CaseInsensitiveTypeNames
		{
			get
			{
				return (this.options & SigComparerOptions.CaseInsensitiveTypeNames) > (SigComparerOptions)0U;
			}
		}

		// Token: 0x17000FF8 RID: 4088
		// (get) Token: 0x06004F50 RID: 20304 RVA: 0x00188358 File Offset: 0x00188358
		private bool CaseInsensitiveMethodFieldNames
		{
			get
			{
				return (this.options & SigComparerOptions.CaseInsensitiveMethodFieldNames) > (SigComparerOptions)0U;
			}
		}

		// Token: 0x17000FF9 RID: 4089
		// (get) Token: 0x06004F51 RID: 20305 RVA: 0x0018836C File Offset: 0x0018836C
		private bool CaseInsensitivePropertyNames
		{
			get
			{
				return (this.options & SigComparerOptions.CaseInsensitivePropertyNames) > (SigComparerOptions)0U;
			}
		}

		// Token: 0x17000FFA RID: 4090
		// (get) Token: 0x06004F52 RID: 20306 RVA: 0x00188380 File Offset: 0x00188380
		private bool CaseInsensitiveEventNames
		{
			get
			{
				return (this.options & SigComparerOptions.CaseInsensitiveEventNames) > (SigComparerOptions)0U;
			}
		}

		// Token: 0x17000FFB RID: 4091
		// (get) Token: 0x06004F53 RID: 20307 RVA: 0x00188394 File Offset: 0x00188394
		private bool PrivateScopeFieldIsComparable
		{
			get
			{
				return (this.options & SigComparerOptions.PrivateScopeFieldIsComparable) > (SigComparerOptions)0U;
			}
		}

		// Token: 0x17000FFC RID: 4092
		// (get) Token: 0x06004F54 RID: 20308 RVA: 0x001883A8 File Offset: 0x001883A8
		private bool PrivateScopeMethodIsComparable
		{
			get
			{
				return (this.options & SigComparerOptions.PrivateScopeMethodIsComparable) > (SigComparerOptions)0U;
			}
		}

		// Token: 0x17000FFD RID: 4093
		// (get) Token: 0x06004F55 RID: 20309 RVA: 0x001883BC File Offset: 0x001883BC
		private bool RawSignatureCompare
		{
			get
			{
				return (this.options & SigComparerOptions.RawSignatureCompare) > (SigComparerOptions)0U;
			}
		}

		// Token: 0x17000FFE RID: 4094
		// (get) Token: 0x06004F56 RID: 20310 RVA: 0x001883D0 File Offset: 0x001883D0
		private bool IgnoreModifiers
		{
			get
			{
				return (this.options & SigComparerOptions.IgnoreModifiers) > (SigComparerOptions)0U;
			}
		}

		// Token: 0x17000FFF RID: 4095
		// (get) Token: 0x06004F57 RID: 20311 RVA: 0x001883E4 File Offset: 0x001883E4
		private bool MscorlibIsNotSpecial
		{
			get
			{
				return (this.options & SigComparerOptions.MscorlibIsNotSpecial) > (SigComparerOptions)0U;
			}
		}

		// Token: 0x17001000 RID: 4096
		// (get) Token: 0x06004F58 RID: 20312 RVA: 0x001883F8 File Offset: 0x001883F8
		private bool DontProjectWinMDRefs
		{
			get
			{
				return (this.options & SigComparerOptions.DontProjectWinMDRefs) > (SigComparerOptions)0U;
			}
		}

		// Token: 0x17001001 RID: 4097
		// (get) Token: 0x06004F59 RID: 20313 RVA: 0x0018840C File Offset: 0x0018840C
		private bool DontCheckTypeEquivalence
		{
			get
			{
				return (this.options & SigComparerOptions.DontCheckTypeEquivalence) > (SigComparerOptions)0U;
			}
		}

		// Token: 0x17001002 RID: 4098
		// (get) Token: 0x06004F5A RID: 20314 RVA: 0x00188420 File Offset: 0x00188420
		private bool IgnoreMultiDimensionalArrayLowerBoundsAndSizes
		{
			get
			{
				return (this.options & SigComparerOptions.IgnoreMultiDimensionalArrayLowerBoundsAndSizes) > (SigComparerOptions)0U;
			}
		}

		// Token: 0x06004F5B RID: 20315 RVA: 0x00188434 File Offset: 0x00188434
		public SigComparer(SigComparerOptions options)
		{
			this = new SigComparer(options, null);
		}

		// Token: 0x06004F5C RID: 20316 RVA: 0x00188440 File Offset: 0x00188440
		public SigComparer(SigComparerOptions options, ModuleDef sourceModule)
		{
			this.recursionCounter = default(RecursionCounter);
			this.options = options;
			this.genericArguments = null;
			this.sourceModule = sourceModule;
		}

		// Token: 0x06004F5D RID: 20317 RVA: 0x00188464 File Offset: 0x00188464
		private int GetHashCode_FnPtr_SystemIntPtr()
		{
			return this.GetHashCode_TypeNamespace("System") + this.GetHashCode_TypeName("IntPtr");
		}

		// Token: 0x06004F5E RID: 20318 RVA: 0x00188480 File Offset: 0x00188480
		private bool Equals_Names(bool caseInsensitive, UTF8String a, UTF8String b)
		{
			if (caseInsensitive)
			{
				return UTF8String.ToSystemStringOrEmpty(a).Equals(UTF8String.ToSystemStringOrEmpty(b), StringComparison.OrdinalIgnoreCase);
			}
			return UTF8String.Equals(a, b);
		}

		// Token: 0x06004F5F RID: 20319 RVA: 0x001884A4 File Offset: 0x001884A4
		private bool Equals_Names(bool caseInsensitive, string a, string b)
		{
			if (caseInsensitive)
			{
				return (a ?? string.Empty).Equals(b ?? string.Empty, StringComparison.OrdinalIgnoreCase);
			}
			return (a ?? string.Empty) == (b ?? string.Empty);
		}

		// Token: 0x06004F60 RID: 20320 RVA: 0x001884FC File Offset: 0x001884FC
		private int GetHashCode_Name(bool caseInsensitive, string a)
		{
			if (caseInsensitive)
			{
				return (a ?? string.Empty).ToUpperInvariant().GetHashCode();
			}
			return (a ?? string.Empty).GetHashCode();
		}

		// Token: 0x06004F61 RID: 20321 RVA: 0x00188530 File Offset: 0x00188530
		private bool Equals_TypeNamespaces(UTF8String a, UTF8String b)
		{
			return this.Equals_Names(this.CaseInsensitiveTypeNamespaces, a, b);
		}

		// Token: 0x06004F62 RID: 20322 RVA: 0x00188540 File Offset: 0x00188540
		private bool Equals_TypeNamespaces(UTF8String a, string b)
		{
			return this.Equals_Names(this.CaseInsensitiveTypeNamespaces, UTF8String.ToSystemStringOrEmpty(a), b);
		}

		// Token: 0x06004F63 RID: 20323 RVA: 0x00188558 File Offset: 0x00188558
		private int GetHashCode_TypeNamespace(UTF8String a)
		{
			return this.GetHashCode_Name(this.CaseInsensitiveTypeNamespaces, UTF8String.ToSystemStringOrEmpty(a));
		}

		// Token: 0x06004F64 RID: 20324 RVA: 0x0018856C File Offset: 0x0018856C
		private int GetHashCode_TypeNamespace(string a)
		{
			return this.GetHashCode_Name(this.CaseInsensitiveTypeNamespaces, a);
		}

		// Token: 0x06004F65 RID: 20325 RVA: 0x0018857C File Offset: 0x0018857C
		private bool Equals_TypeNames(UTF8String a, UTF8String b)
		{
			return this.Equals_Names(this.CaseInsensitiveTypeNames, a, b);
		}

		// Token: 0x06004F66 RID: 20326 RVA: 0x0018858C File Offset: 0x0018858C
		private bool Equals_TypeNames(UTF8String a, string b)
		{
			return this.Equals_Names(this.CaseInsensitiveTypeNames, UTF8String.ToSystemStringOrEmpty(a), b);
		}

		// Token: 0x06004F67 RID: 20327 RVA: 0x001885A4 File Offset: 0x001885A4
		private int GetHashCode_TypeName(UTF8String a)
		{
			return this.GetHashCode_Name(this.CaseInsensitiveTypeNames, UTF8String.ToSystemStringOrEmpty(a));
		}

		// Token: 0x06004F68 RID: 20328 RVA: 0x001885B8 File Offset: 0x001885B8
		private int GetHashCode_TypeName(string a)
		{
			return this.GetHashCode_Name(this.CaseInsensitiveTypeNames, a);
		}

		// Token: 0x06004F69 RID: 20329 RVA: 0x001885C8 File Offset: 0x001885C8
		private bool Equals_MethodFieldNames(UTF8String a, UTF8String b)
		{
			return this.Equals_Names(this.CaseInsensitiveMethodFieldNames, a, b);
		}

		// Token: 0x06004F6A RID: 20330 RVA: 0x001885D8 File Offset: 0x001885D8
		private bool Equals_MethodFieldNames(UTF8String a, string b)
		{
			return this.Equals_Names(this.CaseInsensitiveMethodFieldNames, UTF8String.ToSystemStringOrEmpty(a), b);
		}

		// Token: 0x06004F6B RID: 20331 RVA: 0x001885F0 File Offset: 0x001885F0
		private int GetHashCode_MethodFieldName(UTF8String a)
		{
			return this.GetHashCode_Name(this.CaseInsensitiveMethodFieldNames, UTF8String.ToSystemStringOrEmpty(a));
		}

		// Token: 0x06004F6C RID: 20332 RVA: 0x00188604 File Offset: 0x00188604
		private int GetHashCode_MethodFieldName(string a)
		{
			return this.GetHashCode_Name(this.CaseInsensitiveMethodFieldNames, a);
		}

		// Token: 0x06004F6D RID: 20333 RVA: 0x00188614 File Offset: 0x00188614
		private bool Equals_PropertyNames(UTF8String a, UTF8String b)
		{
			return this.Equals_Names(this.CaseInsensitivePropertyNames, a, b);
		}

		// Token: 0x06004F6E RID: 20334 RVA: 0x00188624 File Offset: 0x00188624
		private bool Equals_PropertyNames(UTF8String a, string b)
		{
			return this.Equals_Names(this.CaseInsensitivePropertyNames, UTF8String.ToSystemStringOrEmpty(a), b);
		}

		// Token: 0x06004F6F RID: 20335 RVA: 0x0018863C File Offset: 0x0018863C
		private int GetHashCode_PropertyName(UTF8String a)
		{
			return this.GetHashCode_Name(this.CaseInsensitivePropertyNames, UTF8String.ToSystemStringOrEmpty(a));
		}

		// Token: 0x06004F70 RID: 20336 RVA: 0x00188650 File Offset: 0x00188650
		private int GetHashCode_PropertyName(string a)
		{
			return this.GetHashCode_Name(this.CaseInsensitivePropertyNames, a);
		}

		// Token: 0x06004F71 RID: 20337 RVA: 0x00188660 File Offset: 0x00188660
		private bool Equals_EventNames(UTF8String a, UTF8String b)
		{
			return this.Equals_Names(this.CaseInsensitiveEventNames, a, b);
		}

		// Token: 0x06004F72 RID: 20338 RVA: 0x00188670 File Offset: 0x00188670
		private bool Equals_EventNames(UTF8String a, string b)
		{
			return this.Equals_Names(this.CaseInsensitiveEventNames, UTF8String.ToSystemStringOrEmpty(a), b);
		}

		// Token: 0x06004F73 RID: 20339 RVA: 0x00188688 File Offset: 0x00188688
		private int GetHashCode_EventName(UTF8String a)
		{
			return this.GetHashCode_Name(this.CaseInsensitiveEventNames, UTF8String.ToSystemStringOrEmpty(a));
		}

		// Token: 0x06004F74 RID: 20340 RVA: 0x0018869C File Offset: 0x0018869C
		private int GetHashCode_EventName(string a)
		{
			return this.GetHashCode_Name(this.CaseInsensitiveEventNames, a);
		}

		// Token: 0x06004F75 RID: 20341 RVA: 0x001886AC File Offset: 0x001886AC
		private SigComparerOptions ClearOptions(SigComparerOptions flags)
		{
			SigComparerOptions result = this.options;
			this.options &= ~flags;
			return result;
		}

		// Token: 0x06004F76 RID: 20342 RVA: 0x001886C4 File Offset: 0x001886C4
		private SigComparerOptions SetOptions(SigComparerOptions flags)
		{
			SigComparerOptions result = this.options;
			this.options |= flags;
			return result;
		}

		// Token: 0x06004F77 RID: 20343 RVA: 0x001886DC File Offset: 0x001886DC
		private void RestoreOptions(SigComparerOptions oldFlags)
		{
			this.options = oldFlags;
		}

		// Token: 0x06004F78 RID: 20344 RVA: 0x001886E8 File Offset: 0x001886E8
		private void InitializeGenericArguments()
		{
			if (this.genericArguments == null)
			{
				this.genericArguments = new GenericArguments();
			}
		}

		// Token: 0x06004F79 RID: 20345 RVA: 0x00188700 File Offset: 0x00188700
		private static GenericInstSig GetGenericInstanceType(IMemberRefParent parent)
		{
			TypeSpec typeSpec = parent as TypeSpec;
			if (typeSpec == null)
			{
				return null;
			}
			return typeSpec.TypeSig.RemoveModifiers() as GenericInstSig;
		}

		// Token: 0x06004F7A RID: 20346 RVA: 0x00188730 File Offset: 0x00188730
		private bool Equals(IAssembly aAsm, IAssembly bAsm, TypeRef b)
		{
			if (this.Equals(aAsm, bAsm))
			{
				return true;
			}
			TypeDef typeDef = b.Resolve(this.sourceModule);
			return typeDef != null && this.Equals(aAsm, typeDef.Module.Assembly);
		}

		// Token: 0x06004F7B RID: 20347 RVA: 0x00188778 File Offset: 0x00188778
		private bool Equals(IAssembly aAsm, IAssembly bAsm, ExportedType b)
		{
			if (this.Equals(aAsm, bAsm))
			{
				return true;
			}
			TypeDef typeDef = b.Resolve();
			return typeDef != null && this.Equals(aAsm, typeDef.Module.Assembly);
		}

		// Token: 0x06004F7C RID: 20348 RVA: 0x001887BC File Offset: 0x001887BC
		private bool Equals(IAssembly aAsm, TypeRef a, IAssembly bAsm, TypeRef b)
		{
			if (this.Equals(aAsm, bAsm))
			{
				return true;
			}
			TypeDef typeDef = a.Resolve(this.sourceModule);
			TypeDef typeDef2 = b.Resolve(this.sourceModule);
			return typeDef != null && typeDef2 != null && this.Equals(typeDef.Module.Assembly, typeDef2.Module.Assembly);
		}

		// Token: 0x06004F7D RID: 20349 RVA: 0x00188824 File Offset: 0x00188824
		private bool Equals(IAssembly aAsm, ExportedType a, IAssembly bAsm, ExportedType b)
		{
			if (this.Equals(aAsm, bAsm))
			{
				return true;
			}
			TypeDef typeDef = a.Resolve();
			TypeDef typeDef2 = b.Resolve();
			return typeDef != null && typeDef2 != null && this.Equals(typeDef.Module.Assembly, typeDef2.Module.Assembly);
		}

		// Token: 0x06004F7E RID: 20350 RVA: 0x00188880 File Offset: 0x00188880
		private bool Equals(IAssembly aAsm, TypeRef a, IAssembly bAsm, ExportedType b)
		{
			if (this.Equals(aAsm, bAsm))
			{
				return true;
			}
			TypeDef typeDef = a.Resolve(this.sourceModule);
			TypeDef typeDef2 = b.Resolve();
			return typeDef != null && typeDef2 != null && this.Equals(typeDef.Module.Assembly, typeDef2.Module.Assembly);
		}

		// Token: 0x06004F7F RID: 20351 RVA: 0x001888E0 File Offset: 0x001888E0
		private bool Equals(TypeDef a, IModule bMod, TypeRef b)
		{
			if (this.Equals(a.Module, bMod) && this.Equals(a.DefinitionAssembly, b.DefinitionAssembly))
			{
				return true;
			}
			TypeDef typeDef = b.Resolve(this.sourceModule);
			return typeDef != null && ((!this.DontCheckTypeEquivalence && TIAHelper.Equivalent(a, typeDef)) || (this.Equals(a.Module, typeDef.Module) && this.Equals(a.DefinitionAssembly, typeDef.DefinitionAssembly)));
		}

		// Token: 0x06004F80 RID: 20352 RVA: 0x00188978 File Offset: 0x00188978
		private bool Equals(TypeDef a, FileDef bFile, ExportedType b)
		{
			if (this.Equals(a.Module, bFile) && this.Equals(a.DefinitionAssembly, b.DefinitionAssembly))
			{
				return true;
			}
			TypeDef typeDef = b.Resolve();
			return typeDef != null && this.Equals(a.Module, typeDef.Module) && this.Equals(a.DefinitionAssembly, typeDef.DefinitionAssembly);
		}

		// Token: 0x06004F81 RID: 20353 RVA: 0x001889EC File Offset: 0x001889EC
		private bool TypeDefScopeEquals(TypeDef a, TypeDef b)
		{
			return a != null && b != null && ((!this.DontCheckTypeEquivalence && TIAHelper.Equivalent(a, b)) || this.Equals(a.Module, b.Module));
		}

		// Token: 0x06004F82 RID: 20354 RVA: 0x00188A38 File Offset: 0x00188A38
		private bool Equals(TypeRef a, IModule ma, TypeRef b, IModule mb)
		{
			if (this.Equals(ma, mb) && this.Equals(a.DefinitionAssembly, b.DefinitionAssembly))
			{
				return true;
			}
			TypeDef typeDef = a.Resolve(this.sourceModule);
			TypeDef typeDef2 = b.Resolve(this.sourceModule);
			return typeDef != null && typeDef2 != null && this.Equals(typeDef.Module, typeDef2.Module) && this.Equals(typeDef.DefinitionAssembly, typeDef2.DefinitionAssembly);
		}

		// Token: 0x06004F83 RID: 20355 RVA: 0x00188AC4 File Offset: 0x00188AC4
		private bool Equals(TypeRef a, IModule ma, ExportedType b, FileDef fb)
		{
			if (this.Equals(ma, fb) && this.Equals(a.DefinitionAssembly, b.DefinitionAssembly))
			{
				return true;
			}
			TypeDef typeDef = a.Resolve(this.sourceModule);
			TypeDef typeDef2 = b.Resolve();
			return typeDef != null && typeDef2 != null && this.Equals(typeDef.Module, typeDef2.Module) && this.Equals(typeDef.DefinitionAssembly, typeDef2.DefinitionAssembly);
		}

		// Token: 0x06004F84 RID: 20356 RVA: 0x00188B48 File Offset: 0x00188B48
		private bool Equals(Assembly aAsm, IAssembly bAsm, TypeRef b)
		{
			if (this.Equals(bAsm, aAsm))
			{
				return true;
			}
			TypeDef typeDef = b.Resolve(this.sourceModule);
			return typeDef != null && this.Equals(typeDef.Module.Assembly, aAsm);
		}

		// Token: 0x06004F85 RID: 20357 RVA: 0x00188B90 File Offset: 0x00188B90
		private bool Equals(Assembly aAsm, IAssembly bAsm, ExportedType b)
		{
			if (this.Equals(bAsm, aAsm))
			{
				return true;
			}
			TypeDef typeDef = b.Resolve();
			return typeDef != null && this.Equals(typeDef.Module.Assembly, aAsm);
		}

		// Token: 0x06004F86 RID: 20358 RVA: 0x00188BD4 File Offset: 0x00188BD4
		private bool Equals(Type a, IModule bMod, TypeRef b)
		{
			if (this.Equals(bMod, a.Module) && this.Equals(b.DefinitionAssembly, a.Assembly))
			{
				return true;
			}
			TypeDef typeDef = b.Resolve(this.sourceModule);
			return typeDef != null && this.Equals(typeDef.Module, a.Module) && this.Equals(typeDef.DefinitionAssembly, a.Assembly);
		}

		// Token: 0x06004F87 RID: 20359 RVA: 0x00188C50 File Offset: 0x00188C50
		private bool Equals(Type a, FileDef bFile, ExportedType b)
		{
			if (this.Equals(bFile, a.Module) && this.Equals(b.DefinitionAssembly, a.Assembly))
			{
				return true;
			}
			TypeDef typeDef = b.Resolve();
			return typeDef != null && this.Equals(typeDef.Module, a.Module) && this.Equals(typeDef.DefinitionAssembly, a.Assembly);
		}

		// Token: 0x06004F88 RID: 20360 RVA: 0x00188CC4 File Offset: 0x00188CC4
		public bool Equals(IMemberRef a, IMemberRef b)
		{
			if (a == b)
			{
				return true;
			}
			if (a == null || b == null)
			{
				return false;
			}
			if (!this.recursionCounter.Increment())
			{
				return false;
			}
			IType a2;
			IType b2;
			bool result;
			IField field;
			IField field2;
			IMethod a3;
			IMethod b3;
			PropertyDef a4;
			PropertyDef b4;
			if ((a2 = (a as IType)) != null && (b2 = (b as IType)) != null)
			{
				result = this.Equals(a2, b2);
			}
			else if ((field = (a as IField)) != null && (field2 = (b as IField)) != null && field.IsField && field2.IsField)
			{
				result = this.Equals(field, field2);
			}
			else if ((a3 = (a as IMethod)) != null && (b3 = (b as IMethod)) != null)
			{
				result = this.Equals(a3, b3);
			}
			else if ((a4 = (a as PropertyDef)) != null && (b4 = (b as PropertyDef)) != null)
			{
				result = this.Equals(a4, b4);
			}
			else
			{
				EventDef a5;
				EventDef b5;
				result = ((a5 = (a as EventDef)) != null && (b5 = (b as EventDef)) != null && this.Equals(a5, b5));
			}
			this.recursionCounter.Decrement();
			return result;
		}

		// Token: 0x06004F89 RID: 20361 RVA: 0x00188DFC File Offset: 0x00188DFC
		public int GetHashCode(IMemberRef a)
		{
			if (a == null)
			{
				return 0;
			}
			if (!this.recursionCounter.Increment())
			{
				return 0;
			}
			IType a2;
			int result;
			IField a3;
			IMethod a4;
			PropertyDef a5;
			EventDef a6;
			if ((a2 = (a as IType)) != null)
			{
				result = this.GetHashCode(a2);
			}
			else if ((a3 = (a as IField)) != null)
			{
				result = this.GetHashCode(a3);
			}
			else if ((a4 = (a as IMethod)) != null)
			{
				result = this.GetHashCode(a4);
			}
			else if ((a5 = (a as PropertyDef)) != null)
			{
				result = this.GetHashCode(a5);
			}
			else if ((a6 = (a as EventDef)) != null)
			{
				result = this.GetHashCode(a6);
			}
			else
			{
				result = 0;
			}
			this.recursionCounter.Decrement();
			return result;
		}

		// Token: 0x06004F8A RID: 20362 RVA: 0x00188EBC File Offset: 0x00188EBC
		public bool Equals(ITypeDefOrRef a, ITypeDefOrRef b)
		{
			return this.Equals(a, b);
		}

		// Token: 0x06004F8B RID: 20363 RVA: 0x00188EC8 File Offset: 0x00188EC8
		public int GetHashCode(ITypeDefOrRef a)
		{
			return this.GetHashCode(a);
		}

		// Token: 0x06004F8C RID: 20364 RVA: 0x00188ED4 File Offset: 0x00188ED4
		public bool Equals(IType a, IType b)
		{
			if (a == b)
			{
				return true;
			}
			if (a == null || b == null)
			{
				return false;
			}
			if (!this.recursionCounter.Increment())
			{
				return false;
			}
			TypeDef typeDef;
			TypeDef typeDef2;
			bool result;
			TypeRef typeRef;
			TypeRef typeRef2;
			TypeSpec typeSpec;
			TypeSpec typeSpec2;
			TypeSig typeSig;
			TypeSig typeSig2;
			ExportedType exportedType;
			ExportedType exportedType2;
			if ((typeDef = (a as TypeDef)) != null & (typeDef2 = (b as TypeDef)) != null)
			{
				result = this.Equals(typeDef, typeDef2);
			}
			else if ((typeRef = (a as TypeRef)) != null & (typeRef2 = (b as TypeRef)) != null)
			{
				result = this.Equals(typeRef, typeRef2);
			}
			else if ((typeSpec = (a as TypeSpec)) != null & (typeSpec2 = (b as TypeSpec)) != null)
			{
				result = this.Equals(typeSpec, typeSpec2);
			}
			else if ((typeSig = (a as TypeSig)) != null & (typeSig2 = (b as TypeSig)) != null)
			{
				result = this.Equals(typeSig, typeSig2);
			}
			else if ((exportedType = (a as ExportedType)) != null & (exportedType2 = (b as ExportedType)) != null)
			{
				result = this.Equals(exportedType, exportedType2);
			}
			else if (typeDef != null && typeRef2 != null)
			{
				result = this.Equals(typeDef, typeRef2);
			}
			else if (typeRef != null && typeDef2 != null)
			{
				result = this.Equals(typeDef2, typeRef);
			}
			else if (typeDef != null && typeSpec2 != null)
			{
				result = this.Equals(typeDef, typeSpec2);
			}
			else if (typeSpec != null && typeDef2 != null)
			{
				result = this.Equals(typeDef2, typeSpec);
			}
			else if (typeDef != null && typeSig2 != null)
			{
				result = this.Equals(typeDef, typeSig2);
			}
			else if (typeSig != null && typeDef2 != null)
			{
				result = this.Equals(typeDef2, typeSig);
			}
			else if (typeDef != null && exportedType2 != null)
			{
				result = this.Equals(typeDef, exportedType2);
			}
			else if (exportedType != null && typeDef2 != null)
			{
				result = this.Equals(typeDef2, exportedType);
			}
			else if (typeRef != null && typeSpec2 != null)
			{
				result = this.Equals(typeRef, typeSpec2);
			}
			else if (typeSpec != null && typeRef2 != null)
			{
				result = this.Equals(typeRef2, typeSpec);
			}
			else if (typeRef != null && typeSig2 != null)
			{
				result = this.Equals(typeRef, typeSig2);
			}
			else if (typeSig != null && typeRef2 != null)
			{
				result = this.Equals(typeRef2, typeSig);
			}
			else if (typeRef != null && exportedType2 != null)
			{
				result = this.Equals(typeRef, exportedType2);
			}
			else if (exportedType != null && typeRef2 != null)
			{
				result = this.Equals(typeRef2, exportedType);
			}
			else if (typeSpec != null && typeSig2 != null)
			{
				result = this.Equals(typeSpec, typeSig2);
			}
			else if (typeSig != null && typeSpec2 != null)
			{
				result = this.Equals(typeSpec2, typeSig);
			}
			else if (typeSpec != null && exportedType2 != null)
			{
				result = this.Equals(typeSpec, exportedType2);
			}
			else if (exportedType != null && typeSpec2 != null)
			{
				result = this.Equals(typeSpec2, exportedType);
			}
			else if (typeSig != null && exportedType2 != null)
			{
				result = this.Equals(typeSig, exportedType2);
			}
			else
			{
				result = (exportedType != null && typeSig2 != null && this.Equals(typeSig2, exportedType));
			}
			this.recursionCounter.Decrement();
			return result;
		}

		// Token: 0x06004F8D RID: 20365 RVA: 0x0018925C File Offset: 0x0018925C
		public int GetHashCode(IType a)
		{
			if (a == null)
			{
				return 0;
			}
			if (!this.recursionCounter.Increment())
			{
				return 0;
			}
			TypeDef a2;
			int result;
			TypeRef a3;
			TypeSpec a4;
			TypeSig a5;
			ExportedType a6;
			if ((a2 = (a as TypeDef)) != null)
			{
				result = this.GetHashCode(a2);
			}
			else if ((a3 = (a as TypeRef)) != null)
			{
				result = this.GetHashCode(a3);
			}
			else if ((a4 = (a as TypeSpec)) != null)
			{
				result = this.GetHashCode(a4);
			}
			else if ((a5 = (a as TypeSig)) != null)
			{
				result = this.GetHashCode(a5);
			}
			else if ((a6 = (a as ExportedType)) != null)
			{
				result = this.GetHashCode(a6);
			}
			else
			{
				result = 0;
			}
			this.recursionCounter.Decrement();
			return result;
		}

		// Token: 0x06004F8E RID: 20366 RVA: 0x0018931C File Offset: 0x0018931C
		public bool Equals(TypeRef a, TypeDef b)
		{
			return this.Equals(b, a);
		}

		// Token: 0x06004F8F RID: 20367 RVA: 0x00189328 File Offset: 0x00189328
		public bool Equals(TypeDef a, TypeRef b)
		{
			if (a == b)
			{
				return true;
			}
			if (a == null || b == null)
			{
				return false;
			}
			if (!this.recursionCounter.Increment())
			{
				return false;
			}
			bool flag;
			if (!this.DontProjectWinMDRefs)
			{
				TypeRef typeRef = WinMDHelpers.ToCLR(a.Module ?? this.sourceModule, a);
				b = (WinMDHelpers.ToCLR(b.Module ?? this.sourceModule, b) ?? b);
				if (typeRef != null)
				{
					flag = this.Equals(typeRef, b);
					goto IL_19F;
				}
			}
			IResolutionScope resolutionScope = b.ResolutionScope;
			TypeRef b2;
			IModule bMod;
			AssemblyRef bAsm;
			if (!this.Equals_TypeNames(a.Name, b.Name) || !this.Equals_TypeNamespaces(a.Namespace, b.Namespace))
			{
				flag = false;
			}
			else if ((b2 = (resolutionScope as TypeRef)) != null)
			{
				flag = this.Equals(a.DeclaringType, b2);
			}
			else if (a.DeclaringType != null)
			{
				flag = false;
			}
			else if (this.DontCompareTypeScope)
			{
				flag = true;
			}
			else if ((bMod = (resolutionScope as IModule)) != null)
			{
				flag = this.Equals(a, bMod, b);
			}
			else if ((bAsm = (resolutionScope as AssemblyRef)) != null)
			{
				ModuleDef module = a.Module;
				flag = (module != null && this.Equals(module.Assembly, bAsm, b));
				if (!flag && !this.DontCheckTypeEquivalence)
				{
					TypeDef b3 = b.Resolve();
					flag = this.TypeDefScopeEquals(a, b3);
				}
			}
			else
			{
				flag = false;
			}
			if (flag && !this.TypeRefCanReferenceGlobalType && a.IsGlobalModuleType)
			{
				flag = false;
			}
			IL_19F:
			this.recursionCounter.Decrement();
			return flag;
		}

		// Token: 0x06004F90 RID: 20368 RVA: 0x001894E4 File Offset: 0x001894E4
		public bool Equals(ExportedType a, TypeDef b)
		{
			return this.Equals(b, a);
		}

		// Token: 0x06004F91 RID: 20369 RVA: 0x001894F0 File Offset: 0x001894F0
		public bool Equals(TypeDef a, ExportedType b)
		{
			if (a == b)
			{
				return true;
			}
			if (a == null || b == null)
			{
				return false;
			}
			if (!this.recursionCounter.Increment())
			{
				return false;
			}
			bool flag;
			if (!this.DontProjectWinMDRefs)
			{
				TypeRef typeRef = WinMDHelpers.ToCLR(a.Module ?? this.sourceModule, a);
				b = (WinMDHelpers.ToCLR(b.Module ?? this.sourceModule, b) ?? b);
				if (typeRef != null)
				{
					flag = this.Equals(typeRef, b);
					goto IL_19F;
				}
			}
			IImplementation implementation = b.Implementation;
			ExportedType b2;
			if (!this.Equals_TypeNames(a.Name, b.TypeName) || !this.Equals_TypeNamespaces(a.Namespace, b.TypeNamespace))
			{
				flag = false;
			}
			else if ((b2 = (implementation as ExportedType)) != null)
			{
				flag = this.Equals(a.DeclaringType, b2);
			}
			else if (a.DeclaringType != null)
			{
				flag = false;
			}
			else if (this.DontCompareTypeScope)
			{
				flag = true;
			}
			else
			{
				FileDef bFile;
				AssemblyRef bAsm;
				if ((bFile = (implementation as FileDef)) != null)
				{
					flag = this.Equals(a, bFile, b);
				}
				else if ((bAsm = (implementation as AssemblyRef)) != null)
				{
					ModuleDef module = a.Module;
					flag = (module != null && this.Equals(module.Assembly, bAsm, b));
				}
				else
				{
					flag = false;
				}
				if (!flag && !this.DontCheckTypeEquivalence)
				{
					TypeDef b3 = b.Resolve();
					flag = this.TypeDefScopeEquals(a, b3);
				}
			}
			if (flag && !this.TypeRefCanReferenceGlobalType && a.IsGlobalModuleType)
			{
				flag = false;
			}
			IL_19F:
			this.recursionCounter.Decrement();
			return flag;
		}

		// Token: 0x06004F92 RID: 20370 RVA: 0x001896AC File Offset: 0x001896AC
		public bool Equals(TypeSpec a, TypeDef b)
		{
			return this.Equals(b, a);
		}

		// Token: 0x06004F93 RID: 20371 RVA: 0x001896B8 File Offset: 0x001896B8
		public bool Equals(TypeDef a, TypeSpec b)
		{
			return a == b || (a != null && b != null && this.Equals(a, b.TypeSig));
		}

		// Token: 0x06004F94 RID: 20372 RVA: 0x001896E0 File Offset: 0x001896E0
		public bool Equals(TypeSig a, TypeDef b)
		{
			return this.Equals(b, a);
		}

		// Token: 0x06004F95 RID: 20373 RVA: 0x001896EC File Offset: 0x001896EC
		public bool Equals(TypeDef a, TypeSig b)
		{
			if (a == b)
			{
				return true;
			}
			if (a == null || b == null)
			{
				return false;
			}
			if (!this.recursionCounter.Increment())
			{
				return false;
			}
			TypeDefOrRefSig typeDefOrRefSig = b as TypeDefOrRefSig;
			bool result;
			if (typeDefOrRefSig != null)
			{
				result = this.Equals(a, typeDefOrRefSig.TypeDefOrRef);
			}
			else
			{
				result = ((b is ModifierSig || b is PinnedSig) && this.Equals(a, b.Next));
			}
			this.recursionCounter.Decrement();
			return result;
		}

		// Token: 0x06004F96 RID: 20374 RVA: 0x00189780 File Offset: 0x00189780
		public bool Equals(TypeSpec a, TypeRef b)
		{
			return this.Equals(b, a);
		}

		// Token: 0x06004F97 RID: 20375 RVA: 0x0018978C File Offset: 0x0018978C
		public bool Equals(TypeRef a, TypeSpec b)
		{
			return a == b || (a != null && b != null && this.Equals(a, b.TypeSig));
		}

		// Token: 0x06004F98 RID: 20376 RVA: 0x001897B4 File Offset: 0x001897B4
		public bool Equals(ExportedType a, TypeRef b)
		{
			return this.Equals(b, a);
		}

		// Token: 0x06004F99 RID: 20377 RVA: 0x001897C0 File Offset: 0x001897C0
		public bool Equals(TypeRef a, ExportedType b)
		{
			if (a == b)
			{
				return true;
			}
			if (a == null || b == null)
			{
				return false;
			}
			if (!this.recursionCounter.Increment())
			{
				return false;
			}
			if (!this.DontProjectWinMDRefs)
			{
				a = (WinMDHelpers.ToCLR(a.Module ?? this.sourceModule, a) ?? a);
				b = (WinMDHelpers.ToCLR(b.Module ?? this.sourceModule, b) ?? b);
			}
			bool result = this.Equals_TypeNames(a.Name, b.TypeName) && this.Equals_TypeNamespaces(a.Namespace, b.TypeNamespace) && this.EqualsScope(a, b);
			this.recursionCounter.Decrement();
			return result;
		}

		// Token: 0x06004F9A RID: 20378 RVA: 0x00189894 File Offset: 0x00189894
		public bool Equals(TypeSig a, TypeRef b)
		{
			return this.Equals(b, a);
		}

		// Token: 0x06004F9B RID: 20379 RVA: 0x001898A0 File Offset: 0x001898A0
		public bool Equals(TypeRef a, TypeSig b)
		{
			if (a == b)
			{
				return true;
			}
			if (a == null || b == null)
			{
				return false;
			}
			if (!this.recursionCounter.Increment())
			{
				return false;
			}
			TypeDefOrRefSig typeDefOrRefSig = b as TypeDefOrRefSig;
			bool result;
			if (typeDefOrRefSig != null)
			{
				result = this.Equals(a, typeDefOrRefSig.TypeDefOrRef);
			}
			else
			{
				result = ((b is ModifierSig || b is PinnedSig) && this.Equals(a, b.Next));
			}
			this.recursionCounter.Decrement();
			return result;
		}

		// Token: 0x06004F9C RID: 20380 RVA: 0x00189934 File Offset: 0x00189934
		public bool Equals(TypeSig a, TypeSpec b)
		{
			return this.Equals(b, a);
		}

		// Token: 0x06004F9D RID: 20381 RVA: 0x00189940 File Offset: 0x00189940
		public bool Equals(TypeSpec a, TypeSig b)
		{
			return a == b || (a != null && b != null && this.Equals(a.TypeSig, b));
		}

		// Token: 0x06004F9E RID: 20382 RVA: 0x00189968 File Offset: 0x00189968
		public bool Equals(ExportedType a, TypeSpec b)
		{
			return this.Equals(b, a);
		}

		// Token: 0x06004F9F RID: 20383 RVA: 0x00189974 File Offset: 0x00189974
		public bool Equals(TypeSpec a, ExportedType b)
		{
			return a == b || (a != null && b != null && this.Equals(a.TypeSig, b));
		}

		// Token: 0x06004FA0 RID: 20384 RVA: 0x0018999C File Offset: 0x0018999C
		public bool Equals(ExportedType a, TypeSig b)
		{
			return this.Equals(b, a);
		}

		// Token: 0x06004FA1 RID: 20385 RVA: 0x001899A8 File Offset: 0x001899A8
		public bool Equals(TypeSig a, ExportedType b)
		{
			if (a == b)
			{
				return true;
			}
			if (a == null || b == null)
			{
				return false;
			}
			if (!this.recursionCounter.Increment())
			{
				return false;
			}
			TypeDefOrRefSig typeDefOrRefSig = a as TypeDefOrRefSig;
			bool result;
			if (typeDefOrRefSig != null)
			{
				result = this.Equals(typeDefOrRefSig.TypeDefOrRef, b);
			}
			else
			{
				result = ((a is ModifierSig || a is PinnedSig) && this.Equals(a.Next, b));
			}
			this.recursionCounter.Decrement();
			return result;
		}

		// Token: 0x06004FA2 RID: 20386 RVA: 0x00189A3C File Offset: 0x00189A3C
		private int GetHashCodeGlobalType()
		{
			return 1654396648;
		}

		// Token: 0x06004FA3 RID: 20387 RVA: 0x00189A44 File Offset: 0x00189A44
		public bool Equals(TypeRef a, TypeRef b)
		{
			if (a == b)
			{
				return true;
			}
			if (a == null || b == null)
			{
				return false;
			}
			if (!this.recursionCounter.Increment())
			{
				return false;
			}
			if (!this.DontProjectWinMDRefs)
			{
				a = (WinMDHelpers.ToCLR(a.Module ?? this.sourceModule, a) ?? a);
				b = (WinMDHelpers.ToCLR(b.Module ?? this.sourceModule, b) ?? b);
			}
			bool result = this.Equals_TypeNames(a.Name, b.Name) && this.Equals_TypeNamespaces(a.Namespace, b.Namespace) && this.EqualsResolutionScope(a, b);
			this.recursionCounter.Decrement();
			return result;
		}

		// Token: 0x06004FA4 RID: 20388 RVA: 0x00189B18 File Offset: 0x00189B18
		public int GetHashCode(TypeRef a)
		{
			if (a != null)
			{
				if (!this.DontProjectWinMDRefs)
				{
					a = (WinMDHelpers.ToCLR(a.Module ?? this.sourceModule, a) ?? a);
				}
				int num = this.GetHashCode_TypeName(a.Name);
				if (a.ResolutionScope is TypeRef)
				{
					num += -1049070942;
				}
				else
				{
					num += this.GetHashCode_TypeNamespace(a.Namespace);
				}
				return num;
			}
			if (!this.TypeRefCanReferenceGlobalType)
			{
				return 0;
			}
			return this.GetHashCodeGlobalType();
		}

		// Token: 0x06004FA5 RID: 20389 RVA: 0x00189BAC File Offset: 0x00189BAC
		public bool Equals(ExportedType a, ExportedType b)
		{
			if (a == b)
			{
				return true;
			}
			if (a == null || b == null)
			{
				return false;
			}
			if (!this.recursionCounter.Increment())
			{
				return false;
			}
			if (!this.DontProjectWinMDRefs)
			{
				a = (WinMDHelpers.ToCLR(a.Module ?? this.sourceModule, a) ?? a);
				b = (WinMDHelpers.ToCLR(b.Module ?? this.sourceModule, b) ?? b);
			}
			bool result = this.Equals_TypeNames(a.TypeName, b.TypeName) && this.Equals_TypeNamespaces(a.TypeNamespace, b.TypeNamespace) && this.EqualsImplementation(a, b);
			this.recursionCounter.Decrement();
			return result;
		}

		// Token: 0x06004FA6 RID: 20390 RVA: 0x00189C80 File Offset: 0x00189C80
		public int GetHashCode(ExportedType a)
		{
			if (a != null)
			{
				if (!this.DontProjectWinMDRefs)
				{
					a = (WinMDHelpers.ToCLR(a.Module ?? this.sourceModule, a) ?? a);
				}
				int num = this.GetHashCode_TypeName(a.TypeName);
				if (a.Implementation is ExportedType)
				{
					num += -1049070942;
				}
				else
				{
					num += this.GetHashCode_TypeNamespace(a.TypeNamespace);
				}
				return num;
			}
			if (!this.TypeRefCanReferenceGlobalType)
			{
				return 0;
			}
			return this.GetHashCodeGlobalType();
		}

		// Token: 0x06004FA7 RID: 20391 RVA: 0x00189D14 File Offset: 0x00189D14
		public bool Equals(TypeDef a, TypeDef b)
		{
			if (a == b)
			{
				return true;
			}
			if (a == null || b == null)
			{
				return false;
			}
			if (!this.recursionCounter.Increment())
			{
				return false;
			}
			bool result;
			if (!this.DontProjectWinMDRefs)
			{
				TypeRef typeRef = WinMDHelpers.ToCLR(a.Module ?? this.sourceModule, a);
				TypeRef typeRef2 = WinMDHelpers.ToCLR(b.Module ?? this.sourceModule, b);
				if (typeRef != null || typeRef2 != null)
				{
					IType type = typeRef;
					IType a2 = type ?? a;
					type = typeRef2;
					result = this.Equals(a2, type ?? b);
					goto IL_FB;
				}
			}
			result = (this.Equals_TypeNames(a.Name, b.Name) && this.Equals_TypeNamespaces(a.Namespace, b.Namespace) && this.Equals(a.DeclaringType, b.DeclaringType) && (this.DontCompareTypeScope || this.TypeDefScopeEquals(a, b)));
			IL_FB:
			this.recursionCounter.Decrement();
			return result;
		}

		// Token: 0x06004FA8 RID: 20392 RVA: 0x00189E2C File Offset: 0x00189E2C
		public int GetHashCode(TypeDef a)
		{
			if (a == null || a.IsGlobalModuleType)
			{
				return this.GetHashCodeGlobalType();
			}
			if (!this.DontProjectWinMDRefs)
			{
				TypeRef typeRef = WinMDHelpers.ToCLR(a.Module ?? this.sourceModule, a);
				if (typeRef != null)
				{
					return this.GetHashCode(typeRef);
				}
			}
			int num = this.GetHashCode_TypeName(a.Name);
			if (a.DeclaringType != null)
			{
				num += -1049070942;
			}
			else
			{
				num += this.GetHashCode_TypeNamespace(a.Namespace);
			}
			return num;
		}

		// Token: 0x06004FA9 RID: 20393 RVA: 0x00189EC0 File Offset: 0x00189EC0
		public bool Equals(TypeSpec a, TypeSpec b)
		{
			if (a == b)
			{
				return true;
			}
			if (a == null || b == null)
			{
				return false;
			}
			if (!this.recursionCounter.Increment())
			{
				return false;
			}
			bool result = this.Equals(a.TypeSig, b.TypeSig);
			this.recursionCounter.Decrement();
			return result;
		}

		// Token: 0x06004FAA RID: 20394 RVA: 0x00189F18 File Offset: 0x00189F18
		public int GetHashCode(TypeSpec a)
		{
			if (a == null)
			{
				return 0;
			}
			return this.GetHashCode(a.TypeSig);
		}

		// Token: 0x06004FAB RID: 20395 RVA: 0x00189F30 File Offset: 0x00189F30
		private bool EqualsResolutionScope(TypeRef a, TypeRef b)
		{
			if (a == b)
			{
				return true;
			}
			if (a == null || b == null)
			{
				return false;
			}
			IResolutionScope resolutionScope = a.ResolutionScope;
			IResolutionScope resolutionScope2 = b.ResolutionScope;
			if (resolutionScope == resolutionScope2)
			{
				return true;
			}
			if (resolutionScope == null || resolutionScope2 == null)
			{
				return false;
			}
			if (!this.recursionCounter.Increment())
			{
				return false;
			}
			bool flag = true;
			TypeRef a2;
			TypeRef b2;
			bool flag2;
			IModule ma;
			IModule mb;
			AssemblyRef assemblyRef;
			AssemblyRef assemblyRef2;
			ModuleDef moduleDef;
			if ((a2 = (resolutionScope as TypeRef)) != null | (b2 = (resolutionScope2 as TypeRef)) != null)
			{
				flag2 = this.Equals(a2, b2);
				flag = false;
			}
			else if (this.DontCompareTypeScope)
			{
				flag2 = true;
			}
			else if ((ma = (resolutionScope as IModule)) != null & (mb = (resolutionScope2 as IModule)) != null)
			{
				flag2 = this.Equals(a, ma, b, mb);
			}
			else if ((assemblyRef = (resolutionScope as AssemblyRef)) != null & (assemblyRef2 = (resolutionScope2 as AssemblyRef)) != null)
			{
				flag2 = this.Equals(assemblyRef, a, assemblyRef2, b);
			}
			else if (assemblyRef != null && resolutionScope2 is ModuleRef)
			{
				ModuleDef module = b.Module;
				flag2 = (module != null && this.Equals(module.Assembly, b, assemblyRef, a));
			}
			else if (assemblyRef2 != null && resolutionScope is ModuleRef)
			{
				ModuleDef module2 = a.Module;
				flag2 = (module2 != null && this.Equals(module2.Assembly, a, assemblyRef2, b));
			}
			else if (assemblyRef != null && (moduleDef = (resolutionScope2 as ModuleDef)) != null)
			{
				flag2 = this.Equals(moduleDef.Assembly, assemblyRef, a);
			}
			else if (assemblyRef2 != null && (moduleDef = (resolutionScope as ModuleDef)) != null)
			{
				flag2 = this.Equals(moduleDef.Assembly, assemblyRef2, b);
			}
			else
			{
				flag2 = false;
				flag = false;
			}
			if (!flag2 && flag && !this.DontCheckTypeEquivalence)
			{
				TypeDef typeDef = a.Resolve();
				TypeDef typeDef2 = b.Resolve();
				if (typeDef != null && typeDef2 != null)
				{
					flag2 = this.TypeDefScopeEquals(typeDef, typeDef2);
				}
			}
			this.recursionCounter.Decrement();
			return flag2;
		}

		// Token: 0x06004FAC RID: 20396 RVA: 0x0018A170 File Offset: 0x0018A170
		private bool EqualsImplementation(ExportedType a, ExportedType b)
		{
			if (a == b)
			{
				return true;
			}
			if (a == null || b == null)
			{
				return false;
			}
			IImplementation implementation = a.Implementation;
			IImplementation implementation2 = b.Implementation;
			if (implementation == implementation2)
			{
				return true;
			}
			if (implementation == null || implementation2 == null)
			{
				return false;
			}
			if (!this.recursionCounter.Increment())
			{
				return false;
			}
			bool flag = true;
			ExportedType a2;
			ExportedType b2;
			bool flag2;
			FileDef fileDef;
			FileDef fileDef2;
			AssemblyRef assemblyRef;
			AssemblyRef assemblyRef2;
			if ((a2 = (implementation as ExportedType)) != null | (b2 = (implementation2 as ExportedType)) != null)
			{
				flag2 = this.Equals(a2, b2);
				flag = false;
			}
			else if (this.DontCompareTypeScope)
			{
				flag2 = true;
			}
			else if ((fileDef = (implementation as FileDef)) != null & (fileDef2 = (implementation2 as FileDef)) != null)
			{
				flag2 = this.Equals(fileDef, fileDef2);
			}
			else if ((assemblyRef = (implementation as AssemblyRef)) != null & (assemblyRef2 = (implementation2 as AssemblyRef)) != null)
			{
				flag2 = this.Equals(assemblyRef, a, assemblyRef2, b);
			}
			else if (fileDef != null && assemblyRef2 != null)
			{
				flag2 = this.Equals(a.DefinitionAssembly, assemblyRef2, b);
			}
			else if (fileDef2 != null && assemblyRef != null)
			{
				flag2 = this.Equals(b.DefinitionAssembly, assemblyRef, a);
			}
			else
			{
				flag2 = false;
				flag = false;
			}
			if (!flag2 && flag && !this.DontCheckTypeEquivalence)
			{
				TypeDef typeDef = a.Resolve();
				TypeDef typeDef2 = b.Resolve();
				if (typeDef != null && typeDef2 != null)
				{
					flag2 = this.TypeDefScopeEquals(typeDef, typeDef2);
				}
			}
			this.recursionCounter.Decrement();
			return flag2;
		}

		// Token: 0x06004FAD RID: 20397 RVA: 0x0018A320 File Offset: 0x0018A320
		private bool EqualsScope(TypeRef a, ExportedType b)
		{
			if (a == b)
			{
				return true;
			}
			if (a == null || b == null)
			{
				return false;
			}
			IResolutionScope resolutionScope = a.ResolutionScope;
			IImplementation implementation = b.Implementation;
			if (resolutionScope == implementation)
			{
				return true;
			}
			if (resolutionScope == null || implementation == null)
			{
				return false;
			}
			if (!this.recursionCounter.Increment())
			{
				return false;
			}
			bool flag = true;
			TypeRef a2;
			ExportedType b2;
			bool flag2;
			IModule module;
			FileDef fileDef;
			AssemblyRef assemblyRef;
			AssemblyRef assemblyRef2;
			if ((a2 = (resolutionScope as TypeRef)) != null | (b2 = (implementation as ExportedType)) != null)
			{
				flag2 = this.Equals(a2, b2);
				flag = false;
			}
			else if (this.DontCompareTypeScope)
			{
				flag2 = true;
			}
			else if ((module = (resolutionScope as IModule)) != null & (fileDef = (implementation as FileDef)) != null)
			{
				flag2 = this.Equals(a, module, b, fileDef);
			}
			else if ((assemblyRef = (resolutionScope as AssemblyRef)) != null & (assemblyRef2 = (implementation as AssemblyRef)) != null)
			{
				flag2 = this.Equals(assemblyRef, a, assemblyRef2, b);
			}
			else if (module != null && assemblyRef2 != null)
			{
				flag2 = this.Equals(a.DefinitionAssembly, assemblyRef2, b);
			}
			else if (fileDef != null && assemblyRef != null)
			{
				flag2 = this.Equals(b.DefinitionAssembly, assemblyRef, a);
			}
			else
			{
				flag = false;
				flag2 = false;
			}
			if (!flag2 && flag && !this.DontCheckTypeEquivalence)
			{
				TypeDef typeDef = a.Resolve();
				TypeDef typeDef2 = b.Resolve();
				if (typeDef != null && typeDef2 != null)
				{
					flag2 = this.TypeDefScopeEquals(typeDef, typeDef2);
				}
			}
			this.recursionCounter.Decrement();
			return flag2;
		}

		// Token: 0x06004FAE RID: 20398 RVA: 0x0018A4D4 File Offset: 0x0018A4D4
		private bool Equals(FileDef a, FileDef b)
		{
			return a == b || (a != null && b != null && UTF8String.CaseInsensitiveEquals(a.Name, b.Name));
		}

		// Token: 0x06004FAF RID: 20399 RVA: 0x0018A500 File Offset: 0x0018A500
		private bool Equals(IModule a, FileDef b)
		{
			return a == b || (a != null && b != null && UTF8String.CaseInsensitiveEquals(a.Name, b.Name));
		}

		// Token: 0x06004FB0 RID: 20400 RVA: 0x0018A52C File Offset: 0x0018A52C
		internal bool Equals(IModule a, IModule b)
		{
			return a == b || (a != null && b != null && ((!this.MscorlibIsNotSpecial && SigComparer.IsCorLib(a) && SigComparer.IsCorLib(b)) || UTF8String.CaseInsensitiveEquals(a.Name, b.Name)));
		}

		// Token: 0x06004FB1 RID: 20401 RVA: 0x0018A588 File Offset: 0x0018A588
		private static bool IsCorLib(ModuleDef a)
		{
			return a != null && a.IsManifestModule && a.Assembly.IsCorLib();
		}

		// Token: 0x06004FB2 RID: 20402 RVA: 0x0018A5A8 File Offset: 0x0018A5A8
		private static bool IsCorLib(IModule a)
		{
			ModuleDef moduleDef = a as ModuleDef;
			return moduleDef != null && moduleDef.IsManifestModule && moduleDef.Assembly.IsCorLib();
		}

		// Token: 0x06004FB3 RID: 20403 RVA: 0x0018A5E0 File Offset: 0x0018A5E0
		private static bool IsCorLib(Module a)
		{
			return a != null && a.Assembly.ManifestModule == a && a.Assembly == typeof(void).Assembly;
		}

		// Token: 0x06004FB4 RID: 20404 RVA: 0x0018A61C File Offset: 0x0018A61C
		private static bool IsCorLib(IAssembly a)
		{
			return a.IsCorLib();
		}

		// Token: 0x06004FB5 RID: 20405 RVA: 0x0018A624 File Offset: 0x0018A624
		private static bool IsCorLib(Assembly a)
		{
			return a == typeof(void).Assembly;
		}

		// Token: 0x06004FB6 RID: 20406 RVA: 0x0018A63C File Offset: 0x0018A63C
		private bool Equals(ModuleDef a, ModuleDef b)
		{
			if (a == b)
			{
				return true;
			}
			if (a == null || b == null)
			{
				return false;
			}
			if (!this.MscorlibIsNotSpecial && SigComparer.IsCorLib(a) && SigComparer.IsCorLib(b))
			{
				return true;
			}
			if (!this.recursionCounter.Increment())
			{
				return false;
			}
			bool result = this.Equals(a, b) && this.Equals(a.Assembly, b.Assembly);
			this.recursionCounter.Decrement();
			return result;
		}

		// Token: 0x06004FB7 RID: 20407 RVA: 0x0018A6CC File Offset: 0x0018A6CC
		private bool Equals(IAssembly a, IAssembly b)
		{
			if (a == b)
			{
				return true;
			}
			if (a == null || b == null)
			{
				return false;
			}
			if (!this.MscorlibIsNotSpecial && SigComparer.IsCorLib(a) && SigComparer.IsCorLib(b))
			{
				return true;
			}
			if (!this.recursionCounter.Increment())
			{
				return false;
			}
			bool result = UTF8String.CaseInsensitiveEquals(a.Name, b.Name) && (!this.CompareAssemblyPublicKeyToken || PublicKeyBase.TokenEquals(a.PublicKeyOrToken, b.PublicKeyOrToken)) && (!this.CompareAssemblyVersion || Utils.Equals(a.Version, b.Version)) && (!this.CompareAssemblyLocale || Utils.LocaleEquals(a.Culture, b.Culture));
			this.recursionCounter.Decrement();
			return result;
		}

		// Token: 0x06004FB8 RID: 20408 RVA: 0x0018A7B4 File Offset: 0x0018A7B4
		public bool Equals(TypeSig a, TypeSig b)
		{
			if (this.IgnoreModifiers)
			{
				a = a.RemoveModifiers();
				b = b.RemoveModifiers();
			}
			if (a == b)
			{
				return true;
			}
			if (a == null || b == null)
			{
				return false;
			}
			if (!this.recursionCounter.Increment())
			{
				return false;
			}
			if (!this.DontProjectWinMDRefs)
			{
				a = (WinMDHelpers.ToCLR(a.Module ?? this.sourceModule, a) ?? a);
				b = (WinMDHelpers.ToCLR(b.Module ?? this.sourceModule, b) ?? b);
			}
			bool result;
			if (a.ElementType != b.ElementType)
			{
				result = false;
			}
			else
			{
				switch (a.ElementType)
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
				case ElementType.TypedByRef:
				case ElementType.I:
				case ElementType.U:
				case ElementType.Object:
				case ElementType.Sentinel:
					result = true;
					goto IL_4AA;
				case ElementType.Ptr:
				case ElementType.ByRef:
				case ElementType.SZArray:
				case ElementType.Pinned:
					result = this.Equals(a.Next, b.Next);
					goto IL_4AA;
				case ElementType.ValueType:
				case ElementType.Class:
					if (this.RawSignatureCompare)
					{
						result = SigComparer.TokenEquals((a as ClassOrValueTypeSig).TypeDefOrRef, (b as ClassOrValueTypeSig).TypeDefOrRef);
						goto IL_4AA;
					}
					result = this.Equals((a as ClassOrValueTypeSig).TypeDefOrRef, (b as ClassOrValueTypeSig).TypeDefOrRef);
					goto IL_4AA;
				case ElementType.Var:
				case ElementType.MVar:
					result = ((a as GenericSig).Number == (b as GenericSig).Number);
					goto IL_4AA;
				case ElementType.Array:
				{
					ArraySig arraySig = a as ArraySig;
					ArraySig arraySig2 = b as ArraySig;
					result = (arraySig.Rank == arraySig2.Rank && (this.IgnoreMultiDimensionalArrayLowerBoundsAndSizes || (this.Equals(arraySig.Sizes, arraySig2.Sizes) && this.Equals(arraySig.LowerBounds, arraySig2.LowerBounds))) && this.Equals(a.Next, b.Next));
					goto IL_4AA;
				}
				case ElementType.GenericInst:
				{
					GenericInstSig genericInstSig = (GenericInstSig)a;
					GenericInstSig genericInstSig2 = (GenericInstSig)b;
					if (this.RawSignatureCompare)
					{
						ClassOrValueTypeSig genericType = genericInstSig.GenericType;
						ClassOrValueTypeSig genericType2 = genericInstSig2.GenericType;
						result = (SigComparer.TokenEquals((genericType != null) ? genericType.TypeDefOrRef : null, (genericType2 != null) ? genericType2.TypeDefOrRef : null) && this.Equals(genericInstSig.GenericArguments, genericInstSig2.GenericArguments));
						goto IL_4AA;
					}
					result = (this.Equals(genericInstSig.GenericType, genericInstSig2.GenericType) && this.Equals(genericInstSig.GenericArguments, genericInstSig2.GenericArguments));
					goto IL_4AA;
				}
				case ElementType.ValueArray:
					result = ((a as ValueArraySig).Size == (b as ValueArraySig).Size && this.Equals(a.Next, b.Next));
					goto IL_4AA;
				case ElementType.FnPtr:
					result = this.Equals((a as FnPtrSig).Signature, (b as FnPtrSig).Signature);
					goto IL_4AA;
				case ElementType.CModReqd:
				case ElementType.CModOpt:
					if (this.RawSignatureCompare)
					{
						result = (SigComparer.TokenEquals((a as ModifierSig).Modifier, (b as ModifierSig).Modifier) && this.Equals(a.Next, b.Next));
						goto IL_4AA;
					}
					result = (this.Equals((a as ModifierSig).Modifier, (b as ModifierSig).Modifier) && this.Equals(a.Next, b.Next));
					goto IL_4AA;
				case ElementType.Module:
					result = ((a as ModuleSig).Index == (b as ModuleSig).Index && this.Equals(a.Next, b.Next));
					goto IL_4AA;
				}
				result = false;
			}
			IL_4AA:
			this.recursionCounter.Decrement();
			return result;
		}

		// Token: 0x06004FB9 RID: 20409 RVA: 0x0018AC7C File Offset: 0x0018AC7C
		private static bool TokenEquals(ITypeDefOrRef a, ITypeDefOrRef b)
		{
			return a == b || (a != null && b != null && a.MDToken == b.MDToken);
		}

		// Token: 0x06004FBA RID: 20410 RVA: 0x0018ACA8 File Offset: 0x0018ACA8
		public int GetHashCode(TypeSig a)
		{
			if (a == null)
			{
				return 0;
			}
			if (!this.recursionCounter.Increment())
			{
				return 0;
			}
			if (this.genericArguments != null)
			{
				a = this.genericArguments.Resolve(a);
			}
			int num;
			switch (a.ElementType)
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
				num = this.GetHashCode((a as TypeDefOrRefSig).TypeDefOrRef);
				goto IL_2F5;
			case ElementType.Ptr:
				num = 1976400808 + this.GetHashCode(a.Next);
				goto IL_2F5;
			case ElementType.ByRef:
				num = -634749586 + this.GetHashCode(a.Next);
				goto IL_2F5;
			case ElementType.Var:
				num = (int)(1288450097U + (a as GenericVar).Number);
				goto IL_2F5;
			case ElementType.Array:
			{
				ArraySig arraySig = (ArraySig)a;
				num = (int)(4198635765U + arraySig.Rank + (uint)this.GetHashCode(arraySig.Next));
				goto IL_2F5;
			}
			case ElementType.GenericInst:
			{
				GenericInstSig genericInstSig = (GenericInstSig)a;
				num = -2050514639;
				if (this.SubstituteGenericParameters)
				{
					this.InitializeGenericArguments();
					this.genericArguments.PushTypeArgs(genericInstSig.GenericArguments);
					num += this.GetHashCode(genericInstSig.GenericType);
					this.genericArguments.PopTypeArgs();
				}
				else
				{
					num += this.GetHashCode(genericInstSig.GenericType);
				}
				num += this.GetHashCode(genericInstSig.GenericArguments);
				goto IL_2F5;
			}
			case ElementType.ValueArray:
				num = (int)(3619996763U + (a as ValueArraySig).Size + (uint)this.GetHashCode(a.Next));
				goto IL_2F5;
			case ElementType.FnPtr:
				num = this.GetHashCode_FnPtr_SystemIntPtr();
				goto IL_2F5;
			case ElementType.SZArray:
				num = 871833535 + this.GetHashCode(a.Next);
				goto IL_2F5;
			case ElementType.MVar:
				num = (int)(3304368801U + (a as GenericMVar).Number);
				goto IL_2F5;
			case ElementType.CModReqd:
			case ElementType.CModOpt:
			case ElementType.Pinned:
				num = this.GetHashCode(a.Next);
				goto IL_2F5;
			case ElementType.Module:
				num = (int)(3995222445U + (a as ModuleSig).Index + (uint)this.GetHashCode(a.Next));
				goto IL_2F5;
			case ElementType.Sentinel:
				num = 68439620;
				goto IL_2F5;
			}
			num = 0;
			IL_2F5:
			this.recursionCounter.Decrement();
			return num;
		}

		// Token: 0x06004FBB RID: 20411 RVA: 0x0018AFBC File Offset: 0x0018AFBC
		public bool Equals(IList<TypeSig> a, IList<TypeSig> b)
		{
			if (a == b)
			{
				return true;
			}
			if (a == null || b == null)
			{
				return false;
			}
			if (!this.recursionCounter.Increment())
			{
				return false;
			}
			bool result;
			if (a.Count != b.Count)
			{
				result = false;
			}
			else
			{
				int num = 0;
				while (num < a.Count && this.Equals(a[num], b[num]))
				{
					num++;
				}
				result = (num == a.Count);
			}
			this.recursionCounter.Decrement();
			return result;
		}

		// Token: 0x06004FBC RID: 20412 RVA: 0x0018B054 File Offset: 0x0018B054
		public int GetHashCode(IList<TypeSig> a)
		{
			if (a == null)
			{
				return 0;
			}
			if (!this.recursionCounter.Increment())
			{
				return 0;
			}
			uint num = 0U;
			for (int i = 0; i < a.Count; i++)
			{
				num += (uint)this.GetHashCode(a[i]);
				num = (num << 13 | num >> 19);
			}
			this.recursionCounter.Decrement();
			return (int)num;
		}

		// Token: 0x06004FBD RID: 20413 RVA: 0x0018B0BC File Offset: 0x0018B0BC
		private bool Equals(IList<uint> a, IList<uint> b)
		{
			if (a == b)
			{
				return true;
			}
			if (a == null || b == null)
			{
				return false;
			}
			if (a.Count != b.Count)
			{
				return false;
			}
			for (int i = 0; i < a.Count; i++)
			{
				if (a[i] != b[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06004FBE RID: 20414 RVA: 0x0018B124 File Offset: 0x0018B124
		private bool Equals(IList<int> a, IList<int> b)
		{
			if (a == b)
			{
				return true;
			}
			if (a == null || b == null)
			{
				return false;
			}
			if (a.Count != b.Count)
			{
				return false;
			}
			for (int i = 0; i < a.Count; i++)
			{
				if (a[i] != b[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06004FBF RID: 20415 RVA: 0x0018B18C File Offset: 0x0018B18C
		public bool Equals(CallingConventionSig a, CallingConventionSig b)
		{
			if (a == b)
			{
				return true;
			}
			if (a == null || b == null)
			{
				return false;
			}
			if (!this.recursionCounter.Increment())
			{
				return false;
			}
			bool result;
			if (a.GetCallingConvention() != b.GetCallingConvention())
			{
				result = false;
			}
			else
			{
				switch (a.GetCallingConvention() & CallingConvention.Mask)
				{
				case CallingConvention.Default:
				case CallingConvention.C:
				case CallingConvention.StdCall:
				case CallingConvention.ThisCall:
				case CallingConvention.FastCall:
				case CallingConvention.VarArg:
				case CallingConvention.Property:
				case CallingConvention.NativeVarArg:
				{
					MethodBaseSig methodBaseSig = a as MethodBaseSig;
					MethodBaseSig methodBaseSig2 = b as MethodBaseSig;
					result = (methodBaseSig != null && methodBaseSig2 != null && this.Equals(methodBaseSig, methodBaseSig2));
					goto IL_152;
				}
				case CallingConvention.Field:
				{
					FieldSig fieldSig = a as FieldSig;
					FieldSig fieldSig2 = b as FieldSig;
					result = (fieldSig != null && fieldSig2 != null && this.Equals(fieldSig, fieldSig2));
					goto IL_152;
				}
				case CallingConvention.LocalSig:
				{
					LocalSig localSig = a as LocalSig;
					LocalSig localSig2 = b as LocalSig;
					result = (localSig != null && localSig2 != null && this.Equals(localSig, localSig2));
					goto IL_152;
				}
				case CallingConvention.GenericInst:
				{
					GenericInstMethodSig genericInstMethodSig = a as GenericInstMethodSig;
					GenericInstMethodSig genericInstMethodSig2 = b as GenericInstMethodSig;
					result = (genericInstMethodSig != null && genericInstMethodSig2 != null && this.Equals(genericInstMethodSig, genericInstMethodSig2));
					goto IL_152;
				}
				}
				result = false;
			}
			IL_152:
			this.recursionCounter.Decrement();
			return result;
		}

		// Token: 0x06004FC0 RID: 20416 RVA: 0x0018B2FC File Offset: 0x0018B2FC
		public int GetHashCode(CallingConventionSig a)
		{
			if (a == null)
			{
				return 0;
			}
			if (!this.recursionCounter.Increment())
			{
				return 0;
			}
			int result;
			switch (a.GetCallingConvention() & CallingConvention.Mask)
			{
			case CallingConvention.Default:
			case CallingConvention.C:
			case CallingConvention.StdCall:
			case CallingConvention.ThisCall:
			case CallingConvention.FastCall:
			case CallingConvention.VarArg:
			case CallingConvention.Property:
			case CallingConvention.NativeVarArg:
			{
				MethodBaseSig methodBaseSig = a as MethodBaseSig;
				result = ((methodBaseSig == null) ? 0 : this.GetHashCode(methodBaseSig));
				goto IL_ED;
			}
			case CallingConvention.Field:
			{
				FieldSig fieldSig = a as FieldSig;
				result = ((fieldSig == null) ? 0 : this.GetHashCode(fieldSig));
				goto IL_ED;
			}
			case CallingConvention.LocalSig:
			{
				LocalSig localSig = a as LocalSig;
				result = ((localSig == null) ? 0 : this.GetHashCode(localSig));
				goto IL_ED;
			}
			case CallingConvention.GenericInst:
			{
				GenericInstMethodSig genericInstMethodSig = a as GenericInstMethodSig;
				result = ((genericInstMethodSig == null) ? 0 : this.GetHashCode(genericInstMethodSig));
				goto IL_ED;
			}
			}
			result = this.GetHashCode_CallingConvention(a);
			IL_ED:
			this.recursionCounter.Decrement();
			return result;
		}

		// Token: 0x06004FC1 RID: 20417 RVA: 0x0018B408 File Offset: 0x0018B408
		public bool Equals(MethodBaseSig a, MethodBaseSig b)
		{
			if (a == b)
			{
				return true;
			}
			if (a == null || b == null)
			{
				return false;
			}
			if (!this.recursionCounter.Increment())
			{
				return false;
			}
			bool result = a.GetCallingConvention() == b.GetCallingConvention() && (this.DontCompareReturnType || this.Equals(a.RetType, b.RetType)) && this.Equals(a.Params, b.Params) && (!a.Generic || a.GenParamCount == b.GenParamCount) && (!this.CompareSentinelParams || this.Equals(a.ParamsAfterSentinel, b.ParamsAfterSentinel));
			this.recursionCounter.Decrement();
			return result;
		}

		// Token: 0x06004FC2 RID: 20418 RVA: 0x0018B4DC File Offset: 0x0018B4DC
		public int GetHashCode(MethodBaseSig a)
		{
			if (a == null)
			{
				return 0;
			}
			if (!this.recursionCounter.Increment())
			{
				return 0;
			}
			int num = this.GetHashCode_CallingConvention(a) + this.GetHashCode(a.Params);
			if (!this.DontCompareReturnType)
			{
				num += this.GetHashCode(a.RetType);
			}
			if (a.Generic)
			{
				num += SigComparer.GetHashCode_ElementType_MVar((int)a.GenParamCount);
			}
			if (this.CompareSentinelParams)
			{
				num += this.GetHashCode(a.ParamsAfterSentinel);
			}
			this.recursionCounter.Decrement();
			return num;
		}

		// Token: 0x06004FC3 RID: 20419 RVA: 0x0018B578 File Offset: 0x0018B578
		private int GetHashCode_CallingConvention(CallingConventionSig a)
		{
			return this.GetHashCode(a.GetCallingConvention());
		}

		// Token: 0x06004FC4 RID: 20420 RVA: 0x0018B588 File Offset: 0x0018B588
		private int GetHashCode(CallingConvention a)
		{
			switch (a & CallingConvention.Mask)
			{
			case CallingConvention.Default:
			case CallingConvention.C:
			case CallingConvention.StdCall:
			case CallingConvention.ThisCall:
			case CallingConvention.FastCall:
			case CallingConvention.VarArg:
			case CallingConvention.Field:
			case CallingConvention.Property:
			case CallingConvention.Unmanaged:
			case CallingConvention.GenericInst:
			case CallingConvention.NativeVarArg:
				return (int)(a & (CallingConvention.Generic | CallingConvention.HasThis | CallingConvention.ExplicitThis));
			}
			return (int)a;
		}

		// Token: 0x06004FC5 RID: 20421 RVA: 0x0018B5E0 File Offset: 0x0018B5E0
		public bool Equals(FieldSig a, FieldSig b)
		{
			if (a == b)
			{
				return true;
			}
			if (a == null || b == null)
			{
				return false;
			}
			if (!this.recursionCounter.Increment())
			{
				return false;
			}
			bool result = a.GetCallingConvention() == b.GetCallingConvention() && this.Equals(a.Type, b.Type);
			this.recursionCounter.Decrement();
			return result;
		}

		// Token: 0x06004FC6 RID: 20422 RVA: 0x0018B650 File Offset: 0x0018B650
		public int GetHashCode(FieldSig a)
		{
			if (a == null)
			{
				return 0;
			}
			if (!this.recursionCounter.Increment())
			{
				return 0;
			}
			int result = this.GetHashCode_CallingConvention(a) + this.GetHashCode(a.Type);
			this.recursionCounter.Decrement();
			return result;
		}

		// Token: 0x06004FC7 RID: 20423 RVA: 0x0018B69C File Offset: 0x0018B69C
		public bool Equals(LocalSig a, LocalSig b)
		{
			if (a == b)
			{
				return true;
			}
			if (a == null || b == null)
			{
				return false;
			}
			if (!this.recursionCounter.Increment())
			{
				return false;
			}
			bool result = a.GetCallingConvention() == b.GetCallingConvention() && this.Equals(a.Locals, b.Locals);
			this.recursionCounter.Decrement();
			return result;
		}

		// Token: 0x06004FC8 RID: 20424 RVA: 0x0018B70C File Offset: 0x0018B70C
		public int GetHashCode(LocalSig a)
		{
			if (a == null)
			{
				return 0;
			}
			if (!this.recursionCounter.Increment())
			{
				return 0;
			}
			int result = this.GetHashCode_CallingConvention(a) + this.GetHashCode(a.Locals);
			this.recursionCounter.Decrement();
			return result;
		}

		// Token: 0x06004FC9 RID: 20425 RVA: 0x0018B758 File Offset: 0x0018B758
		public bool Equals(GenericInstMethodSig a, GenericInstMethodSig b)
		{
			if (a == b)
			{
				return true;
			}
			if (a == null || b == null)
			{
				return false;
			}
			if (!this.recursionCounter.Increment())
			{
				return false;
			}
			bool result = a.GetCallingConvention() == b.GetCallingConvention() && this.Equals(a.GenericArguments, b.GenericArguments);
			this.recursionCounter.Decrement();
			return result;
		}

		// Token: 0x06004FCA RID: 20426 RVA: 0x0018B7C8 File Offset: 0x0018B7C8
		public int GetHashCode(GenericInstMethodSig a)
		{
			if (a == null)
			{
				return 0;
			}
			if (!this.recursionCounter.Increment())
			{
				return 0;
			}
			int result = this.GetHashCode_CallingConvention(a) + this.GetHashCode(a.GenericArguments);
			this.recursionCounter.Decrement();
			return result;
		}

		// Token: 0x06004FCB RID: 20427 RVA: 0x0018B814 File Offset: 0x0018B814
		public bool Equals(IMethod a, IMethod b)
		{
			if (a == b)
			{
				return true;
			}
			if (a == null || b == null)
			{
				return false;
			}
			if (!this.recursionCounter.Increment())
			{
				return false;
			}
			MethodDef methodDef;
			MethodDef methodDef2;
			bool result;
			MemberRef memberRef;
			MemberRef memberRef2;
			MethodSpec a2;
			MethodSpec b2;
			if ((methodDef = (a as MethodDef)) != null & (methodDef2 = (b as MethodDef)) != null)
			{
				result = this.Equals(methodDef, methodDef2);
			}
			else if ((memberRef = (a as MemberRef)) != null & (memberRef2 = (b as MemberRef)) != null)
			{
				result = this.Equals(memberRef, memberRef2);
			}
			else if ((a2 = (a as MethodSpec)) != null && (b2 = (b as MethodSpec)) != null)
			{
				result = this.Equals(a2, b2);
			}
			else if (methodDef != null && memberRef2 != null)
			{
				result = this.Equals(methodDef, memberRef2);
			}
			else
			{
				result = (memberRef != null && methodDef2 != null && this.Equals(methodDef2, memberRef));
			}
			this.recursionCounter.Decrement();
			return result;
		}

		// Token: 0x06004FCC RID: 20428 RVA: 0x0018B920 File Offset: 0x0018B920
		public int GetHashCode(IMethod a)
		{
			if (a == null)
			{
				return 0;
			}
			if (!this.recursionCounter.Increment())
			{
				return 0;
			}
			MethodDef a2;
			int result;
			MemberRef a3;
			MethodSpec a4;
			if ((a2 = (a as MethodDef)) != null)
			{
				result = this.GetHashCode(a2);
			}
			else if ((a3 = (a as MemberRef)) != null)
			{
				result = this.GetHashCode(a3);
			}
			else if ((a4 = (a as MethodSpec)) != null)
			{
				result = this.GetHashCode(a4);
			}
			else
			{
				result = 0;
			}
			this.recursionCounter.Decrement();
			return result;
		}

		// Token: 0x06004FCD RID: 20429 RVA: 0x0018B9A8 File Offset: 0x0018B9A8
		public bool Equals(MemberRef a, MethodDef b)
		{
			return this.Equals(b, a);
		}

		// Token: 0x06004FCE RID: 20430 RVA: 0x0018B9B4 File Offset: 0x0018B9B4
		public bool Equals(MethodDef a, MemberRef b)
		{
			if (a == b)
			{
				return true;
			}
			if (a == null || b == null)
			{
				return false;
			}
			if (!this.recursionCounter.Increment())
			{
				return false;
			}
			bool result;
			if (!this.DontProjectWinMDRefs)
			{
				MemberRef memberRef = WinMDHelpers.ToCLR(a.Module ?? this.sourceModule, a);
				b = (WinMDHelpers.ToCLR(b.Module ?? this.sourceModule, b) ?? b);
				if (memberRef != null)
				{
					result = this.Equals(memberRef, b);
					goto IL_F3;
				}
			}
			result = ((this.PrivateScopeMethodIsComparable || !a.IsPrivateScope) && this.Equals_MethodFieldNames(a.Name, b.Name) && this.Equals(a.Signature, b.Signature) && (!this.CompareMethodFieldDeclaringType || this.Equals(a.DeclaringType, b.Class)));
			IL_F3:
			this.recursionCounter.Decrement();
			return result;
		}

		// Token: 0x06004FCF RID: 20431 RVA: 0x0018BAC4 File Offset: 0x0018BAC4
		public bool Equals(MethodDef a, MethodDef b)
		{
			if (a == b)
			{
				return true;
			}
			if (a == null || b == null)
			{
				return false;
			}
			if (!this.recursionCounter.Increment())
			{
				return false;
			}
			bool result;
			if (!this.DontProjectWinMDRefs)
			{
				MemberRef memberRef = WinMDHelpers.ToCLR(a.Module ?? this.sourceModule, a);
				MemberRef memberRef2 = WinMDHelpers.ToCLR(b.Module ?? this.sourceModule, b);
				if (memberRef != null || memberRef2 != null)
				{
					IMethod method = memberRef;
					IMethod a2 = method ?? a;
					method = memberRef2;
					result = this.Equals(a2, method ?? b);
					goto IL_EE;
				}
			}
			result = (this.Equals_MethodFieldNames(a.Name, b.Name) && this.Equals(a.Signature, b.Signature) && (!this.CompareMethodFieldDeclaringType || this.Equals(a.DeclaringType, b.DeclaringType)));
			IL_EE:
			this.recursionCounter.Decrement();
			return result;
		}

		// Token: 0x06004FD0 RID: 20432 RVA: 0x0018BBD0 File Offset: 0x0018BBD0
		public int GetHashCode(MethodDef a)
		{
			if (a == null)
			{
				return 0;
			}
			if (!this.DontProjectWinMDRefs)
			{
				MemberRef memberRef = WinMDHelpers.ToCLR(a.Module ?? this.sourceModule, a);
				if (memberRef != null)
				{
					return this.GetHashCode(memberRef);
				}
			}
			if (!this.recursionCounter.Increment())
			{
				return 0;
			}
			int num = this.GetHashCode_MethodFieldName(a.Name) + this.GetHashCode(a.Signature);
			if (this.CompareMethodFieldDeclaringType)
			{
				num += this.GetHashCode(a.DeclaringType);
			}
			this.recursionCounter.Decrement();
			return num;
		}

		// Token: 0x06004FD1 RID: 20433 RVA: 0x0018BC70 File Offset: 0x0018BC70
		public bool Equals(MemberRef a, MemberRef b)
		{
			if (a == b)
			{
				return true;
			}
			if (a == null || b == null)
			{
				return false;
			}
			if (!this.recursionCounter.Increment())
			{
				return false;
			}
			if (!this.DontProjectWinMDRefs)
			{
				a = (WinMDHelpers.ToCLR(a.Module ?? this.sourceModule, a) ?? a);
				b = (WinMDHelpers.ToCLR(b.Module ?? this.sourceModule, b) ?? b);
			}
			bool result = this.Equals_MethodFieldNames(a.Name, b.Name) && this.Equals(a.Signature, b.Signature) && (!this.CompareMethodFieldDeclaringType || this.Equals(a.Class, b.Class));
			this.recursionCounter.Decrement();
			return result;
		}

		// Token: 0x06004FD2 RID: 20434 RVA: 0x0018BD60 File Offset: 0x0018BD60
		public int GetHashCode(MemberRef a)
		{
			if (a == null)
			{
				return 0;
			}
			if (!this.recursionCounter.Increment())
			{
				return 0;
			}
			if (!this.DontProjectWinMDRefs)
			{
				a = (WinMDHelpers.ToCLR(a.Module ?? this.sourceModule, a) ?? a);
			}
			int num = this.GetHashCode_MethodFieldName(a.Name);
			GenericInstSig genericInstanceType;
			if (this.SubstituteGenericParameters && (genericInstanceType = SigComparer.GetGenericInstanceType(a.Class)) != null)
			{
				this.InitializeGenericArguments();
				this.genericArguments.PushTypeArgs(genericInstanceType.GenericArguments);
				num += this.GetHashCode(a.Signature);
				this.genericArguments.PopTypeArgs();
			}
			else
			{
				num += this.GetHashCode(a.Signature);
			}
			if (this.CompareMethodFieldDeclaringType)
			{
				num += this.GetHashCode(a.Class);
			}
			this.recursionCounter.Decrement();
			return num;
		}

		// Token: 0x06004FD3 RID: 20435 RVA: 0x0018BE50 File Offset: 0x0018BE50
		public bool Equals(MethodSpec a, MethodSpec b)
		{
			if (a == b)
			{
				return true;
			}
			if (a == null || b == null)
			{
				return false;
			}
			if (!this.recursionCounter.Increment())
			{
				return false;
			}
			bool result = this.Equals(a.Method, b.Method) && this.Equals(a.Instantiation, b.Instantiation);
			this.recursionCounter.Decrement();
			return result;
		}

		// Token: 0x06004FD4 RID: 20436 RVA: 0x0018BEC4 File Offset: 0x0018BEC4
		public int GetHashCode(MethodSpec a)
		{
			if (a == null)
			{
				return 0;
			}
			if (!this.recursionCounter.Increment())
			{
				return 0;
			}
			SigComparerOptions oldFlags = this.SetOptions((SigComparerOptions)1024U);
			GenericInstMethodSig genericInstMethodSig = a.GenericInstMethodSig;
			if (genericInstMethodSig != null)
			{
				this.InitializeGenericArguments();
				this.genericArguments.PushMethodArgs(genericInstMethodSig.GenericArguments);
			}
			int hashCode = this.GetHashCode(a.Method);
			if (genericInstMethodSig != null)
			{
				this.genericArguments.PopMethodArgs();
			}
			this.RestoreOptions(oldFlags);
			this.recursionCounter.Decrement();
			return hashCode;
		}

		// Token: 0x06004FD5 RID: 20437 RVA: 0x0018BF50 File Offset: 0x0018BF50
		private bool Equals(IMemberRefParent a, IMemberRefParent b)
		{
			if (a == b)
			{
				return true;
			}
			if (a == null || b == null)
			{
				return false;
			}
			if (!this.recursionCounter.Increment())
			{
				return false;
			}
			ITypeDefOrRef a2;
			ITypeDefOrRef b2;
			bool result;
			ModuleRef moduleRef;
			ModuleRef moduleRef2;
			MethodDef a3;
			MethodDef b3;
			TypeDef a4;
			if ((a2 = (a as ITypeDefOrRef)) != null && (b2 = (b as ITypeDefOrRef)) != null)
			{
				result = this.Equals(a2, b2);
			}
			else if ((moduleRef = (a as ModuleRef)) != null & (moduleRef2 = (b as ModuleRef)) != null)
			{
				ModuleDef module = moduleRef.Module;
				ModuleDef module2 = moduleRef2.Module;
				result = (this.Equals(moduleRef, moduleRef2) && this.Equals((module != null) ? module.Assembly : null, (module2 != null) ? module2.Assembly : null));
			}
			else if ((a3 = (a as MethodDef)) != null && (b3 = (b as MethodDef)) != null)
			{
				result = this.Equals(a3, b3);
			}
			else if (moduleRef2 != null && (a4 = (a as TypeDef)) != null)
			{
				result = this.EqualsGlobal(a4, moduleRef2);
			}
			else
			{
				result = (moduleRef != null && (a4 = (b as TypeDef)) != null && this.EqualsGlobal(a4, moduleRef));
			}
			this.recursionCounter.Decrement();
			return result;
		}

		// Token: 0x06004FD6 RID: 20438 RVA: 0x0018C0B0 File Offset: 0x0018C0B0
		private int GetHashCode(IMemberRefParent a)
		{
			if (a == null)
			{
				return 0;
			}
			if (!this.recursionCounter.Increment())
			{
				return 0;
			}
			ITypeDefOrRef a2;
			int result;
			MethodDef methodDef;
			if ((a2 = (a as ITypeDefOrRef)) != null)
			{
				result = this.GetHashCode(a2);
			}
			else if (a is ModuleRef)
			{
				result = this.GetHashCodeGlobalType();
			}
			else if ((methodDef = (a as MethodDef)) != null)
			{
				result = this.GetHashCode(methodDef.DeclaringType);
			}
			else
			{
				result = 0;
			}
			this.recursionCounter.Decrement();
			return result;
		}

		// Token: 0x06004FD7 RID: 20439 RVA: 0x0018C13C File Offset: 0x0018C13C
		public bool Equals(IField a, IField b)
		{
			if (a == b)
			{
				return true;
			}
			if (a == null || b == null)
			{
				return false;
			}
			if (!this.recursionCounter.Increment())
			{
				return false;
			}
			FieldDef fieldDef;
			FieldDef fieldDef2;
			bool result;
			MemberRef memberRef;
			MemberRef memberRef2;
			if ((fieldDef = (a as FieldDef)) != null & (fieldDef2 = (b as FieldDef)) != null)
			{
				result = this.Equals(fieldDef, fieldDef2);
			}
			else if ((memberRef = (a as MemberRef)) != null & (memberRef2 = (b as MemberRef)) != null)
			{
				result = this.Equals(memberRef, memberRef2);
			}
			else if (fieldDef != null && memberRef2 != null)
			{
				result = this.Equals(fieldDef, memberRef2);
			}
			else
			{
				result = (fieldDef2 != null && memberRef != null && this.Equals(fieldDef2, memberRef));
			}
			this.recursionCounter.Decrement();
			return result;
		}

		// Token: 0x06004FD8 RID: 20440 RVA: 0x0018C21C File Offset: 0x0018C21C
		public int GetHashCode(IField a)
		{
			if (a == null)
			{
				return 0;
			}
			if (!this.recursionCounter.Increment())
			{
				return 0;
			}
			FieldDef a2;
			int result;
			MemberRef a3;
			if ((a2 = (a as FieldDef)) != null)
			{
				result = this.GetHashCode(a2);
			}
			else if ((a3 = (a as MemberRef)) != null)
			{
				result = this.GetHashCode(a3);
			}
			else
			{
				result = 0;
			}
			this.recursionCounter.Decrement();
			return result;
		}

		// Token: 0x06004FD9 RID: 20441 RVA: 0x0018C28C File Offset: 0x0018C28C
		public bool Equals(MemberRef a, FieldDef b)
		{
			return this.Equals(b, a);
		}

		// Token: 0x06004FDA RID: 20442 RVA: 0x0018C298 File Offset: 0x0018C298
		public bool Equals(FieldDef a, MemberRef b)
		{
			if (a == b)
			{
				return true;
			}
			if (a == null || b == null)
			{
				return false;
			}
			if (!this.recursionCounter.Increment())
			{
				return false;
			}
			bool result = (this.PrivateScopeFieldIsComparable || !a.IsPrivateScope) && this.Equals_MethodFieldNames(a.Name, b.Name) && this.Equals(a.Signature, b.Signature) && (!this.CompareMethodFieldDeclaringType || this.Equals(a.DeclaringType, b.Class));
			this.recursionCounter.Decrement();
			return result;
		}

		// Token: 0x06004FDB RID: 20443 RVA: 0x0018C34C File Offset: 0x0018C34C
		public bool Equals(FieldDef a, FieldDef b)
		{
			if (a == b)
			{
				return true;
			}
			if (a == null || b == null)
			{
				return false;
			}
			if (!this.recursionCounter.Increment())
			{
				return false;
			}
			bool result = this.Equals_MethodFieldNames(a.Name, b.Name) && this.Equals(a.Signature, b.Signature) && (!this.CompareMethodFieldDeclaringType || this.Equals(a.DeclaringType, b.DeclaringType));
			this.recursionCounter.Decrement();
			return result;
		}

		// Token: 0x06004FDC RID: 20444 RVA: 0x0018C3E8 File Offset: 0x0018C3E8
		public int GetHashCode(FieldDef a)
		{
			if (a == null)
			{
				return 0;
			}
			if (!this.recursionCounter.Increment())
			{
				return 0;
			}
			int num = this.GetHashCode_MethodFieldName(a.Name) + this.GetHashCode(a.Signature);
			if (this.CompareMethodFieldDeclaringType)
			{
				num += this.GetHashCode(a.DeclaringType);
			}
			this.recursionCounter.Decrement();
			return num;
		}

		// Token: 0x06004FDD RID: 20445 RVA: 0x0018C454 File Offset: 0x0018C454
		public bool Equals(PropertyDef a, PropertyDef b)
		{
			if (a == b)
			{
				return true;
			}
			if (a == null || b == null)
			{
				return false;
			}
			if (!this.recursionCounter.Increment())
			{
				return false;
			}
			bool result = this.Equals_PropertyNames(a.Name, b.Name) && this.Equals(a.Type, b.Type) && (!this.ComparePropertyDeclaringType || this.Equals(a.DeclaringType, b.DeclaringType));
			this.recursionCounter.Decrement();
			return result;
		}

		// Token: 0x06004FDE RID: 20446 RVA: 0x0018C4F0 File Offset: 0x0018C4F0
		public int GetHashCode(PropertyDef a)
		{
			if (a == null)
			{
				return 0;
			}
			if (!this.recursionCounter.Increment())
			{
				return 0;
			}
			PropertySig propertySig = a.PropertySig;
			int num = this.GetHashCode_PropertyName(a.Name) + this.GetHashCode((propertySig != null) ? propertySig.RetType : null);
			if (this.ComparePropertyDeclaringType)
			{
				num += this.GetHashCode(a.DeclaringType);
			}
			this.recursionCounter.Decrement();
			return num;
		}

		// Token: 0x06004FDF RID: 20447 RVA: 0x0018C570 File Offset: 0x0018C570
		public bool Equals(EventDef a, EventDef b)
		{
			if (a == b)
			{
				return true;
			}
			if (a == null || b == null)
			{
				return false;
			}
			if (!this.recursionCounter.Increment())
			{
				return false;
			}
			bool result = this.Equals_EventNames(a.Name, b.Name) && this.Equals(a.EventType, b.EventType) && (!this.CompareEventDeclaringType || this.Equals(a.DeclaringType, b.DeclaringType));
			this.recursionCounter.Decrement();
			return result;
		}

		// Token: 0x06004FE0 RID: 20448 RVA: 0x0018C60C File Offset: 0x0018C60C
		public int GetHashCode(EventDef a)
		{
			if (a == null)
			{
				return 0;
			}
			if (!this.recursionCounter.Increment())
			{
				return 0;
			}
			int num = this.GetHashCode_EventName(a.Name) + this.GetHashCode(a.EventType);
			if (this.CompareEventDeclaringType)
			{
				num += this.GetHashCode(a.DeclaringType);
			}
			this.recursionCounter.Decrement();
			return num;
		}

		// Token: 0x06004FE1 RID: 20449 RVA: 0x0018C678 File Offset: 0x0018C678
		private bool EqualsGlobal(TypeDef a, ModuleRef b)
		{
			if (a == b)
			{
				return true;
			}
			if (a == null || b == null)
			{
				return false;
			}
			if (!this.recursionCounter.Increment())
			{
				return false;
			}
			bool result = a.IsGlobalModuleType && this.Equals(a.Module, b) && this.Equals(a.DefinitionAssembly, SigComparer.GetAssembly(b.Module));
			this.recursionCounter.Decrement();
			return result;
		}

		// Token: 0x06004FE2 RID: 20450 RVA: 0x0018C6F8 File Offset: 0x0018C6F8
		private static AssemblyDef GetAssembly(ModuleDef module)
		{
			if (module == null)
			{
				return null;
			}
			return module.Assembly;
		}

		// Token: 0x06004FE3 RID: 20451 RVA: 0x0018C708 File Offset: 0x0018C708
		public bool Equals(Type a, IType b)
		{
			return this.Equals(b, a);
		}

		// Token: 0x06004FE4 RID: 20452 RVA: 0x0018C714 File Offset: 0x0018C714
		public bool Equals(IType a, Type b)
		{
			if (a == null)
			{
				return false;
			}
			if (!this.recursionCounter.Increment())
			{
				return false;
			}
			TypeDef a2;
			bool result;
			TypeRef a3;
			TypeSpec a4;
			TypeSig a5;
			if ((a2 = (a as TypeDef)) != null)
			{
				result = this.Equals(a2, b);
			}
			else if ((a3 = (a as TypeRef)) != null)
			{
				result = this.Equals(a3, b);
			}
			else if ((a4 = (a as TypeSpec)) != null)
			{
				result = this.Equals(a4, b);
			}
			else if ((a5 = (a as TypeSig)) != null)
			{
				result = this.Equals(a5, b);
			}
			else
			{
				ExportedType a6;
				result = ((a6 = (a as ExportedType)) != null && this.Equals(a6, b));
			}
			this.recursionCounter.Decrement();
			return result;
		}

		// Token: 0x06004FE5 RID: 20453 RVA: 0x0018C7D8 File Offset: 0x0018C7D8
		public bool Equals(Type a, TypeDef b)
		{
			return this.Equals(b, a);
		}

		// Token: 0x06004FE6 RID: 20454 RVA: 0x0018C7E4 File Offset: 0x0018C7E4
		public bool Equals(TypeDef a, Type b)
		{
			if (a == null)
			{
				return false;
			}
			if (b == null)
			{
				return a.IsGlobalModuleType;
			}
			if (!this.recursionCounter.Increment())
			{
				return false;
			}
			bool result;
			if (!this.DontProjectWinMDRefs)
			{
				TypeRef typeRef = WinMDHelpers.ToCLR(a.Module ?? this.sourceModule, a);
				if (typeRef != null)
				{
					result = this.Equals(typeRef, b);
					goto IL_D5;
				}
			}
			result = (!b.HasElementType && this.Equals_TypeNames(a.Name, b.Name) && this.Equals_TypeNamespaces(a.Namespace, b) && this.EnclosingTypeEquals(a.DeclaringType, b.DeclaringType) && (this.DontCompareTypeScope || this.Equals(a.Module, b.Module)));
			IL_D5:
			this.recursionCounter.Decrement();
			return result;
		}

		// Token: 0x06004FE7 RID: 20455 RVA: 0x0018C8D8 File Offset: 0x0018C8D8
		private bool EnclosingTypeEquals(TypeDef a, Type b)
		{
			return a == b || (a != null && b != null && this.Equals(a, b));
		}

		// Token: 0x06004FE8 RID: 20456 RVA: 0x0018C8FC File Offset: 0x0018C8FC
		public bool Equals(Type a, TypeRef b)
		{
			return this.Equals(b, a);
		}

		// Token: 0x06004FE9 RID: 20457 RVA: 0x0018C908 File Offset: 0x0018C908
		public bool Equals(TypeRef a, Type b)
		{
			if (a == null)
			{
				return false;
			}
			if (b == null)
			{
				return false;
			}
			if (!this.recursionCounter.Increment())
			{
				return false;
			}
			if (!this.DontProjectWinMDRefs)
			{
				a = (WinMDHelpers.ToCLR(a.Module ?? this.sourceModule, a) ?? a);
			}
			IResolutionScope resolutionScope = a.ResolutionScope;
			bool result;
			TypeRef a2;
			IModule bMod;
			if (!b.IsTypeDef())
			{
				result = false;
			}
			else if (!this.Equals_TypeNames(a.Name, b.Name) || !this.Equals_TypeNamespaces(a.Namespace, b))
			{
				result = false;
			}
			else if ((a2 = (resolutionScope as TypeRef)) != null)
			{
				result = this.Equals(a2, b.DeclaringType);
			}
			else if (b.IsNested)
			{
				result = false;
			}
			else if (this.DontCompareTypeScope)
			{
				result = true;
			}
			else if ((bMod = (resolutionScope as IModule)) != null)
			{
				result = this.Equals(b, bMod, a);
			}
			else
			{
				AssemblyRef bAsm;
				result = ((bAsm = (resolutionScope as AssemblyRef)) != null && this.Equals(b.Assembly, bAsm, a));
			}
			this.recursionCounter.Decrement();
			return result;
		}

		// Token: 0x06004FEA RID: 20458 RVA: 0x0018CA44 File Offset: 0x0018CA44
		private bool Equals_TypeNamespaces(UTF8String a, Type b)
		{
			return b.IsNested || this.Equals_TypeNamespaces(a, b.Namespace);
		}

		// Token: 0x06004FEB RID: 20459 RVA: 0x0018CA60 File Offset: 0x0018CA60
		public bool Equals(Type a, TypeSpec b)
		{
			return this.Equals(b, a);
		}

		// Token: 0x06004FEC RID: 20460 RVA: 0x0018CA6C File Offset: 0x0018CA6C
		public bool Equals(TypeSpec a, Type b)
		{
			return a != null && b != null && this.Equals(a.TypeSig, b);
		}

		// Token: 0x06004FED RID: 20461 RVA: 0x0018CA8C File Offset: 0x0018CA8C
		public bool Equals(Type a, TypeSig b)
		{
			return this.Equals(b, a);
		}

		// Token: 0x06004FEE RID: 20462 RVA: 0x0018CA98 File Offset: 0x0018CA98
		public bool Equals(TypeSig a, Type b)
		{
			return this.Equals(a, b, false);
		}

		// Token: 0x06004FEF RID: 20463 RVA: 0x0018CAA4 File Offset: 0x0018CAA4
		private bool Equals(ITypeDefOrRef a, Type b, bool treatAsGenericInst)
		{
			TypeSpec typeSpec = a as TypeSpec;
			if (typeSpec != null)
			{
				return this.Equals(typeSpec.TypeSig, b, treatAsGenericInst);
			}
			return this.Equals(a, b);
		}

		// Token: 0x06004FF0 RID: 20464 RVA: 0x0018CADC File Offset: 0x0018CADC
		private static bool IsFnPtrElementType(Type a)
		{
			if (a == null || !a.HasElementType)
			{
				return false;
			}
			Type elementType = a.GetElementType();
			return elementType != null && !elementType.HasElementType && !(elementType != typeof(IntPtr)) && a.FullName.StartsWith("(fnptr)");
		}

		// Token: 0x06004FF1 RID: 20465 RVA: 0x0018CB4C File Offset: 0x0018CB4C
		private bool Equals(TypeSig a, Type b, bool treatAsGenericInst)
		{
			if (a == null)
			{
				return false;
			}
			if (b == null)
			{
				return false;
			}
			if (!this.recursionCounter.Increment())
			{
				return false;
			}
			if (this.genericArguments != null)
			{
				a = this.genericArguments.Resolve(a);
			}
			bool flag;
			switch (a.ElementType)
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
			case ElementType.TypedByRef:
			case ElementType.I:
			case ElementType.U:
			case ElementType.Object:
				flag = this.Equals(((TypeDefOrRefSig)a).TypeDefOrRef, b, treatAsGenericInst);
				goto IL_49C;
			case ElementType.Ptr:
				if (!b.IsPointer)
				{
					flag = false;
					goto IL_49C;
				}
				if (SigComparer.IsFnPtrElementType(b))
				{
					a = a.Next.RemoveModifiers();
					flag = (a != null && a.ElementType == ElementType.FnPtr);
					goto IL_49C;
				}
				flag = this.Equals(a.Next, b.GetElementType());
				goto IL_49C;
			case ElementType.ByRef:
				if (!b.IsByRef)
				{
					flag = false;
					goto IL_49C;
				}
				if (SigComparer.IsFnPtrElementType(b))
				{
					a = a.Next.RemoveModifiers();
					flag = (a != null && a.ElementType == ElementType.FnPtr);
					goto IL_49C;
				}
				flag = this.Equals(a.Next, b.GetElementType());
				goto IL_49C;
			case ElementType.ValueType:
			case ElementType.Class:
				flag = this.Equals((a as ClassOrValueTypeSig).TypeDefOrRef, b, treatAsGenericInst);
				goto IL_49C;
			case ElementType.Var:
				flag = (b.IsGenericParameter && (long)b.GenericParameterPosition == (long)((ulong)(a as GenericSig).Number) && b.DeclaringMethod == null);
				goto IL_49C;
			case ElementType.Array:
				flag = (b.IsArray && !b.IsSZArray() && (ulong)(a as ArraySig).Rank == (ulong)((long)b.GetArrayRank()) && (SigComparer.IsFnPtrElementType(b) ? ((a = a.Next.RemoveModifiers()) != null && a.ElementType == ElementType.FnPtr) : this.Equals(a.Next, b.GetElementType())));
				goto IL_49C;
			case ElementType.GenericInst:
			{
				if ((!b.IsGenericType || b.IsGenericTypeDefinition) && !treatAsGenericInst)
				{
					flag = false;
					goto IL_49C;
				}
				GenericInstSig genericInstSig = (GenericInstSig)a;
				if (this.SubstituteGenericParameters)
				{
					this.InitializeGenericArguments();
					this.genericArguments.PushTypeArgs(genericInstSig.GenericArguments);
					flag = this.Equals(genericInstSig.GenericType, b.GetGenericTypeDefinition());
					this.genericArguments.PopTypeArgs();
				}
				else
				{
					flag = this.Equals(genericInstSig.GenericType, b.GetGenericTypeDefinition());
				}
				flag = (flag && this.Equals(genericInstSig.GenericArguments, b.GetGenericArguments()));
				goto IL_49C;
			}
			case ElementType.FnPtr:
				flag = (b == typeof(IntPtr));
				goto IL_49C;
			case ElementType.SZArray:
				if (!b.IsArray || !b.IsSZArray())
				{
					flag = false;
					goto IL_49C;
				}
				if (SigComparer.IsFnPtrElementType(b))
				{
					a = a.Next.RemoveModifiers();
					flag = (a != null && a.ElementType == ElementType.FnPtr);
					goto IL_49C;
				}
				flag = this.Equals(a.Next, b.GetElementType());
				goto IL_49C;
			case ElementType.MVar:
				flag = (b.IsGenericParameter && (long)b.GenericParameterPosition == (long)((ulong)(a as GenericSig).Number) && b.DeclaringMethod != null);
				goto IL_49C;
			case ElementType.CModReqd:
			case ElementType.CModOpt:
				flag = this.Equals(a.Next, b, treatAsGenericInst);
				goto IL_49C;
			case ElementType.Pinned:
				flag = this.Equals(a.Next, b, treatAsGenericInst);
				goto IL_49C;
			}
			flag = false;
			IL_49C:
			this.recursionCounter.Decrement();
			return flag;
		}

		// Token: 0x06004FF2 RID: 20466 RVA: 0x0018D008 File Offset: 0x0018D008
		public bool Equals(Type a, ExportedType b)
		{
			return this.Equals(b, a);
		}

		// Token: 0x06004FF3 RID: 20467 RVA: 0x0018D014 File Offset: 0x0018D014
		public bool Equals(ExportedType a, Type b)
		{
			if (a == null)
			{
				return false;
			}
			if (b == null)
			{
				return false;
			}
			if (!this.recursionCounter.Increment())
			{
				return false;
			}
			if (!this.DontProjectWinMDRefs)
			{
				a = (WinMDHelpers.ToCLR(a.Module ?? this.sourceModule, a) ?? a);
			}
			IImplementation implementation = a.Implementation;
			bool result;
			ExportedType a2;
			FileDef bFile;
			if (!b.IsTypeDef())
			{
				result = false;
			}
			else if (!this.Equals_TypeNames(a.TypeName, b.Name) || !this.Equals_TypeNamespaces(a.TypeNamespace, b))
			{
				result = false;
			}
			else if ((a2 = (implementation as ExportedType)) != null)
			{
				result = this.Equals(a2, b.DeclaringType);
			}
			else if (b.IsNested)
			{
				result = false;
			}
			else if (this.DontCompareTypeScope)
			{
				result = true;
			}
			else if ((bFile = (implementation as FileDef)) != null)
			{
				result = this.Equals(b, bFile, a);
			}
			else
			{
				AssemblyRef bAsm;
				result = ((bAsm = (implementation as AssemblyRef)) != null && this.Equals(b.Assembly, bAsm, a));
			}
			this.recursionCounter.Decrement();
			return result;
		}

		// Token: 0x06004FF4 RID: 20468 RVA: 0x0018D150 File Offset: 0x0018D150
		public int GetHashCode(Type a)
		{
			return this.GetHashCode(a, false);
		}

		// Token: 0x06004FF5 RID: 20469 RVA: 0x0018D15C File Offset: 0x0018D15C
		public int GetHashCode(Type a, bool treatAsGenericInst)
		{
			if (a == null)
			{
				return this.GetHashCode_TypeDef(a);
			}
			if (!this.recursionCounter.Increment())
			{
				return 0;
			}
			int result;
			switch (treatAsGenericInst ? ElementType.GenericInst : a.GetElementType2())
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
				result = this.GetHashCode_TypeDef(a);
				goto IL_297;
			case ElementType.Ptr:
				result = 1976400808 + (SigComparer.IsFnPtrElementType(a) ? this.GetHashCode_FnPtr_SystemIntPtr() : this.GetHashCode(a.GetElementType()));
				goto IL_297;
			case ElementType.ByRef:
				result = -634749586 + (SigComparer.IsFnPtrElementType(a) ? this.GetHashCode_FnPtr_SystemIntPtr() : this.GetHashCode(a.GetElementType()));
				goto IL_297;
			case ElementType.Var:
				result = 1288450097 + a.GenericParameterPosition;
				goto IL_297;
			case ElementType.Array:
				result = -96331531 + a.GetArrayRank() + (SigComparer.IsFnPtrElementType(a) ? this.GetHashCode_FnPtr_SystemIntPtr() : this.GetHashCode(a.GetElementType()));
				goto IL_297;
			case ElementType.GenericInst:
				result = -2050514639 + this.GetHashCode(a.GetGenericTypeDefinition()) + this.GetHashCode(a.GetGenericArguments());
				goto IL_297;
			case ElementType.FnPtr:
				result = this.GetHashCode_FnPtr_SystemIntPtr();
				goto IL_297;
			case ElementType.SZArray:
				result = 871833535 + (SigComparer.IsFnPtrElementType(a) ? this.GetHashCode_FnPtr_SystemIntPtr() : this.GetHashCode(a.GetElementType()));
				goto IL_297;
			case ElementType.MVar:
				result = -990598495 + a.GenericParameterPosition;
				goto IL_297;
			case ElementType.CModReqd:
			case ElementType.CModOpt:
			case ElementType.Pinned:
				result = this.GetHashCode(a.GetElementType());
				goto IL_297;
			case ElementType.Sentinel:
				result = 68439620;
				goto IL_297;
			}
			result = 0;
			IL_297:
			this.recursionCounter.Decrement();
			return result;
		}

		// Token: 0x06004FF6 RID: 20470 RVA: 0x0018D410 File Offset: 0x0018D410
		private int GetHashCode(IList<Type> a)
		{
			if (a == null)
			{
				return 0;
			}
			if (!this.recursionCounter.Increment())
			{
				return 0;
			}
			uint num = 0U;
			for (int i = 0; i < a.Count; i++)
			{
				num += (uint)this.GetHashCode(a[i]);
				num = (num << 13 | num >> 19);
			}
			this.recursionCounter.Decrement();
			return (int)num;
		}

		// Token: 0x06004FF7 RID: 20471 RVA: 0x0018D478 File Offset: 0x0018D478
		private static int GetHashCode_ElementType_MVar(int numGenericParams)
		{
			return SigComparer.GetHashCode(numGenericParams, -990598495);
		}

		// Token: 0x06004FF8 RID: 20472 RVA: 0x0018D488 File Offset: 0x0018D488
		private static int GetHashCode(int numGenericParams, int etypeHashCode)
		{
			uint num = 0U;
			for (int i = 0; i < numGenericParams; i++)
			{
				num += (uint)(etypeHashCode + i);
				num = (num << 13 | num >> 19);
			}
			return (int)num;
		}

		// Token: 0x06004FF9 RID: 20473 RVA: 0x0018D4BC File Offset: 0x0018D4BC
		public int GetHashCode_TypeDef(Type a)
		{
			if (a == null)
			{
				return this.GetHashCodeGlobalType();
			}
			int num = this.GetHashCode_TypeName(a.Name);
			if (a.IsNested)
			{
				num += -1049070942;
			}
			else
			{
				num += this.GetHashCode_TypeNamespace(a.Namespace);
			}
			return num;
		}

		// Token: 0x06004FFA RID: 20474 RVA: 0x0018D510 File Offset: 0x0018D510
		private bool Equals(IList<TypeSig> a, IList<Type> b)
		{
			if (a == b)
			{
				return true;
			}
			if (a == null || b == null)
			{
				return false;
			}
			if (!this.recursionCounter.Increment())
			{
				return false;
			}
			bool result;
			if (a.Count != b.Count)
			{
				result = false;
			}
			else
			{
				int num = 0;
				while (num < a.Count && this.Equals(a[num], b[num]))
				{
					num++;
				}
				result = (num == a.Count);
			}
			this.recursionCounter.Decrement();
			return result;
		}

		// Token: 0x06004FFB RID: 20475 RVA: 0x0018D5A8 File Offset: 0x0018D5A8
		private bool Equals(ModuleDef a, Module b)
		{
			if (a == b)
			{
				return true;
			}
			if (a == null || b == null)
			{
				return false;
			}
			if (!this.MscorlibIsNotSpecial && SigComparer.IsCorLib(a) && SigComparer.IsCorLib(b))
			{
				return true;
			}
			if (!this.recursionCounter.Increment())
			{
				return false;
			}
			bool result = this.Equals(a, b) && this.Equals(a.Assembly, b.Assembly);
			this.recursionCounter.Decrement();
			return result;
		}

		// Token: 0x06004FFC RID: 20476 RVA: 0x0018D638 File Offset: 0x0018D638
		private bool Equals(FileDef a, Module b)
		{
			return a == b || (a != null && b != null && UTF8String.ToSystemStringOrEmpty(a.Name).Equals(b.Name, StringComparison.OrdinalIgnoreCase));
		}

		// Token: 0x06004FFD RID: 20477 RVA: 0x0018D668 File Offset: 0x0018D668
		private bool Equals(IModule a, Module b)
		{
			return a == b || (a != null && b != null && ((!this.MscorlibIsNotSpecial && SigComparer.IsCorLib(a) && SigComparer.IsCorLib(b)) || UTF8String.ToSystemStringOrEmpty(a.Name).Equals(b.ScopeName, StringComparison.OrdinalIgnoreCase)));
		}

		// Token: 0x06004FFE RID: 20478 RVA: 0x0018D6CC File Offset: 0x0018D6CC
		private bool Equals(IAssembly a, Assembly b)
		{
			if (a == b)
			{
				return true;
			}
			if (a == null || b == null)
			{
				return false;
			}
			if (!this.MscorlibIsNotSpecial && SigComparer.IsCorLib(a) && SigComparer.IsCorLib(b))
			{
				return true;
			}
			if (!this.recursionCounter.Increment())
			{
				return false;
			}
			AssemblyName name = b.GetName();
			bool result = UTF8String.ToSystemStringOrEmpty(a.Name).Equals(name.Name, StringComparison.OrdinalIgnoreCase) && (!this.CompareAssemblyPublicKeyToken || PublicKeyBase.TokenEquals(a.PublicKeyOrToken, new PublicKeyToken(name.GetPublicKeyToken()))) && (!this.CompareAssemblyVersion || Utils.Equals(a.Version, name.Version)) && (!this.CompareAssemblyLocale || Utils.LocaleEquals(a.Culture, name.CultureInfo.Name));
			this.recursionCounter.Decrement();
			return result;
		}

		// Token: 0x06004FFF RID: 20479 RVA: 0x0018D7CC File Offset: 0x0018D7CC
		private bool DeclaringTypeEquals(IMethod a, MethodBase b)
		{
			if (!this.CompareMethodFieldDeclaringType)
			{
				return true;
			}
			if (a == b)
			{
				return true;
			}
			if (a == null || b == null)
			{
				return false;
			}
			if (!this.recursionCounter.Increment())
			{
				return false;
			}
			MethodDef a2;
			bool result;
			MemberRef a3;
			if ((a2 = (a as MethodDef)) != null)
			{
				result = this.DeclaringTypeEquals(a2, b);
			}
			else if ((a3 = (a as MemberRef)) != null)
			{
				result = this.DeclaringTypeEquals(a3, b);
			}
			else
			{
				MethodSpec a4;
				result = ((a4 = (a as MethodSpec)) != null && this.DeclaringTypeEquals(a4, b));
			}
			this.recursionCounter.Decrement();
			return result;
		}

		// Token: 0x06005000 RID: 20480 RVA: 0x0018D874 File Offset: 0x0018D874
		private bool DeclaringTypeEquals(MethodDef a, MethodBase b)
		{
			return !this.CompareMethodFieldDeclaringType || a == b || (a != null && b != null && this.Equals(a.DeclaringType, b.DeclaringType));
		}

		// Token: 0x06005001 RID: 20481 RVA: 0x0018D8BC File Offset: 0x0018D8BC
		private bool DeclaringTypeEquals(MemberRef a, MethodBase b)
		{
			return !this.CompareMethodFieldDeclaringType || a == b || (a != null && b != null && this.Equals(a.Class, b.DeclaringType, b.Module));
		}

		// Token: 0x06005002 RID: 20482 RVA: 0x0018D90C File Offset: 0x0018D90C
		private bool DeclaringTypeEquals(MethodSpec a, MethodBase b)
		{
			return !this.CompareMethodFieldDeclaringType || a == b || (a != null && b != null && this.DeclaringTypeEquals(a.Method, b));
		}

		// Token: 0x06005003 RID: 20483 RVA: 0x0018D940 File Offset: 0x0018D940
		public bool Equals(MethodBase a, IMethod b)
		{
			return this.Equals(b, a);
		}

		// Token: 0x06005004 RID: 20484 RVA: 0x0018D94C File Offset: 0x0018D94C
		public bool Equals(IMethod a, MethodBase b)
		{
			if (a == b)
			{
				return true;
			}
			if (a == null || b == null)
			{
				return false;
			}
			if (!this.recursionCounter.Increment())
			{
				return false;
			}
			MethodDef a2;
			bool result;
			MemberRef a3;
			if ((a2 = (a as MethodDef)) != null)
			{
				result = this.Equals(a2, b);
			}
			else if ((a3 = (a as MemberRef)) != null)
			{
				result = this.Equals(a3, b);
			}
			else
			{
				MethodSpec a4;
				result = ((a4 = (a as MethodSpec)) != null && this.Equals(a4, b));
			}
			this.recursionCounter.Decrement();
			return result;
		}

		// Token: 0x06005005 RID: 20485 RVA: 0x0018D9E8 File Offset: 0x0018D9E8
		public bool Equals(MethodBase a, MethodDef b)
		{
			return this.Equals(b, a);
		}

		// Token: 0x06005006 RID: 20486 RVA: 0x0018D9F4 File Offset: 0x0018D9F4
		public bool Equals(MethodDef a, MethodBase b)
		{
			if (a == b)
			{
				return true;
			}
			if (a == null || b == null)
			{
				return false;
			}
			if (!this.recursionCounter.Increment())
			{
				return false;
			}
			bool result;
			if (!this.DontProjectWinMDRefs)
			{
				MemberRef memberRef = WinMDHelpers.ToCLR(a.Module ?? this.sourceModule, a);
				if (memberRef != null)
				{
					result = this.Equals(memberRef, b);
					goto IL_FF;
				}
			}
			MethodSig methodSig = a.MethodSig;
			result = (this.Equals_MethodFieldNames(a.Name, b.Name) && methodSig != null && ((methodSig.Generic && b.IsGenericMethodDefinition && b.IsGenericMethod) || (!methodSig.Generic && !b.IsGenericMethodDefinition && !b.IsGenericMethod)) && this.Equals(methodSig, b) && (!this.CompareMethodFieldDeclaringType || this.Equals(a.DeclaringType, b.DeclaringType)));
			IL_FF:
			this.recursionCounter.Decrement();
			return result;
		}

		// Token: 0x06005007 RID: 20487 RVA: 0x0018DB10 File Offset: 0x0018DB10
		public bool Equals(MethodBase a, MethodSig b)
		{
			return this.Equals(b, a);
		}

		// Token: 0x06005008 RID: 20488 RVA: 0x0018DB1C File Offset: 0x0018DB1C
		public bool Equals(MethodSig a, MethodBase b)
		{
			if (a == b)
			{
				return true;
			}
			if (a == null || b == null)
			{
				return false;
			}
			if (!this.recursionCounter.Increment())
			{
				return false;
			}
			bool result = SigComparer.Equals(a.GetCallingConvention(), b) && (this.DontCompareReturnType || this.ReturnTypeEquals(a.RetType, b)) && this.Equals(a.Params, b.GetParameters(), b.DeclaringType) && (!a.Generic || (ulong)a.GenParamCount == (ulong)((long)b.GetGenericArguments().Length));
			this.recursionCounter.Decrement();
			return result;
		}

		// Token: 0x06005009 RID: 20489 RVA: 0x0018DBD8 File Offset: 0x0018DBD8
		public bool Equals(MethodBase a, MemberRef b)
		{
			return this.Equals(b, a);
		}

		// Token: 0x0600500A RID: 20490 RVA: 0x0018DBE4 File Offset: 0x0018DBE4
		public bool Equals(MemberRef a, MethodBase b)
		{
			if (a == b)
			{
				return true;
			}
			if (a == null || b == null)
			{
				return false;
			}
			if (!this.recursionCounter.Increment())
			{
				return false;
			}
			if (!this.DontProjectWinMDRefs)
			{
				a = (WinMDHelpers.ToCLR(a.Module ?? this.sourceModule, a) ?? a);
			}
			bool flag;
			if (b.IsGenericMethod && !b.IsGenericMethodDefinition)
			{
				flag = (a.IsMethodRef && a.MethodSig.Generic);
				SigComparerOptions oldFlags = this.ClearOptions(SigComparerOptions.CompareMethodFieldDeclaringType);
				flag = (flag && this.Equals(a, b.Module.ResolveMethod(b.MetadataToken)));
				this.RestoreOptions(oldFlags);
				flag = (flag && this.DeclaringTypeEquals(a, b));
				flag = (flag && SigComparer.GenericMethodArgsEquals((int)a.MethodSig.GenParamCount, b.GetGenericArguments()));
			}
			else
			{
				MethodSig methodSig = a.MethodSig;
				flag = (this.Equals_MethodFieldNames(a.Name, b.Name) && methodSig != null && ((methodSig.Generic && b.IsGenericMethodDefinition && b.IsGenericMethod) || (!methodSig.Generic && !b.IsGenericMethodDefinition && !b.IsGenericMethod)));
				GenericInstSig genericInstanceType;
				if (this.SubstituteGenericParameters && (genericInstanceType = SigComparer.GetGenericInstanceType(a.Class)) != null)
				{
					this.InitializeGenericArguments();
					this.genericArguments.PushTypeArgs(genericInstanceType.GenericArguments);
					flag = (flag && this.Equals(methodSig, b));
					this.genericArguments.PopTypeArgs();
				}
				else
				{
					flag = (flag && this.Equals(methodSig, b));
				}
				flag = (flag && (!this.CompareMethodFieldDeclaringType || this.Equals(a.Class, b.DeclaringType, b.Module)));
			}
			this.recursionCounter.Decrement();
			return flag;
		}

		// Token: 0x0600500B RID: 20491 RVA: 0x0018DE18 File Offset: 0x0018DE18
		private static bool GenericMethodArgsEquals(int numMethodArgs, IList<Type> methodGenArgs)
		{
			if (numMethodArgs != methodGenArgs.Count)
			{
				return false;
			}
			for (int i = 0; i < numMethodArgs; i++)
			{
				if (methodGenArgs[i].GetElementType2() != ElementType.MVar)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600500C RID: 20492 RVA: 0x0018DE5C File Offset: 0x0018DE5C
		private bool Equals(IMemberRefParent a, Type b, Module bModule)
		{
			if (a == null)
			{
				return false;
			}
			if (!this.recursionCounter.Increment())
			{
				return false;
			}
			ITypeDefOrRef a2;
			bool result;
			ModuleRef moduleRef;
			MethodDef methodDef;
			if ((a2 = (a as ITypeDefOrRef)) != null)
			{
				result = this.Equals(a2, b);
			}
			else if ((moduleRef = (a as ModuleRef)) != null)
			{
				ModuleDef module = moduleRef.Module;
				result = (b == null && this.Equals(moduleRef, bModule) && this.Equals((module != null) ? module.Assembly : null, bModule.Assembly));
			}
			else if ((methodDef = (a as MethodDef)) != null)
			{
				result = this.Equals(methodDef.DeclaringType, b);
			}
			else
			{
				TypeDef typeDef;
				result = (b == null && (typeDef = (a as TypeDef)) != null && typeDef.IsGlobalModuleType);
			}
			this.recursionCounter.Decrement();
			return result;
		}

		// Token: 0x0600500D RID: 20493 RVA: 0x0018DF44 File Offset: 0x0018DF44
		public bool Equals(MethodBase a, MethodSpec b)
		{
			return this.Equals(b, a);
		}

		// Token: 0x0600500E RID: 20494 RVA: 0x0018DF50 File Offset: 0x0018DF50
		public bool Equals(MethodSpec a, MethodBase b)
		{
			if (a == b)
			{
				return true;
			}
			if (a == null || b == null)
			{
				return false;
			}
			if (!this.recursionCounter.Increment())
			{
				return false;
			}
			bool flag = b.IsGenericMethod && !b.IsGenericMethodDefinition;
			SigComparerOptions oldFlags = this.ClearOptions(SigComparerOptions.CompareMethodFieldDeclaringType);
			bool flag2 = flag && this.Equals(a.Method, b.Module.ResolveMethod(b.MetadataToken));
			this.RestoreOptions(oldFlags);
			bool flag3 = flag2 && this.DeclaringTypeEquals(a.Method, b);
			GenericInstMethodSig genericInstMethodSig = a.GenericInstMethodSig;
			bool result = flag3 && genericInstMethodSig != null && this.Equals(genericInstMethodSig.GenericArguments, b.GetGenericArguments());
			this.recursionCounter.Decrement();
			return result;
		}

		// Token: 0x0600500F RID: 20495 RVA: 0x0018E028 File Offset: 0x0018E028
		public int GetHashCode(MethodBase a)
		{
			if (a == null)
			{
				return 0;
			}
			if (!this.recursionCounter.Increment())
			{
				return 0;
			}
			int num = this.GetHashCode_MethodFieldName(a.Name) + this.GetHashCode_MethodSig(a);
			if (this.CompareMethodFieldDeclaringType)
			{
				num += this.GetHashCode(a.DeclaringType);
			}
			this.recursionCounter.Decrement();
			return num;
		}

		// Token: 0x06005010 RID: 20496 RVA: 0x0018E090 File Offset: 0x0018E090
		private int GetHashCode_MethodSig(MethodBase a)
		{
			if (a == null)
			{
				return 0;
			}
			if (!this.recursionCounter.Increment())
			{
				return 0;
			}
			int num = SigComparer.GetHashCode_CallingConvention(a.CallingConvention, a.IsGenericMethod) + this.GetHashCode(a.GetParameters(), a.DeclaringType);
			if (!this.DontCompareReturnType)
			{
				num += this.GetHashCode_ReturnType(a);
			}
			if (a.IsGenericMethod)
			{
				num += SigComparer.GetHashCode_ElementType_MVar(a.GetGenericArguments().Length);
			}
			this.recursionCounter.Decrement();
			return num;
		}

		// Token: 0x06005011 RID: 20497 RVA: 0x0018E11C File Offset: 0x0018E11C
		private int GetHashCode(IList<ParameterInfo> a, Type declaringType)
		{
			if (a == null)
			{
				return 0;
			}
			if (!this.recursionCounter.Increment())
			{
				return 0;
			}
			uint num = 0U;
			for (int i = 0; i < a.Count; i++)
			{
				num += (uint)this.GetHashCode(a[i], declaringType);
				num = (num << 13 | num >> 19);
			}
			this.recursionCounter.Decrement();
			return (int)num;
		}

		// Token: 0x06005012 RID: 20498 RVA: 0x0018E184 File Offset: 0x0018E184
		private int GetHashCode_ReturnType(MethodBase a)
		{
			MethodInfo methodInfo = a as MethodInfo;
			if (methodInfo != null)
			{
				return this.GetHashCode(methodInfo.ReturnParameter, a.DeclaringType);
			}
			return this.GetHashCode(typeof(void));
		}

		// Token: 0x06005013 RID: 20499 RVA: 0x0018E1C8 File Offset: 0x0018E1C8
		private int GetHashCode(ParameterInfo a, Type declaringType)
		{
			return this.GetHashCode(a.ParameterType, declaringType.MustTreatTypeAsGenericInstType(a.ParameterType));
		}

		// Token: 0x06005014 RID: 20500 RVA: 0x0018E1F4 File Offset: 0x0018E1F4
		private int GetHashCode(Type a, Type declaringType)
		{
			return this.GetHashCode(a, declaringType.MustTreatTypeAsGenericInstType(a));
		}

		// Token: 0x06005015 RID: 20501 RVA: 0x0018E204 File Offset: 0x0018E204
		private static bool Equals(CallingConvention a, MethodBase b)
		{
			CallingConventions callingConvention = b.CallingConvention;
			if ((a & CallingConvention.Generic) > CallingConvention.Default != b.IsGenericMethod)
			{
				return false;
			}
			if ((a & CallingConvention.HasThis) > CallingConvention.Default != (callingConvention & CallingConventions.HasThis) > (CallingConventions)0)
			{
				return false;
			}
			if ((a & CallingConvention.ExplicitThis) > CallingConvention.Default != (callingConvention & CallingConventions.ExplicitThis) > (CallingConventions)0)
			{
				return false;
			}
			CallingConvention callingConvention2 = a & CallingConvention.Mask;
			switch (callingConvention & CallingConventions.Any)
			{
			case CallingConventions.Standard:
				if (callingConvention2 == CallingConvention.VarArg || callingConvention2 == CallingConvention.NativeVarArg)
				{
					return false;
				}
				break;
			case CallingConventions.VarArgs:
				if (callingConvention2 != CallingConvention.VarArg && callingConvention2 != CallingConvention.NativeVarArg)
				{
					return false;
				}
				break;
			}
			return true;
		}

		// Token: 0x06005016 RID: 20502 RVA: 0x0018E2A0 File Offset: 0x0018E2A0
		private static int GetHashCode_CallingConvention(CallingConventions a, bool isGeneric)
		{
			CallingConvention callingConvention = CallingConvention.Default;
			if (isGeneric)
			{
				callingConvention |= CallingConvention.Generic;
			}
			if ((a & CallingConventions.HasThis) != (CallingConventions)0)
			{
				callingConvention |= CallingConvention.HasThis;
			}
			if ((a & CallingConventions.ExplicitThis) != (CallingConventions)0)
			{
				callingConvention |= CallingConvention.ExplicitThis;
			}
			return (int)callingConvention;
		}

		// Token: 0x06005017 RID: 20503 RVA: 0x0018E2DC File Offset: 0x0018E2DC
		private bool ReturnTypeEquals(TypeSig a, MethodBase b)
		{
			if (a == b)
			{
				return true;
			}
			if (a == null || b == null)
			{
				return false;
			}
			if (!this.recursionCounter.Increment())
			{
				return false;
			}
			MethodInfo methodInfo = b as MethodInfo;
			bool result;
			if (methodInfo != null)
			{
				result = this.Equals(a, methodInfo.ReturnParameter, b.DeclaringType);
			}
			else
			{
				result = (b is ConstructorInfo && SigComparer.IsSystemVoid(a));
			}
			this.recursionCounter.Decrement();
			return result;
		}

		// Token: 0x06005018 RID: 20504 RVA: 0x0018E364 File Offset: 0x0018E364
		private static bool IsSystemVoid(TypeSig a)
		{
			return a.RemovePinnedAndModifiers().GetElementType() == ElementType.Void;
		}

		// Token: 0x06005019 RID: 20505 RVA: 0x0018E374 File Offset: 0x0018E374
		private bool Equals(IList<TypeSig> a, IList<ParameterInfo> b, Type declaringType)
		{
			if (a == b)
			{
				return true;
			}
			if (a == null || b == null)
			{
				return false;
			}
			if (!this.recursionCounter.Increment())
			{
				return false;
			}
			bool result;
			if (a.Count != b.Count)
			{
				result = false;
			}
			else
			{
				int num = 0;
				while (num < a.Count && this.Equals(a[num], b[num], declaringType))
				{
					num++;
				}
				result = (num == a.Count);
			}
			this.recursionCounter.Decrement();
			return result;
		}

		// Token: 0x0600501A RID: 20506 RVA: 0x0018E40C File Offset: 0x0018E40C
		private bool Equals(TypeSig a, ParameterInfo b, Type declaringType)
		{
			if (a == b)
			{
				return true;
			}
			if (a == null || b == null)
			{
				return false;
			}
			if (!this.recursionCounter.Increment())
			{
				return false;
			}
			TypeSig a2;
			bool result = this.ModifiersEquals(a, b.GetRequiredCustomModifiers(), b.GetOptionalCustomModifiers(), out a2) && this.Equals(a2, b.ParameterType, declaringType.MustTreatTypeAsGenericInstType(b.ParameterType));
			this.recursionCounter.Decrement();
			return result;
		}

		// Token: 0x0600501B RID: 20507 RVA: 0x0018E48C File Offset: 0x0018E48C
		private bool ModifiersEquals(TypeSig a, IList<Type> reqMods2, IList<Type> optMods2, out TypeSig aAfterModifiers)
		{
			aAfterModifiers = a;
			if (!(a is ModifierSig))
			{
				return reqMods2.Count == 0 && optMods2.Count == 0;
			}
			if (!this.recursionCounter.Increment())
			{
				return false;
			}
			List<ITypeDefOrRef> list = new List<ITypeDefOrRef>(reqMods2.Count);
			List<ITypeDefOrRef> list2 = new List<ITypeDefOrRef>(optMods2.Count);
			for (;;)
			{
				ModifierSig modifierSig = aAfterModifiers as ModifierSig;
				if (modifierSig == null)
				{
					break;
				}
				if (modifierSig is CModOptSig)
				{
					list2.Add(modifierSig.Modifier);
				}
				else
				{
					list.Add(modifierSig.Modifier);
				}
				aAfterModifiers = aAfterModifiers.Next;
			}
			list2.Reverse();
			list.Reverse();
			bool result = list.Count == reqMods2.Count && list2.Count == optMods2.Count && this.ModifiersEquals(list, reqMods2) && this.ModifiersEquals(list2, optMods2);
			this.recursionCounter.Decrement();
			return result;
		}

		// Token: 0x0600501C RID: 20508 RVA: 0x0018E588 File Offset: 0x0018E588
		private bool ModifiersEquals(IList<ITypeDefOrRef> a, IList<Type> b)
		{
			if (a == b)
			{
				return true;
			}
			if (a == null || b == null)
			{
				return false;
			}
			if (!this.recursionCounter.Increment())
			{
				return false;
			}
			bool result;
			if (a.Count != b.Count)
			{
				result = false;
			}
			else
			{
				int num = 0;
				while (num < b.Count && this.Equals(a[num], b[num]))
				{
					num++;
				}
				result = (num == b.Count);
			}
			this.recursionCounter.Decrement();
			return result;
		}

		// Token: 0x0600501D RID: 20509 RVA: 0x0018E620 File Offset: 0x0018E620
		public bool Equals(FieldInfo a, IField b)
		{
			return this.Equals(b, a);
		}

		// Token: 0x0600501E RID: 20510 RVA: 0x0018E62C File Offset: 0x0018E62C
		public bool Equals(IField a, FieldInfo b)
		{
			if (a == b)
			{
				return true;
			}
			if (a == null || b == null)
			{
				return false;
			}
			if (!this.recursionCounter.Increment())
			{
				return false;
			}
			FieldDef a2;
			bool result;
			if ((a2 = (a as FieldDef)) != null)
			{
				result = this.Equals(a2, b);
			}
			else
			{
				MemberRef a3;
				result = ((a3 = (a as MemberRef)) != null && this.Equals(a3, b));
			}
			this.recursionCounter.Decrement();
			return result;
		}

		// Token: 0x0600501F RID: 20511 RVA: 0x0018E6AC File Offset: 0x0018E6AC
		public bool Equals(FieldInfo a, FieldDef b)
		{
			return this.Equals(b, a);
		}

		// Token: 0x06005020 RID: 20512 RVA: 0x0018E6B8 File Offset: 0x0018E6B8
		public bool Equals(FieldDef a, FieldInfo b)
		{
			if (a == b)
			{
				return true;
			}
			if (a == null || b == null)
			{
				return false;
			}
			if (!this.recursionCounter.Increment())
			{
				return false;
			}
			bool result = this.Equals_MethodFieldNames(a.Name, b.Name) && this.Equals(a.FieldSig, b) && (!this.CompareMethodFieldDeclaringType || this.Equals(a.DeclaringType, b.DeclaringType));
			this.recursionCounter.Decrement();
			return result;
		}

		// Token: 0x06005021 RID: 20513 RVA: 0x0018E750 File Offset: 0x0018E750
		private bool Equals(FieldSig a, FieldInfo b)
		{
			if (a == b)
			{
				return true;
			}
			if (a == null || b == null)
			{
				return false;
			}
			if (!this.recursionCounter.Increment())
			{
				return false;
			}
			TypeSig a2;
			bool result = this.ModifiersEquals(a.Type, b.GetRequiredCustomModifiers(), b.GetOptionalCustomModifiers(), out a2) && this.Equals(a2, b.FieldType, b.DeclaringType.MustTreatTypeAsGenericInstType(b.FieldType));
			this.recursionCounter.Decrement();
			return result;
		}

		// Token: 0x06005022 RID: 20514 RVA: 0x0018E7D8 File Offset: 0x0018E7D8
		public bool Equals(FieldInfo a, MemberRef b)
		{
			return this.Equals(b, a);
		}

		// Token: 0x06005023 RID: 20515 RVA: 0x0018E7E4 File Offset: 0x0018E7E4
		public bool Equals(MemberRef a, FieldInfo b)
		{
			if (a == b)
			{
				return true;
			}
			if (a == null || b == null)
			{
				return false;
			}
			if (!this.recursionCounter.Increment())
			{
				return false;
			}
			bool flag = this.Equals_MethodFieldNames(a.Name, b.Name);
			GenericInstSig genericInstanceType;
			if (this.SubstituteGenericParameters && (genericInstanceType = SigComparer.GetGenericInstanceType(a.Class)) != null)
			{
				this.InitializeGenericArguments();
				this.genericArguments.PushTypeArgs(genericInstanceType.GenericArguments);
				flag = (flag && this.Equals(a.FieldSig, b));
				this.genericArguments.PopTypeArgs();
			}
			else
			{
				flag = (flag && this.Equals(a.FieldSig, b));
			}
			flag = (flag && (!this.CompareMethodFieldDeclaringType || this.Equals(a.Class, b.DeclaringType, b.Module)));
			this.recursionCounter.Decrement();
			return flag;
		}

		// Token: 0x06005024 RID: 20516 RVA: 0x0018E8EC File Offset: 0x0018E8EC
		public int GetHashCode(FieldInfo a)
		{
			if (a == null)
			{
				return 0;
			}
			if (!this.recursionCounter.Increment())
			{
				return 0;
			}
			int num = this.GetHashCode_MethodFieldName(a.Name) + this.GetHashCode_FieldSig(a);
			if (this.CompareMethodFieldDeclaringType)
			{
				num += this.GetHashCode(a.DeclaringType);
			}
			this.recursionCounter.Decrement();
			return num;
		}

		// Token: 0x06005025 RID: 20517 RVA: 0x0018E954 File Offset: 0x0018E954
		private int GetHashCode_FieldSig(FieldInfo a)
		{
			if (a == null)
			{
				return 0;
			}
			if (!this.recursionCounter.Increment())
			{
				return 0;
			}
			int result = SigComparer.GetHashCode_CallingConvention((CallingConventions)0, false) + this.GetHashCode(a.FieldType, a.DeclaringType);
			this.recursionCounter.Decrement();
			return result;
		}

		// Token: 0x06005026 RID: 20518 RVA: 0x0018E9A4 File Offset: 0x0018E9A4
		public bool Equals(PropertyDef a, PropertyInfo b)
		{
			if (a == b)
			{
				return true;
			}
			if (a == null || b == null)
			{
				return false;
			}
			if (!this.recursionCounter.Increment())
			{
				return false;
			}
			bool result = this.Equals_PropertyNames(a.Name, b.Name) && this.Equals(a.PropertySig, b) && (!this.ComparePropertyDeclaringType || this.Equals(a.DeclaringType, b.DeclaringType));
			this.recursionCounter.Decrement();
			return result;
		}

		// Token: 0x06005027 RID: 20519 RVA: 0x0018EA3C File Offset: 0x0018EA3C
		private bool Equals(PropertySig a, PropertyInfo b)
		{
			if (a == b)
			{
				return true;
			}
			if (a == null || b == null)
			{
				return false;
			}
			if (!this.recursionCounter.Increment())
			{
				return false;
			}
			TypeSig a2;
			bool result = this.ModifiersEquals(a.RetType, b.GetRequiredCustomModifiers(), b.GetOptionalCustomModifiers(), out a2) && this.Equals(a2, b.PropertyType, b.DeclaringType.MustTreatTypeAsGenericInstType(b.PropertyType));
			this.recursionCounter.Decrement();
			return result;
		}

		// Token: 0x06005028 RID: 20520 RVA: 0x0018EAC4 File Offset: 0x0018EAC4
		public int GetHashCode(PropertyInfo a)
		{
			if (a == null)
			{
				return 0;
			}
			if (!this.recursionCounter.Increment())
			{
				return 0;
			}
			int num = this.GetHashCode_PropertyName(a.Name) + this.GetHashCode(a.PropertyType, a.DeclaringType);
			if (this.ComparePropertyDeclaringType)
			{
				num += this.GetHashCode(a.DeclaringType);
			}
			this.recursionCounter.Decrement();
			return num;
		}

		// Token: 0x06005029 RID: 20521 RVA: 0x0018EB38 File Offset: 0x0018EB38
		public bool Equals(EventDef a, EventInfo b)
		{
			if (a == b)
			{
				return true;
			}
			if (a == null || b == null)
			{
				return false;
			}
			if (!this.recursionCounter.Increment())
			{
				return false;
			}
			bool result = this.Equals_EventNames(a.Name, b.Name) && this.Equals(a.EventType, b.EventHandlerType, b.DeclaringType.MustTreatTypeAsGenericInstType(b.EventHandlerType)) && (!this.CompareEventDeclaringType || this.Equals(a.DeclaringType, b.DeclaringType));
			this.recursionCounter.Decrement();
			return result;
		}

		// Token: 0x0600502A RID: 20522 RVA: 0x0018EBE8 File Offset: 0x0018EBE8
		public int GetHashCode(EventInfo a)
		{
			if (a == null)
			{
				return 0;
			}
			if (!this.recursionCounter.Increment())
			{
				return 0;
			}
			int num = this.GetHashCode_EventName(a.Name) + this.GetHashCode(a.EventHandlerType, a.DeclaringType);
			if (this.CompareEventDeclaringType)
			{
				num += this.GetHashCode(a.DeclaringType);
			}
			this.recursionCounter.Decrement();
			return num;
		}

		// Token: 0x0600502B RID: 20523 RVA: 0x0018EC5C File Offset: 0x0018EC5C
		public override string ToString()
		{
			return string.Format("{0} - {1}", this.recursionCounter, this.options);
		}

		// Token: 0x04002739 RID: 10041
		private const SigComparerOptions SigComparerOptions_SubstituteGenericParameters = (SigComparerOptions)1024U;

		// Token: 0x0400273A RID: 10042
		private const int HASHCODE_MAGIC_GLOBAL_TYPE = 1654396648;

		// Token: 0x0400273B RID: 10043
		private const int HASHCODE_MAGIC_NESTED_TYPE = -1049070942;

		// Token: 0x0400273C RID: 10044
		private const int HASHCODE_MAGIC_ET_MODULE = -299744851;

		// Token: 0x0400273D RID: 10045
		private const int HASHCODE_MAGIC_ET_VALUEARRAY = -674970533;

		// Token: 0x0400273E RID: 10046
		private const int HASHCODE_MAGIC_ET_GENERICINST = -2050514639;

		// Token: 0x0400273F RID: 10047
		private const int HASHCODE_MAGIC_ET_VAR = 1288450097;

		// Token: 0x04002740 RID: 10048
		private const int HASHCODE_MAGIC_ET_MVAR = -990598495;

		// Token: 0x04002741 RID: 10049
		private const int HASHCODE_MAGIC_ET_ARRAY = -96331531;

		// Token: 0x04002742 RID: 10050
		private const int HASHCODE_MAGIC_ET_SZARRAY = 871833535;

		// Token: 0x04002743 RID: 10051
		private const int HASHCODE_MAGIC_ET_BYREF = -634749586;

		// Token: 0x04002744 RID: 10052
		private const int HASHCODE_MAGIC_ET_PTR = 1976400808;

		// Token: 0x04002745 RID: 10053
		private const int HASHCODE_MAGIC_ET_SENTINEL = 68439620;

		// Token: 0x04002746 RID: 10054
		private RecursionCounter recursionCounter;

		// Token: 0x04002747 RID: 10055
		private SigComparerOptions options;

		// Token: 0x04002748 RID: 10056
		private GenericArguments genericArguments;

		// Token: 0x04002749 RID: 10057
		private readonly ModuleDef sourceModule;
	}
}
