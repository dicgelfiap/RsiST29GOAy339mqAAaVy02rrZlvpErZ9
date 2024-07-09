using System;
using System.IO;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Bcpg
{
	// Token: 0x02000280 RID: 640
	public class UserAttributeSubpacket
	{
		// Token: 0x06001445 RID: 5189 RVA: 0x0006D1B8 File Offset: 0x0006D1B8
		protected internal UserAttributeSubpacket(UserAttributeSubpacketTag type, byte[] data) : this(type, false, data)
		{
		}

		// Token: 0x06001446 RID: 5190 RVA: 0x0006D1C4 File Offset: 0x0006D1C4
		protected internal UserAttributeSubpacket(UserAttributeSubpacketTag type, bool forceLongLength, byte[] data)
		{
			this.type = type;
			this.longLength = forceLongLength;
			this.data = data;
		}

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x06001447 RID: 5191 RVA: 0x0006D1E4 File Offset: 0x0006D1E4
		public virtual UserAttributeSubpacketTag SubpacketType
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x06001448 RID: 5192 RVA: 0x0006D1EC File Offset: 0x0006D1EC
		public virtual byte[] GetData()
		{
			return this.data;
		}

		// Token: 0x06001449 RID: 5193 RVA: 0x0006D1F4 File Offset: 0x0006D1F4
		public virtual void Encode(Stream os)
		{
			int num = this.data.Length + 1;
			if (num < 192 && !this.longLength)
			{
				os.WriteByte((byte)num);
			}
			else if (num <= 8383 && !this.longLength)
			{
				num -= 192;
				os.WriteByte((byte)((num >> 8 & 255) + 192));
				os.WriteByte((byte)num);
			}
			else
			{
				os.WriteByte(byte.MaxValue);
				os.WriteByte((byte)(num >> 24));
				os.WriteByte((byte)(num >> 16));
				os.WriteByte((byte)(num >> 8));
				os.WriteByte((byte)num);
			}
			os.WriteByte((byte)this.type);
			os.Write(this.data, 0, this.data.Length);
		}

		// Token: 0x0600144A RID: 5194 RVA: 0x0006D2CC File Offset: 0x0006D2CC
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			UserAttributeSubpacket userAttributeSubpacket = obj as UserAttributeSubpacket;
			return userAttributeSubpacket != null && this.type == userAttributeSubpacket.type && Arrays.AreEqual(this.data, userAttributeSubpacket.data);
		}

		// Token: 0x0600144B RID: 5195 RVA: 0x0006D31C File Offset: 0x0006D31C
		public override int GetHashCode()
		{
			return this.type.GetHashCode() ^ Arrays.GetHashCode(this.data);
		}

		// Token: 0x04000DCA RID: 3530
		internal readonly UserAttributeSubpacketTag type;

		// Token: 0x04000DCB RID: 3531
		private readonly bool longLength;

		// Token: 0x04000DCC RID: 3532
		protected readonly byte[] data;
	}
}
