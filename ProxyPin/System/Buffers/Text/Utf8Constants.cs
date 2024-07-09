using System;

namespace System.Buffers.Text
{
	// Token: 0x02000CF3 RID: 3315
	internal static class Utf8Constants
	{
		// Token: 0x04003DEC RID: 15852
		public const byte Colon = 58;

		// Token: 0x04003DED RID: 15853
		public const byte Comma = 44;

		// Token: 0x04003DEE RID: 15854
		public const byte Minus = 45;

		// Token: 0x04003DEF RID: 15855
		public const byte Period = 46;

		// Token: 0x04003DF0 RID: 15856
		public const byte Plus = 43;

		// Token: 0x04003DF1 RID: 15857
		public const byte Slash = 47;

		// Token: 0x04003DF2 RID: 15858
		public const byte Space = 32;

		// Token: 0x04003DF3 RID: 15859
		public const byte Hyphen = 45;

		// Token: 0x04003DF4 RID: 15860
		public const byte Separator = 44;

		// Token: 0x04003DF5 RID: 15861
		public const int GroupSize = 3;

		// Token: 0x04003DF6 RID: 15862
		public static readonly TimeSpan s_nullUtcOffset = TimeSpan.MinValue;

		// Token: 0x04003DF7 RID: 15863
		public const int DateTimeMaxUtcOffsetHours = 14;

		// Token: 0x04003DF8 RID: 15864
		public const int DateTimeNumFractionDigits = 7;

		// Token: 0x04003DF9 RID: 15865
		public const int MaxDateTimeFraction = 9999999;

		// Token: 0x04003DFA RID: 15866
		public const ulong BillionMaxUIntValue = 4294967295000000000UL;

		// Token: 0x04003DFB RID: 15867
		public const uint Billion = 1000000000U;
	}
}
