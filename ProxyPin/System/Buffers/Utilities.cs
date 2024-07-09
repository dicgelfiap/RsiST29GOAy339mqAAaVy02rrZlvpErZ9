using System;
using System.Runtime.CompilerServices;

namespace System.Buffers
{
	// Token: 0x02000C89 RID: 3209
	internal static class Utilities
	{
		// Token: 0x0600807C RID: 32892 RVA: 0x002608B4 File Offset: 0x002608B4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static int SelectBucketIndex(int bufferSize)
		{
			uint num = (uint)(bufferSize - 1) >> 4;
			int num2 = 0;
			if (num > 65535U)
			{
				num >>= 16;
				num2 = 16;
			}
			if (num > 255U)
			{
				num >>= 8;
				num2 += 8;
			}
			if (num > 15U)
			{
				num >>= 4;
				num2 += 4;
			}
			if (num > 3U)
			{
				num >>= 2;
				num2 += 2;
			}
			if (num > 1U)
			{
				num >>= 1;
				num2++;
			}
			return num2 + (int)num;
		}

		// Token: 0x0600807D RID: 32893 RVA: 0x00260924 File Offset: 0x00260924
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static int GetMaxSizeForBucket(int binIndex)
		{
			return 16 << binIndex;
		}
	}
}
