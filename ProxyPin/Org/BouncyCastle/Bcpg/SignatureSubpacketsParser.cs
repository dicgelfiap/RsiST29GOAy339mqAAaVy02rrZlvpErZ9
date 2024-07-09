using System;
using System.IO;
using Org.BouncyCastle.Bcpg.Sig;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.IO;

namespace Org.BouncyCastle.Bcpg
{
	// Token: 0x020002BC RID: 700
	public class SignatureSubpacketsParser
	{
		// Token: 0x06001599 RID: 5529 RVA: 0x00072040 File Offset: 0x00072040
		public SignatureSubpacketsParser(Stream input)
		{
			this.input = input;
		}

		// Token: 0x0600159A RID: 5530 RVA: 0x00072050 File Offset: 0x00072050
		public SignatureSubpacket ReadPacket()
		{
			int num = this.input.ReadByte();
			if (num < 0)
			{
				return null;
			}
			bool isLongLength = false;
			int num2;
			if (num < 192)
			{
				num2 = num;
			}
			else if (num <= 223)
			{
				num2 = (num - 192 << 8) + this.input.ReadByte() + 192;
			}
			else
			{
				if (num != 255)
				{
					throw new IOException("unexpected length header");
				}
				isLongLength = true;
				num2 = (this.input.ReadByte() << 24 | this.input.ReadByte() << 16 | this.input.ReadByte() << 8 | this.input.ReadByte());
			}
			int num3 = this.input.ReadByte();
			if (num3 < 0)
			{
				throw new EndOfStreamException("unexpected EOF reading signature sub packet");
			}
			if (num2 <= 0)
			{
				throw new EndOfStreamException("out of range data found in signature sub packet");
			}
			byte[] array = new byte[num2 - 1];
			int num4 = Streams.ReadFully(this.input, array);
			bool critical = (num3 & 128) != 0;
			SignatureSubpacketTag signatureSubpacketTag = (SignatureSubpacketTag)(num3 & 127);
			if (num4 != array.Length)
			{
				SignatureSubpacketTag signatureSubpacketTag2 = signatureSubpacketTag;
				switch (signatureSubpacketTag2)
				{
				case SignatureSubpacketTag.CreationTime:
					array = this.CheckData(array, 4, num4, "Signature Creation Time");
					break;
				case SignatureSubpacketTag.ExpireTime:
					array = this.CheckData(array, 4, num4, "Signature Expiration Time");
					break;
				default:
					if (signatureSubpacketTag2 != SignatureSubpacketTag.KeyExpireTime)
					{
						if (signatureSubpacketTag2 != SignatureSubpacketTag.IssuerKeyId)
						{
							throw new EndOfStreamException("truncated subpacket data.");
						}
						array = this.CheckData(array, 8, num4, "Issuer");
					}
					else
					{
						array = this.CheckData(array, 4, num4, "Signature Key Expiration Time");
					}
					break;
				}
			}
			switch (signatureSubpacketTag)
			{
			case SignatureSubpacketTag.CreationTime:
				return new SignatureCreationTime(critical, isLongLength, array);
			case SignatureSubpacketTag.ExpireTime:
				return new SignatureExpirationTime(critical, isLongLength, array);
			case SignatureSubpacketTag.Exportable:
				return new Exportable(critical, isLongLength, array);
			case SignatureSubpacketTag.TrustSig:
				return new TrustSignature(critical, isLongLength, array);
			case SignatureSubpacketTag.Revocable:
				return new Revocable(critical, isLongLength, array);
			case SignatureSubpacketTag.KeyExpireTime:
				return new KeyExpirationTime(critical, isLongLength, array);
			case SignatureSubpacketTag.PreferredSymmetricAlgorithms:
			case SignatureSubpacketTag.PreferredHashAlgorithms:
			case SignatureSubpacketTag.PreferredCompressionAlgorithms:
				return new PreferredAlgorithms(signatureSubpacketTag, critical, isLongLength, array);
			case SignatureSubpacketTag.IssuerKeyId:
				return new IssuerKeyId(critical, isLongLength, array);
			case SignatureSubpacketTag.NotationData:
				return new NotationData(critical, isLongLength, array);
			case SignatureSubpacketTag.PrimaryUserId:
				return new PrimaryUserId(critical, isLongLength, array);
			case SignatureSubpacketTag.KeyFlags:
				return new KeyFlags(critical, isLongLength, array);
			case SignatureSubpacketTag.SignerUserId:
				return new SignerUserId(critical, isLongLength, array);
			}
			return new SignatureSubpacket(signatureSubpacketTag, critical, isLongLength, array);
		}

		// Token: 0x0600159B RID: 5531 RVA: 0x0007231C File Offset: 0x0007231C
		private byte[] CheckData(byte[] data, int expected, int bytesRead, string name)
		{
			if (bytesRead != expected)
			{
				throw new EndOfStreamException("truncated " + name + " subpacket data.");
			}
			return Arrays.CopyOfRange(data, 0, expected);
		}

		// Token: 0x04000EA8 RID: 3752
		private readonly Stream input;
	}
}
