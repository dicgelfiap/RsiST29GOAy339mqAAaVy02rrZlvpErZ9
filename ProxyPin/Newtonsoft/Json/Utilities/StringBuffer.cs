using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x02000AC8 RID: 2760
	[NullableContext(2)]
	[Nullable(0)]
	internal struct StringBuffer
	{
		// Token: 0x170016C8 RID: 5832
		// (get) Token: 0x06006DEA RID: 28138 RVA: 0x00214994 File Offset: 0x00214994
		// (set) Token: 0x06006DEB RID: 28139 RVA: 0x0021499C File Offset: 0x0021499C
		public int Position
		{
			get
			{
				return this._position;
			}
			set
			{
				this._position = value;
			}
		}

		// Token: 0x170016C9 RID: 5833
		// (get) Token: 0x06006DEC RID: 28140 RVA: 0x002149A8 File Offset: 0x002149A8
		public bool IsEmpty
		{
			get
			{
				return this._buffer == null;
			}
		}

		// Token: 0x06006DED RID: 28141 RVA: 0x002149B4 File Offset: 0x002149B4
		public StringBuffer(IArrayPool<char> bufferPool, int initalSize)
		{
			this = new StringBuffer(BufferUtils.RentBuffer(bufferPool, initalSize));
		}

		// Token: 0x06006DEE RID: 28142 RVA: 0x002149C4 File Offset: 0x002149C4
		[NullableContext(1)]
		private StringBuffer(char[] buffer)
		{
			this._buffer = buffer;
			this._position = 0;
		}

		// Token: 0x06006DEF RID: 28143 RVA: 0x002149D4 File Offset: 0x002149D4
		public void Append(IArrayPool<char> bufferPool, char value)
		{
			if (this._position == this._buffer.Length)
			{
				this.EnsureSize(bufferPool, 1);
			}
			char[] buffer = this._buffer;
			int position = this._position;
			this._position = position + 1;
			buffer[position] = value;
		}

		// Token: 0x06006DF0 RID: 28144 RVA: 0x00214A1C File Offset: 0x00214A1C
		[NullableContext(1)]
		public void Append([Nullable(2)] IArrayPool<char> bufferPool, char[] buffer, int startIndex, int count)
		{
			if (this._position + count >= this._buffer.Length)
			{
				this.EnsureSize(bufferPool, count);
			}
			Array.Copy(buffer, startIndex, this._buffer, this._position, count);
			this._position += count;
		}

		// Token: 0x06006DF1 RID: 28145 RVA: 0x00214A70 File Offset: 0x00214A70
		public void Clear(IArrayPool<char> bufferPool)
		{
			if (this._buffer != null)
			{
				BufferUtils.ReturnBuffer(bufferPool, this._buffer);
				this._buffer = null;
			}
			this._position = 0;
		}

		// Token: 0x06006DF2 RID: 28146 RVA: 0x00214A98 File Offset: 0x00214A98
		private void EnsureSize(IArrayPool<char> bufferPool, int appendLength)
		{
			char[] array = BufferUtils.RentBuffer(bufferPool, (this._position + appendLength) * 2);
			if (this._buffer != null)
			{
				Array.Copy(this._buffer, array, this._position);
				BufferUtils.ReturnBuffer(bufferPool, this._buffer);
			}
			this._buffer = array;
		}

		// Token: 0x06006DF3 RID: 28147 RVA: 0x00214AEC File Offset: 0x00214AEC
		[NullableContext(1)]
		public override string ToString()
		{
			return this.ToString(0, this._position);
		}

		// Token: 0x06006DF4 RID: 28148 RVA: 0x00214AFC File Offset: 0x00214AFC
		[NullableContext(1)]
		public string ToString(int start, int length)
		{
			return new string(this._buffer, start, length);
		}

		// Token: 0x170016CA RID: 5834
		// (get) Token: 0x06006DF5 RID: 28149 RVA: 0x00214B0C File Offset: 0x00214B0C
		public char[] InternalBuffer
		{
			get
			{
				return this._buffer;
			}
		}

		// Token: 0x04003700 RID: 14080
		private char[] _buffer;

		// Token: 0x04003701 RID: 14081
		private int _position;
	}
}
