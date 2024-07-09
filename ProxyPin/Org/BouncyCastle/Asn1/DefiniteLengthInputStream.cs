using System;
using System.IO;
using Org.BouncyCastle.Utilities.IO;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x02000265 RID: 613
	internal class DefiniteLengthInputStream : LimitedInputStream
	{
		// Token: 0x06001376 RID: 4982 RVA: 0x0006AAEC File Offset: 0x0006AAEC
		internal DefiniteLengthInputStream(Stream inStream, int length, int limit) : base(inStream, limit)
		{
			if (length < 0)
			{
				throw new ArgumentException("negative lengths not allowed", "length");
			}
			this._originalLength = length;
			this._remaining = length;
			if (length == 0)
			{
				this.SetParentEofDetect(true);
			}
		}

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x06001377 RID: 4983 RVA: 0x0006AB28 File Offset: 0x0006AB28
		internal int Remaining
		{
			get
			{
				return this._remaining;
			}
		}

		// Token: 0x06001378 RID: 4984 RVA: 0x0006AB30 File Offset: 0x0006AB30
		public override int ReadByte()
		{
			if (this._remaining == 0)
			{
				return -1;
			}
			int num = this._in.ReadByte();
			if (num < 0)
			{
				throw new EndOfStreamException(string.Concat(new object[]
				{
					"DEF length ",
					this._originalLength,
					" object truncated by ",
					this._remaining
				}));
			}
			if (--this._remaining == 0)
			{
				this.SetParentEofDetect(true);
			}
			return num;
		}

		// Token: 0x06001379 RID: 4985 RVA: 0x0006ABC0 File Offset: 0x0006ABC0
		public override int Read(byte[] buf, int off, int len)
		{
			if (this._remaining == 0)
			{
				return 0;
			}
			int count = Math.Min(len, this._remaining);
			int num = this._in.Read(buf, off, count);
			if (num < 1)
			{
				throw new EndOfStreamException(string.Concat(new object[]
				{
					"DEF length ",
					this._originalLength,
					" object truncated by ",
					this._remaining
				}));
			}
			if ((this._remaining -= num) == 0)
			{
				this.SetParentEofDetect(true);
			}
			return num;
		}

		// Token: 0x0600137A RID: 4986 RVA: 0x0006AC60 File Offset: 0x0006AC60
		internal void ReadAllIntoByteArray(byte[] buf)
		{
			if (this._remaining != buf.Length)
			{
				throw new ArgumentException("buffer length not right for data");
			}
			if (this._remaining == 0)
			{
				return;
			}
			int limit = this.Limit;
			if (this._remaining >= limit)
			{
				throw new IOException(string.Concat(new object[]
				{
					"corrupted stream - out of bounds length found: ",
					this._remaining,
					" >= ",
					limit
				}));
			}
			if ((this._remaining -= Streams.ReadFully(this._in, buf)) != 0)
			{
				throw new EndOfStreamException(string.Concat(new object[]
				{
					"DEF length ",
					this._originalLength,
					" object truncated by ",
					this._remaining
				}));
			}
			this.SetParentEofDetect(true);
		}

		// Token: 0x0600137B RID: 4987 RVA: 0x0006AD4C File Offset: 0x0006AD4C
		internal byte[] ToArray()
		{
			if (this._remaining == 0)
			{
				return DefiniteLengthInputStream.EmptyBytes;
			}
			int limit = this.Limit;
			if (this._remaining >= limit)
			{
				throw new IOException(string.Concat(new object[]
				{
					"corrupted stream - out of bounds length found: ",
					this._remaining,
					" >= ",
					limit
				}));
			}
			byte[] array = new byte[this._remaining];
			if ((this._remaining -= Streams.ReadFully(this._in, array)) != 0)
			{
				throw new EndOfStreamException(string.Concat(new object[]
				{
					"DEF length ",
					this._originalLength,
					" object truncated by ",
					this._remaining
				}));
			}
			this.SetParentEofDetect(true);
			return array;
		}

		// Token: 0x04000DA3 RID: 3491
		private static readonly byte[] EmptyBytes = new byte[0];

		// Token: 0x04000DA4 RID: 3492
		private readonly int _originalLength;

		// Token: 0x04000DA5 RID: 3493
		private int _remaining;
	}
}
