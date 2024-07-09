using System;
using Org.BouncyCastle.Crypto.Parameters;

namespace Org.BouncyCastle.Crypto.Agreement
{
	// Token: 0x0200034B RID: 843
	public sealed class X448Agreement : IRawAgreement
	{
		// Token: 0x06001915 RID: 6421 RVA: 0x00080FA0 File Offset: 0x00080FA0
		public void Init(ICipherParameters parameters)
		{
			this.privateKey = (X448PrivateKeyParameters)parameters;
		}

		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x06001916 RID: 6422 RVA: 0x00080FB0 File Offset: 0x00080FB0
		public int AgreementSize
		{
			get
			{
				return X448PrivateKeyParameters.SecretSize;
			}
		}

		// Token: 0x06001917 RID: 6423 RVA: 0x00080FB8 File Offset: 0x00080FB8
		public void CalculateAgreement(ICipherParameters publicKey, byte[] buf, int off)
		{
			this.privateKey.GenerateSecret((X448PublicKeyParameters)publicKey, buf, off);
		}

		// Token: 0x040010D8 RID: 4312
		private X448PrivateKeyParameters privateKey;
	}
}
