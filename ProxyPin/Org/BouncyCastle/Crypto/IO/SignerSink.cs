using System;
using Org.BouncyCastle.Utilities.IO;

namespace Org.BouncyCastle.Crypto.IO
{
	// Token: 0x020003DF RID: 991
	public class SignerSink : BaseOutputStream
	{
		// Token: 0x06001F55 RID: 8021 RVA: 0x000B6E74 File Offset: 0x000B6E74
		public SignerSink(ISigner signer)
		{
			this.mSigner = signer;
		}

		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x06001F56 RID: 8022 RVA: 0x000B6E84 File Offset: 0x000B6E84
		public virtual ISigner Signer
		{
			get
			{
				return this.mSigner;
			}
		}

		// Token: 0x06001F57 RID: 8023 RVA: 0x000B6E8C File Offset: 0x000B6E8C
		public override void WriteByte(byte b)
		{
			this.mSigner.Update(b);
		}

		// Token: 0x06001F58 RID: 8024 RVA: 0x000B6E9C File Offset: 0x000B6E9C
		public override void Write(byte[] buf, int off, int len)
		{
			if (len > 0)
			{
				this.mSigner.BlockUpdate(buf, off, len);
			}
		}

		// Token: 0x0400148F RID: 5263
		private readonly ISigner mSigner;
	}
}
