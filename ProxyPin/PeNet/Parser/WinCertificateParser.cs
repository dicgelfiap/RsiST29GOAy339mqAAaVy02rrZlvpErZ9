using System;
using PeNet.Structures;

namespace PeNet.Parser
{
	// Token: 0x02000C07 RID: 3079
	internal class WinCertificateParser : SafeParser<WIN_CERTIFICATE>
	{
		// Token: 0x06007ACC RID: 31436 RVA: 0x00241FD8 File Offset: 0x00241FD8
		internal WinCertificateParser(byte[] buff, uint offset) : base(buff, offset)
		{
		}

		// Token: 0x06007ACD RID: 31437 RVA: 0x00241FE4 File Offset: 0x00241FE4
		protected override WIN_CERTIFICATE ParseTarget()
		{
			if (this._offset == 0U)
			{
				return null;
			}
			return new WIN_CERTIFICATE(this._buff, this._offset);
		}
	}
}
