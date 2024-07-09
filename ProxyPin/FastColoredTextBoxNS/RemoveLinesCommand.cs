using System;
using System.Collections.Generic;

namespace FastColoredTextBoxNS
{
	// Token: 0x02000A35 RID: 2613
	public class RemoveLinesCommand : UndoableCommand
	{
		// Token: 0x06006671 RID: 26225 RVA: 0x001F1AC0 File Offset: 0x001F1AC0
		public RemoveLinesCommand(TextSource ts, List<int> iLines) : base(ts)
		{
			iLines.Sort();
			this.iLines = iLines;
			this.lastSel = (this.sel = new RangeInfo(ts.CurrentTB.Selection));
		}

		// Token: 0x06006672 RID: 26226 RVA: 0x001F1B14 File Offset: 0x001F1B14
		public override void Undo()
		{
			FastColoredTextBox currentTB = this.ts.CurrentTB;
			this.ts.OnTextChanging();
			currentTB.Selection.BeginUpdate();
			for (int i = 0; i < this.iLines.Count; i++)
			{
				int num = this.iLines[i];
				bool flag = num < this.ts.Count;
				if (flag)
				{
					currentTB.Selection.Start = new Place(0, num);
				}
				else
				{
					currentTB.Selection.Start = new Place(this.ts[this.ts.Count - 1].Count, this.ts.Count - 1);
				}
				InsertCharCommand.InsertLine(this.ts);
				currentTB.Selection.Start = new Place(0, num);
				string text = this.prevText[this.prevText.Count - i - 1];
				InsertTextCommand.InsertText(text, this.ts);
				this.ts[num].IsChanged = true;
				bool flag2 = num < this.ts.Count - 1;
				if (flag2)
				{
					this.ts[num + 1].IsChanged = true;
				}
				else
				{
					this.ts[num - 1].IsChanged = true;
				}
				bool flag3 = text.Trim() != string.Empty;
				if (flag3)
				{
					this.ts.OnTextChanged(num, num);
				}
			}
			currentTB.Selection.EndUpdate();
			this.ts.NeedRecalc(new TextSource.TextChangedEventArgs(0, 1));
		}

		// Token: 0x06006673 RID: 26227 RVA: 0x001F1CD0 File Offset: 0x001F1CD0
		public override void Execute()
		{
			FastColoredTextBox currentTB = this.ts.CurrentTB;
			this.prevText.Clear();
			this.ts.OnTextChanging();
			currentTB.Selection.BeginUpdate();
			for (int i = this.iLines.Count - 1; i >= 0; i--)
			{
				int num = this.iLines[i];
				this.prevText.Add(this.ts[num].Text);
				this.ts.RemoveLine(num);
			}
			currentTB.Selection.Start = new Place(0, 0);
			currentTB.Selection.EndUpdate();
			this.ts.NeedRecalc(new TextSource.TextChangedEventArgs(0, 1));
			this.lastSel = new RangeInfo(currentTB.Selection);
		}

		// Token: 0x06006674 RID: 26228 RVA: 0x001F1DB0 File Offset: 0x001F1DB0
		public override UndoableCommand Clone()
		{
			return new RemoveLinesCommand(this.ts, new List<int>(this.iLines));
		}

		// Token: 0x0400347B RID: 13435
		private List<int> iLines;

		// Token: 0x0400347C RID: 13436
		private List<string> prevText = new List<string>();
	}
}
