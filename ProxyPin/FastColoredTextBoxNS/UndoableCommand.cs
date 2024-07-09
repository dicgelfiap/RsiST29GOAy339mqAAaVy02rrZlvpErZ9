using System;

namespace FastColoredTextBoxNS
{
	// Token: 0x02000A2F RID: 2607
	public abstract class UndoableCommand : Command
	{
		// Token: 0x06006651 RID: 26193 RVA: 0x001F03EC File Offset: 0x001F03EC
		public UndoableCommand(TextSource ts)
		{
			this.ts = ts;
			this.sel = new RangeInfo(ts.CurrentTB.Selection);
		}

		// Token: 0x06006652 RID: 26194 RVA: 0x001F0414 File Offset: 0x001F0414
		public virtual void Undo()
		{
			this.OnTextChanged(true);
		}

		// Token: 0x06006653 RID: 26195 RVA: 0x001F0420 File Offset: 0x001F0420
		public override void Execute()
		{
			this.lastSel = new RangeInfo(this.ts.CurrentTB.Selection);
			this.OnTextChanged(false);
		}

		// Token: 0x06006654 RID: 26196 RVA: 0x001F0448 File Offset: 0x001F0448
		protected virtual void OnTextChanged(bool invert)
		{
			bool flag = this.sel.Start.iLine < this.lastSel.Start.iLine;
			if (invert)
			{
				bool flag2 = flag;
				if (flag2)
				{
					this.ts.OnTextChanged(this.sel.Start.iLine, this.sel.Start.iLine);
				}
				else
				{
					this.ts.OnTextChanged(this.sel.Start.iLine, this.lastSel.Start.iLine);
				}
			}
			else
			{
				bool flag3 = flag;
				if (flag3)
				{
					this.ts.OnTextChanged(this.sel.Start.iLine, this.lastSel.Start.iLine);
				}
				else
				{
					this.ts.OnTextChanged(this.lastSel.Start.iLine, this.lastSel.Start.iLine);
				}
			}
		}

		// Token: 0x06006655 RID: 26197
		public abstract UndoableCommand Clone();

		// Token: 0x0400346F RID: 13423
		internal RangeInfo sel;

		// Token: 0x04003470 RID: 13424
		internal RangeInfo lastSel;

		// Token: 0x04003471 RID: 13425
		internal bool autoUndo;
	}
}
