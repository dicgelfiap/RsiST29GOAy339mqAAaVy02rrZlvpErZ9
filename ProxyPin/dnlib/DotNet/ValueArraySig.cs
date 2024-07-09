using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x0200087D RID: 2173
	[ComVisible(true)]
	public sealed class ValueArraySig : NonLeafSig
	{
		// Token: 0x17001111 RID: 4369
		// (get) Token: 0x060052F5 RID: 21237 RVA: 0x00196620 File Offset: 0x00196620
		public override ElementType ElementType
		{
			get
			{
				return ElementType.ValueArray;
			}
		}

		// Token: 0x17001112 RID: 4370
		// (get) Token: 0x060052F6 RID: 21238 RVA: 0x00196624 File Offset: 0x00196624
		// (set) Token: 0x060052F7 RID: 21239 RVA: 0x0019662C File Offset: 0x0019662C
		public uint Size
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

		// Token: 0x060052F8 RID: 21240 RVA: 0x00196638 File Offset: 0x00196638
		public ValueArraySig(TypeSig nextSig, uint size) : base(nextSig)
		{
			this.size = size;
		}

		// Token: 0x040027D8 RID: 10200
		private uint size;
	}
}
