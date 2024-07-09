using System;

namespace Org.BouncyCastle.Bcpg
{
	// Token: 0x020002AC RID: 684
	public class MarkerPacket : ContainedPacket
	{
		// Token: 0x06001531 RID: 5425 RVA: 0x000705A0 File Offset: 0x000705A0
		public MarkerPacket(BcpgInputStream bcpgIn)
		{
			bcpgIn.ReadFully(this.marker);
		}

		// Token: 0x06001532 RID: 5426 RVA: 0x000705CC File Offset: 0x000705CC
		public override void Encode(BcpgOutputStream bcpgOut)
		{
			bcpgOut.WritePacket(PacketTag.Marker, this.marker, true);
		}

		// Token: 0x04000E3F RID: 3647
		private byte[] marker = new byte[]
		{
			80,
			71,
			80
		};
	}
}
