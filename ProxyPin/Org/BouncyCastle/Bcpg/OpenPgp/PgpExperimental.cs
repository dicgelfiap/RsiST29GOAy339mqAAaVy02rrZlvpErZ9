using System;

namespace Org.BouncyCastle.Bcpg.OpenPgp
{
	// Token: 0x0200064E RID: 1614
	public class PgpExperimental : PgpObject
	{
		// Token: 0x0600381D RID: 14365 RVA: 0x0012DE7C File Offset: 0x0012DE7C
		public PgpExperimental(BcpgInputStream bcpgIn)
		{
			this.p = (ExperimentalPacket)bcpgIn.ReadPacket();
		}

		// Token: 0x04001DAD RID: 7597
		private readonly ExperimentalPacket p;
	}
}
