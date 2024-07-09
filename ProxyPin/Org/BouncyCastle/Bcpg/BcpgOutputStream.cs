using System;
using System.IO;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.IO;

namespace Org.BouncyCastle.Bcpg
{
	// Token: 0x02000299 RID: 665
	public class BcpgOutputStream : BaseOutputStream
	{
		// Token: 0x060014CC RID: 5324 RVA: 0x0006F5E0 File Offset: 0x0006F5E0
		internal static BcpgOutputStream Wrap(Stream outStr)
		{
			if (outStr is BcpgOutputStream)
			{
				return (BcpgOutputStream)outStr;
			}
			return new BcpgOutputStream(outStr);
		}

		// Token: 0x060014CD RID: 5325 RVA: 0x0006F5FC File Offset: 0x0006F5FC
		public BcpgOutputStream(Stream outStr)
		{
			if (outStr == null)
			{
				throw new ArgumentNullException("outStr");
			}
			this.outStr = outStr;
		}

		// Token: 0x060014CE RID: 5326 RVA: 0x0006F61C File Offset: 0x0006F61C
		public BcpgOutputStream(Stream outStr, PacketTag tag)
		{
			if (outStr == null)
			{
				throw new ArgumentNullException("outStr");
			}
			this.outStr = outStr;
			this.WriteHeader(tag, true, true, 0L);
		}

		// Token: 0x060014CF RID: 5327 RVA: 0x0006F648 File Offset: 0x0006F648
		public BcpgOutputStream(Stream outStr, PacketTag tag, long length, bool oldFormat)
		{
			if (outStr == null)
			{
				throw new ArgumentNullException("outStr");
			}
			this.outStr = outStr;
			if (length > (long)((ulong)-1))
			{
				this.WriteHeader(tag, false, true, 0L);
				this.partialBufferLength = 65536;
				this.partialBuffer = new byte[this.partialBufferLength];
				this.partialPower = 16;
				this.partialOffset = 0;
				return;
			}
			this.WriteHeader(tag, oldFormat, false, length);
		}

		// Token: 0x060014D0 RID: 5328 RVA: 0x0006F6C4 File Offset: 0x0006F6C4
		public BcpgOutputStream(Stream outStr, PacketTag tag, long length)
		{
			if (outStr == null)
			{
				throw new ArgumentNullException("outStr");
			}
			this.outStr = outStr;
			this.WriteHeader(tag, false, false, length);
		}

		// Token: 0x060014D1 RID: 5329 RVA: 0x0006F6F0 File Offset: 0x0006F6F0
		public BcpgOutputStream(Stream outStr, PacketTag tag, byte[] buffer)
		{
			if (outStr == null)
			{
				throw new ArgumentNullException("outStr");
			}
			this.outStr = outStr;
			this.WriteHeader(tag, false, true, 0L);
			this.partialBuffer = buffer;
			uint num = (uint)this.partialBuffer.Length;
			this.partialPower = 0;
			while (num != 1U)
			{
				num >>= 1;
				this.partialPower++;
			}
			if (this.partialPower > 30)
			{
				throw new IOException("Buffer cannot be greater than 2^30 in length.");
			}
			this.partialBufferLength = 1 << this.partialPower;
			this.partialOffset = 0;
		}

		// Token: 0x060014D2 RID: 5330 RVA: 0x0006F78C File Offset: 0x0006F78C
		private void WriteNewPacketLength(long bodyLen)
		{
			if (bodyLen < 192L)
			{
				this.outStr.WriteByte((byte)bodyLen);
				return;
			}
			if (bodyLen <= 8383L)
			{
				bodyLen -= 192L;
				this.outStr.WriteByte((byte)((bodyLen >> 8 & 255L) + 192L));
				this.outStr.WriteByte((byte)bodyLen);
				return;
			}
			this.outStr.WriteByte(byte.MaxValue);
			this.outStr.WriteByte((byte)(bodyLen >> 24));
			this.outStr.WriteByte((byte)(bodyLen >> 16));
			this.outStr.WriteByte((byte)(bodyLen >> 8));
			this.outStr.WriteByte((byte)bodyLen);
		}

		// Token: 0x060014D3 RID: 5331 RVA: 0x0006F844 File Offset: 0x0006F844
		private void WriteHeader(PacketTag tag, bool oldPackets, bool partial, long bodyLen)
		{
			int num = 128;
			if (this.partialBuffer != null)
			{
				this.PartialFlush(true);
				this.partialBuffer = null;
			}
			if (oldPackets)
			{
				num |= (int)((int)tag << 2);
				if (partial)
				{
					this.WriteByte((byte)(num | 3));
					return;
				}
				if (bodyLen <= 255L)
				{
					this.WriteByte((byte)num);
					this.WriteByte((byte)bodyLen);
					return;
				}
				if (bodyLen <= 65535L)
				{
					this.WriteByte((byte)(num | 1));
					this.WriteByte((byte)(bodyLen >> 8));
					this.WriteByte((byte)bodyLen);
					return;
				}
				this.WriteByte((byte)(num | 2));
				this.WriteByte((byte)(bodyLen >> 24));
				this.WriteByte((byte)(bodyLen >> 16));
				this.WriteByte((byte)(bodyLen >> 8));
				this.WriteByte((byte)bodyLen);
				return;
			}
			else
			{
				num |= (int)((PacketTag)64 | tag);
				this.WriteByte((byte)num);
				if (partial)
				{
					this.partialOffset = 0;
					return;
				}
				this.WriteNewPacketLength(bodyLen);
				return;
			}
		}

