using System;
using System.IO;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.IO;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x020002FA RID: 762
	public class CmsTypedStream
	{
		// Token: 0x06001719 RID: 5913 RVA: 0x00079150 File Offset: 0x00079150
		public CmsTypedStream(Stream inStream) : this(PkcsObjectIdentifiers.Data.Id, inStream, 32768)
		{
		}

		// Token: 0x0600171A RID: 5914 RVA: 0x00079168 File Offset: 0x00079168
		public CmsTypedStream(string oid, Stream inStream) : this(oid, inStream, 32768)
		{
		}

		// Token: 0x0600171B RID: 5915 RVA: 0x00079178 File Offset: 0x00079178
		public CmsTypedStream(string oid, Stream inStream, int bufSize)
		{
			this._oid = oid;
			this._in = new CmsTypedStream.FullReaderStream(new BufferedStream(inStream, bufSize));
		}

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x0600171C RID: 5916 RVA: 0x0007919C File Offset: 0x0007919C
		public string ContentType
		{
			get
			{
				return this._oid;
			}
		}

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x0600171D RID: 5917 RVA: 0x000791A4 File Offset: 0x000791A4
		public Stream ContentStream
		{
			get
			{
				return this._in;
			}
		}

		// Token: 0x0600171E RID: 5918 RVA: 0x000791AC File Offset: 0x000791AC
		public void Drain()
		{
			Streams.Drain(this._in);
			Platform.Dispose(this._in);
		}

		// Token: 0x04000F9B RID: 3995
		private const int BufferSize = 32768;

		// Token: 0x04000F9C RID: 3996
		private readonly string _oid;

		// Token: 0x04000F9D RID: 3997
		private readonly Stream _in;

		// Token: 0x02000DE3 RID: 3555
		private class FullReaderStream : FilterStream
		{
			// Token: 0x06008B90 RID: 35728 RVA: 0x0029E7CC File Offset: 0x0029E7CC
			internal FullReaderStream(Stream input) : base(input)
			{
			}

			// Token: 0x06008B91 RID: 35729 RVA: 0x0029E7D8 File Offset: 0x0029E7D8
			public override int Read(byte[] buf, int off, int len)
			{
				return Streams.ReadFully(this.s, buf, off, len);
			}
		}
	}
}
