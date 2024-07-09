using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x020007FE RID: 2046
	[ComVisible(true)]
	public class MarshalType
	{
		// Token: 0x17000E24 RID: 3620
		// (get) Token: 0x060049A2 RID: 18850 RVA: 0x0017A21C File Offset: 0x0017A21C
		public NativeType NativeType
		{
			get
			{
				return this.nativeType;
			}
		}

		// Token: 0x060049A3 RID: 18851 RVA: 0x0017A224 File Offset: 0x0017A224
		public MarshalType(NativeType nativeType)
		{
			this.nativeType = nativeType;
		}

		// Token: 0x060049A4 RID: 18852 RVA: 0x0017A234 File Offset: 0x0017A234
		public override string ToString()
		{
			return this.nativeType.ToString();
		}

		// Token: 0x04002542 RID: 9538
		protected readonly NativeType nativeType;
	}
}
