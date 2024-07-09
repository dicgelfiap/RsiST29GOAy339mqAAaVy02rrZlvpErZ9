using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x0200087B RID: 2171
	[ComVisible(true)]
	public sealed class CModOptSig : ModifierSig
	{
		// Token: 0x1700110F RID: 4367
		// (get) Token: 0x060052F1 RID: 21233 RVA: 0x00196600 File Offset: 0x00196600
		public override ElementType ElementType
		{
			get
			{
				return ElementType.CModOpt;
			}
		}

		// Token: 0x060052F2 RID: 21234 RVA: 0x00196604 File Offset: 0x00196604
		public CModOptSig(ITypeDefOrRef modifier, TypeSig nextSig) : base(modifier, nextSig)
		{
		}
	}
}
