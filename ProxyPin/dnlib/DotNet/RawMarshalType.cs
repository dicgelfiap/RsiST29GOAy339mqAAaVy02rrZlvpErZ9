using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x020007FF RID: 2047
	[ComVisible(true)]
	public sealed class RawMarshalType : MarshalType
	{
		// Token: 0x17000E25 RID: 3621
		// (get) Token: 0x060049A5 RID: 18853 RVA: 0x0017A248 File Offset: 0x0017A248
		// (set) Token: 0x060049A6 RID: 18854 RVA: 0x0017A250 File Offset: 0x0017A250
		public byte[] Data
		{
			get
			{
				return this.data;
			}
			set
			{
				this.data = value;
			}
		}

		// Token: 0x060049A7 RID: 18855 RVA: 0x0017A25C File Offset: 0x0017A25C
		public RawMarshalType(byte[] data) : base((NativeType)4294967295U)
		{
			this.data = data;
		}

		// Token: 0x04002543 RID: 9539
		private byte[] data;
	}
}
