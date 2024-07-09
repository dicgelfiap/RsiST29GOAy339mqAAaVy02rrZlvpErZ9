using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x0200086E RID: 2158
	[ComVisible(true)]
	public sealed class GenericVar : GenericSig
	{
		// Token: 0x170010F8 RID: 4344
		// (get) Token: 0x060052B0 RID: 21168 RVA: 0x0019627C File Offset: 0x0019627C
		public override ElementType ElementType
		{
			get
			{
				return ElementType.Var;
			}
		}

		// Token: 0x060052B1 RID: 21169 RVA: 0x00196280 File Offset: 0x00196280
		public GenericVar(uint number) : base(true, number)
		{
		}

		// Token: 0x060052B2 RID: 21170 RVA: 0x0019628C File Offset: 0x0019628C
		public GenericVar(int number) : base(true, (uint)number)
		{
		}

		// Token: 0x060052B3 RID: 21171 RVA: 0x00196298 File Offset: 0x00196298
		public GenericVar(uint number, TypeDef genericParamProvider) : base(true, number, genericParamProvider)
		{
		}

		// Token: 0x060052B4 RID: 21172 RVA: 0x001962A4 File Offset: 0x001962A4
		public GenericVar(int number, TypeDef genericParamProvider) : base(true, (uint)number, genericParamProvider)
		{
		}
	}
}
