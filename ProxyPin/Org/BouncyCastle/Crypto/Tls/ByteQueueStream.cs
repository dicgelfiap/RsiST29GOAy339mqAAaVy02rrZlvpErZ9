using System;
using System.IO;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x020004CF RID: 1231
	public class ByteQueueStream : Stream
	{
		// Token: 0x06002609 RID: 9737 RVA: 0x000CFD14 File Offset: 0x000CFD14
		public ByteQueueStream()
		{
			this.buffer = new ByteQueue();
		}

		// Token: 0x1700072B RID: 1835
		// (get) Token: 0x0600260A RID: 9738 RVA: 0x000CFD28 File Offset: 0x000CFD28
		public virtual int Available
		{
			get
			{
				return this.buffer.Available;
			}
		}

		// Token: 0x1700072C RID: 1836
		// (get) Token: 0x0600260B RID: 9739 RVA: 0x000CFD38 File Offset: 0x000CFD38
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700072D RID: 1837
		// (get) Token: 0x0600260C RID: 9740 RVA: 0x000CFD3C File Offset: 0x000CFD3C
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700072E RID: 1838
		// (get) Token: 0x0600260D RID: 9741 RVA: 0x000CFD40 File Offset: 0x000CFD40
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600260E RID: 9742 RVA: 0x000CFD44 File Offset: 0x000CFD44
		public override void Flush()
		{
		}

		// Token: 0x1700072F RID: 1839
		// (get) Token: 0x0600260F RID: 9743 RVA: 0x000CFD48 File Offset: 0x000CFD48
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06002610 RID: 9744 RVA: 0x000CFD50 File Offset: 0x000CFD50
		public virtual int Peek(byte[] buf)
		{
			int num = Math.Min(this.buffer.Available, buf.Length);
			this.buffer.Read(buf, 0, num, 0);
			return num;
		}

		// Token: 0x17000730 RID: 1840
		// (get) Token: 0x06002611 RID: 9745 RVA: 0x000CFD88 File Offset: 0x000CFD88
		// (set) Token: 0x06002612 RID: 9746 RVA: 0x000CFD90 File Offset: 0x000CFD90
		public override long Position
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

		// Token: 0x06002613 RID: 9747 RVA: 0x000CFD98 File Offset: 0x000CFD98
		public virtual int Read(byte[] buf)
		{
			return this.Read(buf, 0, buf.Length);
		}

		// Token: 0x06002614 RID: 9748 RVA: 0x000CFDA8 File Offset: 0x000CFDA8
		public override int Read(byte[] buf, int off, int len)
		{
			int num = Math.Min(this.buffer.Available, len);
			this.buffer.RemoveData(buf, off, num, 0);
			return num;
		}

		// Token: 0x06002615 RID: 9749 RVA: 0x000CFDDC File Offset: 0x000CFDDC
		public override int ReadByte()
		{
			if (this.buffer.Available == 0)
			{
				return -1;
			}
			return (int)(this.buffer.RemoveData(1, 0)[0] & byte.MaxValue);
		}

		// Token: 0x06002616 RID: 9750 RVA: 0x000CFE08 File Offset: 0x000CFE08
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002617 RID: 9751 RVA: 0x000CFE10 File Offset: 0x000CFE10
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002618 RID: 9752 RVA: 0x000CFE18 File Offset: 0x000CFE18
		public virtual int Skip(int n)
		{
			int num = Math.Min(this.buffer.Available, n);
			this.buffer.RemoveData(num);
			return num;
		}

		// Token: 0x06002619 RID: 9753 RVA: 0x000CFE48 File Offset: 0x000CFE48
		public virtual void Write(byte[] buf)
		{
			this.buffer.AddData(buf, 0, buf.Length);
		}

		// Token: 0x0600261A RID: 9754 RVA: 0x000CFE5C File Offset: 0x000CFE5C
		public override void Write(byte[] buf, int off, int len)
		{
			this.buffer.AddData(buf, off, len);
		}

		// Token: 0x0600261B RID: 9755 RVA: 0x000CFE6C File Offset: 0x000CFE6C
		public override void WriteByte(byte b)
		{
			this.buffer.AddData(new byte[]
			{
				b
			}, 0, 1);
		}

		// Token: 0x040017D7 RID: 6103
		private readonly ByteQueue buffer;
	}
}
