using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x02000874 RID: 2164
	[ComVisible(true)]
	public sealed class PtrSig : NonLeafSig
	{
		// Token: 0x17001102 RID: 4354
		// (get) Token: 0x060052CF RID: 21199 RVA: 0x00196480 File Offset: 0x00196480
		public override ElementType ElementType
		{
			get
			{
				return ElementType.Ptr;
			}
		}

		// Token: 0x060052D0 RID: 21200 RVA: 0x00196484 File Offset: 0x00196484
		public PtrSig(TypeSig nextSig) : base(nextSig)
		{
		}
	}
}
