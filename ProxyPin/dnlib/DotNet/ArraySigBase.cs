using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x02000876 RID: 2166
	[ComVisible(true)]
	public abstract class ArraySigBase : NonLeafSig
	{
		// Token: 0x060052D3 RID: 21203 RVA: 0x001964A0 File Offset: 0x001964A0
		protected ArraySigBase(TypeSig arrayType) : base(arrayType)
		{
		}

		// Token: 0x17001104 RID: 4356
		// (get) Token: 0x060052D4 RID: 21204 RVA: 0x001964AC File Offset: 0x001964AC
		public bool IsMultiDimensional
		{
			get
			{
				return this.ElementType == ElementType.Array;
			}
		}

		// Token: 0x17001105 RID: 4357
		// (get) Token: 0x060052D5 RID: 21205 RVA: 0x001964B8 File Offset: 0x001964B8
		public bool IsSingleDimensional
		{
			get
			{
				return this.ElementType == ElementType.SZArray;
			}
		}

		// Token: 0x17001106 RID: 4358
		// (get) Token: 0x060052D6 RID: 21206
		// (set) Token: 0x060052D7 RID: 21207
		public abstract uint Rank { get; set; }

		// Token: 0x060052D8 RID: 21208
		public abstract IList<uint> GetSizes();

		// Token: 0x060052D9 RID: 21209
		public abstract IList<int> GetLowerBounds();
	}
}
