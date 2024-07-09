using System;

namespace Org.BouncyCastle.Security
{
	// Token: 0x020006B1 RID: 1713
	[Obsolete("Never thrown")]
	[Serializable]
	public class NoSuchAlgorithmException : GeneralSecurityException
	{
		// Token: 0x06003BED RID: 15341 RVA: 0x0014854C File Offset: 0x0014854C
		public NoSuchAlgorithmException()
		{
		}

		// Token: 0x06003BEE RID: 15342 RVA: 0x00148554 File Offset: 0x00148554
		public NoSuchAlgorithmException(string message) : base(message)
		{
		}

		// Token: 0x06003BEF RID: 15343 RVA: 0x00148560 File Offset: 0x00148560
		public NoSuchAlgorithmException(string message, Exception exception) : base(message, exception)
		{
		}
	}
}
