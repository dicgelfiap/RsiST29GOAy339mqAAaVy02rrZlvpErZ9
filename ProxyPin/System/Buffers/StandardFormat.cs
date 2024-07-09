using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Buffers
{
	// Token: 0x02000CED RID: 3309
	[System.Memory.IsReadOnly]
	[ComVisible(true)]
	public struct StandardFormat : IEquatable<StandardFormat>
	{
		// Token: 0x17001CA7 RID: 7335
		// (get) Token: 0x060085D5 RID: 34261 RVA: 0x00273264 File Offset: 0x00273264
		public char Symbol
		{
			get
			{
				return (char)this._format;
			}
		}

		// Token: 0x17001CA8 RID: 7336
		// (get) Token: 0x060085D6 RID: 34262 RVA: 0x0027326C File Offset: 0x0027326C
		public byte Precision
		{
			get
			{
				return this._precision;
			}
		}

		// Token: 0x17001CA9 RID: 7337
		// (get) Token: 0x060085D7 RID: 34263 RVA: 0x00273274 File Offset: 0x00273274
		public bool HasPrecision
		{
			get
			{
				return this._precision != byte.MaxValue;
			}
		}

		// Token: 0x17001CAA RID: 7338
		// (get) Token: 0x060085D8 RID: 34264 RVA: 0x00273288 File Offset: 0x00273288
		public bool IsDefault
		{
			get
			{
				return this._format == 0 && this._precision == 0;
			}
		}

		// Token: 0x060085D9 RID: 34265 RVA: 0x002732A0 File Offset: 0x002732A0
		public StandardFormat(char symbol, byte precision = 255)
		{
			if (precision != 255 && precision > 99)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException_PrecisionTooLarge();
			}
			if (symbol != (char)((byte)symbol))
			{
				ThrowHelper.ThrowArgumentOutOfRangeException_SymbolDoesNotFit();
			}
			this._format = (byte)symbol;
			this._precision = precision;
		}

		// Token: 0x060085DA RID: 34266 RVA: 0x002732D8 File Offset: 0x002732D8
		public static implicit operator StandardFormat(char symbol)
		{
			return new StandardFormat(symbol, byte.MaxValue);
		}

		// Token: 0x060085DB RID: 34267 RVA: 0x002732E8 File Offset: 0x002732E8
		public unsafe static StandardFormat Parse(ReadOnlySpan<char> format)
		{
			if (format.Length == 0)
			{
				return default(StandardFormat);
			}
			char symbol = (char)(*format[0]);
			byte precision;
			if (format.Length == 1)
			{
				precision = byte.MaxValue;
			}
			else
			{
				uint num = 0U;
				for (int i = 1; i < format.Length; i++)
				{
					uint num2 = (uint)(*format[i] - 48);
					if (num2 > 9U)
					{
						throw new FormatException(System.Memory2565708.SR.Format(System.Memory2565708.SR.Argument_CannotParsePrecision, 99));
					}
					num = num * 10U + num2;
					if (num > 99U)
					{
						throw new FormatException(System.Memory2565708.SR.Format(System.Memory2565708.SR.Argument_PrecisionTooLarge, 99));
					}
				}
				precision = (byte)num;
			}
			return new StandardFormat(symbol, precision);
		}

		// Token: 0x060085DC RID: 34268 RVA: 0x002733AC File Offset: 0x002733AC
		public static StandardFormat Parse(string format)
		{
			if (format != null)
			{
				return StandardFormat.Parse(format.AsSpan());
			}
			return default(StandardFormat);
		}

		// Token: 0x060085DD RID: 34269 RVA: 0x002733D8 File Offset: 0x002733D8
		public override bool Equals(object obj)
		{
			if (obj is StandardFormat)
			{
				StandardFormat other = (StandardFormat)obj;
				return this.Equals(other);
			}
			return false;
		}

		// Token: 0x060085DE RID: 34270 RVA: 0x00273408 File Offset: 0x00273408
		public override int GetHashCode()
		{
			return this._format.GetHashCode() ^ this._precision.GetHashCode();
		}

		// Token: 0x060085DF RID: 34271 RVA: 0x00273438 File Offset: 0x00273438
		public bool Equals(StandardFormat other)
		{
			return this._format == other._format && this._precision == other._precision;
		}

		// Token: 0x060085E0 RID: 34272 RVA: 0x0027345C File Offset: 0x0027345C
		public unsafe override string ToString()
		{
			char* ptr = stackalloc char[(UIntPtr)8];
			int length = 0;
			char symbol = this.Symbol;
			if (symbol != '\0')
			{
				ptr[(IntPtr)(length++) * 2] = symbol;
				byte b = this.Precision;
				if (b != 255)
				{
					if (b >= 100)
					{
						ptr[(IntPtr)(length++) * 2] = (char)(48 + b / 100 % 10);
						b %= 100;
					}
					if (b >= 10)
					{
						ptr[(IntPtr)(length++) * 2] = (char)(48 + b / 10 % 10);
						b %= 10;
					}
					ptr[(IntPtr)(length++) * 2] = (char)(48 + b);
				}
			}
			return new string(ptr, 0, length);
		}

		// Token: 0x060085E1 RID: 34273 RVA: 0x00273500 File Offset: 0x00273500
		public static bool operator ==(StandardFormat left, StandardFormat right)
		{
			return left.Equals(right);
		}

		// Token: 0x060085E2 RID: 34274 RVA: 0x0027350C File Offset: 0x0027350C
		public static bool operator !=(StandardFormat left, StandardFormat right)
		{
			return !left.Equals(right);
		}

		// Token: 0x04003DE1 RID: 15841
		public const byte NoPrecision = 255;

		// Token: 0x04003DE2 RID: 15842
		public const byte MaxPrecision = 99;

		// Token: 0x04003DE3 RID: 15843
		private readonly byte _format;

		// Token: 0x04003DE4 RID: 15844
		private readonly byte _precision;
	}
}
