using System;
using System.IO;
using Org.BouncyCastle.Crypto.IO;

namespace Org.BouncyCastle.Crypto.Operators
{
	// Token: 0x02000422 RID: 1058
	public class DefaultVerifierCalculator : IStreamCalculator
	{
		// Token: 0x0600219D RID: 8605 RVA: 0x000C2D48 File Offset: 0x000C2D48
		public DefaultVerifierCalculator(ISigner signer)
		{
			this.mSignerSink = new SignerSink(signer);
		}

		// Token: 0x17000655 RID: 1621
		// (get) Token: 0x0600219E RID: 8606 RVA: 0x000C2D5C File Offset: 0x000C2D5C
		public Stream Stream
		{
			get
			{
				return this.mSignerSink;
			}
		}

		// Token: 0x0600219F RID: 8607 RVA: 0x000C2D64 File Offset: 0x000C2D64
		public object GetResult()
		{
			return new DefaultVerifierResult(this.mSignerSink.Signer);
		}

		// Token: 0x040015CC RID: 5580
		private readonly SignerSink mSignerSink;
	}
}
