using System;

namespace Org.BouncyCastle.Security
{
	// Token: 0x02000692 RID: 1682
	[Serializable]
	public class GeneralSecurityException : Exception
	{
		// Token: 0x06003AB3 RID: 15027 RVA: 0x0013C538 File Offset: 0x0013C538
		public GeneralSecurityException()
		{
		}

		// Token: 0x06003AB4 RID: 15028 RVA: 0x0013C540 File Offset: 0x0013C540
		public GeneralSecurityException(string message) : base(message)
		{
		}

		// Token: 0x06003AB5 RID: 15029 RVA: 0x0013C54C File Offset: 0x0013C54C
		public GeneralSecurityException(string message, Exception exception) : base(message, exception)
		{
		}
	}
}
