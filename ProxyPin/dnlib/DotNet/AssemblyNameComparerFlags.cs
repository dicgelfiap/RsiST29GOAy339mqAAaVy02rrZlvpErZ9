using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x0200077B RID: 1915
	[Flags]
	[ComVisible(true)]
	public enum AssemblyNameComparerFlags
	{
		// Token: 0x040023D8 RID: 9176
		Name = 1,
		// Token: 0x040023D9 RID: 9177
		Version = 2,
		// Token: 0x040023DA RID: 9178
		PublicKeyToken = 4,
		// Token: 0x040023DB RID: 9179
		Culture = 8,
		// Token: 0x040023DC RID: 9180
		ContentType = 16,
		// Token: 0x040023DD RID: 9181
		All = 31
	}
}
