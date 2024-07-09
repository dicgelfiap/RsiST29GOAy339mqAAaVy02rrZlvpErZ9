using System;

namespace System
{
	// Token: 0x02000CDF RID: 3295
	internal struct MutableDecimal
	{
		// Token: 0x17001C88 RID: 7304
		// (get) Token: 0x06008553 RID: 34131 RVA: 0x00271544 File Offset: 0x00271544
		// (set) Token: 0x06008554 RID: 34132 RVA: 0x00271558 File Offset: 0x00271558
		public bool IsNegative
		{
			get
			{
				return (this.Flags & 2147483648U) > 0U;
			}
			set
			{
				this.Flags = ((this.Flags & 2147483647U) | (value ? 2147483648U : 0U));
			}
		}

		// Token: 0x17001C89 RID: 7305
		// (get) Token: 0x06008555 RID: 34133 RVA: 0x00271580 File Offset: 0x00271580
		// (set) Token: 0x06008556 RID: 34134 RVA: 0x0027158C File Offset: 0x0027158C
		public int Scale
		{
			get
			{
				return (int)((byte)(this.Flags >> 16));
			}
			set
			{
				this.Flags = ((this.Flags & 4278255615U) | (uint)((uint)value << 16));
			}
		}

		// Token: 0x04003DBE RID: 15806
		public uint Flags;

		// Token: 0x04003DBF RID: 15807
		public uint High;

		// Token: 0x04003DC0 RID: 15808
		public uint Low;

		// Token: 0x04003DC1 RID: 15809
		public uint Mid;

		// Token: 0x04003DC2 RID: 15810
		private const uint SignMask = 2147483648U;

		// Token: 0x04003DC3 RID: 15811
		private const uint ScaleMask = 16711680U;

		// Token: 0x04003DC4 RID: 15812
		private const int ScaleShift = 16;
	}
}
