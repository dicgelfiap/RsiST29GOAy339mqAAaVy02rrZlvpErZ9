using System;

namespace Org.BouncyCastle.X509.Store
{
	// Token: 0x02000308 RID: 776
	public interface IX509Selector : ICloneable
	{
		// Token: 0x06001770 RID: 6000
		bool Match(object obj);
	}
}
