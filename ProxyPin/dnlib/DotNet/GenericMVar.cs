using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x0200086F RID: 2159
	[ComVisible(true)]
	public sealed class GenericMVar : GenericSig
	{
		// Token: 0x170010F9 RID: 4345
		// (get) Token: 0x060052B5 RID: 21173 RVA: 0x001962B0 File Offset: 0x001962B0
		public override ElementType ElementType
		{
			get
			{
				return ElementType.MVar;
			}
		}

		// Token: 0x060052B6 RID: 21174 RVA: 0x001962B4 File Offset: 0x001962B4
		public GenericMVar(uint number) : base(false, number)
		{
		}

		// Token: 0x060052B7 RID: 21175 RVA: 0x001962C0 File Offset: 0x001962C0
		public GenericMVar(int number) : base(false, (uint)number)
		{
		}

		// Token: 0x060052B8 RID: 21176 RVA: 0x001962CC File Offset: 0x001962CC
		public GenericMVar(uint number, MethodDef genericParamProvider) : base(false, number, genericParamProvider)
		{
		}

		// Token: 0x060052B9 RID: 21177 RVA: 0x001962D8 File Offset: 0x001962D8
		public GenericMVar(int number, MethodDef genericParamProvider) : base(false, (uint)number, genericParamProvider)
		{
		}
	}
}
