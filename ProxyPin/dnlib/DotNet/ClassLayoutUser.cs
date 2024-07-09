using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x0200078B RID: 1931
	[ComVisible(true)]
	public class ClassLayoutUser : ClassLayout
	{
		// Token: 0x0600450B RID: 17675 RVA: 0x0016C950 File Offset: 0x0016C950
		public ClassLayoutUser()
		{
		}

		// Token: 0x0600450C RID: 17676 RVA: 0x0016C958 File Offset: 0x0016C958
		public ClassLayoutUser(ushort packingSize, uint classSize)
		{
			this.packingSize = packingSize;
			this.classSize = classSize;
		}
	}
}
