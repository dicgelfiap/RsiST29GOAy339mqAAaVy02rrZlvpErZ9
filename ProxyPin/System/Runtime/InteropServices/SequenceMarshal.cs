using System;
using System.Buffers;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000CE2 RID: 3298
	[ComVisible(true)]
	public static class SequenceMarshal
	{
		// Token: 0x0600856F RID: 34159 RVA: 0x00271818 File Offset: 0x00271818
		public static bool TryGetReadOnlySequenceSegment<T>(ReadOnlySequence<T> sequence, out ReadOnlySequenceSegment<T> startSegment, out int startIndex, out ReadOnlySequenceSegment<T> endSegment, out int endIndex)
		{
			return sequence.TryGetReadOnlySequenceSegment(out startSegment, out startIndex, out endSegment, out endIndex);
		}

		// Token: 0x06008570 RID: 34160 RVA: 0x00271828 File Offset: 0x00271828
		public static bool TryGetArray<T>(ReadOnlySequence<T> sequence, out ArraySegment<T> segment)
		{
			return sequence.TryGetArray(out segment);
		}

		// Token: 0x06008571 RID: 34161 RVA: 0x00271834 File Offset: 0x00271834
		public static bool TryGetReadOnlyMemory<T>(ReadOnlySequence<T> sequence, out ReadOnlyMemory<T> memory)
		{
			if (!sequence.IsSingleSegment)
			{
				memory = default(ReadOnlyMemory<T>);
				return false;
			}
			memory = sequence.First;
			return true;
		}

		// Token: 0x06008572 RID: 34162 RVA: 0x0027185C File Offset: 0x0027185C
		internal static bool TryGetString(ReadOnlySequence<char> sequence, out string text, out int start, out int length)
		{
			return sequence.TryGetString(out text, out start, out length);
		}
	}
}
