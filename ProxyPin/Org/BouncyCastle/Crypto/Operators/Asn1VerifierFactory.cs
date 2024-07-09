using System;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Operators
{
	// Token: 0x0200041B RID: 1051
	public class Asn1VerifierFactory : IVerifierFactory
	{
		// Token: 0x06002186 RID: 8582 RVA: 0x000C2A40 File Offset: 0x000C2A40
		public Asn1VerifierFactory(string algorithm, AsymmetricKeyParameter publicKey)
		{
			if (algorithm == null)
			{
				throw new ArgumentNullException("algorithm");
			}
			if (publicKey == null)
			{
				throw new ArgumentNullException("publicKey");
			}
			if (publicKey.IsPrivate)
			{
				throw new ArgumentException("Key for verifying must be public", "publicKey");
			}
			DerObjectIdentifier algorithmOid = X509Utilities.GetAlgorithmOid(algorithm);
			this.publicKey = publicKey;
			this.algID = X509Utilities.GetSigAlgID(algorithmOid, algorithm);
		}

		// Token: 0x06002187 RID: 8583 RVA: 0x000C2AB0 File Offset: 0x000C2AB0
		public Asn1VerifierFactory(AlgorithmIdentifier algorithm, AsymmetricKeyParameter publicKey)
		{
			this.publicKey = publicKey;
			this.algID = algorithm;
		}

		// Token: 0x17000651 RID: 1617
		// (get) Token: 0x06002188 RID: 8584 RVA: 0x000C2AC8 File Offset: 0x000C2AC8
		public object AlgorithmDetails
		{
			get
			{
				return this.algID;
			}
		}

		// Token: 0x06002189 RID: 8585 RVA: 0x000C2AD0 File Offset: 0x000C2AD0
		public IStreamCalculator CreateCalculator()
		{
			ISigner signer = SignerUtilities.InitSigner(X509Utilities.GetSignatureName(this.algID), false, this.publicKey, null);
			return new DefaultVerifierCalculator(signer);
		}

		// Token: 0x040015C2 RID: 5570
		private readonly AlgorithmIdentifier algID;

		// Token: 0x040015C3 RID: 5571
		private readonly AsymmetricKeyParameter publicKey;
	}
}
