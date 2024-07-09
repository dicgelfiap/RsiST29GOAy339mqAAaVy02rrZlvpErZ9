using System;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x02000CFB RID: 3323
	internal static class MathF
	{
		// Token: 0x060086AD RID: 34477 RVA: 0x0027B4D0 File Offset: 0x0027B4D0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Abs(float x)
		{
			return Math.Abs(x);
		}

		// Token: 0x060086AE RID: 34478 RVA: 0x0027B4D8 File Offset: 0x0027B4D8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Acos(float x)
		{
			return (float)Math.Acos((double)x);
		}

		// Token: 0x060086AF RID: 34479 RVA: 0x0027B4E4 File Offset: 0x0027B4E4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Cos(float x)
		{
			return (float)Math.Cos((double)x);
		}

		// Token: 0x060086B0 RID: 34480 RVA: 0x0027B4F0 File Offset: 0x0027B4F0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float IEEERemainder(float x, float y)
		{
			return (float)Math.IEEERemainder((double)x, (double)y);
		}

		// Token: 0x060086B1 RID: 34481 RVA: 0x0027B4FC File Offset: 0x0027B4FC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Pow(float x, float y)
		{
			return (float)Math.Pow((double)x, (double)y);
		}

		// Token: 0x060086B2 RID: 34482 RVA: 0x0027B508 File Offset: 0x0027B508
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Sin(float x)
		{
			return (float)Math.Sin((double)x);
		}

		// Token: 0x060086B3 RID: 34483 RVA: 0x0027B514 File Offset: 0x0027B514
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Sqrt(float x)
		{
			return (float)Math.Sqrt((double)x);
		}

		// Token: 0x060086B4 RID: 34484 RVA: 0x0027B520 File Offset: 0x0027B520
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Tan(float x)
		{
			return (float)Math.Tan((double)x);
		}

		// Token: 0x04003E31 RID: 15921
		public const float PI = 3.1415927f;
	}
}
