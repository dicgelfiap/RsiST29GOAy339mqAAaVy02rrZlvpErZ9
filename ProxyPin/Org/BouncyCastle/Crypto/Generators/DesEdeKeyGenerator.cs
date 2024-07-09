using System;
using Org.BouncyCastle.Crypto.Parameters;

namespace Org.BouncyCastle.Crypto.Generators
{
	// Token: 0x020003B5 RID: 949
	public class DesEdeKeyGenerator : DesKeyGenerator
	{
		// Token: 0x06001E4B RID: 7755 RVA: 0x000B1614 File Offset: 0x000B1614
		public DesEdeKeyGenerator()
		{
		}

		// Token: 0x06001E4C RID: 7756 RVA: 0x000B161C File Offset: 0x000B161C
		internal DesEdeKeyGenerator(int defaultStrength) : base(defaultStrength)
		{
		}

		// Token: 0x06001E4D RID: 7757 RVA: 0x000B1628 File Offset: 0x000B1628
		protected override void engineInit(KeyGenerationParameters parameters)
		{
			this.random = parameters.Random;
			this.strength = (parameters.Strength + 7) / 8;
			if (this.strength == 0 || this.strength == 21)
			{
				this.strength = 24;
				return;
			}
			if (this.strength == 14)
			{
				this.strength = 16;
				return;
			}
			if (this.strength != 24 && this.strength != 16)
			{
				throw new ArgumentException(string.Concat(new object[]
				{
					"DESede key must be ",
					192,
					" or ",
					128,
					" bits long."
				}));
			}
		}

		// Token: 0x06001E4E RID: 7758 RVA: 0x000B16EC File Offset: 0x000B16EC
		protected override byte[] engineGenerateKey()
		{
			byte[] array = new byte[this.strength];
			do
			{
				this.random.NextBytes(array);
				DesParameters.SetOddParity(array);
			}
			while (DesEdeParameters.IsWeakKey(array, 0, array.Length) || !DesEdeParameters.IsRealEdeKey(array, 0));
			return array;
		}
	}
}
