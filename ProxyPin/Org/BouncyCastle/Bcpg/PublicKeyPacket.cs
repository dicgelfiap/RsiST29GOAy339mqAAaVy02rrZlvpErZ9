using System;
using System.IO;
using Org.BouncyCastle.Utilities.Date;

namespace Org.BouncyCastle.Bcpg
{
	// Token: 0x020002B4 RID: 692
	public class PublicKeyPacket : ContainedPacket
	{
		// Token: 0x0600154D RID: 5453 RVA: 0x00070C08 File Offset: 0x00070C08
		internal PublicKeyPacket(BcpgInputStream bcpgIn)
		{
			this.version = bcpgIn.ReadByte();
			this.time = (long)((ulong)(bcpgIn.ReadByte() << 24 | bcpgIn.ReadByte() << 16 | bcpgIn.ReadByte() << 8 | bcpgIn.ReadByte()));
			if (this.version <= 3)
			{
				this.validDays = (bcpgIn.ReadByte() << 8 | bcpgIn.ReadByte());
			}
			this.algorithm = (PublicKeyAlgorithmTag)bcpgIn.ReadByte();
			PublicKeyAlgorithmTag publicKeyAlgorithmTag = this.algorithm;
			switch (publicKeyAlgorithmTag)
			{
			case PublicKeyAlgorithmTag.RsaGeneral:
			case PublicKeyAlgorithmTag.RsaEncrypt:
			case PublicKeyAlgorithmTag.RsaSign:
				this.key = new RsaPublicBcpgKey(bcpgIn);
				return;
			default:
				switch (publicKeyAlgorithmTag)
				{
				case PublicKeyAlgorithmTag.ElGamalEncrypt:
				case PublicKeyAlgorithmTag.ElGamalGeneral:
					this.key = new ElGamalPublicBcpgKey(bcpgIn);
					return;
				case PublicKeyAlgorithmTag.Dsa:
					this.key = new DsaPublicBcpgKey(bcpgIn);
					return;
				case PublicKeyAlgorithmTag.EC:
					this.key = new ECDHPublicBcpgKey(bcpgIn);
					return;
				case PublicKeyAlgorithmTag.ECDsa:
					this.key = new ECDsaPublicBcpgKey(bcpgIn);
					return;
				default:
					throw new IOException("unknown PGP public key algorithm encountered");
				}
				break;
			}
		}

		// Token: 0x0600154E RID: 5454 RVA: 0x00070D0C File Offset: 0x00070D0C
		public PublicKeyPacket(PublicKeyAlgorithmTag algorithm, DateTime time, IBcpgKey key)
		{
			this.version = 4;
			this.time = DateTimeUtilities.DateTimeToUnixMs(time) / 1000L;
			this.algorithm = algorithm;
			this.key = key;
		}

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x0600154F RID: 5455 RVA: 0x00070D3C File Offset: 0x00070D3C
		public virtual int Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x06001550 RID: 5456 RVA: 0x00070D44 File Offset: 0x00070D44
		public virtual PublicKeyAlgorithmTag Algorithm
		{
			get
			{
				return this.algorithm;
			}
		}

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x06001551 RID: 5457 RVA: 0x00070D4C File Offset: 0x00070D4C
		public virtual int ValidDays
		{
			get
			{
				return this.validDays;
			}
		}

		// Token: 0x06001552 RID: 5458 RVA: 0x00070D54 File Offset: 0x00070D54
		public virtual DateTime GetTime()
		{
			return DateTimeUtilities.UnixMsToDateTime(this.time * 1000L);
		}

		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x06001553 RID: 5459 RVA: 0x00070D68 File Offset: 0x00070D68
		public virtual IBcpgKey Key
		{
			get
			{
				return this.key;
			}
		}

		// Token: 0x06001554 RID: 5460 RVA: 0x00070D70 File Offset: 0x00070D70
		public virtual byte[] GetEncodedContents()
		{
			MemoryStream memoryStream = new MemoryStream();
			BcpgOutputStream bcpgOutputStream = new BcpgOutputStream(memoryStream);
			bcpgOutputStream.WriteByte((byte)this.version);
			bcpgOutputStream.WriteInt((int)this.time);
			if (this.version <= 3)
			{
				bcpgOutputStream.WriteShort((short)this.validDays);
			}
			bcpgOutputStream.WriteByte((byte)this.algorithm);
			bcpgOutputStream.WriteObject((BcpgObject)this.key);
			return memoryStream.ToArray();
		}

		// Token: 0x06001555 RID: 5461 RVA: 0x00070DE8 File Offset: 0x00070DE8
		public override void Encode(BcpgOutputStream bcpgOut)
		{
			bcpgOut.WritePacket(PacketTag.PublicKey, this.GetEncodedContents(), true);
		}

		// Token: 0x04000E7A RID: 3706
		private int version;

		// Token: 0x04000E7B RID: 3707
		private long time;

		// Token: 0x04000E7C RID: 3708
		private int validDays;

		// Token: 0x04000E7D RID: 3709
		private PublicKeyAlgorithmTag algorithm;

		// Token: 0x04000E7E RID: 3710
		private IBcpgKey key;
	}
}
