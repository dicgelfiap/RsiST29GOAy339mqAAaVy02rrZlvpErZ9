using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x02000803 RID: 2051
	[ComVisible(true)]
	public sealed class ArrayMarshalType : MarshalType
	{
		// Token: 0x17000E30 RID: 3632
		// (get) Token: 0x060049C3 RID: 18883 RVA: 0x0017A490 File Offset: 0x0017A490
		// (set) Token: 0x060049C4 RID: 18884 RVA: 0x0017A498 File Offset: 0x0017A498
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

		// Token: 0x17000E31 RID: 3633
		// (get) Token: 0x060049C5 RID: 18885 RVA: 0x0017A4A4 File Offset: 0x0017A4A4
		// (set) Token: 0x060049C6 RID: 18886 RVA: 0x0017A4AC File Offset: 0x0017A4AC
		public int ParamNumber
		{
			get
			{
				return this.paramNum;
			}
			set
			{
				this.paramNum = value;
			}
		}

		// Token: 0x17000E32 RID: 3634
		// (get) Token: 0x060049C7 RID: 18887 RVA: 0x0017A4B8 File Offset: 0x0017A4B8
		// (set) Token: 0x060049C8 RID: 18888 RVA: 0x0017A4C0 File Offset: 0x0017A4C0
		public int Size
		{
			get
			{
				return this.numElems;
			}
			set
			{
				this.numElems = value;
			}
		}

		// Token: 0x17000E33 RID: 3635
		// (get) Token: 0x060049C9 RID: 18889 RVA: 0x0017A4CC File Offset: 0x0017A4CC
		// (set) Token: 0x060049CA RID: 18890 RVA: 0x0017A4D4 File Offset: 0x0017A4D4
		public int Flags
		{
			get
			{
				return this.flags;
			}
			set
			{
				this.flags = value;
			}
		}

		// Token: 0x17000E34 RID: 3636
		// (get) Token: 0x060049CB RID: 18891 RVA: 0x0017A4E0 File Offset: 0x0017A4E0
		public bool IsElementTypeValid
		{
			get
			{
				return this.elementType != (NativeType)4294967294U;
			}
		}

		// Token: 0x17000E35 RID: 3637
		// (get) Token: 0x060049CC RID: 18892 RVA: 0x0017A4F0 File Offset: 0x0017A4F0
		public bool IsParamNumberValid
		{
			get
			{
				return this.paramNum >= 0;
			}
		}

		// Token: 0x17000E36 RID: 3638
		// (get) Token: 0x060049CD RID: 18893 RVA: 0x0017A500 File Offset: 0x0017A500
		public bool IsSizeValid
		{
			get
			{
				return this.numElems >= 0;
			}
		}

		// Token: 0x17000E37 RID: 3639
		// (get) Token: 0x060049CE RID: 18894 RVA: 0x0017A510 File Offset: 0x0017A510
		public bool IsFlagsValid
		{
			get
			{
				return this.flags >= 0;
			}
		}

		// Token: 0x17000E38 RID: 3640
		// (get) Token: 0x060049CF RID: 18895 RVA: 0x0017A520 File Offset: 0x0017A520
		public bool IsSizeParamIndexSpecified
		{
			get
			{
				return this.IsFlagsValid && (this.flags & 1) != 0;
			}
		}

		// Token: 0x17000E39 RID: 3641
		// (get) Token: 0x060049D0 RID: 18896 RVA: 0x0017A53C File Offset: 0x0017A53C
		public bool IsSizeParamIndexNotSpecified
		{
			get
			{
				return this.IsFlagsValid && (this.flags & 1) == 0;
			}
		}

		// Token: 0x060049D1 RID: 18897 RVA: 0x0017A558 File Offset: 0x0017A558
		public ArrayMarshalType() : this((NativeType)4294967294U, -1, -1, -1)
		{
		}

		// Token: 0x060049D2 RID: 18898 RVA: 0x0017A568 File Offset: 0x0017A568
		public ArrayMarshalType(NativeType elementType) : this(elementType, -1, -1, -1)
		{
		}

		// Token: 0x060049D3 RID: 18899 RVA: 0x0017A574 File Offset: 0x0017A574
		public ArrayMarshalType(NativeType elementType, int paramNum) : this(elementType, paramNum, -1, -1)
		{
		}

		// Token: 0x060049D4 RID: 18900 RVA: 0x0017A580 File Offset: 0x0017A580
		public ArrayMarshalType(NativeType elementType, int paramNum, int numElems) : this(elementType, paramNum, numElems, -1)
		{
		}

		// Token: 0x060049D5 RID: 18901 RVA: 0x0017A58C File Offset: 0x0017A58C
		public ArrayMarshalType(NativeType elementType, int paramNum, int numElems, int flags) : base(NativeType.Array)
		{
			this.elementType = elementType;
			this.paramNum = paramNum;
			this.numElems = numElems;
			this.flags = flags;
		}

		// Token: 0x060049D6 RID: 18902 RVA: 0x0017A5B4 File Offset: 0x0017A5B4
		public override string ToString()
		{
			return string.Format("{0} ({1}, {2}, {3}, {4})", new object[]
			{
				this.nativeType,
				this.elementType,
				this.paramNum,
				this.numElems,
				this.flags
			});
		}

		// Token: 0x04002549 RID: 9545
		private NativeType elementType;

		// Token: 0x0400254A RID: 9546
		private int paramNum;

		// Token: 0x0400254B RID: 9547
		private int numElems;

		// Token: 0x0400254C RID: 9548
		private int flags;

		// Token: 0x0400254D RID: 9549
		private const int ntaSizeParamIndexSpecified = 1;
	}
}
