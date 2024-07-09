using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x02000870 RID: 2160
	[ComVisible(true)]
	public sealed class SentinelSig : LeafSig
	{
		// Token: 0x170010FA RID: 4346
		// (get) Token: 0x060052BA RID: 21178 RVA: 0x001962E4 File Offset: 0x001962E4
		public override ElementType ElementType
		{
			get
			{
				return ElementType.Sentinel;
			}
		}
	}
}
