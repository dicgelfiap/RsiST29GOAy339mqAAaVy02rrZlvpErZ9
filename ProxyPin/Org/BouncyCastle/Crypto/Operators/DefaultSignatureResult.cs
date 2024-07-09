using System;

namespace Org.BouncyCastle.Crypto.Operators
{
	// Token: 0x02000421 RID: 1057
	public class DefaultSignatureResult : IBlockResult
	{
		// Token: 0x0600219A RID: 8602 RVA: 0x000C2D04 File Offset: 0x000C2D04
		public DefaultSignatureResult(ISigner signer)
		{
			this.mSigner = signer;
		}

		// Token: 0x0600219B RID: 8603 RVA: 0x000C2D14 File Offset: 0x000C2D14
		public byte[] Collect()
		{
			return this.mSigner.GenerateSignature();
		}

		// Token: 0x0600219C RID: 8604 RVA: 0x000C2D24 File Offset: 0x000C2D24
		public int Collect(byte[] sig, int sigOff)
		{
			byte[] array = this.Collect();
			array.CopyTo(sig, sigOff);
			return array.Length;
		}

		// Token: 0x040015CB RID: 5579
		private readonly ISigner mSigner;
	}
}
