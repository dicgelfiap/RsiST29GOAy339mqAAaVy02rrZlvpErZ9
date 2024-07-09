using System;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x02000462 RID: 1122
	public class MgfParameters : IDerivationParameters
	{
		// Token: 0x0600230C RID: 8972 RVA: 0x000C60C8 File Offset: 0x000C60C8
		public MgfParameters(byte[] seed) : this(seed, 0, seed.Length)
		{
		}

		// Token: 0x0600230D RID: 8973 RVA: 0x000C60D8 File Offset: 0x000C60D8
		public MgfParameters(byte[] seed, int off, int len)
		{
			this.seed = new byte[len];
			Array.Copy(seed, off, this.seed, 0, len);
		}

		// Token: 0x0600230E RID: 8974 RVA: 0x000C60FC File Offset: 0x000C60FC
		public byte[] GetSeed()
		{
			return (byte[])this.seed.Clone();
		}

		// Token: 0x0400164D RID: 5709
		private readonly byte[] seed;
	}
}
