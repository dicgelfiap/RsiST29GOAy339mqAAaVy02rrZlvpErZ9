using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace dnlib.DotNet
{
	// Token: 0x02000838 RID: 2104
	[ComVisible(true)]
	[Serializable]
	public class AssemblyResolveException : ResolveException
	{
		// Token: 0x06004EBA RID: 20154 RVA: 0x00186D54 File Offset: 0x00186D54
		public AssemblyResolveException()
		{
		}

		// Token: 0x06004EBB RID: 20155 RVA: 0x00186D5C File Offset: 0x00186D5C
		public AssemblyResolveException(string message) : base(message)
		{
		}

		// Token: 0x06004EBC RID: 20156 RVA: 0x00186D68 File Offset: 0x00186D68
		public AssemblyResolveException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06004EBD RID: 20157 RVA: 0x00186D74 File Offset: 0x00186D74
		protected AssemblyResolveException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
