using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x0200083B RID: 2107
	[ComVisible(true)]
	public sealed class Resolver : IResolver, ITypeResolver, IMemberRefResolver
	{
		// Token: 0x17000FD3 RID: 4051
		// (get) Token: 0x06004EC6 RID: 20166 RVA: 0x00186DD8 File Offset: 0x00186DD8
		// (set) Token: 0x06004EC7 RID: 20167 RVA: 0x00186DE0 File Offset: 0x00186DE0
		public bool ProjectWinMDRefs
		{
			get
			{
				return this.projectWinMDRefs;
			}
			set
			{
				this.projectWinMDRefs = value;
			}
		}

		// Token: 0x06004EC8 RID: 20168 RVA: 0x00186DEC File Offset: 0x00186DEC
		public Resolver(IAssemblyResolver assemblyResolver)
		{
			if (assemblyResolver == null)
			{
				throw new ArgumentNullException("assemblyResolver");
			}
			this.assemblyResolver = assemblyResolver;
		}

		// Token: 0x06004EC9 RID: 20169 RVA: 0x00186E14 File Offset: 0x00186E14
		public TypeDef Resolve(TypeRef typeRef, ModuleDef sourceModule)
		{
			if (typeRef == null)
			{
				return null;
			}
			if (this.ProjectWinMDRefs)
			{
				typeRef = (WinMDHelpers.ToCLR(typeRef.Module ?? sourceModule, typeRef) ?? typeRef);
			}
			TypeRef nonNestedTypeRef = TypeRef.GetNonNestedTypeRef(typeRef);
			if (nonNestedTypeRef == null)
			{
				return null;
			}
			IResolutionScope resolutionScope = nonNestedTypeRef.ResolutionScope;
			ModuleDef module = nonNestedTypeRef.Module;
			AssemblyRef assemblyRef = resolutionScope as AssemblyRef;
			if (assemblyRef != null)
			{
				AssemblyDef assemblyDef = this.assemblyResolver.Resolve(assemblyRef, sourceModule ?? module);
				TypeDef result;
				if (assemblyDef != null)
				{
					if ((result = assemblyDef.Find(typeRef)) == null)
					{
						return this.ResolveExportedType(assemblyDef.Modules, typeRef, sourceModule);
					}
				}
				else
				{
					result = null;
				}
				return result;
			}
			ModuleDef moduleDef = resolutionScope as ModuleDef;
			if (moduleDef != null)
			{
				TypeDef result2;
				if ((result2 = moduleDef.Find(typeRef)) == null)
				{
					result2 = this.ResolveExportedType(new ModuleDef[]
					{
						moduleDef
					}, typeRef, sourceModule);
				}
				return result2;
			}
			ModuleRef moduleRef = resolutionScope as ModuleRef;
			if (moduleRef == null)
			{
				return null;
			}
			if (module == null)
			{
				return null;
			}
			if (default(SigComparer).Equals(moduleRef, module))
			{
				TypeDef result3;
				if ((result3 = module.Find(typeRef)) == null)
				{
					result3 = this.ResolveExportedType(new ModuleDef[]
					{
						module
					}, typeRef, sourceModule);
				}
				return result3;
			}
			AssemblyDef assembly = module.Assembly;
			if (assembly == null)
			{
				return null;
			}
			ModuleDef moduleDef2 = assembly.FindModule(moduleRef.Name);
			TypeDef result4;
			if (moduleDef2 != null)
			{
				if ((result4 = moduleDef2.Find(typeRef)) == null)
				{
					return this.ResolveExportedType(new ModuleDef[]
					{
						moduleDef2
					}, typeRef, sourceModule);
				}
			}
			else
			{
				result4 = null;
			}
			return result4;
		}

		// Token: 0x06004ECA RID: 20170 RVA: 0x00186F90 File Offset: 0x00186F90
		private TypeDef ResolveExportedType(IList<ModuleDef> modules, TypeRef typeRef, ModuleDef sourceModule)
		{
			for (int i = 0; i < 30; i++)
			{
				ExportedType exportedType = Resolver.FindExportedType(modules, typeRef);
				if (exportedType == null)
				{
					return null;
				}
				AssemblyDef assemblyDef = modules[0].Context.AssemblyResolver.Resolve(exportedType.DefinitionAssembly, sourceModule ?? typeRef.Module);
				if (assemblyDef == null)
				{
					return null;
				}
				TypeDef typeDef = assemblyDef.Find(typeRef);
				if (typeDef != null)
				{
					return typeDef;
				}
				modules = assemblyDef.Modules;
			}
			return null;
		}

		// Token: 0x06004ECB RID: 20171 RVA: 0x00187010 File Offset: 0x00187010
		private static ExportedType FindExportedType(IList<ModuleDef> modules, TypeRef typeRef)
		{
			if (typeRef == null)
			{
				return null;
			}
			int count = modules.Count;
			for (int i = 0; i < count; i++)
			{
				IList<ExportedType> exportedTypes = modules[i].ExportedTypes;
				int count2 = exportedTypes.Count;
				for (int j = 0; j < count2; j++)
				{
					ExportedType exportedType = exportedTypes[j];
					if (new SigComparer(SigComparerOptions.DontCompareTypeScope).Equals(exportedType, typeRef))
					{
						return exportedType;
					}
				}
			}
			return null;
		}

		// Token: 0x06004ECC RID: 20172 RVA: 0x0018708C File Offset: 0x0018708C
		public IMemberForwarded Resolve(MemberRef memberRef)
		{
			if (memberRef == null)
			{
				return null;
			}
			if (this.ProjectWinMDRefs)
			{
				memberRef = (WinMDHelpers.ToCLR(memberRef.Module, memberRef) ?? memberRef);
			}
			IMemberRefParent @class = memberRef.Class;
			MethodDef methodDef = @class as MethodDef;
			if (methodDef != null)
			{
				return methodDef;
			}
			TypeDef declaringType = this.GetDeclaringType(memberRef, @class);
			if (declaringType == null)
			{
				return null;
			}
			return declaringType.Resolve(memberRef);
		}

		// Token: 0x06004ECD RID: 20173 RVA: 0x001870F4 File Offset: 0x001870F4
		private TypeDef GetDeclaringType(MemberRef memberRef, IMemberRefParent parent)
		{
			if (memberRef == null || parent == null)
			{
				return null;
			}
			TypeSpec typeSpec = parent as TypeSpec;
			if (typeSpec != null)
			{
				parent = typeSpec.ScopeType;
			}
			TypeDef typeDef = parent as TypeDef;
			if (typeDef != null)
			{
				return typeDef;
			}
			TypeRef typeRef = parent as TypeRef;
			if (typeRef != null)
			{
				return this.Resolve(typeRef, memberRef.Module);
			}
			ModuleRef moduleRef = parent as ModuleRef;
			if (moduleRef != null)
			{
				ModuleDef module = memberRef.Module;
				if (module == null)
				{
					return null;
				}
				TypeDef typeDef2 = null;
				if (default(SigComparer).Equals(module, moduleRef))
				{
					typeDef2 = module.GlobalType;
				}
				AssemblyDef assembly = module.Assembly;
				if (typeDef2 == null && assembly != null)
				{
					ModuleDef moduleDef = assembly.FindModule(moduleRef.Name);
					if (moduleDef != null)
					{
						typeDef2 = moduleDef.GlobalType;
					}
				}
				return typeDef2;
			}
			else
			{
				MethodDef methodDef = parent as MethodDef;
				if (methodDef != null)
				{
					return methodDef.DeclaringType;
				}
				return null;
			}
		}

		// Token: 0x040026C6 RID: 9926
		private readonly IAssemblyResolver assemblyResolver;

		// Token: 0x040026C7 RID: 9927
		private bool projectWinMDRefs = true;
	}
}
