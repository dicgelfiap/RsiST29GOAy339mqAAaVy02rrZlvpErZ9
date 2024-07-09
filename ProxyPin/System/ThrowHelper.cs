using System;
using System.Buffers;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x02000CCF RID: 3279
	internal static class ThrowHelper
	{
		// Token: 0x06008416 RID: 33814 RVA: 0x00268FA4 File Offset: 0x00268FA4
		internal static void ThrowArgumentNullException(ExceptionArgument argument)
		{
			throw ThrowHelper.CreateArgumentNullException(argument);
		}

		// Token: 0x06008417 RID: 33815 RVA: 0x00268FAC File Offset: 0x00268FAC
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static Exception CreateArgumentNullException(ExceptionArgument argument)
		{
			return new ArgumentNullException(argument.ToString());
		}

		// Token: 0x06008418 RID: 33816 RVA: 0x00268FC0 File Offset: 0x00268FC0
		internal static void ThrowArrayTypeMismatchException()
		{
			throw ThrowHelper.CreateArrayTypeMismatchException();
		}

		// Token: 0x06008419 RID: 33817 RVA: 0x00268FC8 File Offset: 0x00268FC8
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static Exception CreateArrayTypeMismatchException()
		{
			return new ArrayTypeMismatchException();
		}

		// Token: 0x0600841A RID: 33818 RVA: 0x00268FD0 File Offset: 0x00268FD0
		internal static void ThrowArgumentException_InvalidTypeWithPointersNotSupported(Type type)
		{
			throw ThrowHelper.CreateArgumentException_InvalidTypeWithPointersNotSupported(type);
		}

		// Token: 0x0600841B RID: 33819 RVA: 0x00268FD8 File Offset: 0x00268FD8
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static Exception CreateArgumentException_InvalidTypeWithPointersNotSupported(Type type)
		{
			return new ArgumentException(System.Memory2565708.SR.Format(System.Memory2565708.SR.Argument_InvalidTypeWithPointersNotSupported, type));
		}

		// Token: 0x0600841C RID: 33820 RVA: 0x00268FEC File Offset: 0x00268FEC
		internal static void ThrowArgumentException_DestinationTooShort()
		{
			throw ThrowHelper.CreateArgumentException_DestinationTooShort();
		}

		// Token: 0x0600841D RID: 33821 RVA: 0x00268FF4 File Offset: 0x00268FF4
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static Exception CreateArgumentException_DestinationTooShort()
		{
			return new ArgumentException(System.Memory2565708.SR.Argument_DestinationTooShort);
		}

		// Token: 0x0600841E RID: 33822 RVA: 0x00269000 File Offset: 0x00269000
		internal static void ThrowIndexOutOfRangeException()
		{
			throw ThrowHelper.CreateIndexOutOfRangeException();
		}

		// Token: 0x0600841F RID: 33823 RVA: 0x00269008 File Offset: 0x00269008
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static Exception CreateIndexOutOfRangeException()
		{
			return new IndexOutOfRangeException();
		}

		// Token: 0x06008420 RID: 33824 RVA: 0x00269010 File Offset: 0x00269010
		internal static void ThrowArgumentOutOfRangeException()
		{
			throw ThrowHelper.CreateArgumentOutOfRangeException();
		}

		// Token: 0x06008421 RID: 33825 RVA: 0x00269018 File Offset: 0x00269018
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static Exception CreateArgumentOutOfRangeException()
		{
			return new ArgumentOutOfRangeException();
		}

		// Token: 0x06008422 RID: 33826 RVA: 0x00269020 File Offset: 0x00269020
		internal static void ThrowArgumentOutOfRangeException(ExceptionArgument argument)
		{
			throw ThrowHelper.CreateArgumentOutOfRangeException(argument);
		}

		// Token: 0x06008423 RID: 33827 RVA: 0x00269028 File Offset: 0x00269028
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static Exception CreateArgumentOutOfRangeException(ExceptionArgument argument)
		{
			return new ArgumentOutOfRangeException(argument.ToString());
		}

		// Token: 0x06008424 RID: 33828 RVA: 0x0026903C File Offset: 0x0026903C
		internal static void ThrowArgumentOutOfRangeException_PrecisionTooLarge()
		{
			throw ThrowHelper.CreateArgumentOutOfRangeException_PrecisionTooLarge();
		}

		// Token: 0x06008425 RID: 33829 RVA: 0x00269044 File Offset: 0x00269044
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static Exception CreateArgumentOutOfRangeException_PrecisionTooLarge()
		{
			return new ArgumentOutOfRangeException("precision", System.Memory2565708.SR.Format(System.Memory2565708.SR.Argument_PrecisionTooLarge, 99));
		}

		// Token: 0x06008426 RID: 33830 RVA: 0x00269064 File Offset: 0x00269064
		internal static void ThrowArgumentOutOfRangeException_SymbolDoesNotFit()
		{
			throw ThrowHelper.CreateArgumentOutOfRangeException_SymbolDoesNotFit();
		}

		// Token: 0x06008427 RID: 33831 RVA: 0x0026906C File Offset: 0x0026906C
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static Exception CreateArgumentOutOfRangeException_SymbolDoesNotFit()
		{
			return new ArgumentOutOfRangeException("symbol", System.Memory2565708.SR.Argument_BadFormatSpecifier);
		}

		// Token: 0x06008428 RID: 33832 RVA: 0x00269080 File Offset: 0x00269080
		internal static void ThrowInvalidOperationException()
		{
			throw ThrowHelper.CreateInvalidOperationException();
		}

		// Token: 0x06008429 RID: 33833 RVA: 0x00269088 File Offset: 0x00269088
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static Exception CreateInvalidOperationException()
		{
			return new InvalidOperationException();
		}

		// Token: 0x0600842A RID: 33834 RVA: 0x00269090 File Offset: 0x00269090
		internal static void ThrowInvalidOperationException_OutstandingReferences()
		{
			throw ThrowHelper.CreateInvalidOperationException_OutstandingReferences();
		}

		// Token: 0x0600842B RID: 33835 RVA: 0x00269098 File Offset: 0x00269098
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static Exception CreateInvalidOperationException_OutstandingReferences()
		{
			return new InvalidOperationException(System.Memory2565708.SR.OutstandingReferences);
		}

		// Token: 0x0600842C RID: 33836 RVA: 0x002690A4 File Offset: 0x002690A4
		internal static void ThrowInvalidOperationException_UnexpectedSegmentType()
		{
			throw ThrowHelper.CreateInvalidOperationException_UnexpectedSegmentType();
		}

		// Token: 0x0600842D RID: 33837 RVA: 0x002690AC File Offset: 0x002690AC
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static Exception CreateInvalidOperationException_UnexpectedSegmentType()
		{
			return new InvalidOperationException(System.Memory2565708.SR.UnexpectedSegmentType);
		}

		// Token: 0x0600842E RID: 33838 RVA: 0x002690B8 File Offset: 0x002690B8
		internal static void ThrowInvalidOperationException_EndPositionNotReached()
		{
			throw ThrowHelper.CreateInvalidOperationException_EndPositionNotReached();
		}

		// Token: 0x0600842F RID: 33839 RVA: 0x002690C0 File Offset: 0x002690C0
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static Exception CreateInvalidOperationException_EndPositionNotReached()
		{
			return new InvalidOperationException(System.Memory2565708.SR.EndPositionNotReached);
		}

		// Token: 0x06008430 RID: 33840 RVA: 0x002690CC File Offset: 0x002690CC
		internal static void ThrowArgumentOutOfRangeException_PositionOutOfRange()
		{
			throw ThrowHelper.CreateArgumentOutOfRangeException_PositionOutOfRange();
		}

		// Token: 0x06008431 RID: 33841 RVA: 0x002690D4 File Offset: 0x002690D4
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static Exception CreateArgumentOutOfRangeException_PositionOutOfRange()
		{
			return new ArgumentOutOfRangeException("position");
		}

		// Token: 0x06008432 RID: 33842 RVA: 0x002690E0 File Offset: 0x002690E0
		internal static void ThrowArgumentOutOfRangeException_OffsetOutOfRange()
		{
			throw ThrowHelper.CreateArgumentOutOfRangeException_OffsetOutOfRange();
		}

		// Token: 0x06008433 RID: 33843 RVA: 0x002690E8 File Offset: 0x002690E8
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static Exception CreateArgumentOutOfRangeException_OffsetOutOfRange()
		{
			return new ArgumentOutOfRangeException("offset");
		}

		// Token: 0x06008434 RID: 33844 RVA: 0x002690F4 File Offset: 0x002690F4
		internal static void ThrowObjectDisposedException_ArrayMemoryPoolBuffer()
		{
			throw ThrowHelper.CreateObjectDisposedException_ArrayMemoryPoolBuffer();
		}

		// Token: 0x06008435 RID: 33845 RVA: 0x002690FC File Offset: 0x002690FC
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static Exception CreateObjectDisposedException_ArrayMemoryPoolBuffer()
		{
			return new ObjectDisposedException("ArrayMemoryPoolBuffer");
		}

		// Token: 0x06008436 RID: 33846 RVA: 0x00269108 File Offset: 0x00269108
		internal static void ThrowFormatException_BadFormatSpecifier()
		{
			throw ThrowHelper.CreateFormatException_BadFormatSpecifier();
		}

		// Token: 0x06008437 RID: 33847 RVA: 0x00269110 File Offset: 0x00269110
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static Exception CreateFormatException_BadFormatSpecifier()
		{
			return new FormatException(System.Memory2565708.SR.Argument_BadFormatSpecifier);
		}

		// Token: 0x06008438 RID: 33848 RVA: 0x0026911C File Offset: 0x0026911C
		internal static void ThrowArgumentException_OverlapAlignmentMismatch()
		{
			throw ThrowHelper.CreateArgumentException_OverlapAlignmentMismatch();
		}

		// Token: 0x06008439 RID: 33849 RVA: 0x00269124 File Offset: 0x00269124
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static Exception CreateArgumentException_OverlapAlignmentMismatch()
		{
			return new ArgumentException(System.Memory2565708.SR.Argument_OverlapAlignmentMismatch);
		}

		// Token: 0x0600843A RID: 33850 RVA: 0x00269130 File Offset: 0x00269130
		internal static void ThrowNotSupportedException()
		{
			throw ThrowHelper.CreateThrowNotSupportedException();
		}

		// Token: 0x0600843B RID: 33851 RVA: 0x00269138 File Offset: 0x00269138
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static Exception CreateThrowNotSupportedException()
		{
			return new NotSupportedException();
		}

		// Token: 0x0600843C RID: 33852 RVA: 0x00269140 File Offset: 0x00269140
		public static bool TryFormatThrowFormatException(out int bytesWritten)
		{
			bytesWritten = 0;
			ThrowHelper.ThrowFormatException_BadFormatSpecifier();
			return false;
		}

		// Token: 0x0600843D RID: 33853 RVA: 0x0026914C File Offset: 0x0026914C
		public static bool TryParseThrowFormatException<T>(out T value, out int bytesConsumed)
		{
			value = default(T);
			bytesConsumed = 0;
			ThrowHelper.ThrowFormatException_BadFormatSpecifier();
			return false;
		}

		// Token: 0x0600843E RID: 33854 RVA: 0x00269160 File Offset: 0x00269160
		public static void ThrowArgumentValidationException<T>(ReadOnlySequenceSegment<T> startSegment, int startIndex, ReadOnlySequenceSegment<T> endSegment)
		{
			throw ThrowHelper.CreateArgumentValidationException<T>(startSegment, startIndex, endSegment);
		}

		// Token: 0x0600843F RID: 33855 RVA: 0x0026916C File Offset: 0x0026916C
		private static Exception CreateArgumentValidationException<T>(ReadOnlySequenceSegment<T> startSegment, int startIndex, ReadOnlySequenceSegment<T> endSegment)
		{
			if (startSegment == null)
			{
				return ThrowHelper.CreateArgumentNullException(ExceptionArgument.startSegment);
			}
			if (endSegment == null)
			{
				return ThrowHelper.CreateArgumentNullException(ExceptionArgument.endSegment);
			}
			if (startSegment != endSegment && startSegment.RunningIndex > endSegment.RunningIndex)
			{
				return ThrowHelper.CreateArgumentOutOfRangeException(ExceptionArgument.endSegment);
			}
			if (startSegment.Memory.Length < startIndex)
			{
				return ThrowHelper.CreateArgumentOutOfRangeException(ExceptionArgument.startIndex);
			}
			return ThrowHelper.CreateArgumentOutOfRangeException(ExceptionArgument.endIndex);
		}

		// Token: 0x06008440 RID: 33856 RVA: 0x002691DC File Offset: 0x002691DC
		public static void ThrowArgumentValidationException(Array array, int start)
		{
			throw ThrowHelper.CreateArgumentValidationException(array, start);
		}

		// Token: 0x06008441 RID: 33857 RVA: 0x002691E8 File Offset: 0x002691E8
		private static Exception CreateArgumentValidationException(Array array, int start)
		{
			if (array == null)
			{
				return ThrowHelper.CreateArgumentNullException(ExceptionArgument.array);
			}
			if (start > array.Length)
			{
				return ThrowHelper.CreateArgumentOutOfRangeException(ExceptionArgument.start);
			}
			return ThrowHelper.CreateArgumentOutOfRangeException(ExceptionArgument.length);
		}

		// Token: 0x06008442 RID: 33858 RVA: 0x00269214 File Offset: 0x00269214
		public static void ThrowStartOrEndArgumentValidationException(long start)
		{
			throw ThrowHelper.CreateStartOrEndArgumentValidationException(start);
		}

		// Token: 0x06008443 RID: 33859 RVA: 0x0026921C File Offset: 0x0026921C
		private static Exception CreateStartOrEndArgumentValidationException(long start)
		{
			if (start < 0L)
			{
				return ThrowHelper.CreateArgumentOutOfRangeException(ExceptionArgument.start);
			}
			return ThrowHelper.CreateArgumentOutOfRangeException(ExceptionArgument.length);
		}
	}
}
