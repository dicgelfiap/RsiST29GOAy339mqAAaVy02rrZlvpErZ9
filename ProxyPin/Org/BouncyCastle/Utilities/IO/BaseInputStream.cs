using System;
using System.IO;

namespace Org.BouncyCastle.Utilities.IO
{
	// Token: 0x02000262 RID: 610
	public abstract class BaseInputStream : Stream
	{
		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x06001362 RID: 4962 RVA: 0x0006A870 File Offset: 0x0006A870
		public sealed override bool CanRead
		{
			get
			{
				return !this.closed;
			}
		}

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x06001363 RID: 4963 RVA: 0x0006A87C File Offset: 0x0006A87C
		public sealed override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x06001364 RID: 4964 RVA: 0x0006A880 File Offset: 0x0006A880
		public sealed override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001365 RID: 4965 RVA: 0x0006A884 File Offset: 0x0006A884
		public override void Close()
		{
			this.closed = true;
			base.Close();
		}

		// Token: 0x06001366 RID: 4966 RVA: 0x0006A894 File Offset: 0x0006A894
		public sealed override void Flush()
		{
		}

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x06001367 RID: 4967 RVA: 0x0006A898 File Offset: 0x0006A898
		public sealed override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x06001368 RID: 4968 RVA: 0x0006A8A0 File Offset: 0x0006A8A0
		// (set) Token: 0x06001369 RID: 4969 RVA: 0x0006A8A8 File Offset: 0x0006A8A8
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

		// Token: 0x0600136A RID: 4970 RVA: 0x0006A8B0 File Offset: 0x0006A8B0
		public override int Read(byte[] buffer, int offset, int count)
		{
			int i = offset;
			try
			{
				int num = offset + count;
				while (i < num)
				{
					int num2 = this.ReadByte();
					if (num2 == -1)
					{
						break;
					}
					buffer[i++] = (byte)num2;
				}
			}
			catch (IOException)
			{
				if (i == offset)
				{
					throw;
				}
			}
			return i - offset;
		}

		// Token: 0x0600136B RID: 4971 RVA: 0x0006A90C File Offset: 0x0006A90C
		public sealed override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600136C RID: 4972 RVA: 0x0006A914 File Offset: 0x0006A914
		public sealed override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600136D RID: 4973 RVA: 0x0006A91C File Offset: 0x0006A91C
		public sealed override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x04000D9D RID: 3485
		private bool closed;
	}
}
