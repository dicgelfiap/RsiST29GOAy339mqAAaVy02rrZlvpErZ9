using System;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000509 RID: 1289
	public abstract class HeartbeatMode
	{
		// Token: 0x06002766 RID: 10086 RVA: 0x000D59FC File Offset: 0x000D59FC
		public static bool IsValid(byte heartbeatMode)
		{
			return heartbeatMode >= 1 && heartbeatMode <= 2;
		}

		// Token: 0x040019BD RID: 6589
		public const byte peer_allowed_to_send = 1;

		// Token: 0x040019BE RID: 6590
		public const byte peer_not_allowed_to_send = 2;
	}
}
