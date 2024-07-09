using System;
using System.IO;

namespace Org.BouncyCastle.Utilities.IO
{
	// Token: 0x020006E8 RID: 1768
	public class PushbackStream : FilterStream
	{
		// Token: 0x06003DA4 RID: 15780 RVA: 0x00151160 File Offset: 0x00151160
		public PushbackStream(Stream s) : base(s)
		{
		}

		// Token: 0x06003DA5 RID: 15781 RVA: 0x00151170 File Offset: 0x00151170
		public override int ReadByte()
		{
			if (this.buf != -1)
			{
				int result = this.buf;
				this.buf = -1;
				return result;
			}
			return base.ReadByte();
		}

		// Token: 0x06003DA6 RID: 15782 RVA: 0x001511A4 File Offset: 0x001511A4
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (this.buf != -1 && count > 0)
			{
				buffer[offset] = (byte)this.buf;
				this.buf = -1;
				return 1;
			}
			return base.Read(buffer, offset, count);
		}

		// Token: 0x06003DA7 RID: 15783 RVA: 0x001511D8 File Offset: 0x001511D8
		public virtual void Unread(int b)
		{
			if (this.buf != -1)
			{
				throw new InvalidOperationException("Can only push back one byte");
			}
			this.buf = (b & 255);
		}

		// Token: 0x04001EFF RID: 7935
		private int buf = -1;
	}
}
