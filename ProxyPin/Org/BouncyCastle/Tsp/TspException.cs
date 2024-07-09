using System;

namespace Org.BouncyCastle.Tsp
{
	// Token: 0x020006C3 RID: 1731
	[Serializable]
	public class TspException : Exception
	{
		// Token: 0x06003C9D RID: 15517 RVA: 0x0014EC80 File Offset: 0x0014EC80
		public TspException()
		{
		}

		// Token: 0x06003C9E RID: 15518 RVA: 0x0014EC88 File Offset: 0x0014EC88
		public TspException(string message) : base(message)
		{
		}

		// Token: 0x06003C9F RID: 15519 RVA: 0x0014EC94 File Offset: 0x0014EC94
		public TspException(string message, Exception e) : base(message, e)
		{
		}
	}
}
