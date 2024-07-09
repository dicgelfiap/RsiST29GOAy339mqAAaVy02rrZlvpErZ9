using System;
using System.Runtime.CompilerServices;

namespace System.Numerics
{
	// Token: 0x02000CFE RID: 3326
	internal class ConstantHelper
	{
		// Token: 0x060086C4 RID: 34500 RVA: 0x0027B6D8 File Offset: 0x0027B6D8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static byte GetByteWithAllBitsSet()
		{
			byte result = 0;
			*(&result) = byte.MaxValue;
			return result;
		}

		// Token: 0x060086C5 RID: 34501 RVA: 0x0027B6F8 File Offset: 0x0027B6F8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static sbyte GetSByteWithAllBitsSet()
		{
			sbyte result = 0;
			*(&result) = -1;
			return result;
		}

		// Token: 0x060086C6 RID: 34502 RVA: 0x0027B714 File Offset: 0x0027B714
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static ushort GetUInt16WithAllBitsSet()
		{
			ushort result = 0;
			*(&result) = ushort.MaxValue;
			return result;
		}

		// Token: 0x060086C7 RID: 34503 RVA: 0x0027B734 File Offset: 0x0027B734
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static short GetInt16WithAllBitsSet()
		{
			short result = 0;
			*(&result) = -1;
			return result;
		}

		// Token: 0x060086C8 RID: 34504 RVA: 0x0027B750 File Offset: 0x0027B750
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static uint GetUInt32WithAllBitsSet()
		{
			uint result = 0U;
			*(&result) = uint.MaxValue;
			return result;
		}

		// Token: 0x060086C9 RID: 34505 RVA: 0x0027B76C File Offset: 0x0027B76C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static int GetInt32WithAllBitsSet()
		{
			int result = 0;
			*(&result) = -1;
			return result;
		}

		// Token: 0x060086CA RID: 34506 RVA: 0x0027B788 File Offset: 0x0027B788
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static ulong GetUInt64WithAllBitsSet()
		{
			ulong result = 0UL;
			*(&result) = ulong.MaxValue;
			return result;
		}

		// Token: 0x060086CB RID: 34507 RVA: 0x0027B7A4 File Offset: 0x0027B7A4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static long GetInt64WithAllBitsSet()
		{
			long result = 0L;
			*(&result) = -1L;
			return result;
		}

		// Token: 0x060086CC RID: 34508 RVA: 0x0027B7C0 File Offset: 0x0027B7C0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static float GetSingleWithAllBitsSet()
		{
			float result = 0f;
			*(int*)(&result) = -1;
			return result;
		}

		// Token: 0x060086CD RID: 34509 RVA: 0x0027B7E0 File Offset: 0x0027B7E0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static double GetDoubleWithAllBitsSet()
		{
			double result = 0.0;
			*(long*)(&result) = -1L;
			return result;
		}
	}
}
