using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Buffers
{
	// Token: 0x02000CE5 RID: 3301
	[ComVisible(true)]
	public static class BuffersExtensions
	{
		// Token: 0x06008588 RID: 34184 RVA: 0x00271E38 File Offset: 0x00271E38
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static SequencePosition? PositionOf<T>([System.Memory.IsReadOnly] [In] this ReadOnlySequence<T> source, T value) where T : object, IEquatable<T>
		{
			if (!source.IsSingleSegment)
			{
				return BuffersExtensions.PositionOfMultiSegment<T>(ref source, value);
			}
			int num = source.First.Span.IndexOf(value);
			if (num != -1)
			{
				return new SequencePosition?(source.GetPosition((long)num));
			}
			return null;
		}

		// Token: 0x06008589 RID: 34185 RVA: 0x00271E90 File Offset: 0x00271E90
		private static SequencePosition? PositionOfMultiSegment<T>([System.Memory.IsReadOnly] [In] ref ReadOnlySequence<T> source, T value) where T : object, IEquatable<T>
		{
			SequencePosition start = source.Start;
			SequencePosition origin = start;
			ReadOnlyMemory<T> readOnlyMemory;
			while (source.TryGet(ref start, out readOnlyMemory, true))
			{
				int num = readOnlyMemory.Span.IndexOf(value);
				if (num != -1)
				{
					return new SequencePosition?(source.GetPosition((long)num, origin));
				}
				if (start.GetObject() == null)
				{
					break;
				}
				origin = start;
			}
			return null;
		}

		// Token: 0x0600858A RID: 34186 RVA: 0x00271EF8 File Offset: 0x00271EF8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void CopyTo<T>([System.Memory.IsReadOnly] [In] this ReadOnlySequence<T> source, Span<T> destination)
		{
			if (source.Length > (long)destination.Length)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.destination);
			}
			if (source.IsSingleSegment)
			{
				source.First.Span.CopyTo(destination);
				return;
			}
			BuffersExtensions.CopyToMultiSegment<T>(ref source, destination);
		}

		// Token: 0x0600858B RID: 34187 RVA: 0x00271F4C File Offset: 0x00271F4C
		private static void CopyToMultiSegment<T>([System.Memory.IsReadOnly] [In] ref ReadOnlySequence<T> sequence, Span<T> destination)
		{
			SequencePosition start = sequence.Start;
			ReadOnlyMemory<T> readOnlyMemory;
			while (sequence.TryGet(ref start, out readOnlyMemory, true))
			{
				ReadOnlySpan<T> span = readOnlyMemory.Span;
				span.CopyTo(destination);
				if (start.GetObject() == null)
				{
					break;
				}
				destination = destination.Slice(span.Length);
			}
		}

		// Token: 0x0600858C RID: 34188 RVA: 0x00271FA4 File Offset: 0x00271FA4
		public static T[] ToArray<T>([System.Memory.IsReadOnly] [In] this ReadOnlySequence<T> sequence)
		{
			T[] array = new T[sequence.Length];
			ref sequence.CopyTo(array);
			return array;
		}

		// Token: 0x0600858D RID: 34189 RVA: 0x00271FD0 File Offset: 0x00271FD0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Write<T>(this IBufferWriter<T> writer, ReadOnlySpan<T> value)
		{
			Span<T> span = writer.GetSpan(0);
			if (value.Length <= span.Length)
			{
				value.CopyTo(span);
				writer.Advance(value.Length);
				return;
			}
			BuffersExtensions.WriteMultiSegment<T>(writer, ref value, span);
		}

		// Token: 0x0600858E RID: 34190 RVA: 0x0027201C File Offset: 0x0027201C
		private static void WriteMultiSegment<T>(IBufferWriter<T> writer, [System.Memory.IsReadOnly] [In] ref ReadOnlySpan<T> source, Span<T> destination)
		{
			ReadOnlySpan<T> readOnlySpan = source;
			for (;;)
			{
				int num = Math.Min(destination.Length, readOnlySpan.Length);
				readOnlySpan.Slice(0, num).CopyTo(destination);
				writer.Advance(num);
				readOnlySpan = readOnlySpan.Slice(num);
				if (readOnlySpan.Length <= 0)
				{
					break;
				}
				destination = writer.GetSpan(readOnlySpan.Length);
			}
		}
	}
}
