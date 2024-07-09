using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x02000802 RID: 2050
	[ComVisible(true)]
	public sealed class FixedArrayMarshalType : MarshalType
	{
		// Token: 0x17000E2C RID: 3628
		// (get) Token: 0x060049B9 RID: 18873 RVA: 0x0017A3E8 File Offset: 0x0017A3E8
		// (set) Token: 0x060049BA RID: 18874 RVA: 0x0017A3F0 File Offset: 0x0017A3F0
		public NativeType ElementType
		{
			get
			{
				return this.elementType;
			}
			set
			{
				this.elementType = value;
			}
		}

		// Token: 0x17000E2D RID: 3629
		// (get) Token: 0x060049BB RID: 18875 RVA: 0x0017A3FC File Offset: 0x0017A3FC
		// (set) Token: 0x060049BC RID: 18876 RVA: 0x0017A404 File Offset: 0x0017A404
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

		// Token: 0x17000E2E RID: 3630
		// (get) Token: 0x060049BD RID: 18877 RVA: 0x0017A410 File Offset: 0x0017A410
		public bool IsElementTypeValid
		{
			get
			{
				return this.elementType != (NativeType)4294967294U;
			}
		}

		// Token: 0x17000E2F RID: 3631
		// (get) Token: 0x060049BE RID: 18878 RVA: 0x0017A420 File Offset: 0x0017A420
		public bool IsSizeValid
		{
			get
			{
				return this.size >= 0;
			}
		}

		// Token: 0x060049BF RID: 18879 RVA: 0x0017A430 File Offset: 0x0017A430
		public FixedArrayMarshalType() : this(0)
		{
		}

		// Token: 0x060049C0 RID: 18880 RVA: 0x0017A43C File Offset: 0x0017A43C
		public FixedArrayMarshalType(int size) : this(size, (NativeType)4294967294U)
		{
		}

		// Token: 0x060049C1 RID: 18881 RVA: 0x0017A448 File Offset: 0x0017A448
		public FixedArrayMarshalType(int size, NativeType elementType) : base(NativeType.FixedArray)
		{
			this.size = size;
			this.elementType = elementType;
		}

		// Token: 0x060049C2 RID: 18882 RVA: 0x0017A460 File Offset: 0x0017A460
		public override string ToString()
		{
			return string.Format("{0} ({1}, {2})", this.nativeType, this.size, this.elementType);
		}

		// Token: 0x04002547 RID: 9543
		private int size;

		// Token: 0x04002548 RID: 9544
		private NativeType elementType;
	}
}
