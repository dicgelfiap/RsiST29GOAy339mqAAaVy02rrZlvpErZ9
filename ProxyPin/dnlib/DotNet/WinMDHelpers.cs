using System;
using System.Collections.Generic;

namespace dnlib.DotNet
{
	// Token: 0x0200088B RID: 2187
	internal static class WinMDHelpers
	{
		// Token: 0x060053B7 RID: 21431 RVA: 0x00197B20 File Offset: 0x00197B20
		static WinMDHelpers()
		{
			foreach (WinMDHelpers.ProjectedClass projectedClass in WinMDHelpers.ProjectedClasses)
			{
				WinMDHelpers.winMDToCLR.Add(projectedClass.WinMDClass, projectedClass);
			}
		}

		// Token: 0x060053B8 RID: 21432 RVA: 0x001982D0 File Offset: 0x001982D0
		private static AssemblyRef ToCLR(ModuleDef module, ref UTF8String ns, ref UTF8String name)
		{
			WinMDHelpers.ProjectedClass projectedClass;
			if (!WinMDHelpers.winMDToCLR.TryGetValue(new WinMDHelpers.ClassName(ns, name, false), out projectedClass))
			{
				return null;
			}
			ns = projectedClass.ClrClass.Namespace;
			name = projectedClass.ClrClass.Name;
			return WinMDHelpers.CreateAssembly(module, projectedClass.ContractAssembly);
		}

		// Token: 0x060053B9 RID: 21433 RVA: 0x00198324 File Offset: 0x00198324
		private static AssemblyRef CreateAssembly(ModuleDef module, ClrAssembly clrAsm)
		{
			AssemblyRef assemblyRef = (module != null) ? module.CorLibTypes.AssemblyRef : null;
			AssemblyRefUser assemblyRefUser = new AssemblyRefUser(WinMDHelpers.GetName(clrAsm), WinMDHelpers.contractAsmVersion, new PublicKeyToken(WinMDHelpers.GetPublicKeyToken(clrAsm)), UTF8String.Empty);
			if (assemblyRef != null && assemblyRef.Name == WinMDHelpers.mscorlibName && WinMDHelpers.IsValidMscorlibVersion(assemblyRef.Version))
			{
				assemblyRefUser.Version = assemblyRef.Version;
			}
			ModuleDefMD moduleDefMD = module as ModuleDefMD;
			if (moduleDefMD != null)
			{
				Version version = null;
				foreach (AssemblyRef assemblyRef2 in moduleDefMD.GetAssemblyRefs())
				{
					if (!assemblyRef2.IsContentTypeWindowsRuntime && !(assemblyRef2.Name != assemblyRefUser.Name) && !(assemblyRef2.Culture != assemblyRefUser.Culture) && PublicKeyBase.TokenEquals(assemblyRef2.PublicKeyOrToken, assemblyRefUser.PublicKeyOrToken) && WinMDHelpers.IsValidMscorlibVersion(assemblyRef2.Version) && (version == null || assemblyRef2.Version > version))
					{
						version = assemblyRef2.Version;
					}
				}
				if (version != null)
				{
					assemblyRefUser.Version = version;
				}
			}
			return assemblyRefUser;
		}

		// Token: 0x060053BA RID: 21434 RVA: 0x0019848C File Offset: 0x0019848C
		private static bool IsValidMscorlibVersion(Version version)
		{
			return version != null && version.Major <= 5;
		}

		// Token: 0x060053BB RID: 21435 RVA: 0x001984A4 File Offset: 0x001984A4
		private static UTF8String GetName(ClrAssembly clrAsm)
		{
			UTF8String result;
			switch (clrAsm)
			{
			case ClrAssembly.Mscorlib:
				result = WinMDHelpers.clrAsmName_Mscorlib;
				break;
			case ClrAssembly.SystemNumericsVectors:
				result = WinMDHelpers.clrAsmName_SystemNumericsVectors;
				break;
			case ClrAssembly.SystemObjectModel:
				result = WinMDHelpers.clrAsmName_SystemObjectModel;
				break;
			case ClrAssembly.SystemRuntime:
				result = WinMDHelpers.clrAsmName_SystemRuntime;
				break;
			case ClrAssembly.SystemRuntimeInteropServicesWindowsRuntime:
				result = WinMDHelpers.clrAsmName_SystemRuntimeInteropServicesWindowsRuntime;
				break;
			case ClrAssembly.SystemRuntimeWindowsRuntime:
				result = WinMDHelpers.clrAsmName_SystemRuntimeWindowsRuntime;
				break;
			case ClrAssembly.SystemRuntimeWindowsRuntimeUIXaml:
				result = WinMDHelpers.clrAsmName_SystemRuntimeWindowsRuntimeUIXaml;
				break;
			default:
				throw new InvalidOperationException();
			}
			return result;
		}

