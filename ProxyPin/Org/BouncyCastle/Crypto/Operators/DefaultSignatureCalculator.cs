using System;
using System.IO;
using Org.BouncyCastle.Crypto.IO;

namespace Org.BouncyCastle.Crypto.Operators
{
	// Token: 0x02000420 RID: 1056
	public class DefaultSignatureCalculator : IStreamCalculator
	{
		// Token: 0x06002197 RID: 8599 RVA: 0x000C2CD4 File Offset: 0x000C2CD4
		public DefaultSignatureCalculator(ISigner signer)
		{
			this.mSignerSink = new SignerSink(signer);
		}

		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x06002198 RID: 8600 RVA: 0x000C2CE8 File Offset: 0x000C2CE8
		public Stream Stream
		{
			get
			{
				return this.mSignerSink;
			}
		}

		// Token: 0x06002199 RID: 8601 RVA: 0x000C2CF0 File Offset: 0x000C2CF0
		public object GetResult()
		{
			return new DefaultSignatureResult(this.mSignerSink.Signer);
		}

		// Token: 0x040015CA RID: 5578
		private readonly SignerSink mSignerSink;
	}
}
