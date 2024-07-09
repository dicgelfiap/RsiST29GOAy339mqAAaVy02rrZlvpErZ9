using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Generators
{
	// Token: 0x020003C2 RID: 962
	public class ElGamalParametersGenerator
	{
		// Token: 0x06001E87 RID: 7815 RVA: 0x000B2854 File Offset: 0x000B2854
		public void Init(int size, int certainty, SecureRandom random)
		{
			this.size = size;
			this.certainty = certainty;
			this.random = random;
		}

		// Token: 0x06001E88 RID: 7816 RVA: 0x000B286C File Offset: 0x000B286C
		public ElGamalParameters GenerateParameters()
		{
			BigInteger[] array = DHParametersHelper.GenerateSafePrimes(this.size, this.certainty, this.random);
			BigInteger p = array[0];
			BigInteger q = array[1];
			BigInteger g = DHParametersHelper.SelectGenerator(p, q, this.random);
			return new ElGamalParameters(p, g);
		}

		// Token: 0x04001432 RID: 5170
		private int size;

		// Token: 0x04001433 RID: 5171
		private int certainty;

		// Token: 0x04001434 RID: 5172
		private SecureRandom random;
	}
}
