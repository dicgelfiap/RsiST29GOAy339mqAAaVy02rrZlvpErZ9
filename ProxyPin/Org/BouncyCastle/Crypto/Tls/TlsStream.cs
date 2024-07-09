using System;
using System.IO;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000551 RID: 1361
	internal class TlsStream : Stream
	{
		// Token: 0x060029C5 RID: 10693 RVA: 0x000E01E0 File Offset: 0x000E01E0
		internal TlsStream(TlsProtocol handler)
		{
			this.handler = handler;
		}

		// Token: 0x170007A6 RID: 1958
		// (get) Token: 0x060029C6 RID: 10694 RVA: 0x000E01F0 File Offset: 0x000E01F0
		public override bool CanRead
		{
			get
			{
				return !this.handler.IsClosed;
			}
		}

		// Token: 0x170007A7 RID: 1959
		// (get) Token: 0x060029C7 RID: 10695 RVA: 0x000E0200 File Offset: 0x000E0200
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170007A8 RID: 1960
		// (get) Token: 0x060029C8 RID: 10696 RVA: 0x000E0204 File Offset: 0x000E0204
		public override bool CanWrite
		{
			get
			{
				return !this.handler.IsClosed;
			}
		}

		// Token: 0x060029C9 RID: 10697 RVA: 0x000E0214 File Offset: 0x000E0214
		public override void Close()
		{
			this.handler.Close();
			base.Close();
		}

		// Token: 0x060029CA RID: 10698 RVA: 0x000E0228 File Offset: 0x000E0228
		public override void Flush()
		{
			this.handler.Flush();
		}

		// Token: 0x170007A9 RID: 1961
		// (get) Token: 0x060029CB RID: 10699 RVA: 0x000E0238 File Offset: 0x000E0238
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170007AA RID: 1962
		// (get) Token: 0x060029CC RID: 10700 RVA: 0x000E0240 File Offset: 0x000E0240
		// (set) Token: 0x060029CD RID: 10701 RVA: 0x000E0248 File Offset: 0x000E0248
		public override long Position
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

		// Token: 0x060029CE RID: 10702 RVA: 0x000E0250 File Offset: 0x000E0250
		public override int Read(byte[] buf, int off, int len)
		{
			return this.handler.ReadApplicationData(buf, off, len);
		}

		// Token: 0x060029CF RID: 10703 RVA: 0x000E0260 File Offset: 0x000E0260
		public override int ReadByte()
		{
			byte[] array = new byte[1];
			if (this.Read(array, 0, 1) <= 0)
			{
				return -1;
			}
			return (int)array[0];
		}

		// Token: 0x060029D0 RID: 10704 RVA: 0x000E028C File Offset: 0x000E028C
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060029D1 RID: 10705 RVA: 0x000E0294 File Offset: 0x000E0294
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060029D2 RID: 10706 RVA: 0x000E029C File Offset: 0x000E029C
		public override void Write(byte[] buf, int off, int len)
		{
			this.handler.WriteData(buf, off, len);
		}

		// Token: 0x060029D3 RID: 10707 RVA: 0x000E02AC File Offset: 0x000E02AC
		public override void WriteByte(byte b)
		{
			this.handler.WriteData(new byte[]
			{
				b
			}, 0, 1);
		}

		// Token: 0x04001B1B RID: 6939
		private readonly TlsProtocol handler;
	}
}
