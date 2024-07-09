using System;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.CryptoPro;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x02000454 RID: 1108
	public class Gost3410KeyGenerationParameters : KeyGenerationParameters
	{
		// Token: 0x060022C0 RID: 8896 RVA: 0x000C5764 File Offset: 0x000C5764
		public Gost3410KeyGenerationParameters(SecureRandom random, Gost3410Parameters parameters) : base(random, parameters.P.BitLength - 1)
		{
			this.parameters = parameters;
		}

		// Token: 0x060022C1 RID: 8897 RVA: 0x000C5790 File Offset: 0x000C5790
		public Gost3410KeyGenerationParameters(SecureRandom random, DerObjectIdentifier publicKeyParamSet) : this(random, Gost3410KeyGenerationParameters.LookupParameters(publicKeyParamSet))
		{
			this.publicKeyParamSet = publicKeyParamSet;
		}

		// Token: 0x17000697 RID: 1687
		// (get) Token: 0x060022C2 RID: 8898 RVA: 0x000C57A8 File Offset: 0x000C57A8
		public Gost3410Parameters Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x17000698 RID: 1688
		// (get) Token: 0x060022C3 RID: 8899 RVA: 0x000C57B0 File Offset: 0x000C57B0
		public DerObjectIdentifier PublicKeyParamSet
		{
			get
			{
				return this.publicKeyParamSet;
			}
		}

		// Token: 0x060022C4 RID: 8900 RVA: 0x000C57B8 File Offset: 0x000C57B8
		private static Gost3410Parameters LookupParameters(DerObjectIdentifier publicKeyParamSet)
		{
			if (publicKeyParamSet == null)
			{
				throw new ArgumentNullException("publicKeyParamSet");
			}
			Gost3410ParamSetParameters byOid = Gost3410NamedParameters.GetByOid(publicKeyParamSet);
			if (byOid == null)
			{
				throw new ArgumentException("OID is not a valid CryptoPro public key parameter set", "publicKeyParamSet");
			}
			return new Gost3410Parameters(byOid.P, byOid.Q, byOid.A);
		}

		// Token: 0x04001625 RID: 5669
		private readonly Gost3410Parameters parameters;

		// Token: 0x04001626 RID: 5670
		private readonly DerObjectIdentifier publicKeyParamSet;
	}
}
