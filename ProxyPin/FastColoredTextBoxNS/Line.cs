using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace FastColoredTextBoxNS
{
	// Token: 0x02000A3A RID: 2618
	public class Line : IList<Char>, ICollection<Char>, IEnumerable<Char>, IEnumerable
	{
		// Token: 0x170015A9 RID: 5545
		// (get) Token: 0x06006695 RID: 26261 RVA: 0x001F2FF0 File Offset: 0x001F2FF0
		// (set) Token: 0x06006696 RID: 26262 RVA: 0x001F2FF8 File Offset: 0x001F2FF8
		public string FoldingStartMarker { get; set; }

		// Token: 0x170015AA RID: 5546
		// (get) Token: 0x06006697 RID: 26263 RVA: 0x001F3004 File Offset: 0x001F3004
		// (set) Token: 0x06006698 RID: 26264 RVA: 0x001F300C File Offset: 0x001F300C
		public string FoldingEndMarker { get; set; }

		// Token: 0x170015AB RID: 5547
		// (get) Token: 0x06006699 RID: 26265 RVA: 0x001F3018 File Offset: 0x001F3018
		// (set) Token: 0x0600669A RID: 26266 RVA: 0x001F3020 File Offset: 0x001F3020
		public bool IsChanged { get; set; }

		// Token: 0x170015AC RID: 5548
		// (get) Token: 0x0600669B RID: 26267 RVA: 0x001F302C File Offset: 0x001F302C
		// (set) Token: 0x0600669C RID: 26268 RVA: 0x001F3034 File Offset: 0x001F3034
		public DateTime LastVisit { get; set; }

		// Token: 0x170015AD RID: 5549
		// (get) Token: 0x0600669D RID: 26269 RVA: 0x001F3040 File Offset: 0x001F3040
		// (set) Token: 0x0600669E RID: 26270 RVA: 0x001F3048 File Offset: 0x001F3048
		public Brush BackgroundBrush { get; set; }

		// Token: 0x170015AE RID: 5550
		// (get) Token: 0x0600669F RID: 26271 RVA: 0x001F3054 File Offset: 0x001F3054
		// (set) Token: 0x060066A0 RID: 26272 RVA: 0x001F305C File Offset: 0x001F305C
		public int UniqueId { get; private set; }

		// Token: 0x170015AF RID: 5551
		// (get) Token: 0x060066A1 RID: 26273 RVA: 0x001F3068 File Offset: 0x001F3068
		// (set) Token: 0x060066A2 RID: 26274 RVA: 0x001F3070 File Offset: 0x001F3070
		public int AutoIndentSpacesNeededCount { get; set; }

		// Token: 0x060066A3 RID: 26275 RVA: 0x001F307C File Offset: 0x001F307C
		internal Line(int uid)
		{
			this.UniqueId = uid;
			this.chars = new List<Char>();
		}

		// Token: 0x060066A4 RID: 26276 RVA: 0x001F309C File Offset: 0x001F309C
		public void ClearStyle(StyleIndex styleIndex)
		{
			this.FoldingStartMarker = null;
			this.FoldingEndMarker = null;
			for (int i = 0; i < this.Count; i++)
			{
				Char value = this[i];
				value.style &= ~styleIndex;
				this[i] = value;
			}
		}

		// Token: 0x170015B0 RID: 5552
		// (get) Token: 0x060066A5 RID: 26277 RVA: 0x001F30F8 File Offset: 0x001F30F8
		public virtual string Text
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder(this.Count);
				foreach (Char @char in this)
				{
					stringBuilder.Append(@char.c);
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x060066A6 RID: 26278 RVA: 0x001F316C File Offset: 0x001F316C
		public void ClearFoldingMarkers()
		{
			this.FoldingStartMarker = null;
			this.FoldingEndMarker = null;
		}

		// Token: 0x170015B1 RID: 5553
		// (get) Token: 0x060066A7 RID: 26279 RVA: 0x001F3180 File Offset: 0x001F3180
		public int StartSpacesCount
		{
			get
			{
				int num = 0;
				for (int i = 0; i < this.Count; i++)
				{
					bool flag = this[i].c == ' ';
					if (!flag)
					{
						break;
					}
					num++;
				}
				return num;
			}
		}

		// Token: 0x060066A8 RID: 26280 RVA: 0x001F31DC File Offset: 0x001F31DC
		public int IndexOf(Char item)
		{
			return this.chars.IndexOf(item);
		}

		// Token: 0x060066A9 RID: 26281 RVA: 0x001F3204 File Offset: 0x001F3204
		public void Insert(int index, Char item)
		{
			this.chars.Insert(index, item);
		}

		// Token: 0x060066AA RID: 26282 RVA: 0x001F3218 File Offset: 0x001F3218
		public void RemoveAt(int index)
		{
			this.chars.RemoveAt(index);
		}

		// Token: 0x170015B2 RID: 5554
		public Char this[int index]
		{
			get
			{
				return this.chars[index];
			}
			set
			{
				this.chars[index] = value;
			}
		}

		// Token: 0x060066AD RID: 26285 RVA: 0x001F3264 File Offset: 0x001F3264
		public void Add(Char item)
		{
			this.chars.Add(item);
		}

		// Token: 0x060066AE RID: 26286 RVA: 0x001F3274 File Offset: 0x001F3274
		public void Clear()
		{
			this.chars.Clear();
		}

		// Token: 0x060066AF RID: 26287 RVA: 0x001F3284 File Offset: 0x001F3284
		public bool Contains(Char item)
		{
			return this.chars.Contains(item);
		}

		// Token: 0x060066B0 RID: 26288 RVA: 0x001F32AC File Offset: 0x001F32AC
		public void CopyTo(Char[] array, int arrayIndex)
		{
			this.chars.CopyTo(array, arrayIndex);
		}

		// Token: 0x170015B3 RID: 5555
		// (get) Token: 0x060066B1 RID: 26289 RVA: 0x001F32C0 File Offset: 0x001F32C0
		public int Count
		{
			get
			{
				return this.chars.Count;
			}
		}

		// Token: 0x170015B4 RID: 5556
		// (get) Token: 0x060066B2 RID: 26290 RVA: 0x001F32E4 File Offset: 0x001F32E4
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060066B3 RID: 26291 RVA: 0x001F3300 File Offset: 0x001F3300
		public bool Remove(Char item)
		{
			return this.chars.Remove(item);
		}

		// Token: 0x060066B4 RID: 26292 RVA: 0x001F3328 File Offset: 0x001F3328
		public IEnumerator<Char> GetEnumerator()
		{
			return this.chars.GetEnumerator();
		}

		// Token: 0x060066B5 RID: 26293 RVA: 0x001F3354 File Offset: 0x001F3354
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.chars.GetEnumerator();
		}

		// Token: 0x060066B6 RID: 26294 RVA: 0x001F3380 File Offset: 0x001F3380
		public virtual void RemoveRange(int index, int count)
		{
			bool flag = index >= this.Count;
			if (!flag)
			{
				this.chars.RemoveRange(index, Math.Min(this.Count - index, count));
			}
		}

		// Token: 0x060066B7 RID: 26295 RVA: 0x001F33C8 File Offset: 0x001F33C8
		public virtual void TrimExcess()
		{
			this.chars.TrimExcess();
		}

		// Token: 0x060066B8 RID: 26296 RVA: 0x001F33D8 File Offset: 0x001F33D8
		public virtual void AddRange(IEnumerable<Char> collection)
		{
			this.chars.AddRange(collection);
		}

		// Token: 0x0400348E RID: 13454
		protected List<Char> chars;
	}
}