		// Token: 0x060053BC RID: 21436 RVA: 0x00198530 File Offset: 0x00198530
		private static byte[] GetPublicKeyToken(ClrAssembly clrAsm)
		{
			byte[] result;
			switch (clrAsm)
			{
			case ClrAssembly.Mscorlib:
				result = WinMDHelpers.neutralPublicKey;
				break;
			case ClrAssembly.SystemNumericsVectors:
				result = WinMDHelpers.contractPublicKeyToken;
				break;
			case ClrAssembly.SystemObjectModel:
				result = WinMDHelpers.contractPublicKeyToken;
				break;
			case ClrAssembly.SystemRuntime:
				result = WinMDHelpers.contractPublicKeyToken;
				break;
			case ClrAssembly.SystemRuntimeInteropServicesWindowsRuntime:
				result = WinMDHelpers.contractPublicKeyToken;
				break;
			case ClrAssembly.SystemRuntimeWindowsRuntime:
				result = WinMDHelpers.neutralPublicKey;
				break;
			case ClrAssembly.SystemRuntimeWindowsRuntimeUIXaml:
				result = WinMDHelpers.neutralPublicKey;
				break;
			default:
				throw new InvalidOperationException();
			}
			return result;
		}

		// Token: 0x060053BD RID: 21437 RVA: 0x001985BC File Offset: 0x001985BC
		public static TypeRef ToCLR(ModuleDef module, TypeDef td)
		{
			bool flag;
			return WinMDHelpers.ToCLR(module, td, out flag);
		}

		// Token: 0x060053BE RID: 21438 RVA: 0x001985D8 File Offset: 0x001985D8
		public static TypeRef ToCLR(ModuleDef module, TypeDef td, out bool isClrValueType)
		{
			isClrValueType = false;
			if (td == null || !td.IsWindowsRuntime)
			{
				return null;
			}
			IAssembly definitionAssembly = td.DefinitionAssembly;
			if (definitionAssembly == null || !definitionAssembly.IsContentTypeWindowsRuntime)
			{
				return null;
			}
			WinMDHelpers.ProjectedClass projectedClass;
			if (!WinMDHelpers.winMDToCLR.TryGetValue(new WinMDHelpers.ClassName(td.Namespace, td.Name, false), out projectedClass))
			{
				return null;
			}
			isClrValueType = projectedClass.ClrClass.IsValueType;
			return new TypeRefUser(module, projectedClass.ClrClass.Namespace, projectedClass.ClrClass.Name, WinMDHelpers.CreateAssembly(module, projectedClass.ContractAssembly));
		}

		// Token: 0x060053BF RID: 21439 RVA: 0x00198674 File Offset: 0x00198674
		public static TypeRef ToCLR(ModuleDef module, TypeRef tr)
		{
			bool flag;
			return WinMDHelpers.ToCLR(module, tr, out flag);
		}

		// Token: 0x060053C0 RID: 21440 RVA: 0x00198690 File Offset: 0x00198690
		public static TypeRef ToCLR(ModuleDef module, TypeRef tr, out bool isClrValueType)
		{
			isClrValueType = false;
			if (tr == null)
			{
				return null;
			}
			IAssembly definitionAssembly = tr.DefinitionAssembly;
			if (definitionAssembly == null || !definitionAssembly.IsContentTypeWindowsRuntime)
			{
				return null;
			}
			if (tr.DeclaringType != null)
			{
				return null;
			}
			WinMDHelpers.ProjectedClass projectedClass;
			if (!WinMDHelpers.winMDToCLR.TryGetValue(new WinMDHelpers.ClassName(tr.Namespace, tr.Name, false), out projectedClass))
			{
				return null;
			}
			isClrValueType = projectedClass.ClrClass.IsValueType;
			return new TypeRefUser(module, projectedClass.ClrClass.Namespace, projectedClass.ClrClass.Name, WinMDHelpers.CreateAssembly(module, projectedClass.ContractAssembly));
		}

