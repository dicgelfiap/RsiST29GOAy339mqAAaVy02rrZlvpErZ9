using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Signers;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000539 RID: 1337
	public class TlsECDsaSigner : TlsDsaSigner
	{
		// Token: 0x06002910 RID: 10512 RVA: 0x000DD26C File Offset: 0x000DD26C
		public override bool IsValidPublicKey(AsymmetricKeyParameter publicKey)
		{
			return publicKey is ECPublicKeyParameters;
		}

		// Token: 0x06002911 RID: 10513 RVA: 0x000DD278 File Offset: 0x000DD278
		protected override IDsa CreateDsaImpl(byte hashAlgorithm)
		{
			return new ECDsaSigner(new HMacDsaKCalculator(TlsUtilities.CreateHash(hashAlgorithm)));
		}

		// Token: 0x17000794 RID: 1940
		// (get) Token: 0x06002912 RID: 10514 RVA: 0x000DD28C File Offset: 0x000DD28C
		protected override byte SignatureAlgorithm
		{
			get
			{
				return 3;
			}
		}
	}
}
