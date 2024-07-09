using System;
using System.IO;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Utilities
{
	// Token: 0x020001D2 RID: 466
	[Obsolete("Use Org.BouncyCastle.Utilities.IO.FilterStream")]
	public class FilterStream : Stream
	{
		// Token: 0x06000F05 RID: 3845 RVA: 0x0005B10C File Offset: 0x0005B10C
		[Obsolete("Use Org.BouncyCastle.Utilities.IO.FilterStream")]
		public FilterStream(Stream s)
		{
			this.s = s;
		}

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x06000F06 RID: 3846 RVA: 0x0005B11C File Offset: 0x0005B11C
		public override bool CanRead
		{
			get
			{
				return this.s.CanRead;
			}
		}

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06000F07 RID: 3847 RVA: 0x0005B12C File Offset: 0x0005B12C
		public override bool CanSeek
		{
			get
			{
				return this.s.CanSeek;
			}
		}

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06000F08 RID: 3848 RVA: 0x0005B13C File Offset: 0x0005B13C
		public override bool CanWrite
		{
			get
			{
				return this.s.CanWrite;
			}
		}

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06000F09 RID: 3849 RVA: 0x0005B14C File Offset: 0x0005B14C
		public override long Length
		{
			get
			{
				return this.s.Length;
			}
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06000F0A RID: 3850 RVA: 0x0005B15C File Offset: 0x0005B15C
		// (set) Token: 0x06000F0B RID: 3851 RVA: 0x0005B16C File Offset: 0x0005B16C
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

		// Token: 0x06000F0C RID: 3852 RVA: 0x0005B17C File Offset: 0x0005B17C
		public override void Close()
		{
			Platform.Dispose(this.s);
			base.Close();
		}

		// Token: 0x06000F0D RID: 3853 RVA: 0x0005B190 File Offset: 0x0005B190
		public override void Flush()
		{
			this.s.Flush();
		}

		// Token: 0x06000F0E RID: 3854 RVA: 0x0005B1A0 File Offset: 0x0005B1A0
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this.s.Seek(offset, origin);
		}

		// Token: 0x06000F0F RID: 3855 RVA: 0x0005B1B0 File Offset: 0x0005B1B0
		public override void SetLength(long value)
		{
			this.s.SetLength(value);
		}

		// Token: 0x06000F10 RID: 3856 RVA: 0x0005B1C0 File Offset: 0x0005B1C0
		public override int Read(byte[] buffer, int offset, int count)
		{
			return this.s.Read(buffer, offset, count);
		}

		// Token: 0x06000F11 RID: 3857 RVA: 0x0005B1D0 File Offset: 0x0005B1D0
		public override int ReadByte()
		{
			return this.s.ReadByte();
		}

		// Token: 0x06000F12 RID: 3858 RVA: 0x0005B1E0 File Offset: 0x0005B1E0
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.s.Write(buffer, offset, count);
		}

		// Token: 0x06000F13 RID: 3859 RVA: 0x0005B1F0 File Offset: 0x0005B1F0
		public override void WriteByte(byte value)
		{
			this.s.WriteByte(value);
		}

		// Token: 0x04000B69 RID: 2921
		protected readonly Stream s;
	}
}
