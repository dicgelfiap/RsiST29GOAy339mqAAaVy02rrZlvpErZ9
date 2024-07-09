using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x0200087C RID: 2172
	[ComVisible(true)]
	public sealed class PinnedSig : NonLeafSig
	{
		// Token: 0x17001110 RID: 4368
		// (get) Token: 0x060052F3 RID: 21235 RVA: 0x00196610 File Offset: 0x00196610
		public override ElementType ElementType
		{
			get
			{
				return ElementType.Pinned;
			}
		}

		// Token: 0x060052F4 RID: 21236 RVA: 0x00196614 File Offset: 0x00196614
		public PinnedSig(TypeSig nextSig) : base(nextSig)
		{
		}
	}
}
