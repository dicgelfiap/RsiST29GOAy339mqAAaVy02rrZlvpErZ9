using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x02000873 RID: 2163
	[ComVisible(true)]
	public abstract class NonLeafSig : TypeSig
	{
		// Token: 0x17001101 RID: 4353
		// (get) Token: 0x060052CD RID: 21197 RVA: 0x00196468 File Offset: 0x00196468
		public sealed override TypeSig Next
		{
			get
			{
				return this.nextSig;
			}
		}

		// Token: 0x060052CE RID: 21198 RVA: 0x00196470 File Offset: 0x00196470
		protected NonLeafSig(TypeSig nextSig)
		{
			this.nextSig = nextSig;
		}

		// Token: 0x040027D3 RID: 10195
		private readonly TypeSig nextSig;
	}
}
