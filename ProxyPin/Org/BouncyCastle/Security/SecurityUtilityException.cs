using System;

namespace Org.BouncyCastle.Security
{
	// Token: 0x020006B6 RID: 1718
	[Serializable]
	public class SecurityUtilityException : Exception
	{
		// Token: 0x06003C2A RID: 15402 RVA: 0x0014B5D4 File Offset: 0x0014B5D4
		public SecurityUtilityException()
		{
		}

		// Token: 0x06003C2B RID: 15403 RVA: 0x0014B5DC File Offset: 0x0014B5DC
		public SecurityUtilityException(string message) : base(message)
		{
		}

		// Token: 0x06003C2C RID: 15404 RVA: 0x0014B5E8 File Offset: 0x0014B5E8
		public SecurityUtilityException(string message, Exception exception) : base(message, exception)
		{
		}
	}
}