		// Token: 0x060053C1 RID: 21441 RVA: 0x00198730 File Offset: 0x00198730
		public static ExportedType ToCLR(ModuleDef module, ExportedType et)
		{
			if (et == null)
			{
				return null;
			}
			IAssembly definitionAssembly = et.DefinitionAssembly;
			if (definitionAssembly == null || !definitionAssembly.IsContentTypeWindowsRuntime)
			{
				return null;
			}
			if (et.DeclaringType != null)
			{
				return null;
			}
			WinMDHelpers.ProjectedClass projectedClass;
			if (!WinMDHelpers.winMDToCLR.TryGetValue(new WinMDHelpers.ClassName(et.TypeNamespace, et.TypeName, false), out projectedClass))
			{
				return null;
			}
			return new ExportedTypeUser(module, 0U, projectedClass.ClrClass.Namespace, projectedClass.ClrClass.Name, et.Attributes, WinMDHelpers.CreateAssembly(module, projectedClass.ContractAssembly));
		}

		// Token: 0x060053C2 RID: 21442 RVA: 0x001987C4 File Offset: 0x001987C4
		public static TypeSig ToCLR(ModuleDef module, TypeSig ts)
		{
			if (ts == null)
			{
				return null;
			}
			ElementType elementType = ts.ElementType;
			if (elementType != ElementType.Class && elementType != ElementType.ValueType)
			{
				return null;
			}
			ITypeDefOrRef typeDefOrRef = ((ClassOrValueTypeSig)ts).TypeDefOrRef;
			TypeDef typeDef = typeDefOrRef as TypeDef;
			bool flag;
			TypeRef typeRef;
			if (typeDef != null)
			{
				typeRef = WinMDHelpers.ToCLR(module, typeDef, out flag);
				if (typeRef == null)
				{
					return null;
				}
			}
			else
			{
				TypeRef tr;
				if ((tr = (typeDefOrRef as TypeRef)) == null)
				{
					return null;
				}
				typeRef = WinMDHelpers.ToCLR(module, tr, out flag);
				if (typeRef == null)
				{
					return null;
				}
			}
			if (!flag)
			{
				return new ClassSig(typeRef);
			}
			return new ValueTypeSig(typeRef);
		}

		// Token: 0x060053C3 RID: 21443 RVA: 0x00198860 File Offset: 0x00198860
		public static MemberRef ToCLR(ModuleDef module, MemberRef mr)
		{
			if (mr == null)
			{
				return null;
			}
			if (mr.Name != WinMDHelpers.CloseName)
			{
				return null;
			}
			MethodSig methodSig = mr.MethodSig;
			if (methodSig == null)
			{
				return null;
			}
			IMemberRefParent @class = mr.Class;
			TypeRef typeRef = @class as TypeRef;
			IMemberRefParent class2;
			if (typeRef != null)
			{
				TypeRef typeRef2 = WinMDHelpers.ToCLR(module, typeRef);
				if (typeRef2 == null || !WinMDHelpers.IsIDisposable(typeRef2))
				{
					return null;
				}
				class2 = typeRef2;
			}
			else
			{
				TypeSpec typeSpec;
				if ((typeSpec = (@class as TypeSpec)) == null)
				{
					return null;
				}
				GenericInstSig genericInstSig = typeSpec.TypeSig as GenericInstSig;
				if (genericInstSig == null || !(genericInstSig.GenericType is ClassSig))
				{
					return null;
				}
				typeRef = genericInstSig.GenericType.TypeRef;
				if (typeRef == null)
				{
					return null;
				}
				bool flag;
				TypeRef typeRef3 = WinMDHelpers.ToCLR(module, typeRef, out flag);
				if (typeRef3 == null || !WinMDHelpers.IsIDisposable(typeRef3))
				{
					return null;
				}
				class2 = new TypeSpecUser(new GenericInstSig(flag ? new ValueTypeSig(typeRef3) : new ClassSig(typeRef3), genericInstSig.GenericArguments));
			}
			return new MemberRefUser(mr.Module, WinMDHelpers.DisposeName, methodSig, class2);
		}

