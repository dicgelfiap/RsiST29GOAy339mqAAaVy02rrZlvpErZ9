using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.Emit
{
	// Token: 0x020009E7 RID: 2535
	[ComVisible(true)]
	public interface IStringResolver
	{
		// Token: 0x06006140 RID: 24896
		string ReadUserString(uint token);
	}
}
