using System;
using System.Collections.Generic;

namespace FastColoredTextBoxNS
{
	// Token: 0x02000A36 RID: 2614
	public class MultiRangeCommand : UndoableCommand
	{
		// Token: 0x06006675 RID: 26229 RVA: 0x001F1DE0 File Offset: 0x001F1DE0
		public MultiRangeCommand(UndoableCommand command) : base(command.ts)
		{
			this.cmd = command;
			this.range = this.ts.CurrentTB.Selection.Clone();
		}

		// Token: 0x06006676 RID: 26230 RVA: 0x001F1E2C File Offset: 0x001F1E2C
		public override void Execute()
		{
			this.commandsByRanges.Clear();
			Range range = this.range.Clone();
			int num = -1;
			int iLine = range.Start.iLine;
			int iLine2 = range.End.iLine;
			this.ts.CurrentTB.Selection.ColumnSelectionMode = false;
			this.ts.CurrentTB.Selection.BeginUpdate();
			this.ts.CurrentTB.BeginUpdate();
			this.ts.CurrentTB.AllowInsertRemoveLines = false;
			try
			{
				bool flag = this.cmd is InsertTextCommand;
				if (flag)
				{
					this.ExecuteInsertTextCommand(ref num, (this.cmd as InsertTextCommand).InsertedText);
				}
				else
				{
					bool flag2 = this.cmd is InsertCharCommand && (this.cmd as InsertCharCommand).c != '\0' && (this.cmd as InsertCharCommand).c != '\b';
					if (flag2)
					{
						this.ExecuteInsertTextCommand(ref num, (this.cmd as InsertCharCommand).c.ToString());
					}
					else
					{
						this.ExecuteCommand(ref num);
					}
				}
			}
			catch (ArgumentOutOfRangeException)
			{
			}
			finally
			{
				this.ts.CurrentTB.AllowInsertRemoveLines = true;
				this.ts.CurrentTB.EndUpdate();
				this.ts.CurrentTB.Selection = this.range;
				bool flag3 = num >= 0;
				if (flag3)
				{
					this.ts.CurrentTB.Selection.Start = new Place(num, iLine);
					this.ts.CurrentTB.Selection.End = new Place(num, iLine2);
				}
				this.ts.CurrentTB.Selection.ColumnSelectionMode = true;
				this.ts.CurrentTB.Selection.EndUpdate();
			}
		}

		// Token: 0x06006677 RID: 26231 RVA: 0x001F204C File Offset: 0x001F204C
		private void ExecuteInsertTextCommand(ref int iChar, string text)
		{
			string[] array = text.Split(new char[]
			{
				'\n'
			});
			int num = 0;
			foreach (Range range in this.range.GetSubRanges(true))
			{
				Line line = this.ts.CurrentTB[range.Start.iLine];
				bool flag = range.End < range.Start && line.StartSpacesCount == line.Count;
				bool flag2 = !flag;
				if (flag2)
				{
					string text2 = array[num % array.Length];
					bool flag3 = range.End < range.Start && text2 != "";
					if (flag3)
					{
						text2 = new string(' ', range.Start.iChar - range.End.iChar) + text2;
						range.Start = range.End;
					}
					this.ts.CurrentTB.Selection = range;
					InsertTextCommand insertTextCommand = new InsertTextCommand(this.ts, text2);
					insertTextCommand.Execute();
					bool flag4 = this.ts.CurrentTB.Selection.End.iChar > iChar;
					if (flag4)
					{
						iChar = this.ts.CurrentTB.Selection.End.iChar;
					}
					this.commandsByRanges.Add(insertTextCommand);
				}
				num++;
			}
		}

		// Token: 0x06006678 RID: 26232 RVA: 0x001F2218 File Offset: 0x001F2218
		private void ExecuteCommand(ref int iChar)
		{
			foreach (Range selection in this.range.GetSubRanges(false))
			{
				this.ts.CurrentTB.Selection = selection;
				UndoableCommand undoableCommand = this.cmd.Clone();
				undoableCommand.Execute();
				bool flag = this.ts.CurrentTB.Selection.End.iChar > iChar;
				if (flag)
				{
					iChar = this.ts.CurrentTB.Selection.End.iChar;
				}
				this.commandsByRanges.Add(undoableCommand);
			}
		}

		// Token: 0x06006679 RID: 26233 RVA: 0x001F22E8 File Offset: 0x001F22E8
		public override void Undo()
		{
			this.ts.CurrentTB.BeginUpdate();
			this.ts.CurrentTB.Selection.BeginUpdate();
			try
			{
				for (int i = this.commandsByRanges.Count - 1; i >= 0; i--)
				{
					this.commandsByRanges[i].Undo();
				}
			}
			finally
			{
				this.ts.CurrentTB.Selection.EndUpdate();
				this.ts.CurrentTB.EndUpdate();
			}
			this.ts.CurrentTB.Selection = this.range.Clone();
			this.ts.CurrentTB.OnTextChanged(this.range);
			this.ts.CurrentTB.OnSelectionChanged();
			this.ts.CurrentTB.Selection.ColumnSelectionMode = true;
		}

		// Token: 0x0600667A RID: 26234 RVA: 0x001F23F0 File Offset: 0x001F23F0
		public override UndoableCommand Clone()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0400347D RID: 13437
		private UndoableCommand cmd;

		// Token: 0x0400347E RID: 13438
		private Range range;

		// Token: 0x0400347F RID: 13439
		private List<UndoableCommand> commandsByRanges = new List<UndoableCommand>();
	}
}
