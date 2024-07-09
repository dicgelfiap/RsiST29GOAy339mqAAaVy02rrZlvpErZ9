using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Operators
{
	// Token: 0x02000424 RID: 1060
	public class DefaultVerifierResult : IVerifier
	{
		// Token: 0x060021A2 RID: 8610 RVA: 0x000C2D78 File Offset: 0x000C2D78
		public DefaultVerifierResult(ISigner signer)
		{
			this.mSigner = signer;
		}

		// Token: 0x060021A3 RID: 8611 RVA: 0x000C2D88 File Offset: 0x000C2D88
		public bool IsVerified(byte[] signature)
		{
			return this.mSigner.VerifySignature(signature);
		}

		// Token: 0x060021A4 RID: 8612 RVA: 0x000C2D98 File Offset: 0x000C2D98
		public bool IsVerified(byte[] sig, int sigOff, int sigLen)
		{
			byte[] signature = Arrays.CopyOfRange(sig, sigOff, sigOff + sigLen);
			return this.IsVerified(signature);
		}

		// Token: 0x040015CD RID: 5581
		private readonly ISigner mSigner;
	}
}
