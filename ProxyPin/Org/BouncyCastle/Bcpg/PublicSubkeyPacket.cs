using System;

namespace Org.BouncyCastle.Bcpg
{
	// Token: 0x020002B5 RID: 693
	public class PublicSubkeyPacket : PublicKeyPacket
	{
		// Token: 0x06001556 RID: 5462 RVA: 0x00070DF8 File Offset: 0x00070DF8
		internal PublicSubkeyPacket(BcpgInputStream bcpgIn) : base(bcpgIn)
		{
		}

		// Token: 0x06001557 RID: 5463 RVA: 0x00070E04 File Offset: 0x00070E04
		public PublicSubkeyPacket(PublicKeyAlgorithmTag algorithm, DateTime time, IBcpgKey key) : base(algorithm, time, key)
		{
		}

		// Token: 0x06001558 RID: 5464 RVA: 0x00070E10 File Offset: 0x00070E10
		public override void Encode(BcpgOutputStream bcpgOut)
		{
			bcpgOut.WritePacket(PacketTag.PublicSubkey, this.GetEncodedContents(), true);
		}
	}
}
