using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json
{
	// Token: 0x02000A81 RID: 2689
	public abstract class JsonNameTable
	{
		// Token: 0x060068C4 RID: 26820
		[NullableContext(1)]
		[return: Nullable(2)]
		public abstract string Get(char[] key, int start, int length);
	}
}
