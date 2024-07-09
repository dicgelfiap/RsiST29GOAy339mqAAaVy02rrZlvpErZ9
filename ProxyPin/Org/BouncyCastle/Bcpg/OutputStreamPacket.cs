using System;

namespace Org.BouncyCastle.Bcpg
{
	// Token: 0x020002B0 RID: 688
	public abstract class OutputStreamPacket
	{
		// Token: 0x06001543 RID: 5443 RVA: 0x00070948 File Offset: 0x00070948
		internal OutputStreamPacket(BcpgOutputStream bcpgOut)
		{
			if (bcpgOut == null)
			{
				throw new ArgumentNullException("bcpgOut");
			}
			this.bcpgOut = bcpgOut;
		}

		// Token: 0x06001544 RID: 5444
		public abstract BcpgOutputStream Open();

		// Token: 0x06001545 RID: 5445
		public abstract void Close();

		// Token: 0x04000E48 RID: 3656
		private readonly BcpgOutputStream bcpgOut;
	}
}
