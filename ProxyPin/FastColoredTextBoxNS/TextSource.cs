using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;

namespace FastColoredTextBoxNS
{
	// Token: 0x02000A49 RID: 2633
	public class TextSource : IList<Line>, ICollection<Line>, IEnumerable<Line>, IEnumerable, IDisposable
	{
		// Token: 0x170015D2 RID: 5586
		// (get) Token: 0x0600675D RID: 26461 RVA: 0x001F8200 File Offset: 0x001F8200
		// (set) Token: 0x0600675E RID: 26462 RVA: 0x001F8208 File Offset: 0x001F8208
		public CommandManager Manager { get; set; }

		// Token: 0x1400004B RID: 75
		// (add) Token: 0x0600675F RID: 26463 RVA: 0x001F8214 File Offset: 0x001F8214
		// (remove) Token: 0x06006760 RID: 26464 RVA: 0x001F8250 File Offset: 0x001F8250
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<LineInsertedEventArgs> LineInserted;

		// Token: 0x1400004C RID: 76
		// (add) Token: 0x06006761 RID: 26465 RVA: 0x001F828C File Offset: 0x001F828C
		// (remove) Token: 0x06006762 RID: 26466 RVA: 0x001F82C8 File Offset: 0x001F82C8
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<LineRemovedEventArgs> LineRemoved;

		// Token: 0x1400004D RID: 77
		// (add) Token: 0x06006763 RID: 26467 RVA: 0x001F8304 File Offset: 0x001F8304
		// (remove) Token: 0x06006764 RID: 26468 RVA: 0x001F8340 File Offset: 0x001F8340
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<TextSource.TextChangedEventArgs> TextChanged;

		// Token: 0x1400004E RID: 78
		// (add) Token: 0x06006765 RID: 26469 RVA: 0x001F837C File Offset: 0x001F837C
		// (remove) Token: 0x06006766 RID: 26470 RVA: 0x001F83B8 File Offset: 0x001F83B8
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<TextSource.TextChangedEventArgs> RecalcNeeded;

		// Token: 0x1400004F RID: 79
		// (add) Token: 0x06006767 RID: 26471 RVA: 0x001F83F4 File Offset: 0x001F83F4
		// (remove) Token: 0x06006768 RID: 26472 RVA: 0x001F8430 File Offset: 0x001F8430
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<TextSource.TextChangedEventArgs> RecalcWordWrap;

		// Token: 0x14000050 RID: 80
		// (add) Token: 0x06006769 RID: 26473 RVA: 0x001F846C File Offset: 0x001F846C
		// (remove) Token: 0x0600676A RID: 26474 RVA: 0x001F84A8 File Offset: 0x001F84A8
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<TextChangingEventArgs> TextChanging;

		// Token: 0x14000051 RID: 81
		// (add) Token: 0x0600676B RID: 26475 RVA: 0x001F84E4 File Offset: 0x001F84E4
		// (remove) Token: 0x0600676C RID: 26476 RVA: 0x001F8520 File Offset: 0x001F8520
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler CurrentTBChanged;

		// Token: 0x170015D3 RID: 5587
		// (get) Token: 0x0600676D RID: 26477 RVA: 0x001F855C File Offset: 0x001F855C
		// (set) Token: 0x0600676E RID: 26478 RVA: 0x001F857C File Offset: 0x001F857C
		public FastColoredTextBox CurrentTB
		{
			get
			{
				return this.currentTB;
			}
			set
			{
				bool flag = this.currentTB == value;
				if (!flag)
				{
					this.currentTB = value;
					this.OnCurrentTBChanged();
				}
			}
		}

		// Token: 0x0600676F RID: 26479 RVA: 0x001F85B4 File Offset: 0x001F85B4
		public virtual void ClearIsChanged()
		{
			foreach (Line line in this.lines)
			{
				line.IsChanged = false;
			}
		}

		// Token: 0x06006770 RID: 26480 RVA: 0x001F8614 File Offset: 0x001F8614
		public virtual Line CreateLine()
		{
			return new Line(this.GenerateUniqueLineId());
		}

