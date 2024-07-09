using System;

namespace dnlib.DotNet
{
	// Token: 0x0200079B RID: 1947
	internal sealed class CAAssemblyRefFinder : IAssemblyRefFinder
	{
		// Token: 0x060045A1 RID: 17825 RVA: 0x0016DE20 File Offset: 0x0016DE20
		public CAAssemblyRefFinder(ModuleDef module)
		{
			this.module = module;
		}

		// Token: 0x060045A2 RID: 17826 RVA: 0x0016DE30 File Offset: 0x0016DE30
		public AssemblyRef FindAssemblyRef(TypeRef nonNestedTypeRef)
		{
			AssemblyDef assembly = this.module.Assembly;
			if (assembly != null)
			{
				TypeDefMD typeDefMD = assembly.Find(nonNestedTypeRef) as TypeDefMD;
				if (typeDefMD != null && typeDefMD.ReaderModule == this.module)
				{
					return this.module.UpdateRowId<AssemblyRefUser>(new AssemblyRefUser(assembly));
				}
			}
			else if (this.module.Find(nonNestedTypeRef) != null)
			{
				return AssemblyRef.CurrentAssembly;
			}
			AssemblyDef assemblyDef = this.module.Context.AssemblyResolver.Resolve(this.module.CorLibTypes.AssemblyRef, this.module);
			if (assemblyDef != null && assemblyDef.Find(nonNestedTypeRef) != null)
			{
				return this.module.CorLibTypes.AssemblyRef;
			}
			if (assembly != null)
			{
				return this.module.UpdateRowId<AssemblyRefUser>(new AssemblyRefUser(assembly));
			}
			return AssemblyRef.CurrentAssembly;
		}

		// Token: 0x0400244E RID: 9294
		private readonly ModuleDef module;
	}
}
