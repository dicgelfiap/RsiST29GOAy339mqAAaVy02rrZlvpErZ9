using System;
using System.IO;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.IO;

namespace Org.BouncyCastle.Bcpg
{
	// Token: 0x02000297 RID: 663
	public class BcpgInputStream : BaseInputStream
	{
		// Token: 0x060014BF RID: 5311 RVA: 0x0006F154 File Offset: 0x0006F154
		internal static BcpgInputStream Wrap(Stream inStr)
		{
			if (inStr is BcpgInputStream)
			{
				return (BcpgInputStream)inStr;
			}
			return new BcpgInputStream(inStr);
		}

		// Token: 0x060014C0 RID: 5312 RVA: 0x0006F170 File Offset: 0x0006F170
		private BcpgInputStream(Stream inputStream)
		{
			this.m_in = inputStream;
		}

		// Token: 0x060014C1 RID: 5313 RVA: 0x0006F188 File Offset: 0x0006F188
		public override int ReadByte()
		{
			if (this.next)
			{
				this.next = false;
				return this.nextB;
			}
			return this.m_in.ReadByte();
		}

		// Token: 0x060014C2 RID: 5314 RVA: 0x0006F1B0 File Offset: 0x0006F1B0
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (!this.next)
			{
				return this.m_in.Read(buffer, offset, count);
			}
			if (this.nextB < 0)
			{
				return 0;
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			buffer[offset] = (byte)this.nextB;
			this.next = false;
			return 1;
		}

		// Token: 0x060014C3 RID: 5315 RVA: 0x0006F20C File Offset: 0x0006F20C
		public byte[] ReadAll()
		{
			return Streams.ReadAll(this);
		}

		// Token: 0x060014C4 RID: 5316 RVA: 0x0006F214 File Offset: 0x0006F214
		public void ReadFully(byte[] buffer, int off, int len)
		{
			if (Streams.ReadFully(this, buffer, off, len) < len)
			{
				throw new EndOfStreamException();
			}
		}

		// Token: 0x060014C5 RID: 5317 RVA: 0x0006F22C File Offset: 0x0006F22C
		public void ReadFully(byte[] buffer)
		{
			this.ReadFully(buffer, 0, buffer.Length);
		}

		// Token: 0x060014C6 RID: 5318 RVA: 0x0006F23C File Offset: 0x0006F23C
		public PacketTag NextPacketTag()
		{
			if (!this.next)
			{
				try
				{
					this.nextB = this.m_in.ReadByte();
				}
				catch (EndOfStreamException)
				{
					this.nextB = -1;
				}
				this.next = true;
			}
			if (this.nextB < 0)
			{
				return (PacketTag)this.nextB;
			}
			int num = this.nextB & 63;
			if ((this.nextB & 64) == 0)
			{
				num >>= 2;
			}
			return (PacketTag)num;
		}

		// Token: 0x060014C7 RID: 5319 RVA: 0x0006F2C0 File Offset: 0x0006F2C0
		public Packet ReadPacket()
		{
			int num = this.ReadByte();
			if (num < 0)
			{
				return null;
			}
			if ((num & 128) == 0)
			{
				throw new IOException("invalid header encountered");
			}
			bool flag = (num & 64) != 0;
			int num2 = 0;
			bool flag2 = false;
			PacketTag packetTag;
			if (flag)
			{
				packetTag = (PacketTag)(num & 63);
				int num3 = this.ReadByte();
				if (num3 < 192)
				{
					num2 = num3;
				}
				else if (num3 <= 223)
				{
					int num4 = this.m_in.ReadByte();
					num2 = (num3 - 192 << 8) + num4 + 192;
				}
				else if (num3 == 255)
				{
					num2 = (this.m_in.ReadByte() << 24 | this.m_in.ReadByte() << 16 | this.m_in.ReadByte() << 8 | this.m_in.ReadByte());
				}
				else
				{
					flag2 = true;
					num2 = 1 << num3;
				}
			}
			else
			{
				int num5 = num & 3;
				packetTag = (PacketTag)((num & 63) >> 2);
				switch (num5)
				{
				case 0:
					num2 = this.ReadByte();
					break;
				case 1:
					num2 = (this.ReadByte() << 8 | this.ReadByte());
					break;
				case 2:
					num2 = (this.ReadByte() << 24 | this.ReadByte() << 16 | this.ReadByte() << 8 | this.ReadByte());
					break;
				case 3:
					flag2 = true;
					break;
				default:
					throw new IOException("unknown length type encountered");
				}
			}
			BcpgInputStream bcpgIn;
			if (num2 == 0 && flag2)
			{
				bcpgIn = this;
			}
			else
			{
				BcpgInputStream.PartialInputStream inputStream = new BcpgInputStream.PartialInputStream(this, flag2, num2);
				bcpgIn = new BcpgInputStream(inputStream);
			}
			PacketTag packetTag2 = packetTag;
			switch (packetTag2)
			{
			case PacketTag.Reserved:
				return new InputStreamPacket(bcpgIn);
			case PacketTag.PublicKeyEncryptedSession:
				return new PublicKeyEncSessionPacket(bcpgIn);
			case PacketTag.Signature:
				return new SignaturePacket(bcpgIn);
			case PacketTag.SymmetricKeyEncryptedSessionKey:
				return new SymmetricKeyEncSessionPacket(bcpgIn);
			case PacketTag.OnePassSignature:
				return new OnePassSignaturePacket(bcpgIn);
			case PacketTag.SecretKey:
				return new SecretKeyPacket(bcpgIn);
			case PacketTag.PublicKey:
				return new PublicKeyPacket(bcpgIn);
			case PacketTag.SecretSubkey:
				return new SecretSubkeyPacket(bcpgIn);
			case PacketTag.CompressedData:
				return new CompressedDataPacket(bcpgIn);
			case PacketTag.SymmetricKeyEncrypted:
				return new SymmetricEncDataPacket(bcpgIn);
			case PacketTag.Marker:
				return new MarkerPacket(bcpgIn);
			case PacketTag.LiteralData:
				return new LiteralDataPacket(bcpgIn);
			case PacketTag.Trust:
				return new TrustPacket(bcpgIn);
			case PacketTag.UserId:
				return new UserIdPacket(bcpgIn);
			case PacketTag.PublicSubkey:
				return new PublicSubkeyPacket(bcpgIn);
			case (PacketTag)15:
			case (PacketTag)16:
				break;
			case PacketTag.UserAttribute:
				return new UserAttributePacket(bcpgIn);
			case PacketTag.SymmetricEncryptedIntegrityProtected:
				return new SymmetricEncIntegrityPacket(bcpgIn);
			case PacketTag.ModificationDetectionCode:
				return new ModDetectionCodePacket(bcpgIn);
			default:
				switch (packetTag2)
				{
				case PacketTag.Experimental1:
				case PacketTag.Experimental2:
				case PacketTag.Experimental3:
				case PacketTag.Experimental4:
					return new ExperimentalPacket(packetTag, bcpgIn);
				}
				break;
			}
			throw new IOException("unknown packet type encountered: " + packetTag);
		}

