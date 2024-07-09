using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Buffers.Text
{
	// Token: 0x02000CF4 RID: 3316
	internal static class FormattingHelpers
	{
		// Token: 0x06008600 RID: 34304 RVA: 0x00273E4C File Offset: 0x00273E4C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static char GetSymbolOrDefault([System.Memory.IsReadOnly] [In] ref StandardFormat format, char defaultSymbol)
		{
			char c = format.Symbol;
			if (c == '\0' && format.Precision == 0)
			{
				c = defaultSymbol;
			}
			return c;
		}

		// Token: 0x06008601 RID: 34305 RVA: 0x00273E78 File Offset: 0x00273E78
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void FillWithAsciiZeros(Span<byte> buffer)
		{
			for (int i = 0; i < buffer.Length; i++)
			{
				*buffer[i] = 48;
			}
		}

		// Token: 0x06008602 RID: 34306 RVA: 0x00273EAC File Offset: 0x00273EAC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void WriteHexByte(byte value, Span<byte> buffer, int startingIndex = 0, FormattingHelpers.HexCasing casing = FormattingHelpers.HexCasing.Uppercase)
		{
			uint num = (uint)(((int)(value & 240) << 4) + (int)(value & 15) - 35209);
			uint num2 = (uint)(((-num & 28784U) >> 4) + num + (FormattingHelpers.HexCasing)47545U | casing);
			*buffer[startingIndex + 1] = (byte)num2;
			*buffer[startingIndex] = (byte)(num2 >> 8);
		}

		// Token: 0x06008603 RID: 34307 RVA: 0x00273F04 File Offset: 0x00273F04
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void WriteDigits(ulong value, Span<byte> buffer)
		{
			for (int i = buffer.Length - 1; i >= 1; i--)
			{
				ulong num = 48UL + value;
				value /= 10UL;
				*buffer[i] = (byte)(num - value * 10UL);
			}
			*buffer[0] = (byte)(48UL + value);
		}

		// Token: 0x06008604 RID: 34308 RVA: 0x00273F5C File Offset: 0x00273F5C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void WriteDigitsWithGroupSeparator(ulong value, Span<byte> buffer)
		{
			int num = 0;
			for (int i = buffer.Length - 1; i >= 1; i--)
			{
				ulong num2 = 48UL + value;
				value /= 10UL;
				*buffer[i] = (byte)(num2 - value * 10UL);
				if (num == 2)
				{
					*buffer[--i] = 44;
					num = 0;
				}
				else
				{
					num++;
				}
			}
			*buffer[0] = (byte)(48UL + value);
		}

		// Token: 0x06008605 RID: 34309 RVA: 0x00273FD4 File Offset: 0x00273FD4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void WriteDigits(uint value, Span<byte> buffer)
		{
			for (int i = buffer.Length - 1; i >= 1; i--)
			{
				uint num = 48U + value;
				value /= 10U;
				*buffer[i] = (byte)(num - value * 10U);
			}
			*buffer[0] = (byte)(48U + value);
		}

		// Token: 0x06008606 RID: 34310 RVA: 0x00274028 File Offset: 0x00274028
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void WriteFourDecimalDigits(uint value, Span<byte> buffer, int startingIndex = 0)
		{
			uint num = 48U + value;
			value /= 10U;
			*buffer[startingIndex + 3] = (byte)(num - value * 10U);
			num = 48U + value;
			value /= 10U;
			*buffer[startingIndex + 2] = (byte)(num - value * 10U);
			num = 48U + value;
			value /= 10U;
			*buffer[startingIndex + 1] = (byte)(num - value * 10U);
			*buffer[startingIndex] = (byte)(48U + value);
		}

		// Token: 0x06008607 RID: 34311 RVA: 0x002740A0 File Offset: 0x002740A0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void WriteTwoDecimalDigits(uint value, Span<byte> buffer, int startingIndex = 0)
		{
			uint num = 48U + value;
			value /= 10U;
			*buffer[startingIndex + 1] = (byte)(num - value * 10U);
			*buffer[startingIndex] = (byte)(48U + value);
		}

		// Token: 0x06008608 RID: 34312 RVA: 0x002740DC File Offset: 0x002740DC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ulong DivMod(ulong numerator, ulong denominator, out ulong modulo)
		{
			ulong num = numerator / denominator;
			modulo = numerator - num * denominator;
			return num;
		}

		// Token: 0x06008609 RID: 34313 RVA: 0x002740FC File Offset: 0x002740FC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint DivMod(uint numerator, uint denominator, out uint modulo)
		{
			uint num = numerator / denominator;
			modulo = numerator - num * denominator;
			return num;
		}

		// Token: 0x0600860A RID: 34314 RVA: 0x0027411C File Offset: 0x0027411C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int CountDecimalTrailingZeros(uint value, out uint valueWithoutTrailingZeros)
		{
			int num = 0;
			if (value != 0U)
			{
				for (;;)
				{
					uint num3;
					uint num2 = FormattingHelpers.DivMod(value, 10U, out num3);
					if (num3 != 0U)
					{
						break;
					}
					value = num2;
					num++;
				}
			}
			valueWithoutTrailingZeros = value;
			return num;
		}

		// Token: 0x0600860B RID: 34315 RVA: 0x00274154 File Offset: 0x00274154
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int CountDigits(ulong value)
		{
			int num = 1;
			uint num2;
			if (value >= 10000000UL)
			{
				if (value >= 100000000000000UL)
				{
					num2 = (uint)(value / 100000000000000UL);
					num += 14;
				}
				else
				{
					num2 = (uint)(value / 10000000UL);
					num += 7;
				}
			}
			else
			{
				num2 = (uint)value;
			}
			if (num2 >= 10U)
			{
				if (num2 < 100U)
				{
					num++;
				}
				else if (num2 < 1000U)
				{
					num += 2;
				}
				else if (num2 < 10000U)
				{
					num += 3;
				}
				else if (num2 < 100000U)
				{
					num += 4;
				}
				else if (num2 < 1000000U)
				{
					num += 5;
				}
				else
				{
					num += 6;
				}
			}
			return num;
		}

		// Token: 0x0600860C RID: 34316 RVA: 0x00274220 File Offset: 0x00274220
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int CountDigits(uint value)
		{
			int num = 1;
			if (value >= 100000U)
			{
				value /= 100000U;
				num += 5;
			}
			if (value >= 10U)
			{
				if (value < 100U)
				{
					num++;
				}
				else if (value < 1000U)
				{
					num += 2;
				}
				else if (value < 10000U)
				{
					num += 3;
				}
				else
				{
					num += 4;
				}
			}
			return num;
		}

		// Token: 0x0600860D RID: 34317 RVA: 0x00274294 File Offset: 0x00274294
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int CountHexDigits(ulong value)
		{
			int num = 1;
			if (value > (ulong)-1)
			{
				num += 8;
				value >>= 32;
			}
			if (value > 65535UL)
			{
				num += 4;
				value >>= 16;
			}
			if (value > 255UL)
			{
				num += 2;
				value >>= 8;
			}
			if (value > 15UL)
			{
				num++;
			}
			return num;
		}

		// Token: 0x04003DFC RID: 15868
		internal const string HexTableLower = "0123456789abcdef";

		// Token: 0x04003DFD RID: 15869
		internal const string HexTableUpper = "0123456789ABCDEF";

		// Token: 0x020011CA RID: 4554
		public enum HexCasing : uint
		{
			// Token: 0x04004C78 RID: 19576
			Uppercase,
			// Token: 0x04004C79 RID: 19577
			Lowercase = 8224U
		}
	}
}
