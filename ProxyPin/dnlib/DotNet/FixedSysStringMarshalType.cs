using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x02000800 RID: 2048
	[ComVisible(true)]
	public sealed class FixedSysStringMarshalType : MarshalType
	{
		// Token: 0x17000E26 RID: 3622
		// (get) Token: 0x060049A8 RID: 18856 RVA: 0x0017A26C File Offset: 0x0017A26C
		// (set) Token: 0x060049A9 RID: 18857 RVA: 0x0017A274 File Offset: 0x0017A274
		public int Size
		{
			get
			{
				return this.size;
			}
			set
			{
				this.size = value;
			}
		}

		// Token: 0x17000E27 RID: 3623
		// (get) Token: 0x060049AA RID: 18858 RVA: 0x0017A280 File Offset: 0x0017A280
		public bool IsSizeValid
		{
			get
			{
				return this.size >= 0;
			}
		}

		// Token: 0x060049AB RID: 18859 RVA: 0x0017A290 File Offset: 0x0017A290
		public FixedSysStringMarshalType() : this(-1)
		{
		}

		// Token: 0x060049AC RID: 18860 RVA: 0x0017A29C File Offset: 0x0017A29C
		public FixedSysStringMarshalType(int size) : base(NativeType.FixedSysString)
		{
			this.size = size;
		}

		// Token: 0x060049AD RID: 18861 RVA: 0x0017A2B0 File Offset: 0x0017A2B0
		public override string ToString()
		{
			if (this.IsSizeValid)
			{
				return string.Format("{0} ({1})", this.nativeType, this.size);
			}
			return string.Format("{0} (<no size>)", this.nativeType);
		}

		// Token: 0x04002544 RID: 9540
		private int size;
	}
}