		// Token: 0x060053C4 RID: 21444 RVA: 0x00198988 File Offset: 0x00198988
		private static bool IsIDisposable(TypeRef tr)
		{
			return tr.Name == WinMDHelpers.IDisposableName && tr.Namespace == WinMDHelpers.IDisposableNamespace;
		}

		// Token: 0x060053C5 RID: 21445 RVA: 0x001989B4 File Offset: 0x001989B4
		public static MemberRef ToCLR(ModuleDef module, MethodDef md)
		{
			if (md == null)
			{
				return null;
			}
			if (md.Name != WinMDHelpers.CloseName)
			{
				return null;
			}
			TypeDef declaringType = md.DeclaringType;
			if (declaringType == null)
			{
				return null;
			}
			TypeRef typeRef = WinMDHelpers.ToCLR(module, declaringType);
			if (typeRef == null || !WinMDHelpers.IsIDisposable(typeRef))
			{
				return null;
			}
			return new MemberRefUser(md.Module, WinMDHelpers.DisposeName, md.MethodSig, typeRef);
		}

		// Token: 0x04002834 RID: 10292
		private static readonly WinMDHelpers.ProjectedClass[] ProjectedClasses = new WinMDHelpers.ProjectedClass[]
		{
			new WinMDHelpers.ProjectedClass("Windows.Foundation.Metadata", "AttributeUsageAttribute", "System", "AttributeUsageAttribute", ClrAssembly.Mscorlib, ClrAssembly.SystemRuntime, false, false),
			new WinMDHelpers.ProjectedClass("Windows.Foundation.Metadata", "AttributeTargets", "System", "AttributeTargets", ClrAssembly.Mscorlib, ClrAssembly.SystemRuntime, true, true),
			new WinMDHelpers.ProjectedClass("Windows.UI", "Color", "Windows.UI", "Color", ClrAssembly.SystemRuntimeWindowsRuntime, ClrAssembly.SystemRuntimeWindowsRuntime, true, true),
			new WinMDHelpers.ProjectedClass("Windows.Foundation", "DateTime", "System", "DateTimeOffset", ClrAssembly.Mscorlib, ClrAssembly.SystemRuntime, true, true),
			new WinMDHelpers.ProjectedClass("Windows.Foundation", "EventHandler`1", "System", "EventHandler`1", ClrAssembly.Mscorlib, ClrAssembly.SystemRuntime, false, false),
			new WinMDHelpers.ProjectedClass("Windows.Foundation", "EventRegistrationToken", "System.Runtime.InteropServices.WindowsRuntime", "EventRegistrationToken", ClrAssembly.Mscorlib, ClrAssembly.SystemRuntimeInteropServicesWindowsRuntime, true, true),
			new WinMDHelpers.ProjectedClass("Windows.Foundation", "HResult", "System", "Exception", ClrAssembly.Mscorlib, ClrAssembly.SystemRuntime, true, false),
			new WinMDHelpers.ProjectedClass("Windows.Foundation", "IReference`1", "System", "Nullable`1", ClrAssembly.Mscorlib, ClrAssembly.SystemRuntime, false, true),
			new WinMDHelpers.ProjectedClass("Windows.Foundation", "Point", "Windows.Foundation", "Point", ClrAssembly.SystemRuntimeWindowsRuntime, ClrAssembly.SystemRuntimeWindowsRuntime, true, true),
			new WinMDHelpers.ProjectedClass("Windows.Foundation", "Rect", "Windows.Foundation", "Rect", ClrAssembly.SystemRuntimeWindowsRuntime, ClrAssembly.SystemRuntimeWindowsRuntime, true, true),
			new WinMDHelpers.ProjectedClass("Windows.Foundation", "Size", "Windows.Foundation", "Size", ClrAssembly.SystemRuntimeWindowsRuntime, ClrAssembly.SystemRuntimeWindowsRuntime, true, true),
			new WinMDHelpers.ProjectedClass("Windows.Foundation", "TimeSpan", "System", "TimeSpan", ClrAssembly.Mscorlib, ClrAssembly.SystemRuntime, true, true),
			new WinMDHelpers.ProjectedClass("Windows.Foundation", "Uri", "System", "Uri", ClrAssembly.SystemRuntime, ClrAssembly.SystemRuntime, false, false),
			new WinMDHelpers.ProjectedClass("Windows.Foundation", "IClosable", "System", "IDisposable", ClrAssembly.Mscorlib, ClrAssembly.SystemRuntime, false, false),
			new WinMDHelpers.ProjectedClass("Windows.Foundation.Collections", "IIterable`1", "System.Collections.Generic", "IEnumerable`1", ClrAssembly.Mscorlib, ClrAssembly.SystemRuntime, false, false),
			new WinMDHelpers.ProjectedClass("Windows.Foundation.Collections", "IVector`1", "System.Collections.Generic", "IList`1", ClrAssembly.Mscorlib, ClrAssembly.SystemRuntime, false, false),
			new WinMDHelpers.ProjectedClass("Windows.Foundation.Collections", "IVectorView`1", "System.Collections.Generic", "IReadOnlyList`1", ClrAssembly.Mscorlib, ClrAssembly.SystemRuntime, false, false),
			new WinMDHelpers.ProjectedClass("Windows.Foundation.Collections", "IMap`2", "System.Collections.Generic", "IDictionary`2", ClrAssembly.Mscorlib, ClrAssembly.SystemRuntime, false, false),
			new WinMDHelpers.ProjectedClass("Windows.Foundation.Collections", "IMapView`2", "System.Collections.Generic", "IReadOnlyDictionary`2", ClrAssembly.Mscorlib, ClrAssembly.SystemRuntime, false, false),
			new WinMDHelpers.ProjectedClass("Windows.Foundation.Collections", "IKeyValuePair`2", "System.Collections.Generic", "KeyValuePair`2", ClrAssembly.Mscorlib, ClrAssembly.SystemRuntime, false, true),
			new WinMDHelpers.ProjectedClass("Windows.UI.Xaml.Input", "ICommand", "System.Windows.Input", "ICommand", ClrAssembly.SystemObjectModel, ClrAssembly.SystemObjectModel, false, false),
			new WinMDHelpers.ProjectedClass("Windows.UI.Xaml.Interop", "IBindableIterable", "System.Collections", "IEnumerable", ClrAssembly.Mscorlib, ClrAssembly.SystemRuntime, false, false),
			new WinMDHelpers.ProjectedClass("Windows.UI.Xaml.Interop", "IBindableVector", "System.Collections", "IList", ClrAssembly.Mscorlib, ClrAssembly.SystemRuntime, false, false),
			new WinMDHelpers.ProjectedClass("Windows.UI.Xaml.Interop", "INotifyCollectionChanged", "System.Collections.Specialized", "INotifyCollectionChanged", ClrAssembly.SystemObjectModel, ClrAssembly.SystemObjectModel, false, false),
			new WinMDHelpers.ProjectedClass("Windows.UI.Xaml.Interop", "NotifyCollectionChangedEventHandler", "System.Collections.Specialized", "NotifyCollectionChangedEventHandler", ClrAssembly.SystemObjectModel, ClrAssembly.SystemObjectModel, false, false),
			new WinMDHelpers.ProjectedClass("Windows.UI.Xaml.Interop", "NotifyCollectionChangedEventArgs", "System.Collections.Specialized", "NotifyCollectionChangedEventArgs", ClrAssembly.SystemObjectModel, ClrAssembly.SystemObjectModel, false, false),
			new WinMDHelpers.ProjectedClass("Windows.UI.Xaml.Interop", "NotifyCollectionChangedAction", "System.Collections.Specialized", "NotifyCollectionChangedAction", ClrAssembly.SystemObjectModel, ClrAssembly.SystemObjectModel, true, true),
			new WinMDHelpers.ProjectedClass("Windows.UI.Xaml.Data", "INotifyPropertyChanged", "System.ComponentModel", "INotifyPropertyChanged", ClrAssembly.SystemObjectModel, ClrAssembly.SystemObjectModel, false, false),
			new WinMDHelpers.ProjectedClass("Windows.UI.Xaml.Data", "PropertyChangedEventHandler", "System.ComponentModel", "PropertyChangedEventHandler", ClrAssembly.SystemObjectModel, ClrAssembly.SystemObjectModel, false, false),
			new WinMDHelpers.ProjectedClass("Windows.UI.Xaml.Data", "PropertyChangedEventArgs", "System.ComponentModel", "PropertyChangedEventArgs", ClrAssembly.SystemObjectModel, ClrAssembly.SystemObjectModel, false, false),
			new WinMDHelpers.ProjectedClass("Windows.UI.Xaml", "CornerRadius", "Windows.UI.Xaml", "CornerRadius", ClrAssembly.SystemRuntimeWindowsRuntimeUIXaml, ClrAssembly.SystemRuntimeWindowsRuntimeUIXaml, true, true),
			new WinMDHelpers.ProjectedClass("Windows.UI.Xaml", "Duration", "Windows.UI.Xaml", "Duration", ClrAssembly.SystemRuntimeWindowsRuntimeUIXaml, ClrAssembly.SystemRuntimeWindowsRuntimeUIXaml, true, true),
			new WinMDHelpers.ProjectedClass("Windows.UI.Xaml", "DurationType", "Windows.UI.Xaml", "DurationType", ClrAssembly.SystemRuntimeWindowsRuntimeUIXaml, ClrAssembly.SystemRuntimeWindowsRuntimeUIXaml, true, true),
			new WinMDHelpers.ProjectedClass("Windows.UI.Xaml", "GridLength", "Windows.UI.Xaml", "GridLength", ClrAssembly.SystemRuntimeWindowsRuntimeUIXaml, ClrAssembly.SystemRuntimeWindowsRuntimeUIXaml, true, true),
			new WinMDHelpers.ProjectedClass("Windows.UI.Xaml", "GridUnitType", "Windows.UI.Xaml", "GridUnitType", ClrAssembly.SystemRuntimeWindowsRuntimeUIXaml, ClrAssembly.SystemRuntimeWindowsRuntimeUIXaml, true, true),
			new WinMDHelpers.ProjectedClass("Windows.UI.Xaml", "Thickness", "Windows.UI.Xaml", "Thickness", ClrAssembly.SystemRuntimeWindowsRuntimeUIXaml, ClrAssembly.SystemRuntimeWindowsRuntimeUIXaml, true, true),
			new WinMDHelpers.ProjectedClass("Windows.UI.Xaml.Interop", "TypeName", "System", "Type", ClrAssembly.Mscorlib, ClrAssembly.SystemRuntime, true, false),
			new WinMDHelpers.ProjectedClass("Windows.UI.Xaml.Controls.Primitives", "GeneratorPosition", "Windows.UI.Xaml.Controls.Primitives", "GeneratorPosition", ClrAssembly.SystemRuntimeWindowsRuntimeUIXaml, ClrAssembly.SystemRuntimeWindowsRuntimeUIXaml, true, true),
			new WinMDHelpers.ProjectedClass("Windows.UI.Xaml.Media", "Matrix", "Windows.UI.Xaml.Media", "Matrix", ClrAssembly.SystemRuntimeWindowsRuntimeUIXaml, ClrAssembly.SystemRuntimeWindowsRuntimeUIXaml, true, true),
			new WinMDHelpers.ProjectedClass("Windows.UI.Xaml.Media.Animation", "KeyTime", "Windows.UI.Xaml.Media.Animation", "KeyTime", ClrAssembly.SystemRuntimeWindowsRuntimeUIXaml, ClrAssembly.SystemRuntimeWindowsRuntimeUIXaml, true, true),
			new WinMDHelpers.ProjectedClass("Windows.UI.Xaml.Media.Animation", "RepeatBehavior", "Windows.UI.Xaml.Media.Animation", "RepeatBehavior", ClrAssembly.SystemRuntimeWindowsRuntimeUIXaml, ClrAssembly.SystemRuntimeWindowsRuntimeUIXaml, true, true),
			new WinMDHelpers.ProjectedClass("Windows.UI.Xaml.Media.Animation", "RepeatBehaviorType", "Windows.UI.Xaml.Media.Animation", "RepeatBehaviorType", ClrAssembly.SystemRuntimeWindowsRuntimeUIXaml, ClrAssembly.SystemRuntimeWindowsRuntimeUIXaml, true, true),
			new WinMDHelpers.ProjectedClass("Windows.UI.Xaml.Media.Media3D", "Matrix3D", "Windows.UI.Xaml.Media.Media3D", "Matrix3D", ClrAssembly.SystemRuntimeWindowsRuntimeUIXaml, ClrAssembly.SystemRuntimeWindowsRuntimeUIXaml, true, true),
			new WinMDHelpers.ProjectedClass("Windows.Foundation.Numerics", "Vector2", "System.Numerics", "Vector2", ClrAssembly.SystemNumericsVectors, ClrAssembly.SystemNumericsVectors, true, true),
			new WinMDHelpers.ProjectedClass("Windows.Foundation.Numerics", "Vector3", "System.Numerics", "Vector3", ClrAssembly.SystemNumericsVectors, ClrAssembly.SystemNumericsVectors, true, true),
			new WinMDHelpers.ProjectedClass("Windows.Foundation.Numerics", "Vector4", "System.Numerics", "Vector4", ClrAssembly.SystemNumericsVectors, ClrAssembly.SystemNumericsVectors, true, true),
			new WinMDHelpers.ProjectedClass("Windows.Foundation.Numerics", "Matrix3x2", "System.Numerics", "Matrix3x2", ClrAssembly.SystemNumericsVectors, ClrAssembly.SystemNumericsVectors, true, true),
			new WinMDHelpers.ProjectedClass("Windows.Foundation.Numerics", "Matrix4x4", "System.Numerics", "Matrix4x4", ClrAssembly.SystemNumericsVectors, ClrAssembly.SystemNumericsVectors, true, true),
			new WinMDHelpers.ProjectedClass("Windows.Foundation.Numerics", "Plane", "System.Numerics", "Plane", ClrAssembly.SystemNumericsVectors, ClrAssembly.SystemNumericsVectors, true, true),
			new WinMDHelpers.ProjectedClass("Windows.Foundation.Numerics", "Quaternion", "System.Numerics", "Quaternion", ClrAssembly.SystemNumericsVectors, ClrAssembly.SystemNumericsVectors, true, true)
		};

