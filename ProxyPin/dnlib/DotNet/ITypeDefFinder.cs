using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x020007F7 RID: 2039
	[ComVisible(true)]
	public interface ITypeDefFinder
	{
		// Token: 0x06004971 RID: 18801
		TypeDef Find(string fullName, bool isReflectionName);

		// Token: 0x06004972 RID: 18802
		TypeDef Find(TypeRef typeRef);
	}
}
