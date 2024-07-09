using System;
using System.Text;

namespace Org.BouncyCastle.Bcpg
{
	// Token: 0x020002C6 RID: 710
	public class UserIdPacket : ContainedPacket
	{
		// Token: 0x060015AF RID: 5551 RVA: 0x00072724 File Offset: 0x00072724
		public UserIdPacket(BcpgInputStream bcpgIn)
		{
			this.idData = bcpgIn.ReadAll();
		}

		// Token: 0x060015B0 RID: 5552 RVA: 0x00072738 File Offset: 0x00072738
		public UserIdPacket(string id)
		{
			this.idData = Encoding.UTF8.GetBytes(id);
		}

		// Token: 0x060015B1 RID: 5553 RVA: 0x00072754 File Offset: 0x00072754
		public string GetId()
		{
			return Encoding.UTF8.GetString(this.idData, 0, this.idData.Length);
		}

		// Token: 0x060015B2 RID: 5554 RVA: 0x00072770 File Offset: 0x00072770
		public override void Encode(BcpgOutputStream bcpgOut)
		{
			bcpgOut.WritePacket(PacketTag.UserId, this.idData, true);
		}

		// Token: 0x04000EDB RID: 3803
		private readonly byte[] idData;
	}
}
