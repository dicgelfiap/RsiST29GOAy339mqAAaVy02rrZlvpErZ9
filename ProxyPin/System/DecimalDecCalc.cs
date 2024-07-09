using System;

namespace System
{
	// Token: 0x02000CD1 RID: 3281
	internal static class DecimalDecCalc
	{
		// Token: 0x06008444 RID: 33860 RVA: 0x00269234 File Offset: 0x00269234
		private static uint D32DivMod1E9(uint hi32, ref uint lo32)
		{
			ulong num = (ulong)hi32 << 32 | (ulong)lo32;
			lo32 = (uint)(num / 1000000000UL);
			return (uint)(num % 1000000000UL);
		}

		// Token: 0x06008445 RID: 33861 RVA: 0x00269264 File Offset: 0x00269264
		internal static uint DecDivMod1E9(ref MutableDecimal value)
		{
			return DecimalDecCalc.D32DivMod1E9(DecimalDecCalc.D32DivMod1E9(DecimalDecCalc.D32DivMod1E9(0U, ref value.High), ref value.Mid), ref value.Low);
		}

		// Token: 0x06008446 RID: 33862 RVA: 0x00269288 File Offset: 0x00269288
		internal static void DecAddInt32(ref MutableDecimal value, uint i)
		{
			if (DecimalDecCalc.D32AddCarry(ref value.Low, i) && DecimalDecCalc.D32AddCarry(ref value.Mid, 1U))
			{
				DecimalDecCalc.D32AddCarry(ref value.High, 1U);
			}
		}

		// Token: 0x06008447 RID: 33863 RVA: 0x002692BC File Offset: 0x002692BC
		private static bool D32AddCarry(ref uint value, uint i)
		{
			uint num = value;
			uint num2 = num + i;
			value = num2;
			return num2 < num || num2 < i;
		}

		// Token: 0x06008448 RID: 33864 RVA: 0x002692E4 File Offset: 0x002692E4
		internal static void DecMul10(ref MutableDecimal value)
		{
			MutableDecimal d = value;
			DecimalDecCalc.DecShiftLeft(ref value);
			DecimalDecCalc.DecShiftLeft(ref value);
			DecimalDecCalc.DecAdd(ref value, d);
			DecimalDecCalc.DecShiftLeft(ref value);
		}

		// Token: 0x06008449 RID: 33865 RVA: 0x00269318 File Offset: 0x00269318
		private static void DecShiftLeft(ref MutableDecimal value)
		{
			uint num = ((value.Low & 2147483648U) != 0U) ? 1U : 0U;
			uint num2 = ((value.Mid & 2147483648U) != 0U) ? 1U : 0U;
			value.Low <<= 1;
			value.Mid = (value.Mid << 1 | num);
			value.High = (value.High << 1 | num2);
		}

		// Token: 0x0600844A RID: 33866 RVA: 0x0026938C File Offset: 0x0026938C
		private static void DecAdd(ref MutableDecimal value, MutableDecimal d)
		{
			if (DecimalDecCalc.D32AddCarry(ref value.Low, d.Low) && DecimalDecCalc.D32AddCarry(ref value.Mid, 1U))
			{
				DecimalDecCalc.D32AddCarry(ref value.High, 1U);
			}
			if (DecimalDecCalc.D32AddCarry(ref value.Mid, d.Mid))
			{
				DecimalDecCalc.D32AddCarry(ref value.High, 1U);
			}
			DecimalDecCalc.D32AddCarry(ref value.High, d.High);
		}
	}
}
