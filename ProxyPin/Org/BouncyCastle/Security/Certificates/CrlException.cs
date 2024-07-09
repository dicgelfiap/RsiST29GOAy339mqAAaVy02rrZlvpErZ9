using System;

namespace Org.BouncyCastle.Security.Certificates
{
	// Token: 0x020006A7 RID: 1703
	[Serializable]
	public class CrlException : GeneralSecurityException
	{
		// Token: 0x06003B93 RID: 15251 RVA: 0x0014446C File Offset: 0x0014446C
		public CrlException()
		{
		}

		// Token: 0x06003B94 RID: 15252 RVA: 0x00144474 File Offset: 0x00144474
		public CrlException(string msg) : base(msg)
		{
		}

		// Token: 0x06003B95 RID: 15253 RVA: 0x00144480 File Offset: 0x00144480
		public CrlException(string msg, Exception e) : base(msg, e)
		{
		}
	}
}
