using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace dnlib.DotNet
{
	// Token: 0x02000852 RID: 2130
	[ComVisible(true)]
	[Serializable]
	public class InvalidKeyException : Exception
	{
		// Token: 0x0600506C RID: 20588 RVA: 0x0018FBE4 File Offset: 0x0018FBE4
		public InvalidKeyException()
		{
		}

		// Token: 0x0600506D RID: 20589 RVA: 0x0018FBEC File Offset: 0x0018FBEC
		public InvalidKeyException(string message) : base(message)
		{
		}

		// Token: 0x0600506E RID: 20590 RVA: 0x0018FBF8 File Offset: 0x0018FBF8
		public InvalidKeyException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600506F RID: 20591 RVA: 0x0018FC04 File Offset: 0x0018FC04
		protected InvalidKeyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
