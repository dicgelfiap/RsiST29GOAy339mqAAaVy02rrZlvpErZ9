using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x02000788 RID: 1928
	[ComVisible(true)]
	public sealed class LocalSig : CallingConventionSig
	{
		// Token: 0x17000C41 RID: 3137
		// (get) Token: 0x060044F0 RID: 17648 RVA: 0x0016C69C File Offset: 0x0016C69C
		public IList<TypeSig> Locals
		{
			get
			{
				return this.locals;
			}
		}

		// Token: 0x060044F1 RID: 17649 RVA: 0x0016C6A4 File Offset: 0x0016C6A4
		public LocalSig()
		{
			this.callingConvention = CallingConvention.LocalSig;
			this.locals = new List<TypeSig>();
		}

		// Token: 0x060044F2 RID: 17650 RVA: 0x0016C6C0 File Offset: 0x0016C6C0
		internal LocalSig(CallingConvention callingConvention, uint count)
		{
			this.callingConvention = callingConvention;
			this.locals = new List<TypeSig>((int)count);
		}

		// Token: 0x060044F3 RID: 17651 RVA: 0x0016C6DC File Offset: 0x0016C6DC
		public LocalSig(TypeSig local1)
		{
			this.callingConvention = CallingConvention.LocalSig;
			this.locals = new List<TypeSig>
			{
				local1
			};
		}

		// Token: 0x060044F4 RID: 17652 RVA: 0x0016C700 File Offset: 0x0016C700
		public LocalSig(TypeSig local1, TypeSig local2)
		{
			this.callingConvention = CallingConvention.LocalSig;
			this.locals = new List<TypeSig>
			{
				local1,
				local2
			};
		}

		// Token: 0x060044F5 RID: 17653 RVA: 0x0016C738 File Offset: 0x0016C738
		public LocalSig(TypeSig local1, TypeSig local2, TypeSig local3)
		{
			this.callingConvention = CallingConvention.LocalSig;
			this.locals = new List<TypeSig>
			{
				local1,
				local2,
				local3
			};
		}

		// Token: 0x060044F6 RID: 17654 RVA: 0x0016C778 File Offset: 0x0016C778
		public LocalSig(params TypeSig[] locals)
		{
			this.callingConvention = CallingConvention.LocalSig;
			this.locals = new List<TypeSig>(locals);
		}

		// Token: 0x060044F7 RID: 17655 RVA: 0x0016C794 File Offset: 0x0016C794
		public LocalSig(IList<TypeSig> locals)
		{
			this.callingConvention = CallingConvention.LocalSig;
			this.locals = new List<TypeSig>(locals);
		}

		// Token: 0x060044F8 RID: 17656 RVA: 0x0016C7B0 File Offset: 0x0016C7B0
		internal LocalSig(IList<TypeSig> locals, bool dummy)
		{
			this.callingConvention = CallingConvention.LocalSig;
			this.locals = locals;
		}

		// Token: 0x060044F9 RID: 17657 RVA: 0x0016C7C8 File Offset: 0x0016C7C8
		public LocalSig Clone()
		{
			return new LocalSig(this.locals);
		}

		// Token: 0x0400241E RID: 9246
		private readonly IList<TypeSig> locals;
	}
}
