using System;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x0200043B RID: 1083
	public class DsaKeyGenerationParameters : KeyGenerationParameters
	{
		// Token: 0x06002225 RID: 8741 RVA: 0x000C4128 File Offset: 0x000C4128
		public DsaKeyGenerationParameters(SecureRandom random, DsaParameters parameters) : base(random, parameters.P.BitLength - 1)
		{
			this.parameters = parameters;
		}

		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x06002226 RID: 8742 RVA: 0x000C4154 File Offset: 0x000C4154
		public DsaParameters Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x040015EE RID: 5614
		private readonly DsaParameters parameters;
	}
}
