using System;
using System.IO;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.IO;

namespace Org.BouncyCastle.Bcpg
{
	// Token: 0x020002B3 RID: 691
	public class PublicKeyEncSessionPacket : ContainedPacket
	{
		// Token: 0x06001546 RID: 5446 RVA: 0x00070968 File Offset: 0x00070968
		internal PublicKeyEncSessionPacket(BcpgInputStream bcpgIn)
		{
			this.version = bcpgIn.ReadByte();
			this.keyId |= (long)bcpgIn.ReadByte() << 56;
			this.keyId |= (long)bcpgIn.ReadByte() << 48;
			this.keyId |= (long)bcpgIn.ReadByte() << 40;
			this.keyId |= (long)bcpgIn.ReadByte() << 32;
			this.keyId |= (long)bcpgIn.ReadByte() << 24;
			this.keyId |= (long)bcpgIn.ReadByte() << 16;
			this.keyId |= (long)bcpgIn.ReadByte() << 8;
			this.keyId |= (long)((ulong)bcpgIn.ReadByte());
			this.algorithm = (PublicKeyAlgorithmTag)bcpgIn.ReadByte();
			PublicKeyAlgorithmTag publicKeyAlgorithmTag = this.algorithm;
			switch (publicKeyAlgorithmTag)
			{
			case PublicKeyAlgorithmTag.RsaGeneral:
			case PublicKeyAlgorithmTag.RsaEncrypt:
				this.data = new byte[][]
				{
					new MPInteger(bcpgIn).GetEncoded()
				};
				return;
			default:
				switch (publicKeyAlgorithmTag)
				{
				case PublicKeyAlgorithmTag.ElGamalEncrypt:
				case PublicKeyAlgorithmTag.ElGamalGeneral:
				{
					MPInteger mpinteger = new MPInteger(bcpgIn);
					MPInteger mpinteger2 = new MPInteger(bcpgIn);
					this.data = new byte[][]
					{
						mpinteger.GetEncoded(),
						mpinteger2.GetEncoded()
					};
					return;
				}
				case PublicKeyAlgorithmTag.EC:
					this.data = new byte[][]
					{
						Streams.ReadAll(bcpgIn)
					};
					return;
				}
				throw new IOException("unknown PGP public key algorithm encountered");
			}
		}

		// Token: 0x06001547 RID: 5447 RVA: 0x00070B04 File Offset: 0x00070B04
		public PublicKeyEncSessionPacket(long keyId, PublicKeyAlgorithmTag algorithm, byte[][] data)
		{
			this.version = 3;
			this.keyId = keyId;
			this.algorithm = algorithm;
			this.data = new byte[data.Length][];
			for (int i = 0; i < data.Length; i++)
			{
				this.data[i] = Arrays.Clone(data[i]);
			}
		}

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x06001548 RID: 5448 RVA: 0x00070B68 File Offset: 0x00070B68
		public int Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x06001549 RID: 5449 RVA: 0x00070B70 File Offset: 0x00070B70
		public long KeyId
		{
			get
			{
				return this.keyId;
			}
		}

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x0600154A RID: 5450 RVA: 0x00070B78 File Offset: 0x00070B78
		public PublicKeyAlgorithmTag Algorithm
		{
			get
			{
				return this.algorithm;
			}
		}

		// Token: 0x0600154B RID: 5451 RVA: 0x00070B80 File Offset: 0x00070B80
		public byte[][] GetEncSessionKey()
		{
			return this.data;
		}

		// Token: 0x0600154C RID: 5452 RVA: 0x00070B88 File Offset: 0x00070B88
		public override void Encode(BcpgOutputStream bcpgOut)
		{
			MemoryStream memoryStream = new MemoryStream();
			BcpgOutputStream bcpgOutputStream = new BcpgOutputStream(memoryStream);
			bcpgOutputStream.WriteByte((byte)this.version);
			bcpgOutputStream.WriteLong(this.keyId);
			bcpgOutputStream.WriteByte((byte)this.algorithm);
			for (int i = 0; i < this.data.Length; i++)
			{
				bcpgOutputStream.Write(this.data[i]);
			}
			Platform.Dispose(bcpgOutputStream);
			bcpgOut.WritePacket(PacketTag.PublicKeyEncryptedSession, memoryStream.ToArray(), true);
		}

		// Token: 0x04000E76 RID: 3702
		private int version;

		// Token: 0x04000E77 RID: 3703
		private long keyId;

		// Token: 0x04000E78 RID: 3704
		private PublicKeyAlgorithmTag algorithm;

		// Token: 0x04000E79 RID: 3705
		private byte[][] data;
	}
}