		// Token: 0x060014C8 RID: 5320 RVA: 0x0006F598 File Offset: 0x0006F598
		public override void Close()
		{
			Platform.Dispose(this.m_in);
			base.Close();
		}

		// Token: 0x04000E0C RID: 3596
		private Stream m_in;

		// Token: 0x04000E0D RID: 3597
		private bool next = false;

		// Token: 0x04000E0E RID: 3598
		private int nextB;

		// Token: 0x02000DD8 RID: 3544
		private class PartialInputStream : BaseInputStream
		{
			// Token: 0x06008B64 RID: 35684 RVA: 0x0029D738 File Offset: 0x0029D738
			internal PartialInputStream(BcpgInputStream bcpgIn, bool partial, int dataLength)
			{
				this.m_in = bcpgIn;
				this.partial = partial;
				this.dataLength = dataLength;
			}

			// Token: 0x06008B65 RID: 35685 RVA: 0x0029D758 File Offset: 0x0029D758
			public override int ReadByte()
			{
				while (this.dataLength == 0)
				{
					if (!this.partial || this.ReadPartialDataLength() < 0)
					{
						return -1;
					}
				}
				int num = this.m_in.ReadByte();
				if (num < 0)
				{
					throw new EndOfStreamException("Premature end of stream in PartialInputStream");
				}
				this.dataLength--;
				return num;
			}

			// Token: 0x06008B66 RID: 35686 RVA: 0x0029D7B8 File Offset: 0x0029D7B8
			public override int Read(byte[] buffer, int offset, int count)
			{
				while (this.dataLength == 0)
				{
					if (!this.partial || this.ReadPartialDataLength() < 0)
					{
						return 0;
					}
				}
				int count2 = (this.dataLength > count || this.dataLength < 0) ? count : this.dataLength;
				int num = this.m_in.Read(buffer, offset, count2);
				if (num < 1)
				{
					throw new EndOfStreamException("Premature end of stream in PartialInputStream");
				}
				this.dataLength -= num;
				return num;
			}

			// Token: 0x06008B67 RID: 35687 RVA: 0x0029D840 File Offset: 0x0029D840
			private int ReadPartialDataLength()
			{
				int num = this.m_in.ReadByte();
				if (num < 0)
				{
					return -1;
				}
				this.partial = false;
				if (num < 192)
				{
					this.dataLength = num;
				}
				else if (num <= 223)
				{
					this.dataLength = (num - 192 << 8) + this.m_in.ReadByte() + 192;
				}
				else if (num == 255)
				{
					this.dataLength = (this.m_in.ReadByte() << 24 | this.m_in.ReadByte() << 16 | this.m_in.ReadByte() << 8 | this.m_in.ReadByte());
				}
				else
				{
					this.partial = true;
					this.dataLength = 1 << num;
				}
				return 0;
			}

			// Token: 0x04004068 RID: 16488
			private BcpgInputStream m_in;

			// Token: 0x04004069 RID: 16489
			private bool partial;

			// Token: 0x0400406A RID: 16490
			private int dataLength;
		}
	}
}
