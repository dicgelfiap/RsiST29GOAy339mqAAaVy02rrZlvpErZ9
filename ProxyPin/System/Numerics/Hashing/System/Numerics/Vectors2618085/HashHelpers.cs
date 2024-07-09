using System;

namespace System.Numerics.Hashing
{
	// Token: 0x02000D02 RID: 3330
	internal static class HashHelpers
	{
		// Token: 0x06008767 RID: 34663 RVA: 0x0028F8DC File Offset: 0x0028F8DC
		public static int Combine(int h1, int h2)
		{
			uint num = (uint)(h1 << 5 | (int)((uint)h1 >> 27));
			return (int)(num + (uint)h1 ^ (uint)h2);
		}

		// Token: 0x04003E7B RID: 15995
		public static readonly int RandomSeed = Guid.NewGuid().GetHashCode();
	}
}
