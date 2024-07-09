using System;
using System.Collections;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Operators
{
	// Token: 0x02000419 RID: 1049
	public class Asn1SignatureFactory : ISignatureFactory
	{
		// Token: 0x0600217F RID: 8575 RVA: 0x000C2974 File Offset: 0x000C2974
		public Asn1SignatureFactory(string algorithm, AsymmetricKeyParameter privateKey) : this(algorithm, privateKey, null)
		{
		}

		// Token: 0x06002180 RID: 8576 RVA: 0x000C2980 File Offset: 0x000C2980
		public Asn1SignatureFactory(string algorithm, AsymmetricKeyParameter privateKey, SecureRandom random)
		{
			if (algorithm == null)
			{
				throw new ArgumentNullException("algorithm");
			}
			if (privateKey == null)
			{
				throw new ArgumentNullException("privateKey");
			}
			if (!privateKey.IsPrivate)
			{
				throw new ArgumentException("Key for signing must be private", "privateKey");
			}
			DerObjectIdentifier algorithmOid = X509Utilities.GetAlgorithmOid(algorithm);
			this.algorithm = algorithm;
			this.privateKey = privateKey;
			this.random = random;
			this.algID = X509Utilities.GetSigAlgID(algorithmOid, algorithm);
		}

		// Token: 0x1700064E RID: 1614
		// (get) Token: 0x06002181 RID: 8577 RVA: 0x000C2A00 File Offset: 0x000C2A00
		public object AlgorithmDetails
		{
			get
			{
				return this.algID;
			}
		}

		// Token: 0x06002182 RID: 8578 RVA: 0x000C2A08 File Offset: 0x000C2A08
		public IStreamCalculator CreateCalculator()
		{
			ISigner signer = SignerUtilities.InitSigner(this.algorithm, true, this.privateKey, this.random);
			return new DefaultSignatureCalculator(signer);
		}

		// Token: 0x1700064F RID: 1615
		// (get) Token: 0x06002183 RID: 8579 RVA: 0x000C2A38 File Offset: 0x000C2A38
		public static IEnumerable SignatureAlgNames
		{
			get
			{
				return X509Utilities.GetAlgNames();
			}
		}

		// Token: 0x040015BE RID: 5566
		private readonly AlgorithmIdentifier algID;

		// Token: 0x040015BF RID: 5567
		private readonly string algorithm;

		// Token: 0x040015C0 RID: 5568
		private readonly AsymmetricKeyParameter privateKey;

		// Token: 0x040015C1 RID: 5569
		private readonly SecureRandom random;
	}
}
