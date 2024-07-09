using System;
using System.IO;

namespace Org.BouncyCastle.Bcpg
{
	// Token: 0x020002AF RID: 687
	public class OnePassSignaturePacket : ContainedPacket
	{
		// Token: 0x0600153C RID: 5436 RVA: 0x00070764 File Offset: 0x00070764
		internal OnePassSignaturePacket(BcpgInputStream bcpgIn)
		{
			this.version = bcpgIn.ReadByte();
			this.sigType = bcpgIn.ReadByte();
			this.hashAlgorithm = (HashAlgorithmTag)bcpgIn.ReadByte();
			this.keyAlgorithm = (PublicKeyAlgorithmTag)bcpgIn.ReadByte();
			this.keyId |= (long)bcpgIn.ReadByte() << 56;
			this.keyId |= (long)bcpgIn.ReadByte() << 48;
			this.keyId |= (long)bcpgIn.ReadByte() << 40;
			this.keyId |= (long)bcpgIn.ReadByte() << 32;
			this.keyId |= (long)bcpgIn.ReadByte() << 24;
			this.keyId |= (long)bcpgIn.ReadByte() << 16;
			this.keyId |= (long)bcpgIn.ReadByte() << 8;
			this.keyId |= (long)((ulong)bcpgIn.ReadByte());
			this.nested = bcpgIn.ReadByte();
		}

		// Token: 0x0600153D RID: 5437 RVA: 0x0007086C File Offset: 0x0007086C
		public OnePassSignaturePacket(int sigType, HashAlgorithmTag hashAlgorithm, PublicKeyAlgorithmTag keyAlgorithm, long keyId, bool isNested)
		{
			this.version = 3;
			this.sigType = sigType;
			this.hashAlgorithm = hashAlgorithm;
			this.keyAlgorithm = keyAlgorithm;
			this.keyId = keyId;
			this.nested = (isNested ? 0 : 1);
		}

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x0600153E RID: 5438 RVA: 0x000708AC File Offset: 0x000708AC
		public int SignatureType
		{
			get
			{
				return this.sigType;
			}
		}

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x0600153F RID: 5439 RVA: 0x000708B4 File Offset: 0x000708B4
		public PublicKeyAlgorithmTag KeyAlgorithm
		{
			get
			{
				return this.keyAlgorithm;
			}
		}

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x06001540 RID: 5440 RVA: 0x000708BC File Offset: 0x000708BC
		public HashAlgorithmTag HashAlgorithm
		{
			get
			{
				return this.hashAlgorithm;
			}
		}

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x06001541 RID: 5441 RVA: 0x000708C4 File Offset: 0x000708C4
		public long KeyId
		{
			get
			{
				return this.keyId;
			}
		}

		// Token: 0x06001542 RID: 5442 RVA: 0x000708CC File Offset: 0x000708CC
		public override void Encode(BcpgOutputStream bcpgOut)
		{
			MemoryStream memoryStream = new MemoryStream();
			BcpgOutputStream bcpgOutputStream = new BcpgOutputStream(memoryStream);
			bcpgOutputStream.Write(new byte[]
			{
				(byte)this.version,
				(byte)this.sigType,
				(byte)this.hashAlgorithm,
				(byte)this.keyAlgorithm
			});
			bcpgOutputStream.WriteLong(this.keyId);
			bcpgOutputStream.WriteByte((byte)this.nested);
			bcpgOut.WritePacket(PacketTag.OnePassSignature, memoryStream.ToArray(), true);
		}

		// Token: 0x04000E42 RID: 3650
		private int version;

		// Token: 0x04000E43 RID: 3651
		private int sigType;

		// Token: 0x04000E44 RID: 3652
		private HashAlgorithmTag hashAlgorithm;

		// Token: 0x04000E45 RID: 3653
		private PublicKeyAlgorithmTag keyAlgorithm;

		// Token: 0x04000E46 RID: 3654
		private long keyId;

		// Token: 0x04000E47 RID: 3655
		private int nested;
	}
}
