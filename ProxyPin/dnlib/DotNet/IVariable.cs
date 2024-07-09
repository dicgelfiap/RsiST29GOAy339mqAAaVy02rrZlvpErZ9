using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x020007F8 RID: 2040
	[ComVisible(true)]
	public interface IVariable
	{
		// Token: 0x17000E11 RID: 3601
		// (get) Token: 0x06004973 RID: 18803
		TypeSig Type { get; }

		// Token: 0x17000E12 RID: 3602
		// (get) Token: 0x06004974 RID: 18804
		int Index { get; }

		// Token: 0x17000E13 RID: 3603
		// (get) Token: 0x06004975 RID: 18805
		// (set) Token: 0x06004976 RID: 18806
		string Name { get; set; }
	}
}
