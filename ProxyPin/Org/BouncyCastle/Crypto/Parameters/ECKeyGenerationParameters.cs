using System;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x02000445 RID: 1093
	public class ECKeyGenerationParameters : KeyGenerationParameters
	{
		// Token: 0x06002268 RID: 8808 RVA: 0x000C4A44 File Offset: 0x000C4A44
		public ECKeyGenerationParameters(ECDomainParameters domainParameters, SecureRandom random) : base(random, domainParameters.N.BitLength)
		{
			this.domainParams = domainParameters;
		}

		// Token: 0x06002269 RID: 8809 RVA: 0x000C4A60 File Offset: 0x000C4A60
		public ECKeyGenerationParameters(DerObjectIdentifier publicKeyParamSet, SecureRandom random) : this(ECKeyParameters.LookupParameters(publicKeyParamSet), random)
		{
			this.publicKeyParamSet = publicKeyParamSet;
		}

		// Token: 0x17000689 RID: 1673
		// (get) Token: 0x0600226A RID: 8810 RVA: 0x000C4A78 File Offset: 0x000C4A78
		public ECDomainParameters DomainParameters
		{
			get
			{
				return this.domainParams;
			}
		}

		// Token: 0x1700068A RID: 1674
		// (get) Token: 0x0600226B RID: 8811 RVA: 0x000C4A80 File Offset: 0x000C4A80
		public DerObjectIdentifier PublicKeyParamSet
		{
			get
			{
				return this.publicKeyParamSet;
			}
		}

		// Token: 0x0400160A RID: 5642
		private readonly ECDomainParameters domainParams;

		// Token: 0x0400160B RID: 5643
		private readonly DerObjectIdentifier publicKeyParamSet;
	}
}
