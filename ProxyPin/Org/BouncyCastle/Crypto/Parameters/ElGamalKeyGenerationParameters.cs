using System;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x0200044F RID: 1103
	public class ElGamalKeyGenerationParameters : KeyGenerationParameters
	{
		// Token: 0x060022A6 RID: 8870 RVA: 0x000C5468 File Offset: 0x000C5468
		public ElGamalKeyGenerationParameters(SecureRandom random, ElGamalParameters parameters) : base(random, ElGamalKeyGenerationParameters.GetStrength(parameters))
		{
			this.parameters = parameters;
		}

		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x060022A7 RID: 8871 RVA: 0x000C5480 File Offset: 0x000C5480
		public ElGamalParameters Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x060022A8 RID: 8872 RVA: 0x000C5488 File Offset: 0x000C5488
		internal static int GetStrength(ElGamalParameters parameters)
		{
			if (parameters.L == 0)
			{
				return parameters.P.BitLength;
			}
			return parameters.L;
		}

		// Token: 0x0400161E RID: 5662
		private readonly ElGamalParameters parameters;
	}
}
