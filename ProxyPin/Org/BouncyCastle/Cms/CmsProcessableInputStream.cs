using System;
using System.IO;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.IO;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x020002EF RID: 751
	public class CmsProcessableInputStream : CmsProcessable, CmsReadable
	{
		// Token: 0x06001689 RID: 5769 RVA: 0x00075318 File Offset: 0x00075318
		public CmsProcessableInputStream(Stream input)
		{
			this.input = input;
		}

		// Token: 0x0600168A RID: 5770 RVA: 0x00075330 File Offset: 0x00075330
		public virtual Stream GetInputStream()
		{
			this.CheckSingleUsage();
			return this.input;
		}

		// Token: 0x0600168B RID: 5771 RVA: 0x00075340 File Offset: 0x00075340
		public virtual void Write(Stream output)
		{
			this.CheckSingleUsage();
			Streams.PipeAll(this.input, output);
			Platform.Dispose(this.input);
		}

		// Token: 0x0600168C RID: 5772 RVA: 0x00075360 File Offset: 0x00075360
		[Obsolete]
		public virtual object GetContent()
		{
			return this.GetInputStream();
		}

		// Token: 0x0600168D RID: 5773 RVA: 0x00075368 File Offset: 0x00075368
		protected virtual void CheckSingleUsage()
		{
			lock (this)
			{
				if (this.used)
				{
					throw new InvalidOperationException("CmsProcessableInputStream can only be used once");
				}
				this.used = true;
			}
		}

		// Token: 0x04000F47 RID: 3911
		private readonly Stream input;

		// Token: 0x04000F48 RID: 3912
		private bool used = false;
	}
}
