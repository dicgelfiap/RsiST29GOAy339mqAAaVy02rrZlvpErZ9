using System;

namespace FastColoredTextBoxNS
{
	// Token: 0x02000A26 RID: 2598
	public class HintClickEventArgs : EventArgs
	{
		// Token: 0x06006625 RID: 26149 RVA: 0x001EFC9C File Offset: 0x001EFC9C
		public HintClickEventArgs(Hint hint)
		{
			this.Hint = hint;
		}

		// Token: 0x17001597 RID: 5527
		// (get) Token: 0x06006626 RID: 26150 RVA: 0x001EFCB0 File Offset: 0x001EFCB0
		// (set) Token: 0x06006627 RID: 26151 RVA: 0x001EFCB8 File Offset: 0x001EFCB8
		public Hint Hint { get; private set; }
	}
}
