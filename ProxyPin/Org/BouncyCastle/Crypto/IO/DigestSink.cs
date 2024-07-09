using System;
using Org.BouncyCastle.Utilities.IO;

namespace Org.BouncyCastle.Crypto.IO
{
	// Token: 0x020003DB RID: 987
	public class DigestSink : BaseOutputStream
	{
		// Token: 0x06001F2B RID: 7979 RVA: 0x000B6A94 File Offset: 0x000B6A94
		public DigestSink(IDigest digest)
		{
			this.mDigest = digest;
		}

		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x06001F2C RID: 7980 RVA: 0x000B6AA4 File Offset: 0x000B6AA4
		public virtual IDigest Digest
		{
			get
			{
				return this.mDigest;
			}
		}

		// Token: 0x06001F2D RID: 7981 RVA: 0x000B6AAC File Offset: 0x000B6AAC
		public override void WriteByte(byte b)
		{
			this.mDigest.Update(b);
		}

		// Token: 0x06001F2E RID: 7982 RVA: 0x000B6ABC File Offset: 0x000B6ABC
		public override void Write(byte[] buf, int off, int len)
		{
			if (len > 0)
			{
				this.mDigest.BlockUpdate(buf, off, len);
			}
		}

		// Token: 0x04001487 RID: 5255
		private readonly IDigest mDigest;
	}
}
