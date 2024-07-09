using System;
using System.IO;

namespace Org.BouncyCastle.OpenSsl
{
	// Token: 0x02000675 RID: 1653
	[Serializable]
	public class PemException : IOException
	{
		// Token: 0x060039CF RID: 14799 RVA: 0x00136464 File Offset: 0x00136464
		public PemException(string message) : base(message)
		{
		}

		// Token: 0x060039D0 RID: 14800 RVA: 0x00136470 File Offset: 0x00136470
		public PemException(string message, Exception exception) : base(message, exception)
		{
		}
	}
}
