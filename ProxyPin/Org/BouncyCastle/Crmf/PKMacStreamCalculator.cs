using System;
using System.IO;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.IO;

namespace Org.BouncyCastle.Crmf
{
	// Token: 0x02000325 RID: 805
	internal class PKMacStreamCalculator : IStreamCalculator
	{
		// Token: 0x0600183D RID: 6205 RVA: 0x0007D760 File Offset: 0x0007D760
		public PKMacStreamCalculator(IMac mac)
		{
			this._stream = new MacSink(mac);
		}

		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x0600183E RID: 6206 RVA: 0x0007D774 File Offset: 0x0007D774
		public Stream Stream
		{
			get
			{
				return this._stream;
			}
		}

		// Token: 0x0600183F RID: 6207 RVA: 0x0007D77C File Offset: 0x0007D77C
		public object GetResult()
		{
			return new DefaultPKMacResult(this._stream.Mac);
		}

		// Token: 0x04001010 RID: 4112
		private readonly MacSink _stream;
	}
}
