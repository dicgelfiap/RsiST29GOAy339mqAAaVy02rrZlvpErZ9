using System;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x0200043D RID: 1085
	public class DsaParameterGenerationParameters
	{
		// Token: 0x0600222C RID: 8748 RVA: 0x000C41FC File Offset: 0x000C41FC
		public DsaParameterGenerationParameters(int L, int N, int certainty, SecureRandom random) : this(L, N, certainty, random, -1)
		{
		}

		// Token: 0x0600222D RID: 8749 RVA: 0x000C420C File Offset: 0x000C420C
		public DsaParameterGenerationParameters(int L, int N, int certainty, SecureRandom random, int usageIndex)
		{
			this.l = L;
			this.n = N;
			this.certainty = certainty;
			this.random = random;
			this.usageIndex = usageIndex;
		}

		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x0600222E RID: 8750 RVA: 0x000C423C File Offset: 0x000C423C
		public virtual int L
		{
			get
			{
				return this.l;
			}
		}

		// Token: 0x17000674 RID: 1652
		// (get) Token: 0x0600222F RID: 8751 RVA: 0x000C4244 File Offset: 0x000C4244
		public virtual int N
		{
			get
			{
				return this.n;
			}
		}

		// Token: 0x17000675 RID: 1653
		// (get) Token: 0x06002230 RID: 8752 RVA: 0x000C424C File Offset: 0x000C424C
		public virtual int UsageIndex
		{
			get
			{
				return this.usageIndex;
			}
		}

		// Token: 0x17000676 RID: 1654
		// (get) Token: 0x06002231 RID: 8753 RVA: 0x000C4254 File Offset: 0x000C4254
		public virtual int Certainty
		{
			get
			{
				return this.certainty;
			}
		}

		// Token: 0x17000677 RID: 1655
		// (get) Token: 0x06002232 RID: 8754 RVA: 0x000C425C File Offset: 0x000C425C
		public virtual SecureRandom Random
		{
			get
			{
				return this.random;
			}
		}

		// Token: 0x040015F0 RID: 5616
		public const int DigitalSignatureUsage = 1;

		// Token: 0x040015F1 RID: 5617
		public const int KeyEstablishmentUsage = 2;

		// Token: 0x040015F2 RID: 5618
		private readonly int l;

		// Token: 0x040015F3 RID: 5619
		private readonly int n;

		// Token: 0x040015F4 RID: 5620
		private readonly int certainty;

		// Token: 0x040015F5 RID: 5621
		private readonly SecureRandom random;

		// Token: 0x040015F6 RID: 5622
		private readonly int usageIndex;
	}
}
