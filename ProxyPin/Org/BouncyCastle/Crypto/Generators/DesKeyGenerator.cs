using System;
using Org.BouncyCastle.Crypto.Parameters;

namespace Org.BouncyCastle.Crypto.Generators
{
	// Token: 0x020003B4 RID: 948
	public class DesKeyGenerator : CipherKeyGenerator
	{
		// Token: 0x06001E47 RID: 7751 RVA: 0x000B156C File Offset: 0x000B156C
		public DesKeyGenerator()
		{
		}

		// Token: 0x06001E48 RID: 7752 RVA: 0x000B1574 File Offset: 0x000B1574
		internal DesKeyGenerator(int defaultStrength) : base(defaultStrength)
		{
		}

		// Token: 0x06001E49 RID: 7753 RVA: 0x000B1580 File Offset: 0x000B1580
		protected override void engineInit(KeyGenerationParameters parameters)
		{
			base.engineInit(parameters);
			if (this.strength == 0 || this.strength == 7)
			{
				this.strength = 8;
				return;
			}
			if (this.strength != 8)
			{
				throw new ArgumentException("DES key must be " + 64 + " bits long.");
			}
		}

		// Token: 0x06001E4A RID: 7754 RVA: 0x000B15E0 File Offset: 0x000B15E0
		protected override byte[] engineGenerateKey()
		{
			byte[] array = new byte[8];
			do
			{
				this.random.NextBytes(array);
				DesParameters.SetOddParity(array);
			}
			while (DesParameters.IsWeakKey(array, 0));
			return array;
		}
	}
}
