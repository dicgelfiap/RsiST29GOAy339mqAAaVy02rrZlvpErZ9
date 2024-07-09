using System;
using System.Collections;
using System.Collections.Generic;

namespace FastColoredTextBoxNS
{
	// Token: 0x020009FC RID: 2556
	public abstract class BaseBookmarks : ICollection<Bookmark>, IEnumerable<Bookmark>, IEnumerable, IDisposable
	{
		// Token: 0x06006249 RID: 25161
		public abstract void Add(Bookmark item);

		// Token: 0x0600624A RID: 25162
		public abstract void Clear();

		// Token: 0x0600624B RID: 25163
		public abstract bool Contains(Bookmark item);

		// Token: 0x0600624C RID: 25164
		public abstract void CopyTo(Bookmark[] array, int arrayIndex);

		// Token: 0x170014A9 RID: 5289
		// (get) Token: 0x0600624D RID: 25165
		public abstract int Count { get; }

		// Token: 0x170014AA RID: 5290
		// (get) Token: 0x0600624E RID: 25166
		public abstract bool IsReadOnly { get; }

		// Token: 0x0600624F RID: 25167
		public abstract bool Remove(Bookmark item);

		// Token: 0x06006250 RID: 25168
		public abstract IEnumerator<Bookmark> GetEnumerator();

		// Token: 0x06006251 RID: 25169 RVA: 0x001D525C File Offset: 0x001D525C
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06006252 RID: 25170
		public abstract void Dispose();

		// Token: 0x06006253 RID: 25171
		public abstract void Add(int lineIndex, string bookmarkName);

		// Token: 0x06006254 RID: 25172
		public abstract void Add(int lineIndex);

		// Token: 0x06006255 RID: 25173
		public abstract bool Contains(int lineIndex);

		// Token: 0x06006256 RID: 25174
		public abstract bool Remove(int lineIndex);

		// Token: 0x06006257 RID: 25175
		public abstract Bookmark GetBookmark(int i);
	}
}
