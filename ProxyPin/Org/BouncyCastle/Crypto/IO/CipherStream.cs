using System;
using System.IO;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.IO
{
	// Token: 0x020003DA RID: 986
	public class CipherStream : Stream
	{
		// Token: 0x06001F18 RID: 7960 RVA: 0x000B66DC File Offset: 0x000B66DC
		public CipherStream(Stream stream, IBufferedCipher readCipher, IBufferedCipher writeCipher)
		{
			this.stream = stream;
			if (readCipher != null)
			{
				this.inCipher = readCipher;
				this.mInBuf = null;
			}
			if (writeCipher != null)
			{
				this.outCipher = writeCipher;
			}
		}

		// Token: 0x17000602 RID: 1538
		// (get) Token: 0x06001F19 RID: 7961 RVA: 0x000B670C File Offset: 0x000B670C
		public IBufferedCipher ReadCipher
		{
			get
			{
				return this.inCipher;
			}
		}

		// Token: 0x17000603 RID: 1539
		// (get) Token: 0x06001F1A RID: 7962 RVA: 0x000B6714 File Offset: 0x000B6714
		public IBufferedCipher WriteCipher
		{
			get
			{
				return this.outCipher;
			}
		}

		// Token: 0x06001F1B RID: 7963 RVA: 0x000B671C File Offset: 0x000B671C
		public override int ReadByte()
		{
			if (this.inCipher == null)
			{
				return this.stream.ReadByte();
			}
			if ((this.mInBuf == null || this.mInPos >= this.mInBuf.Length) && !this.FillInBuf())
			{
				return -1;
			}
			return (int)this.mInBuf[this.mInPos++];
		}

		// Token: 0x06001F1C RID: 7964 RVA: 0x000B6788 File Offset: 0x000B6788
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (this.inCipher == null)
			{
				return this.stream.Read(buffer, offset, count);
			}
			int num = 0;
			while (num < count && ((this.mInBuf != null && this.mInPos < this.mInBuf.Length) || this.FillInBuf()))
			{
				int num2 = Math.Min(count - num, this.mInBuf.Length - this.mInPos);
				Array.Copy(this.mInBuf, this.mInPos, buffer, offset + num, num2);
				this.mInPos += num2;
				num += num2;
			}
			return num;
		}

		// Token: 0x06001F1D RID: 7965 RVA: 0x000B6828 File Offset: 0x000B6828
		private bool FillInBuf()
		{
			if (this.inStreamEnded)
			{
				return false;
			}
			this.mInPos = 0;
			do
			{
				this.mInBuf = this.ReadAndProcessBlock();
			}
			while (!this.inStreamEnded && this.mInBuf == null);
			return this.mInBuf != null;
		}

		// Token: 0x06001F1E RID: 7966 RVA: 0x000B6878 File Offset: 0x000B6878
		private byte[] ReadAndProcessBlock()
		{
			int blockSize = this.inCipher.GetBlockSize();
			int num = (blockSize == 0) ? 256 : blockSize;
			byte[] array = new byte[num];
			int num2 = 0;
			for (;;)
			{
				int num3 = this.stream.Read(array, num2, array.Length - num2);
				if (num3 < 1)
				{
					break;
				}
				num2 += num3;
				if (num2 >= array.Length)
				{
					goto IL_5A;
				}
			}
			this.inStreamEnded = true;
			IL_5A:
			byte[] array2 = this.inStreamEnded ? this.inCipher.DoFinal(array, 0, num2) : this.inCipher.ProcessBytes(array);
			if (array2 != null && array2.Length == 0)
			{
				array2 = null;
			}
			return array2;
		}

		// Token: 0x06001F1F RID: 7967 RVA: 0x000B6924 File Offset: 0x000B6924
		public override void Write(byte[] buffer, int offset, int count)
		{
			int num = offset + count;
			if (this.outCipher == null)
			{
				this.stream.Write(buffer, offset, count);
				return;
			}
			byte[] array = this.outCipher.ProcessBytes(buffer, offset, count);
			if (array != null)
			{
				this.stream.Write(array, 0, array.Length);
			}
		}

		// Token: 0x06001F20 RID: 7968 RVA: 0x000B6978 File Offset: 0x000B6978
		public override void WriteByte(byte b)
		{
			if (this.outCipher == null)
			{
				this.stream.WriteByte(b);
				return;
			}
			byte[] array = this.outCipher.ProcessByte(b);
			if (array != null)
			{
				this.stream.Write(array, 0, array.Length);
			}
		}

		// Token: 0x17000604 RID: 1540
		// (get) Token: 0x06001F21 RID: 7969 RVA: 0x000B69C4 File Offset: 0x000B69C4
		public override bool CanRead
		{
			get
			{
				return this.stream.CanRead && this.inCipher != null;
			}
		}

		// Token: 0x17000605 RID: 1541
		// (get) Token: 0x06001F22 RID: 7970 RVA: 0x000B69E4 File Offset: 0x000B69E4
		public override bool CanWrite
		{
			get
			{
				return this.stream.CanWrite && this.outCipher != null;
			}
		}

		// Token: 0x17000606 RID: 1542
		// (get) Token: 0x06001F23 RID: 7971 RVA: 0x000B6A04 File Offset: 0x000B6A04
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000607 RID: 1543
		// (get) Token: 0x06001F24 RID: 7972 RVA: 0x000B6A08 File Offset: 0x000B6A08
		public sealed override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x06001F25 RID: 7973 RVA: 0x000B6A10 File Offset: 0x000B6A10
		// (set) Token: 0x06001F26 RID: 7974 RVA: 0x000B6A18 File Offset: 0x000B6A18
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

		// Token: 0x06001F27 RID: 7975 RVA: 0x000B6A20 File Offset: 0x000B6A20
		public override void Close()
		{
			if (this.outCipher != null)
			{
				byte[] array = this.outCipher.DoFinal();
				this.stream.Write(array, 0, array.Length);
				this.stream.Flush();
			}
			Platform.Dispose(this.stream);
			base.Close();
		}

		// Token: 0x06001F28 RID: 7976 RVA: 0x000B6A74 File Offset: 0x000B6A74
		public override void Flush()
		{
			this.stream.Flush();
		}

		// Token: 0x06001F29 RID: 7977 RVA: 0x000B6A84 File Offset: 0x000B6A84
		public sealed override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001F2A RID: 7978 RVA: 0x000B6A8C File Offset: 0x000B6A8C
		public sealed override void SetLength(long length)
		{
			throw new NotSupportedException();
		}

		// Token: 0x04001481 RID: 5249
		internal Stream stream;

		// Token: 0x04001482 RID: 5250
		internal IBufferedCipher inCipher;

		// Token: 0x04001483 RID: 5251
		internal IBufferedCipher outCipher;

		// Token: 0x04001484 RID: 5252
		private byte[] mInBuf;

		// Token: 0x04001485 RID: 5253
		private int mInPos;

		// Token: 0x04001486 RID: 5254
		private bool inStreamEnded;
	}
}
