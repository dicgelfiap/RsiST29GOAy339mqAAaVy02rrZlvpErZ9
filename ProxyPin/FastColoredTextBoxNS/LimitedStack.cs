using System;

namespace FastColoredTextBoxNS
{
	// Token: 0x02000A39 RID: 2617
	public class LimitedStack<T>
	{
		// Token: 0x170015A6 RID: 5542
		// (get) Token: 0x0600668C RID: 26252 RVA: 0x001F2DA8 File Offset: 0x001F2DA8
		public int MaxItemCount
		{
			get
			{
				return this.items.Length;
			}
		}

		// Token: 0x170015A7 RID: 5543
		// (get) Token: 0x0600668D RID: 26253 RVA: 0x001F2DCC File Offset: 0x001F2DCC
		public int Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x0600668E RID: 26254 RVA: 0x001F2DEC File Offset: 0x001F2DEC
		public LimitedStack(int maxItemCount)
		{
			this.items = new T[maxItemCount];
			this.count = 0;
			this.start = 0;
		}

		// Token: 0x0600668F RID: 26255 RVA: 0x001F2E10 File Offset: 0x001F2E10
		public T Pop()
		{
			bool flag = this.count == 0;
			if (flag)
			{
				throw new Exception("Stack is empty");
			}
			int lastIndex = this.LastIndex;
			T result = this.items[lastIndex];
			this.items[lastIndex] = default(T);
			this.count--;
			return result;
		}

		// Token: 0x170015A8 RID: 5544
		// (get) Token: 0x06006690 RID: 26256 RVA: 0x001F2E80 File Offset: 0x001F2E80
		private int LastIndex
		{
			get
			{
				return (this.start + this.count - 1) % this.items.Length;
			}
		}

		// Token: 0x06006691 RID: 26257 RVA: 0x001F2EB4 File Offset: 0x001F2EB4
		public T Peek()
		{
			bool flag = this.count == 0;
			T result;
			if (flag)
			{
				result = default(T);
			}
			else
			{
				result = this.items[this.LastIndex];
			}
			return result;
		}

		// Token: 0x06006692 RID: 26258 RVA: 0x001F2F00 File Offset: 0x001F2F00
		public void Push(T item)
		{
			bool flag = this.count == this.items.Length;
			if (flag)
			{
				this.start = (this.start + 1) % this.items.Length;
			}
			else
			{
				this.count++;
			}
			this.items[this.LastIndex] = item;
		}

		// Token: 0x06006693 RID: 26259 RVA: 0x001F2F68 File Offset: 0x001F2F68
		public void Clear()
		{
			this.items = new T[this.items.Length];
			this.count = 0;
			this.start = 0;
		}

		// Token: 0x06006694 RID: 26260 RVA: 0x001F2F8C File Offset: 0x001F2F8C
		public T[] ToArray()
		{
			T[] array = new T[this.count];
			for (int i = 0; i < this.count; i++)
			{
				array[i] = this.items[(this.start + i) % this.items.Length];
			}
			return array;
		}

		// Token: 0x0400348B RID: 13451
		private T[] items;

		// Token: 0x0400348C RID: 13452
		private int count;

		// Token: 0x0400348D RID: 13453
		private int start;
	}
}
