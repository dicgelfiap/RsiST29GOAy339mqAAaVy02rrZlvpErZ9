using System;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x020002FC RID: 764
	internal class CounterSignatureDigestCalculator : IDigestCalculator
	{
		// Token: 0x0600172D RID: 5933 RVA: 0x00079560 File Offset: 0x00079560
		internal CounterSignatureDigestCalculator(string alg, byte[] data)
		{
			this.alg = alg;
			this.data = data;
		}

		// Token: 0x0600172E RID: 5934 RVA: 0x00079578 File Offset: 0x00079578
		public byte[] GetDigest()
		{
			IDigest digestInstance = CmsSignedHelper.Instance.GetDigestInstance(this.alg);
			return DigestUtilities.DoFinal(digestInstance, this.data);
		}

		// Token: 0x04000F9E RID: 3998
		private readonly string alg;

		// Token: 0x04000F9F RID: 3999
		private readonly byte[] data;
	}
}
