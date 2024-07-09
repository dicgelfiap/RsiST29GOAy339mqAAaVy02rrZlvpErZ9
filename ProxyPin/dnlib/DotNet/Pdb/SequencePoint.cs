using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.Pdb
{
	// Token: 0x02000925 RID: 2341
	[DebuggerDisplay("({StartLine}, {StartColumn}) - ({EndLine}, {EndColumn}) {Document.Url}")]
	[ComVisible(true)]
	public sealed class SequencePoint
	{
		// Token: 0x17001301 RID: 4865
		// (get) Token: 0x06005A4E RID: 23118 RVA: 0x001B75A0 File Offset: 0x001B75A0
		// (set) Token: 0x06005A4F RID: 23119 RVA: 0x001B75A8 File Offset: 0x001B75A8
		public PdbDocument Document { get; set; }

		// Token: 0x17001302 RID: 4866
		// (get) Token: 0x06005A50 RID: 23120 RVA: 0x001B75B4 File Offset: 0x001B75B4
		// (set) Token: 0x06005A51 RID: 23121 RVA: 0x001B75BC File Offset: 0x001B75BC
		public int StartLine { get; set; }

		// Token: 0x17001303 RID: 4867
		// (get) Token: 0x06005A52 RID: 23122 RVA: 0x001B75C8 File Offset: 0x001B75C8
		// (set) Token: 0x06005A53 RID: 23123 RVA: 0x001B75D0 File Offset: 0x001B75D0
		public int StartColumn { get; set; }

		// Token: 0x17001304 RID: 4868
		// (get) Token: 0x06005A54 RID: 23124 RVA: 0x001B75DC File Offset: 0x001B75DC
		// (set) Token: 0x06005A55 RID: 23125 RVA: 0x001B75E4 File Offset: 0x001B75E4
		public int EndLine { get; set; }

		// Token: 0x17001305 RID: 4869
		// (get) Token: 0x06005A56 RID: 23126 RVA: 0x001B75F0 File Offset: 0x001B75F0
		// (set) Token: 0x06005A57 RID: 23127 RVA: 0x001B75F8 File Offset: 0x001B75F8
		public int EndColumn { get; set; }

		// Token: 0x06005A58 RID: 23128 RVA: 0x001B7604 File Offset: 0x001B7604
		public SequencePoint Clone()
		{
			return new SequencePoint
			{
				Document = this.Document,
				StartLine = this.StartLine,
				StartColumn = this.StartColumn,
				EndLine = this.EndLine,
				EndColumn = this.EndColumn
			};
		}
	}
}
