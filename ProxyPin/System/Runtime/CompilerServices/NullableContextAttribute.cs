using System;
using Microsoft.CodeAnalysis;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000A64 RID: 2660
	[CompilerGenerated]
	[Newtonsoft.Json.Embedded]
	[AttributeUsage(AttributeTargets.Module | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Method | AttributeTargets.Interface | AttributeTargets.Delegate, AllowMultiple = false, Inherited = false)]
	internal sealed class NullableContextAttribute : Attribute
	{
		// Token: 0x0600682A RID: 26666 RVA: 0x001FB138 File Offset: 0x001FB138
		public NullableContextAttribute(byte A_1)
		{
			this.Flag = A_1;
		}

		// Token: 0x04003502 RID: 13570
		public readonly byte Flag;
	}
}