		// Token: 0x04002835 RID: 10293
		private static readonly Dictionary<WinMDHelpers.ClassName, WinMDHelpers.ProjectedClass> winMDToCLR = new Dictionary<WinMDHelpers.ClassName, WinMDHelpers.ProjectedClass>();

		// Token: 0x04002836 RID: 10294
		private static readonly Version contractAsmVersion = new Version(4, 0, 0, 0);

		// Token: 0x04002837 RID: 10295
		private static readonly UTF8String mscorlibName = new UTF8String("mscorlib");

		// Token: 0x04002838 RID: 10296
		private static readonly UTF8String clrAsmName_Mscorlib = new UTF8String("mscorlib");

		// Token: 0x04002839 RID: 10297
		private static readonly UTF8String clrAsmName_SystemNumericsVectors = new UTF8String("System.Numerics.Vectors");

		// Token: 0x0400283A RID: 10298
		private static readonly UTF8String clrAsmName_SystemObjectModel = new UTF8String("System.ObjectModel");

		// Token: 0x0400283B RID: 10299
		private static readonly UTF8String clrAsmName_SystemRuntime = new UTF8String("System.Runtime");

		// Token: 0x0400283C RID: 10300
		private static readonly UTF8String clrAsmName_SystemRuntimeInteropServicesWindowsRuntime = new UTF8String("System.Runtime.InteropServices.WindowsRuntime");

