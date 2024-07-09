using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x02000A69 RID: 2665
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
	internal class DoesNotReturnIfAttribute : Attribute
	{
		// Token: 0x06006830 RID: 26672 RVA: 0x001FB178 File Offset: 0x001FB178
		public DoesNotReturnIfAttribute(bool parameterValue)
		{
			this.ParameterValue = parameterValue;
		}

		// Token: 0x170015FA RID: 5626
		// (get) Token: 0x06006831 RID: 26673 RVA: 0x001FB188 File Offset: 0x001FB188
		public bool ParameterValue { get; }
	}
}
