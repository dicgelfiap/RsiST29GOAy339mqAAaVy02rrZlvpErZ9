using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x02000875 RID: 2165
	[ComVisible(true)]
	public sealed class ByRefSig : NonLeafSig
	{
		// Token: 0x17001103 RID: 4355
		// (get) Token: 0x060052D1 RID: 21201 RVA: 0x00196490 File Offset: 0x00196490
		public override ElementType ElementType
		{
			get
			{
				return ElementType.ByRef;
			}
		}

		// Token: 0x060052D2 RID: 21202 RVA: 0x00196494 File Offset: 0x00196494
		public ByRefSig(TypeSig nextSig) : base(nextSig)
		{
		}
	}
}
