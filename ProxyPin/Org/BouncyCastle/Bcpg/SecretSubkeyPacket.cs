using System;

namespace Org.BouncyCastle.Bcpg
{
	// Token: 0x020002BA RID: 698
	public class SecretSubkeyPacket : SecretKeyPacket
	{
		// Token: 0x06001582 RID: 5506 RVA: 0x00071638 File Offset: 0x00071638
		internal SecretSubkeyPacket(BcpgInputStream bcpgIn) : base(bcpgIn)
		{
		}

		// Token: 0x06001583 RID: 5507 RVA: 0x00071644 File Offset: 0x00071644
		public SecretSubkeyPacket(PublicKeyPacket pubKeyPacket, SymmetricKeyAlgorithmTag encAlgorithm, S2k s2k, byte[] iv, byte[] secKeyData) : base(pubKeyPacket, encAlgorithm, s2k, iv, secKeyData)
		{
		}

		// Token: 0x06001584 RID: 5508 RVA: 0x00071654 File Offset: 0x00071654
		public SecretSubkeyPacket(PublicKeyPacket pubKeyPacket, SymmetricKeyAlgorithmTag encAlgorithm, int s2kUsage, S2k s2k, byte[] iv, byte[] secKeyData) : base(pubKeyPacket, encAlgorithm, s2kUsage, s2k, iv, secKeyData)
		{
		}

		// Token: 0x06001585 RID: 5509 RVA: 0x00071668 File Offset: 0x00071668
		public override void Encode(BcpgOutputStream bcpgOut)
		{
			bcpgOut.WritePacket(PacketTag.SecretSubkey, base.GetEncodedContents(), true);
		}
	}
}
