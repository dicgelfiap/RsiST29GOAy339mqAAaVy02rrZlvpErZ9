using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x020007E1 RID: 2017
	[ComVisible(true)]
	public interface ICustomAttribute
	{
		// Token: 0x17000DD3 RID: 3539
		// (get) Token: 0x060048C6 RID: 18630
		ITypeDefOrRef AttributeType { get; }

		// Token: 0x17000DD4 RID: 3540
		// (get) Token: 0x060048C7 RID: 18631
		string TypeFullName { get; }

		// Token: 0x17000DD5 RID: 3541
		// (get) Token: 0x060048C8 RID: 18632
		IList<CANamedArgument> NamedArguments { get; }

		// Token: 0x17000DD6 RID: 3542
		// (get) Token: 0x060048C9 RID: 18633
		bool HasNamedArguments { get; }

		// Token: 0x17000DD7 RID: 3543
		// (get) Token: 0x060048CA RID: 18634
		IEnumerable<CANamedArgument> Fields { get; }

		// Token: 0x17000DD8 RID: 3544
		// (get) Token: 0x060048CB RID: 18635
		IEnumerable<CANamedArgument> Properties { get; }
	}
}
