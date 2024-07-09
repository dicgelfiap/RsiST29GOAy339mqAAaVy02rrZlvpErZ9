using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ProtoBuf
{
	// Token: 0x02000C3F RID: 3135
	[protobuf-net.IsReadOnly]
	[ComVisible(true)]
	public struct SubItemToken
	{
		// Token: 0x06007CBE RID: 31934 RVA: 0x0024B1D4 File Offset: 0x0024B1D4
		internal SubItemToken(int value)
		{
			this.value64 = (long)value;
		}

		// Token: 0x06007CBF RID: 31935 RVA: 0x0024B1E0 File Offset: 0x0024B1E0
		internal SubItemToken(long value)
		{
			this.value64 = value;
		}

		// Token: 0x04003C22 RID: 15394
		internal readonly long value64;
	}
}
