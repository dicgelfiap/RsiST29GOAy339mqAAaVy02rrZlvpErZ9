using System;
using System.Collections;
using System.Collections.Generic;

namespace FastColoredTextBoxNS
{
	// Token: 0x02000A0A RID: 2570
	public class LinesAccessor : IList<string>, ICollection<string>, IEnumerable<string>, IEnumerable
	{
		// Token: 0x06006312 RID: 25362 RVA: 0x001D9F18 File Offset: 0x001D9F18
		public LinesAccessor(IList<Line> ts)
		{
			this.ts = ts;
		}

		// Token: 0x06006313 RID: 25363 RVA: 0x001D9F2C File Offset: 0x001D9F2C
		public int IndexOf(string item)
		{
			for (int i = 0; i < this.ts.Count; i++)
			{
				bool flag = this.ts[i].Text == item;
				if (flag)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06006314 RID: 25364 RVA: 0x001D9F88 File Offset: 0x001D9F88
		public void Insert(int index, string item)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06006315 RID: 25365 RVA: 0x001D9F90 File Offset: 0x001D9F90
		public void RemoveAt(int index)
		{
			throw new NotImplementedException();
		}

		// Token: 0x170014D7 RID: 5335
		public string this[int index]
		{
			get
			{
				return this.ts[index].Text;
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06006318 RID: 25368 RVA: 0x001D9FCC File Offset: 0x001D9FCC
		public void Add(string item)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06006319 RID: 25369 RVA: 0x001D9FD4 File Offset: 0x001D9FD4
		public void Clear()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600631A RID: 25370 RVA: 0x001D9FDC File Offset: 0x001D9FDC
		public bool Contains(string item)
		{
			for (int i = 0; i < this.ts.Count; i++)
			{
				bool flag = this.ts[i].Text == item;
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600631B RID: 25371 RVA: 0x001DA038 File Offset: 0x001DA038
		public void CopyTo(string[] array, int arrayIndex)
		{
			for (int i = 0; i < this.ts.Count; i++)
			{
				array[i + arrayIndex] = this.ts[i].Text;
			}
		}

		// Token: 0x170014D8 RID: 5336
		// (get) Token: 0x0600631C RID: 25372 RVA: 0x001DA084 File Offset: 0x001DA084
		public int Count
		{
			get
			{
				return this.ts.Count;
			}
		}

		// Token: 0x170014D9 RID: 5337
		// (get) Token: 0x0600631D RID: 25373 RVA: 0x001DA0A8 File Offset: 0x001DA0A8
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600631E RID: 25374 RVA: 0x001DA0C4 File Offset: 0x001DA0C4
		public bool Remove(string item)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600631F RID: 25375 RVA: 0x001DA0CC File Offset: 0x001DA0CC
		public IEnumerator<string> GetEnumerator()
		{
			int num;
			for (int i = 0; i < this.ts.Count; i = num + 1)
			{
				yield return this.ts[i].Text;
				num = i;
			}
			yield break;
		}

		// Token: 0x06006320 RID: 25376 RVA: 0x001DA0DC File Offset: 0x001DA0DC
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04003270 RID: 12912
		private IList<Line> ts;
	}
}
