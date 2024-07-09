using System;
using System.IO;

namespace Org.BouncyCastle.Utilities.Zlib
{
	// Token: 0x020006F9 RID: 1785
	public class ZInputStream : Stream
	{
		// Token: 0x06003E3D RID: 15933 RVA: 0x00158320 File Offset: 0x00158320
		private static ZStream GetDefaultZStream(bool nowrap)
		{
			ZStream zstream = new ZStream();
			zstream.inflateInit(nowrap);
			return zstream;
		}

		// Token: 0x06003E3E RID: 15934 RVA: 0x00158340 File Offset: 0x00158340
		public ZInputStream(Stream input) : this(input, false)
		{
		}

		// Token: 0x06003E3F RID: 15935 RVA: 0x0015834C File Offset: 0x0015834C
		public ZInputStream(Stream input, bool nowrap) : this(input, ZInputStream.GetDefaultZStream(nowrap))
		{
		}

		// Token: 0x06003E40 RID: 15936 RVA: 0x0015835C File Offset: 0x0015835C
		public ZInputStream(Stream input, ZStream z)
		{
			this.flushLevel = 0;
			this.buf = new byte[4096];
			this.buf1 = new byte[1];
			this.nomoreinput = false;
			base..ctor();
			if (z == null)
			{
				z = new ZStream();
			}
			if (z.istate == null && z.dstate == null)
			{
				z.inflateInit();
			}
			this.input = input;
			this.compress = (z.istate == null);
			this.z = z;
			this.z.next_in = this.buf;
			this.z.next_in_index = 0;
			this.z.avail_in = 0;
		}

		// Token: 0x06003E41 RID: 15937 RVA: 0x00158410 File Offset: 0x00158410
		public ZInputStream(Stream input, int level) : this(input, level, false)
		{
		}

		// Token: 0x06003E42 RID: 15938 RVA: 0x0015841C File Offset: 0x0015841C
		public ZInputStream(Stream input, int level, bool nowrap)
		{
			this.flushLevel = 0;
			this.buf = new byte[4096];
			this.buf1 = new byte[1];
			this.nomoreinput = false;
			base..ctor();
			this.input = input;
			this.compress = true;
			this.z = new ZStream();
			this.z.deflateInit(level, nowrap);
			this.z.next_in = this.buf;
			this.z.next_in_index = 0;
			this.z.avail_in = 0;
		}

		// Token: 0x17000A92 RID: 2706
		// (get) Token: 0x06003E43 RID: 15939 RVA: 0x001584B0 File Offset: 0x001584B0
		public sealed override bool CanRead
		{
			get
			{
				return !this.closed;
			}
		}

		// Token: 0x17000A93 RID: 2707
		// (get) Token: 0x06003E44 RID: 15940 RVA: 0x001584BC File Offset: 0x001584BC
		public sealed override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000A94 RID: 2708
		// (get) Token: 0x06003E45 RID: 15941 RVA: 0x001584C0 File Offset: 0x001584C0
		public sealed override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06003E46 RID: 15942 RVA: 0x001584C4 File Offset: 0x001584C4
		public override void Close()
		{
			if (this.closed)
			{
				return;
			}
			this.closed = true;
			Platform.Dispose(this.input);
			base.Close();
		}

		// Token: 0x06003E47 RID: 15943 RVA: 0x001584EC File Offset: 0x001584EC
		public sealed override void Flush()
		{
		}

		// Token: 0x17000A95 RID: 2709
		// (get) Token: 0x06003E48 RID: 15944 RVA: 0x001584F0 File Offset: 0x001584F0
		// (set) Token: 0x06003E49 RID: 15945 RVA: 0x001584F8 File Offset: 0x001584F8
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

		// Token: 0x17000A96 RID: 2710
		// (get) Token: 0x06003E4A RID: 15946 RVA: 0x00158504 File Offset: 0x00158504
		public sealed override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000A97 RID: 2711
		// (get) Token: 0x06003E4B RID: 15947 RVA: 0x0015850C File Offset: 0x0015850C
		// (set) Token: 0x06003E4C RID: 15948 RVA: 0x00158514 File Offset: 0x00158514
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

		// Token: 0x06003E4D RID: 15949 RVA: 0x0015851C File Offset: 0x0015851C
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
					this.z.avail_in = this.input.Read(this.buf, 0, this.buf.Length);
					if (this.z.avail_in <= 0)
					{
						this.z.avail_in = 0;
						this.nomoreinput = true;
					}
				}
				int num = this.compress ? this.z.deflate(this.flushLevel) : this.z.inflate(this.flushLevel);
				if (this.nomoreinput && num == -5)
				{
					break;
				}
				if (num != 0 && num != 1)
				{
					goto Block_9;
				}
				if ((this.nomoreinput || num == 1) && this.z.avail_out == len)
				{
					return 0;
				}
				if (this.z.avail_out != len || num != 0)
				{
					goto IL_162;
				}
			}
			return 0;
			Block_9:
			throw new IOException((this.compress ? "de" : "in") + "flating: " + this.z.msg);
			IL_162:
			return len - this.z.avail_out;
		}

		// Token: 0x06003E4E RID: 15950 RVA: 0x0015869C File Offset: 0x0015869C
		public override int ReadByte()
		{
			if (this.Read(this.buf1, 0, 1) <= 0)
			{
				return -1;
			}
			return (int)this.buf1[0];
		}

		// Token: 0x06003E4F RID: 15951 RVA: 0x001586BC File Offset: 0x001586BC
		public sealed override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003E50 RID: 15952 RVA: 0x001586C4 File Offset: 0x001586C4
		public sealed override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000A98 RID: 2712
		// (get) Token: 0x06003E51 RID: 15953 RVA: 0x001586CC File Offset: 0x001586CC
		public virtual long TotalIn
		{
			get
			{
				return this.z.total_in;
			}
		}

		// Token: 0x17000A99 RID: 2713
		// (get) Token: 0x06003E52 RID: 15954 RVA: 0x001586DC File Offset: 0x001586DC
		public virtual long TotalOut
		{
			get
			{
				return this.z.total_out;
			}
		}

		// Token: 0x06003E53 RID: 15955 RVA: 0x001586EC File Offset: 0x001586EC
		public sealed override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x04002050 RID: 8272
		private const int BufferSize = 4096;

		// Token: 0x04002051 RID: 8273
		protected ZStream z;

		// Token: 0x04002052 RID: 8274
		protected int flushLevel;

		// Token: 0x04002053 RID: 8275
		protected byte[] buf;

		// Token: 0x04002054 RID: 8276
		protected byte[] buf1;

		// Token: 0x04002055 RID: 8277
		protected bool compress;

		// Token: 0x04002056 RID: 8278
		protected Stream input;

		// Token: 0x04002057 RID: 8279
		protected bool closed;

		// Token: 0x04002058 RID: 8280
		private bool nomoreinput;
	}
}
