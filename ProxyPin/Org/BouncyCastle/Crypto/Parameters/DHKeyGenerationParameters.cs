using System;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x02000434 RID: 1076
	public class DHKeyGenerationParameters : KeyGenerationParameters
	{
		// Token: 0x060021F2 RID: 8690 RVA: 0x000C3A08 File Offset: 0x000C3A08
		public DHKeyGenerationParameters(SecureRandom random, DHParameters parameters) : base(random, DHKeyGenerationParameters.GetStrength(parameters))
		{
			this.parameters = parameters;
		}

		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x060021F3 RID: 8691 RVA: 0x000C3A20 File Offset: 0x000C3A20
		public DHParameters Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x060021F4 RID: 8692 RVA: 0x000C3A28 File Offset: 0x000C3A28
		internal static int GetStrength(DHParameters parameters)
		{
			if (parameters.L == 0)
			{
				return parameters.P.BitLength;
			}
			return parameters.L;
		}

		// Token: 0x040015DE RID: 5598
		private readonly DHParameters parameters;
	}
}
