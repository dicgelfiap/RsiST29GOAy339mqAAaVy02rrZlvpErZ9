using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Generators
{
	// Token: 0x020003BA RID: 954
	public class DHParametersGenerator
	{
		// Token: 0x06001E5B RID: 7771 RVA: 0x000B18E0 File Offset: 0x000B18E0
		public virtual void Init(int size, int certainty, SecureRandom random)
		{
			this.size = size;
			this.certainty = certainty;
			this.random = random;
		}

		// Token: 0x06001E5C RID: 7772 RVA: 0x000B18F8 File Offset: 0x000B18F8
		public virtual DHParameters GenerateParameters()
		{
			BigInteger[] array = DHParametersHelper.GenerateSafePrimes(this.size, this.certainty, this.random);
			BigInteger p = array[0];
			BigInteger q = array[1];
			BigInteger g = DHParametersHelper.SelectGenerator(p, q, this.random);
			return new DHParameters(p, g, q, BigInteger.Two, null);
		}

		// Token: 0x0400141B RID: 5147
		private int size;

		// Token: 0x0400141C RID: 5148
		private int certainty;

		// Token: 0x0400141D RID: 5149
		private SecureRandom random;
	}
}
