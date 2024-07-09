using System;
using System.IO;

namespace Org.BouncyCastle.Utilities.Zlib
{
	// Token: 0x020006F7 RID: 1783
	[Obsolete("Use 'ZOutputStream' instead")]
	public class ZDeflaterOutputStream : Stream
	{
		// Token: 0x06003E1B RID: 15899 RVA: 0x00157DB0 File Offset: 0x00157DB0
		public ZDeflaterOutputStream(Stream outp) : this(outp, 6, false)
		{
		}

		// Token: 0x06003E1C RID: 15900 RVA: 0x00157DBC File Offset: 0x00157DBC
		public ZDeflaterOutputStream(Stream outp, int level) : this(outp, level, false)
		{
		}

		// Token: 0x06003E1D RID: 15901 RVA: 0x00157DC8 File Offset: 0x00157DC8
		public ZDeflaterOutputStream(Stream outp, int level, bool nowrap)
		{
			this.outp = outp;
			this.z.deflateInit(level, nowrap);
		}

		// Token: 0x17000A88 RID: 2696
		// (get) Token: 0x06003E1E RID: 15902 RVA: 0x00157E24 File Offset: 0x00157E24
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000A89 RID: 2697
		// (get) Token: 0x06003E1F RID: 15903 RVA: 0x00157E28 File Offset: 0x00157E28
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000A8A RID: 2698
		// (get) Token: 0x06003E20 RID: 15904 RVA: 0x00157E2C File Offset: 0x00157E2C
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000A8B RID: 2699
		// (get) Token: 0x06003E21 RID: 15905 RVA: 0x00157E30 File Offset: 0x00157E30
		public override long Length
		{
			get
			{
				return 0L;
			}
		}

		// Token: 0x17000A8C RID: 2700
		// (get) Token: 0x06003E22 RID: 15906 RVA: 0x00157E34 File Offset: 0x00157E34
		// (set) Token: 0x06003E23 RID: 15907 RVA: 0x00157E38 File Offset: 0x00157E38
		public override long Position
		{
			get
			{
				return 0L;
			}
			set
			{
			}
		}

		// Token: 0x06003E24 RID: 15908 RVA: 0x00157E3C File Offset: 0x00157E3C
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
				this.z.avail_out = 4192;
				int num = this.z.deflate(this.flushLevel);
				if (num != 0)
				{
					break;
				}
				if (this.z.avail_out < 4192)
				{
					this.outp.Write(this.buf, 0, 4192 - this.z.avail_out);
				}
				if (this.z.avail_in <= 0 && this.z.avail_out != 0)
				{
					return;
				}
			}
			throw new IOException("deflating: " + this.z.msg);
		}

		// Token: 0x06003E25 RID: 15909 RVA: 0x00157F34 File Offset: 0x00157F34
		public override long Seek(long offset, SeekOrigin origin)
		{
			return 0L;
		}

		// Token: 0x06003E26 RID: 15910 RVA: 0x00157F38 File Offset: 0x00157F38
		public override void SetLength(long value)
		{
		}

		// Token: 0x06003E27 RID: 15911 RVA: 0x00157F3C File Offset: 0x00157F3C
		public override int Read(byte[] buffer, int offset, int count)
		{
			return 0;
		}

		// Token: 0x06003E28 RID: 15912 RVA: 0x00157F40 File Offset: 0x00157F40
		public override void Flush()
		{
			this.outp.Flush();
		}

		// Token: 0x06003E29 RID: 15913 RVA: 0x00157F50 File Offset: 0x00157F50
		public override void WriteByte(byte b)
		{
			this.buf1[0] = b;
			this.Write(this.buf1, 0, 1);
		}

		// Token: 0x06003E2A RID: 15914 RVA: 0x00157F6C File Offset: 0x00157F6C
		public void Finish()
		{
			for (;;)
			{
				this.z.next_out = this.buf;
				this.z.next_out_index = 0;
				this.z.avail_out = 4192;
				int num = this.z.deflate(4);
				if (num != 1 && num != 0)
				{
					break;
				}
				if (4192 - this.z.avail_out > 0)
				{
					this.outp.Write(this.buf, 0, 4192 - this.z.avail_out);
				}
				if (this.z.avail_in <= 0 && this.z.avail_out != 0)
				{
					goto Block_4;
				}
			}
			throw new IOException("deflating: " + this.z.msg);
			Block_4:
			this.Flush();
		}

		// Token: 0x06003E2B RID: 15915 RVA: 0x00158040 File Offset: 0x00158040
		public void End()
		{
			if (this.z == null)
			{
				return;
			}
			this.z.deflateEnd();
			this.z.free();
			this.z = null;
		}

		// Token: 0x06003E2C RID: 15916 RVA: 0x0015806C File Offset: 0x0015806C
		public override void Close()
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
				this.End();
				Platform.Dispose(this.outp);
				this.outp = null;
			}
			base.Close();
		}

		// Token: 0x04002043 RID: 8259
		private const int BUFSIZE = 4192;

		// Token: 0x04002044 RID: 8260
		protected ZStream z = new ZStream();

		// Token: 0x04002045 RID: 8261
		protected int flushLevel = 0;

		// Token: 0x04002046 RID: 8262
		protected byte[] buf = new byte[4192];

		// Token: 0x04002047 RID: 8263
		private byte[] buf1 = new byte[1];

		// Token: 0x04002048 RID: 8264
		protected Stream outp;
	}
}
