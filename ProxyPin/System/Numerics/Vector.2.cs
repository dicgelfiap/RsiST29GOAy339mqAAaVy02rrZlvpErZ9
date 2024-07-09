using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Numerics
{
	// Token: 0x02000D01 RID: 3329
	[Intrinsic]
	[ComVisible(true)]
	public static class Vector
	{
		// Token: 0x0600870F RID: 34575 RVA: 0x0028EBC0 File Offset: 0x0028EBC0
		[CLSCompliant(false)]
		[Intrinsic]
		public unsafe static void Widen(Vector<byte> source, out Vector<ushort> low, out Vector<ushort> high)
		{
			int count = Vector<byte>.Count;
			ushort* ptr = stackalloc ushort[checked(unchecked((UIntPtr)(count / 2)) * 2)];
			for (int i = 0; i < count / 2; i++)
			{
				ptr[i] = (ushort)source[i];
			}
			ushort* ptr2 = stackalloc ushort[checked(unchecked((UIntPtr)(count / 2)) * 2)];
			for (int j = 0; j < count / 2; j++)
			{
				ptr2[j] = (ushort)source[j + count / 2];
			}
			low = new Vector<ushort>((void*)ptr);
			high = new Vector<ushort>((void*)ptr2);
		}

		// Token: 0x06008710 RID: 34576 RVA: 0x0028EC4C File Offset: 0x0028EC4C
		[CLSCompliant(false)]
		[Intrinsic]
		public unsafe static void Widen(Vector<ushort> source, out Vector<uint> low, out Vector<uint> high)
		{
			int count = Vector<ushort>.Count;
			uint* ptr = stackalloc uint[checked(unchecked((UIntPtr)(count / 2)) * 4)];
			for (int i = 0; i < count / 2; i++)
			{
				ptr[i] = (uint)source[i];
			}
			uint* ptr2 = stackalloc uint[checked(unchecked((UIntPtr)(count / 2)) * 4)];
			for (int j = 0; j < count / 2; j++)
			{
				ptr2[j] = (uint)source[j + count / 2];
			}
			low = new Vector<uint>((void*)ptr);
			high = new Vector<uint>((void*)ptr2);
		}

		// Token: 0x06008711 RID: 34577 RVA: 0x0028ECD8 File Offset: 0x0028ECD8
		[CLSCompliant(false)]
		[Intrinsic]
		public unsafe static void Widen(Vector<uint> source, out Vector<ulong> low, out Vector<ulong> high)
		{
			int count = Vector<uint>.Count;
			ulong* ptr = stackalloc ulong[checked(unchecked((UIntPtr)(count / 2)) * 8)];
			for (int i = 0; i < count / 2; i++)
			{
				ptr[i] = (ulong)source[i];
			}
			ulong* ptr2 = stackalloc ulong[checked(unchecked((UIntPtr)(count / 2)) * 8)];
			for (int j = 0; j < count / 2; j++)
			{
				ptr2[j] = (ulong)source[j + count / 2];
			}
			low = new Vector<ulong>((void*)ptr);
			high = new Vector<ulong>((void*)ptr2);
		}

		// Token: 0x06008712 RID: 34578 RVA: 0x0028ED68 File Offset: 0x0028ED68
		[CLSCompliant(false)]
		[Intrinsic]
		public unsafe static void Widen(Vector<sbyte> source, out Vector<short> low, out Vector<short> high)
		{
			int count = Vector<sbyte>.Count;
			short* ptr = stackalloc short[checked(unchecked((UIntPtr)(count / 2)) * 2)];
			for (int i = 0; i < count / 2; i++)
			{
				ptr[i] = (short)source[i];
			}
			short* ptr2 = stackalloc short[checked(unchecked((UIntPtr)(count / 2)) * 2)];
			for (int j = 0; j < count / 2; j++)
			{
				ptr2[j] = (short)source[j + count / 2];
			}
			low = new Vector<short>((void*)ptr);
			high = new Vector<short>((void*)ptr2);
		}

		// Token: 0x06008713 RID: 34579 RVA: 0x0028EDF4 File Offset: 0x0028EDF4
		[Intrinsic]
		public unsafe static void Widen(Vector<short> source, out Vector<int> low, out Vector<int> high)
		{
			int count = Vector<short>.Count;
			int* ptr = stackalloc int[checked(unchecked((UIntPtr)(count / 2)) * 4)];
			for (int i = 0; i < count / 2; i++)
			{
				ptr[i] = (int)source[i];
			}
			int* ptr2 = stackalloc int[checked(unchecked((UIntPtr)(count / 2)) * 4)];
			for (int j = 0; j < count / 2; j++)
			{
				ptr2[j] = (int)source[j + count / 2];
			}
			low = new Vector<int>((void*)ptr);
			high = new Vector<int>((void*)ptr2);
		}

		// Token: 0x06008714 RID: 34580 RVA: 0x0028EE80 File Offset: 0x0028EE80
		[Intrinsic]
		public unsafe static void Widen(Vector<int> source, out Vector<long> low, out Vector<long> high)
		{
			int count = Vector<int>.Count;
			long* ptr = stackalloc long[checked(unchecked((UIntPtr)(count / 2)) * 8)];
			for (int i = 0; i < count / 2; i++)
			{
				ptr[i] = (long)source[i];
			}
			long* ptr2 = stackalloc long[checked(unchecked((UIntPtr)(count / 2)) * 8)];
			for (int j = 0; j < count / 2; j++)
			{
				ptr2[j] = (long)source[j + count / 2];
			}
			low = new Vector<long>((void*)ptr);
			high = new Vector<long>((void*)ptr2);
		}

		// Token: 0x06008715 RID: 34581 RVA: 0x0028EF10 File Offset: 0x0028EF10
		[Intrinsic]
		public unsafe static void Widen(Vector<float> source, out Vector<double> low, out Vector<double> high)
		{
			int count = Vector<float>.Count;
			double* ptr = stackalloc double[checked(unchecked((UIntPtr)(count / 2)) * 8)];
			for (int i = 0; i < count / 2; i++)
			{
				ptr[i] = (double)source[i];
			}
			double* ptr2 = stackalloc double[checked(unchecked((UIntPtr)(count / 2)) * 8)];
			for (int j = 0; j < count / 2; j++)
			{
				ptr2[j] = (double)source[j + count / 2];
			}
			low = new Vector<double>((void*)ptr);
			high = new Vector<double>((void*)ptr2);
		}

		// Token: 0x06008716 RID: 34582 RVA: 0x0028EFA0 File Offset: 0x0028EFA0
		[CLSCompliant(false)]
		[Intrinsic]
		public unsafe static Vector<byte> Narrow(Vector<ushort> low, Vector<ushort> high)
		{
			int count = Vector<byte>.Count;
			byte* ptr = stackalloc byte[(UIntPtr)count];
			for (int i = 0; i < count / 2; i++)
			{
				ptr[i] = (byte)low[i];
			}
			for (int j = 0; j < count / 2; j++)
			{
				ptr[j + count / 2] = (byte)high[j];
			}
			return new Vector<byte>((void*)ptr);
		}

		// Token: 0x06008717 RID: 34583 RVA: 0x0028F004 File Offset: 0x0028F004
		[CLSCompliant(false)]
		[Intrinsic]
		public unsafe static Vector<ushort> Narrow(Vector<uint> low, Vector<uint> high)
		{
			int count = Vector<ushort>.Count;
			ushort* ptr = stackalloc ushort[checked(unchecked((UIntPtr)count) * 2)];
			for (int i = 0; i < count / 2; i++)
			{
				ptr[i] = (ushort)low[i];
			}
			for (int j = 0; j < count / 2; j++)
			{
				ptr[j + count / 2] = (ushort)high[j];
			}
			return new Vector<ushort>((void*)ptr);
		}

		// Token: 0x06008718 RID: 34584 RVA: 0x0028F070 File Offset: 0x0028F070
		[CLSCompliant(false)]
		[Intrinsic]
		public unsafe static Vector<uint> Narrow(Vector<ulong> low, Vector<ulong> high)
		{
			int count = Vector<uint>.Count;
			uint* ptr = stackalloc uint[checked(unchecked((UIntPtr)count) * 4)];
			for (int i = 0; i < count / 2; i++)
			{
				ptr[i] = (uint)low[i];
			}
			for (int j = 0; j < count / 2; j++)
			{
				ptr[j + count / 2] = (uint)high[j];
			}
			return new Vector<uint>((void*)ptr);
		}

		// Token: 0x06008719 RID: 34585 RVA: 0x0028F0DC File Offset: 0x0028F0DC
		[CLSCompliant(false)]
		[Intrinsic]
		public unsafe static Vector<sbyte> Narrow(Vector<short> low, Vector<short> high)
		{
			int count = Vector<sbyte>.Count;
			sbyte* ptr = stackalloc sbyte[(UIntPtr)count];
			for (int i = 0; i < count / 2; i++)
			{
				ptr[i] = (sbyte)low[i];
			}
			for (int j = 0; j < count / 2; j++)
			{
				ptr[j + count / 2] = (sbyte)high[j];
			}
			return new Vector<sbyte>((void*)ptr);
		}

		// Token: 0x0600871A RID: 34586 RVA: 0x0028F140 File Offset: 0x0028F140
		[Intrinsic]
		public unsafe static Vector<short> Narrow(Vector<int> low, Vector<int> high)
		{
			int count = Vector<short>.Count;
			short* ptr = stackalloc short[checked(unchecked((UIntPtr)count) * 2)];
			for (int i = 0; i < count / 2; i++)
			{
				ptr[i] = (short)low[i];
			}
			for (int j = 0; j < count / 2; j++)
			{
				ptr[j + count / 2] = (short)high[j];
			}
			return new Vector<short>((void*)ptr);
		}

		// Token: 0x0600871B RID: 34587 RVA: 0x0028F1AC File Offset: 0x0028F1AC
		[Intrinsic]
		public unsafe static Vector<int> Narrow(Vector<long> low, Vector<long> high)
		{
			int count = Vector<int>.Count;
			int* ptr = stackalloc int[checked(unchecked((UIntPtr)count) * 4)];
			for (int i = 0; i < count / 2; i++)
			{
				ptr[i] = (int)low[i];
			}
			for (int j = 0; j < count / 2; j++)
			{
				ptr[j + count / 2] = (int)high[j];
			}
			return new Vector<int>((void*)ptr);
		}

		// Token: 0x0600871C RID: 34588 RVA: 0x0028F218 File Offset: 0x0028F218
		[Intrinsic]
		public unsafe static Vector<float> Narrow(Vector<double> low, Vector<double> high)
		{
			int count = Vector<float>.Count;
			float* ptr = stackalloc float[checked(unchecked((UIntPtr)count) * 4)];
			for (int i = 0; i < count / 2; i++)
			{
				ptr[i] = (float)low[i];
			}
			for (int j = 0; j < count / 2; j++)
			{
				ptr[j + count / 2] = (float)high[j];
			}
			return new Vector<float>((void*)ptr);
		}

		// Token: 0x0600871D RID: 34589 RVA: 0x0028F284 File Offset: 0x0028F284
		[Intrinsic]
		public unsafe static Vector<float> ConvertToSingle(Vector<int> value)
		{
			int count = Vector<float>.Count;
			float* ptr = stackalloc float[checked(unchecked((UIntPtr)count) * 4)];
			for (int i = 0; i < count; i++)
			{
				ptr[i] = (float)value[i];
			}
			return new Vector<float>((void*)ptr);
		}

		// Token: 0x0600871E RID: 34590 RVA: 0x0028F2C8 File Offset: 0x0028F2C8
		[CLSCompliant(false)]
		[Intrinsic]
		public unsafe static Vector<float> ConvertToSingle(Vector<uint> value)
		{
			int count = Vector<float>.Count;
			float* ptr = stackalloc float[checked(unchecked((UIntPtr)count) * 4)];
			for (int i = 0; i < count; i++)
			{
				ptr[i] = value[i];
			}
			return new Vector<float>((void*)ptr);
		}

		// Token: 0x0600871F RID: 34591 RVA: 0x0028F30C File Offset: 0x0028F30C
		[Intrinsic]
		public unsafe static Vector<double> ConvertToDouble(Vector<long> value)
		{
			int count = Vector<double>.Count;
			double* ptr = stackalloc double[checked(unchecked((UIntPtr)count) * 8)];
			for (int i = 0; i < count; i++)
			{
				ptr[i] = (double)value[i];
			}
			return new Vector<double>((void*)ptr);
		}

		// Token: 0x06008720 RID: 34592 RVA: 0x0028F350 File Offset: 0x0028F350
		[CLSCompliant(false)]
		[Intrinsic]
		public unsafe static Vector<double> ConvertToDouble(Vector<ulong> value)
		{
			int count = Vector<double>.Count;
			double* ptr = stackalloc double[checked(unchecked((UIntPtr)count) * 8)];
			for (int i = 0; i < count; i++)
			{
				ptr[i] = value[i];
			}
			return new Vector<double>((void*)ptr);
		}

		// Token: 0x06008721 RID: 34593 RVA: 0x0028F394 File Offset: 0x0028F394
		[Intrinsic]
		public unsafe static Vector<int> ConvertToInt32(Vector<float> value)
		{
			int count = Vector<int>.Count;
			int* ptr = stackalloc int[checked(unchecked((UIntPtr)count) * 4)];
			for (int i = 0; i < count; i++)
			{
				ptr[i] = (int)value[i];
			}
			return new Vector<int>((void*)ptr);
		}

		// Token: 0x06008722 RID: 34594 RVA: 0x0028F3D8 File Offset: 0x0028F3D8
		[CLSCompliant(false)]
		[Intrinsic]
		public unsafe static Vector<uint> ConvertToUInt32(Vector<float> value)
		{
			int count = Vector<uint>.Count;
			uint* ptr = stackalloc uint[checked(unchecked((UIntPtr)count) * 4)];
			for (int i = 0; i < count; i++)
			{
				ptr[i] = (uint)value[i];
			}
			return new Vector<uint>((void*)ptr);
		}

		// Token: 0x06008723 RID: 34595 RVA: 0x0028F41C File Offset: 0x0028F41C
		[Intrinsic]
		public unsafe static Vector<long> ConvertToInt64(Vector<double> value)
		{
			int count = Vector<long>.Count;
			long* ptr = stackalloc long[checked(unchecked((UIntPtr)count) * 8)];
			for (int i = 0; i < count; i++)
			{
				ptr[i] = (long)value[i];
			}
			return new Vector<long>((void*)ptr);
		}

		// Token: 0x06008724 RID: 34596 RVA: 0x0028F460 File Offset: 0x0028F460
		[CLSCompliant(false)]
		[Intrinsic]
		public unsafe static Vector<ulong> ConvertToUInt64(Vector<double> value)
		{
			int count = Vector<ulong>.Count;
			ulong* ptr = stackalloc ulong[checked(unchecked((UIntPtr)count) * 8)];
			for (int i = 0; i < count; i++)
			{
				ptr[i] = (ulong)value[i];
			}
			return new Vector<ulong>((void*)ptr);
		}

		// Token: 0x06008725 RID: 34597 RVA: 0x0028F4A4 File Offset: 0x0028F4A4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<float> ConditionalSelect(Vector<int> condition, Vector<float> left, Vector<float> right)
		{
			return Vector<float>.ConditionalSelect((Vector<float>)condition, left, right);
		}

		// Token: 0x06008726 RID: 34598 RVA: 0x0028F4B4 File Offset: 0x0028F4B4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<double> ConditionalSelect(Vector<long> condition, Vector<double> left, Vector<double> right)
		{
			return Vector<double>.ConditionalSelect((Vector<double>)condition, left, right);
		}

		// Token: 0x06008727 RID: 34599 RVA: 0x0028F4C4 File Offset: 0x0028F4C4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<T> ConditionalSelect<T>(Vector<T> condition, Vector<T> left, Vector<T> right) where T : struct
		{
			return Vector<T>.ConditionalSelect(condition, left, right);
		}

		// Token: 0x06008728 RID: 34600 RVA: 0x0028F4D0 File Offset: 0x0028F4D0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<T> Equals<T>(Vector<T> left, Vector<T> right) where T : struct
		{
			return Vector<T>.Equals(left, right);
		}

		// Token: 0x06008729 RID: 34601 RVA: 0x0028F4DC File Offset: 0x0028F4DC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<int> Equals(Vector<float> left, Vector<float> right)
		{
			return (Vector<int>)Vector<float>.Equals(left, right);
		}

		// Token: 0x0600872A RID: 34602 RVA: 0x0028F4EC File Offset: 0x0028F4EC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<int> Equals(Vector<int> left, Vector<int> right)
		{
			return Vector<int>.Equals(left, right);
		}

		// Token: 0x0600872B RID: 34603 RVA: 0x0028F4F8 File Offset: 0x0028F4F8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<long> Equals(Vector<double> left, Vector<double> right)
		{
			return (Vector<long>)Vector<double>.Equals(left, right);
		}

		// Token: 0x0600872C RID: 34604 RVA: 0x0028F508 File Offset: 0x0028F508
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<long> Equals(Vector<long> left, Vector<long> right)
		{
			return Vector<long>.Equals(left, right);
		}

		// Token: 0x0600872D RID: 34605 RVA: 0x0028F514 File Offset: 0x0028F514
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool EqualsAll<T>(Vector<T> left, Vector<T> right) where T : struct
		{
			return left == right;
		}

		// Token: 0x0600872E RID: 34606 RVA: 0x0028F520 File Offset: 0x0028F520
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool EqualsAny<T>(Vector<T> left, Vector<T> right) where T : struct
		{
			return !Vector<T>.Equals(left, right).Equals(Vector<T>.Zero);
		}

		// Token: 0x0600872F RID: 34607 RVA: 0x0028F548 File Offset: 0x0028F548
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<T> LessThan<T>(Vector<T> left, Vector<T> right) where T : struct
		{
			return Vector<T>.LessThan(left, right);
		}

		// Token: 0x06008730 RID: 34608 RVA: 0x0028F554 File Offset: 0x0028F554
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<int> LessThan(Vector<float> left, Vector<float> right)
		{
			return (Vector<int>)Vector<float>.LessThan(left, right);
		}

		// Token: 0x06008731 RID: 34609 RVA: 0x0028F564 File Offset: 0x0028F564
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<int> LessThan(Vector<int> left, Vector<int> right)
		{
			return Vector<int>.LessThan(left, right);
		}

		// Token: 0x06008732 RID: 34610 RVA: 0x0028F570 File Offset: 0x0028F570
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<long> LessThan(Vector<double> left, Vector<double> right)
		{
			return (Vector<long>)Vector<double>.LessThan(left, right);
		}

		// Token: 0x06008733 RID: 34611 RVA: 0x0028F580 File Offset: 0x0028F580
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<long> LessThan(Vector<long> left, Vector<long> right)
		{
			return Vector<long>.LessThan(left, right);
		}

		// Token: 0x06008734 RID: 34612 RVA: 0x0028F58C File Offset: 0x0028F58C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool LessThanAll<T>(Vector<T> left, Vector<T> right) where T : struct
		{
			return ((Vector<int>)Vector<T>.LessThan(left, right)).Equals(Vector<int>.AllOnes);
		}

		// Token: 0x06008735 RID: 34613 RVA: 0x0028F5B8 File Offset: 0x0028F5B8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool LessThanAny<T>(Vector<T> left, Vector<T> right) where T : struct
		{
			return !((Vector<int>)Vector<T>.LessThan(left, right)).Equals(Vector<int>.Zero);
		}

		// Token: 0x06008736 RID: 34614 RVA: 0x0028F5E8 File Offset: 0x0028F5E8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<T> LessThanOrEqual<T>(Vector<T> left, Vector<T> right) where T : struct
		{
			return Vector<T>.LessThanOrEqual(left, right);
		}

		// Token: 0x06008737 RID: 34615 RVA: 0x0028F5F4 File Offset: 0x0028F5F4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<int> LessThanOrEqual(Vector<float> left, Vector<float> right)
		{
			return (Vector<int>)Vector<float>.LessThanOrEqual(left, right);
		}

		// Token: 0x06008738 RID: 34616 RVA: 0x0028F604 File Offset: 0x0028F604
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<int> LessThanOrEqual(Vector<int> left, Vector<int> right)
		{
			return Vector<int>.LessThanOrEqual(left, right);
		}

		// Token: 0x06008739 RID: 34617 RVA: 0x0028F610 File Offset: 0x0028F610
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<long> LessThanOrEqual(Vector<long> left, Vector<long> right)
		{
			return Vector<long>.LessThanOrEqual(left, right);
		}

		// Token: 0x0600873A RID: 34618 RVA: 0x0028F61C File Offset: 0x0028F61C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<long> LessThanOrEqual(Vector<double> left, Vector<double> right)
		{
			return (Vector<long>)Vector<double>.LessThanOrEqual(left, right);
		}

		// Token: 0x0600873B RID: 34619 RVA: 0x0028F62C File Offset: 0x0028F62C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool LessThanOrEqualAll<T>(Vector<T> left, Vector<T> right) where T : struct
		{
			return ((Vector<int>)Vector<T>.LessThanOrEqual(left, right)).Equals(Vector<int>.AllOnes);
		}

		// Token: 0x0600873C RID: 34620 RVA: 0x0028F658 File Offset: 0x0028F658
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool LessThanOrEqualAny<T>(Vector<T> left, Vector<T> right) where T : struct
		{
			return !((Vector<int>)Vector<T>.LessThanOrEqual(left, right)).Equals(Vector<int>.Zero);
		}

		// Token: 0x0600873D RID: 34621 RVA: 0x0028F688 File Offset: 0x0028F688
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<T> GreaterThan<T>(Vector<T> left, Vector<T> right) where T : struct
		{
			return Vector<T>.GreaterThan(left, right);
		}

		// Token: 0x0600873E RID: 34622 RVA: 0x0028F694 File Offset: 0x0028F694
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<int> GreaterThan(Vector<float> left, Vector<float> right)
		{
			return (Vector<int>)Vector<float>.GreaterThan(left, right);
		}

		// Token: 0x0600873F RID: 34623 RVA: 0x0028F6A4 File Offset: 0x0028F6A4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<int> GreaterThan(Vector<int> left, Vector<int> right)
		{
			return Vector<int>.GreaterThan(left, right);
		}

		// Token: 0x06008740 RID: 34624 RVA: 0x0028F6B0 File Offset: 0x0028F6B0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<long> GreaterThan(Vector<double> left, Vector<double> right)
		{
			return (Vector<long>)Vector<double>.GreaterThan(left, right);
		}

		// Token: 0x06008741 RID: 34625 RVA: 0x0028F6C0 File Offset: 0x0028F6C0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<long> GreaterThan(Vector<long> left, Vector<long> right)
		{
			return Vector<long>.GreaterThan(left, right);
		}

		// Token: 0x06008742 RID: 34626 RVA: 0x0028F6CC File Offset: 0x0028F6CC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool GreaterThanAll<T>(Vector<T> left, Vector<T> right) where T : struct
		{
			return ((Vector<int>)Vector<T>.GreaterThan(left, right)).Equals(Vector<int>.AllOnes);
		}

		// Token: 0x06008743 RID: 34627 RVA: 0x0028F6F8 File Offset: 0x0028F6F8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool GreaterThanAny<T>(Vector<T> left, Vector<T> right) where T : struct
		{
			return !((Vector<int>)Vector<T>.GreaterThan(left, right)).Equals(Vector<int>.Zero);
		}

		// Token: 0x06008744 RID: 34628 RVA: 0x0028F728 File Offset: 0x0028F728
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<T> GreaterThanOrEqual<T>(Vector<T> left, Vector<T> right) where T : struct
		{
			return Vector<T>.GreaterThanOrEqual(left, right);
		}

		// Token: 0x06008745 RID: 34629 RVA: 0x0028F734 File Offset: 0x0028F734
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<int> GreaterThanOrEqual(Vector<float> left, Vector<float> right)
		{
			return (Vector<int>)Vector<float>.GreaterThanOrEqual(left, right);
		}

		// Token: 0x06008746 RID: 34630 RVA: 0x0028F744 File Offset: 0x0028F744
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<int> GreaterThanOrEqual(Vector<int> left, Vector<int> right)
		{
			return Vector<int>.GreaterThanOrEqual(left, right);
		}

		// Token: 0x06008747 RID: 34631 RVA: 0x0028F750 File Offset: 0x0028F750
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<long> GreaterThanOrEqual(Vector<long> left, Vector<long> right)
		{
			return Vector<long>.GreaterThanOrEqual(left, right);
		}

		// Token: 0x06008748 RID: 34632 RVA: 0x0028F75C File Offset: 0x0028F75C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<long> GreaterThanOrEqual(Vector<double> left, Vector<double> right)
		{
			return (Vector<long>)Vector<double>.GreaterThanOrEqual(left, right);
		}

		// Token: 0x06008749 RID: 34633 RVA: 0x0028F76C File Offset: 0x0028F76C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool GreaterThanOrEqualAll<T>(Vector<T> left, Vector<T> right) where T : struct
		{
			return ((Vector<int>)Vector<T>.GreaterThanOrEqual(left, right)).Equals(Vector<int>.AllOnes);
		}

		// Token: 0x0600874A RID: 34634 RVA: 0x0028F798 File Offset: 0x0028F798
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool GreaterThanOrEqualAny<T>(Vector<T> left, Vector<T> right) where T : struct
		{
			return !((Vector<int>)Vector<T>.GreaterThanOrEqual(left, right)).Equals(Vector<int>.Zero);
		}

		// Token: 0x17001CBA RID: 7354
		// (get) Token: 0x0600874B RID: 34635 RVA: 0x0028F7C8 File Offset: 0x0028F7C8
		public static bool IsHardwareAccelerated
		{
			[Intrinsic]
			get
			{
				return false;
			}
		}

		// Token: 0x0600874C RID: 34636 RVA: 0x0028F7CC File Offset: 0x0028F7CC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<T> Abs<T>(Vector<T> value) where T : struct
		{
			return Vector<T>.Abs(value);
		}

		// Token: 0x0600874D RID: 34637 RVA: 0x0028F7D4 File Offset: 0x0028F7D4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<T> Min<T>(Vector<T> left, Vector<T> right) where T : struct
		{
			return Vector<T>.Min(left, right);
		}

		// Token: 0x0600874E RID: 34638 RVA: 0x0028F7E0 File Offset: 0x0028F7E0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<T> Max<T>(Vector<T> left, Vector<T> right) where T : struct
		{
			return Vector<T>.Max(left, right);
		}

		// Token: 0x0600874F RID: 34639 RVA: 0x0028F7EC File Offset: 0x0028F7EC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static T Dot<T>(Vector<T> left, Vector<T> right) where T : struct
		{
			return Vector<T>.DotProduct(left, right);
		}

		// Token: 0x06008750 RID: 34640 RVA: 0x0028F7F8 File Offset: 0x0028F7F8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<T> SquareRoot<T>(Vector<T> value) where T : struct
		{
			return Vector<T>.SquareRoot(value);
		}

		// Token: 0x06008751 RID: 34641 RVA: 0x0028F800 File Offset: 0x0028F800
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<T> Add<T>(Vector<T> left, Vector<T> right) where T : struct
		{
			return left + right;
		}

		// Token: 0x06008752 RID: 34642 RVA: 0x0028F80C File Offset: 0x0028F80C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<T> Subtract<T>(Vector<T> left, Vector<T> right) where T : struct
		{
			return left - right;
		}

		// Token: 0x06008753 RID: 34643 RVA: 0x0028F818 File Offset: 0x0028F818
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<T> Multiply<T>(Vector<T> left, Vector<T> right) where T : struct
		{
			return left * right;
		}

		// Token: 0x06008754 RID: 34644 RVA: 0x0028F824 File Offset: 0x0028F824
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<T> Multiply<T>(Vector<T> left, T right) where T : struct
		{
			return left * right;
		}

		// Token: 0x06008755 RID: 34645 RVA: 0x0028F830 File Offset: 0x0028F830
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<T> Multiply<T>(T left, Vector<T> right) where T : struct
		{
			return left * right;
		}

		// Token: 0x06008756 RID: 34646 RVA: 0x0028F83C File Offset: 0x0028F83C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<T> Divide<T>(Vector<T> left, Vector<T> right) where T : struct
		{
			return left / right;
		}

		// Token: 0x06008757 RID: 34647 RVA: 0x0028F848 File Offset: 0x0028F848
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<T> Negate<T>(Vector<T> value) where T : struct
		{
			return -value;
		}

		// Token: 0x06008758 RID: 34648 RVA: 0x0028F850 File Offset: 0x0028F850
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<T> BitwiseAnd<T>(Vector<T> left, Vector<T> right) where T : struct
		{
			return left & right;
		}

		// Token: 0x06008759 RID: 34649 RVA: 0x0028F85C File Offset: 0x0028F85C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<T> BitwiseOr<T>(Vector<T> left, Vector<T> right) where T : struct
		{
			return left | right;
		}

		// Token: 0x0600875A RID: 34650 RVA: 0x0028F868 File Offset: 0x0028F868
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<T> OnesComplement<T>(Vector<T> value) where T : struct
		{
			return ~value;
		}

		// Token: 0x0600875B RID: 34651 RVA: 0x0028F870 File Offset: 0x0028F870
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<T> Xor<T>(Vector<T> left, Vector<T> right) where T : struct
		{
			return left ^ right;
		}

		// Token: 0x0600875C RID: 34652 RVA: 0x0028F87C File Offset: 0x0028F87C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<T> AndNot<T>(Vector<T> left, Vector<T> right) where T : struct
		{
			return left & ~right;
		}

		// Token: 0x0600875D RID: 34653 RVA: 0x0028F88C File Offset: 0x0028F88C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<byte> AsVectorByte<T>(Vector<T> value) where T : struct
		{
			return (Vector<byte>)value;
		}

		// Token: 0x0600875E RID: 34654 RVA: 0x0028F894 File Offset: 0x0028F894
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<sbyte> AsVectorSByte<T>(Vector<T> value) where T : struct
		{
			return (Vector<sbyte>)value;
		}

		// Token: 0x0600875F RID: 34655 RVA: 0x0028F89C File Offset: 0x0028F89C
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<ushort> AsVectorUInt16<T>(Vector<T> value) where T : struct
		{
			return (Vector<ushort>)value;
		}

		// Token: 0x06008760 RID: 34656 RVA: 0x0028F8A4 File Offset: 0x0028F8A4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<short> AsVectorInt16<T>(Vector<T> value) where T : struct
		{
			return (Vector<short>)value;
		}

		// Token: 0x06008761 RID: 34657 RVA: 0x0028F8AC File Offset: 0x0028F8AC
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<uint> AsVectorUInt32<T>(Vector<T> value) where T : struct
		{
			return (Vector<uint>)value;
		}

		// Token: 0x06008762 RID: 34658 RVA: 0x0028F8B4 File Offset: 0x0028F8B4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<int> AsVectorInt32<T>(Vector<T> value) where T : struct
		{
			return (Vector<int>)value;
		}

		// Token: 0x06008763 RID: 34659 RVA: 0x0028F8BC File Offset: 0x0028F8BC
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<ulong> AsVectorUInt64<T>(Vector<T> value) where T : struct
		{
			return (Vector<ulong>)value;
		}

		// Token: 0x06008764 RID: 34660 RVA: 0x0028F8C4 File Offset: 0x0028F8C4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<long> AsVectorInt64<T>(Vector<T> value) where T : struct
		{
			return (Vector<long>)value;
		}

		// Token: 0x06008765 RID: 34661 RVA: 0x0028F8CC File Offset: 0x0028F8CC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<float> AsVectorSingle<T>(Vector<T> value) where T : struct
		{
			return (Vector<float>)value;
		}

		// Token: 0x06008766 RID: 34662 RVA: 0x0028F8D4 File Offset: 0x0028F8D4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<double> AsVectorDouble<T>(Vector<T> value) where T : struct
		{
			return (Vector<double>)value;
		}
	}
}
