using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x0200087E RID: 2174
	[ComVisible(true)]
	public sealed class ModuleSig : NonLeafSig
	{
		// Token: 0x17001113 RID: 4371
		// (get) Token: 0x060052F9 RID: 21241 RVA: 0x00196648 File Offset: 0x00196648
		public override ElementType ElementType
		{
			get
			{
				return ElementType.Module;
			}
		}

		// Token: 0x17001114 RID: 4372
		// (get) Token: 0x060052FA RID: 21242 RVA: 0x0019664C File Offset: 0x0019664C
		// (set) Token: 0x060052FB RID: 21243 RVA: 0x00196654 File Offset: 0x00196654
		public uint Index
		{
			get
			{
				return this.index;
			}
			set
			{
				this.index = value;
			}
		}

		// Token: 0x060052FC RID: 21244 RVA: 0x00196660 File Offset: 0x00196660
		public ModuleSig(uint index, TypeSig nextSig) : base(nextSig)
		{
			this.index = index;
		}

		// Token: 0x040027D9 RID: 10201
		private uint index;
	}
}
