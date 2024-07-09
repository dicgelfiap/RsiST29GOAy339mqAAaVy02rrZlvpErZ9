using System;

namespace Org.BouncyCastle.Crypto
{
	// Token: 0x02000349 RID: 841
	public interface IRawAgreement
	{
		// Token: 0x0600190E RID: 6414
		void Init(ICipherParameters parameters);

		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x0600190F RID: 6415
		int AgreementSize { get; }

		// Token: 0x06001910 RID: 6416
		void CalculateAgreement(ICipherParameters publicKey, byte[] buf, int off);
	}
}
