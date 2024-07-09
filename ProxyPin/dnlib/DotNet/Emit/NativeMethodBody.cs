using System;
using System.Runtime.InteropServices;
using dnlib.PE;

namespace dnlib.DotNet.Emit
{
	// Token: 0x020009E5 RID: 2533
	[ComVisible(true)]
	public sealed class NativeMethodBody : MethodBody
	{
		// Token: 0x1700146A RID: 5226
		// (get) Token: 0x0600611E RID: 24862 RVA: 0x001CEC80 File Offset: 0x001CEC80
		// (set) Token: 0x0600611F RID: 24863 RVA: 0x001CEC88 File Offset: 0x001CEC88
		public RVA RVA
		{
			get
			{
				return this.rva;
			}
			set
			{
				this.rva = value;
			}
		}

		// Token: 0x06006120 RID: 24864 RVA: 0x001CEC94 File Offset: 0x001CEC94
		public NativeMethodBody()
		{
		}

		// Token: 0x06006121 RID: 24865 RVA: 0x001CEC9C File Offset: 0x001CEC9C
		public NativeMethodBody(RVA rva)
		{
			this.rva = rva;
		}

		// Token: 0x040030A5 RID: 12453
		private RVA rva;
	}
}
