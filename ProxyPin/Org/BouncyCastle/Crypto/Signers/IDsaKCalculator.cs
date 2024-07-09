using System;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Signers
{
	// Token: 0x020004A5 RID: 1189
	public interface IDsaKCalculator
	{
		// Token: 0x17000700 RID: 1792
		// (get) Token: 0x06002495 RID: 9365
		bool IsDeterministic { get; }

		// Token: 0x06002496 RID: 9366
		void Init(BigInteger n, SecureRandom random);

		// Token: 0x06002497 RID: 9367
		void Init(BigInteger n, BigInteger d, byte[] message);

		// Token: 0x06002498 RID: 9368
		BigInteger NextK();
	}
}
