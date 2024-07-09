using System;

namespace Org.BouncyCastle.Crypto
{
	// Token: 0x02000368 RID: 872
	public interface IXof : IDigest
	{
		// Token: 0x06001AC8 RID: 6856
		int DoFinal(byte[] output, int outOff, int outLen);

		// Token: 0x06001AC9 RID: 6857
		int DoOutput(byte[] output, int outOff, int outLen);
	}
}
