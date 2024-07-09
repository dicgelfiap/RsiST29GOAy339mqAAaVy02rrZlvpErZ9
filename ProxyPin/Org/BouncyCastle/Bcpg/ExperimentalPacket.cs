using System;

namespace Org.BouncyCastle.Bcpg
{
	// Token: 0x020002A9 RID: 681
	public class ExperimentalPacket : ContainedPacket
	{
		// Token: 0x06001528 RID: 5416 RVA: 0x00070484 File Offset: 0x00070484
		internal ExperimentalPacket(PacketTag tag, BcpgInputStream bcpgIn)
		{
			this.tag = tag;
			this.contents = bcpgIn.ReadAll();
		}

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x06001529 RID: 5417 RVA: 0x000704A0 File Offset: 0x000704A0
		public PacketTag Tag
		{
			get
			{
				return this.tag;
			}
		}

		// Token: 0x0600152A RID: 5418 RVA: 0x000704A8 File Offset: 0x000704A8
		public byte[] GetContents()
		{
			return (byte[])this.contents.Clone();
		}

		// Token: 0x0600152B RID: 5419 RVA: 0x000704BC File Offset: 0x000704BC
		public override void Encode(BcpgOutputStream bcpgOut)
		{
			bcpgOut.WritePacket(this.tag, this.contents, true);
		}

		// Token: 0x04000E2E RID: 3630
		private readonly PacketTag tag;

		// Token: 0x04000E2F RID: 3631
		private readonly byte[] contents;
	}
}
