using System;
using Microsoft.CodeAnalysis;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000C8E RID: 3214
	[CompilerGenerated]
	[System.Collections.Immutable.Embedded]
	[AttributeUsage(AttributeTargets.Module, AllowMultiple = false, Inherited = false)]
	internal sealed class NullablePublicOnlyAttribute : Attribute
	{
		// Token: 0x06008083 RID: 32899 RVA: 0x00260988 File Offset: 0x00260988
		public NullablePublicOnlyAttribute(bool A_1)
		{
			this.IncludesInternals = A_1;
		}

		// Token: 0x04003D1C RID: 15644
		public readonly bool IncludesInternals;
	}
}
