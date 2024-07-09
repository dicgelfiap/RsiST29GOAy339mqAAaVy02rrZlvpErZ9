using System;
using System.IO;

namespace Org.BouncyCastle.Utilities.Zlib
{
	// Token: 0x02000530 RID: 1328
	public class ZOutputStream : Stream
	{
		// Token: 0x0600287F RID: 10367 RVA: 0x000DAB68 File Offset: 0x000DAB68
		private static ZStream GetDefaultZStream(bool nowrap)
		{
			ZStream zstream = new ZStream();
			zstream.inflateInit(nowrap);
			return zstream;
		}

		// Token: 0x06002880 RID: 10368 RVA: 0x000DAB88 File Offset: 0x000DAB88
		public ZOutputStream(Stream output) : this(output, false)
		{
		}

		// Token: 0x06002881 RID: 10369 RVA: 0x000DAB94 File Offset: 0x000DAB94
		public ZOutputStream(Stream output, bool nowrap) : this(output, ZOutputStream.GetDefaultZStream(nowrap))
		{
		}

		// Token: 0x06002882 RID: 10370 RVA: 0x000DABA4 File Offset: 0x000DABA4
		public ZOutputStream(Stream output, ZStream z)
		{
			this.flushLevel = 0;
			this.buf = new byte[4096];
			this.buf1 = new byte[1];
			base..ctor();
			if (z == null)
			{
				z = new ZStream();
			}
			if (z.istate == null && z.dstate == null)
			{
				z.inflateInit();
			}
			this.output = output;
			this.compress = (z.istate == null);
			this.z = z;
		}

		// Token: 0x06002883 RID: 10371 RVA: 0x000DAC28 File Offset: 0x000DAC28
		public ZOutputStream(Stream output, int level) : this(output, level, false)
		{
		}

		// Token: 0x06002884 RID: 10372 RVA: 0x000DAC34 File Offset: 0x000DAC34
		public ZOutputStream(Stream output, int level, bool nowrap)
		{
			this.flushLevel = 0;
			this.buf = new byte[4096];
			this.buf1 = new byte[1];
			base..ctor();
			this.output = output;
			this.compress = true;
			this.z = new ZStream();
			this.z.deflateInit(level, nowrap);
		}

		// Token: 0x17000788 RID: 1928
		// (get) Token: 0x06002885 RID: 10373 RVA: 0x000DAC98 File Offset: 0x000DAC98
		public sealed override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000789 RID: 1929
		// (get) Token: 0x06002886 RID: 10374 RVA: 0x000DAC9C File Offset: 0x000DAC9C
		public sealed override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700078A RID: 1930
		// (get) Token: 0x06002887 RID: 10375 RVA: 0x000DACA0 File Offset: 0x000DACA0
		public sealed override bool CanWrite
		{
			get
			{
				return !this.closed;
			}
		}

		// Token: 0x06002888 RID: 10376 RVA: 0x000DACAC File Offset: 0x000DACAC
		public override void Close()
		{
			if (this.closed)
			{
				return;
			}
			this.DoClose();
			base.Close();
		}

		// Token: 0x06002889 RID: 10377 RVA: 0x000DACC8 File Offset: 0x000DACC8
		private void DoClose()
		{
			try
			{
				this.Finish();
			}
			catch (IOException)
			{
			}
			finally
			{
				this.closed = true;
				this.End();
				Platform.Dispose(this.output);
				this.output = null;
			}
		}

		// Token: 0x0600288A RID: 10378 RVA: 0x000DAD28 File Offset: 0x000DAD28
		public virtual void End()
		{
			if (this.z == null)
			{
				return;
			}
			if (this.compress)
			{
				this.z.deflateEnd();
			}
			else
			{
				this.z.inflateEnd();
			}
			this.z.free();
			this.z = null;
		}

		// Token: 0x0600288B RID: 10379 RVA: 0x000DAD80 File Offset: 0x000DAD80
		public virtual void Finish()
		{
			for (;;)
			{
				this.z.next_out = this.buf;
				this.z.next_out_index = 0;
				this.z.avail_out = this.buf.Length;
				int num = this.compress ? this.z.deflate(4) : this.z.inflate(4);
				if (num != 1 && num != 0)
				{
					break;
				}
				int num2 = this.buf.Length - this.z.avail_out;
				if (num2 > 0)
				{
					this.output.Write(this.buf, 0, num2);
				}
				if (this.z.avail_in <= 0 && this.z.avail_out != 0)
				{
					goto Block_6;
				}
			}
			throw new IOException((this.compress ? "de" : "in") + "flating: " + this.z.msg);
			Block_6:
			this.Flush();
		}

