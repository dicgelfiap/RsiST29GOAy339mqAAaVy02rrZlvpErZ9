using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.Writer
{
	// Token: 0x020008BB RID: 2235
	[ComVisible(true)]
	public interface ITokenProvider : IWriterError
	{
		// Token: 0x06005659 RID: 22105
		MDToken GetToken(object o);

		// Token: 0x0600565A RID: 22106
		MDToken GetToken(IList<TypeSig> locals, uint origToken);
	}
}
