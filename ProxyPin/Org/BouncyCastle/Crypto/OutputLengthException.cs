using System;

namespace Org.BouncyCastle.Crypto
{
	// Token: 0x02000567 RID: 1383
	[Serializable]
	public class OutputLengthException : DataLengthException
	{
		// Token: 0x06002AF3 RID: 10995 RVA: 0x000E50B0 File Offset: 0x000E50B0
		public OutputLengthException()
		{
		}

		// Token: 0x06002AF4 RID: 10996 RVA: 0x000E50B8 File Offset: 0x000E50B8
		public OutputLengthException(string message) : base(message)
		{
		}

		// Token: 0x06002AF5 RID: 10997 RVA: 0x000E50C4 File Offset: 0x000E50C4
		public OutputLengthException(string message, Exception exception) : base(message, exception)
		{
		}
	}
}
