using System;
using System.IO;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.IO
{
	// Token: 0x020003E0 RID: 992
	public class SignerStream : Stream
	{
		// Token: 0x06001F59 RID: 8025 RVA: 0x000B6EB4 File Offset: 0x000B6EB4
		public SignerStream(Stream stream, ISigner readSigner, ISigner writeSigner)
		{
			this.stream = stream;
			this.inSigner = readSigner;
			this.outSigner = writeSigner;
		}

		// Token: 0x06001F5A RID: 8026 RVA: 0x000B6ED4 File Offset: 0x000B6ED4
		public virtual ISigner ReadSigner()
		{
			return this.inSigner;
		}

		// Token: 0x06001F5B RID: 8027 RVA: 0x000B6EDC File Offset: 0x000B6EDC
		public virtual ISigner WriteSigner()
		{
			return this.outSigner;
		}

		// Token: 0x06001F5C RID: 8028 RVA: 0x000B6EE4 File Offset: 0x000B6EE4
		public override int Read(byte[] buffer, int offset, int count)
		{
			int num = this.stream.Read(buffer, offset, count);
			if (this.inSigner != null && num > 0)
			{
				this.inSigner.BlockUpdate(buffer, offset, num);
			}
			return num;
		}

		// Token: 0x06001F5D RID: 8029 RVA: 0x000B6F28 File Offset: 0x000B6F28
		public override int ReadByte()
		{
			int num = this.stream.ReadByte();
			if (this.inSigner != null && num >= 0)
			{
				this.inSigner.Update((byte)num);
			}
			return num;
		}

		// Token: 0x06001F5E RID: 8030 RVA: 0x000B6F68 File Offset: 0x000B6F68
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (this.outSigner != null && count > 0)
			{
				this.outSigner.BlockUpdate(buffer, offset, count);
			}
			this.stream.Write(buffer, offset, count);
		}

		// Token: 0x06001F5F RID: 8031 RVA: 0x000B6F98 File Offset: 0x000B6F98
		public override void WriteByte(byte b)
		{
			if (this.outSigner != null)
			{
				this.outSigner.Update(b);
			}
			this.stream.WriteByte(b);
		}

		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x06001F60 RID: 8032 RVA: 0x000B6FC0 File Offset: 0x000B6FC0
		public override bool CanRead
		{
			get
			{
				return this.stream.CanRead;
			}
		}

		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x06001F61 RID: 8033 RVA: 0x000B6FD0 File Offset: 0x000B6FD0
		public override bool CanWrite
		{
			get
			{
				return this.stream.CanWrite;
			}
		}

		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x06001F62 RID: 8034 RVA: 0x000B6FE0 File Offset: 0x000B6FE0
		public override bool CanSeek
		{
			get
			{
				return this.stream.CanSeek;
			}
		}

		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x06001F63 RID: 8035 RVA: 0x000B6FF0 File Offset: 0x000B6FF0
		public override long Length
		{
			get
			{
				return this.stream.Length;
			}
		}

		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x06001F64 RID: 8036 RVA: 0x000B7000 File Offset: 0x000B7000
		// (set) Token: 0x06001F65 RID: 8037 RVA: 0x000B7010 File Offset: 0x000B7010
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

		// Token: 0x06001F66 RID: 8038 RVA: 0x000B7020 File Offset: 0x000B7020
		public override void Close()
		{
			Platform.Dispose(this.stream);
			base.Close();
		}

		// Token: 0x06001F67 RID: 8039 RVA: 0x000B7034 File Offset: 0x000B7034
		public override void Flush()
		{
			this.stream.Flush();
		}

		// Token: 0x06001F68 RID: 8040 RVA: 0x000B7044 File Offset: 0x000B7044
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this.stream.Seek(offset, origin);
		}

		// Token: 0x06001F69 RID: 8041 RVA: 0x000B7054 File Offset: 0x000B7054
		public override void SetLength(long length)
		{
			this.stream.SetLength(length);
		}

		// Token: 0x04001490 RID: 5264
		protected readonly Stream stream;

		// Token: 0x04001491 RID: 5265
		protected readonly ISigner inSigner;

		// Token: 0x04001492 RID: 5266
		protected readonly ISigner outSigner;
	}
}
