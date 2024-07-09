using System;
using System.Runtime.InteropServices;

namespace ProtoBuf
{
	// Token: 0x02000C26 RID: 3110
	[ComVisible(true)]
	public interface IExtensible
	{
		// Token: 0x06007BAA RID: 31658
		IExtension GetExtensionObject(bool createIfMissing);
	}
}