		// Token: 0x06006771 RID: 26481 RVA: 0x001F8638 File Offset: 0x001F8638
		private void OnCurrentTBChanged()
		{
			bool flag = this.CurrentTBChanged != null;
			if (flag)
			{
				this.CurrentTBChanged(this, EventArgs.Empty);
			}
		}

		// Token: 0x170015D4 RID: 5588
		// (get) Token: 0x06006772 RID: 26482 RVA: 0x001F866C File Offset: 0x001F866C
		// (set) Token: 0x06006773 RID: 26483 RVA: 0x001F8674 File Offset: 0x001F8674
		public TextStyle DefaultStyle { get; set; }

		// Token: 0x06006774 RID: 26484 RVA: 0x001F8680 File Offset: 0x001F8680
		public TextSource(FastColoredTextBox currentTB)
		{
			this.CurrentTB = currentTB;
			this.linesAccessor = new LinesAccessor(this);
			this.Manager = new CommandManager(this);
			bool flag = Enum.GetUnderlyingType(typeof(StyleIndex)) == typeof(uint);
			if (flag)
			{
				this.Styles = new Style[32];
			}
			else
			{
				this.Styles = new Style[16];
			}
			this.InitDefaultStyle();
		}

		// Token: 0x06006775 RID: 26485 RVA: 0x001F8710 File Offset: 0x001F8710
		public virtual void InitDefaultStyle()
		{
			this.DefaultStyle = new TextStyle(null, null, FontStyle.Regular);
		}

