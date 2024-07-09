using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Signers;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000535 RID: 1333
	public class TlsDssSigner : TlsDsaSigner
	{
		// Token: 0x060028CF RID: 10447 RVA: 0x000DBE78 File Offset: 0x000DBE78
		public override bool IsValidPublicKey(AsymmetricKeyParameter publicKey)
		{
			return publicKey is DsaPublicKeyParameters;
		}

		// Token: 0x060028D0 RID: 10448 RVA: 0x000DBE84 File Offset: 0x000DBE84
		protected override IDsa CreateDsaImpl(byte hashAlgorithm)
		{
			return new DsaSigner(new HMacDsaKCalculator(TlsUtilities.CreateHash(hashAlgorithm)));
		}

		// Token: 0x17000792 RID: 1938
		// (get) Token: 0x060028D1 RID: 10449 RVA: 0x000DBE98 File Offset: 0x000DBE98
		protected override byte SignatureAlgorithm
		{
			get
			{
				return 2;
			}
		}
	}
}
