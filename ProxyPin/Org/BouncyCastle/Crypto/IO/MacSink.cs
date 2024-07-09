using System;
using Org.BouncyCastle.Utilities.IO;

namespace Org.BouncyCastle.Crypto.IO
{
	// Token: 0x020003DD RID: 989
	public class MacSink : BaseOutputStream
	{
		// Token: 0x06001F40 RID: 8000 RVA: 0x000B6C84 File Offset: 0x000B6C84
		public MacSink(IMac mac)
		{
			this.mMac = mac;
		}

		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x06001F41 RID: 8001 RVA: 0x000B6C94 File Offset: 0x000B6C94
		public virtual IMac Mac
		{
			get
			{
				return this.mMac;
			}
		}

		// Token: 0x06001F42 RID: 8002 RVA: 0x000B6C9C File Offset: 0x000B6C9C
		public override void WriteByte(byte b)
		{
			this.mMac.Update(b);
		}

		// Token: 0x06001F43 RID: 8003 RVA: 0x000B6CAC File Offset: 0x000B6CAC
		public override void Write(byte[] buf, int off, int len)
		{
			if (len > 0)
			{
				this.mMac.BlockUpdate(buf, off, len);
			}
		}

		// Token: 0x0400148B RID: 5259
		private readonly IMac mMac;
	}
}
