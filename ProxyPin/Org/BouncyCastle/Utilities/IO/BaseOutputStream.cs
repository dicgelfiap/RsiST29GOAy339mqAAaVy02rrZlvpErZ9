using System;
using System.IO;

namespace Org.BouncyCastle.Utilities.IO
{
	// Token: 0x02000254 RID: 596
	public abstract class BaseOutputStream : Stream
	{
		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x06001317 RID: 4887 RVA: 0x00069F18 File Offset: 0x00069F18
		public sealed override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x06001318 RID: 4888 RVA: 0x00069F1C File Offset: 0x00069F1C
		public sealed override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x06001319 RID: 4889 RVA: 0x00069F20 File Offset: 0x00069F20
		public sealed override bool CanWrite
		{
			get
			{
				return !this.closed;
			}
		}

		// Token: 0x0600131A RID: 4890 RVA: 0x00069F2C File Offset: 0x00069F2C
		public override void Close()
		{
			this.closed = true;
			base.Close();
		}

		// Token: 0x0600131B RID: 4891 RVA: 0x00069F3C File Offset: 0x00069F3C
		public override void Flush()
		{
		}

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x0600131C RID: 4892 RVA: 0x00069F40 File Offset: 0x00069F40
		public sealed override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x0600131D RID: 4893 RVA: 0x00069F48 File Offset: 0x00069F48
		// (set) Token: 0x0600131E RID: 4894 RVA: 0x00069F50 File Offset: 0x00069F50
		public sealed override long Position
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x0600131F RID: 4895 RVA: 0x00069F58 File Offset: 0x00069F58
		public sealed override int Read(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001320 RID: 4896 RVA: 0x00069F60 File Offset: 0x00069F60
		public sealed override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001321 RID: 4897 RVA: 0x00069F68 File Offset: 0x00069F68
		public sealed override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001322 RID: 4898 RVA: 0x00069F70 File Offset: 0x00069F70
		public override void Write(byte[] buffer, int offset, int count)
		{
			int num = offset + count;
			for (int i = offset; i < num; i++)
			{
				this.WriteByte(buffer[i]);
			}
		}

		// Token: 0x06001323 RID: 4899 RVA: 0x00069FA0 File Offset: 0x00069FA0
		public virtual void Write(params byte[] buffer)
		{
			this.Write(buffer, 0, buffer.Length);
		}

		// Token: 0x06001324 RID: 4900 RVA: 0x00069FB0 File Offset: 0x00069FB0
		public override void WriteByte(byte b)
		{
			this.Write(new byte[]
			{
				b
			}, 0, 1);
		}

		// Token: 0x04000D92 RID: 3474
		private bool closed;
	}
}
