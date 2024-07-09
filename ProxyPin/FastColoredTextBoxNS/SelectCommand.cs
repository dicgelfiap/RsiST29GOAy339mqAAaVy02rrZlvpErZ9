using System;

namespace FastColoredTextBoxNS
{
	// Token: 0x02000A37 RID: 2615
	public class SelectCommand : UndoableCommand
	{
		// Token: 0x0600667B RID: 26235 RVA: 0x001F23F8 File Offset: 0x001F23F8
		public SelectCommand(TextSource ts) : base(ts)
		{
		}

		// Token: 0x0600667C RID: 26236 RVA: 0x001F2404 File Offset: 0x001F2404
		public override void Execute()
		{
			this.lastSel = new RangeInfo(this.ts.CurrentTB.Selection);
		}

		// Token: 0x0600667D RID: 26237 RVA: 0x001F2424 File Offset: 0x001F2424
		protected override void OnTextChanged(bool invert)
		{
		}

		// Token: 0x0600667E RID: 26238 RVA: 0x001F2428 File Offset: 0x001F2428
		public override void Undo()
		{
			this.ts.CurrentTB.Selection = new Range(this.ts.CurrentTB, this.lastSel.Start, this.lastSel.End);
		}

		// Token: 0x0600667F RID: 26239 RVA: 0x001F2474 File Offset: 0x001F2474
		public override UndoableCommand Clone()
		{
			SelectCommand selectCommand = new SelectCommand(this.ts);
			bool flag = this.lastSel != null;
			if (flag)
			{
				selectCommand.lastSel = new RangeInfo(new Range(this.ts.CurrentTB, this.lastSel.Start, this.lastSel.End));
			}
			return selectCommand;
		}
	}
}
