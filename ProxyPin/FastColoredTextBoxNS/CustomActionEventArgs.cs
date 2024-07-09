using System;

namespace FastColoredTextBoxNS
{
	// Token: 0x02000A27 RID: 2599
	public class CustomActionEventArgs : EventArgs
	{
		// Token: 0x17001598 RID: 5528
		// (get) Token: 0x06006628 RID: 26152 RVA: 0x001EFCC4 File Offset: 0x001EFCC4
		// (set) Token: 0x06006629 RID: 26153 RVA: 0x001EFCCC File Offset: 0x001EFCCC
		public FCTBAction Action { get; private set; }

		// Token: 0x0600662A RID: 26154 RVA: 0x001EFCD8 File Offset: 0x001EFCD8
		public CustomActionEventArgs(FCTBAction action)
		{
			this.Action = action;
		}
	}
}
