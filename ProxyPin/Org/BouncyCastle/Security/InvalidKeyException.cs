using System;

namespace Org.BouncyCastle.Security
{
	// Token: 0x020006AE RID: 1710
	[Serializable]
	public class InvalidKeyException : KeyException
	{
		// Token: 0x06003BDF RID: 15327 RVA: 0x00147DA0 File Offset: 0x00147DA0
		public InvalidKeyException()
		{
		}

		// Token: 0x06003BE0 RID: 15328 RVA: 0x00147DA8 File Offset: 0x00147DA8
		public InvalidKeyException(string message) : base(message)
		{
		}

		// Token: 0x06003BE1 RID: 15329 RVA: 0x00147DB4 File Offset: 0x00147DB4
		public InvalidKeyException(string message, Exception exception) : base(message, exception)
		{
		}
	}
}
