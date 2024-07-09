using System;

namespace Org.BouncyCastle.Bcpg
{
	// Token: 0x020002BF RID: 703
	public class SymmetricEncIntegrityPacket : InputStreamPacket
	{
		// Token: 0x0600159D RID: 5533 RVA: 0x00072350 File Offset: 0x00072350
		internal SymmetricEncIntegrityPacket(BcpgInputStream bcpgIn) : base(bcpgIn)
		{
			this.version = bcpgIn.ReadByte();
		}

		// Token: 0x04000EC2 RID: 3778
		internal readonly int version;
	}
}
