using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x02000789 RID: 1929
	[ComVisible(true)]
	public sealed class GenericInstMethodSig : CallingConventionSig
	{
		// Token: 0x17000C42 RID: 3138
		// (get) Token: 0x060044FA RID: 17658 RVA: 0x0016C7D8 File Offset: 0x0016C7D8
		public IList<TypeSig> GenericArguments
		{
			get
			{
				return this.genericArgs;
			}
		}

		// Token: 0x060044FB RID: 17659 RVA: 0x0016C7E0 File Offset: 0x0016C7E0
		public GenericInstMethodSig()
		{
			this.callingConvention = CallingConvention.GenericInst;
			this.genericArgs = new List<TypeSig>();
		}

		// Token: 0x060044FC RID: 17660 RVA: 0x0016C7FC File Offset: 0x0016C7FC
		internal GenericInstMethodSig(CallingConvention callingConvention, uint size)
		{
			this.callingConvention = callingConvention;
			this.genericArgs = new List<TypeSig>((int)size);
		}

		// Token: 0x060044FD RID: 17661 RVA: 0x0016C818 File Offset: 0x0016C818
		public GenericInstMethodSig(TypeSig arg1)
		{
			this.callingConvention = CallingConvention.GenericInst;
			this.genericArgs = new List<TypeSig>
			{
				arg1
			};
		}

		// Token: 0x060044FE RID: 17662 RVA: 0x0016C83C File Offset: 0x0016C83C
		public GenericInstMethodSig(TypeSig arg1, TypeSig arg2)
		{
			this.callingConvention = CallingConvention.GenericInst;
			this.genericArgs = new List<TypeSig>
			{
				arg1,
				arg2
			};
		}

		// Token: 0x060044FF RID: 17663 RVA: 0x0016C874 File Offset: 0x0016C874
		public GenericInstMethodSig(TypeSig arg1, TypeSig arg2, TypeSig arg3)
		{
			this.callingConvention = CallingConvention.GenericInst;
			this.genericArgs = new List<TypeSig>
			{
				arg1,
				arg2,
				arg3
			};
		}

		// Token: 0x06004500 RID: 17664 RVA: 0x0016C8B4 File Offset: 0x0016C8B4
		public GenericInstMethodSig(params TypeSig[] args)
		{
			this.callingConvention = CallingConvention.GenericInst;
			this.genericArgs = new List<TypeSig>(args);
		}

		// Token: 0x06004501 RID: 17665 RVA: 0x0016C8D0 File Offset: 0x0016C8D0
		public GenericInstMethodSig(IList<TypeSig> args)
		{
			this.callingConvention = CallingConvention.GenericInst;
			this.genericArgs = new List<TypeSig>(args);
		}

		// Token: 0x06004502 RID: 17666 RVA: 0x0016C8EC File Offset: 0x0016C8EC
		public GenericInstMethodSig Clone()
		{
			return new GenericInstMethodSig(this.genericArgs);
		}

		// Token: 0x0400241F RID: 9247
		private readonly IList<TypeSig> genericArgs;
	}
}
