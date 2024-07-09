using System;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.CryptoPro;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x02000455 RID: 1109
	public abstract class Gost3410KeyParameters : AsymmetricKeyParameter
	{
		// Token: 0x060022C5 RID: 8901 RVA: 0x000C5810 File Offset: 0x000C5810
		protected Gost3410KeyParameters(bool isPrivate, Gost3410Parameters parameters) : base(isPrivate)
		{
			this.parameters = parameters;
		}

		// Token: 0x060022C6 RID: 8902 RVA: 0x000C5820 File Offset: 0x000C5820
		protected Gost3410KeyParameters(bool isPrivate, DerObjectIdentifier publicKeyParamSet) : base(isPrivate)
		{
			this.parameters = Gost3410KeyParameters.LookupParameters(publicKeyParamSet);
			this.publicKeyParamSet = publicKeyParamSet;
		}

		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x060022C7 RID: 8903 RVA: 0x000C583C File Offset: 0x000C583C
		public Gost3410Parameters Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x060022C8 RID: 8904 RVA: 0x000C5844 File Offset: 0x000C5844
		public DerObjectIdentifier PublicKeyParamSet
		{
			get
			{
				return this.publicKeyParamSet;
			}
		}

		// Token: 0x060022C9 RID: 8905 RVA: 0x000C584C File Offset: 0x000C584C
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

		// Token: 0x04001627 RID: 5671
		private readonly Gost3410Parameters parameters;

		// Token: 0x04001628 RID: 5672
		private readonly DerObjectIdentifier publicKeyParamSet;
	}
}
