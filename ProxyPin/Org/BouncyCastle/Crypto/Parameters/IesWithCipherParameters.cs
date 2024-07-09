using System;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x0200045C RID: 1116
	public class IesWithCipherParameters : IesParameters
	{
		// Token: 0x060022ED RID: 8941 RVA: 0x000C5D6C File Offset: 0x000C5D6C
		public IesWithCipherParameters(byte[] derivation, byte[] encoding, int macKeySize, int cipherKeySize) : base(derivation, encoding, macKeySize)
		{
			this.cipherKeySize = cipherKeySize;
		}

		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x060022EE RID: 8942 RVA: 0x000C5D80 File Offset: 0x000C5D80
		public int CipherKeySize
		{
			get
			{
				return this.cipherKeySize;
			}
		}

		// Token: 0x0400163A RID: 5690
		private int cipherKeySize;
	}
}