		// Token: 0x0400283D RID: 10301
		private static readonly UTF8String clrAsmName_SystemRuntimeWindowsRuntime = new UTF8String("System.Runtime.WindowsRuntime");

		// Token: 0x0400283E RID: 10302
		private static readonly UTF8String clrAsmName_SystemRuntimeWindowsRuntimeUIXaml = new UTF8String("System.Runtime.WindowsRuntime.UI.Xaml");

		// Token: 0x0400283F RID: 10303
		private static readonly byte[] contractPublicKeyToken = new byte[]
		{
			176,
			63,
			95,
			127,
			17,
			213,
			10,
			58
		};

		// Token: 0x04002840 RID: 10304
		private static readonly byte[] neutralPublicKey = new byte[]
		{
			183,
			122,
			92,
			86,
			25,
			52,
			224,
			137
		};

		// Token: 0x04002841 RID: 10305
		private static readonly UTF8String CloseName = new UTF8String("Close");

		// Token: 0x04002842 RID: 10306
		private static readonly UTF8String DisposeName = new UTF8String("Dispose");

		// Token: 0x04002843 RID: 10307
		private static readonly UTF8String IDisposableNamespace = new UTF8String("System");

		// Token: 0x04002844 RID: 10308
		private static readonly UTF8String IDisposableName = new UTF8String("IDisposable");

