using System;

namespace Org.BouncyCastle.Bcpg
{
	// Token: 0x0200029B RID: 667
	public class InputStreamPacket : Packet
	{
		// Token: 0x060014E4 RID: 5348 RVA: 0x0006FCC0 File Offset: 0x0006FCC0
		public InputStreamPacket(BcpgInputStream bcpgIn)
		{
			this.bcpgIn = bcpgIn;
		}

		// Token: 0x060014E5 RID: 5349 RVA: 0x0006FCD0 File Offset: 0x0006FCD0
		public BcpgInputStream GetInputStream()
		{
			return this.bcpgIn;
		}

		// Token: 0x04000E15 RID: 3605
		private readonly BcpgInputStream bcpgIn;
	}
}
