using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x02000AAA RID: 2730
	[NullableContext(1)]
	[Nullable(0)]
	internal struct DateTimeParser
	{
		// Token: 0x06006CA2 RID: 27810 RVA: 0x0020D2B8 File Offset: 0x0020D2B8
		public bool Parse(char[] text, int startIndex, int length)
		{
			this._text = text;
			this._end = startIndex + length;
			return this.ParseDate(startIndex) && this.ParseChar(DateTimeParser.Lzyyyy_MM_dd + startIndex, 'T') && this.ParseTimeAndZoneAndWhitespace(DateTimeParser.Lzyyyy_MM_ddT + startIndex);
		}

		// Token: 0x06006CA3 RID: 27811 RVA: 0x0020D310 File Offset: 0x0020D310
		private bool ParseDate(int start)
		{
			return this.Parse4Digit(start, out this.Year) && 1 <= this.Year && this.ParseChar(start + DateTimeParser.Lzyyyy, '-') && this.Parse2Digit(start + DateTimeParser.Lzyyyy_, out this.Month) && 1 <= this.Month && this.Month <= 12 && this.ParseChar(start + DateTimeParser.Lzyyyy_MM, '-') && this.Parse2Digit(start + DateTimeParser.Lzyyyy_MM_, out this.Day) && 1 <= this.Day && this.Day <= DateTime.DaysInMonth(this.Year, this.Month);
		}

		// Token: 0x06006CA4 RID: 27812 RVA: 0x0020D3DC File Offset: 0x0020D3DC
		private bool ParseTimeAndZoneAndWhitespace(int start)
		{
			return this.ParseTime(ref start) && this.ParseZone(start);
		}

		// Token: 0x06006CA5 RID: 27813 RVA: 0x0020D3F4 File Offset: 0x0020D3F4
		private bool ParseTime(ref int start)
		{
			if (!this.Parse2Digit(start, out this.Hour) || this.Hour > 24 || !this.ParseChar(start + DateTimeParser.LzHH, ':') || !this.Parse2Digit(start + DateTimeParser.LzHH_, out this.Minute) || this.Minute >= 60 || !this.ParseChar(start + DateTimeParser.LzHH_mm, ':') || !this.Parse2Digit(start + DateTimeParser.LzHH_mm_, out this.Second) || this.Second >= 60 || (this.Hour == 24 && (this.Minute != 0 || this.Second != 0)))
			{
				return false;
			}
			start += DateTimeParser.LzHH_mm_ss;
			if (this.ParseChar(start, '.'))
			{
				this.Fraction = 0;
				int num = 0;
				for (;;)
				{
					int num2 = start + 1;
					start = num2;
					if (num2 >= this._end || num >= 7)
					{
						break;
					}
					int num3 = (int)(this._text[start] - '0');
					if (num3 < 0 || num3 > 9)
					{
						break;
					}
					this.Fraction = this.Fraction * 10 + num3;
					num++;
				}
				if (num < 7)
				{
					if (num == 0)
					{
						return false;
					}
					this.Fraction *= DateTimeParser.Power10[7 - num];
				}
				if (this.Hour == 24 && this.Fraction != 0)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06006CA6 RID: 27814 RVA: 0x0020D570 File Offset: 0x0020D570
		private bool ParseZone(int start)
		{
			if (start < this._end)
			{
				char c = this._text[start];
				if (c == 'Z' || c == 'z')
				{
					this.Zone = ParserTimeZone.Utc;
					start++;
				}
				else
				{
					if (start + 2 < this._end && this.Parse2Digit(start + DateTimeParser.Lz_, out this.ZoneHour) && this.ZoneHour <= 99)
					{
						if (c != '+')
						{
							if (c == '-')
							{
								this.Zone = ParserTimeZone.LocalWestOfUtc;
								start += DateTimeParser.Lz_zz;
							}
						}
						else
						{
							this.Zone = ParserTimeZone.LocalEastOfUtc;
							start += DateTimeParser.Lz_zz;
						}
					}
					if (start < this._end)
					{
						if (this.ParseChar(start, ':'))
						{
							start++;
							if (start + 1 < this._end && this.Parse2Digit(start, out this.ZoneMinute) && this.ZoneMinute <= 99)
							{
								start += 2;
							}
						}
						else if (start + 1 < this._end && this.Parse2Digit(start, out this.ZoneMinute) && this.ZoneMinute <= 99)
						{
							start += 2;
						}
					}
				}
			}
			return start == this._end;
		}

		// Token: 0x06006CA7 RID: 27815 RVA: 0x0020D6B0 File Offset: 0x0020D6B0
		private bool Parse4Digit(int start, out int num)
		{
			if (start + 3 < this._end)
			{
				int num2 = (int)(this._text[start] - '0');
				int num3 = (int)(this._text[start + 1] - '0');
				int num4 = (int)(this._text[start + 2] - '0');
				int num5 = (int)(this._text[start + 3] - '0');
				if (0 <= num2 && num2 < 10 && 0 <= num3 && num3 < 10 && 0 <= num4 && num4 < 10 && 0 <= num5 && num5 < 10)
				{
					num = ((num2 * 10 + num3) * 10 + num4) * 10 + num5;
					return true;
				}
			}
			num = 0;
			return false;
		}

		// Token: 0x06006CA8 RID: 27816 RVA: 0x0020D75C File Offset: 0x0020D75C
		private bool Parse2Digit(int start, out int num)
		{
			if (start + 1 < this._end)
			{
				int num2 = (int)(this._text[start] - '0');
				int num3 = (int)(this._text[start + 1] - '0');
				if (0 <= num2 && num2 < 10 && 0 <= num3 && num3 < 10)
				{
					num = num2 * 10 + num3;
					return true;
				}
			}
			num = 0;
			return false;
		}

		// Token: 0x06006CA9 RID: 27817 RVA: 0x0020D7C4 File Offset: 0x0020D7C4
		private bool ParseChar(int start, char ch)
		{
			return start < this._end && this._text[start] == ch;
		}

		// Token: 0x04003694 RID: 13972
		public int Year;

		// Token: 0x04003695 RID: 13973
		public int Month;

		// Token: 0x04003696 RID: 13974
		public int Day;

		// Token: 0x04003697 RID: 13975
		public int Hour;

		// Token: 0x04003698 RID: 13976
		public int Minute;

		// Token: 0x04003699 RID: 13977
		public int Second;

		// Token: 0x0400369A RID: 13978
		public int Fraction;

		// Token: 0x0400369B RID: 13979
		public int ZoneHour;

		// Token: 0x0400369C RID: 13980
		public int ZoneMinute;

		// Token: 0x0400369D RID: 13981
		public ParserTimeZone Zone;

		// Token: 0x0400369E RID: 13982
		private char[] _text;

		// Token: 0x0400369F RID: 13983
		private int _end;

		// Token: 0x040036A0 RID: 13984
		private static readonly int[] Power10 = new int[]
		{
			-1,
			10,
			100,
			1000,
			10000,
			100000,
			1000000
		};

		// Token: 0x040036A1 RID: 13985
		private static readonly int Lzyyyy = "yyyy".Length;

		// Token: 0x040036A2 RID: 13986
		private static readonly int Lzyyyy_ = "yyyy-".Length;

		// Token: 0x040036A3 RID: 13987
		private static readonly int Lzyyyy_MM = "yyyy-MM".Length;

		// Token: 0x040036A4 RID: 13988
		private static readonly int Lzyyyy_MM_ = "yyyy-MM-".Length;

		// Token: 0x040036A5 RID: 13989
		private static readonly int Lzyyyy_MM_dd = "yyyy-MM-dd".Length;

		// Token: 0x040036A6 RID: 13990
		private static readonly int Lzyyyy_MM_ddT = "yyyy-MM-ddT".Length;

		// Token: 0x040036A7 RID: 13991
		private static readonly int LzHH = "HH".Length;

		// Token: 0x040036A8 RID: 13992
		private static readonly int LzHH_ = "HH:".Length;

		// Token: 0x040036A9 RID: 13993
		private static readonly int LzHH_mm = "HH:mm".Length;

		// Token: 0x040036AA RID: 13994
		private static readonly int LzHH_mm_ = "HH:mm:".Length;

		// Token: 0x040036AB RID: 13995
		private static readonly int LzHH_mm_ss = "HH:mm:ss".Length;

		// Token: 0x040036AC RID: 13996
		private static readonly int Lz_ = "-".Length;

		// Token: 0x040036AD RID: 13997
		private static readonly int Lz_zz = "-zz".Length;

		// Token: 0x040036AE RID: 13998
		private const short MaxFractionDigits = 7;
	}
}
