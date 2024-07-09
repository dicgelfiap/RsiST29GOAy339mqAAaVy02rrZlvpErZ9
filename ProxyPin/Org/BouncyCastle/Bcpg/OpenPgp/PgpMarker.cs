using System;
using System.IO;

namespace Org.BouncyCastle.Bcpg.OpenPgp
{
	// Token: 0x02000656 RID: 1622
	public class PgpMarker : PgpObject
	{
		// Token: 0x06003847 RID: 14407 RVA: 0x0012E7A0 File Offset: 0x0012E7A0
		public PgpMarker(BcpgInputStream bcpgInput)
		{
			Packet packet = bcpgInput.ReadPacket();
			if (!(packet is MarkerPacket))
			{
				throw new IOException("unexpected packet in stream: " + packet);
			}
			this.data = (MarkerPacket)packet;
		}

		// Token: 0x04001DCC RID: 7628
		private readonly MarkerPacket data;
	}
}
