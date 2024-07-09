using System;
using System.IO;

namespace Org.BouncyCastle.Security
{
	// Token: 0x02000670 RID: 1648
	[Serializable]
	public class EncryptionException : IOException
	{
		// Token: 0x060039C3 RID: 14787 RVA: 0x00135F00 File Offset: 0x00135F00
		public EncryptionException(string message) : base(message)
		{
		}

		// Token: 0x060039C4 RID: 14788 RVA: 0x00135F0C File Offset: 0x00135F0C
		public EncryptionException(string message, Exception exception) : base(message, exception)
		{
		}
	}
}
