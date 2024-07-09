using System;

namespace Org.BouncyCastle.Security
{
	// Token: 0x020006AD RID: 1709
	[Serializable]
	public class KeyException : GeneralSecurityException
	{
		// Token: 0x06003BDC RID: 15324 RVA: 0x00147D80 File Offset: 0x00147D80
		public KeyException()
		{
		}

		// Token: 0x06003BDD RID: 15325 RVA: 0x00147D88 File Offset: 0x00147D88
		public KeyException(string message) : base(message)
		{
		}

		// Token: 0x06003BDE RID: 15326 RVA: 0x00147D94 File Offset: 0x00147D94
		public KeyException(string message, Exception exception) : base(message, exception)
		{
		}
	}
}
