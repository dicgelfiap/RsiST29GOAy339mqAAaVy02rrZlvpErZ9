using System;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x0200042E RID: 1070
	public class AeadParameters : ICipherParameters
	{
		// Token: 0x060021D4 RID: 8660 RVA: 0x000C356C File Offset: 0x000C356C
		public AeadParameters(KeyParameter key, int macSize, byte[] nonce) : this(key, macSize, nonce, null)
		{
		}

		// Token: 0x060021D5 RID: 8661 RVA: 0x000C3578 File Offset: 0x000C3578
		public AeadParameters(KeyParameter key, int macSize, byte[] nonce, byte[] associatedText)
		{
			this.key = key;
			this.nonce = nonce;
			this.macSize = macSize;
			this.associatedText = associatedText;
		}

		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x060021D6 RID: 8662 RVA: 0x000C35A0 File Offset: 0x000C35A0
		public virtual KeyParameter Key
		{
			get
			{
				return this.key;
			}
		}

		// Token: 0x17000660 RID: 1632
		// (get) Token: 0x060021D7 RID: 8663 RVA: 0x000C35A8 File Offset: 0x000C35A8
		public virtual int MacSize
		{
			get
			{
				return this.macSize;
			}
		}

		// Token: 0x060021D8 RID: 8664 RVA: 0x000C35B0 File Offset: 0x000C35B0
		public virtual byte[] GetAssociatedText()
		{
			return this.associatedText;
		}

		// Token: 0x060021D9 RID: 8665 RVA: 0x000C35B8 File Offset: 0x000C35B8
		public virtual byte[] GetNonce()
		{
			return this.nonce;
		}

		// Token: 0x040015D3 RID: 5587
		private readonly byte[] associatedText;

		// Token: 0x040015D4 RID: 5588
		private readonly byte[] nonce;

		// Token: 0x040015D5 RID: 5589
		private readonly KeyParameter key;

		// Token: 0x040015D6 RID: 5590
		private readonly int macSize;
	}
}
