using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace dnlib.IO
{
	// Token: 0x02000763 RID: 1891
	[ComVisible(true)]
	[Serializable]
	public sealed class DataReaderException : IOException
	{
		// Token: 0x06004225 RID: 16933 RVA: 0x00164B30 File Offset: 0x00164B30
		internal DataReaderException(string message) : base(message)
		{
		}

		// Token: 0x06004226 RID: 16934 RVA: 0x00164B3C File Offset: 0x00164B3C
		internal DataReaderException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
