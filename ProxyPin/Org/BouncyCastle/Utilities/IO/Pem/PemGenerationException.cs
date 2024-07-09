using System;

namespace Org.BouncyCastle.Utilities.IO.Pem
{
	// Token: 0x020006E1 RID: 1761
	[Serializable]
	public class PemGenerationException : Exception
	{
		// Token: 0x06003D8D RID: 15757 RVA: 0x00151014 File Offset: 0x00151014
		public PemGenerationException()
		{
		}

		// Token: 0x06003D8E RID: 15758 RVA: 0x0015101C File Offset: 0x0015101C
		public PemGenerationException(string message) : base(message)
		{
		}

		// Token: 0x06003D8F RID: 15759 RVA: 0x00151028 File Offset: 0x00151028
		public PemGenerationException(string message, Exception exception) : base(message, exception)
		{
		}
	}
}
