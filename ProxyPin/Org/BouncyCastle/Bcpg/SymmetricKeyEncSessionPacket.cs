using System;
using System.IO;

namespace Org.BouncyCastle.Bcpg
{
	// Token: 0x020002C1 RID: 705
	public class SymmetricKeyEncSessionPacket : ContainedPacket
	{
		// Token: 0x0600159E RID: 5534 RVA: 0x00072368 File Offset: 0x00072368
		public SymmetricKeyEncSessionPacket(BcpgInputStream bcpgIn)
		{
			this.version = bcpgIn.ReadByte();
			this.encAlgorithm = (SymmetricKeyAlgorithmTag)bcpgIn.ReadByte();
			this.s2k = new S2k(bcpgIn);
			this.secKeyData = bcpgIn.ReadAll();
		}

		// Token: 0x0600159F RID: 5535 RVA: 0x000723B0 File Offset: 0x000723B0
		public SymmetricKeyEncSessionPacket(SymmetricKeyAlgorithmTag encAlgorithm, S2k s2k, byte[] secKeyData)
		{
			this.version = 4;
			this.encAlgorithm = encAlgorithm;
			this.s2k = s2k;
			this.secKeyData = secKeyData;
		}

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x060015A0 RID: 5536 RVA: 0x000723D4 File Offset: 0x000723D4
		public SymmetricKeyAlgorithmTag EncAlgorithm
		{
			get
			{
				return this.encAlgorithm;
			}
		}

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x060015A1 RID: 5537 RVA: 0x000723DC File Offset: 0x000723DC
		public S2k S2k
		{
			get
			{
				return this.s2k;
			}
		}

		// Token: 0x060015A2 RID: 5538 RVA: 0x000723E4 File Offset: 0x000723E4
		public byte[] GetSecKeyData()
		{
			return this.secKeyData;
		}

		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x060015A3 RID: 5539 RVA: 0x000723EC File Offset: 0x000723EC
		public int Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x060015A4 RID: 5540 RVA: 0x000723F4 File Offset: 0x000723F4
		public override void Encode(BcpgOutputStream bcpgOut)
		{
			MemoryStream memoryStream = new MemoryStream();
			BcpgOutputStream bcpgOutputStream = new BcpgOutputStream(memoryStream);
			bcpgOutputStream.Write(new byte[]
			{
				(byte)this.version,
				(byte)this.encAlgorithm
			});
			bcpgOutputStream.WriteObject(this.s2k);
			if (this.secKeyData != null && this.secKeyData.Length > 0)
			{
				bcpgOutputStream.Write(this.secKeyData);
			}
			bcpgOut.WritePacket(PacketTag.SymmetricKeyEncryptedSessionKey, memoryStream.ToArray(), true);
		}

		// Token: 0x04000ED2 RID: 3794
		private int version;

		// Token: 0x04000ED3 RID: 3795
		private SymmetricKeyAlgorithmTag encAlgorithm;

		// Token: 0x04000ED4 RID: 3796
		private S2k s2k;

		// Token: 0x04000ED5 RID: 3797
		private readonly byte[] secKeyData;
	}
}
