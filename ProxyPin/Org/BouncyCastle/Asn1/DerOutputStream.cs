using System;
using System.IO;
using Org.BouncyCastle.Utilities.IO;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x0200023E RID: 574
	public class DerOutputStream : FilterStream
	{
		// Token: 0x0600128B RID: 4747 RVA: 0x000684C0 File Offset: 0x000684C0
		public DerOutputStream(Stream os) : base(os)
		{
		}

		// Token: 0x0600128C RID: 4748 RVA: 0x000684CC File Offset: 0x000684CC
		private void WriteLength(int length)
		{
			if (length > 127)
			{
				int num = 1;
				uint num2 = (uint)length;
				while ((num2 >>= 8) != 0U)
				{
					num++;
				}
				this.WriteByte((byte)(num | 128));
				for (int i = (num - 1) * 8; i >= 0; i -= 8)
				{
					this.WriteByte((byte)(length >> i));
				}
				return;
			}
			this.WriteByte((byte)length);
		}

		// Token: 0x0600128D RID: 4749 RVA: 0x00068530 File Offset: 0x00068530
		internal void WriteEncoded(int tag, byte[] bytes)
		{
			this.WriteByte((byte)tag);
			this.WriteLength(bytes.Length);
			this.Write(bytes, 0, bytes.Length);
		}

		// Token: 0x0600128E RID: 4750 RVA: 0x00068560 File Offset: 0x00068560
		internal void WriteEncoded(int tag, byte first, byte[] bytes)
		{
			this.WriteByte((byte)tag);
			this.WriteLength(bytes.Length + 1);
			this.WriteByte(first);
			this.Write(bytes, 0, bytes.Length);
		}

		// Token: 0x0600128F RID: 4751 RVA: 0x00068598 File Offset: 0x00068598
		internal void WriteEncoded(int tag, byte[] bytes, int offset, int length)
		{
			this.WriteByte((byte)tag);
			this.WriteLength(length);
			this.Write(bytes, offset, length);
		}

		// Token: 0x06001290 RID: 4752 RVA: 0x000685C4 File Offset: 0x000685C4
		internal void WriteTag(int flags, int tagNo)
		{
			if (tagNo < 31)
			{
				this.WriteByte((byte)(flags | tagNo));
				return;
			}
			this.WriteByte((byte)(flags | 31));
			if (tagNo < 128)
			{
				this.WriteByte((byte)tagNo);
				return;
			}
			byte[] array = new byte[5];
			int num = array.Length;
			array[--num] = (byte)(tagNo & 127);
			do
			{
				tagNo >>= 7;
				array[--num] = (byte)((tagNo & 127) | 128);
			}
			while (tagNo > 127);
			this.Write(array, num, array.Length - num);
		}

		// Token: 0x06001291 RID: 4753 RVA: 0x00068648 File Offset: 0x00068648
		internal void WriteEncoded(int flags, int tagNo, byte[] bytes)
		{
			this.WriteTag(flags, tagNo);
			this.WriteLength(bytes.Length);
			this.Write(bytes, 0, bytes.Length);
		}

		// Token: 0x06001292 RID: 4754 RVA: 0x00068678 File Offset: 0x00068678
		protected void WriteNull()
		{
			this.WriteByte(5);
			this.WriteByte(0);
		}

		// Token: 0x06001293 RID: 4755 RVA: 0x00068688 File Offset: 0x00068688
		[Obsolete("Use version taking an Asn1Encodable arg instead")]
		public virtual void WriteObject(object obj)
		{
			if (obj == null)
			{
				this.WriteNull();
				return;
			}
			if (obj is Asn1Object)
			{
				((Asn1Object)obj).Encode(this);
				return;
			}
			if (obj is Asn1Encodable)
			{
				((Asn1Encodable)obj).ToAsn1Object().Encode(this);
				return;
			}
			throw new IOException("object not Asn1Object");
		}

		// Token: 0x06001294 RID: 4756 RVA: 0x000686E8 File Offset: 0x000686E8
		public virtual void WriteObject(Asn1Encodable obj)
		{
			if (obj == null)
			{
				this.WriteNull();
				return;
			}
			obj.ToAsn1Object().Encode(this);
		}

		// Token: 0x06001295 RID: 4757 RVA: 0x00068704 File Offset: 0x00068704
		public virtual void WriteObject(Asn1Object obj)
		{
			if (obj == null)
			{
				this.WriteNull();
				return;
			}
			obj.Encode(this);
		}
	}
}
