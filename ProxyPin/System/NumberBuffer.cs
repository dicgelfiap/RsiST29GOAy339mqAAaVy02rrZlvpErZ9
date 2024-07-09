using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace System
{
	// Token: 0x02000CD3 RID: 3283
	internal ref struct NumberBuffer
	{
		// Token: 0x17001C6E RID: 7278
		// (get) Token: 0x06008455 RID: 33877 RVA: 0x00269CA8 File Offset: 0x00269CA8
		public Span<byte> Digits
		{
			get
			{
				return new Span<byte>(Unsafe.AsPointer<byte>(ref this._b0), 51);
			}
		}

		// Token: 0x17001C6F RID: 7279
		// (get) Token: 0x06008456 RID: 33878 RVA: 0x00269CBC File Offset: 0x00269CBC
		public unsafe byte* UnsafeDigits
		{
			get
			{
				return (byte*)Unsafe.AsPointer<byte>(ref this._b0);
			}
		}

		// Token: 0x17001C70 RID: 7280
		// (get) Token: 0x06008457 RID: 33879 RVA: 0x00269CCC File Offset: 0x00269CCC
		public int NumDigits
		{
			get
			{
				return this.Digits.IndexOf(0);
			}
		}

		// Token: 0x06008458 RID: 33880 RVA: 0x00269CDC File Offset: 0x00269CDC
		[Conditional("DEBUG")]
		public void CheckConsistency()
		{
		}

		// Token: 0x06008459 RID: 33881 RVA: 0x00269CE0 File Offset: 0x00269CE0
		public unsafe override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append('[');
			stringBuilder.Append('"');
			Span<byte> digits = this.Digits;
			for (int i = 0; i < 51; i++)
			{
				byte b = *digits[i];
				if (b == 0)
				{
					break;
				}
				stringBuilder.Append((char)b);
			}
			stringBuilder.Append('"');
			stringBuilder.Append(", Scale = " + this.Scale);
			stringBuilder.Append(", IsNegative   = " + this.IsNegative.ToString());
			stringBuilder.Append(']');
			return stringBuilder.ToString();
		}

		// Token: 0x04003D73 RID: 15731
		public int Scale;

		// Token: 0x04003D74 RID: 15732
		public bool IsNegative;

		// Token: 0x04003D75 RID: 15733
		public const int BufferSize = 51;

		// Token: 0x04003D76 RID: 15734
		private byte _b0;

		// Token: 0x04003D77 RID: 15735
		private byte _b1;

		// Token: 0x04003D78 RID: 15736
		private byte _b2;

		// Token: 0x04003D79 RID: 15737
		private byte _b3;

		// Token: 0x04003D7A RID: 15738
		private byte _b4;

		// Token: 0x04003D7B RID: 15739
		private byte _b5;

		// Token: 0x04003D7C RID: 15740
		private byte _b6;

		// Token: 0x04003D7D RID: 15741
		private byte _b7;

		// Token: 0x04003D7E RID: 15742
		private byte _b8;

		// Token: 0x04003D7F RID: 15743
		private byte _b9;

		// Token: 0x04003D80 RID: 15744
		private byte _b10;

		// Token: 0x04003D81 RID: 15745
		private byte _b11;

		// Token: 0x04003D82 RID: 15746
		private byte _b12;

		// Token: 0x04003D83 RID: 15747
		private byte _b13;

		// Token: 0x04003D84 RID: 15748
		private byte _b14;

		// Token: 0x04003D85 RID: 15749
		private byte _b15;

		// Token: 0x04003D86 RID: 15750
		private byte _b16;

		// Token: 0x04003D87 RID: 15751
		private byte _b17;

		// Token: 0x04003D88 RID: 15752
		private byte _b18;

		// Token: 0x04003D89 RID: 15753
		private byte _b19;

		// Token: 0x04003D8A RID: 15754
		private byte _b20;

		// Token: 0x04003D8B RID: 15755
		private byte _b21;

		// Token: 0x04003D8C RID: 15756
		private byte _b22;

		// Token: 0x04003D8D RID: 15757
		private byte _b23;

		// Token: 0x04003D8E RID: 15758
		private byte _b24;

		// Token: 0x04003D8F RID: 15759
		private byte _b25;

		// Token: 0x04003D90 RID: 15760
		private byte _b26;

		// Token: 0x04003D91 RID: 15761
		private byte _b27;

		// Token: 0x04003D92 RID: 15762
		private byte _b28;

		// Token: 0x04003D93 RID: 15763
		private byte _b29;

		// Token: 0x04003D94 RID: 15764
		private byte _b30;

		// Token: 0x04003D95 RID: 15765
		private byte _b31;

		// Token: 0x04003D96 RID: 15766
		private byte _b32;

		// Token: 0x04003D97 RID: 15767
		private byte _b33;

		// Token: 0x04003D98 RID: 15768
		private byte _b34;

		// Token: 0x04003D99 RID: 15769
		private byte _b35;

		// Token: 0x04003D9A RID: 15770
		private byte _b36;

		// Token: 0x04003D9B RID: 15771
		private byte _b37;

		// Token: 0x04003D9C RID: 15772
		private byte _b38;

		// Token: 0x04003D9D RID: 15773
		private byte _b39;

		// Token: 0x04003D9E RID: 15774
		private byte _b40;

		// Token: 0x04003D9F RID: 15775
		private byte _b41;

		// Token: 0x04003DA0 RID: 15776
		private byte _b42;

		// Token: 0x04003DA1 RID: 15777
		private byte _b43;

		// Token: 0x04003DA2 RID: 15778
		private byte _b44;

		// Token: 0x04003DA3 RID: 15779
		private byte _b45;

		// Token: 0x04003DA4 RID: 15780
		private byte _b46;

		// Token: 0x04003DA5 RID: 15781
		private byte _b47;

		// Token: 0x04003DA6 RID: 15782
		private byte _b48;

		// Token: 0x04003DA7 RID: 15783
		private byte _b49;

		// Token: 0x04003DA8 RID: 15784
		private byte _b50;
	}
}
