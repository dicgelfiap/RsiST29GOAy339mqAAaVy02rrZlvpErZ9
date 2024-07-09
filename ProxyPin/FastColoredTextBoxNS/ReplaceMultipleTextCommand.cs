using System;
using System.Collections.Generic;

namespace FastColoredTextBoxNS
{
	// Token: 0x02000A34 RID: 2612
	public class ReplaceMultipleTextCommand : UndoableCommand
	{
		// Token: 0x0600666D RID: 26221 RVA: 0x001F1774 File Offset: 0x001F1774
		public ReplaceMultipleTextCommand(TextSource ts, List<ReplaceMultipleTextCommand.ReplaceRange> ranges) : base(ts)
		{
			ranges.Sort(delegate(ReplaceMultipleTextCommand.ReplaceRange r1, ReplaceMultipleTextCommand.ReplaceRange r2)
			{
				bool flag = r1.ReplacedRange.Start.iLine == r2.ReplacedRange.Start.iLine;
				int result;
				if (flag)
				{
					Place start = r1.ReplacedRange.Start;
					result = start.iChar.CompareTo(r2.ReplacedRange.Start.iChar);
				}
				else
				{
					Place start = r1.ReplacedRange.Start;
					result = start.iLine.CompareTo(r2.ReplacedRange.Start.iLine);
				}
				return result;
			});
			this.ranges = ranges;
			this.lastSel = (this.sel = new RangeInfo(ts.CurrentTB.Selection));
		}

		// Token: 0x0600666E RID: 26222 RVA: 0x001F17E8 File Offset: 0x001F17E8
		public override void Undo()
		{
			FastColoredTextBox currentTB = this.ts.CurrentTB;
			this.ts.OnTextChanging();
			currentTB.Selection.BeginUpdate();
			for (int i = 0; i < this.ranges.Count; i++)
			{
				currentTB.Selection.Start = this.ranges[i].ReplacedRange.Start;
				for (int j = 0; j < this.ranges[i].ReplaceText.Length; j++)
				{
					currentTB.Selection.GoRight(true);
				}
				ClearSelectedCommand.ClearSelected(this.ts);
				int index = this.ranges.Count - 1 - i;
				InsertTextCommand.InsertText(this.prevText[index], this.ts);
				this.ts.OnTextChanged(this.ranges[i].ReplacedRange.Start.iLine, this.ranges[i].ReplacedRange.Start.iLine);
			}
			currentTB.Selection.EndUpdate();
			this.ts.NeedRecalc(new TextSource.TextChangedEventArgs(0, 1));
		}

		// Token: 0x0600666F RID: 26223 RVA: 0x001F1930 File Offset: 0x001F1930
		public override void Execute()
		{
			FastColoredTextBox currentTB = this.ts.CurrentTB;
			this.prevText.Clear();
			this.ts.OnTextChanging();
			currentTB.Selection.BeginUpdate();
			for (int i = this.ranges.Count - 1; i >= 0; i--)
			{
				currentTB.Selection.Start = this.ranges[i].ReplacedRange.Start;
				currentTB.Selection.End = this.ranges[i].ReplacedRange.End;
				this.prevText.Add(currentTB.Selection.Text);
				ClearSelectedCommand.ClearSelected(this.ts);
				InsertTextCommand.InsertText(this.ranges[i].ReplaceText, this.ts);
				this.ts.OnTextChanged(this.ranges[i].ReplacedRange.Start.iLine, this.ranges[i].ReplacedRange.End.iLine);
			}
			currentTB.Selection.EndUpdate();
			this.ts.NeedRecalc(new TextSource.TextChangedEventArgs(0, 1));
			this.lastSel = new RangeInfo(currentTB.Selection);
		}

		// Token: 0x06006670 RID: 26224 RVA: 0x001F1A90 File Offset: 0x001F1A90
		public override UndoableCommand Clone()
		{
			return new ReplaceMultipleTextCommand(this.ts, new List<ReplaceMultipleTextCommand.ReplaceRange>(this.ranges));
		}

		// Token: 0x04003479 RID: 13433
		private List<ReplaceMultipleTextCommand.ReplaceRange> ranges;

		// Token: 0x0400347A RID: 13434
		private List<string> prevText = new List<string>();

		// Token: 0x0200105D RID: 4189
		public class ReplaceRange
		{
			// Token: 0x17001E1E RID: 7710
			// (get) Token: 0x06009011 RID: 36881 RVA: 0x002AE2E0 File Offset: 0x002AE2E0
			// (set) Token: 0x06009012 RID: 36882 RVA: 0x002AE2E8 File Offset: 0x002AE2E8
			public Range ReplacedRange { get; set; }

			// Token: 0x17001E1F RID: 7711
			// (get) Token: 0x06009013 RID: 36883 RVA: 0x002AE2F4 File Offset: 0x002AE2F4
			// (set) Token: 0x06009014 RID: 36884 RVA: 0x002AE2FC File Offset: 0x002AE2FC
			public string ReplaceText { get; set; }
		}
	}
}
