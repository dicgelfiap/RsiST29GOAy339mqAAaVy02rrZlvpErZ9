using System;

namespace Org.BouncyCastle.Utilities
{
	// Token: 0x020006FE RID: 1790
	public abstract class Integers
	{
		// Token: 0x06003EB2 RID: 16050 RVA: 0x00159A44 File Offset: 0x00159A44
		public static int NumberOfLeadingZeros(int i)
		{
			if (i <= 0)
			{
				return ~i >> 26 & 32;
			}
			uint num = (uint)i;
			int num2 = 1;
			if (num >> 16 == 0U)
			{
				num2 += 16;
				num <<= 16;
			}
			if (num >> 24 == 0U)
			{
				num2 += 8;
				num <<= 8;
			}
			if (num >> 28 == 0U)
			{
				num2 += 4;
				num <<= 4;
			}
			if (num >> 30 == 0U)
			{
				num2 += 2;
				num <<= 2;
			}
			return num2 - (int)(num >> 31);
		}

		// Token: 0x06003EB3 RID: 16051 RVA: 0x00159AB8 File Offset: 0x00159AB8
		public static int RotateLeft(int i, int distance)
		{
			return i << distance ^ (int)((uint)i >> -distance);
		}

		// Token: 0x06003EB4 RID: 16052 RVA: 0x00159AC8 File Offset: 0x00159AC8
		[CLSCompliant(false)]
		public static uint RotateLeft(uint i, int distance)
		{
			return i << distance ^ i >> -distance;
		}

		// Token: 0x06003EB5 RID: 16053 RVA: 0x00159AD8 File Offset: 0x00159AD8
		public static int RotateRight(int i, int distance)
		{
			return (int)((uint)i >> distance ^ (uint)((uint)i << -distance));
		}

		// Token: 0x06003EB6 RID: 16054 RVA: 0x00159AE8 File Offset: 0x00159AE8
		[CLSCompliant(false)]
		public static uint RotateRight(uint i, int distance)
		{
			return i >> distance ^ i << -distance;
		}
	}
}
