using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace dnlib.DotNet
{
	// Token: 0x02000837 RID: 2103
	[ComVisible(true)]
	[Serializable]
	public class ResolveException : Exception
	{
		// Token: 0x06004EB6 RID: 20150 RVA: 0x00186D28 File Offset: 0x00186D28
		public ResolveException()
		{
		}

		// Token: 0x06004EB7 RID: 20151 RVA: 0x00186D30 File Offset: 0x00186D30
		public ResolveException(string message) : base(message)
		{
		}

		// Token: 0x06004EB8 RID: 20152 RVA: 0x00186D3C File Offset: 0x00186D3C
		public ResolveException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06004EB9 RID: 20153 RVA: 0x00186D48 File Offset: 0x00186D48
		protected ResolveException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
