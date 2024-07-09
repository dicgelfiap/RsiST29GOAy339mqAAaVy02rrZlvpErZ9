using System;
using System.IO;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.IO
{
	// Token: 0x020003DE RID: 990
	public class MacStream : Stream
	{
		// Token: 0x06001F44 RID: 8004 RVA: 0x000B6CC4 File Offset: 0x000B6CC4
		public MacStream(Stream stream, IMac readMac, IMac writeMac)
		{
			this.stream = stream;
			this.inMac = readMac;
			this.outMac = writeMac;
		}

		// Token: 0x06001F45 RID: 8005 RVA: 0x000B6CE4 File Offset: 0x000B6CE4
		public virtual IMac ReadMac()
		{
			return this.inMac;
		}

		// Token: 0x06001F46 RID: 8006 RVA: 0x000B6CEC File Offset: 0x000B6CEC
		public virtual IMac WriteMac()
		{
			return this.outMac;
		}

		// Token: 0x06001F47 RID: 8007 RVA: 0x000B6CF4 File Offset: 0x000B6CF4
		public override int Read(byte[] buffer, int offset, int count)
		{
			int num = this.stream.Read(buffer, offset, count);
			if (this.inMac != null && num > 0)
			{
				this.inMac.BlockUpdate(buffer, offset, num);
			}
			return num;
		}

		// Token: 0x06001F48 RID: 8008 RVA: 0x000B6D38 File Offset: 0x000B6D38
		public override int ReadByte()
		{
			int num = this.stream.ReadByte();
			if (this.inMac != null && num >= 0)
			{
				this.inMac.Update((byte)num);
			}
			return num;
		}

		// Token: 0x06001F49 RID: 8009 RVA: 0x000B6D78 File Offset: 0x000B6D78
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (this.outMac != null && count > 0)
			{
				this.outMac.BlockUpdate(buffer, offset, count);
			}
			this.stream.Write(buffer, offset, count);
		}

		// Token: 0x06001F4A RID: 8010 RVA: 0x000B6DA8 File Offset: 0x000B6DA8
		public override void WriteByte(byte b)
		{
			if (this.outMac != null)
			{
				this.outMac.Update(b);
			}
			this.stream.WriteByte(b);
		}

		// Token: 0x17000610 RID: 1552
		// (get) Token: 0x06001F4B RID: 8011 RVA: 0x000B6DD0 File Offset: 0x000B6DD0
		public override bool CanRead
		{
			get
			{
				return this.stream.CanRead;
			}
		}

		// Token: 0x17000611 RID: 1553
		// (get) Token: 0x06001F4C RID: 8012 RVA: 0x000B6DE0 File Offset: 0x000B6DE0
		public override bool CanWrite
		{
			get
			{
				return this.stream.CanWrite;
			}
		}

		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x06001F4D RID: 8013 RVA: 0x000B6DF0 File Offset: 0x000B6DF0
		public override bool CanSeek
		{
			get
			{
				return this.stream.CanSeek;
			}
		}

		// Token: 0x17000613 RID: 1555
		// (get) Token: 0x06001F4E RID: 8014 RVA: 0x000B6E00 File Offset: 0x000B6E00
		public override long Length
		{
			get
			{
				return this.stream.Length;
			}
		}

		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x06001F4F RID: 8015 RVA: 0x000B6E10 File Offset: 0x000B6E10
		// (set) Token: 0x06001F50 RID: 8016 RVA: 0x000B6E20 File Offset: 0x000B6E20
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

		// Token: 0x06001F51 RID: 8017 RVA: 0x000B6E30 File Offset: 0x000B6E30
		public override void Close()
		{
			Platform.Dispose(this.stream);
			base.Close();
		}

		// Token: 0x06001F52 RID: 8018 RVA: 0x000B6E44 File Offset: 0x000B6E44
		public override void Flush()
		{
			this.stream.Flush();
		}

		// Token: 0x06001F53 RID: 8019 RVA: 0x000B6E54 File Offset: 0x000B6E54
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this.stream.Seek(offset, origin);
		}

		// Token: 0x06001F54 RID: 8020 RVA: 0x000B6E64 File Offset: 0x000B6E64
		public override void SetLength(long length)
		{
			this.stream.SetLength(length);
		}

		// Token: 0x0400148C RID: 5260
		protected readonly Stream stream;

		// Token: 0x0400148D RID: 5261
		protected readonly IMac inMac;

		// Token: 0x0400148E RID: 5262
		protected readonly IMac outMac;
	}
}
