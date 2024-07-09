using System;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x02000476 RID: 1142
	public sealed class Srp6GroupParameters
	{
		// Token: 0x0600236B RID: 9067 RVA: 0x000C6CC4 File Offset: 0x000C6CC4
		public Srp6GroupParameters(BigInteger N, BigInteger g)
		{
			this.n = N;
			this.g = g;
		}

		// Token: 0x170006DF RID: 1759
		// (get) Token: 0x0600236C RID: 9068 RVA: 0x000C6CDC File Offset: 0x000C6CDC
		public BigInteger G
		{
			get
			{
				return this.g;
			}
		}

		// Token: 0x170006E0 RID: 1760
		// (get) Token: 0x0600236D RID: 9069 RVA: 0x000C6CE4 File Offset: 0x000C6CE4
		public BigInteger N
		{
			get
			{
				return this.n;
			}
		}

		// Token: 0x04001683 RID: 5763
		private readonly BigInteger n;

		// Token: 0x04001684 RID: 5764
		private readonly BigInteger g;
	}
}
