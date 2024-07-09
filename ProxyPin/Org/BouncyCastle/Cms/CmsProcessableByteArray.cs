using System;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Cms;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x020002ED RID: 749
	public class CmsProcessableByteArray : CmsProcessable, CmsReadable
	{
		// Token: 0x0600167E RID: 5758 RVA: 0x00075230 File Offset: 0x00075230
		public CmsProcessableByteArray(byte[] bytes)
		{
			this.type = CmsObjectIdentifiers.Data;
			this.bytes = bytes;
		}

		// Token: 0x0600167F RID: 5759 RVA: 0x0007524C File Offset: 0x0007524C
		public CmsProcessableByteArray(DerObjectIdentifier type, byte[] bytes)
		{
			this.bytes = bytes;
			this.type = type;
		}

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x06001680 RID: 5760 RVA: 0x00075264 File Offset: 0x00075264
		public DerObjectIdentifier Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x06001681 RID: 5761 RVA: 0x0007526C File Offset: 0x0007526C
		public virtual Stream GetInputStream()
		{
			return new MemoryStream(this.bytes, false);
		}

		// Token: 0x06001682 RID: 5762 RVA: 0x0007527C File Offset: 0x0007527C
		public virtual void Write(Stream zOut)
		{
			zOut.Write(this.bytes, 0, this.bytes.Length);
		}

		// Token: 0x06001683 RID: 5763 RVA: 0x00075294 File Offset: 0x00075294
		[Obsolete]
		public virtual object GetContent()
		{
			return this.bytes.Clone();
		}

		// Token: 0x04000F42 RID: 3906
		private readonly DerObjectIdentifier type;

		// Token: 0x04000F43 RID: 3907
		private readonly byte[] bytes;
	}
}
