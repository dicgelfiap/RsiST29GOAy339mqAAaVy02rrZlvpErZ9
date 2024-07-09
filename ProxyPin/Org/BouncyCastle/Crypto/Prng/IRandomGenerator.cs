using System;

namespace Org.BouncyCastle.Crypto.Prng
{
	// Token: 0x02000487 RID: 1159
	public interface IRandomGenerator
	{
		// Token: 0x060023C3 RID: 9155
		void AddSeedMaterial(byte[] seed);

		// Token: 0x060023C4 RID: 9156
		void AddSeedMaterial(long seed);

		// Token: 0x060023C5 RID: 9157
		void NextBytes(byte[] bytes);

		// Token: 0x060023C6 RID: 9158
		void NextBytes(byte[] bytes, int start, int len);
	}
}
