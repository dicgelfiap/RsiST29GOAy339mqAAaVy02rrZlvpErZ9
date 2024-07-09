using System;
using System.IO;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Bcpg
{
	// Token: 0x020002B9 RID: 697
	public class SecretKeyPacket : ContainedPacket
	{
		// Token: 0x06001577 RID: 5495 RVA: 0x0007138C File Offset: 0x0007138C
		internal SecretKeyPacket(BcpgInputStream bcpgIn)
		{
			if (this is SecretSubkeyPacket)
			{
				this.pubKeyPacket = new PublicSubkeyPacket(bcpgIn);
			}
			else
			{
				this.pubKeyPacket = new PublicKeyPacket(bcpgIn);
			}
			this.s2kUsage = bcpgIn.ReadByte();
			if (this.s2kUsage == 255 || this.s2kUsage == 254)
			{
				this.encAlgorithm = (SymmetricKeyAlgorithmTag)bcpgIn.ReadByte();
				this.s2k = new S2k(bcpgIn);
			}
			else
			{
				this.encAlgorithm = (SymmetricKeyAlgorithmTag)this.s2kUsage;
			}
			if ((this.s2k == null || this.s2k.Type != 101 || this.s2k.ProtectionMode != 1) && this.s2kUsage != 0)
			{
				if (this.encAlgorithm < SymmetricKeyAlgorithmTag.Aes128)
				{
					this.iv = new byte[8];
				}
				else
				{
					this.iv = new byte[16];
				}
				bcpgIn.ReadFully(this.iv);
			}
			this.secKeyData = bcpgIn.ReadAll();
		}

		// Token: 0x06001578 RID: 5496 RVA: 0x0007149C File Offset: 0x0007149C
		public SecretKeyPacket(PublicKeyPacket pubKeyPacket, SymmetricKeyAlgorithmTag encAlgorithm, S2k s2k, byte[] iv, byte[] secKeyData)
		{
			this.pubKeyPacket = pubKeyPacket;
			this.encAlgorithm = encAlgorithm;
			if (encAlgorithm != SymmetricKeyAlgorithmTag.Null)
			{
				this.s2kUsage = 255;
			}
			else
			{
				this.s2kUsage = 0;
			}
			this.s2k = s2k;
			this.iv = Arrays.Clone(iv);
			this.secKeyData = secKeyData;
		}

		// Token: 0x06001579 RID: 5497 RVA: 0x000714FC File Offset: 0x000714FC
		public SecretKeyPacket(PublicKeyPacket pubKeyPacket, SymmetricKeyAlgorithmTag encAlgorithm, int s2kUsage, S2k s2k, byte[] iv, byte[] secKeyData)
		{
			this.pubKeyPacket = pubKeyPacket;
			this.encAlgorithm = encAlgorithm;
			this.s2kUsage = s2kUsage;
			this.s2k = s2k;
			this.iv = Arrays.Clone(iv);
			this.secKeyData = secKeyData;
		}

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x0600157A RID: 5498 RVA: 0x00071538 File Offset: 0x00071538
		public SymmetricKeyAlgorithmTag EncAlgorithm
		{
			get
			{
				return this.encAlgorithm;
			}
		}

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x0600157B RID: 5499 RVA: 0x00071540 File Offset: 0x00071540
		public int S2kUsage
		{
			get
			{
				return this.s2kUsage;
			}
		}

		// Token: 0x0600157C RID: 5500 RVA: 0x00071548 File Offset: 0x00071548
		public byte[] GetIV()
		{
			return Arrays.Clone(this.iv);
		}

		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x0600157D RID: 5501 RVA: 0x00071558 File Offset: 0x00071558
		public S2k S2k
		{
			get
			{
				return this.s2k;
			}
		}

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x0600157E RID: 5502 RVA: 0x00071560 File Offset: 0x00071560
		public PublicKeyPacket PublicKeyPacket
		{
			get
			{
				return this.pubKeyPacket;
			}
		}

		// Token: 0x0600157F RID: 5503 RVA: 0x00071568 File Offset: 0x00071568
		public byte[] GetSecretKeyData()
		{
			return this.secKeyData;
		}

		// Token: 0x06001580 RID: 5504 RVA: 0x00071570 File Offset: 0x00071570
		public byte[] GetEncodedContents()
		{
			MemoryStream memoryStream = new MemoryStream();
			BcpgOutputStream bcpgOutputStream = new BcpgOutputStream(memoryStream);
			bcpgOutputStream.Write(this.pubKeyPacket.GetEncodedContents());
			bcpgOutputStream.WriteByte((byte)this.s2kUsage);
			if (this.s2kUsage == 255 || this.s2kUsage == 254)
			{
				bcpgOutputStream.WriteByte((byte)this.encAlgorithm);
				bcpgOutputStream.WriteObject(this.s2k);
			}
			if (this.iv != null)
			{
				bcpgOutputStream.Write(this.iv);
			}
			if (this.secKeyData != null && this.secKeyData.Length > 0)
			{
				bcpgOutputStream.Write(this.secKeyData);
			}
			return memoryStream.ToArray();
		}

		// Token: 0x06001581 RID: 5505 RVA: 0x00071628 File Offset: 0x00071628
		public override void Encode(BcpgOutputStream bcpgOut)
		{
			bcpgOut.WritePacket(PacketTag.SecretKey, this.GetEncodedContents(), true);
		}

		// Token: 0x04000E94 RID: 3732
		public const int UsageNone = 0;

		// Token: 0x04000E95 RID: 3733
		public const int UsageChecksum = 255;

		// Token: 0x04000E96 RID: 3734
		public const int UsageSha1 = 254;

		// Token: 0x04000E97 RID: 3735
		private PublicKeyPacket pubKeyPacket;

		// Token: 0x04000E98 RID: 3736
		private readonly byte[] secKeyData;

		// Token: 0x04000E99 RID: 3737
		private int s2kUsage;

		// Token: 0x04000E9A RID: 3738
		private SymmetricKeyAlgorithmTag encAlgorithm;

		// Token: 0x04000E9B RID: 3739
		private S2k s2k;

		// Token: 0x04000E9C RID: 3740
		private byte[] iv;
	}
}
