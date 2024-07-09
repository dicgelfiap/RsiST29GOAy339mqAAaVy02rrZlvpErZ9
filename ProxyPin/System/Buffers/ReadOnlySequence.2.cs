using System;
using System.Runtime.CompilerServices;

namespace System.Buffers
{
	// Token: 0x02000CEA RID: 3306
	internal static class ReadOnlySequence
	{
		// Token: 0x060085C3 RID: 34243 RVA: 0x00273124 File Offset: 0x00273124
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int SegmentToSequenceStart(int startIndex)
		{
			return startIndex | 0;
		}

		// Token: 0x060085C4 RID: 34244 RVA: 0x0027312C File Offset: 0x0027312C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int SegmentToSequenceEnd(int endIndex)
		{
			return endIndex | 0;
		}

		// Token: 0x060085C5 RID: 34245 RVA: 0x00273134 File Offset: 0x00273134
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int ArrayToSequenceStart(int startIndex)
		{
			return startIndex | 0;
		}

		// Token: 0x060085C6 RID: 34246 RVA: 0x0027313C File Offset: 0x0027313C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int ArrayToSequenceEnd(int endIndex)
		{
			return endIndex | int.MinValue;
		}

		// Token: 0x060085C7 RID: 34247 RVA: 0x00273148 File Offset: 0x00273148
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int MemoryManagerToSequenceStart(int startIndex)
		{
			return startIndex | int.MinValue;
		}

		// Token: 0x060085C8 RID: 34248 RVA: 0x00273154 File Offset: 0x00273154
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int MemoryManagerToSequenceEnd(int endIndex)
		{
			return endIndex | 0;
		}

		// Token: 0x060085C9 RID: 34249 RVA: 0x0027315C File Offset: 0x0027315C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int StringToSequenceStart(int startIndex)
		{
			return startIndex | int.MinValue;
		}

		// Token: 0x060085CA RID: 34250 RVA: 0x00273168 File Offset: 0x00273168
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int StringToSequenceEnd(int endIndex)
		{
			return endIndex | int.MinValue;
		}

		// Token: 0x04003DD2 RID: 15826
		public const int FlagBitMask = -2147483648;

		// Token: 0x04003DD3 RID: 15827
		public const int IndexBitMask = 2147483647;

		// Token: 0x04003DD4 RID: 15828
		public const int SegmentStartMask = 0;

		// Token: 0x04003DD5 RID: 15829
		public const int SegmentEndMask = 0;

		// Token: 0x04003DD6 RID: 15830
		public const int ArrayStartMask = 0;

		// Token: 0x04003DD7 RID: 15831
		public const int ArrayEndMask = -2147483648;

		// Token: 0x04003DD8 RID: 15832
		public const int MemoryManagerStartMask = -2147483648;

		// Token: 0x04003DD9 RID: 15833
		public const int MemoryManagerEndMask = 0;

		// Token: 0x04003DDA RID: 15834
		public const int StringStartMask = -2147483648;

		// Token: 0x04003DDB RID: 15835
		public const int StringEndMask = -2147483648;
	}
}
