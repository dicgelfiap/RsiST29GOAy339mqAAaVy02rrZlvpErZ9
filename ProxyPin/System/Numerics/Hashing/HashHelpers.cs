using System;

namespace System.Numerics.Hashing
{
	// Token: 0x02000CE1 RID: 3297
	internal static class HashHelpers
	{
		// Token: 0x0600856D RID: 34157 RVA: 0x002717CC File Offset: 0x002717CC
		public static int Combine(int h1, int h2)
		{
			uint num = (uint)(h1 << 5 | (int)((uint)h1 >> 27));
			return (int)(num + (uint)h1 ^ (uint)h2);
		}

		// Token: 0x04003DC7 RID: 15815
		public static readonly int RandomSeed = Guid.NewGuid().GetHashCode();
	}
}
