using System;

namespace Org.BouncyCastle.Ocsp
{
	// Token: 0x02000636 RID: 1590
	[Serializable]
	public class OcspException : Exception
	{
		// Token: 0x06003772 RID: 14194 RVA: 0x001299F8 File Offset: 0x001299F8
		public OcspException()
		{
		}

		// Token: 0x06003773 RID: 14195 RVA: 0x00129A00 File Offset: 0x00129A00
		public OcspException(string message) : base(message)
		{
		}

		// Token: 0x06003774 RID: 14196 RVA: 0x00129A0C File Offset: 0x00129A0C
		public OcspException(string message, Exception e) : base(message, e)
		{
		}
	}
}
