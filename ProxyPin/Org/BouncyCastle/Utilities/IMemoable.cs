using System;

namespace Org.BouncyCastle.Utilities
{
	// Token: 0x0200034F RID: 847
	public interface IMemoable
	{
		// Token: 0x06001948 RID: 6472
		IMemoable Copy();

		// Token: 0x06001949 RID: 6473
		void Reset(IMemoable other);
	}
}