		// Token: 0x0600288C RID: 10380 RVA: 0x000DAE84 File Offset: 0x000DAE84
		public override void Flush()
		{
			this.output.Flush();
		}

		// Token: 0x1700078B RID: 1931
		// (get) Token: 0x0600288D RID: 10381 RVA: 0x000DAE94 File Offset: 0x000DAE94
		// (set) Token: 0x0600288E RID: 10382 RVA: 0x000DAE9C File Offset: 0x000DAE9C
		public virtual int FlushMode
		{
			get
			{
				return this.flushLevel;
			}
			set
			{
				this.flushLevel = value;
			}
		}

		// Token: 0x1700078C RID: 1932
		// (get) Token: 0x0600288F RID: 10383 RVA: 0x000DAEA8 File Offset: 0x000DAEA8
		public sealed override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700078D RID: 1933
		// (get) Token: 0x06002890 RID: 10384 RVA: 0x000DAEB0 File Offset: 0x000DAEB0
		// (set) Token: 0x06002891 RID: 10385 RVA: 0x000DAEB8 File Offset: 0x000DAEB8
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

		// Token: 0x06002892 RID: 10386 RVA: 0x000DAEC0 File Offset: 0x000DAEC0
		public sealed override int Read(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002893 RID: 10387 RVA: 0x000DAEC8 File Offset: 0x000DAEC8
		public sealed override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002894 RID: 10388 RVA: 0x000DAED0 File Offset: 0x000DAED0
		public sealed override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700078E RID: 1934
		// (get) Token: 0x06002895 RID: 10389 RVA: 0x000DAED8 File Offset: 0x000DAED8
		public virtual long TotalIn
		{
			get
			{
				return this.z.total_in;
			}
		}

		// Token: 0x1700078F RID: 1935
		// (get) Token: 0x06002896 RID: 10390 RVA: 0x000DAEE8 File Offset: 0x000DAEE8
		public virtual long TotalOut
		{
			get
			{
				return this.z.total_out;
			}
		}

		// Token: 0x06002897 RID: 10391 RVA: 0x000DAEF8 File Offset: 0x000DAEF8
		public override void Write(byte[] b, int off, int len)
		{
			if (len == 0)
			{
				return;
			}
			this.z.next_in = b;
			this.z.next_in_index = off;
			this.z.avail_in = len;
			for (;;)
			{
				this.z.next_out = this.buf;
				this.z.next_out_index = 0;
				this.z.avail_out = this.buf.Length;
				int num = this.compress ? this.z.deflate(this.flushLevel) : this.z.inflate(this.flushLevel);
				if (num != 0)
				{
					break;
				}
				this.output.Write(this.buf, 0, this.buf.Length - this.z.avail_out);
				if (this.z.avail_in <= 0 && this.z.avail_out != 0)
				{
					return;
				}
			}
			throw new IOException((this.compress ? "de" : "in") + "flating: " + this.z.msg);
		}

		// Token: 0x06002898 RID: 10392 RVA: 0x000DB01C File Offset: 0x000DB01C
		public override void WriteByte(byte b)
		{
			this.buf1[0] = b;
			this.Write(this.buf1, 0, 1);
		}

		// Token: 0x04001ABD RID: 6845
		private const int BufferSize = 4096;

		// Token: 0x04001ABE RID: 6846
		protected ZStream z;

		// Token: 0x04001ABF RID: 6847
		protected int flushLevel;

		// Token: 0x04001AC0 RID: 6848
		protected byte[] buf;

		// Token: 0x04001AC1 RID: 6849
		protected byte[] buf1;

		// Token: 0x04001AC2 RID: 6850
		protected bool compress;

		// Token: 0x04001AC3 RID: 6851
		protected Stream output;

		// Token: 0x04001AC4 RID: 6852
		protected bool closed;
	}
}
