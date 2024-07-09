using System;

namespace FastColoredTextBoxNS
{
	// Token: 0x02000A31 RID: 2609
	public class InsertTextCommand : UndoableCommand
	{
		// Token: 0x0600665E RID: 26206 RVA: 0x001F0E04 File Offset: 0x001F0E04
		public InsertTextCommand(TextSource ts, string insertedText) : base(ts)
		{
			this.InsertedText = insertedText;
		}

		// Token: 0x0600665F RID: 26207 RVA: 0x001F0E18 File Offset: 0x001F0E18
		public override void Undo()
		{
			this.ts.CurrentTB.Selection.Start = this.sel.Start;
			this.ts.CurrentTB.Selection.End = this.lastSel.Start;
			this.ts.OnTextChanging();
			ClearSelectedCommand.ClearSelected(this.ts);
			base.Undo();
		}

		// Token: 0x06006660 RID: 26208 RVA: 0x001F0E8C File Offset: 0x001F0E8C
		public override void Execute()
		{
			this.ts.OnTextChanging(ref this.InsertedText);
			InsertTextCommand.InsertText(this.InsertedText, this.ts);
			base.Execute();
		}

		// Token: 0x06006661 RID: 26209 RVA: 0x001F0EBC File Offset: 0x001F0EBC
		internal static void InsertText(string insertedText, TextSource ts)
		{
			FastColoredTextBox currentTB = ts.CurrentTB;
			try
			{
				currentTB.Selection.BeginUpdate();
				char c = '\0';
				bool flag = ts.Count == 0;
				if (flag)
				{
					InsertCharCommand.InsertLine(ts);
					currentTB.Selection.Start = Place.Empty;
				}
				currentTB.ExpandBlock(currentTB.Selection.Start.iLine);
				int length = insertedText.Length;
				for (int i = 0; i < length; i++)
				{
					char c2 = insertedText[i];
					bool flag2 = c2 == '\r' && (i >= length - 1 || insertedText[i + 1] != '\n');
					if (flag2)
					{
						InsertCharCommand.InsertChar('\n', ref c, ts);
					}
					else
					{
						InsertCharCommand.InsertChar(c2, ref c, ts);
					}
				}
				ts.NeedRecalc(new TextSource.TextChangedEventArgs(0, 1));
			}
			finally
			{
				currentTB.Selection.EndUpdate();
			}
		}

		// Token: 0x06006662 RID: 26210 RVA: 0x001F0FD4 File Offset: 0x001F0FD4
		public override UndoableCommand Clone()
		{
			return new InsertTextCommand(this.ts, this.InsertedText);
		}

		// Token: 0x04003474 RID: 13428
		public string InsertedText;
	}
}
