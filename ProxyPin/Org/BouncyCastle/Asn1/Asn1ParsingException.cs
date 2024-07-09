using System;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x02000240 RID: 576
	[Serializable]
	public class Asn1ParsingException : InvalidOperationException
	{
		// Token: 0x06001298 RID: 4760 RVA: 0x00068788 File Offset: 0x00068788
		public Asn1ParsingException()
		{
		}

		// Token: 0x06001299 RID: 4761 RVA: 0x00068790 File Offset: 0x00068790
		public Asn1ParsingException(string message) : base(message)
		{
		}

		// Token: 0x0600129A RID: 4762 RVA: 0x0006879C File Offset: 0x0006879C
		public Asn1ParsingException(string message, Exception exception) : base(message, exception)
		{
		}
	}
}
