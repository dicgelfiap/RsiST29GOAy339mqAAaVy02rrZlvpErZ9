using System;
using System.IO;

namespace Org.BouncyCastle.Utilities.IO
{
	// Token: 0x020006E9 RID: 1769
	[Serializable]
	public class StreamOverflowException : IOException
	{
		// Token: 0x06003DA8 RID: 15784 RVA: 0x00151200 File Offset: 0x00151200
		public StreamOverflowException()
		{
		}

		// Token: 0x06003DA9 RID: 15785 RVA: 0x00151208 File Offset: 0x00151208
		public StreamOverflowException(string message) : base(message)
		{
		}

		// Token: 0x06003DAA RID: 15786 RVA: 0x00151214 File Offset: 0x00151214
		public StreamOverflowException(string message, Exception exception) : base(message, exception)
		{
		}
	}
}
