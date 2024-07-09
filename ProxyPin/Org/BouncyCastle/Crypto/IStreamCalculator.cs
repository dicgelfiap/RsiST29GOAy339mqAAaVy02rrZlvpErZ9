using System;
using System.IO;

namespace Org.BouncyCastle.Crypto
{
	// Token: 0x02000324 RID: 804
	public interface IStreamCalculator
	{
		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x0600183B RID: 6203
		Stream Stream { get; }

		// Token: 0x0600183C RID: 6204
		object GetResult();
	}
}
