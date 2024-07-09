using System;

namespace Org.BouncyCastle.Utilities.Zlib
{
	// Token: 0x020006FA RID: 1786
	public sealed class ZStream
	{
		// Token: 0x06003E54 RID: 15956 RVA: 0x001586F4 File Offset: 0x001586F4
		public int inflateInit()
		{
			return this.inflateInit(15);
		}

		// Token: 0x06003E55 RID: 15957 RVA: 0x00158700 File Offset: 0x00158700
		public int inflateInit(bool nowrap)
		{
			return this.inflateInit(15, nowrap);
		}

		// Token: 0x06003E56 RID: 15958 RVA: 0x0015870C File Offset: 0x0015870C
		public int inflateInit(int w)
		{
			return this.inflateInit(w, false);
		}

		// Token: 0x06003E57 RID: 15959 RVA: 0x00158718 File Offset: 0x00158718
		public int inflateInit(int w, bool nowrap)
		{
			this.istate = new Inflate();
			return this.istate.inflateInit(this, nowrap ? (-w) : w);
		}

		// Token: 0x06003E58 RID: 15960 RVA: 0x00158740 File Offset: 0x00158740
		public int inflate(int f)
		{
			if (this.istate == null)
			{
				return -2;
			}
			return this.istate.inflate(this, f);
		}

		// Token: 0x06003E59 RID: 15961 RVA: 0x00158760 File Offset: 0x00158760
		public int inflateEnd()
		{
			if (this.istate == null)
			{
				return -2;
			}
			int result = this.istate.inflateEnd(this);
			this.istate = null;
			return result;
		}

		// Token: 0x06003E5A RID: 15962 RVA: 0x00158794 File Offset: 0x00158794
		public int inflateSync()
		{
			if (this.istate == null)
			{
				return -2;
			}
			return this.istate.inflateSync(this);
		}

		// Token: 0x06003E5B RID: 15963 RVA: 0x001587B0 File Offset: 0x001587B0
		public int inflateSetDictionary(byte[] dictionary, int dictLength)
		{
			if (this.istate == null)
			{
				return -2;
			}
			return this.istate.inflateSetDictionary(this, dictionary, dictLength);
		}

		// Token: 0x06003E5C RID: 15964 RVA: 0x001587D0 File Offset: 0x001587D0
		public int deflateInit(int level)
		{
			return this.deflateInit(level, 15);
		}

		// Token: 0x06003E5D RID: 15965 RVA: 0x001587DC File Offset: 0x001587DC
		public int deflateInit(int level, bool nowrap)
		{
			return this.deflateInit(level, 15, nowrap);
		}

		// Token: 0x06003E5E RID: 15966 RVA: 0x001587E8 File Offset: 0x001587E8
		public int deflateInit(int level, int bits)
		{
			return this.deflateInit(level, bits, false);
		}

		// Token: 0x06003E5F RID: 15967 RVA: 0x001587F4 File Offset: 0x001587F4
		public int deflateInit(int level, int bits, bool nowrap)
		{
			this.dstate = new Deflate();
			return this.dstate.deflateInit(this, level, nowrap ? (-bits) : bits);
		}

		// Token: 0x06003E60 RID: 15968 RVA: 0x0015882C File Offset: 0x0015882C
		public int deflate(int flush)
		{
			if (this.dstate == null)
			{
				return -2;
			}
			return this.dstate.deflate(this, flush);
		}

		// Token: 0x06003E61 RID: 15969 RVA: 0x0015884C File Offset: 0x0015884C
		public int deflateEnd()
		{
			if (this.dstate == null)
			{
				return -2;
			}
			int result = this.dstate.deflateEnd();
			this.dstate = null;
			return result;
		}

		// Token: 0x06003E62 RID: 15970 RVA: 0x00158880 File Offset: 0x00158880
		public int deflateParams(int level, int strategy)
		{
			if (this.dstate == null)
			{
				return -2;
			}
			return this.dstate.deflateParams(this, level, strategy);
		}

		// Token: 0x06003E63 RID: 15971 RVA: 0x001588A0 File Offset: 0x001588A0
		public int deflateSetDictionary(byte[] dictionary, int dictLength)
		{
			if (this.dstate == null)
			{
				return -2;
			}
			return this.dstate.deflateSetDictionary(this, dictionary, dictLength);
		}