		// Token: 0x0200100A RID: 4106
		private readonly struct ClassName : IEquatable<WinMDHelpers.ClassName>
		{
			// Token: 0x06008EF2 RID: 36594 RVA: 0x002AB024 File Offset: 0x002AB024
			public ClassName(UTF8String ns, UTF8String name, bool isValueType = false)
			{
				this.Namespace = ns;
				this.Name = name;
				this.IsValueType = isValueType;
			}

			// Token: 0x06008EF3 RID: 36595 RVA: 0x002AB03C File Offset: 0x002AB03C
			public ClassName(string ns, string name, bool isValueType = false)
			{
				this.Namespace = ns;
				this.Name = name;
				this.IsValueType = isValueType;
			}

			// Token: 0x06008EF4 RID: 36596 RVA: 0x002AB060 File Offset: 0x002AB060
			public static bool operator ==(WinMDHelpers.ClassName a, WinMDHelpers.ClassName b)
			{
				return a.Equals(b);
			}

			// Token: 0x06008EF5 RID: 36597 RVA: 0x002AB06C File Offset: 0x002AB06C
			public static bool operator !=(WinMDHelpers.ClassName a, WinMDHelpers.ClassName b)
			{
				return !a.Equals(b);
			}

			// Token: 0x06008EF6 RID: 36598 RVA: 0x002AB07C File Offset: 0x002AB07C
			public bool Equals(WinMDHelpers.ClassName other)
			{
				return UTF8String.Equals(this.Namespace, other.Namespace) && UTF8String.Equals(this.Name, other.Name);
			}

			// Token: 0x06008EF7 RID: 36599 RVA: 0x002AB0A8 File Offset: 0x002AB0A8
			public override bool Equals(object obj)
			{
				return obj is WinMDHelpers.ClassName && this.Equals((WinMDHelpers.ClassName)obj);
			}

			// Token: 0x06008EF8 RID: 36600 RVA: 0x002AB0C4 File Offset: 0x002AB0C4
			public override int GetHashCode()
			{
				return UTF8String.GetHashCode(this.Namespace) ^ UTF8String.GetHashCode(this.Name);
			}

			// Token: 0x06008EF9 RID: 36601 RVA: 0x002AB0E0 File Offset: 0x002AB0E0
			public override string ToString()
			{
				return string.Format("{0}.{1}", this.Namespace, this.Name);
			}

			// Token: 0x04004455 RID: 17493
			public readonly UTF8String Namespace;

			// Token: 0x04004456 RID: 17494
			public readonly UTF8String Name;

			// Token: 0x04004457 RID: 17495
			public readonly bool IsValueType;
		}

