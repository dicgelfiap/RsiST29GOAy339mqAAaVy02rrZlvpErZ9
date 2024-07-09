using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x02000877 RID: 2167
	[ComVisible(true)]
	public sealed class ArraySig : ArraySigBase
	{
		// Token: 0x17001107 RID: 4359
		// (get) Token: 0x060052DA RID: 21210 RVA: 0x001964C4 File Offset: 0x001964C4
		public override ElementType ElementType
		{
			get
			{
				return ElementType.Array;
			}
		}

		// Token: 0x17001108 RID: 4360
		// (get) Token: 0x060052DB RID: 21211 RVA: 0x001964C8 File Offset: 0x001964C8
		// (set) Token: 0x060052DC RID: 21212 RVA: 0x001964D0 File Offset: 0x001964D0
		public override uint Rank
		{
			get
			{
				return this.rank;
			}
			set
			{
				this.rank = value;
			}
		}

		// Token: 0x17001109 RID: 4361
		// (get) Token: 0x060052DD RID: 21213 RVA: 0x001964DC File Offset: 0x001964DC
		public IList<uint> Sizes
		{
			get
			{
				return this.sizes;
			}
		}

		// Token: 0x1700110A RID: 4362
		// (get) Token: 0x060052DE RID: 21214 RVA: 0x001964E4 File Offset: 0x001964E4
		public IList<int> LowerBounds
		{
			get
			{
				return this.lowerBounds;
			}
		}

		// Token: 0x060052DF RID: 21215 RVA: 0x001964EC File Offset: 0x001964EC
		public ArraySig(TypeSig arrayType) : base(arrayType)
		{
			this.sizes = new List<uint>();
			this.lowerBounds = new List<int>();
		}

		// Token: 0x060052E0 RID: 21216 RVA: 0x0019650C File Offset: 0x0019650C
		public ArraySig(TypeSig arrayType, uint rank) : base(arrayType)
		{
			this.rank = rank;
			this.sizes = new List<uint>();
			this.lowerBounds = new List<int>();
		}

		// Token: 0x060052E1 RID: 21217 RVA: 0x00196534 File Offset: 0x00196534
		public ArraySig(TypeSig arrayType, int rank) : this(arrayType, (uint)rank)
		{
		}

		// Token: 0x060052E2 RID: 21218 RVA: 0x00196540 File Offset: 0x00196540
		public ArraySig(TypeSig arrayType, uint rank, IEnumerable<uint> sizes, IEnumerable<int> lowerBounds) : base(arrayType)
		{
			this.rank = rank;
			this.sizes = new List<uint>(sizes);
			this.lowerBounds = new List<int>(lowerBounds);
		}

		// Token: 0x060052E3 RID: 21219 RVA: 0x0019656C File Offset: 0x0019656C
		public ArraySig(TypeSig arrayType, int rank, IEnumerable<uint> sizes, IEnumerable<int> lowerBounds) : this(arrayType, (uint)rank, sizes, lowerBounds)
		{
		}

		// Token: 0x060052E4 RID: 21220 RVA: 0x0019657C File Offset: 0x0019657C
		internal ArraySig(TypeSig arrayType, uint rank, IList<uint> sizes, IList<int> lowerBounds) : base(arrayType)
		{
			this.rank = rank;
			this.sizes = sizes;
			this.lowerBounds = lowerBounds;
		}

		// Token: 0x060052E5 RID: 21221 RVA: 0x0019659C File Offset: 0x0019659C
		public override IList<uint> GetSizes()
		{
			return this.sizes;
		}

		// Token: 0x060052E6 RID: 21222 RVA: 0x001965A4 File Offset: 0x001965A4
		public override IList<int> GetLowerBounds()
		{
			return this.lowerBounds;
		}

		// Token: 0x040027D4 RID: 10196
		private uint rank;

		// Token: 0x040027D5 RID: 10197
		private readonly IList<uint> sizes;

		// Token: 0x040027D6 RID: 10198
		private readonly IList<int> lowerBounds;
	}
}
