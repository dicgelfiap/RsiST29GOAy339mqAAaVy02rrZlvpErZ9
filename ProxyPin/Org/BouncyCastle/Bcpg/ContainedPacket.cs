using System;
using System.IO;

namespace Org.BouncyCastle.Bcpg
{
	// Token: 0x0200029E RID: 670
	public abstract class ContainedPacket : Packet
	{
		// Token: 0x060014E8 RID: 5352 RVA: 0x0006FCF8 File Offset: 0x0006FCF8
		public byte[] GetEncoded()
		{
			MemoryStream memoryStream = new MemoryStream();
			BcpgOutputStream bcpgOutputStream = new BcpgOutputStream(memoryStream);
			bcpgOutputStream.WritePacket(this);
			return memoryStream.ToArray();
		}

		// Token: 0x060014E9 RID: 5353
		public abstract void Encode(BcpgOutputStream bcpgOut);
	}
}
