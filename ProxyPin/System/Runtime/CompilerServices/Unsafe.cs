using System;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000D03 RID: 3331
	[ComVisible(true)]
	public static class Unsafe
	{
		// Token: 0x06008769 RID: 34665 RVA: 0x0028F928 File Offset: 0x0028F928
		[System.Runtime.CompilerServices.Unsafe2629577.NonVersionable]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static T Read<T>(void* source)
		{
			return *(T*)source;
		}

		// Token: 0x0600876A RID: 34666 RVA: 0x0028F930 File Offset: 0x0028F930
		[System.Runtime.CompilerServices.Unsafe2629577.NonVersionable]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static T ReadUnaligned<T>(void* source)
		{
			return *(T*)source;
		}

		// Token: 0x0600876B RID: 34667 RVA: 0x0028F93C File Offset: 0x0028F93C
		[System.Runtime.CompilerServices.Unsafe2629577.NonVersionable]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static T ReadUnaligned<T>(ref byte source)
		{
			return source;
		}

		// Token: 0x0600876C RID: 34668 RVA: 0x0028F948 File Offset: 0x0028F948
		[System.Runtime.CompilerServices.Unsafe2629577.NonVersionable]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void Write<T>(void* destination, T value)
		{
			*(T*)destination = value;
		}

		// Token: 0x0600876D RID: 34669 RVA: 0x0028F954 File Offset: 0x0028F954
		[System.Runtime.CompilerServices.Unsafe2629577.NonVersionable]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void WriteUnaligned<T>(void* destination, T value)
		{
			*(T*)destination = value;
		}

		// Token: 0x0600876E RID: 34670 RVA: 0x0028F960 File Offset: 0x0028F960
		[System.Runtime.CompilerServices.Unsafe2629577.NonVersionable]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void WriteUnaligned<T>(ref byte destination, T value)
		{
			destination = value;
		}

		// Token: 0x0600876F RID: 34671 RVA: 0x0028F96C File Offset: 0x0028F96C
		[System.Runtime.CompilerServices.Unsafe2629577.NonVersionable]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void Copy<T>(void* destination, ref T source)
		{
			*(T*)destination = source;
		}

		// Token: 0x06008770 RID: 34672 RVA: 0x0028F97C File Offset: 0x0028F97C
		[System.Runtime.CompilerServices.Unsafe2629577.NonVersionable]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void Copy<T>(ref T destination, void* source)
		{
			destination = *(T*)source;
		}

		// Token: 0x06008771 RID: 34673 RVA: 0x0028F98C File Offset: 0x0028F98C
		[System.Runtime.CompilerServices.Unsafe2629577.NonVersionable]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void* AsPointer<T>(ref T value)
		{
			return (void*)(&value);
		}

		// Token: 0x06008772 RID: 34674 RVA: 0x0028F990 File Offset: 0x0028F990
		[System.Runtime.CompilerServices.Unsafe2629577.NonVersionable]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int SizeOf<T>()
		{
			return sizeof(T);
		}

		// Token: 0x06008773 RID: 34675 RVA: 0x0028F998 File Offset: 0x0028F998
		[System.Runtime.CompilerServices.Unsafe2629577.NonVersionable]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void CopyBlock(void* destination, void* source, uint byteCount)
		{
			cpblk(destination, source, byteCount);
		}

		// Token: 0x06008774 RID: 34676 RVA: 0x0028F9A0 File Offset: 0x0028F9A0
		[System.Runtime.CompilerServices.Unsafe2629577.NonVersionable]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void CopyBlock(ref byte destination, ref byte source, uint byteCount)
		{
			cpblk(ref destination, ref source, byteCount);
		}

		// Token: 0x06008775 RID: 34677 RVA: 0x0028F9A8 File Offset: 0x0028F9A8
		[System.Runtime.CompilerServices.Unsafe2629577.NonVersionable]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void CopyBlockUnaligned(void* destination, void* source, uint byteCount)
		{
			cpblk(destination, source, byteCount);
		}

		// Token: 0x06008776 RID: 34678 RVA: 0x0028F9B0 File Offset: 0x0028F9B0
		[System.Runtime.CompilerServices.Unsafe2629577.NonVersionable]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void CopyBlockUnaligned(ref byte destination, ref byte source, uint byteCount)
		{
			cpblk(ref destination, ref source, byteCount);
		}

		// Token: 0x06008777 RID: 34679 RVA: 0x0028F9B8 File Offset: 0x0028F9B8
		[System.Runtime.CompilerServices.Unsafe2629577.NonVersionable]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void InitBlock(void* startAddress, byte value, uint byteCount)
		{
			initblk(startAddress, value, byteCount);
		}

		// Token: 0x06008778 RID: 34680 RVA: 0x0028F9C0 File Offset: 0x0028F9C0
		[System.Runtime.CompilerServices.Unsafe2629577.NonVersionable]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void InitBlock(ref byte startAddress, byte value, uint byteCount)
		{
			initblk(ref startAddress, value, byteCount);
		}

		// Token: 0x06008779 RID: 34681 RVA: 0x0028F9C8 File Offset: 0x0028F9C8
		[System.Runtime.CompilerServices.Unsafe2629577.NonVersionable]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void InitBlockUnaligned(void* startAddress, byte value, uint byteCount)
		{
			initblk(startAddress, value, byteCount);
		}

		// Token: 0x0600877A RID: 34682 RVA: 0x0028F9D0 File Offset: 0x0028F9D0
		[System.Runtime.CompilerServices.Unsafe2629577.NonVersionable]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void InitBlockUnaligned(ref byte startAddress, byte value, uint byteCount)
		{
			initblk(ref startAddress, value, byteCount);
		}

		// Token: 0x0600877B RID: 34683 RVA: 0x0028F9D8 File Offset: 0x0028F9D8
		[System.Runtime.CompilerServices.Unsafe2629577.NonVersionable]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static T As<T>(object o) where T : class
		{
			return o;
		}

		// Token: 0x0600877C RID: 34684 RVA: 0x0028F9DC File Offset: 0x0028F9DC
		[System.Runtime.CompilerServices.Unsafe2629577.NonVersionable]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static ref T AsRef<T>(void* source)
		{
			return ref *(T*)source;
		}

		// Token: 0x0600877D RID: 34685 RVA: 0x0028F9F0 File Offset: 0x0028F9F0
		[System.Runtime.CompilerServices.Unsafe2629577.NonVersionable]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ref T AsRef<T>([System.Runtime.CompilerServices.Unsafe2629577.IsReadOnly] ref T source)
		{
			return ref source;
		}

		// Token: 0x0600877E RID: 34686 RVA: 0x0028F9F4 File Offset: 0x0028F9F4
		[System.Runtime.CompilerServices.Unsafe2629577.NonVersionable]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ref TTo As<TFrom, TTo>(ref TFrom source)
		{
			return ref source;
		}

		// Token: 0x0600877F RID: 34687 RVA: 0x0028F9F8 File Offset: 0x0028F9F8
		[System.Runtime.CompilerServices.Unsafe2629577.NonVersionable]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ref T Add<T>(ref T source, int elementOffset)
		{
			return ref source + (IntPtr)elementOffset * (IntPtr)sizeof(T);
		}

		// Token: 0x06008780 RID: 34688 RVA: 0x0028FA08 File Offset: 0x0028FA08
		[System.Runtime.CompilerServices.Unsafe2629577.NonVersionable]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void* Add<T>(void* source, int elementOffset)
		{
			return (void*)((byte*)source + (IntPtr)elementOffset * (IntPtr)sizeof(T));
		}

		// Token: 0x06008781 RID: 34689 RVA: 0x0028FA18 File Offset: 0x0028FA18
		[System.Runtime.CompilerServices.Unsafe2629577.NonVersionable]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ref T Add<T>(ref T source, IntPtr elementOffset)
		{
			return ref source + elementOffset * (IntPtr)sizeof(T);
		}

		// Token: 0x06008782 RID: 34690 RVA: 0x0028FA24 File Offset: 0x0028FA24
		[System.Runtime.CompilerServices.Unsafe2629577.NonVersionable]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ref T AddByteOffset<T>(ref T source, IntPtr byteOffset)
		{
			return ref source + byteOffset;
		}

		// Token: 0x06008783 RID: 34691 RVA: 0x0028FA2C File Offset: 0x0028FA2C
		[System.Runtime.CompilerServices.Unsafe2629577.NonVersionable]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ref T Subtract<T>(ref T source, int elementOffset)
		{
			return ref source - (IntPtr)elementOffset * (IntPtr)sizeof(T);
		}

		// Token: 0x06008784 RID: 34692 RVA: 0x0028FA3C File Offset: 0x0028FA3C
		[System.Runtime.CompilerServices.Unsafe2629577.NonVersionable]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void* Subtract<T>(void* source, int elementOffset)
		{
			return (void*)((byte*)source - (IntPtr)elementOffset * (IntPtr)sizeof(T));
		}

		// Token: 0x06008785 RID: 34693 RVA: 0x0028FA4C File Offset: 0x0028FA4C
		[System.Runtime.CompilerServices.Unsafe2629577.NonVersionable]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ref T Subtract<T>(ref T source, IntPtr elementOffset)
		{
			return ref source - elementOffset * (IntPtr)sizeof(T);
		}

		// Token: 0x06008786 RID: 34694 RVA: 0x0028FA58 File Offset: 0x0028FA58
		[System.Runtime.CompilerServices.Unsafe2629577.NonVersionable]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ref T SubtractByteOffset<T>(ref T source, IntPtr byteOffset)
		{
			return ref source - byteOffset;
		}

		// Token: 0x06008787 RID: 34695 RVA: 0x0028FA60 File Offset: 0x0028FA60
		[System.Runtime.CompilerServices.Unsafe2629577.NonVersionable]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IntPtr ByteOffset<T>(ref T origin, ref T target)
		{
			return ref target - ref origin;
		}

		// Token: 0x06008788 RID: 34696 RVA: 0x0028FA68 File Offset: 0x0028FA68
		[System.Runtime.CompilerServices.Unsafe2629577.NonVersionable]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AreSame<T>(ref T left, ref T right)
		{
			return ref left == ref right;
		}

		// Token: 0x06008789 RID: 34697 RVA: 0x0028FA70 File Offset: 0x0028FA70
		[System.Runtime.CompilerServices.Unsafe2629577.NonVersionable]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsAddressGreaterThan<T>(ref T left, ref T right)
		{
			return ref left != ref right;
		}

		// Token: 0x0600878A RID: 34698 RVA: 0x0028FA78 File Offset: 0x0028FA78
		[System.Runtime.CompilerServices.Unsafe2629577.NonVersionable]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsAddressLessThan<T>(ref T left, ref T right)
		{
			return ref left < ref right;
		}
	}
}
