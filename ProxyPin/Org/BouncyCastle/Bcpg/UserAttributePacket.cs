using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Bcpg
{
	// Token: 0x020002C3 RID: 707
	public class UserAttributePacket : ContainedPacket
	{
		// Token: 0x060015A9 RID: 5545 RVA: 0x0007250C File Offset: 0x0007250C
		public UserAttributePacket(BcpgInputStream bcpgIn)
		{
			UserAttributeSubpacketsParser userAttributeSubpacketsParser = new UserAttributeSubpacketsParser(bcpgIn);
			IList list = Platform.CreateArrayList();
			UserAttributeSubpacket value;
			while ((value = userAttributeSubpacketsParser.ReadPacket()) != null)
			{
				list.Add(value);
			}
			this.subpackets = new UserAttributeSubpacket[list.Count];
			for (int num = 0; num != this.subpackets.Length; num++)
			{
				this.subpackets[num] = (UserAttributeSubpacket)list[num];
			}
		}

		// Token: 0x060015AA RID: 5546 RVA: 0x00072588 File Offset: 0x00072588
		public UserAttributePacket(UserAttributeSubpacket[] subpackets)
		{
			this.subpackets = subpackets;
		}

		// Token: 0x060015AB RID: 5547 RVA: 0x00072598 File Offset: 0x00072598
		public UserAttributeSubpacket[] GetSubpackets()
		{
			return this.subpackets;
		}

		// Token: 0x060015AC RID: 5548 RVA: 0x000725A0 File Offset: 0x000725A0
		public override void Encode(BcpgOutputStream bcpgOut)
		{
			MemoryStream memoryStream = new MemoryStream();
			for (int num = 0; num != this.subpackets.Length; num++)
			{
				this.subpackets[num].Encode(memoryStream);
			}
			bcpgOut.WritePacket(PacketTag.UserAttribute, memoryStream.ToArray(), false);
		}

		// Token: 0x04000ED7 RID: 3799
		private readonly UserAttributeSubpacket[] subpackets;
	}
}
