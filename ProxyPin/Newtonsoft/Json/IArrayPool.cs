using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json
{
	// Token: 0x02000A73 RID: 2675
	[NullableContext(1)]
	public interface IArrayPool<[Nullable(2)] T>
	{
		// Token: 0x06006839 RID: 26681
		T[] Rent(int minimumLength);

		// Token: 0x0600683A RID: 26682
		void Return([Nullable(new byte[]
		{
			2,
			1
		})] T[] array);
	}
}
