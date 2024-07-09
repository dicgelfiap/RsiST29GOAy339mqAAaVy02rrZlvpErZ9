using System;
using System.IO;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x020004CE RID: 1230
	public class ByteQueue
	{
		// Token: 0x060025FC RID: 9724 RVA: 0x000CF8D4 File Offset: 0x000CF8D4
		public static int NextTwoPow(int i)
		{
			i |= i >> 1;
			i |= i >> 2;
			i |= i >> 4;
			i |= i >> 8;
			i |= i >> 16;
			return i + 1;
		}

		// Token: 0x060025FD RID: 9725 RVA: 0x000CF900 File Offset: 0x000CF900
		public ByteQueue() : this(1024)
		{
		}

		// Token: 0x060025FE RID: 9726 RVA: 0x000CF910 File Offset: 0x000CF910
		public ByteQueue(int capacity)
		{
			this.skipped = 0;
			this.available = 0;
			this.readOnlyBuf = false;
			base..ctor();
			this.databuf = ((capacity == 0) ? TlsUtilities.EmptyBytes : new byte[capacity]);
		}

		// Token: 0x060025FF RID: 9727 RVA: 0x000CF94C File Offset: 0x000CF94C
		public ByteQueue(byte[] buf, int off, int len)
		{
			this.skipped = 0;
			this.available = 0;
			this.readOnlyBuf = false;
			base..ctor();
			this.databuf = buf;
			this.skipped = off;
			this.available = len;
			this.readOnlyBuf = true;
		}

		// Token: 0x06002600 RID: 9728 RVA: 0x000CF988 File Offset: 0x000CF988
		public void AddData(byte[] data, int offset, int len)
		{
			if (this.readOnlyBuf)
			{
				throw new InvalidOperationException("Cannot add data to read-only buffer");
			}
			if (this.skipped + this.available + len > this.databuf.Length)
			{
				int num = ByteQueue.NextTwoPow(this.available + len);
				if (num > this.databuf.Length)
				{
					byte[] destinationArray = new byte[num];
					Array.Copy(this.databuf, this.skipped, destinationArray, 0, this.available);
					this.databuf = destinationArray;
				}
				else
				{
					Array.Copy(this.databuf, this.skipped, this.databuf, 0, this.available);
				}
				this.skipped = 0;
			}
			Array.Copy(data, offset, this.databuf, this.skipped + this.available, len);
			this.available += len;
		}

		// Token: 0x1700072A RID: 1834
		// (get) Token: 0x06002601 RID: 9729 RVA: 0x000CFA64 File Offset: 0x000CFA64
		public int Available
		{
			get
			{
				return this.available;
			}
		}

		// Token: 0x06002602 RID: 9730 RVA: 0x000CFA6C File Offset: 0x000CFA6C
		public void CopyTo(Stream output, int length)
		{
			if (length > this.available)
			{
				throw new InvalidOperationException(string.Concat(new object[]
				{
					"Cannot copy ",
					length,
					" bytes, only got ",
					this.available
				}));
			}
			output.Write(this.databuf, this.skipped, length);
		}

		// Token: 0x06002603 RID: 9731 RVA: 0x000CFAD8 File Offset: 0x000CFAD8
		public void Read(byte[] buf, int offset, int len, int skip)
		{
			if (buf.Length - offset < len)
			{
				throw new ArgumentException(string.Concat(new object[]
				{
					"Buffer size of ",
					buf.Length,
					" is too small for a read of ",
					len,
					" bytes"
				}));
			}
			if (this.available - skip < len)
			{
				throw new InvalidOperationException("Not enough data to read");
			}
			Array.Copy(this.databuf, this.skipped + skip, buf, offset, len);
		}

		// Token: 0x06002604 RID: 9732 RVA: 0x000CFB64 File Offset: 0x000CFB64
		public MemoryStream ReadFrom(int length)
		{
			if (length > this.available)
			{
				throw new InvalidOperationException(string.Concat(new object[]
				{
					"Cannot read ",
					length,
					" bytes, only got ",
					this.available
				}));
			}
			int index = this.skipped;
			this.available -= length;
			this.skipped += length;
			return new MemoryStream(this.databuf, index, length, false);
		}

		// Token: 0x06002605 RID: 9733 RVA: 0x000CFBEC File Offset: 0x000CFBEC
		public void RemoveData(int i)
		{
			if (i > this.available)
			{
				throw new InvalidOperationException(string.Concat(new object[]
				{
					"Cannot remove ",
					i,
					" bytes, only got ",
					this.available
				}));
			}
			this.available -= i;
			this.skipped += i;
		}

		// Token: 0x06002606 RID: 9734 RVA: 0x000CFC60 File Offset: 0x000CFC60
		public void RemoveData(byte[] buf, int off, int len, int skip)
		{
			this.Read(buf, off, len, skip);
			this.RemoveData(skip + len);
		}

		// Token: 0x06002607 RID: 9735 RVA: 0x000CFC78 File Offset: 0x000CFC78
		public byte[] RemoveData(int len, int skip)
		{
			byte[] array = new byte[len];
			this.RemoveData(array, 0, len, skip);
			return array;
		}

		// Token: 0x06002608 RID: 9736 RVA: 0x000CFC9C File Offset: 0x000CFC9C
		public void Shrink()
		{
			if (this.available == 0)
			{
				this.databuf = TlsUtilities.EmptyBytes;
				this.skipped = 0;
				return;
			}
			int num = ByteQueue.NextTwoPow(this.available);
			if (num < this.databuf.Length)
			{
				byte[] destinationArray = new byte[num];
				Array.Copy(this.databuf, this.skipped, destinationArray, 0, this.available);
				this.databuf = destinationArray;
				this.skipped = 0;
			}
		}

		// Token: 0x040017D2 RID: 6098
		private const int DefaultCapacity = 1024;

		// Token: 0x040017D3 RID: 6099
		private byte[] databuf;

		// Token: 0x040017D4 RID: 6100
		private int skipped;

		// Token: 0x040017D5 RID: 6101
		private int available;

		// Token: 0x040017D6 RID: 6102
		private bool readOnlyBuf;
	}
}
