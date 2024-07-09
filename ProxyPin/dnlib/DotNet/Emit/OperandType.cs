using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.Emit
{
	// Token: 0x020009F0 RID: 2544
	[ComVisible(true)]
	public enum OperandType : byte
	{
		// Token: 0x040031C4 RID: 12740
		InlineBrTarget,
		// Token: 0x040031C5 RID: 12741
		InlineField,
		// Token: 0x040031C6 RID: 12742
		InlineI,
		// Token: 0x040031C7 RID: 12743
		InlineI8,
		// Token: 0x040031C8 RID: 12744
		InlineMethod,
		// Token: 0x040031C9 RID: 12745
		InlineNone,
		// Token: 0x040031CA RID: 12746
		InlinePhi,
		// Token: 0x040031CB RID: 12747
		InlineR,
		// Token: 0x040031CC RID: 12748
		NOT_USED_8,
		// Token: 0x040031CD RID: 12749
		InlineSig,
		// Token: 0x040031CE RID: 12750
		InlineString,
		// Token: 0x040031CF RID: 12751
		InlineSwitch,
		// Token: 0x040031D0 RID: 12752
		InlineTok,
		// Token: 0x040031D1 RID: 12753
		InlineType,
		// Token: 0x040031D2 RID: 12754
		InlineVar,
		// Token: 0x040031D3 RID: 12755
		ShortInlineBrTarget,
		// Token: 0x040031D4 RID: 12756
		ShortInlineI,
		// Token: 0x040031D5 RID: 12757
		ShortInlineR,
		// Token: 0x040031D6 RID: 12758
		ShortInlineVar
	}
}
