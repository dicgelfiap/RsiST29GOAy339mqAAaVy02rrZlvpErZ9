using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x020007F3 RID: 2035
	[ComVisible(true)]
	public interface ITokenResolver
	{
		// Token: 0x06004963 RID: 18787
		IMDTokenProvider ResolveToken(uint token, GenericParamContext gpContext);
	}
}
