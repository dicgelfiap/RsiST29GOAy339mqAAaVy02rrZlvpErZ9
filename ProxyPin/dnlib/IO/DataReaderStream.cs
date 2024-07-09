using System;
using System.IO;

namespace dnlib.IO
{
	// Token: 0x02000767 RID: 1895
	internal sealed class DataReaderStream : Stream
	{
		// Token: 0x17000BA3 RID: 2979
		// (get) Token: 0x0600427A RID: 17018 RVA: 0x001659AC File Offset: 0x001659AC
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000BA4 RID: 2980
		// (get) Token: 0x0600427B RID: 17019 RVA: 0x001659B0 File Offset: 0x001659B0
		public override bool CanSeek
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000BA5 RID: 2981
		// (get) Token: 0x0600427C RID: 17020 RVA: 0x001659B4 File Offset: 0x001659B4
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000BA6 RID: 2982
		// (get) Token: 0x0600427D RID: 17021 RVA: 0x001659B8 File Offset: 0x001659B8
		public override long Length
		{
			get
			{
				return (long)((ulong)this.reader.Length);
			}
		}

		// Token: 0x17000BA7 RID: 2983
		// (get) Token: 0x0600427E RID: 17022 RVA: 0x001659C8 File Offset: 0x001659C8
		// (set) Token: 0x0600427F RID: 17023 RVA: 0x001659D0 File Offset: 0x001659D0
		public override long Position
		{
			get
			{
				return this.position;
			}
			set
			{
				this.position = value;
			}
		}

		// Token: 0x06004280 RID: 17024 RVA: 0x001659DC File Offset: 0x001659DC
		public DataReaderStream(in DataReader reader)
		{
			this.reader = reader;
			this.position = (long)((ulong)reader.Position);
		}

		// Token: 0x06004281 RID: 17025 RVA: 0x00165A00 File Offset: 0x00165A00
		public override void Flush()
		{
		}

		// Token: 0x06004282 RID: 17026 RVA: 0x00165A04 File Offset: 0x00165A04
		private bool CheckAndSetPosition()
		{
			if (this.position > (long)((ulong)this.reader.Length))
			{
				return false;
			}
			this.reader.Position = (uint)this.position;
			return true;
		}

		// Token: 0x06004283 RID: 17027 RVA: 0x00165A34 File Offset: 0x00165A34
		public override long Seek(long offset, SeekOrigin origin)
		{
			switch (origin)
			{
			case SeekOrigin.Begin:
				this.Position = offset;
				break;
			case SeekOrigin.Current:
				this.Position += offset;
				break;
			case SeekOrigin.End:
				this.Position = this.Length + offset;
				break;
			}
			return this.Position;
		}

		// Token: 0x06004284 RID: 17028 RVA: 0x00165A90 File Offset: 0x00165A90
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (!this.CheckAndSetPosition())
			{
				return 0;
			}
			int num = (int)Math.Min((uint)count, this.reader.BytesLeft);
			this.reader.ReadBytes(buffer, offset, num);
			this.Position += (long)num;
			return num;
		}

		// Token: 0x06004285 RID: 17029 RVA: 0x00165B14 File Offset: 0x00165B14
		public override int ReadByte()
		{
			if (!this.CheckAndSetPosition() || !this.reader.CanRead(1U))
			{
				return -1;
			}
			long num = this.Position;
			this.Position = num + 1L;
			return (int)this.reader.ReadByte();
		}

		// Token: 0x06004286 RID: 17030 RVA: 0x00165B60 File Offset: 0x00165B60
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004287 RID: 17031 RVA: 0x00165B68 File Offset: 0x00165B68
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x04002389 RID: 9097
		private DataReader reader;

		// Token: 0x0400238A RID: 9098
		private long position;
	}
}
