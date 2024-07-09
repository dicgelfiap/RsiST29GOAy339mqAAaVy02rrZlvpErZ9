using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x02000860 RID: 2144
	[ComVisible(true)]
	public interface IAssemblyRefFinder
	{
		// Token: 0x060051F1 RID: 20977
		AssemblyRef FindAssemblyRef(TypeRef nonNestedTypeRef);
	}
}
