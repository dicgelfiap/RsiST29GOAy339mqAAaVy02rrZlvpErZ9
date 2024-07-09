using System;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Crypto
{
	// Token: 0x02000340 RID: 832
	public interface IBasicAgreement
	{
		// Token: 0x060018E0 RID: 6368
		void Init(ICipherParameters parameters);

		// Token: 0x060018E1 RID: 6369
		int GetFieldSize();

		// Token: 0x060018E2 RID: 6370
		BigInteger CalculateAgreement(ICipherParameters pubKey);
	}
}
