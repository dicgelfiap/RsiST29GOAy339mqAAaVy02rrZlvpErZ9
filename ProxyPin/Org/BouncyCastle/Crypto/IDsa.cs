using System;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Crypto
{
	// Token: 0x02000497 RID: 1175
	public interface IDsa
	{
		// Token: 0x170006ED RID: 1773
		// (get) Token: 0x06002430 RID: 9264
		string AlgorithmName { get; }

		// Token: 0x06002431 RID: 9265
		void Init(bool forSigning, ICipherParameters parameters);

		// Token: 0x06002432 RID: 9266
		BigInteger[] GenerateSignature(byte[] message);

		// Token: 0x06002433 RID: 9267
		bool VerifySignature(byte[] message, BigInteger r, BigInteger s);
	}
}
