using System;
using System.IO;
using Org.BouncyCastle.Apache.Bzip2;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Zlib;

namespace Org.BouncyCastle.Bcpg.OpenPgp
{
	// Token: 0x02000647 RID: 1607
	public class PgpCompressedDataGenerator : IStreamGenerator
	{
		// Token: 0x060037C9 RID: 14281 RVA: 0x0012AD30 File Offset: 0x0012AD30
		public PgpCompressedDataGenerator(CompressionAlgorithmTag algorithm) : this(algorithm, -1)
		{
		}

		// Token: 0x060037CA RID: 14282 RVA: 0x0012AD3C File Offset: 0x0012AD3C
		public PgpCompressedDataGenerator(CompressionAlgorithmTag algorithm, int compression)
		{
			switch (algorithm)
			{
			case CompressionAlgorithmTag.Uncompressed:
			case CompressionAlgorithmTag.Zip:
			case CompressionAlgorithmTag.ZLib:
			case CompressionAlgorithmTag.BZip2:
				if (compression != -1 && (compression < 0 || compression > 9))
				{
					throw new ArgumentException("unknown compression level: " + compression);
				}
				this.algorithm = algorithm;
				this.compression = compression;
				return;
			default:
				throw new ArgumentException("unknown compression algorithm", "algorithm");
			}
		}

		// Token: 0x060037CB RID: 14283 RVA: 0x0012ADB8 File Offset: 0x0012ADB8
		public Stream Open(Stream outStr)
		{
			if (this.dOut != null)
			{
				throw new InvalidOperationException("generator already in open state");
			}
			if (outStr == null)
			{
				throw new ArgumentNullException("outStr");
			}
			this.pkOut = new BcpgOutputStream(outStr, PacketTag.CompressedData);
			this.doOpen();
			return new WrappedGeneratorStream(this, this.dOut);
		}

		// Token: 0x060037CC RID: 14284 RVA: 0x0012AE10 File Offset: 0x0012AE10
		public Stream Open(Stream outStr, byte[] buffer)
		{
			if (this.dOut != null)
			{
				throw new InvalidOperationException("generator already in open state");
			}
			if (outStr == null)
			{
				throw new ArgumentNullException("outStr");
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			this.pkOut = new BcpgOutputStream(outStr, PacketTag.CompressedData, buffer);
			this.doOpen();
			return new WrappedGeneratorStream(this, this.dOut);
		}

		// Token: 0x060037CD RID: 14285 RVA: 0x0012AE7C File Offset: 0x0012AE7C
		private void doOpen()
		{
			this.pkOut.WriteByte((byte)this.algorithm);
			switch (this.algorithm)
			{
			case CompressionAlgorithmTag.Uncompressed:
				this.dOut = this.pkOut;
				return;
			case CompressionAlgorithmTag.Zip:
				this.dOut = new PgpCompressedDataGenerator.SafeZOutputStream(this.pkOut, this.compression, true);
				return;
			case CompressionAlgorithmTag.ZLib:
				this.dOut = new PgpCompressedDataGenerator.SafeZOutputStream(this.pkOut, this.compression, false);
				return;
			case CompressionAlgorithmTag.BZip2:
				this.dOut = new PgpCompressedDataGenerator.SafeCBZip2OutputStream(this.pkOut);
				return;
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x060037CE RID: 14286 RVA: 0x0012AF18 File Offset: 0x0012AF18
		public void Close()
		{
			if (this.dOut != null)
			{
				if (this.dOut != this.pkOut)
				{
					Platform.Dispose(this.dOut);
				}
				this.dOut = null;
				this.pkOut.Finish();
				this.pkOut.Flush();
				this.pkOut = null;
			}
		}

		// Token: 0x04001D71 RID: 7537
		private readonly CompressionAlgorithmTag algorithm;

		// Token: 0x04001D72 RID: 7538
		private readonly int compression;

		// Token: 0x04001D73 RID: 7539
		private Stream dOut;

		// Token: 0x04001D74 RID: 7540
		private BcpgOutputStream pkOut;

		// Token: 0x02000E5D RID: 3677
		private class SafeCBZip2OutputStream : CBZip2OutputStream
		{
			// Token: 0x06008D48 RID: 36168 RVA: 0x002A6070 File Offset: 0x002A6070
			public SafeCBZip2OutputStream(Stream output) : base(output)
			{
			}

			// Token: 0x06008D49 RID: 36169 RVA: 0x002A607C File Offset: 0x002A607C
			public override void Close()
			{
				base.Finish();
			}
		}

		// Token: 0x02000E5E RID: 3678
		private class SafeZOutputStream : ZOutputStream
		{
			// Token: 0x06008D4A RID: 36170 RVA: 0x002A6084 File Offset: 0x002A6084
			public SafeZOutputStream(Stream output, int level, bool nowrap) : base(output, level, nowrap)
			{
			}

			// Token: 0x06008D4B RID: 36171 RVA: 0x002A6090 File Offset: 0x002A6090
			public override void Close()
			{
				this.Finish();
				this.End();
			}
		}
	}
}
