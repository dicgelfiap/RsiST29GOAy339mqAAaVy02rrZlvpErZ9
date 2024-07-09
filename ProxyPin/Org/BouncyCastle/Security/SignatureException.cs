using System;

namespace Org.BouncyCastle.Security
{
	// Token: 0x020006B7 RID: 1719
	[Serializable]
	public class SignatureException : GeneralSecurityException
	{
		// Token: 0x06003C2D RID: 15405 RVA: 0x0014B5F4 File Offset: 0x0014B5F4
		public SignatureException()
		{
		}

		// Token: 0x06003C2E RID: 15406 RVA: 0x0014B5FC File Offset: 0x0014B5FC
		public SignatureException(string message) : base(message)
		{
		}

		// Token: 0x06003C2F RID: 15407 RVA: 0x0014B608 File Offset: 0x0014B608
		public SignatureException(string message, Exception exception) : base(message, exception)
		{
		}
	}
}