		// Token: 0x170015D5 RID: 5589
		public virtual Line this[int i]
		{
			get
			{
				return this.lines[i];
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06006778 RID: 26488 RVA: 0x001F8754 File Offset: 0x001F8754
		public virtual bool IsLineLoaded(int iLine)
		{
			return this.lines[iLine] != null;
		}

		// Token: 0x06006779 RID: 26489 RVA: 0x001F877C File Offset: 0x001F877C
		public virtual IList<string> GetLines()
		{
			return this.linesAccessor;
		}

		// Token: 0x0600677A RID: 26490 RVA: 0x001F879C File Offset: 0x001F879C
		public IEnumerator<Line> GetEnumerator()
		{
			return this.lines.GetEnumerator();
		}

		// Token: 0x0600677B RID: 26491 RVA: 0x001F87C8 File Offset: 0x001F87C8
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.lines as IEnumerator;
		}

		// Token: 0x0600677C RID: 26492 RVA: 0x001F87EC File Offset: 0x001F87EC
		public virtual int BinarySearch(Line item, IComparer<Line> comparer)
		{
			return this.lines.BinarySearch(item, comparer);
		}

		// Token: 0x0600677D RID: 26493 RVA: 0x001F8814 File Offset: 0x001F8814
		public virtual int GenerateUniqueLineId()
		{
			int num = this.lastLineUniqueId;
			this.lastLineUniqueId = num + 1;
			return num;
		}

		// Token: 0x0600677E RID: 26494 RVA: 0x001F8840 File Offset: 0x001F8840
		public virtual void InsertLine(int index, Line line)
		{
			this.lines.Insert(index, line);
			this.OnLineInserted(index);
		}

		// Token: 0x0600677F RID: 26495 RVA: 0x001F885C File Offset: 0x001F885C
		public virtual void OnLineInserted(int index)
		{
			this.OnLineInserted(index, 1);
		}

		// Token: 0x06006780 RID: 26496 RVA: 0x001F8868 File Offset: 0x001F8868
		public virtual void OnLineInserted(int index, int count)
		{
			bool flag = this.LineInserted != null;
			if (flag)
			{
				this.LineInserted(this, new LineInsertedEventArgs(index, count));
			}
		}

		// Token: 0x06006781 RID: 26497 RVA: 0x001F88A0 File Offset: 0x001F88A0
		public virtual void RemoveLine(int index)
		{
			this.RemoveLine(index, 1);
		}

		// Token: 0x170015D6 RID: 5590
		// (get) Token: 0x06006782 RID: 26498 RVA: 0x001F88AC File Offset: 0x001F88AC
		public virtual bool IsNeedBuildRemovedLineIds
		{
			get
			{
				return this.LineRemoved != null;
			}
		}

		// Token: 0x06006783 RID: 26499 RVA: 0x001F88D0 File Offset: 0x001F88D0
		public virtual void RemoveLine(int index, int count)
		{
			List<int> list = new List<int>();
			bool flag = count > 0;
			if (flag)
			{
				bool isNeedBuildRemovedLineIds = this.IsNeedBuildRemovedLineIds;
				if (isNeedBuildRemovedLineIds)
				{
					for (int i = 0; i < count; i++)
					{
						list.Add(this[index + i].UniqueId);
					}
				}
			}
			this.lines.RemoveRange(index, count);
			this.OnLineRemoved(index, count, list);
		}

		// Token: 0x06006784 RID: 26500 RVA: 0x001F8944 File Offset: 0x001F8944
		public virtual void OnLineRemoved(int index, int count, List<int> removedLineIds)
		{
			bool flag = count > 0;
			if (flag)
			{
				bool flag2 = this.LineRemoved != null;
				if (flag2)
				{
					this.LineRemoved(this, new LineRemovedEventArgs(index, count, removedLineIds));
				}
			}
		}

		// Token: 0x06006785 RID: 26501 RVA: 0x001F8988 File Offset: 0x001F8988
		public virtual void OnTextChanged(int fromLine, int toLine)
		{
			bool flag = this.TextChanged != null;
			if (flag)
			{
				this.TextChanged(this, new TextSource.TextChangedEventArgs(Math.Min(fromLine, toLine), Math.Max(fromLine, toLine)));
			}
		}

		// Token: 0x06006786 RID: 26502 RVA: 0x001F89CC File Offset: 0x001F89CC
		public virtual int IndexOf(Line item)
		{
			return this.lines.IndexOf(item);
		}

		// Token: 0x06006787 RID: 26503 RVA: 0x001F89F4 File Offset: 0x001F89F4
		public virtual void Insert(int index, Line item)
		{
			this.InsertLine(index, item);
		}

		// Token: 0x06006788 RID: 26504 RVA: 0x001F8A00 File Offset: 0x001F8A00
		public virtual void RemoveAt(int index)
		{
			this.RemoveLine(index);
		}

		// Token: 0x06006789 RID: 26505 RVA: 0x001F8A0C File Offset: 0x001F8A0C
		public virtual void Add(Line item)
		{
			this.InsertLine(this.Count, item);
		}

		// Token: 0x0600678A RID: 26506 RVA: 0x001F8A20 File Offset: 0x001F8A20
		public virtual void Clear()
		{
			this.RemoveLine(0, this.Count);
		}

		// Token: 0x0600678B RID: 26507 RVA: 0x001F8A34 File Offset: 0x001F8A34
		public virtual bool Contains(Line item)
		{
			return this.lines.Contains(item);
		}

		// Token: 0x0600678C RID: 26508 RVA: 0x001F8A5C File Offset: 0x001F8A5C
		public virtual void CopyTo(Line[] array, int arrayIndex)
		{
			this.lines.CopyTo(array, arrayIndex);
		}

		// Token: 0x170015D7 RID: 5591
		// (get) Token: 0x0600678D RID: 26509 RVA: 0x001F8A70 File Offset: 0x001F8A70
		public virtual int Count
		{
			get
			{
				return this.lines.Count;
			}
		}

		// Token: 0x170015D8 RID: 5592
		// (get) Token: 0x0600678E RID: 26510 RVA: 0x001F8A94 File Offset: 0x001F8A94
		public virtual bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600678F RID: 26511 RVA: 0x001F8AB0 File Offset: 0x001F8AB0
		public virtual bool Remove(Line item)
		{
			int num = this.IndexOf(item);
			bool flag = num >= 0;
			bool result;
			if (flag)
			{
				this.RemoveLine(num);
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06006790 RID: 26512 RVA: 0x001F8AF0 File Offset: 0x001F8AF0
		public virtual void NeedRecalc(TextSource.TextChangedEventArgs args)
		{
			bool flag = this.RecalcNeeded != null;
			if (flag)
			{
				this.RecalcNeeded(this, args);
			}
		}

		// Token: 0x06006791 RID: 26513 RVA: 0x001F8B20 File Offset: 0x001F8B20
		public virtual void OnRecalcWordWrap(TextSource.TextChangedEventArgs args)
		{
			bool flag = this.RecalcWordWrap != null;
			if (flag)
			{
				this.RecalcWordWrap(this, args);
			}
		}

		// Token: 0x06006792 RID: 26514 RVA: 0x001F8B50 File Offset: 0x001F8B50
		public virtual void OnTextChanging()
		{
			string text = null;
			this.OnTextChanging(ref text);
		}

		// Token: 0x06006793 RID: 26515 RVA: 0x001F8B70 File Offset: 0x001F8B70
		public virtual void OnTextChanging(ref string text)
		{
			bool flag = this.TextChanging != null;
			if (flag)
			{
				TextChangingEventArgs textChangingEventArgs = new TextChangingEventArgs
				{
					InsertingText = text
				};
				this.TextChanging(this, textChangingEventArgs);
				text = textChangingEventArgs.InsertingText;
				bool cancel = textChangingEventArgs.Cancel;
				if (cancel)
				{
					text = string.Empty;
				}
			}
		}

		// Token: 0x06006794 RID: 26516 RVA: 0x001F8BD0 File Offset: 0x001F8BD0
		public virtual int GetLineLength(int i)
		{
			return this.lines[i].Count;
		}

		// Token: 0x06006795 RID: 26517 RVA: 0x001F8BFC File Offset: 0x001F8BFC
		public virtual bool LineHasFoldingStartMarker(int iLine)
		{
			return !string.IsNullOrEmpty(this.lines[iLine].FoldingStartMarker);
		}

		// Token: 0x06006796 RID: 26518 RVA: 0x001F8C30 File Offset: 0x001F8C30
		public virtual bool LineHasFoldingEndMarker(int iLine)
		{
			return !string.IsNullOrEmpty(this.lines[iLine].FoldingEndMarker);
		}

		// Token: 0x06006797 RID: 26519 RVA: 0x001F8C64 File Offset: 0x001F8C64
		public virtual void Dispose()
		{
		}

		// Token: 0x06006798 RID: 26520 RVA: 0x001F8C68 File Offset: 0x001F8C68
		public virtual void SaveToFile(string fileName, Encoding enc)
		{
			using (StreamWriter streamWriter = new StreamWriter(fileName, false, enc))
			{
				for (int i = 0; i < this.Count - 1; i++)
				{
					streamWriter.WriteLine(this.lines[i].Text);
				}
				streamWriter.Write(this.lines[this.Count - 1].Text);
			}
		}

		// Token: 0x040034BD RID: 13501
		protected readonly List<Line> lines = new List<Line>();

		// Token: 0x040034BE RID: 13502
		protected LinesAccessor linesAccessor;

		// Token: 0x040034BF RID: 13503
		private int lastLineUniqueId;

		// Token: 0x040034C1 RID: 13505
		private FastColoredTextBox currentTB;

		// Token: 0x040034C2 RID: 13506
		public readonly Style[] Styles;

		// Token: 0x02001069 RID: 4201
		public class TextChangedEventArgs : EventArgs
		{
			// Token: 0x0600906D RID: 36973 RVA: 0x002B02B4 File Offset: 0x002B02B4
			public TextChangedEventArgs(int iFromLine, int iToLine)
			{
				this.iFromLine = iFromLine;
				this.iToLine = iToLine;
			}

			// Token: 0x040045FE RID: 17918
			public int iFromLine;

			// Token: 0x040045FF RID: 17919
			public int iToLine;
		}
	}
}
