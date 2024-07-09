using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x020007BA RID: 1978
	[Flags]
	[ComVisible(true)]
	public enum GenericParamAttributes : ushort
	{
		// Token: 0x040024FA RID: 9466
		VarianceMask = 3,
		// Token: 0x040024FB RID: 9467
		NonVariant = 0,
		// Token: 0x040024FC RID: 9468
		Covariant = 1,
		// Token: 0x040024FD RID: 9469
		Contravariant = 2,
		// Token: 0x040024FE RID: 9470
		SpecialConstraintMask = 28,
		// Token: 0x040024FF RID: 9471
		NoSpecialConstraint = 0,
		// Token: 0x04002500 RID: 9472
		ReferenceTypeConstraint = 4,
		// Token: 0x04002501 RID: 9473
		NotNullableValueTypeConstraint = 8,
		// Token: 0x04002502 RID: 9474
		DefaultConstructorConstraint = 16
	}
}
