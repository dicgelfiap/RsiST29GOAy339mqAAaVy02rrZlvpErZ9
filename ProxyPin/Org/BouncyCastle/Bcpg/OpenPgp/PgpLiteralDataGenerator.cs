using System;
using System.IO;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Date;

namespace Org.BouncyCastle.Bcpg.OpenPgp
{
	// Token: 0x02000655 RID: 1621
	public class PgpLiteralDataGenerator : IStreamGenerator
	{
		// Token: 0x06003840 RID: 14400 RVA: 0x0012E5D0 File Offset: 0x0012E5D0
		public PgpLiteralDataGenerator()
		{
		}

		// Token: 0x06003841 RID: 14401 RVA: 0x0012E5D8 File Offset: 0x0012E5D8
		public PgpLiteralDataGenerator(bool oldFormat)
		{
			this.oldFormat = oldFormat;
		}

		// Token: 0x06003842 RID: 14402 RVA: 0x0012E5E8 File Offset: 0x0012E5E8
		private void WriteHeader(BcpgOutputStream outStr, char format, byte[] encName, long modificationTime)
		{
			outStr.Write(new byte[]
			{
				(byte)format,
				(byte)encName.Length
			});
			outStr.Write(encName);
			long num = modificationTime / 1000L;
			outStr.Write(new byte[]
			{
				(byte)(num >> 24),
				(byte)(num >> 16),
				(byte)(num >> 8),
				(byte)num
			});
		}

		// Token: 0x06003843 RID: 14403 RVA: 0x0012E650 File Offset: 0x0012E650
		public Stream Open(Stream outStr, char format, string name, long length, DateTime modificationTime)
		{
			if (this.pkOut != null)
			{
				throw new InvalidOperationException("generator already in open state");
			}
			if (outStr == null)
			{
				throw new ArgumentNullException("outStr");
			}
			long modificationTime2 = DateTimeUtilities.DateTimeToUnixMs(modificationTime);
			byte[] array = Strings.ToUtf8ByteArray(name);
			this.pkOut = new BcpgOutputStream(outStr, PacketTag.LiteralData, length + 2L + (long)array.Length + 4L, this.oldFormat);
			this.WriteHeader(this.pkOut, format, array, modificationTime2);
			return new WrappedGeneratorStream(this, this.pkOut);
		}

		// Token: 0x06003844 RID: 14404 RVA: 0x0012E6D4 File Offset: 0x0012E6D4
		public Stream Open(Stream outStr, char format, string name, DateTime modificationTime, byte[] buffer)
		{
			if (this.pkOut != null)
			{
				throw new InvalidOperationException("generator already in open state");
			}
			if (outStr == null)
			{
				throw new ArgumentNullException("outStr");
			}
			long modificationTime2 = DateTimeUtilities.DateTimeToUnixMs(modificationTime);
			byte[] encName = Strings.ToUtf8ByteArray(name);
			this.pkOut = new BcpgOutputStream(outStr, PacketTag.LiteralData, buffer);
			this.WriteHeader(this.pkOut, format, encName, modificationTime2);
			return new WrappedGeneratorStream(this, this.pkOut);
		}

		// Token: 0x06003845 RID: 14405 RVA: 0x0012E748 File Offset: 0x0012E748
		public Stream Open(Stream outStr, char format, FileInfo file)
		{
			return this.Open(outStr, format, file.Name, file.Length, file.LastWriteTime);
		}

		// Token: 0x06003846 RID: 14406 RVA: 0x0012E774 File Offset: 0x0012E774
		public void Close()
		{
			if (this.pkOut != null)
			{
				this.pkOut.Finish();
				this.pkOut.Flush();
				this.pkOut = null;
			}
		}

		// Token: 0x04001DC6 RID: 7622
		public const char Binary = 'b';

		// Token: 0x04001DC7 RID: 7623
		public const char Text = 't';

		// Token: 0x04001DC8 RID: 7624
		public const char Utf8 = 'u';

		// Token: 0x04001DC9 RID: 7625
		public const string Console = "_CONSOLE";

		// Token: 0x04001DCA RID: 7626
		private BcpgOutputStream pkOut;

		// Token: 0x04001DCB RID: 7627
		private bool oldFormat;
	}
}
