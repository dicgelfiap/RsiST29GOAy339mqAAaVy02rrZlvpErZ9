using System;
using System.IO;

namespace Org.BouncyCastle.Utilities.Zlib
{
	// Token: 0x020006F8 RID: 1784
	[Obsolete("Use 'ZInputStream' instead")]
	public class ZInflaterInputStream : Stream
	{
		// Token: 0x06003E2D RID: 15917 RVA: 0x001580CC File Offset: 0x001580CC
		public ZInflaterInputStream(Stream inp) : this(inp, false)
		{
		}

		// Token: 0x06003E2E RID: 15918 RVA: 0x001580D8 File Offset: 0x001580D8
		public ZInflaterInputStream(Stream inp, bool nowrap)
		{
			this.inp = inp;
			this.z.inflateInit(nowrap);
			this.z.next_in = this.buf;
			this.z.next_in_index = 0;
			this.z.avail_in = 0;
		}

		// Token: 0x17000A8D RID: 2701
		// (get) Token: 0x06003E2F RID: 15919 RVA: 0x00158168 File Offset: 0x00158168
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000A8E RID: 2702
		// (get) Token: 0x06003E30 RID: 15920 RVA: 0x0015816C File Offset: 0x0015816C
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000A8F RID: 2703
		// (get) Token: 0x06003E31 RID: 15921 RVA: 0x00158170 File Offset: 0x00158170
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000A90 RID: 2704
		// (get) Token: 0x06003E32 RID: 15922 RVA: 0x00158174 File Offset: 0x00158174
		public override long Length
		{
			get
			{
				return 0L;
			}
		}

		// Token: 0x17000A91 RID: 2705
		// (get) Token: 0x06003E33 RID: 15923 RVA: 0x00158178 File Offset: 0x00158178
		// (set) Token: 0x06003E34 RID: 15924 RVA: 0x0015817C File Offset: 0x0015817C
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

		// Token: 0x06003E35 RID: 15925 RVA: 0x00158180 File Offset: 0x00158180
		public override void Write(byte[] b, int off, int len)
		{
		}

		// Token: 0x06003E36 RID: 15926 RVA: 0x00158184 File Offset: 0x00158184
		public override long Seek(long offset, SeekOrigin origin)
		{
			return 0L;
		}

		// Token: 0x06003E37 RID: 15927 RVA: 0x00158188 File Offset: 0x00158188
		public override void SetLength(long value)
		{
		}

		// Token: 0x06003E38 RID: 15928 RVA: 0x0015818C File Offset: 0x0015818C
		public override int Read(byte[] b, int off, int len)
		{
			if (len == 0)
			{
				return 0;
			}
			this.z.next_out = b;
			this.z.next_out_index = off;
			this.z.avail_out = len;
			for (;;)
			{
				if (this.z.avail_in == 0 && !this.nomoreinput)
				{
					this.z.next_in_index = 0;
					this.z.avail_in = this.inp.Read(this.buf, 0, 4192);
					if (this.z.avail_in <= 0)
					{
						this.z.avail_in = 0;
						this.nomoreinput = true;
					}
				}
				int num = this.z.inflate(this.flushLevel);
				if (this.nomoreinput && num == -5)
				{
					break;
				}
				if (num != 0 && num != 1)
				{
					goto Block_8;
				}
				if ((this.nomoreinput || num == 1) && this.z.avail_out == len)
				{
					return 0;
				}
				if (this.z.avail_out != len || num != 0)
				{
					goto IL_124;
				}
			}
			return 0;
			Block_8:
			throw new IOException("inflating: " + this.z.msg);
			IL_124:
			return len - this.z.avail_out;
		}

		// Token: 0x06003E39 RID: 15929 RVA: 0x001582D0 File Offset: 0x001582D0
		public override void Flush()
		{
			this.inp.Flush();
		}

		// Token: 0x06003E3A RID: 15930 RVA: 0x001582E0 File Offset: 0x001582E0
		public override void WriteByte(byte b)
		{
		}

		// Token: 0x06003E3B RID: 15931 RVA: 0x001582E4 File Offset: 0x001582E4
		public override void Close()
		{
			Platform.Dispose(this.inp);
			base.Close();
		}

		// Token: 0x06003E3C RID: 15932 RVA: 0x001582F8 File Offset: 0x001582F8
		public override int ReadByte()
		{
			if (this.Read(this.buf1, 0, 1) <= 0)
			{
				return -1;
			}
			return (int)(this.buf1[0] & byte.MaxValue);
		}

		// Token: 0x04002049 RID: 8265
		private const int BUFSIZE = 4192;

		// Token: 0x0400204A RID: 8266
		protected ZStream z = new ZStream();

		// Token: 0x0400204B RID: 8267
		protected int flushLevel = 0;

		// Token: 0x0400204C RID: 8268
		protected byte[] buf = new byte[4192];

		// Token: 0x0400204D RID: 8269
		private byte[] buf1 = new byte[1];

		// Token: 0x0400204E RID: 8270
		protected Stream inp = null;

		// Token: 0x0400204F RID: 8271
		private bool nomoreinput = false;
	}
}
