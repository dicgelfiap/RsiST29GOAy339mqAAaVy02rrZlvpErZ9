using System;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x0200046D RID: 1133
	public class RC2Parameters : KeyParameter
	{
		// Token: 0x06002339 RID: 9017 RVA: 0x000C64E0 File Offset: 0x000C64E0
		public RC2Parameters(byte[] key) : this(key, (key.Length > 128) ? 1024 : (key.Length * 8))
		{
		}

		// Token: 0x0600233A RID: 9018 RVA: 0x000C6508 File Offset: 0x000C6508
		public RC2Parameters(byte[] key, int keyOff, int keyLen) : this(key, keyOff, keyLen, (keyLen > 128) ? 1024 : (keyLen * 8))
		{
		}

		// Token: 0x0600233B RID: 9019 RVA: 0x000C653C File Offset: 0x000C653C
		public RC2Parameters(byte[] key, int bits) : base(key)
		{
			this.bits = bits;
		}

		// Token: 0x0600233C RID: 9020 RVA: 0x000C654C File Offset: 0x000C654C
		public RC2Parameters(byte[] key, int keyOff, int keyLen, int bits) : base(key, keyOff, keyLen)
		{
			this.bits = bits;
		}

		// Token: 0x170006CA RID: 1738
		// (get) Token: 0x0600233D RID: 9021 RVA: 0x000C6560 File Offset: 0x000C6560
		public int EffectiveKeyBits
		{
			get
			{
				return this.bits;
			}
		}

		// Token: 0x04001664 RID: 5732
		private readonly int bits;
	}
}
