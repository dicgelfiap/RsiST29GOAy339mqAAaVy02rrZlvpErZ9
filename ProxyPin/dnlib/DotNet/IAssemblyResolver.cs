using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x020007BF RID: 1983
	[ComVisible(true)]
	public interface IAssemblyResolver
	{
		// Token: 0x06004854 RID: 18516
		AssemblyDef Resolve(IAssembly assembly, ModuleDef sourceModule);
	}
}
