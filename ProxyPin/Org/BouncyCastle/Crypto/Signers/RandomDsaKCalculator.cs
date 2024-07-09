using System;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Signers
{
	// Token: 0x020004AE RID: 1198
	public class RandomDsaKCalculator : IDsaKCalculator
	{
		// Token: 0x17000705 RID: 1797
		// (get) Token: 0x060024E4 RID: 9444 RVA: 0x000CDA78 File Offset: 0x000CDA78
		public virtual bool IsDeterministic
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060024E5 RID: 9445 RVA: 0x000CDA7C File Offset: 0x000CDA7C
		public virtual void Init(BigInteger n, SecureRandom random)
		{
			this.q = n;
			this.random = random;
		}

		// Token: 0x060024E6 RID: 9446 RVA: 0x000CDA8C File Offset: 0x000CDA8C
		public virtual void Init(BigInteger n, BigInteger d, byte[] message)
		{
			throw new InvalidOperationException("Operation not supported");
		}

		// Token: 0x060024E7 RID: 9447 RVA: 0x000CDA98 File Offset: 0x000CDA98
		public virtual BigInteger NextK()
		{
			int bitLength = this.q.BitLength;
			BigInteger bigInteger;
			do
			{
				bigInteger = new BigInteger(bitLength, this.random);
			}
			while (bigInteger.SignValue < 1 || bigInteger.CompareTo(this.q) >= 0);
			return bigInteger;
		}

		// Token: 0x04001762 RID: 5986
		private BigInteger q;

		// Token: 0x04001763 RID: 5987
		private SecureRandom random;
	}
}
