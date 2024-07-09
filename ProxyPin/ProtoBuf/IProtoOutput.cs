using System;
using System.Runtime.InteropServices;

namespace ProtoBuf
{
	// Token: 0x02000C2B RID: 3115
	[ComVisible(true)]
	public interface IProtoOutput<TOutput>
	{
		// Token: 0x06007BB2 RID: 31666
		void Serialize<T>(TOutput destination, T value, object userState = null);
	}
}
