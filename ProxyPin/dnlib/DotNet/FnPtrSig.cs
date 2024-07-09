using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x02000871 RID: 2161
	[ComVisible(true)]
	public sealed class FnPtrSig : LeafSig
	{
		// Token: 0x170010FB RID: 4347
		// (get) Token: 0x060052BC RID: 21180 RVA: 0x001962F0 File Offset: 0x001962F0
		public override ElementType ElementType
		{
			get
			{
				return ElementType.FnPtr;
			}
		}

		// Token: 0x170010FC RID: 4348
		// (get) Token: 0x060052BD RID: 21181 RVA: 0x001962F4 File Offset: 0x001962F4
		public CallingConventionSig Signature
		{
			get
			{
				return this.signature;
			}
		}

		// Token: 0x170010FD RID: 4349
		// (get) Token: 0x060052BE RID: 21182 RVA: 0x001962FC File Offset: 0x001962FC
		public MethodSig MethodSig
		{
			get
			{
				return this.signature as MethodSig;
			}
		}

		// Token: 0x060052BF RID: 21183 RVA: 0x0019630C File Offset: 0x0019630C
		public FnPtrSig(CallingConventionSig signature)
		{
			this.signature = signature;
		}

		// Token: 0x040027D0 RID: 10192
		private readonly CallingConventionSig signature;
	}
}
