using System;
using System.IO;

namespace Org.BouncyCastle.Bcpg
{
	// Token: 0x02000282 RID: 642
	public class SignatureSubpacket
	{
		// Token: 0x06001454 RID: 5204 RVA: 0x0006D45C File Offset: 0x0006D45C
		protected internal SignatureSubpacket(SignatureSubpacketTag type, bool critical, bool isLongLength, byte[] data)
		{
			this.type = type;
			this.critical = critical;
			this.isLongLength = isLongLength;
			this.data = data;
		}

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x06001455 RID: 5205 RVA: 0x0006D484 File Offset: 0x0006D484
		public SignatureSubpacketTag SubpacketType
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x06001456 RID: 5206 RVA: 0x0006D48C File Offset: 0x0006D48C
		public bool IsCritical()
		{
			return this.critical;
		}

		// Token: 0x06001457 RID: 5207 RVA: 0x0006D494 File Offset: 0x0006D494
		public bool IsLongLength()
		{
			return this.isLongLength;
		}

		// Token: 0x06001458 RID: 5208 RVA: 0x0006D49C File Offset: 0x0006D49C
		public byte[] GetData()
		{
			return (byte[])this.data.Clone();
		}

		// Token: 0x06001459 RID: 5209 RVA: 0x0006D4B0 File Offset: 0x0006D4B0
		public void Encode(Stream os)
		{
			int num = this.data.Length + 1;
			if (this.isLongLength)
			{
				os.WriteByte(byte.MaxValue);
				os.WriteByte((byte)(num >> 24));
				os.WriteByte((byte)(num >> 16));
				os.WriteByte((byte)(num >> 8));
				os.WriteByte((byte)num);
			}
			else if (num < 192)
			{
				os.WriteByte((byte)num);
			}
			else if (num <= 8383)
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
			if (this.critical)
			{
				os.WriteByte((byte)((SignatureSubpacketTag)128 | this.type));
			}
			else
			{
				os.WriteByte((byte)this.type);
			}
			os.Write(this.data, 0, this.data.Length);
		}

		// Token: 0x04000DD2 RID: 3538
		private readonly SignatureSubpacketTag type;

		// Token: 0x04000DD3 RID: 3539
		private readonly bool critical;

		// Token: 0x04000DD4 RID: 3540
		private readonly bool isLongLength;

		// Token: 0x04000DD5 RID: 3541
		internal byte[] data;
	}
}
