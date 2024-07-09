using System;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Crypto.Signers
{
	// Token: 0x020004A7 RID: 1191
	public interface IDsaEncoding
	{
		// Token: 0x0600249F RID: 9375
		BigInteger[] Decode(BigInteger n, byte[] encoding);

		// Token: 0x060024A0 RID: 9376
		byte[] Encode(BigInteger n, BigInteger r, BigInteger s);
	}
}
