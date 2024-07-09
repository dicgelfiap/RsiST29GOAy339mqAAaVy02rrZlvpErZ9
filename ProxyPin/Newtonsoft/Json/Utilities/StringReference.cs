using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x02000AC9 RID: 2761
	[NullableContext(1)]
	[Nullable(0)]
	[Newtonsoft.Json.IsReadOnly]
	internal struct StringReference
	{
		// Token: 0x170016CB RID: 5835
		public char this[int i]
		{
			get
			{
				return this._chars[i];
			}
		}

		// Token: 0x170016CC RID: 5836
		// (get) Token: 0x06006DF7 RID: 28151 RVA: 0x00214B20 File Offset: 0x00214B20
		public char[] Chars
		{
			get
			{
				return this._chars;
			}
		}

		// Token: 0x170016CD RID: 5837
		// (get) Token: 0x06006DF8 RID: 28152 RVA: 0x00214B28 File Offset: 0x00214B28
		public int StartIndex
		{
			get
			{
				return this._startIndex;
			}
		}

		// Token: 0x170016CE RID: 5838
		// (get) Token: 0x06006DF9 RID: 28153 RVA: 0x00214B30 File Offset: 0x00214B30
		public int Length
		{
			get
			{
				return this._length;
			}
		}

		// Token: 0x06006DFA RID: 28154 RVA: 0x00214B38 File Offset: 0x00214B38
		public StringReference(char[] chars, int startIndex, int length)
		{
			this._chars = chars;
			this._startIndex = startIndex;
			this._length = length;
		}

		// Token: 0x06006DFB RID: 28155 RVA: 0x00214B50 File Offset: 0x00214B50
		public override string ToString()
		{
			return new string(this._chars, this._startIndex, this._length);
		}

		// Token: 0x04003702 RID: 14082
		private readonly char[] _chars;

		// Token: 0x04003703 RID: 14083
		private readonly int _startIndex;

		// Token: 0x04003704 RID: 14084
		private readonly int _length;
	}
}
