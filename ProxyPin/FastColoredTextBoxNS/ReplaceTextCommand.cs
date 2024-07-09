using System;
using System.Collections.Generic;

namespace FastColoredTextBoxNS
{
	// Token: 0x02000A32 RID: 2610
	public class ReplaceTextCommand : UndoableCommand
	{
		// Token: 0x06006663 RID: 26211 RVA: 0x001F1000 File Offset: 0x001F1000
		public ReplaceTextCommand(TextSource ts, List<Range> ranges, string insertedText) : base(ts)
		{
			ranges.Sort(delegate(Range r1, Range r2)
			{
				bool flag = r1.Start.iLine == r2.Start.iLine;
				int result;
				if (flag)
				{
					Place start = r1.Start;
					result = start.iChar.CompareTo(r2.Start.iChar);
				}
				else
				{
					Place start = r1.Start;
					result = start.iLine.CompareTo(r2.Start.iLine);
				}
				return result;
			});
			this.ranges = ranges;
			this.insertedText = insertedText;
			this.lastSel = (this.sel = new RangeInfo(ts.CurrentTB.Selection));
		}

		// Token: 0x06006664 RID: 26212 RVA: 0x001F107C File Offset: 0x001F107C
		public override void Undo()
		{
			FastColoredTextBox currentTB = this.ts.CurrentTB;
			this.ts.OnTextChanging();
			currentTB.BeginUpdate();
			currentTB.Selection.BeginUpdate();
			for (int i = 0; i < this.ranges.Count; i++)
			{
				currentTB.Selection.Start = this.ranges[i].Start;
				for (int j = 0; j < this.insertedText.Length; j++)
				{
					currentTB.Selection.GoRight(true);
				}
				ReplaceTextCommand.ClearSelected(this.ts);
				InsertTextCommand.InsertText(this.prevText[this.prevText.Count - i - 1], this.ts);
			}
			currentTB.Selection.EndUpdate();
			currentTB.EndUpdate();
			bool flag = this.ranges.Count > 0;
			if (flag)
			{
				this.ts.OnTextChanged(this.ranges[0].Start.iLine, this.ranges[this.ranges.Count - 1].End.iLine);
			}
			this.ts.NeedRecalc(new TextSource.TextChangedEventArgs(0, 1));
		}

		// Token: 0x06006665 RID: 26213 RVA: 0x001F11D8 File Offset: 0x001F11D8
		public override void Execute()
		{
			FastColoredTextBox currentTB = this.ts.CurrentTB;
			this.prevText.Clear();
			this.ts.OnTextChanging(ref this.insertedText);
			currentTB.Selection.BeginUpdate();
			currentTB.BeginUpdate();
			for (int i = this.ranges.Count - 1; i >= 0; i--)
			{
				currentTB.Selection.Start = this.ranges[i].Start;
				currentTB.Selection.End = this.ranges[i].End;
				this.prevText.Add(currentTB.Selection.Text);
				ReplaceTextCommand.ClearSelected(this.ts);
				bool flag = this.insertedText != "";
				if (flag)
				{
					InsertTextCommand.InsertText(this.insertedText, this.ts);
				}
			}
			bool flag2 = this.ranges.Count > 0;
			if (flag2)
			{
				this.ts.OnTextChanged(this.ranges[0].Start.iLine, this.ranges[this.ranges.Count - 1].End.iLine);
			}
			currentTB.EndUpdate();
			currentTB.Selection.EndUpdate();
			this.ts.NeedRecalc(new TextSource.TextChangedEventArgs(0, 1));
			this.lastSel = new RangeInfo(currentTB.Selection);
		}

		// Token: 0x06006666 RID: 26214 RVA: 0x001F1368 File Offset: 0x001F1368
		public override UndoableCommand Clone()
		{
			return new ReplaceTextCommand(this.ts, new List<Range>(this.ranges), this.insertedText);
		}

		// Token: 0x06006667 RID: 26215 RVA: 0x001F13A0 File Offset: 0x001F13A0
		internal static void ClearSelected(TextSource ts)
		{
			FastColoredTextBox currentTB = ts.CurrentTB;
			currentTB.Selection.Normalize();
			Place start = currentTB.Selection.Start;
			Place end = currentTB.Selection.End;
			int num = Math.Min(end.iLine, start.iLine);
			int num2 = Math.Max(end.iLine, start.iLine);
			int fromX = currentTB.Selection.FromX;
			int toX = currentTB.Selection.ToX;
			bool flag = num < 0;
			if (!flag)
			{
				bool flag2 = num == num2;
				if (flag2)
				{
					ts[num].RemoveRange(fromX, toX - fromX);
				}
				else
				{
					ts[num].RemoveRange(fromX, ts[num].Count - fromX);
					ts[num2].RemoveRange(0, toX);
					ts.RemoveLine(num + 1, num2 - num - 1);
					InsertCharCommand.MergeLines(num, ts);
				}
			}
		}

		// Token: 0x04003475 RID: 13429
		private string insertedText;

		// Token: 0x04003476 RID: 13430
		private List<Range> ranges;

		// Token: 0x04003477 RID: 13431
		private List<string> prevText = new List<string>();
	}
}
