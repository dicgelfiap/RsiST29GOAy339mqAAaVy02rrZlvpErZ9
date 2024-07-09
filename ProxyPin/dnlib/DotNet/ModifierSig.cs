using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x02000879 RID: 2169
	[ComVisible(true)]
	public abstract class ModifierSig : NonLeafSig
	{
		// Token: 0x1700110D RID: 4365
		// (get) Token: 0x060052ED RID: 21229 RVA: 0x001965D8 File Offset: 0x001965D8
		public ITypeDefOrRef Modifier
		{
			get
			{
				return this.modifier;
			}
		}

		// Token: 0x060052EE RID: 21230 RVA: 0x001965E0 File Offset: 0x001965E0
		protected ModifierSig(ITypeDefOrRef modifier, TypeSig nextSig) : base(nextSig)
		{
			this.modifier = modifier;
		}

		// Token: 0x040027D7 RID: 10199
		private readonly ITypeDefOrRef modifier;
	}
}
