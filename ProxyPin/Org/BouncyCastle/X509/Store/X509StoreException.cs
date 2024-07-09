using System;

namespace Org.BouncyCastle.X509.Store
{
	// Token: 0x02000708 RID: 1800
	[Serializable]
	public class X509StoreException : Exception
	{
		// Token: 0x06003EEC RID: 16108 RVA: 0x0015A27C File Offset: 0x0015A27C
		public X509StoreException()
		{
		}

		// Token: 0x06003EED RID: 16109 RVA: 0x0015A284 File Offset: 0x0015A284
		public X509StoreException(string message) : base(message)
		{
		}

		// Token: 0x06003EEE RID: 16110 RVA: 0x0015A290 File Offset: 0x0015A290
		public X509StoreException(string message, Exception e) : base(message, e)
		{
		}
	}
}
