using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x020007F2 RID: 2034
	[ComVisible(true)]
	public interface IMemberRefResolver
	{
		// Token: 0x06004962 RID: 18786
		IMemberForwarded Resolve(MemberRef memberRef);
	}
}
