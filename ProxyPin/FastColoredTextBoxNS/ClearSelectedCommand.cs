using System;

namespace FastColoredTextBoxNS
{
	// Token: 0x02000A33 RID: 2611
	public class ClearSelectedCommand : UndoableCommand
	{
		// Token: 0x06006668 RID: 26216 RVA: 0x001F14A0 File Offset: 0x001F14A0
		public ClearSelectedCommand(TextSource ts) : base(ts)
		{
		}

		// Token: 0x06006669 RID: 26217 RVA: 0x001F14AC File Offset: 0x001F14AC
		public override void Undo()
		{
			this.ts.CurrentTB.Selection.Start = new Place(this.sel.FromX, Math.Min(this.sel.Start.iLine, this.sel.End.iLine));
			this.ts.OnTextChanging();
			InsertTextCommand.InsertText(this.deletedText, this.ts);
			this.ts.OnTextChanged(this.sel.Start.iLine, this.sel.End.iLine);
			this.ts.CurrentTB.Selection.Start = this.sel.Start;
			this.ts.CurrentTB.Selection.End = this.sel.End;
		}

		// Token: 0x0600666A RID: 26218 RVA: 0x001F1598 File Offset: 0x001F1598
		public override void Execute()
		{
			FastColoredTextBox currentTB = this.ts.CurrentTB;
			string a = null;
			this.ts.OnTextChanging(ref a);
			bool flag = a == "";
			if (flag)
			{
				throw new ArgumentOutOfRangeException();
			}
			this.deletedText = currentTB.Selection.Text;
			ClearSelectedCommand.ClearSelected(this.ts);
			this.lastSel = new RangeInfo(currentTB.Selection);
			this.ts.OnTextChanged(this.lastSel.Start.iLine, this.lastSel.Start.iLine);
		}

		// Token: 0x0600666B RID: 26219 RVA: 0x001F1638 File Offset: 0x001F1638
		internal static void ClearSelected(TextSource ts)
		{
			FastColoredTextBox currentTB = ts.CurrentTB;
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
				currentTB.Selection.Start = new Place(fromX, num);
				ts.NeedRecalc(new TextSource.TextChangedEventArgs(num, num2));
			}
		}

		// Token: 0x0600666C RID: 26220 RVA: 0x001F1750 File Offset: 0x001F1750
		public override UndoableCommand Clone()
		{
			return new ClearSelectedCommand(this.ts);
		}

		// Token: 0x04003478 RID: 13432
		private string deletedText;
	}
}
