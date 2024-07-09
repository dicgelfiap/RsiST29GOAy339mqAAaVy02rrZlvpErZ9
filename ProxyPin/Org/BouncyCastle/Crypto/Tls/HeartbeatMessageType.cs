using System;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000508 RID: 1288
	public abstract class HeartbeatMessageType
	{
		// Token: 0x06002764 RID: 10084 RVA: 0x000D59E0 File Offset: 0x000D59E0
		public static bool IsValid(byte heartbeatMessageType)
		{
			return heartbeatMessageType >= 1 && heartbeatMessageType <= 2;
		}

		// Token: 0x040019BB RID: 6587
		public const byte heartbeat_request = 1;

		// Token: 0x040019BC RID: 6588
		public const byte heartbeat_response = 2;
	}
}
