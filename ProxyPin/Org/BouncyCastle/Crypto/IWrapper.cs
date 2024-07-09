using System;

namespace Org.BouncyCastle.Crypto
{
	// Token: 0x02000379 RID: 889
	public interface IWrapper
	{
		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x06001BB8 RID: 7096
		string AlgorithmName { get; }

		// Token: 0x06001BB9 RID: 7097
		void Init(bool forWrapping, ICipherParameters parameters);

		// Token: 0x06001BBA RID: 7098
		byte[] Wrap(byte[] input, int inOff, int length);

		// Token: 0x06001BBB RID: 7099
		byte[] Unwrap(byte[] input, int inOff, int length);
	}
}
