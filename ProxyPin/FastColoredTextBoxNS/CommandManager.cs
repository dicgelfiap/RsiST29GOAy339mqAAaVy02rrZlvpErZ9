using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace FastColoredTextBoxNS
{
	// Token: 0x02000A2C RID: 2604
	public class CommandManager
	{
		// Token: 0x1700159F RID: 5535
		// (get) Token: 0x06006638 RID: 26168 RVA: 0x001EFDC8 File Offset: 0x001EFDC8
		// (set) Token: 0x06006639 RID: 26169 RVA: 0x001EFDD0 File Offset: 0x001EFDD0
		public TextSource TextSource { get; private set; }

		// Token: 0x170015A0 RID: 5536
		// (get) Token: 0x0600663A RID: 26170 RVA: 0x001EFDDC File Offset: 0x001EFDDC
		// (set) Token: 0x0600663B RID: 26171 RVA: 0x001EFDE4 File Offset: 0x001EFDE4
		public bool UndoRedoStackIsEnabled { get; set; }

		// Token: 0x14000049 RID: 73
		// (add) Token: 0x0600663C RID: 26172 RVA: 0x001EFDF0 File Offset: 0x001EFDF0
		// (remove) Token: 0x0600663D RID: 26173 RVA: 0x001EFE2C File Offset: 0x001EFE2C
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler RedoCompleted = delegate(object <sender>, EventArgs <e>)
		{
		};

		// Token: 0x0600663E RID: 26174 RVA: 0x001EFE68 File Offset: 0x001EFE68
		public CommandManager(TextSource ts)
		{
			this.history = new LimitedStack<UndoableCommand>(this.maxHistoryLength);
			this.TextSource = ts;
			this.UndoRedoStackIsEnabled = true;
		}

		// Token: 0x0600663F RID: 26175 RVA: 0x001EFEF0 File Offset: 0x001EFEF0
		public virtual void ExecuteCommand(Command cmd)
		{
			bool flag = this.disabledCommands > 0;
			if (!flag)
			{
				bool columnSelectionMode = cmd.ts.CurrentTB.Selection.ColumnSelectionMode;
				if (columnSelectionMode)
				{
					bool flag2 = cmd is UndoableCommand;
					if (flag2)
					{
						cmd = new MultiRangeCommand((UndoableCommand)cmd);
					}
				}
				bool flag3 = cmd is UndoableCommand;
				if (flag3)
				{
					(cmd as UndoableCommand).autoUndo = (this.autoUndoCommands > 0);
					this.history.Push(cmd as UndoableCommand);
				}
				try
				{
					cmd.Execute();
				}
				catch (ArgumentOutOfRangeException)
				{
					bool flag4 = cmd is UndoableCommand;
					if (flag4)
					{
						this.history.Pop();
					}
				}
				bool flag5 = !this.UndoRedoStackIsEnabled;
				if (flag5)
				{
					this.ClearHistory();
				}
				this.redoStack.Clear();
				this.TextSource.CurrentTB.OnUndoRedoStateChanged();
			}
		}

		// Token: 0x06006640 RID: 26176 RVA: 0x001F0000 File Offset: 0x001F0000
		public void Undo()
		{
			bool flag = this.history.Count > 0;
			if (flag)
			{
				UndoableCommand undoableCommand = this.history.Pop();
				this.BeginDisableCommands();
				try
				{
					undoableCommand.Undo();
				}
				finally
				{
					this.EndDisableCommands();
				}
				this.redoStack.Push(undoableCommand);
			}
			bool flag2 = this.history.Count > 0;
			if (flag2)
			{
				bool autoUndo = this.history.Peek().autoUndo;
				if (autoUndo)
				{
					this.Undo();
				}
			}
			this.TextSource.CurrentTB.OnUndoRedoStateChanged();
		}

		// Token: 0x06006641 RID: 26177 RVA: 0x001F00B4 File Offset: 0x001F00B4
		private void EndDisableCommands()
		{
			this.disabledCommands--;
		}

		// Token: 0x06006642 RID: 26178 RVA: 0x001F00C8 File Offset: 0x001F00C8
		private void BeginDisableCommands()
		{
			this.disabledCommands++;
		}

		// Token: 0x06006643 RID: 26179 RVA: 0x001F00DC File Offset: 0x001F00DC
		public void EndAutoUndoCommands()
		{
			this.autoUndoCommands--;
			bool flag = this.autoUndoCommands == 0;
			if (flag)
			{
				bool flag2 = this.history.Count > 0;
				if (flag2)
				{
					this.history.Peek().autoUndo = false;
				}
			}
		}

		// Token: 0x06006644 RID: 26180 RVA: 0x001F0134 File Offset: 0x001F0134
		public void BeginAutoUndoCommands()
		{
			this.autoUndoCommands++;
		}

		// Token: 0x06006645 RID: 26181 RVA: 0x001F0148 File Offset: 0x001F0148
		internal void ClearHistory()
		{
			this.history.Clear();
			this.redoStack.Clear();
			this.TextSource.CurrentTB.OnUndoRedoStateChanged();
		}

		// Token: 0x06006646 RID: 26182 RVA: 0x001F0184 File Offset: 0x001F0184
		internal void Redo()
		{
			bool flag = this.redoStack.Count == 0;
			if (!flag)
			{
				this.BeginDisableCommands();
				UndoableCommand undoableCommand;
				try
				{
					undoableCommand = this.redoStack.Pop();
					bool columnSelectionMode = this.TextSource.CurrentTB.Selection.ColumnSelectionMode;
					if (columnSelectionMode)
					{
						this.TextSource.CurrentTB.Selection.ColumnSelectionMode = false;
					}
					this.TextSource.CurrentTB.Selection.Start = undoableCommand.sel.Start;
					this.TextSource.CurrentTB.Selection.End = undoableCommand.sel.End;
					undoableCommand.Execute();
					this.history.Push(undoableCommand);
				}
				finally
				{
					this.EndDisableCommands();
				}
				this.RedoCompleted(this, EventArgs.Empty);
				bool autoUndo = undoableCommand.autoUndo;
				if (autoUndo)
				{
					this.Redo();
				}
				this.TextSource.CurrentTB.OnUndoRedoStateChanged();
			}
		}

		// Token: 0x170015A1 RID: 5537
		// (get) Token: 0x06006647 RID: 26183 RVA: 0x001F02A0 File Offset: 0x001F02A0
		public bool UndoEnabled
		{
			get
			{
				return this.history.Count > 0;
			}
		}

		// Token: 0x170015A2 RID: 5538
		// (get) Token: 0x06006648 RID: 26184 RVA: 0x001F02C8 File Offset: 0x001F02C8
		public bool RedoEnabled
		{
			get
			{
				return this.redoStack.Count > 0;
			}
		}

		// Token: 0x04003464 RID: 13412
		private readonly int maxHistoryLength = 200;

		// Token: 0x04003465 RID: 13413
		private LimitedStack<UndoableCommand> history;

		// Token: 0x04003466 RID: 13414
		private Stack<UndoableCommand> redoStack = new Stack<UndoableCommand>();

		// Token: 0x0400346A RID: 13418
		protected int disabledCommands = 0;

		// Token: 0x0400346B RID: 13419
		private int autoUndoCommands = 0;
	}
}
