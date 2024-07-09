using System;
using Org.BouncyCastle.Crypto.Parameters;

namespace Org.BouncyCastle.Crypto.Agreement
{
	// Token: 0x0200034A RID: 842
	public sealed class X25519Agreement : IRawAgreement
	{
		// Token: 0x06001911 RID: 6417 RVA: 0x00080F68 File Offset: 0x00080F68
		public void Init(ICipherParameters parameters)
		{
			this.privateKey = (X25519PrivateKeyParameters)parameters;
		}

		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x06001912 RID: 6418 RVA: 0x00080F78 File Offset: 0x00080F78
		public int AgreementSize
		{
			get
			{
				return X25519PrivateKeyParameters.SecretSize;
			}
		}

		// Token: 0x06001913 RID: 6419 RVA: 0x00080F80 File Offset: 0x00080F80
		public void CalculateAgreement(ICipherParameters publicKey, byte[] buf, int off)
		{
			this.privateKey.GenerateSecret((X25519PublicKeyParameters)publicKey, buf, off);
		}

		// Token: 0x040010D7 RID: 4311
		private X25519PrivateKeyParameters privateKey;
	}
}
