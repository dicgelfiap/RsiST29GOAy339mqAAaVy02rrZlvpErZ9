using System;
using System.IO;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.IO;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x020002EE RID: 750
	public class CmsProcessableFile : CmsProcessable, CmsReadable
	{
		// Token: 0x06001684 RID: 5764 RVA: 0x000752A4 File Offset: 0x000752A4
		public CmsProcessableFile(FileInfo file) : this(file, 32768)
		{
		}

		// Token: 0x06001685 RID: 5765 RVA: 0x000752B4 File Offset: 0x000752B4
		public CmsProcessableFile(FileInfo file, int bufSize)
		{
			this._file = file;
			this._bufSize = bufSize;
		}

		// Token: 0x06001686 RID: 5766 RVA: 0x000752CC File Offset: 0x000752CC
		public virtual Stream GetInputStream()
		{
			return new FileStream(this._file.FullName, FileMode.Open, FileAccess.Read, FileShare.Read, this._bufSize);
		}

		// Token: 0x06001687 RID: 5767 RVA: 0x000752E8 File Offset: 0x000752E8
		public virtual void Write(Stream zOut)
		{
			Stream inputStream = this.GetInputStream();
			Streams.PipeAll(inputStream, zOut);
			Platform.Dispose(inputStream);
		}

		// Token: 0x06001688 RID: 5768 RVA: 0x00075310 File Offset: 0x00075310
		[Obsolete]
		public virtual object GetContent()
		{
			return this._file;
		}

		// Token: 0x04000F44 RID: 3908
		private const int DefaultBufSize = 32768;

		// Token: 0x04000F45 RID: 3909
		private readonly FileInfo _file;

		// Token: 0x04000F46 RID: 3910
		private readonly int _bufSize;
	}
}
