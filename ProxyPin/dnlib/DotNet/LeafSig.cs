using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x02000867 RID: 2151
	[ComVisible(true)]
	public abstract class LeafSig : TypeSig
	{
		// Token: 0x170010E4 RID: 4324
		// (get) Token: 0x06005294 RID: 21140 RVA: 0x0019609C File Offset: 0x0019609C
		public sealed override TypeSig Next
		{
			get
			{
				return null;
			}
		}
	}
}
