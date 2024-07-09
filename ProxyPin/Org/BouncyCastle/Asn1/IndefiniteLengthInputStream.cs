using System;
using System.IO;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x0200027B RID: 635
	internal class IndefiniteLengthInputStream : LimitedInputStream
	{
		// Token: 0x0600142C RID: 5164 RVA: 0x0006CD54 File Offset: 0x0006CD54
		internal IndefiniteLengthInputStream(Stream inStream, int limit) : base(inStream, limit)
		{
			this._lookAhead = this.RequireByte();
			this.CheckForEof();
		}

		// Token: 0x0600142D RID: 5165 RVA: 0x0006CD78 File Offset: 0x0006CD78
		internal void SetEofOn00(bool eofOn00)
		{
			this._eofOn00 = eofOn00;
			if (this._eofOn00)
			{
				this.CheckForEof();
			}
		}

		// Token: 0x0600142E RID: 5166 RVA: 0x0006CD94 File Offset: 0x0006CD94
		private bool CheckForEof()
		{
			if (this._lookAhead != 0)
			{
				return this._lookAhead < 0;
			}
			int num = this.RequireByte();
			if (num != 0)
			{
				throw new IOException("malformed end-of-contents marker");
			}
			this._lookAhead = -1;
			this.SetParentEofDetect(true);
			return true;
		}

		// Token: 0x0600142F RID: 5167 RVA: 0x0006CDE4 File Offset: 0x0006CDE4
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (this._eofOn00 || count <= 1)
			{
				return base.Read(buffer, offset, count);
			}
			if (this._lookAhead < 0)
			{
				return 0;
			}
			int num = this._in.Read(buffer, offset + 1, count - 1);
			if (num <= 0)
			{
				throw new EndOfStreamException();
			}
			buffer[offset] = (byte)this._lookAhead;
			this._lookAhead = this.RequireByte();
			return num + 1;
		}

		// Token: 0x06001430 RID: 5168 RVA: 0x0006CE58 File Offset: 0x0006CE58
		public override int ReadByte()
		{
			if (this._eofOn00 && this.CheckForEof())
			{
				return -1;
			}
			int lookAhead = this._lookAhead;
			this._lookAhead = this.RequireByte();
			return lookAhead;
		}

		// Token: 0x06001431 RID: 5169 RVA: 0x0006CE98 File Offset: 0x0006CE98
		private int RequireByte()
		{
			int num = this._in.ReadByte();
			if (num < 0)
			{
				throw new EndOfStreamException();
			}
			return num;
		}

		// Token: 0x04000DC4 RID: 3524
		private int _lookAhead;

		// Token: 0x04000DC5 RID: 3525
		private bool _eofOn00 = true;
	}
}