		// Token: 0x060014D4 RID: 5332 RVA: 0x0006F938 File Offset: 0x0006F938
		private void PartialFlush(bool isLast)
		{
			if (isLast)
			{
				this.WriteNewPacketLength((long)this.partialOffset);
				this.outStr.Write(this.partialBuffer, 0, this.partialOffset);
			}
			else
			{
				this.outStr.WriteByte((byte)(224 | this.partialPower));
				this.outStr.Write(this.partialBuffer, 0, this.partialBufferLength);
			}
			this.partialOffset = 0;
		}

		// Token: 0x060014D5 RID: 5333 RVA: 0x0006F9B0 File Offset: 0x0006F9B0
		private void WritePartial(byte b)
		{
			if (this.partialOffset == this.partialBufferLength)
			{
				this.PartialFlush(false);
			}
			this.partialBuffer[this.partialOffset++] = b;
		}

		// Token: 0x060014D6 RID: 5334 RVA: 0x0006F9F4 File Offset: 0x0006F9F4
		private void WritePartial(byte[] buffer, int off, int len)
		{
			if (this.partialOffset == this.partialBufferLength)
			{
				this.PartialFlush(false);
			}
			if (len <= this.partialBufferLength - this.partialOffset)
			{
				Array.Copy(buffer, off, this.partialBuffer, this.partialOffset, len);
				this.partialOffset += len;
				return;
			}
			int num = this.partialBufferLength - this.partialOffset;
			Array.Copy(buffer, off, this.partialBuffer, this.partialOffset, num);
			off += num;
			len -= num;
			this.PartialFlush(false);
			while (len > this.partialBufferLength)
			{
				Array.Copy(buffer, off, this.partialBuffer, 0, this.partialBufferLength);
				off += this.partialBufferLength;
				len -= this.partialBufferLength;
				this.PartialFlush(false);
			}
			Array.Copy(buffer, off, this.partialBuffer, 0, len);
			this.partialOffset += len;
		}

		// Token: 0x060014D7 RID: 5335 RVA: 0x0006FAE0 File Offset: 0x0006FAE0
		public override void WriteByte(byte value)
		{
			if (this.partialBuffer != null)
			{
				this.WritePartial(value);
				return;
			}
			this.outStr.WriteByte(value);
		}

		// Token: 0x060014D8 RID: 5336 RVA: 0x0006FB04 File Offset: 0x0006FB04
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (this.partialBuffer != null)
			{
				this.WritePartial(buffer, offset, count);
				return;
			}
			this.outStr.Write(buffer, offset, count);
		}

		// Token: 0x060014D9 RID: 5337 RVA: 0x0006FB2C File Offset: 0x0006FB2C
		internal virtual void WriteShort(short n)
		{
			this.Write(new byte[]
			{
				(byte)(n >> 8),
				(byte)n
			});
		}

		// Token: 0x060014DA RID: 5338 RVA: 0x0006FB58 File Offset: 0x0006FB58
		internal virtual void WriteInt(int n)
		{
			this.Write(new byte[]
			{
				(byte)(n >> 24),
				(byte)(n >> 16),
				(byte)(n >> 8),
				(byte)n
			});
		}

		// Token: 0x060014DB RID: 5339 RVA: 0x0006FB94 File Offset: 0x0006FB94
		internal virtual void WriteLong(long n)
		{
			this.Write(new byte[]
			{
				(byte)(n >> 56),
				(byte)(n >> 48),
				(byte)(n >> 40),
				(byte)(n >> 32),
				(byte)(n >> 24),
				(byte)(n >> 16),
				(byte)(n >> 8),
				(byte)n
			});
		}

		// Token: 0x060014DC RID: 5340 RVA: 0x0006FBF0 File Offset: 0x0006FBF0
		public void WritePacket(ContainedPacket p)
		{
			p.Encode(this);
		}

		// Token: 0x060014DD RID: 5341 RVA: 0x0006FBFC File Offset: 0x0006FBFC
		internal void WritePacket(PacketTag tag, byte[] body, bool oldFormat)
		{
			this.WriteHeader(tag, oldFormat, false, (long)body.Length);
			this.Write(body);
		}

		// Token: 0x060014DE RID: 5342 RVA: 0x0006FC14 File Offset: 0x0006FC14
		public void WriteObject(BcpgObject bcpgObject)
		{
			bcpgObject.Encode(this);
		}

		// Token: 0x060014DF RID: 5343 RVA: 0x0006FC20 File Offset: 0x0006FC20
		public void WriteObjects(params BcpgObject[] v)
		{
			foreach (BcpgObject bcpgObject in v)
			{
				bcpgObject.Encode(this);
			}
		}

		// Token: 0x060014E0 RID: 5344 RVA: 0x0006FC54 File Offset: 0x0006FC54
		public override void Flush()
		{
			this.outStr.Flush();
		}

		// Token: 0x060014E1 RID: 5345 RVA: 0x0006FC64 File Offset: 0x0006FC64
		public void Finish()
		{
			if (this.partialBuffer != null)
			{
				this.PartialFlush(true);
				Array.Clear(this.partialBuffer, 0, this.partialBuffer.Length);
				this.partialBuffer = null;
			}
		}

		// Token: 0x060014E2 RID: 5346 RVA: 0x0006FC94 File Offset: 0x0006FC94
		public override void Close()
		{
			this.Finish();
			this.outStr.Flush();
			Platform.Dispose(this.outStr);
			base.Close();
		}

		// Token: 0x04000E0F RID: 3599
		private const int BufferSizePower = 16;

		// Token: 0x04000E10 RID: 3600
		private Stream outStr;

		// Token: 0x04000E11 RID: 3601
		private byte[] partialBuffer;

		// Token: 0x04000E12 RID: 3602
		private int partialBufferLength;

		// Token: 0x04000E13 RID: 3603
		private int partialPower;

		// Token: 0x04000E14 RID: 3604
		private int partialOffset;
	}
}
