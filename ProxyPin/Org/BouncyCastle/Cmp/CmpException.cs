using System;

namespace Org.BouncyCastle.Cmp
{
	// Token: 0x020002CA RID: 714
	public class CmpException : Exception
	{
		// Token: 0x060015C1 RID: 5569 RVA: 0x000729EC File Offset: 0x000729EC
		public CmpException()
		{
		}

		// Token: 0x060015C2 RID: 5570 RVA: 0x000729F4 File Offset: 0x000729F4
		public CmpException(string message) : base(message)
		{
		}

		// Token: 0x060015C3 RID: 5571 RVA: 0x00072A00 File Offset: 0x00072A00
		public CmpException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
