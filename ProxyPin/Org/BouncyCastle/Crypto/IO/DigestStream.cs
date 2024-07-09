using System;
using System.IO;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.IO
{
	// Token: 0x020003DC RID: 988
	public class DigestStream : Stream
	{
		// Token: 0x06001F2F RID: 7983 RVA: 0x000B6AD4 File Offset: 0x000B6AD4
		public DigestStream(Stream stream, IDigest readDigest, IDigest writeDigest)
		{
			this.stream = stream;
			this.inDigest = readDigest;
			this.outDigest = writeDigest;
		}

		// Token: 0x06001F30 RID: 7984 RVA: 0x000B6AF4 File Offset: 0x000B6AF4
		public virtual IDigest ReadDigest()
		{
			return this.inDigest;
		}

		// Token: 0x06001F31 RID: 7985 RVA: 0x000B6AFC File Offset: 0x000B6AFC
		public virtual IDigest WriteDigest()
		{
			return this.outDigest;
		}

		// Token: 0x06001F32 RID: 7986 RVA: 0x000B6B04 File Offset: 0x000B6B04
		public override int Read(byte[] buffer, int offset, int count)
		{
			int num = this.stream.Read(buffer, offset, count);
			if (this.inDigest != null && num > 0)
			{
				this.inDigest.BlockUpdate(buffer, offset, num);
			}
			return num;
		}

		// Token: 0x06001F33 RID: 7987 RVA: 0x000B6B48 File Offset: 0x000B6B48
		public override int ReadByte()
		{
			int num = this.stream.ReadByte();
			if (this.inDigest != null && num >= 0)
			{
				this.inDigest.Update((byte)num);
			}
			return num;
		}

		// Token: 0x06001F34 RID: 7988 RVA: 0x000B6B88 File Offset: 0x000B6B88
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (this.outDigest != null && count > 0)
			{
				this.outDigest.BlockUpdate(buffer, offset, count);
			}
			this.stream.Write(buffer, offset, count);
		}

		// Token: 0x06001F35 RID: 7989 RVA: 0x000B6BB8 File Offset: 0x000B6BB8
		public override void WriteByte(byte b)
		{
			if (this.outDigest != null)
			{
				this.outDigest.Update(b);
			}
			this.stream.WriteByte(b);
		}

		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x06001F36 RID: 7990 RVA: 0x000B6BE0 File Offset: 0x000B6BE0
		public override bool CanRead
		{
			get
			{
				return this.stream.CanRead;
			}
		}

		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x06001F37 RID: 7991 RVA: 0x000B6BF0 File Offset: 0x000B6BF0
		public override bool CanWrite
		{
			get
			{
				return this.stream.CanWrite;
			}
		}

		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x06001F38 RID: 7992 RVA: 0x000B6C00 File Offset: 0x000B6C00
		public override bool CanSeek
		{
			get
			{
				return this.stream.CanSeek;
			}
		}

		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x06001F39 RID: 7993 RVA: 0x000B6C10 File Offset: 0x000B6C10
		public override long Length
		{
			get
			{
				return this.stream.Length;
			}
		}

		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x06001F3A RID: 7994 RVA: 0x000B6C20 File Offset: 0x000B6C20
		// (set) Token: 0x06001F3B RID: 7995 RVA: 0x000B6C30 File Offset: 0x000B6C30
		public override long Position
		{
			get
			{
				return this.stream.Position;
			}
			set
			{
				this.stream.Position = value;
			}
		}

		// Token: 0x06001F3C RID: 7996 RVA: 0x000B6C40 File Offset: 0x000B6C40
		public override void Close()
		{
			Platform.Dispose(this.stream);
			base.Close();
		}

		// Token: 0x06001F3D RID: 7997 RVA: 0x000B6C54 File Offset: 0x000B6C54
		public override void Flush()
		{
			this.stream.Flush();
		}

		// Token: 0x06001F3E RID: 7998 RVA: 0x000B6C64 File Offset: 0x000B6C64
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this.stream.Seek(offset, origin);
		}

		// Token: 0x06001F3F RID: 7999 RVA: 0x000B6C74 File Offset: 0x000B6C74
		public override void SetLength(long length)
		{
			this.stream.SetLength(length);
		}

		// Token: 0x04001488 RID: 5256
		protected readonly Stream stream;

		// Token: 0x04001489 RID: 5257
		protected readonly IDigest inDigest;

		// Token: 0x0400148A RID: 5258
		protected readonly IDigest outDigest;
	}
}
