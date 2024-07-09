using System;

namespace Org.BouncyCastle.Bcpg
{
	// Token: 0x020002AD RID: 685
	public class ModDetectionCodePacket : ContainedPacket
	{
		// Token: 0x06001533 RID: 5427 RVA: 0x000705E0 File Offset: 0x000705E0
		internal ModDetectionCodePacket(BcpgInputStream bcpgIn)
		{
			if (bcpgIn == null)
			{
				throw new ArgumentNullException("bcpgIn");
			}
			this.digest = new byte[20];
			bcpgIn.ReadFully(this.digest);
		}

		// Token: 0x06001534 RID: 5428 RVA: 0x00070614 File Offset: 0x00070614
		public ModDetectionCodePacket(byte[] digest)
		{
			if (digest == null)
			{
				throw new ArgumentNullException("digest");
			}
			this.digest = (byte[])digest.Clone();
		}

		// Token: 0x06001535 RID: 5429 RVA: 0x00070640 File Offset: 0x00070640
		public byte[] GetDigest()
		{
			return (byte[])this.digest.Clone();
		}

		// Token: 0x06001536 RID: 5430 RVA: 0x00070654 File Offset: 0x00070654
		public override void Encode(BcpgOutputStream bcpgOut)
		{
			bcpgOut.WritePacket(PacketTag.ModificationDetectionCode, this.digest, false);
		}

		// Token: 0x04000E40 RID: 3648
		private readonly byte[] digest;
	}
}
