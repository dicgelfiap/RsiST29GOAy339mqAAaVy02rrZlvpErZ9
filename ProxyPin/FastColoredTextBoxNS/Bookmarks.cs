using System;
using System.Collections.Generic;

namespace FastColoredTextBoxNS
{
	// Token: 0x020009FD RID: 2557
	public class Bookmarks : BaseBookmarks
	{
		// Token: 0x06006259 RID: 25177 RVA: 0x001D5288 File Offset: 0x001D5288
		public Bookmarks(FastColoredTextBox tb)
		{
			this.tb = tb;
			tb.LineInserted += this.tb_LineInserted;
			tb.LineRemoved += this.tb_LineRemoved;
		}

		// Token: 0x0600625A RID: 25178 RVA: 0x001D52DC File Offset: 0x001D52DC
		protected virtual void tb_LineRemoved(object sender, LineRemovedEventArgs e)
		{
			for (int i = 0; i < this.Count; i++)
			{
				bool flag = this.items[i].LineIndex >= e.Index;
				if (flag)
				{
					bool flag2 = this.items[i].LineIndex >= e.Index + e.Count;
					if (flag2)
					{
						this.items[i].LineIndex = this.items[i].LineIndex - e.Count;
					}
					else
					{
						bool flag3 = e.Index <= 0;
						foreach (Bookmark bookmark in this.items)
						{
							bool flag4 = bookmark.LineIndex == e.Index - 1;
							if (flag4)
							{
								flag3 = true;
							}
						}
						bool flag5 = flag3;
						if (flag5)
						{
							this.items.RemoveAt(i);
							i--;
						}
						else
						{
							this.items[i].LineIndex = e.Index - 1;
						}
					}
				}
			}
		}

		// Token: 0x0600625B RID: 25179 RVA: 0x001D5434 File Offset: 0x001D5434
		protected virtual void tb_LineInserted(object sender, LineInsertedEventArgs e)
		{
			for (int i = 0; i < this.Count; i++)
			{
				bool flag = this.items[i].LineIndex >= e.Index;
				if (flag)
				{
					this.items[i].LineIndex = this.items[i].LineIndex + e.Count;
				}
				else
				{
					bool flag2 = this.items[i].LineIndex == e.Index - 1 && e.Count == 1;
					if (flag2)
					{
						bool flag3 = this.tb[e.Index - 1].StartSpacesCount == this.tb[e.Index - 1].Count;
						if (flag3)
						{
							this.items[i].LineIndex = this.items[i].LineIndex + e.Count;
						}
					}
				}
			}
		}

		// Token: 0x0600625C RID: 25180 RVA: 0x001D5550 File Offset: 0x001D5550
		public override void Dispose()
		{
			this.tb.LineInserted -= this.tb_LineInserted;
			this.tb.LineRemoved -= this.tb_LineRemoved;
		}

		// Token: 0x0600625D RID: 25181 RVA: 0x001D5588 File Offset: 0x001D5588
		public override IEnumerator<Bookmark> GetEnumerator()
		{
			foreach (Bookmark item in this.items)
			{
				yield return item;
				item = null;
			}
			List<Bookmark>.Enumerator enumerator = default(List<Bookmark>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x0600625E RID: 25182 RVA: 0x001D5598 File Offset: 0x001D5598
		public override void Add(int lineIndex, string bookmarkName)
		{
			this.Add(new Bookmark(this.tb, bookmarkName ?? ("Bookmark " + this.counter), lineIndex));
		}

		// Token: 0x0600625F RID: 25183 RVA: 0x001D55CC File Offset: 0x001D55CC
		public override void Add(int lineIndex)
		{
			this.Add(new Bookmark(this.tb, "Bookmark " + this.counter, lineIndex));
		}

		// Token: 0x06006260 RID: 25184 RVA: 0x001D55F8 File Offset: 0x001D55F8
		public override void Clear()
		{
			this.items.Clear();
			this.counter = 0;
		}

		// Token: 0x06006261 RID: 25185 RVA: 0x001D5610 File Offset: 0x001D5610
		public override void Add(Bookmark bookmark)
		{
			foreach (Bookmark bookmark2 in this.items)
			{
				bool flag = bookmark2.LineIndex == bookmark.LineIndex;
				if (flag)
				{
					return;
				}
			}
			this.items.Add(bookmark);
			this.counter++;
			this.tb.Invalidate();
		}

		// Token: 0x06006262 RID: 25186 RVA: 0x001D56A8 File Offset: 0x001D56A8
		public override bool Contains(Bookmark item)
		{
			return this.items.Contains(item);
		}

		// Token: 0x06006263 RID: 25187 RVA: 0x001D56D0 File Offset: 0x001D56D0
		public override bool Contains(int lineIndex)
		{
			foreach (Bookmark bookmark in this.items)
			{
				bool flag = bookmark.LineIndex == lineIndex;
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06006264 RID: 25188 RVA: 0x001D5744 File Offset: 0x001D5744
		public override void CopyTo(Bookmark[] array, int arrayIndex)
		{
			this.items.CopyTo(array, arrayIndex);
		}

		// Token: 0x170014AB RID: 5291
		// (get) Token: 0x06006265 RID: 25189 RVA: 0x001D5758 File Offset: 0x001D5758
		public override int Count
		{
			get
			{
				return this.items.Count;
			}
		}

		// Token: 0x170014AC RID: 5292
		// (get) Token: 0x06006266 RID: 25190 RVA: 0x001D577C File Offset: 0x001D577C
		public override bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06006267 RID: 25191 RVA: 0x001D5798 File Offset: 0x001D5798
		public override bool Remove(Bookmark item)
		{
			this.tb.Invalidate();
			return this.items.Remove(item);
		}

		// Token: 0x06006268 RID: 25192 RVA: 0x001D57CC File Offset: 0x001D57CC
		public override bool Remove(int lineIndex)
		{
			bool result = false;
			for (int i = 0; i < this.Count; i++)
			{
				bool flag = this.items[i].LineIndex == lineIndex;
				if (flag)
				{
					this.items.RemoveAt(i);
					i--;
					result = true;
				}
			}
			this.tb.Invalidate();
			return result;
		}

		// Token: 0x06006269 RID: 25193 RVA: 0x001D5840 File Offset: 0x001D5840
		public override Bookmark GetBookmark(int i)
		{
			return this.items[i];
		}

		// Token: 0x04003230 RID: 12848
		protected FastColoredTextBox tb;

		// Token: 0x04003231 RID: 12849
		protected List<Bookmark> items = new List<Bookmark>();

		// Token: 0x04003232 RID: 12850
		protected int counter;
	}
}
