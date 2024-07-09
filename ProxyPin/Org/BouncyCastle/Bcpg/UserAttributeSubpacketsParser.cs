using System;
using System.IO;
using Org.BouncyCastle.Bcpg.Attr;
using Org.BouncyCastle.Utilities.IO;

namespace Org.BouncyCastle.Bcpg
{
	// Token: 0x020002C4 RID: 708
	public class UserAttributeSubpacketsParser
	{
		// Token: 0x060015AD RID: 5549 RVA: 0x000725F0 File Offset: 0x000725F0
		public UserAttributeSubpacketsParser(Stream input)
		{
			this.input = input;
		}

		// Token: 0x060015AE RID: 5550 RVA: 0x00072600 File Offset: 0x00072600
		public virtual UserAttributeSubpacket ReadPacket()
		{
			int num = this.input.ReadByte();
			if (num < 0)
			{
				return null;
			}
			bool forceLongLength = false;
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
					throw new IOException("unrecognised length reading user attribute sub packet");
				}
				num2 = (this.input.ReadByte() << 24 | this.input.ReadByte() << 16 | this.input.ReadByte() << 8 | this.input.ReadByte());
				forceLongLength = true;
			}
			int num3 = this.input.ReadByte();
			if (num3 < 0)
			{
				throw new EndOfStreamException("unexpected EOF reading user attribute sub packet");
			}
			byte[] array = new byte[num2 - 1];
			if (Streams.ReadFully(this.input, array) < array.Length)
			{
				throw new EndOfStreamException();
			}
			UserAttributeSubpacketTag userAttributeSubpacketTag = (UserAttributeSubpacketTag)num3;
			UserAttributeSubpacketTag userAttributeSubpacketTag2 = userAttributeSubpacketTag;
			if (userAttributeSubpacketTag2 == UserAttributeSubpacketTag.ImageAttribute)
			{
				return new ImageAttrib(forceLongLength, array);
			}
			return new UserAttributeSubpacket(userAttributeSubpacketTag, forceLongLength, array);
		}

		// Token: 0x04000ED8 RID: 3800
		private readonly Stream input;
	}
}
