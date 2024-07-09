using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace dnlib.DotNet.Resources
{
	// Token: 0x020008E8 RID: 2280
	[ComVisible(true)]
	[Serializable]
	public sealed class ResourceReaderException : Exception
	{
		// Token: 0x060058D9 RID: 22745 RVA: 0x001B4858 File Offset: 0x001B4858
		public ResourceReaderException()
		{
		}

		// Token: 0x060058DA RID: 22746 RVA: 0x001B4860 File Offset: 0x001B4860
		public ResourceReaderException(string msg) : base(msg)
		{
		}

		// Token: 0x060058DB RID: 22747 RVA: 0x001B486C File Offset: 0x001B486C
		public ResourceReaderException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
