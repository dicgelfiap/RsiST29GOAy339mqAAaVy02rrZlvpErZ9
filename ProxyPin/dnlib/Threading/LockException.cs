using System;
using System.Runtime.Serialization;

namespace dnlib.Threading
{
	// Token: 0x02000745 RID: 1861
	[Serializable]
	internal class LockException : Exception
	{
		// Token: 0x0600411D RID: 16669 RVA: 0x00162B00 File Offset: 0x00162B00
		public LockException()
		{
		}

		// Token: 0x0600411E RID: 16670 RVA: 0x00162B08 File Offset: 0x00162B08
		public LockException(string msg) : base(msg)
		{
		}

		// Token: 0x0600411F RID: 16671 RVA: 0x00162B14 File Offset: 0x00162B14
		protected LockException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
