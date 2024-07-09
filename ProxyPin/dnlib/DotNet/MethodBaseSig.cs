using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x02000785 RID: 1925
	[ComVisible(true)]
	public abstract class MethodBaseSig : CallingConventionSig
	{
		// Token: 0x17000C3B RID: 3131
		// (get) Token: 0x060044AF RID: 17583 RVA: 0x0016C018 File Offset: 0x0016C018
		// (set) Token: 0x060044B0 RID: 17584 RVA: 0x0016C020 File Offset: 0x0016C020
		public CallingConvention CallingConvention
		{
			get
			{
				return this.callingConvention;
			}
			set
			{
				this.callingConvention = value;
			}
		}

		// Token: 0x17000C3C RID: 3132
		// (get) Token: 0x060044B1 RID: 17585 RVA: 0x0016C02C File Offset: 0x0016C02C
		// (set) Token: 0x060044B2 RID: 17586 RVA: 0x0016C034 File Offset: 0x0016C034
		public TypeSig RetType
		{
			get
			{
				return this.retType;
			}
			set
			{
				this.retType = value;
			}
		}

		// Token: 0x17000C3D RID: 3133
		// (get) Token: 0x060044B3 RID: 17587 RVA: 0x0016C040 File Offset: 0x0016C040
		public IList<TypeSig> Params
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x17000C3E RID: 3134
		// (get) Token: 0x060044B4 RID: 17588 RVA: 0x0016C048 File Offset: 0x0016C048
		// (set) Token: 0x060044B5 RID: 17589 RVA: 0x0016C050 File Offset: 0x0016C050
		public uint GenParamCount
		{
			get
			{
				return this.genParamCount;
			}
			set
			{
				this.genParamCount = value;
			}
		}

		// Token: 0x17000C3F RID: 3135
		// (get) Token: 0x060044B6 RID: 17590 RVA: 0x0016C05C File Offset: 0x0016C05C
		// (set) Token: 0x060044B7 RID: 17591 RVA: 0x0016C064 File Offset: 0x0016C064
		public IList<TypeSig> ParamsAfterSentinel
		{
			get
			{
				return this.paramsAfterSentinel;
			}
			set
			{
				this.paramsAfterSentinel = value;
			}
		}

		// Token: 0x04002419 RID: 9241
		protected TypeSig retType;

		// Token: 0x0400241A RID: 9242
		protected IList<TypeSig> parameters;

		// Token: 0x0400241B RID: 9243
		protected uint genParamCount;

		// Token: 0x0400241C RID: 9244
		protected IList<TypeSig> paramsAfterSentinel;
	}
}
