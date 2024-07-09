using System;

namespace Org.BouncyCastle.X509.Store
{
	// Token: 0x02000709 RID: 1801
	[Serializable]
	public class NoSuchStoreException : X509StoreException
	{
		// Token: 0x06003EEF RID: 16111 RVA: 0x0015A29C File Offset: 0x0015A29C
		public NoSuchStoreException()
		{
		}

		// Token: 0x06003EF0 RID: 16112 RVA: 0x0015A2A4 File Offset: 0x0015A2A4
		public NoSuchStoreException(string message) : base(message)
		{
		}

		// Token: 0x06003EF1 RID: 16113 RVA: 0x0015A2B0 File Offset: 0x0015A2B0
		public NoSuchStoreException(string message, Exception e) : base(message, e)
		{
		}
	}
}
