using System;
using System.Runtime.InteropServices;

namespace ProtoBuf
{
	// Token: 0x02000C2A RID: 3114
	[ComVisible(true)]
	public interface IProtoInput<TInput>
	{
		// Token: 0x06007BB1 RID: 31665
		T Deserialize<T>(TInput source, T value = default(T), object userState = null);
	}
}
