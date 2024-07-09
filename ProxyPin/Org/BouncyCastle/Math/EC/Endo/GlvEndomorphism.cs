using System;

namespace Org.BouncyCastle.Math.EC.Endo
{
	// Token: 0x020005EB RID: 1515
	public interface GlvEndomorphism : ECEndomorphism
	{
		// Token: 0x060032F2 RID: 13042
		BigInteger[] DecomposeScalar(BigInteger k);
	}
}
