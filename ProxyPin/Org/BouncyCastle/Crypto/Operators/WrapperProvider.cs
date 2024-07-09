using System;

namespace Org.BouncyCastle.Crypto.Operators
{
	// Token: 0x02000413 RID: 1043
	internal interface WrapperProvider
	{
		// Token: 0x0600216C RID: 8556
		object CreateWrapper(bool forWrapping, ICipherParameters parameters);
	}
}
