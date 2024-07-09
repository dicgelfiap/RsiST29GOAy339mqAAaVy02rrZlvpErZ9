using System;

namespace FastColoredTextBoxNS
{
	// Token: 0x02000A30 RID: 2608
	public class InsertCharCommand : UndoableCommand
	{
		// Token: 0x06006656 RID: 26198 RVA: 0x001F0558 File Offset: 0x001F0558
		public InsertCharCommand(TextSource ts, char c) : base(ts)
		{
			this.c = c;
		}

		// Token: 0x06006657 RID: 26199 RVA: 0x001F0574 File Offset: 0x001F0574
		public override void Undo()
		{
			this.ts.OnTextChanging();
			switch (this.c)
			{
			case '\b':
			{
				this.ts.CurrentTB.Selection.Start = this.lastSel.Start;
				char c = '\0';
				bool flag = this.deletedChar > '\0';
				if (flag)
				{
					this.ts.CurrentTB.ExpandBlock(this.ts.CurrentTB.Selection.Start.iLine);
					InsertCharCommand.InsertChar(this.deletedChar, ref c, this.ts);
				}
				goto IL_1F1;
			}
			case '\t':
				this.ts.CurrentTB.ExpandBlock(this.sel.Start.iLine);
				for (int i = this.sel.FromX; i < this.lastSel.FromX; i++)
				{
					this.ts[this.sel.Start.iLine].RemoveAt(this.sel.Start.iChar);
				}
				this.ts.CurrentTB.Selection.Start = this.sel.Start;
				goto IL_1F1;
			case '\n':
				InsertCharCommand.MergeLines(this.sel.Start.iLine, this.ts);
				goto IL_1F1;
			case '\r':
				goto IL_1F1;
			}
			this.ts.CurrentTB.ExpandBlock(this.sel.Start.iLine);
			this.ts[this.sel.Start.iLine].RemoveAt(this.sel.Start.iChar);
			this.ts.CurrentTB.Selection.Start = this.sel.Start;
			IL_1F1:
			this.ts.NeedRecalc(new TextSource.TextChangedEventArgs(this.sel.Start.iLine, this.sel.Start.iLine));
			base.Undo();
		}

		// Token: 0x06006658 RID: 26200 RVA: 0x001F07B0 File Offset: 0x001F07B0
		public override void Execute()
		{
			this.ts.CurrentTB.ExpandBlock(this.ts.CurrentTB.Selection.Start.iLine);
			string text = this.c.ToString();
			this.ts.OnTextChanging(ref text);
			bool flag = text.Length == 1;
			if (flag)
			{
				this.c = text[0];
			}
			bool flag2 = string.IsNullOrEmpty(text);
			if (flag2)
			{
				throw new ArgumentOutOfRangeException();
			}
			bool flag3 = this.ts.Count == 0;
			if (flag3)
			{
				InsertCharCommand.InsertLine(this.ts);
			}
			InsertCharCommand.InsertChar(this.c, ref this.deletedChar, this.ts);
			this.ts.NeedRecalc(new TextSource.TextChangedEventArgs(this.ts.CurrentTB.Selection.Start.iLine, this.ts.CurrentTB.Selection.Start.iLine));
			base.Execute();
		}

