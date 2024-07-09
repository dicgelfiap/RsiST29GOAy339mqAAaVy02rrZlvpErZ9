using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x0200087A RID: 2170
	[ComVisible(true)]
	public sealed class CModReqdSig : ModifierSig
	{
		// Token: 0x1700110E RID: 4366
		// (get) Token: 0x060052EF RID: 21231 RVA: 0x001965F0 File Offset: 0x001965F0
		public override ElementType ElementType
		{
			get
			{
				return ElementType.CModReqd;
			}
		}

		// Token: 0x060052F0 RID: 21232 RVA: 0x001965F4 File Offset: 0x001965F4
		public CModReqdSig(ITypeDefOrRef modifier, TypeSig nextSig) : base(modifier, nextSig)
		{
		}
	}
}
