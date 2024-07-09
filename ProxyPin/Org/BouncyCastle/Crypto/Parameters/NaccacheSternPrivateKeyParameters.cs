using System;
using System.Collections;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x02000467 RID: 1127
	public class NaccacheSternPrivateKeyParameters : NaccacheSternKeyParameters
	{
		// Token: 0x06002320 RID: 8992 RVA: 0x000C6314 File Offset: 0x000C6314
		[Obsolete]
		public NaccacheSternPrivateKeyParameters(BigInteger g, BigInteger n, int lowerSigmaBound, ArrayList smallPrimes, BigInteger phiN) : base(true, g, n, lowerSigmaBound)
		{
			this.smallPrimes = smallPrimes;
			this.phiN = phiN;
		}

		// Token: 0x06002321 RID: 8993 RVA: 0x000C6330 File Offset: 0x000C6330
		public NaccacheSternPrivateKeyParameters(BigInteger g, BigInteger n, int lowerSigmaBound, IList smallPrimes, BigInteger phiN) : base(true, g, n, lowerSigmaBound)
		{
			this.smallPrimes = smallPrimes;
			this.phiN = phiN;
		}

		// Token: 0x170006C1 RID: 1729
		// (get) Token: 0x06002322 RID: 8994 RVA: 0x000C634C File Offset: 0x000C634C
		public BigInteger PhiN
		{
			get
			{
				return this.phiN;
			}
		}

		// Token: 0x170006C2 RID: 1730
		// (get) Token: 0x06002323 RID: 8995 RVA: 0x000C6354 File Offset: 0x000C6354
		[Obsolete("Use 'SmallPrimesList' instead")]
		public ArrayList SmallPrimes
		{
			get
			{
				return new ArrayList(this.smallPrimes);
			}
		}

		// Token: 0x170006C3 RID: 1731
		// (get) Token: 0x06002324 RID: 8996 RVA: 0x000C6364 File Offset: 0x000C6364
		public IList SmallPrimesList
		{
			get
			{
				return this.smallPrimes;
			}
		}

		// Token: 0x04001658 RID: 5720
		private readonly BigInteger phiN;

		// Token: 0x04001659 RID: 5721
		private readonly IList smallPrimes;
	}
}
