using System;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Crypto.Agreement.JPake
{
	// Token: 0x0200032E RID: 814
	public class JPakePrimeOrderGroup
	{
		// Token: 0x0600186C RID: 6252 RVA: 0x0007E484 File Offset: 0x0007E484
		public JPakePrimeOrderGroup(BigInteger p, BigInteger q, BigInteger g) : this(p, q, g, false)
		{
		}

		// Token: 0x0600186D RID: 6253 RVA: 0x0007E490 File Offset: 0x0007E490
		public JPakePrimeOrderGroup(BigInteger p, BigInteger q, BigInteger g, bool skipChecks)
		{
			JPakeUtilities.ValidateNotNull(p, "p");
			JPakeUtilities.ValidateNotNull(q, "q");
			JPakeUtilities.ValidateNotNull(g, "g");
			if (!skipChecks)
			{
				if (!p.Subtract(JPakeUtilities.One).Mod(q).Equals(JPakeUtilities.Zero))
				{
					throw new ArgumentException("p-1 must be evenly divisible by q");
				}
				if (g.CompareTo(BigInteger.Two) == -1 || g.CompareTo(p.Subtract(JPakeUtilities.One)) == 1)
				{
					throw new ArgumentException("g must be in [2, p-1]");
				}
				if (!g.ModPow(q, p).Equals(JPakeUtilities.One))
				{
					throw new ArgumentException("g^q mod p must equal 1");
				}
				if (!p.IsProbablePrime(20))
				{
					throw new ArgumentException("p must be prime");
				}
				if (!q.IsProbablePrime(20))
				{
					throw new ArgumentException("q must be prime");
				}
			}
			this.p = p;
			this.q = q;
			this.g = g;
		}

		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x0600186E RID: 6254 RVA: 0x0007E594 File Offset: 0x0007E594
		public virtual BigInteger P
		{
			get
			{
				return this.p;
			}
		}

		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x0600186F RID: 6255 RVA: 0x0007E59C File Offset: 0x0007E59C
		public virtual BigInteger Q
		{
			get
			{
				return this.q;
			}
		}

		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x06001870 RID: 6256 RVA: 0x0007E5A4 File Offset: 0x0007E5A4
		public virtual BigInteger G
		{
			get
			{
				return this.g;
			}
		}

		// Token: 0x0400103A RID: 4154
		private readonly BigInteger p;

		// Token: 0x0400103B RID: 4155
		private readonly BigInteger q;

		// Token: 0x0400103C RID: 4156
		private readonly BigInteger g;
	}
}
