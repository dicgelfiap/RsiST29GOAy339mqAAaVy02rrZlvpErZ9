using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace dnlib.DotNet.Emit
{
	// Token: 0x020009E1 RID: 2529
	[ComVisible(true)]
	[Serializable]
	public class InvalidMethodException : Exception
	{
		// Token: 0x060060F3 RID: 24819 RVA: 0x001CE954 File Offset: 0x001CE954
		public InvalidMethodException()
		{
		}

		// Token: 0x060060F4 RID: 24820 RVA: 0x001CE95C File Offset: 0x001CE95C
		public InvalidMethodException(string msg) : base(msg)
		{
		}

		// Token: 0x060060F5 RID: 24821 RVA: 0x001CE968 File Offset: 0x001CE968
		public InvalidMethodException(string msg, Exception innerException) : base(msg, innerException)
		{
		}

		// Token: 0x060060F6 RID: 24822 RVA: 0x001CE974 File Offset: 0x001CE974
		protected InvalidMethodException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
