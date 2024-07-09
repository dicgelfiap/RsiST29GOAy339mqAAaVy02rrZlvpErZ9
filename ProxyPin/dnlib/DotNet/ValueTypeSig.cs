using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x0200086B RID: 2155
	[ComVisible(true)]
	public sealed class ValueTypeSig : ClassOrValueTypeSig
	{
		// Token: 0x170010ED RID: 4333
		// (get) Token: 0x060052A1 RID: 21153 RVA: 0x00196168 File Offset: 0x00196168
		public override ElementType ElementType
		{
			get
			{
				return ElementType.ValueType;
			}
		}

		// Token: 0x060052A2 RID: 21154 RVA: 0x0019616C File Offset: 0x0019616C
		public ValueTypeSig(ITypeDefOrRef typeDefOrRef) : base(typeDefOrRef)
		{
		}
	}
}
