using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace dnlib.DotNet
{
	// Token: 0x0200083A RID: 2106
	[ComVisible(true)]
	[Serializable]
	public class MemberRefResolveException : ResolveException
	{
		// Token: 0x06004EC2 RID: 20162 RVA: 0x00186DAC File Offset: 0x00186DAC
		public MemberRefResolveException()
		{
		}

		// Token: 0x06004EC3 RID: 20163 RVA: 0x00186DB4 File Offset: 0x00186DB4
		public MemberRefResolveException(string message) : base(message)
		{
		}

		// Token: 0x06004EC4 RID: 20164 RVA: 0x00186DC0 File Offset: 0x00186DC0
		public MemberRefResolveException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06004EC5 RID: 20165 RVA: 0x00186DCC File Offset: 0x00186DCC
		protected MemberRefResolveException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