		// Token: 0x06003E64 RID: 15972 RVA: 0x001588C0 File Offset: 0x001588C0
		internal void flush_pending()
		{
			int pending = this.dstate.pending;
			if (pending > this.avail_out)
			{
				pending = this.avail_out;
			}
			if (pending == 0)
			{
				return;
			}
			if (this.dstate.pending_buf.Length > this.dstate.pending_out && this.next_out.Length > this.next_out_index && this.dstate.pending_buf.Length >= this.dstate.pending_out + pending)
			{
				int num = this.next_out.Length;
				int num2 = this.next_out_index + pending;
			}
			Array.Copy(this.dstate.pending_buf, this.dstate.pending_out, this.next_out, this.next_out_index, pending);
			this.next_out_index += pending;
			this.dstate.pending_out += pending;
			this.total_out += (long)pending;
			this.avail_out -= pending;
			this.dstate.pending -= pending;
			if (this.dstate.pending == 0)
			{
				this.dstate.pending_out = 0;
			}
		}

		// Token: 0x06003E65 RID: 15973 RVA: 0x001589F0 File Offset: 0x001589F0
		internal int read_buf(byte[] buf, int start, int size)
		{
			int num = this.avail_in;
			if (num > size)
			{
				num = size;
			}
			if (num == 0)
			{
				return 0;
			}
			this.avail_in -= num;
			if (this.dstate.noheader == 0)
			{
				this.adler = this._adler.adler32(this.adler, this.next_in, this.next_in_index, num);
			}
			Array.Copy(this.next_in, this.next_in_index, buf, start, num);
			this.next_in_index += num;
			this.total_in += (long)num;
			return num;
		}

		// Token: 0x06003E66 RID: 15974 RVA: 0x00158A90 File Offset: 0x00158A90
		public void free()
		{
			this.next_in = null;
			this.next_out = null;
			this.msg = null;
			this._adler = null;
		}

		// Token: 0x04002059 RID: 8281
		private const int MAX_WBITS = 15;

		// Token: 0x0400205A RID: 8282
		private const int DEF_WBITS = 15;

		// Token: 0x0400205B RID: 8283
		private const int Z_NO_FLUSH = 0;

		// Token: 0x0400205C RID: 8284
		private const int Z_PARTIAL_FLUSH = 1;

		// Token: 0x0400205D RID: 8285
		private const int Z_SYNC_FLUSH = 2;

		// Token: 0x0400205E RID: 8286
		private const int Z_FULL_FLUSH = 3;

		// Token: 0x0400205F RID: 8287
		private const int Z_FINISH = 4;

		// Token: 0x04002060 RID: 8288
		private const int MAX_MEM_LEVEL = 9;

		// Token: 0x04002061 RID: 8289
		private const int Z_OK = 0;

		// Token: 0x04002062 RID: 8290
		private const int Z_STREAM_END = 1;

		// Token: 0x04002063 RID: 8291
		private const int Z_NEED_DICT = 2;

		// Token: 0x04002064 RID: 8292
		private const int Z_ERRNO = -1;

		// Token: 0x04002065 RID: 8293
		private const int Z_STREAM_ERROR = -2;

		// Token: 0x04002066 RID: 8294
		private const int Z_DATA_ERROR = -3;

		// Token: 0x04002067 RID: 8295
		private const int Z_MEM_ERROR = -4;

		// Token: 0x04002068 RID: 8296
		private const int Z_BUF_ERROR = -5;

		// Token: 0x04002069 RID: 8297
		private const int Z_VERSION_ERROR = -6;

		// Token: 0x0400206A RID: 8298
		public byte[] next_in;

		// Token: 0x0400206B RID: 8299
		public int next_in_index;

		// Token: 0x0400206C RID: 8300
		public int avail_in;

		// Token: 0x0400206D RID: 8301
		public long total_in;

		// Token: 0x0400206E RID: 8302
		public byte[] next_out;

		// Token: 0x0400206F RID: 8303
		public int next_out_index;

		// Token: 0x04002070 RID: 8304
		public int avail_out;

		// Token: 0x04002071 RID: 8305
		public long total_out;

		// Token: 0x04002072 RID: 8306
		public string msg;

		// Token: 0x04002073 RID: 8307
		internal Deflate dstate;

		// Token: 0x04002074 RID: 8308
		internal Inflate istate;

		// Token: 0x04002075 RID: 8309
		internal int data_type;

		// Token: 0x04002076 RID: 8310
		public long adler;

		// Token: 0x04002077 RID: 8311
		internal Adler32 _adler = new Adler32();
	}
}
