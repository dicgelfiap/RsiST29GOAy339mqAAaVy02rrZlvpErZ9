using System;
using System.Runtime.InteropServices;

namespace PeNet.Asn1
{
	// Token: 0x02000B82 RID: 2946
	[ComVisible(true)]
	public enum Asn1TagClass : byte
	{
		// Token: 0x0400398C RID: 14732
		Universal,
		// Token: 0x0400398D RID: 14733
		Application,
		// Token: 0x0400398E RID: 14734
		ContextDefined,
		// Token: 0x0400398F RID: 14735
		Private
	}
}
