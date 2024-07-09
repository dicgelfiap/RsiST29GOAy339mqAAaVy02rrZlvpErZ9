using System;
using System.IO;

namespace Org.BouncyCastle.Utilities.IO
{
	// Token: 0x02000239 RID: 569
	public class FilterStream : Stream
	{
		// Token: 0x06001261 RID: 4705 RVA: 0x00067A74 File Offset: 0x00067A74
		public FilterStream(Stream s)
		{
			this.s = s;
		}

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x06001262 RID: 4706 RVA: 0x00067A84 File Offset: 0x00067A84
		public override bool CanRead
		{
			get
			{
				return this.s.CanRead;
			}
		}

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x06001263 RID: 4707 RVA: 0x00067A94 File Offset: 0x00067A94
		public override bool CanSeek
		{
			get
			{
				return this.s.CanSeek;
			}
		}

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x06001264 RID: 4708 RVA: 0x00067AA4 File Offset: 0x00067AA4
		public override bool CanWrite
		{
			get
			{
				return this.s.CanWrite;
			}
		}

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x06001265 RID: 4709 RVA: 0x00067AB4 File Offset: 0x00067AB4
		public override long Length
		{
			get
			{
				return this.s.Length;
			}
		}

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x06001266 RID: 4710 RVA: 0x00067AC4 File Offset: 0x00067AC4
		// (set) Token: 0x06001267 RID: 4711 RVA: 0x00067AD4 File Offset: 0x00067AD4
		public override long Position
		{
			get
			{
				return this.s.Position;
			}
			set
			{
				this.s.Position = value;
			}
		}

		// Token: 0x06001268 RID: 4712 RVA: 0x00067AE4 File Offset: 0x00067AE4
		public override void Close()
		{
			Platform.Dispose(this.s);
			base.Close();
		}

		// Token: 0x06001269 RID: 4713 RVA: 0x00067AF8 File Offset: 0x00067AF8
		public override void Flush()
		{
			this.s.Flush();
		}

		// Token: 0x0600126A RID: 4714 RVA: 0x00067B08 File Offset: 0x00067B08
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this.s.Seek(offset, origin);
		}

		// Token: 0x0600126B RID: 4715 RVA: 0x00067B18 File Offset: 0x00067B18
		public override void SetLength(long value)
		{
			this.s.SetLength(value);
		}

		// Token: 0x0600126C RID: 4716 RVA: 0x00067B28 File Offset: 0x00067B28
		public override int Read(byte[] buffer, int offset, int count)
		{
			return this.s.Read(buffer, offset, count);
		}

		// Token: 0x0600126D RID: 4717 RVA: 0x00067B38 File Offset: 0x00067B38
		public override int ReadByte()
		{
			return this.s.ReadByte();
		}

		// Token: 0x0600126E RID: 4718 RVA: 0x00067B48 File Offset: 0x00067B48
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.s.Write(buffer, offset, count);
		}

		// Token: 0x0600126F RID: 4719 RVA: 0x00067B58 File Offset: 0x00067B58
		public override void WriteByte(byte value)
		{
			this.s.WriteByte(value);
		}

		// Token: 0x04000D5C RID: 3420
		protected readonly Stream s;
	}
}
