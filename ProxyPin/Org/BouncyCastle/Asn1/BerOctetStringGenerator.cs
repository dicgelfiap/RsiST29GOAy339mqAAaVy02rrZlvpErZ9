using System;
using System.IO;
using Org.BouncyCastle.Utilities.IO;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x02000253 RID: 595
	public class BerOctetStringGenerator : BerGenerator
	{
		// Token: 0x06001312 RID: 4882 RVA: 0x00069EB4 File Offset: 0x00069EB4
		public BerOctetStringGenerator(Stream outStream) : base(outStream)
		{
			base.WriteBerHeader(36);
		}

		// Token: 0x06001313 RID: 4883 RVA: 0x00069EC8 File Offset: 0x00069EC8
		public BerOctetStringGenerator(Stream outStream, int tagNo, bool isExplicit) : base(outStream, tagNo, isExplicit)
		{
			base.WriteBerHeader(36);
		}

		// Token: 0x06001314 RID: 4884 RVA: 0x00069EDC File Offset: 0x00069EDC
		public Stream GetOctetOutputStream()
		{
			return this.GetOctetOutputStream(new byte[1000]);
		}

		// Token: 0x06001315 RID: 4885 RVA: 0x00069EF0 File Offset: 0x00069EF0
		public Stream GetOctetOutputStream(int bufSize)
		{
			if (bufSize >= 1)
			{
				return this.GetOctetOutputStream(new byte[bufSize]);
			}
			return this.GetOctetOutputStream();
		}

		// Token: 0x06001316 RID: 4886 RVA: 0x00069F0C File Offset: 0x00069F0C
		public Stream GetOctetOutputStream(byte[] buf)
		{
			return new BerOctetStringGenerator.BufferedBerOctetStream(this, buf);
		}

		// Token: 0x02000DD6 RID: 3542
		private class BufferedBerOctetStream : BaseOutputStream
		{
			// Token: 0x06008B60 RID: 35680 RVA: 0x0029D5BC File Offset: 0x0029D5BC
			internal BufferedBerOctetStream(BerOctetStringGenerator gen, byte[] buf)
			{
				this._gen = gen;
				this._buf = buf;
				this._off = 0;
				this._derOut = new DerOutputStream(this._gen.Out);
			}

			// Token: 0x06008B61 RID: 35681 RVA: 0x0029D5F0 File Offset: 0x0029D5F0
			public override void WriteByte(byte b)
			{
				this._buf[this._off++] = b;
				if (this._off == this._buf.Length)
				{
					DerOctetString.Encode(this._derOut, this._buf, 0, this._off);
					this._off = 0;
				}
			}

			// Token: 0x06008B62 RID: 35682 RVA: 0x0029D64C File Offset: 0x0029D64C
			public override void Write(byte[] buf, int offset, int len)
			{
				while (len > 0)
				{
					int num = Math.Min(len, this._buf.Length - this._off);
					if (num == this._buf.Length)
					{
						DerOctetString.Encode(this._derOut, buf, offset, num);
					}
					else
					{
						Array.Copy(buf, offset, this._buf, this._off, num);
						this._off += num;
						if (this._off < this._buf.Length)
						{
							return;
						}
						DerOctetString.Encode(this._derOut, this._buf, 0, this._off);
						this._off = 0;
					}
					offset += num;
					len -= num;
				}
			}

			// Token: 0x06008B63 RID: 35683 RVA: 0x0029D700 File Offset: 0x0029D700
			public override void Close()
			{
				if (this._off != 0)
				{
					DerOctetString.Encode(this._derOut, this._buf, 0, this._off);
				}
				this._gen.WriteBerEnd();
				base.Close();
			}

			// Token: 0x04004062 RID: 16482
			private byte[] _buf;

			// Token: 0x04004063 RID: 16483
			private int _off;

			// Token: 0x04004064 RID: 16484
			private readonly BerOctetStringGenerator _gen;

			// Token: 0x04004065 RID: 16485
			private readonly DerOutputStream _derOut;
		}
	}
}
