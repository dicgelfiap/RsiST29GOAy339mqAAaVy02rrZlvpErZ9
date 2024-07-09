using System;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto
{
	// Token: 0x02000433 RID: 1075
	public class KeyGenerationParameters
	{
		// Token: 0x060021EF RID: 8687 RVA: 0x000C39B8 File Offset: 0x000C39B8
		public KeyGenerationParameters(SecureRandom random, int strength)
		{
			if (random == null)
			{
				throw new ArgumentNullException("random");
			}
			if (strength < 1)
			{
				throw new ArgumentException("strength must be a positive value", "strength");
			}
			this.random = random;
			this.strength = strength;
		}

		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x060021F0 RID: 8688 RVA: 0x000C39F8 File Offset: 0x000C39F8
		public SecureRandom Random
		{
			get
			{
				return this.random;
			}
		}

		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x060021F1 RID: 8689 RVA: 0x000C3A00 File Offset: 0x000C3A00
		public int Strength
		{
			get
			{
				return this.strength;
			}
		}

		// Token: 0x040015DC RID: 5596
		private SecureRandom random;

		// Token: 0x040015DD RID: 5597
		private int strength;
	}
}
