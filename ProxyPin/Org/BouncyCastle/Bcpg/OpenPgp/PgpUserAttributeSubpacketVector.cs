using System;
using Org.BouncyCastle.Bcpg.Attr;

namespace Org.BouncyCastle.Bcpg.OpenPgp
{
	// Token: 0x02000669 RID: 1641
	public class PgpUserAttributeSubpacketVector
	{
		// Token: 0x06003988 RID: 14728 RVA: 0x00134C54 File Offset: 0x00134C54
		internal PgpUserAttributeSubpacketVector(UserAttributeSubpacket[] packets)
		{
			this.packets = packets;
		}

		// Token: 0x06003989 RID: 14729 RVA: 0x00134C64 File Offset: 0x00134C64
		public UserAttributeSubpacket GetSubpacket(UserAttributeSubpacketTag type)
		{
			for (int num = 0; num != this.packets.Length; num++)
			{
				if (this.packets[num].SubpacketType == type)
				{
					return this.packets[num];
				}
			}
			return null;
		}

		// Token: 0x0600398A RID: 14730 RVA: 0x00134CB0 File Offset: 0x00134CB0
		public ImageAttrib GetImageAttribute()
		{
			UserAttributeSubpacket subpacket = this.GetSubpacket(UserAttributeSubpacketTag.ImageAttribute);
			if (subpacket != null)
			{
				return (ImageAttrib)subpacket;
			}
			return null;
		}

		// Token: 0x0600398B RID: 14731 RVA: 0x00134CD8 File Offset: 0x00134CD8
		internal UserAttributeSubpacket[] ToSubpacketArray()
		{
			return this.packets;
		}

		// Token: 0x0600398C RID: 14732 RVA: 0x00134CE0 File Offset: 0x00134CE0
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			PgpUserAttributeSubpacketVector pgpUserAttributeSubpacketVector = obj as PgpUserAttributeSubpacketVector;
			if (pgpUserAttributeSubpacketVector == null)
			{
				return false;
			}
			if (pgpUserAttributeSubpacketVector.packets.Length != this.packets.Length)
			{
				return false;
			}
			for (int num = 0; num != this.packets.Length; num++)
			{
				if (!pgpUserAttributeSubpacketVector.packets[num].Equals(this.packets[num]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600398D RID: 14733 RVA: 0x00134D5C File Offset: 0x00134D5C
		public override int GetHashCode()
		{
			int num = 0;
			foreach (UserAttributeSubpacket obj in this.packets)
			{
				num ^= obj.GetHashCode();
			}
			return num;
		}

		// Token: 0x04001E0C RID: 7692
		private readonly UserAttributeSubpacket[] packets;
	}
}
