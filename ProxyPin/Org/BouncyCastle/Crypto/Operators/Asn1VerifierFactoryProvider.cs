using System;
using System.Collections;
using Org.BouncyCastle.Asn1.X509;

namespace Org.BouncyCastle.Crypto.Operators
{
	// Token: 0x0200041D RID: 1053
	public class Asn1VerifierFactoryProvider : IVerifierFactoryProvider
	{
		// Token: 0x0600218B RID: 8587 RVA: 0x000C2B00 File Offset: 0x000C2B00
		public Asn1VerifierFactoryProvider(AsymmetricKeyParameter publicKey)
		{
			this.publicKey = publicKey;
		}

		// Token: 0x0600218C RID: 8588 RVA: 0x000C2B10 File Offset: 0x000C2B10
		public IVerifierFactory CreateVerifierFactory(object algorithmDetails)
		{
			return new Asn1VerifierFactory((AlgorithmIdentifier)algorithmDetails, this.publicKey);
		}

		// Token: 0x17000652 RID: 1618
		// (get) Token: 0x0600218D RID: 8589 RVA: 0x000C2B24 File Offset: 0x000C2B24
		public IEnumerable SignatureAlgNames
		{
			get
			{
				return X509Utilities.GetAlgNames();
			}
		}

		// Token: 0x040015C4 RID: 5572
		private readonly AsymmetricKeyParameter publicKey;
	}
}