		// Token: 0x06006659 RID: 26201 RVA: 0x001F08BC File Offset: 0x001F08BC
		internal static void InsertChar(char c, ref char deletedChar, TextSource ts)
		{
			FastColoredTextBox currentTB = ts.CurrentTB;
			switch (c)
			{
			case '\b':
			{
				bool flag = currentTB.Selection.Start.iChar == 0 && currentTB.Selection.Start.iLine == 0;
				if (flag)
				{
					return;
				}
				bool flag2 = currentTB.Selection.Start.iChar == 0;
				if (flag2)
				{
					bool flag3 = !ts.CurrentTB.AllowInsertRemoveLines;
					if (flag3)
					{
						throw new ArgumentOutOfRangeException("Cant insert this char in ColumnRange mode");
					}
					bool flag4 = currentTB.LineInfos[currentTB.Selection.Start.iLine - 1].VisibleState > VisibleState.Visible;
					if (flag4)
					{
						currentTB.ExpandBlock(currentTB.Selection.Start.iLine - 1);
					}
					deletedChar = '\n';
					InsertCharCommand.MergeLines(currentTB.Selection.Start.iLine - 1, ts);
				}
				else
				{
					deletedChar = ts[currentTB.Selection.Start.iLine][currentTB.Selection.Start.iChar - 1].c;
					ts[currentTB.Selection.Start.iLine].RemoveAt(currentTB.Selection.Start.iChar - 1);
					currentTB.Selection.Start = new Place(currentTB.Selection.Start.iChar - 1, currentTB.Selection.Start.iLine);
				}
				return;
			}
			case '\t':
			{
				int num = currentTB.TabLength - currentTB.Selection.Start.iChar % currentTB.TabLength;
				bool flag5 = num == 0;
				if (flag5)
				{
					num = currentTB.TabLength;
				}
				for (int i = 0; i < num; i++)
				{
					ts[currentTB.Selection.Start.iLine].Insert(currentTB.Selection.Start.iChar, new Char(' '));
				}
				currentTB.Selection.Start = new Place(currentTB.Selection.Start.iChar + num, currentTB.Selection.Start.iLine);
				return;
			}
			case '\n':
			{
				bool flag6 = !ts.CurrentTB.AllowInsertRemoveLines;
				if (flag6)
				{
					throw new ArgumentOutOfRangeException("Cant insert this char in ColumnRange mode");
				}
				bool flag7 = ts.Count == 0;
				if (flag7)
				{
					InsertCharCommand.InsertLine(ts);
				}
				InsertCharCommand.InsertLine(ts);
				return;
			}
			case '\r':
				return;
			}
			ts[currentTB.Selection.Start.iLine].Insert(currentTB.Selection.Start.iChar, new Char(c));
			currentTB.Selection.Start = new Place(currentTB.Selection.Start.iChar + 1, currentTB.Selection.Start.iLine);
		}

		// Token: 0x0600665A RID: 26202 RVA: 0x001F0BEC File Offset: 0x001F0BEC
		internal static void InsertLine(TextSource ts)
		{
			FastColoredTextBox currentTB = ts.CurrentTB;
			bool flag = !currentTB.Multiline && currentTB.LinesCount > 0;
			if (!flag)
			{
				bool flag2 = ts.Count == 0;
				if (flag2)
				{
					ts.InsertLine(0, ts.CreateLine());
				}
				else
				{
					InsertCharCommand.BreakLines(currentTB.Selection.Start.iLine, currentTB.Selection.Start.iChar, ts);
				}
				currentTB.Selection.Start = new Place(0, currentTB.Selection.Start.iLine + 1);
				ts.NeedRecalc(new TextSource.TextChangedEventArgs(0, 1));
			}
		}

		// Token: 0x0600665B RID: 26203 RVA: 0x001F0CA8 File Offset: 0x001F0CA8
		internal static void MergeLines(int i, TextSource ts)
		{
			FastColoredTextBox currentTB = ts.CurrentTB;
			bool flag = i + 1 >= ts.Count;
			if (!flag)
			{
				currentTB.ExpandBlock(i);
				currentTB.ExpandBlock(i + 1);
				int count = ts[i].Count;
				bool flag2 = ts[i + 1].Count == 0;
				if (flag2)
				{
					ts.RemoveLine(i + 1);
				}
				else
				{
					ts[i].AddRange(ts[i + 1]);
					ts.RemoveLine(i + 1);
				}
				currentTB.Selection.Start = new Place(count, i);
				ts.NeedRecalc(new TextSource.TextChangedEventArgs(0, 1));
			}
		}

		// Token: 0x0600665C RID: 26204 RVA: 0x001F0D64 File Offset: 0x001F0D64
		internal static void BreakLines(int iLine, int pos, TextSource ts)
		{
			Line line = ts.CreateLine();
			for (int i = pos; i < ts[iLine].Count; i++)
			{
				line.Add(ts[iLine][i]);
			}
			ts[iLine].RemoveRange(pos, ts[iLine].Count - pos);
			ts.InsertLine(iLine + 1, line);
		}

		// Token: 0x0600665D RID: 26205 RVA: 0x001F0DD8 File Offset: 0x001F0DD8
		public override UndoableCommand Clone()
		{
			return new InsertCharCommand(this.ts, this.c);
		}

		// Token: 0x04003472 RID: 13426
		public char c;

		// Token: 0x04003473 RID: 13427
		private char deletedChar = '\0';
	}
}
