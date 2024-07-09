using System;
using System.IO;

namespace Org.BouncyCastle.Security
{
	// Token: 0x02000674 RID: 1652
	[Serializable]
	public class PasswordException : IOException
	{
		// Token: 0x060039CD RID: 14797 RVA: 0x0013644C File Offset: 0x0013644C
		public PasswordException(string message) : base(message)
		{
		}

		// Token: 0x060039CE RID: 14798 RVA: 0x00136458 File Offset: 0x00136458
		public PasswordException(string message, Exception exception) : base(message, exception)
		{
		}
	}
}
