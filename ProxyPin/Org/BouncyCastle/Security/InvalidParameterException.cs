using System;

namespace Org.BouncyCastle.Security
{
	// Token: 0x020006AF RID: 1711
	[Serializable]
	public class InvalidParameterException : KeyException
	{
		// Token: 0x06003BE2 RID: 15330 RVA: 0x00147DC0 File Offset: 0x00147DC0
		public InvalidParameterException()
		{
		}

		// Token: 0x06003BE3 RID: 15331 RVA: 0x00147DC8 File Offset: 0x00147DC8
		public InvalidParameterException(string message) : base(message)
		{
		}

		// Token: 0x06003BE4 RID: 15332 RVA: 0x00147DD4 File Offset: 0x00147DD4
		public InvalidParameterException(string message, Exception exception) : base(message, exception)
		{
		}
	}
}
