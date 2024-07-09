using System;
using PeNet.Parser;

namespace PeNet.Authenticode
{
	// Token: 0x02000C0B RID: 3083
	internal class AuthenticodeParser : SafeParser<AuthenticodeInfo>
	{
		// Token: 0x06007AE3 RID: 31459 RVA: 0x00244624 File Offset: 0x00244624
		internal AuthenticodeParser(PeFile peFile) : base(null, 0U)
		{
			this._peFile = peFile;
		}

		// Token: 0x06007AE4 RID: 31460 RVA: 0x00244638 File Offset: 0x00244638
		protected override AuthenticodeInfo ParseTarget()
		{
			return new AuthenticodeInfo(this._peFile);
		}

		// Token: 0x04003B2F RID: 15151
		private readonly PeFile _peFile;
	}
}