		// Token: 0x0200100B RID: 4107
		private sealed class ProjectedClass
		{
			// Token: 0x06008EFA RID: 36602 RVA: 0x002AB0F8 File Offset: 0x002AB0F8
			public ProjectedClass(string mdns, string mdname, string clrns, string clrname, ClrAssembly clrAsm, ClrAssembly contractAsm, bool winMDValueType, bool clrValueType)
			{
				this.WinMDClass = new WinMDHelpers.ClassName(mdns, mdname, winMDValueType);
				this.ClrClass = new WinMDHelpers.ClassName(clrns, clrname, clrValueType);
				this.ClrAssembly = clrAsm;
				this.ContractAssembly = contractAsm;
			}

			// Token: 0x06008EFB RID: 36603 RVA: 0x002AB130 File Offset: 0x002AB130
			public override string ToString()
			{
				return string.Format("{0} <-> {1}, {2}", this.WinMDClass, this.ClrClass, WinMDHelpers.CreateAssembly(null, this.ContractAssembly));
			}

			// Token: 0x04004458 RID: 17496
			public readonly WinMDHelpers.ClassName WinMDClass;

			// Token: 0x04004459 RID: 17497
			public readonly WinMDHelpers.ClassName ClrClass;

			// Token: 0x0400445A RID: 17498
			public readonly ClrAssembly ClrAssembly;

			// Token: 0x0400445B RID: 17499
			public readonly ClrAssembly ContractAssembly;
		}
	}
}
