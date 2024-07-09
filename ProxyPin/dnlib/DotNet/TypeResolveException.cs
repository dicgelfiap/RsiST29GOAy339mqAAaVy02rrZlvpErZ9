using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace dnlib.DotNet
{
	// Token: 0x02000839 RID: 2105
	[ComVisible(true)]
	[Serializable]
	public class TypeResolveException : ResolveException
	{
		// Token: 0x06004EBE RID: 20158 RVA: 0x00186D80 File Offset: 0x00186D80
		public TypeResolveException()
		{
		}

		// Token: 0x06004EBF RID: 20159 RVA: 0x00186D88 File Offset: 0x00186D88
		public TypeResolveException(string message) : base(message)
		{
		}

		// Token: 0x06004EC0 RID: 20160 RVA: 0x00186D94 File Offset: 0x00186D94
		public TypeResolveException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06004EC1 RID: 20161 RVA: 0x00186DA0 File Offset: 0x00186DA0
		protected TypeResolveException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
