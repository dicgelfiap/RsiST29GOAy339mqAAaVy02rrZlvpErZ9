using System;
using System.IO;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x02000237 RID: 567
	[Serializable]
	public class Asn1Exception : IOException
	{
		// Token: 0x06001259 RID: 4697 RVA: 0x00067A3C File Offset: 0x00067A3C
		public Asn1Exception()
		{
		}

		// Token: 0x0600125A RID: 4698 RVA: 0x00067A44 File Offset: 0x00067A44
		public Asn1Exception(string message) : base(message)
		{
		}

		// Token: 0x0600125B RID: 4699 RVA: 0x00067A50 File Offset: 0x00067A50
		public Asn1Exception(string message, Exception exception) : base(message, exception)
		{
		}
	}
}
