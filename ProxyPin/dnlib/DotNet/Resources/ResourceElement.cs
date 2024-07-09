using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.Resources
{
	// Token: 0x020008E6 RID: 2278
	[ComVisible(true)]
	public sealed class ResourceElement
	{
		// Token: 0x1700125D RID: 4701
		// (get) Token: 0x060058CF RID: 22735 RVA: 0x001B47C4 File Offset: 0x001B47C4
		// (set) Token: 0x060058D0 RID: 22736 RVA: 0x001B47CC File Offset: 0x001B47CC
		public string Name { get; set; }

		// Token: 0x1700125E RID: 4702
		// (get) Token: 0x060058D1 RID: 22737 RVA: 0x001B47D8 File Offset: 0x001B47D8
		// (set) Token: 0x060058D2 RID: 22738 RVA: 0x001B47E0 File Offset: 0x001B47E0
		public IResourceData ResourceData { get; set; }

		// Token: 0x060058D3 RID: 22739 RVA: 0x001B47EC File Offset: 0x001B47EC
		public override string ToString()
		{
			return string.Format("N: {0}, V: {1}", this.Name, this.ResourceData);
		}
	}
}
