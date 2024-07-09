using System;
using System.Collections;

namespace Org.BouncyCastle.X509.Store
{
	// Token: 0x02000706 RID: 1798
	public interface IX509Store
	{
		// Token: 0x06003EEB RID: 16107
		ICollection GetMatches(IX509Selector selector);
	}
}
