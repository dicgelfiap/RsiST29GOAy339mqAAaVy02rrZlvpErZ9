using System;

namespace Org.BouncyCastle.Crmf
{
	// Token: 0x0200031D RID: 797
	public class CrmfException : Exception
	{
		// Token: 0x06001820 RID: 6176 RVA: 0x0007D2DC File Offset: 0x0007D2DC
		public CrmfException()
		{
		}

		// Token: 0x06001821 RID: 6177 RVA: 0x0007D2E4 File Offset: 0x0007D2E4
		public CrmfException(string message) : base(message)
		{
		}

		// Token: 0x06001822 RID: 6178 RVA: 0x0007D2F0 File Offset: 0x0007D2F0
		public CrmfException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
