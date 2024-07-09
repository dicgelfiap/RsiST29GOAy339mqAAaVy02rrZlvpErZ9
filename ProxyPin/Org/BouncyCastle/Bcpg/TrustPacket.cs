using System;
using System.IO;

namespace Org.BouncyCastle.Bcpg
{
	// Token: 0x020002C2 RID: 706
	public class TrustPacket : ContainedPacket
	{
		// Token: 0x060015A5 RID: 5541 RVA: 0x00072474 File Offset: 0x00072474
		public TrustPacket(BcpgInputStream bcpgIn)
		{
			MemoryStream memoryStream = new MemoryStream();
			int num;
			while ((num = bcpgIn.ReadByte()) >= 0)
			{
				memoryStream.WriteByte((byte)num);
			}
			this.levelAndTrustAmount = memoryStream.ToArray();
		}

		// Token: 0x060015A6 RID: 5542 RVA: 0x000724B8 File Offset: 0x000724B8
		public TrustPacket(int trustCode)
		{
			this.levelAndTrustAmount = new byte[]
			{
				(byte)trustCode
			};
		}

		// Token: 0x060015A7 RID: 5543 RVA: 0x000724E4 File Offset: 0x000724E4
		public byte[] GetLevelAndTrustAmount()
		{
			return (byte[])this.levelAndTrustAmount.Clone();
		}

		// Token: 0x060015A8 RID: 5544 RVA: 0x000724F8 File Offset: 0x000724F8
		public override void Encode(BcpgOutputStream bcpgOut)
		{
			bcpgOut.WritePacket(PacketTag.Trust, this.levelAndTrustAmount, true);
		}

		// Token: 0x04000ED6 RID: 3798
		private readonly byte[] levelAndTrustAmount;
	}
}
