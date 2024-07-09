using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x0200086C RID: 2156
	[ComVisible(true)]
	public sealed class ClassSig : ClassOrValueTypeSig
	{
		// Token: 0x170010EE RID: 4334
		// (get) Token: 0x060052A3 RID: 21155 RVA: 0x00196178 File Offset: 0x00196178
		public override ElementType ElementType
		{
			get
			{
				return ElementType.Class;
			}
		}

		// Token: 0x060052A4 RID: 21156 RVA: 0x0019617C File Offset: 0x0019617C
		public ClassSig(ITypeDefOrRef typeDefOrRef) : base(typeDefOrRef)
		{
		}
	}
}
